import { Routes } from '@angular/router';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { LoginGuard } from '../../../../guards/login-guard';
import { UserComponent } from '../../../admin/user/user.component';
import { GroupComponent } from '../../../admin/group/group.component';
import { LoginComponent } from '../../../admin/login/login.component';
import { LanguageComponent } from '../../../admin/language/language.component';
import { TranslateComponent } from '../../../admin/translate/translate.component';
import { OperationClaimComponent } from '../../../admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../../../admin/log/logDto.component';


export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]}
    
];
