import { Routes } from '@angular/router';
import {DashboardComponent} from '../components/admin/dashboard/dashboard.component';
import {AdminLayoutComponent} from '../components/admin/admin-layout.component';
import {LoginGuard} from '../guards/login-guard';
import {GroupComponent} from '../components/admin/group/group.component';
import LanguageComponent from '../components/admin/language/language.component';
import {TranslateComponent} from '../components/admin/translate/translate.component';
import {OperationClaimComponent} from '../components/admin/operationclaim/operationClaim.component';
import {LogDtoComponent} from '../components/admin/log/logDto.component';
import {UserComponent} from '../components/admin/user/user.component';


export const routes: Routes = [
    {
        path: '',
        component: AdminLayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent, canActivate: [LoginGuard] },
            { path: 'group', component: GroupComponent, canActivate: [LoginGuard] },
            { path: 'language', component: LanguageComponent, canActivate: [LoginGuard] },
            { path: 'translate', component: TranslateComponent, canActivate: [LoginGuard] },
            { path: 'operationclaim', component: OperationClaimComponent, canActivate: [LoginGuard] },
            { path: 'log', component: LogDtoComponent, canActivate: [LoginGuard] },
            { path: 'user', component: UserComponent, canActivate: [LoginGuard] },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        ]
    }
];
