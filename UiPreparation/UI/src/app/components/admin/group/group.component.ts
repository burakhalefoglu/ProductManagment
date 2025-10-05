import {AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit,} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {IDropdownSettings, NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import {Group} from './Models/Group';
import {GroupService} from './Services/Group.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {environment} from '../../../../environments/environment';
import {LookUp} from '../../../core/models/LookUp';
import {LookUpService} from '../../../core/services/LookUp.service';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {TitleService} from '../../../core/services/title.service';

import {
    DxButtonModule,
    DxDataGridModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxPopupModule,
    DxTagBoxModule,
    DxTextBoxModule,
    DxToolbarModule,
    DxValidatorModule,
} from 'devextreme-angular';

@Component({
    selector: 'app-group',
    templateUrl: './group.component.html',
    styleUrls: ['./group.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        ReactiveFormsModule,
        FormsModule,

        DxDataGridModule,
        DxButtonModule,
        DxPopupModule,
        DxTextBoxModule,
        DxFormModule,
        DxTagBoxModule,
        DxToolbarModule,
        DxValidatorModule,
        DxLoadIndicatorModule,

        NgMultiSelectDropDownModule,
    ],
})
export class GroupComponent implements AfterViewInit, OnInit {
    groupList: Group[] = [];

    isLoading = false;

    groupPopupVisible = false;
    groupUsersPopupVisible = false;
    groupClaimsPopupVisible = false;
    deleteConfirmVisible = false;

    pendingDeleteId?: number;

    userDropdownList: LookUp[] = [];
    claimDropdownList: LookUp[] = [];
    userSelectedIds: number[] = [];
    claimSelectedIds: number[] = [];

    group: Group = new Group();
    groupAddForm!: FormGroup;
    groupId!: number;

    dropdownSettings!: IDropdownSettings;

    constructor(
        private groupService: GroupService,
        private lookupService: LookUpService,
        private alertifyService: AlertifyService,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private titleService: TitleService,
        private cdr: ChangeDetectorRef,

    ) {}

    ngOnInit(): void {
        this.createGroupAddForm();
        this.titleService.setPageTitle('Groups'); // key ise çeviri pipe’ı template’te kullanılıyor

        this.dropdownSettings = environment.getDropDownSetting;

        this.lookupService.getOperationClaimLookUp().subscribe((data: LookUp[]) => {
            this.claimDropdownList = data;
        });

        this.lookupService.getUserLookUp().subscribe((data: LookUp[]) => {
            this.userDropdownList = data;
        });
    }

    ngAfterViewInit(): void {
        this.getGroupList();
    }

    // === Data ===
    getGroupList(): void {
        this.isLoading = true;
        this.groupService.getGroupList().subscribe({
            next: (data) => {
                // yeni referans ver
                this.groupList = Array.isArray(data) ? [...data] : [];
                this.isLoading = false;
                // OnPush tetikle
                this.cdr.markForCheck();
            },
            error: () => {
                this.isLoading = false;
                this.cdr.markForCheck();
            },
        });
    }


    save(): void {
        if (!this.groupAddForm.valid) return;

        this.group = Object.assign({}, this.groupAddForm.value);
        if (this.group.id === 0) {
            this.addGroup();
        } else {
            this.updateGroup();
        }
    }

    addGroup(): void {
        this.groupService.addGroup(this.group).subscribe((data) => {
            this.getGroupList();
            this.group = new Group();
            this.groupPopupVisible = false;
            this.alertifyService.success(data);
            this.clearFormGroup(this.groupAddForm);
        });
    }

    updateGroup(): void {
        this.groupService.updateGroup(this.group).subscribe((data) => {
            const index = this.groupList.findIndex((x) => x.id === this.group.id);
            if (index !== -1) {
                this.groupList[index] = this.group;
                this.groupList = [...this.groupList]; // change detection
            }
            this.group = new Group();
            this.groupPopupVisible = false;
            this.alertifyService.success(data);
            this.clearFormGroup(this.groupAddForm);
        });
    }

    deleteGroup(groupId: number): void {
        this.groupService.deleteGroup(groupId).subscribe((data) => {
            this.alertifyService.success(data.toString());
            this.groupList = this.groupList.filter((x) => x.id !== groupId);
            this.deleteConfirmVisible = false;
            this.pendingDeleteId = undefined;
        });
    }

    createGroupAddForm(): void {
        this.groupAddForm = this.formBuilder.group({
            id: [0],
            groupName: ['', Validators.required],
        });
    }

    clearFormGroup(group: FormGroup): void {
        group.markAsUntouched();
        group.reset();
        Object.keys(group.controls).forEach((key) => {
            const control = group.get(key)!;
            control.setErrors(null);
            if (key === 'id') control.setValue(0);
        });
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim);
    }

    openCreate(): void {
        this.clearFormGroup(this.groupAddForm);
        this.group = new Group();
        this.groupPopupVisible = true;
    }

    openEdit(event: any): void {
        this.groupService.getGroupById(event.row.data.id).subscribe((data) => {
            this.group = data;
            this.groupAddForm.patchValue(data);
            this.groupPopupVisible = true;
        });
    }

    askDelete(event: any): void {
        this.pendingDeleteId = event.row.data.id;
        this.deleteConfirmVisible = true;
    }

    getGroupUsers(event: any): void {
        this.groupService.getGroupUsers(event.row.data.id).subscribe((data: LookUp[]) => {
            this.userSelectedIds = (data || []).map((x) => x.id as number);
            this.groupUsersPopupVisible = true;
        });
    }

    getGroupClaims(event: any): void {
        this.groupService.getGroupClaims(event.row.data.id).subscribe((data: LookUp[]) => {
            this.claimSelectedIds = (data || []).map((x) => x.id as number);
            this.groupClaimsPopupVisible = true;
        });
    }

    saveGroupUsers(): void {
        const ids = this.userSelectedIds || [];
        this.groupService.saveGroupUsers(this.groupId, ids).subscribe((x) => {
            this.groupUsersPopupVisible = false;
            this.alertifyService.success(x);
        });
    }

    saveGroupClaims(): void {
        const ids = this.claimSelectedIds || [];
        this.groupService.saveGroupClaims(this.groupId, ids).subscribe((x) => {
            this.groupClaimsPopupVisible = false;
            this.alertifyService.success(x);
        });
    }
}
