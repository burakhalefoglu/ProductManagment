import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

import { LogDto } from './models/logdto';
import { LogDtoService } from './services/logdto.service';
import { AuthService } from '../../public/login/Services/Auth.service';
import { TitleService } from '../../../core/services/title.service';

// DevExtreme
import {
    DxDataGridModule,
    DxLoadIndicatorModule,
} from 'devextreme-angular';

@Component({
    selector: 'app-logDto',
    templateUrl: './logDto.component.html',
    styleUrls: ['./logDto.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        DxDataGridModule,
        DxLoadIndicatorModule,
    ],
})
export class LogDtoComponent implements AfterViewInit, OnInit {
    logDtoList: LogDto[] = [];
    isLoading = false;

    constructor(
        private logDtoService: LogDtoService,
        private titleService: TitleService,
        private authService: AuthService,
        private cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.titleService.setPageTitle('Logs');
    }

    ngAfterViewInit(): void {
        this.getLogDtoList();
    }

    getLogDtoList(): void {
        this.isLoading = true;
        this.logDtoService.getLogDtoList().subscribe({
            next: (data) => {
                // Yeni referans ver (OnPush)
                this.logDtoList = Array.isArray(data) ? [...data] : [];
                this.isLoading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.isLoading = false;
                this.cdr.markForCheck();
            },
        });
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim);
    }
}
