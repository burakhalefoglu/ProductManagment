import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

import { CustomTranslateService } from './services/custom_translate.service';
import { Translate as TranslateItem } from './models/translate';
import { AuthService } from '../../public/login/Services/Auth.service';
import { AlertifyService } from '../../../core/services/Alertify.service';
import { LookUp } from '../../../core/models/LookUp';
import { LookUpService } from '../../../core/services/LookUp.service';
import { TitleService } from '../../../core/services/title.service';

// DevExtreme
import {
    DxDataGridModule,
    DxPopupModule,
    DxButtonModule,
    DxFormModule,
    DxValidatorModule,
    DxSelectBoxModule,
    DxLoadIndicatorModule,
} from 'devextreme-angular';

@Component({
    selector: 'app-translate',
    templateUrl: './translate.component.html',
    styleUrls: ['./translate.component.scss'],
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
        DxSelectBoxModule,
        DxLoadIndicatorModule,
    ],
})
export class TranslateComponent implements AfterViewInit, OnInit {
    translateList: TranslateItem[] = [];
    langugelookUp: LookUp[] = [];

    // UI state
    isLoading = false;
    formPopupVisible = false;
    deleteConfirmVisible = false;

    // form/model
    translateAddForm!: FormGroup;
    translateModel: TranslateItem = new TranslateItem();

    // silme için bekleyen id
    pendingDeleteId?: number;

    constructor(
        private translateService: CustomTranslateService,
        private lookupService: LookUpService,
        private alertifyService: AlertifyService,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private titleService: TitleService,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.titleService.setPageTitle('Translations');
        this.createTranslateAddForm();

        // Dil lookup
        this.lookupService.getLanguageLookup().subscribe({
            next: (data) => {
                this.langugelookUp = Array.isArray(data) ? [...data] : [];
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    ngAfterViewInit(): void {
        this.getTranslateList();
    }

    // === Data ===
    getTranslateList(): void {
        this.isLoading = true;
        this.translateService.getTranslateList().subscribe({
            next: (data) => {
                this.translateList = Array.isArray(data) ? [...data] : [];
                this.isLoading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.isLoading = false;
                this.cdr.markForCheck();
            },
        });
    }

    // === CRUD ===
    openCreate(): void {
        this.clearFormGroup(this.translateAddForm);
        this.translateModel = new TranslateItem();
        this.formPopupVisible = true;
    }

    openEdit(event:any): void {
        var id = event.row?.data.id;
        if (id == null) return;
        this.clearFormGroup(this.translateAddForm);
        this.translateService.getTranslateById(id).subscribe({
            next: (data) => {
                this.translateModel = data;
                this.translateAddForm.patchValue(data);
                this.formPopupVisible = true;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    save(): void {
        if (!this.translateAddForm.valid) return;

        const payload = { ...this.translateModel, ...this.translateAddForm.value } as TranslateItem;

        if (!payload.id || payload.id === 0) {
            this.addTranslate(payload);
        } else {
            this.updateTranslate(payload);
        }
    }

    addTranslate(model: TranslateItem): void {
        this.translateService.addTranslate(model).subscribe({
            next: (msg) => {
                this.getTranslateList();
                this.translateModel = new TranslateItem();
                this.formPopupVisible = false;
                this.alertifyService.success(msg);
                this.clearFormGroup(this.translateAddForm);
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    updateTranslate(model: TranslateItem): void {
        this.translateService.updateTranslate(model).subscribe({
            next: (msg) => {
                // yerinde güncelle
                const i = this.translateList.findIndex(x => x.id === model.id);
                if (i !== -1) {
                    this.translateList[i] = { ...model };
                    this.translateList = [...this.translateList]; // OnPush
                }
                this.translateModel = new TranslateItem();
                this.formPopupVisible = false;
                this.alertifyService.success(msg);
                this.clearFormGroup(this.translateAddForm);
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    askDelete(event: any): void {
        var id = event.row?.data.id;
        if (id == null) return;
        this.pendingDeleteId = id;
        this.deleteConfirmVisible = true;
    }

    deleteTranslate(id: number | undefined): void {
        if (id == null) return;
        this.translateService.deleteTranslate(id).subscribe({
            next: (msg) => {
                this.alertifyService.success(msg.toString());
                this.translateList = this.translateList.filter(x => x.id !== id);
                this.deleteConfirmVisible = false;
                this.pendingDeleteId = undefined;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    // === Helpers ===
    createTranslateAddForm(): void {
        this.translateAddForm = this.formBuilder.group({
            id: [0],
            langId: [0, Validators.required],
            code: ['', Validators.required],
            value: ['', Validators.required],
        });
    }

    clearFormGroup(group: FormGroup): void {
        group.markAsUntouched();
        group.reset();
        Object.keys(group.controls).forEach((key) => {
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
