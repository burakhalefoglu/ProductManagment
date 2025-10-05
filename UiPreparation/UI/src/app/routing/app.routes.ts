import {Routes} from '@angular/router';
import {ProductAddComponent} from '../components/app/product-add/product-add.component';
import {ProductListComponent} from '../components/app/product-list/product-list.component';
import {LoginGuard} from '../guards/login-guard';
import {ProductUpdateComponent} from '../components/app/product-update/product-update.component';
import {ProductDetailComponent} from '../components/app/product-detail/product-detail.component';
import {PrivateLayoutComponent} from '../components/private-layout.component';

export const routes: Routes = [
    {
        path: '',
        component: PrivateLayoutComponent,
        children: [
            { path: 'products', component: ProductListComponent,  canActivate: [LoginGuard] },
            { path: '', redirectTo: 'products', pathMatch: 'full' },

            { path: 'products/add', component: ProductAddComponent, canActivate: [LoginGuard] },
            { path: 'products/update/:id', component: ProductUpdateComponent, canActivate: [LoginGuard] },
            { path: 'products/detail/:id', component: ProductDetailComponent, canActivate: [LoginGuard] },
        ]
    }
];
