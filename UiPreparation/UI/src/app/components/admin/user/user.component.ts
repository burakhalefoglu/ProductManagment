import {
    AfterViewInit,
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

import { User } from './models/User';
import { PasswordDto } from './models/passwordDto';
import { UserService } from './Services/User.service';
import { AuthService } from '../../public/login/Services/Auth.service';
import { AlertifyService } from '../../../core/services/Alertify.service';
import { LookUp } from '../../../core/models/LookUp';
import { LookUpService } from '../../../core/services/LookUp.service';
import { TitleService } from '../../../core/services/title.service';
import { MustMatch } from '../../../core/directives/must-match';

// DevExtreme
import {
    DxDataGridModule,
    DxPopupModule,
    DxButtonModule,
    DxFormModule,
    DxValidatorModule,
    DxTagBoxModule,
    DxLoadIndicatorModule,
} from 'devextreme-angular';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
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
        DxTagBoxModule,
        DxLoadIndicatorModule,
    ],
})
export class UserComponent implements AfterViewInit, OnInit {
    userList: User[] = [];
    isLoading = false;

    // popuplar
    userFormVisible = false;
    pwdFormVisible = false;
    groupPermVisible = false;
    claimPermVisible = false;
    deleteConfirmVisible = false;

    // formlar & modeller
    userAddForm!: FormGroup;
    passwordForm!: FormGroup;

    userModel!: User;
    activeUserId?: number;

    // lookup & seçimler
    groupDropdownList: LookUp[] = [];
    claimDropdownList: LookUp[] = [];
    groupSelectedIds: number[] = [];
    claimSelectedIds: number[] = [];

    constructor(
        private userService: UserService,
        private formBuilder: FormBuilder,
        private alertifyService: AlertifyService,
        private lookUpService: LookUpService,
        private authService: AuthService,
        private titleService: TitleService,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.titleService.setPageTitle('Users');
        this.createUserAddForm();
        this.createPasswordForm();

        this.lookUpService.getGroupLookUp().subscribe({
            next: (data) => { this.groupDropdownList = Array.isArray(data) ? [...data] : []; this.cdr.markForCheck(); },
            error: () => this.cdr.markForCheck(),
        });

        this.lookUpService.getOperationClaimLookUp().subscribe({
            next: (data) => { this.claimDropdownList = Array.isArray(data) ? [...data] : []; this.cdr.markForCheck(); },
            error: () => this.cdr.markForCheck(),
        });
    }

    ngAfterViewInit(): void {
        this.getUserList();
    }

    // === Data ===
    getUserList(): void {
        this.isLoading = true;
        this.userService.getUserList().subscribe({
            next: (data) => {
                this.userList = Array.isArray(data) ? [...data] : [];
                this.isLoading = false;
                this.cdr.markForCheck();
            },
            error: () => { this.isLoading = false; this.cdr.markForCheck(); },
        });
    }

    // === Formlar ===
    createUserAddForm(): void {
        this.userAddForm = this.formBuilder.group({
            userId: [0],
            fullName: ['', Validators.required],
            email: ['', Validators.required],
            address: [''],
            notes: [''],
            mobilePhones: [''],
            status: [true],
        });
    }

    createPasswordForm(): void {
        this.passwordForm = this.formBuilder.group(
            {
                password: ['', Validators.required],
                confirmPassword: ['', Validators.required],
            },
            { validators: MustMatch('password', 'confirmPassword') }
        );
    }

    clearFormGroup(group: FormGroup): void {
        group.markAsUntouched();
        group.reset();
        Object.keys(group.controls).forEach((key) => {
            const control = group.get(key);
            if (!control) return;
            control.setErrors(null);
            if (key === 'userId') control.setValue(0);
            if (key === 'status') control.setValue(true);
        });
    }

    // === CRUD User ===
    openCreate(): void {
        this.clearFormGroup(this.userAddForm);
        this.userModel = { userId: 0, fullName: '', email: '', address: '', notes: '', mobilePhones: '', status: true } as User;
        this.userFormVisible = true;
    }

