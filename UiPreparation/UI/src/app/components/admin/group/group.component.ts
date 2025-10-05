import {AfterViewInit, ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {IDropdownSettings, NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import { Group } from './Models/Group';
import { GroupService } from './Services/Group.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {SwalComponent, SwalDirective} from '@sweetalert2/ngx-sweetalert2';
import {environment} from '../../../../environments/environment';
import {LookUp} from '../../../core/models/LookUp';
import {LookUpService} from '../../../core/services/LookUp.service';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {TitleService} from '../../../core/services/title.service';


declare var jQuery: any;
@Component({
    selector: 'app-group',
    templateUrl: './group.component.html',
    styleUrls: ['./group.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        SwalDirective,
        ReactiveFormsModule,
        NgMultiSelectDropDownModule,
        FormsModule,
        SwalComponent,
    ]
})
export class GroupComponent implements AfterViewInit, OnInit {
    groupList: Group[] = [];
    filteredData: Group[] = [];

    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'id';
    currentSortDirection: 'asc' | 'desc' = 'asc';


    userDropdownList!: LookUp[];
    userSelectedItems!: LookUp[];

    claimDropdownList!: LookUp[];
    claimSelectedItems!: LookUp[];

    dropdownSettings!: IDropdownSettings;

    group: Group = new Group();

    groupAddForm!: FormGroup;

    isUserChange = false;
    isClaimChange = false;

    groupId!: number;

    constructor(private groupService: GroupService,
                private lookupService: LookUpService,
                private alertifyService: AlertifyService,
                private formBuilder: FormBuilder,
                private authService: AuthService,
                private titleService: TitleService) { }


    ngAfterViewInit(): void {
        this.getGroupList();
    }

    ngOnInit() {

        this.createGroupAddForm();
        this.titleService.setPageTitle('Groups');

        this.dropdownSettings = environment.getDropDownSetting;

        this.lookupService.getOperationClaimLookUp().subscribe((data: LookUp[]) => {
            this.claimDropdownList = data;
        });

        this.lookupService.getUserLookUp().subscribe((data: LookUp[]) => {
            this.userDropdownList = data;
        });
    }


    getGroupList() {
        this.groupService.getGroupList().subscribe(data => {
            this.groupList = data;
            this.filteredData = data; // Başlangıçta hepsi filtrelenmiş veri
            this.paginator.length = data.length; // Sayfalayıcı uzunluğunu ayarla
            this.sortData(this.currentSortColumn, true); // Varsayılan sıralamayı uygula
        });
    }

    save() {
        if (this.groupAddForm.valid) {
            this.group = Object.assign({}, this.groupAddForm.value)

            if (this.group.id === 0) {
                this.addGroup();
            } else {
                this.updateGroup();
            }

        }

    }

    addGroup() {
        this.groupService.addGroup(this.group).subscribe(data => {
            this.getGroupList(); // Listeyi yeniler
            this.group = new Group();
            // jQuery Modal Kapatma
            jQuery('#group').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.groupAddForm);
        })
    }

    updateGroup() {
        this.groupService.updateGroup(this.group).subscribe(data => {

            // MatTableDataSource yerine manuel array güncelleme
            const index = this.groupList.findIndex(x => x.id === this.group.id);
            if (index !== -1) {
                this.groupList[index] = this.group;
                this.filteredData = [...this.groupList]; // Güncel listeyi filtreli verilere kopyala
                this.sortData(this.currentSortColumn, true);
            }

            this.group = new Group();
            // jQuery Modal Kapatma
            jQuery('#group').modal('hide');
            this.alertifyService.success(data);
            this.clearFormGroup(this.groupAddForm);
        })
    }

    createGroupAddForm() {
        this.groupAddForm = this.formBuilder.group({
            id: [0],
            groupName: ['', Validators.required],
        })
    }

    deleteGroup(groupId: number) {
        this.groupService.deleteGroup(groupId).subscribe(data => {
            this.alertifyService.success(data.toString());
            // MatTableDataSource yerine manuel array güncelleme
            this.groupList = this.groupList.filter(x => x.id !== groupId);
            this.filteredData = [...this.groupList]; // Güncel listeyi filtreli verilere kopyala
            this.paginator.length = this.groupList.length;
            this.sortData(this.currentSortColumn, true);
        })
    }

    getGroupById(groupId: number) {
        this.clearFormGroup(this.groupAddForm);
        this.groupService.getGroupById(groupId).subscribe(data => {
            this.group = data;
            this.groupAddForm.patchValue(data);
        })
    }

    getGroupClaims(groupId: number) {
        this.groupId = groupId;
        this.groupService.getGroupClaims(groupId).subscribe(data => {
            this.claimSelectedItems = data;
        })
    }

    getGroupUsers(groupId: number) {
        this.groupId = groupId;
        this.groupService.getGroupUsers(groupId).subscribe(data => {
            this.userSelectedItems = data;
        })
    }

    saveGroupClaims() {
        if (this.isClaimChange) {
            const ids = this.claimSelectedItems.map(function(x) { return x.id as number});
            this.groupService.saveGroupClaims(this.groupId, ids).subscribe(x => {
                // jQuery Modal Kapatma
                jQuery('#groupClaims').modal('hide');
                this.isClaimChange = false;
                this.alertifyService.success(x);
            });
        }
    }

    saveGroupUsers() {
        if (this.isUserChange) {
            const ids = this.userSelectedItems.map(function(x) { return x.id as number});
            this.groupService.saveGroupUsers(this.groupId, ids).subscribe(x => {
                // jQuery Modal Kapatma
                jQuery('#groupUsers').modal('hide');
                this.isUserChange = false;
                this.alertifyService.success(x);
            });
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
        if (comboType === 'User') {
            this.isUserChange = true;
        } else if (comboType === 'Claim') {
            this.isClaimChange = true;
        }
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
        return this.authService.claimGuard(claim);
    }

// MatTableDataSource.paginator ve sort kaldırıldı, manuel filtreleme ve sıralama uygulandı.
// configDataTable() metodu artık gerekli değil.

// *** Yeni Manuel Filtreleme Metodu ***
    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

        // Tüm grup listesi üzerinde filtreleme yap
        this.filteredData = this.groupList.filter(group =>
            group.groupName.toLowerCase().includes(filterValue) ||
            group.id.toString().includes(filterValue)
        );

        // Filtreleme sonrası sayfalama ve sıralamayı güncelle
        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0; // İlk sayfaya dön
        this.sortData(this.currentSortColumn, true); // Filtrelenmiş veriye sıralamayı uygula
    }

// *** Yeni Manuel Sıralama Metodu ***
    sortData(sortColumn: string, isInitialSort = false): void {
        if (this.currentSortColumn === sortColumn && !isInitialSort) {
            // Aynı sütun tekrar tıklanırsa yönü değiştir
            this.currentSortDirection = this.currentSortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            // Yeni sütun tıklanırsa varsayılan yön 'asc'
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
}
