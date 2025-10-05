import {
    AfterViewInit, ChangeDetectionStrategy,
    Component,
    OnInit,
} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {IDropdownSettings, NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import { PasswordDto } from './models/passwordDto';
import { User } from './models/User';
import { UserService } from './Services/User.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {SwalComponent, SwalDirective} from '@sweetalert2/ngx-sweetalert2';
import {LookUp} from '../../../core/models/LookUp';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {LookUpService} from '../../../core/services/LookUp.service';
import {environment} from '../../../../environments/environment';
import {MustMatch} from '../../../core/directives/must-match';
import {TitleService} from '../../../core/services/title.service';

declare var jQuery: any;

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        NgMultiSelectDropDownModule,
        FormsModule,
        ReactiveFormsModule,
        SwalDirective,
        SwalComponent,
    ]
})
export class UserComponent implements AfterViewInit, OnInit {

    userList: User[] = [];
    filteredData: User[] = [];

    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'userId';
    currentSortDirection: 'asc' | 'desc' = 'asc';

    displayedColumns: string[] = [
        'userId',
        'email',
        'fullName',
        'status',
        'mobilePhones',
        'address',
        'notes',
        'passwordChange',
        'updateClaim',
        'updateGroupClaim',
        'update',
        'delete',
    ];

    user!: User;
    groupDropdownList!: LookUp[];
    groupSelectedItems!: LookUp[];
    dropdownSettings!: IDropdownSettings;

    claimDropdownList!: LookUp[];
    claimSelectedItems!: LookUp[];

    isGroupChange = false;
    isClaimChange = false;

    userId!: number | undefined;

    userAddForm!: FormGroup;
    passwordForm!: FormGroup;

    constructor(
        private userService: UserService,
        private formBuilder: FormBuilder,
        private alertifyService: AlertifyService,
        private lookUpService: LookUpService,
        private authService: AuthService,
        private titleService: TitleService,
    ) {}

    ngAfterViewInit(): void {
        this.getUserList();
    }

    ngOnInit() {
        this.titleService.setPageTitle('Users');
        this.createUserAddForm();
        this.createPasswordForm();

        this.dropdownSettings = environment.getDropDownSetting;

        this.lookUpService.getGroupLookUp().subscribe((data) => {
            this.groupDropdownList = data;
        });

        this.lookUpService.getOperationClaimLookUp().subscribe((data) => {
            this.claimDropdownList = data;
        });
    }

    getUserGroupPermissions(userId: number | undefined) {
        this.userId = userId;

        this.userService.getUserGroupPermissions(userId).subscribe((data) => {
            this.groupSelectedItems = data;
        });
    }

    getUserClaimsPermissions(userId: number | undefined) {
        this.userId = userId;

        this.userService.getUserClaims(userId).subscribe((data) => {
            this.claimSelectedItems = data;
        });
    }

    saveUserGroupsPermissions() {
        if (this.isGroupChange) {
            const ids = this.groupSelectedItems.map(function (x) {
                return x.id as number;
            });
            this.userService.saveUserGroupPermissions(this.userId, ids).subscribe(
                (x) => {
                    jQuery('#groupPermissions').modal('hide');
                    this.isGroupChange = false;
                    this.alertifyService.success(x);
                },
                (error) => {
                    this.alertifyService.error(error.error);
                    jQuery('#groupPermissions').modal('hide');
                }
            );
        }
    }

    saveUserClaimsPermission() {
        if (this.isClaimChange) {
            const ids = this.claimSelectedItems.map(function (x) {
                return x.id as number;
            });
            this.userService.saveUserClaims(this.userId, ids).subscribe(
                (x) => {
                    jQuery('#claimsPermissions').modal('hide');
                    this.isClaimChange = false;
                    this.alertifyService.success(x);
                },
                (error) => {
                    this.alertifyService.error(error.error);
                    jQuery('#claimsPermissions').modal('hide');
                }
            );
        }
    }

    onItemSelect(comboType: string) {
        this.setComboStatus(comboType);
    }

    onSelectAll(comboType: string) {
        this.setComboStatus(comboType);
    }
    onItemDeSelect(comboType: string) {
        this.setComboStatus(comboType);
    }

    setComboStatus(comboType: string) {
        if (comboType === 'Group') { this.isGroupChange = true; } else if (comboType === 'Claim') { this.isClaimChange = true; }
    }

