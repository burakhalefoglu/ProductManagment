import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { HealthcheckComponent } from './components/public/healthcheck/healthcheck.component';

const routes: Routes = [
    { path: '', redirectTo: 'public/login', pathMatch: 'full' },

    {
        path: 'public',
        loadChildren: () => import('./routing/public.routes').then(m => m.routes)
    },
    {
        path: 'admin',
        loadChildren: () => import('./routing/admin.routes').then(m => m.routes)
    },
    {
        path: 'app',
        loadChildren: () => import('./routing/app.routes').then(m => m.routes)
    },

    { path: 'healthcheck', component: HealthcheckComponent},

    { path: '**', redirectTo: 'public/login' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes, {
      useHash: false
    })
  ],
  exports: [
    [RouterModule]
  ],
})
export class AppRoutingModule { }
