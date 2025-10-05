import {AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

import { Language } from './models/language';
import { LanguageService } from './services/language.service';
import { AuthService } from '../../public/login/Services/Auth.service';
import { AlertifyService } from '../../../core/services/Alertify.service';
import { TitleService } from '../../../core/services/title.service';

// DevExtreme
import {
    DxDataGridModule,
    DxButtonModule,
    DxPopupModule,
    DxFormModule,
    DxTextBoxModule,
    DxToolbarModule,
    DxValidatorModule,
    DxLoadIndicatorModule,
} from 'devextreme-angular';

@Component({
    selector: 'app-language',
    templateUrl: './language.component.html',
    styleUrls: ['./language.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        ReactiveFormsModule,

        // DevExtreme
        DxDataGridModule,
        DxButtonModule,
        DxPopupModule,
        DxFormModule,
        DxTextBoxModule,
        DxToolbarModule,
        DxValidatorModule,
        DxLoadIndicatorModule,
    ],
})
export default class LanguageComponent implements AfterViewInit, OnInit {
    languageList: Language[] = [];

    // UI state
    isLoading = false;
    formPopupVisible = false;
    deleteConfirmVisible = false;

    // form/model
    language: Language = new Language();
    languageAddForm!: FormGroup;

    // silme için bekleyen id
    pendingDeleteId?: number;

    constructor(
        private languageService: LanguageService,
        private alertifyService: AlertifyService,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private titleService: TitleService,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.createLanguageAddForm();
        this.titleService.setPageTitle('Languages'); // başlık key'i, template'te translate var
    }

    ngAfterViewInit(): void {
        this.getLanguageList();
    }

    // === Data ===
    getLanguageList(): void {
        this.isLoading = true;
        this.languageService.getLanguageList().subscribe({
            next: (data) => {
                // 1) Alan adlarını hızlı doğrula
                console.log('first row keys:', data?.[0] && Object.keys(data[0]));

                // 2) Yeni referans ver
                this.languageList = Array.isArray(data) ? [...data] : [];

                // 3) OnPush tetikle
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
    save(): void {
        if (!this.languageAddForm.valid) return;

        this.language = Object.assign({}, this.languageAddForm.value);

        if (!this.language.id || this.language.id === 0) {
            this.addLanguage();
        } else {
            this.updateLanguage();
        }
    }

    addLanguage(): void {
        this.languageService.addLanguage(this.language).subscribe((data) => {
            this.getLanguageList();
            this.language = new Language();
            this.formPopupVisible = false;
            this.alertifyService.success(data);
            this.clearFormGroup(this.languageAddForm);
        });
    }

    updateLanguage(): void {
        this.languageService.updateLanguage(this.language).subscribe((data) => {
            const index = this.languageList.findIndex((x) => x.id === this.language.id);
            if (index !== -1) {
                this.languageList[index] = this.language;
                this.languageList = [...this.languageList]; // CD
            }
            this.language = new Language();
            this.formPopupVisible = false;
            this.alertifyService.success(data);
            this.clearFormGroup(this.languageAddForm);
        });
    }

    askDelete(event: any): void {
        var languageId = event.row?.data.id;
        if (languageId == null) return;
        this.pendingDeleteId = languageId;
        this.deleteConfirmVisible = true;
    }

    deleteLanguage(languageId: number | undefined): void {
        if (languageId == null) return;
        this.languageService.deleteLanguage(languageId).subscribe((data) => {
            this.alertifyService.success(data.toString());
            this.languageList = this.languageList.filter((x) => x.id !== languageId);
            this.deleteConfirmVisible = false;
            this.pendingDeleteId = undefined;
        });
    }
    openCreate(): void {
        this.clearFormGroup(this.languageAddForm);
        this.language = new Language();
        this.formPopupVisible = true;
    }

    openEdit(event : any): void {
        var languageId = event.row?.data.id;
        if (languageId == null) return;
        this.clearFormGroup(this.languageAddForm);
        this.languageService.getLanguage(languageId).subscribe((data) => {
            this.language = data;
            this.languageAddForm.patchValue(data);
            this.formPopupVisible = true;
        });
    }

    // === Helpers ===
    createLanguageAddForm(): void {
        this.languageAddForm = this.formBuilder.group({
            id: [0],
            name: ['', Validators.required],
            code: ['', Validators.required],
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