    createUserAddForm() {
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

    createPasswordForm() {
        this.passwordForm = this.formBuilder.group(
            {
                password: ['', Validators.required],
                confirmPassword: ['', Validators.required],
            },
            {
                validator: MustMatch('password', 'confirmPassword'),
            }
        );
    }

    getUserList() {
        this.userService.getUserList().subscribe((data) => {
            this.userList = data;
            this.filteredData = data; // Başlangıçta tüm veriler filtrelenmiş kabul edilir
            this.paginator.length = data.length;
            this.sortData(this.currentSortColumn, true); // Varsayılan sıralama
        });
    }

    clearFormGroup(group: FormGroup) {
        group.markAsUntouched();
        group.reset();

        Object.keys(group.controls).forEach((key) => {
            const control = group.get(key);
            if (control) {
                control.setErrors(null);
                if (key === 'userId') { control.setValue(0); } else if (key === 'status') { control.setValue(true); }
            }
        });
    }

    setUserId(id: number | undefined) {
        this.userId = id;
    }

    save() {
        if (this.userAddForm.valid) {
            this.user = Object.assign({}, this.userAddForm.value);

            if (this.user.userId === 0) { this.addUser(); } else { this.updateUser(); }
        }
    }

    savePassword() {
        if (this.passwordForm.valid) {
            const passwordDto: PasswordDto = new PasswordDto();
            passwordDto.userId = this.userId;
            passwordDto.password = this.passwordForm.value.password;

            this.userService.saveUserPassword(passwordDto).subscribe((data) => {
                this.userId = 0;
                jQuery('#passwordChange').modal('hide');
                this.alertifyService.success(data);
                this.clearFormGroup(this.passwordForm);
            });
        }
    }

    addUser() {
        this.userService.addUser(this.user).subscribe((data) => {
            this.getUserList();
            this.user = new User();
            jQuery('#user').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.userAddForm);
        });
    }

    getUserById(id: number | undefined) {
        this.clearFormGroup(this.userAddForm);
        this.userService.getUserById(id).subscribe((data) => {
            this.user = data;
            this.userAddForm.patchValue(data);
        });
    }

    updateUser() {
        this.userService.updateUser(this.user).subscribe((data) => {
            const index = this.userList.findIndex((x) => x.userId === this.user.userId);
            this.userList[index] = this.user;
            this.filteredData = [...this.userList];
            this.sortData(this.currentSortColumn, true);
            this.user = new User();
            jQuery('#user').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.userAddForm);
        });
    }

    deleteUser(id: number | undefined) {
        this.userService.deleteUser(id).subscribe((data) => {
            this.alertifyService.success(data.toString());
            // tslint:disable-next-line:triple-equals
            const index = this.userList.findIndex((x) => x.userId == id);
            this.userList[index].status = false;
            this.filteredData = [...this.userList];
            this.sortData(this.currentSortColumn, true);
        });
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim);
    }

    // configDataTable metodu kaldırıldı.

    // *** Yeni Manuel Sıralama Metodu ***
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

            // 'status' alanı boolean olduğu için özel işlem gerekebilir
            if (sortColumn === 'status') {
                comparison = aValue === bValue ? 0 : aValue ? 1 : -1;
            }

            return this.currentSortDirection === 'asc' ? comparison : comparison * -1;
        });
    }

    // *** Yeni Manuel Filtreleme Metodu ***
    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

        this.filteredData = this.userList.filter(user => {
            const idMatch = user.userId !== undefined && user.userId !== null ? user.userId.toString().includes(filterValue) : false;
            const emailMatch = user.email ? user.email.toLowerCase().includes(filterValue) : false;
            const fullNameMatch = user.fullName ? user.fullName.toLowerCase().includes(filterValue) : false;
            const mobilePhonesMatch = user.mobilePhones ? user.mobilePhones.toLowerCase().includes(filterValue) : false;
            const addressMatch = user.address ? user.address.toLowerCase().includes(filterValue) : false;
            const notesMatch = user.notes ? user.notes.toLowerCase().includes(filterValue) : false;

            // Status filtrelemesi (örneğin "true" veya "false" aratılabilir)
            const statusMatch = user.status !== undefined && user.status !== null
                ? user.status.toString().toLowerCase().includes(filterValue)
                : false;

            return idMatch || emailMatch || fullNameMatch || mobilePhonesMatch || addressMatch || notesMatch || statusMatch;
        });

        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0; // İlk sayfaya dön
        this.sortData(this.currentSortColumn, true); // Filtrelenmiş veriye sıralamayı uygula
    }

    // *** Sayfalama Yardımcı Metotları ***
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
