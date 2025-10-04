import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

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

    { path: '**', redirectTo: 'public/login' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes, {
      useHash: true
    })
  ],
  exports: [
    [RouterModule]
  ],
})
export class AppRoutingModule { }
