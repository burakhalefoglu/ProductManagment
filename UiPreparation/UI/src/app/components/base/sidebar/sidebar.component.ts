import { Component, OnInit } from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {TranslatePipe, TranslateService} from '@ngx-translate/core';
import {AuthService} from '../../public/login/Services/Auth.service';
import {CommonModule, NgClass} from '@angular/common';


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
  { path: '/', title: 'Home', icon: 'home', class: '', claim: 'GetLogDtoQuery' }
];

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],

    imports: [
        CommonModule,
        NgClass,
        TranslatePipe,
        RouterLink
    ]
})
export class SidebarComponent implements OnInit {
  adminMenuItems!: any[];
  userMenuItems!: any[];

  constructor(private router: Router,
    private authService: AuthService,
    public translateService: TranslateService) {

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
    }
  }
 }

