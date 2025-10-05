import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import * as Chartist from 'chartist';
import {CommonModule} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';
import {RouterLink} from '@angular/router';
import {TitleService} from '../../../core/services/title.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        RouterLink,
    ]})
export class DashboardComponent implements OnInit {

    constructor(private titleService: TitleService) { }
    ngOnInit(): void {
        this.titleService.setPageTitle('Dashboard');
    }

}
