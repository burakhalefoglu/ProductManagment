import { Routes } from '@angular/router';
import {GroupComponent} from '../core/components/admin/group/group.component';
import {DashboardComponent} from '../core/components/admin/dashboard/dashboard.component';
import {LoginGuard} from '../core/guards/login-guard';
import {LanguageComponent} from '../core/components/admin/language/language.component';
import {TranslateComponent} from '../core/components/admin/translate/translate.component';
import {OperationClaimComponent} from '../core/components/admin/operationclaim/operationClaim.component';
import {LogDtoComponent} from '../core/components/admin/log/logDto.component';
import {UserComponent} from '../core/components/admin/user/user.component';
import {AdminLayoutComponent} from '../core/components/admin/admin-layout.component';
import {ProductListComponent} from '../core/utils/example';
import {ProductAddComponent} from '../core/components/admin/product/product-add/product-add.component';
import {ProductUpdateComponent} from '../core/components/admin/product/product-update/product-update.component';
import {ProductDetailComponent} from '../core/components/admin/product/product-detail/product-detail.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminLayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent, canActivate: [LoginGuard] },
            { path: 'products', component: ProductListComponent },
            { path: 'products/add', component: ProductAddComponent },
            { path: 'products/update/:id', component: ProductUpdateComponent },
            { path: 'products/detail/:id', component: ProductDetailComponent },
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
