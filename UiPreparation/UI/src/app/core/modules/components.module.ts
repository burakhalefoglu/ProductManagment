import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FooterComponent } from '../components/base/footer/footer.component';
import { NavbarComponent } from '../components/base/navbar/navbar.component';
import { SidebarComponent } from '../components/base/sidebar/sidebar.component';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule,

      FooterComponent,
      NavbarComponent,
      SidebarComponent,
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent
  ]
})
export class ComponentsModule { }
