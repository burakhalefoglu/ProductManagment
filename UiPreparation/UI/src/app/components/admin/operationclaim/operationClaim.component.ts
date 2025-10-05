import {Component, AfterViewInit, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import { OperationClaim } from './models/operationclaim';
import { OperationClaimService } from './services/operationclaim.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {TitleService} from '../../../core/services/title.service';

declare var jQuery: any;

@Component({
    selector: 'app-operationClaim',
    templateUrl: './operationClaim.component.html',
    styleUrls: ['./operationClaim.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        ReactiveFormsModule,
    ]
})
export class OperationClaimComponent implements AfterViewInit, OnInit {

    operationClaimList: OperationClaim[] = [];
    filteredData: OperationClaim[] = [];

    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'id';
    currentSortDirection: 'asc' | 'desc' = 'asc';


    displayedColumns: string[] = ['id', 'name', 'alias', 'description', 'update'];

    operationClaim: OperationClaim = new OperationClaim();

    operationClaimAddForm!: FormGroup;
    constructor(private operationClaimService: OperationClaimService,
                private alertifyService: AlertifyService,
                private formBuilder: FormBuilder,
                private authService: AuthService,
                private titleService: TitleService,) { }

    ngAfterViewInit(): void {
        this.getOperationClaimList();
    }

    ngOnInit() {
        this.createOperationClaimAddForm();
        this.titleService.setPageTitle('Operation Claim');
    }

    getOperationClaimList() {
        this.operationClaimService.getOperationClaimList().subscribe(data => {
            this.operationClaimList = data;
            this.filteredData = data;
            this.paginator.length = data.length;
            this.sortData(this.currentSortColumn, true);
        });
    }

    save() {
        if (this.operationClaimAddForm.valid) {
            this.operationClaim = Object.assign({}, this.operationClaimAddForm.value)
            this.updateOperationClaim();
        }
    }

    updateOperationClaim() {
        this.operationClaimService.updateOperationClaim(this.operationClaim).subscribe(data => {

            const index = this.operationClaimList.findIndex(x => x.id === this.operationClaim.id);
            if (index !== -1) {
                this.operationClaimList[index] = this.operationClaim;
                this.filteredData = [...this.operationClaimList];
                this.sortData(this.currentSortColumn, true);
            }

            this.operationClaim = new OperationClaim();
            jQuery('#operationclaim').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.operationClaimAddForm);
        })
    }

    createOperationClaimAddForm() {
        this.operationClaimAddForm = this.formBuilder.group({
            id: [0],
            name: [''],
            alias: [''],
            description: ['']
        })
    }

    getOperationClaimById(operationClaimId: number) {
        this.clearFormGroup(this.operationClaimAddForm);
        this.operationClaimService.getOperationClaim(operationClaimId).subscribe(data => {
            this.operationClaimAddForm.patchValue(data);
            this.operationClaim = data;
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

        this.filteredData = this.operationClaimList.filter(claim => {
            const nameMatch = claim.name ? claim.name.toLowerCase().includes(filterValue) : false;
            const aliasMatch = claim.alias ? claim.alias.toLowerCase().includes(filterValue) : false;
            const descriptionMatch = claim.description ? claim.description.toLowerCase().includes(filterValue) : false;

            const idMatch = claim.id !== undefined && claim.id !== null
                ? claim.id.toString().includes(filterValue)
                : false;

            return nameMatch || aliasMatch || descriptionMatch || idMatch;
        });

        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0;
        this.sortData(this.currentSortColumn, true);
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
