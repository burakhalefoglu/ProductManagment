import {Component, OnInit, AfterViewInit, ChangeDetectionStrategy} from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LogDto } from './models/logdto';
import { LogDtoService } from './services/logdto.service';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {LookUpService} from '../../../core/services/LookUp.service';
import {AlertifyService} from '../../../core/services/Alertify.service';
import {TitleService} from '../../../core/services/title.service';

@Component({
    selector: 'app-logDto',
    templateUrl: './logDto.component.html',
    styleUrls: ['./logDto.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
    ]
})
export class LogDtoComponent implements AfterViewInit, OnInit {

    logDtoList: LogDto[] = [];
    filteredData: LogDto[] = [];
    paginator = {
        pageIndex: 0,
        pageSize: 10,
        pageSizeOptions: [10, 25, 50, 100],
        length: 0,
    };
    currentSortColumn = 'id';
    currentSortDirection: 'asc' | 'desc' = 'asc';

    displayedColumns: string[] = ['id', 'level', 'exceptionMessage', 'timeStamp', 'user', 'value', 'type'];
    constructor(private logDtoService: LogDtoService,
                private titleService: TitleService,
                private authService: AuthService) { }

    ngOnInit() {
        this.getLogDtoList();
        this.titleService.setPageTitle('Logs');
    }

    getLogDtoList() {
        this.logDtoService.getLogDtoList().subscribe(data => {
            this.logDtoList = data;
            this.filteredData = data;
            this.paginator.length = data.length;
            this.sortData(this.currentSortColumn, true);
        });
    }
    ngAfterViewInit(): void {
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

        this.filteredData = this.logDtoList.filter(log => {
            // Tüm string alanları kontrol et
            const levelMatch = log.level ? log.level.toLowerCase().includes(filterValue) : false;
            const exceptionMatch = log.exceptionMessage ? log.exceptionMessage.toLowerCase().includes(filterValue) : false;
            const userMatch = log.user ? log.user.toLowerCase().includes(filterValue) : false;
            const valueMatch = log.value ? log.value.toLowerCase().includes(filterValue) : false;
            const typeMatch = log.type ? log.type.toLowerCase().includes(filterValue) : false;

            // ID ve TimeStamp (number/date) string'e çevrilerek kontrol edilir.
            const idMatch = log.id !== undefined && log.id !== null
                ? log.id.toString().includes(filterValue)
                : false;

            const timeStampMatch = log.timeStamp !== undefined && log.timeStamp !== null
                ? log.timeStamp.toString().toLowerCase().includes(filterValue)
                : false;

            return levelMatch || exceptionMatch || userMatch || valueMatch || typeMatch || idMatch || timeStampMatch;
        });

        this.paginator.length = this.filteredData.length;
        this.paginator.pageIndex = 0; // İlk sayfaya dön
        this.sortData(this.currentSortColumn, true); // Filtrelenmiş veriye sıralamayı uygula
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
