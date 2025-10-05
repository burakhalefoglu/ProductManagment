import {AfterViewInit, ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { CustomTranslateService } from './services/custom_translate.service';
import { Translate } from './models/translate';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {SwalComponent, SwalDirective} from '@sweetalert2/ngx-sweetalert2';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {LookUp} from '../../../core/models/LookUp';
import {LookUpService} from '../../../core/services/LookUp.service';
import {TitleService} from '../../../core/services/title.service';


declare let jQuery: any;

@Component({
    selector: 'app-translate',
    templateUrl: './translate.component.html',
    styleUrls: ['./translate.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        SwalDirective,
        ReactiveFormsModule,
        SwalComponent,
    ]
})
export class TranslateComponent implements  AfterViewInit, OnInit {
    translateList: Translate[] = [];
    filteredData: Translate[] = [];
    translate: Translate = new Translate();

    translateAddForm!: FormGroup;

    langugelookUp!: LookUp[];

    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'id';
    currentSortDirection: 'asc' | 'desc' = 'asc';

    constructor(private translateService: CustomTranslateService,
                private lookupService: LookUpService,
                private alertifyService: AlertifyService,
                private formBuilder: FormBuilder,
                private authService: AuthService,
                private titleService: TitleService) { }

    ngAfterViewInit(): void {
        this.getTranslateList();
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

        this.filteredData = this.translateList.filter(t => {
            const idMatch = t.id !== undefined && t.id !== null ? t.id.toString().includes(filterValue) : false;
            const codeMatch = t.code ? t.code.toLowerCase().includes(filterValue) : false;
            const valueMatch = t.value ? t.value.toLowerCase().includes(filterValue) : false;
            return idMatch || codeMatch || valueMatch;
        });

        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0;
        this.sortData(this.currentSortColumn, true);
    }


    ngOnInit() {
        this.lookupService.getLanguageLookup().subscribe(data => {
            this.langugelookUp = data;
        })
        this.titleService.setPageTitle('Translations');
        this.createTranslateAddForm();
    }


    getTranslateList() {
        this.translateService.getTranslateList().subscribe(data => {
            this.translateList = data;
            this.filteredData = data;
            this.paginator.length = data.length;
            this.sortData(this.currentSortColumn, true);
        });
    }

    save() {
        if (this.translateAddForm.valid) {
            this.translate = Object.assign({}, this.translateAddForm.value)

            if (this.translate.id === 0) {
                this.addTranslate();
            } else {
                this.updateTranslate();
            }
        }
    }

    addTranslate() {
        this.translateService.addTranslate(this.translate).subscribe(data => {
            this.getTranslateList(); // Yeniden yükleme en kolay yol
            this.translate = new Translate();
            jQuery('#translate').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.translateAddForm);
        })
    }

    updateTranslate() {
        this.translateService.updateTranslate(this.translate).subscribe(data => {

            const index = this.translateList.findIndex(x => x.id === this.translate.id);
            if (index !== -1) {
                this.translateList[index] = this.translate;
                this.filteredData = [...this.translateList];
                this.sortData(this.currentSortColumn, true);
            }

            this.translate = new Translate();
            jQuery('#translate').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.translateAddForm);
        })
    }

    createTranslateAddForm() {
        this.translateAddForm = this.formBuilder.group({
            id: [0],
            langId: [0, Validators.required],
            code: ['', Validators.required],
            value: ['', Validators.required]
        })
    }

    deleteTranslate(translateId: number | undefined) {
        this.translateService.deleteTranslate(translateId).subscribe(data => {
            this.alertifyService.success(data.toString());

            this.translateList = this.translateList.filter(x => x.id !== translateId);
            this.filteredData = [...this.translateList];
            this.paginator.length = this.translateList.length;
            this.sortData(this.currentSortColumn, true);
        })
    }

    getTranslateById(translateId: number | undefined) {
        this.clearFormGroup(this.translateAddForm);
        this.translateService.getTranslateById(translateId).subscribe(data => {
            this.translate = data;
            this.translateAddForm.patchValue(data);
        })
    }


    clearFormGroup(group: FormGroup) {
        group.markAsUntouched();
        group.reset();

        Object.keys(group.controls).forEach(key => {
            const control = group.get(key);
            if (control) {
                control.setErrors(null);
                if (key === 'id') {
                    control.setValue(0);
                }
            }
        });
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim)
    }

    sortData(sortColumn: string, isInitialSort = false): void {
        if (this.currentSortColumn === sortColumn && !isInitialSort) {
            this.currentSortDirection = this.currentSortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.currentSortDirection = 'asc';
        }

        this.currentSortColumn = sortColumn;

        this.filteredData.sort((a, b) => {
            const aValue = (a as any)[sortColumn];
            const bValue = (b as any)[sortColumn];

            let comparison = 0;
            if (aValue > bValue) {
                comparison = 1;
            } else if (aValue < bValue) {
                comparison = -1;
            }

            return this.currentSortDirection === 'asc' ? comparison : comparison * -1;
        });
    }

    onPageChange(event: { pageIndex: number, pageSize: number }) {
        this.paginator.pageIndex = event.pageIndex;
        this.paginator.pageSize = event.pageSize;
    }

    getTotalPages(): number {
        return Math.ceil(this.paginator.length / this.paginator.pageSize);
    }

    getPages(): number[] {
        const total = this.getTotalPages();
        return Array.from({ length: total }, (_, i) => i + 1);
    }

}
