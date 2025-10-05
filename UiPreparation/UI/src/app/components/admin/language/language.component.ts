import {Component, OnInit, AfterViewInit, ChangeDetectionStrategy} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { Language } from './models/language';
import { LanguageService } from './services/language.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {SwalComponent, SwalDirective} from '@sweetalert2/ngx-sweetalert2';
import {LookUpService} from '../../../core/services/LookUp.service';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {TitleService} from '../../../core/services/title.service';

declare var jQuery: any;

@Component({
    selector: 'app-language',
    templateUrl: './language.component.html',
    styleUrls: ['./language.component.scss'],
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
class LanguageComponent implements AfterViewInit, OnInit {

    languageList: Language[] = [];
    filteredData: Language[] = [];

    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'id';
    currentSortDirection: 'asc' | 'desc' = 'asc';


    language: Language = new Language();

    languageAddForm!: FormGroup;

    constructor(private languageService: LanguageService,
                private alertifyService: AlertifyService,
                private formBuilder: FormBuilder,
                private authService: AuthService,
                private titleService: TitleService
                ) { }

    ngAfterViewInit(): void {
        this.getLanguageList();
    }

    ngOnInit() {
        this.createLanguageAddForm();
        this.titleService.setPageTitle('Languages');
    }

    getLanguageList() {
        this.languageService.getLanguageList().subscribe(data => {
            this.languageList = data;
            this.filteredData = data;
            this.paginator.length = data.length;
            this.sortData(this.currentSortColumn, true);
        });
    }

    save() {
        if (this.languageAddForm.valid) {
            this.language = Object.assign({}, this.languageAddForm.value)

            if (this.language.id === 0) {
                this.addLanguage();
            } else {
                this.updateLanguage();
            }
        }
    }

    addLanguage() {
        this.languageService.addLanguage(this.language).subscribe(data => {
            this.getLanguageList();
            this.language = new Language();
            jQuery('#language').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.languageAddForm);
        })
    }

    updateLanguage() {
        this.languageService.updateLanguage(this.language).subscribe(data => {
            const index = this.languageList.findIndex(x => x.id === this.language.id);
            if (index !== -1) {
                this.languageList[index] = this.language;
                this.filteredData = [...this.languageList];
                this.sortData(this.currentSortColumn, true);
            }

            this.language = new Language();
            jQuery('#language').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.languageAddForm);
        })
    }

    createLanguageAddForm() {
        this.languageAddForm = this.formBuilder.group({
            id: [0],
            name: ['', Validators.required],
            code: ['', Validators.required]
        })
    }

    deleteLanguage(languageId: number | undefined) {
        this.languageService.deleteLanguage(languageId).subscribe(data => {
            this.alertifyService.success(data.toString());
            this.languageList = this.languageList.filter(x => x.id !== languageId);
            this.filteredData = [...this.languageList];
            this.paginator.length = this.languageList.length;
            this.sortData(this.currentSortColumn, true);
        })
    }

    getLanguageById(languageId: number | undefined) {
        this.clearFormGroup(this.languageAddForm);
        this.languageService.getLanguage(languageId).subscribe(data => {
            this.language = data;
            this.languageAddForm.patchValue(data);
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

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
        this.filteredData = this.languageList.filter(lang => {
            const idMatch = lang.id !== undefined && lang.id !== null
                ? lang.id.toString().includes(filterValue)
                : false;

            const nameMatch = lang.name
                ? lang.name.toLowerCase().includes(filterValue)
                : false;

            const codeMatch = lang.code
                ? lang.code.toLowerCase().includes(filterValue)
                : false;

            return nameMatch || codeMatch || idMatch;
        });

        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0;
        this.sortData(this.currentSortColumn, true);
    }

// *** Yeni Sayfa Değiştirme Metodu ***
    onPageChange(event: { pageIndex: number, pageSize: number }) {
        this.paginator.pageIndex = event.pageIndex;
        this.paginator.pageSize = event.pageSize;
    }

// *** Sayfalama için yardımcı metot ***
    getTotalPages(): number {
        return Math.ceil(this.paginator.length / this.paginator.pageSize);
    }

    getPages(): number[] {
        const total = this.getTotalPages();
        return Array.from({ length: total }, (_, i) => i + 1);
    }

}

export default LanguageComponent
