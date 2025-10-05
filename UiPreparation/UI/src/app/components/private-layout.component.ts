import {ChangeDetectionStrategy, Component, Signal} from '@angular/core';
import {Router, RouterLink, RouterOutlet} from '@angular/router';
import {TranslatePipe, TranslateService} from '@ngx-translate/core';
import {CommonModule, NgClass} from '@angular/common';
import {AuthService} from './public/login/Services/Auth.service';
import {SharedService} from '../core/services/shared.service';
import {TitleService} from '../core/services/title.service';
import {Subscription} from 'rxjs';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    claim: string;
}
export const ADMINROUTES: RouteInfo[] = [
    { path: '/admin/user', title: 'Users', icon: 'how_to_reg', class: '', claim: 'GetUsersQuery' },
    { path: '/admin/group', title: 'Groups', icon: 'groups', class: '', claim: 'GetGroupsQuery' },
    { path: '/admin/operationclaim', title: 'OperationClaim', icon: 'local_police', class: '', claim: 'GetOperationClaimsQuery'},
    { path: '/admin/language', title: 'Languages', icon: 'language', class: '', claim: 'GetLanguagesQuery' },
    { path: '/admin/translate', title: 'TranslateWords', icon: 'translate', class: '', claim: 'GetTranslatesQuery' },
    { path: '/admin/log', title: 'Logs', icon: 'update', class: '', claim: 'GetLogDtoQuery' }
];

export const USERROUTES: RouteInfo[] = [
    { path: '/app/products', title: 'Product List', icon: 'list', class: '', claim: 'GetProductsQuery' }
];

@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-admin-layout',
    templateUrl: './private-layout.component.html',
    styleUrls: ['./private-layout.component.css'],
    imports: [
        CommonModule,
        NgClass,
        TranslatePipe,
        RouterLink,
        RouterOutlet
    ]
})
export class PrivateLayoutComponent {
    public readonly userNameSignal: Signal<string> = this.authService.userNameSignal;
    clickEventSubscription: Subscription;
    public pageTitle = this.titleService.pageTitle;

    adminMenuItems!: any[];
    userMenuItems!: any[];

    constructor(private router: Router,
                private authService: AuthService,
                public translateService: TranslateService,
                private sharedService: SharedService,
                private titleService: TitleService) {
        this.clickEventSubscription = this.sharedService.getChangeUserNameClickEvent().subscribe(() => {
        });
    }

    ngOnInit() {

        this.adminMenuItems = ADMINROUTES.filter(menuItem => menuItem);
        this.userMenuItems = USERROUTES.filter(menuItem => menuItem);
        const lang = localStorage.getItem('lang') || 'tr-TR'
        this.translateService.use(lang);
    }

    checkClaim(claim: string): boolean {
        return this.authService.claimGuard(claim)
    }
    ngOnDestroy() {
        if (!this.authService.loggedIn()) {
            this.authService.logOut();
            this.router.navigateByUrl('/public/login');
            this.clickEventSubscription.unsubscribe();
        }
    }

    toggleSidebar() {
        document.querySelector('.app-sidebar')?.classList.toggle('show');
    }

    logOut() {
        this.authService.logOut();
        this.router.navigateByUrl('/public/login');

    }
}
