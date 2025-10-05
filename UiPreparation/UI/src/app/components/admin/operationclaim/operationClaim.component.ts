import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

import { OperationClaim } from './models/operationclaim';
import { OperationClaimService } from './services/operationclaim.service';
import { AuthService } from '../../public/login/Services/Auth.service';
import { AlertifyService } from '../../../core/services/Alertify.service';
import { TitleService } from '../../../core/services/title.service';

// DevExtreme
import {
    DxDataGridModule,
    DxPopupModule,
    DxButtonModule,
    DxFormModule,
    DxValidatorModule,
    DxLoadIndicatorModule,
} from 'devextreme-angular';

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

        DxDataGridModule,
        DxPopupModule,
        DxButtonModule,
        DxFormModule,
        DxValidatorModule,
        DxLoadIndicatorModule,
    ],
})
export class OperationClaimComponent implements AfterViewInit, OnInit {

    operationClaimList: OperationClaim[] = [];
    isLoading = false;

    // popup & form
    formPopupVisible = false;
    operationClaimAddForm!: FormGroup;
    operationClaim: OperationClaim = new OperationClaim();

    constructor(
        private operationClaimService: OperationClaimService,
        private alertifyService: AlertifyService,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private titleService: TitleService,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.createOperationClaimAddForm();
        this.titleService.setPageTitle('Operation Claim');
    }

    ngAfterViewInit(): void {
        this.getOperationClaimList();
    }

    // === Data ===
    getOperationClaimList(): void {
        this.isLoading = true;
        this.operationClaimService.getOperationClaimList().subscribe({
            next: (data) => {
                this.operationClaimList = Array.isArray(data) ? [...data] : [];
                this.isLoading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.isLoading = false;
                this.cdr.markForCheck();
            },
        });
    }

    // === CRUD (sende sadece update var) ===
    openEdit(event: any): void {
        var operationClaimId = event.row?.data.id;
        if (operationClaimId == null) return;
        this.clearFormGroup(this.operationClaimAddForm);
        this.operationClaimService.getOperationClaim(operationClaimId).subscribe((data) => {
            this.operationClaim = data;
            this.operationClaimAddForm.patchValue(data);
            this.formPopupVisible = true;
            this.cdr.markForCheck();
        });
    }

    save(): void {
        if (!this.operationClaimAddForm.valid) return;
        this.operationClaim = { ...this.operationClaim, ...this.operationClaimAddForm.value };
        this.updateOperationClaim();
    }

    updateOperationClaim(): void {
        this.operationClaimService.updateOperationClaim(this.operationClaim).subscribe((data) => {
            const index = this.operationClaimList.findIndex(x => x.id === this.operationClaim.id);
            if (index !== -1) {
                this.operationClaimList[index] = { ...this.operationClaim };
                this.operationClaimList = [...this.operationClaimList]; // OnPush
            }
            this.formPopupVisible = false;
            this.alertifyService.success(data);
            this.clearFormGroup(this.operationClaimAddForm);
            this.operationClaim = new OperationClaim();
            this.cdr.markForCheck();
        });
    }

    // === Helpers ===
    createOperationClaimAddForm(): void {
        this.operationClaimAddForm = this.formBuilder.group({
            id: [0],
            name: [''],
            alias: ['', Validators.required],
            description: [''],
        });
    }

    clearFormGroup(group: FormGroup): void {
        group.markAsUntouched();
        group.reset();
        Object.keys(group.controls).forEach(key => {
            const control = group.get(key);
            if (control) {
                control.setErrors(null);
                if (key === 'id') control.setValue(0);
            }
        });
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim);
    }
}
