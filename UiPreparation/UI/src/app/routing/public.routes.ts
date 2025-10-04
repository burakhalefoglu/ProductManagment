import { Routes } from '@angular/router';
import {LoginComponent} from '../core/components/public/login/login.component';
import {RegisterComponent} from '../core/components/public/register/register.component';
import {PublicLayoutComponent} from '../core/components/public/public-layout.component';

export const routes: Routes = [
    {
        path: '',
        component: PublicLayoutComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: '', redirectTo: 'login', pathMatch: 'full' }
        ]
    }
];