    openEdit(event: any): void {
        var id = event.row?.data.userId;
        if (id == null) return;
        this.clearFormGroup(this.userAddForm);
        this.userService.getUserById(id).subscribe({
            next: (data) => {
                this.userModel = data;
                this.userAddForm.patchValue(data);
                this.userFormVisible = true;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    saveUser(): void {
        if (!this.userAddForm.valid) return;
        this.userModel = { ...this.userModel, ...this.userAddForm.value };

        if (!this.userModel.userId || this.userModel.userId === 0) {
            this.userService.addUser(this.userModel).subscribe({
                next: (msg) => {
                    this.getUserList();
                    this.userFormVisible = false;
                    this.alertifyService.success(msg);
                    this.clearFormGroup(this.userAddForm);
                    this.cdr.markForCheck();
                },
                error: () => this.cdr.markForCheck(),
            });
        } else {
            this.userService.updateUser(this.userModel).subscribe({
                next: (msg) => {
                    const i = this.userList.findIndex((x) => x.userId === this.userModel.userId);
                    if (i !== -1) {
                        this.userList[i] = { ...this.userModel };
                        this.userList = [...this.userList];
                    }
                    this.userFormVisible = false;
                    this.alertifyService.success(msg);
                    this.clearFormGroup(this.userAddForm);
                    this.cdr.markForCheck();
                },
                error: () => this.cdr.markForCheck(),
            });
        }
    }

    askDelete(event: any): void {
        var id = event.row?.data.userId;
        if (id == null) return;
        this.activeUserId = id;
        this.deleteConfirmVisible = true;
    }

    deleteUser(id: number | undefined): void {
        if (id == null) return;
        this.userService.deleteUser(id).subscribe({
            next: (msg) => {
                this.alertifyService.success(msg.toString());
                const idx = this.userList.findIndex((x) => x.userId === id);
                if (idx !== -1) {
                    // senin akışında delete "status=false" yapıyor
                    this.userList[idx] = { ...this.userList[idx], status: false };
                    this.userList = [...this.userList];
                }
                this.deleteConfirmVisible = false;
                this.activeUserId = undefined;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    // === Şifre Değiştirme ===
    openPasswordChange(event: any): void {
        var id = event.row?.data.userId;
        if (id == null) return;
        this.activeUserId = id;
        this.clearFormGroup(this.passwordForm);
        this.pwdFormVisible = true;
    }

    savePassword(): void {
        if (!this.passwordForm.valid || this.activeUserId == null) return;
        const dto: PasswordDto = { userId: this.activeUserId, password: this.passwordForm.value.password } as PasswordDto;
        this.userService.saveUserPassword(dto).subscribe({
            next: (msg) => {
                this.activeUserId = undefined;
                this.pwdFormVisible = false;
                this.alertifyService.success(msg);
                this.clearFormGroup(this.passwordForm);
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    // === Grup / Claim Yetkileri ===
    openGroupPermissions(event: any): void {
        var id = event.row?.data.userId;
        if (id == null) return;
        this.activeUserId = id;
        this.userService.getUserGroupPermissions(id).subscribe({
            next: (data: LookUp[]) => {
                this.groupSelectedIds = (data || []).map((x) => x.id as number);
                this.groupPermVisible = true;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    openClaimPermissions(event: any): void {
        var id = event.row?.data.userId;
        if (id == null) return;
        this.activeUserId = id;
        this.userService.getUserClaims(id).subscribe({
            next: (data: LookUp[]) => {
                this.claimSelectedIds = (data || []).map((x) => x.id as number);
                this.claimPermVisible = true;
                this.cdr.markForCheck();
            },
            error: () => this.cdr.markForCheck(),
        });
    }

    saveUserGroupsPermissions(): void {
        const ids = this.groupSelectedIds || [];
        this.userService.saveUserGroupPermissions(this.activeUserId, ids).subscribe({
            next: (x) => { this.groupPermVisible = false; this.alertifyService.success(x); this.cdr.markForCheck(); },
            error: (err) => { this.alertifyService.error(err?.error); this.groupPermVisible = false; this.cdr.markForCheck(); },
        });
    }

    saveUserClaimsPermission(): void {
        const ids = this.claimSelectedIds || [];
        this.userService.saveUserClaims(this.activeUserId, ids).subscribe({
            next: (x) => { this.claimPermVisible = false; this.alertifyService.success(x); this.cdr.markForCheck(); },
            error: (err) => { this.alertifyService.error(err?.error); this.claimPermVisible = false; this.cdr.markForCheck(); },
        });
    }

    // === Auth ===
    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim);
    }
}
