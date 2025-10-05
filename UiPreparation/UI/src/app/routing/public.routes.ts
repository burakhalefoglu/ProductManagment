import { Routes } from '@angular/router';
import {LoginComponent} from '../components/public/login/login.component';
import {PublicLayoutComponent} from '../components/public/public-layout.component';
import {RegisterComponent} from '../components/public/register/register.component';

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
