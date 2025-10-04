import {ChangeDetectionStrategy, Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../base/navbar/navbar.component';
import { SidebarComponent } from '../base/sidebar/sidebar.component';
import { FooterComponent } from '../base/footer/footer.component';

@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-admin-layout',
    template: `
      <div class="sidebar" data-color="danger" data-background-color="white" data-image="./assets/img/sidebar-1.jpg">
        <app-sidebar></app-sidebar>
      </div>
      <app-navbar></app-navbar>
        <router-outlet></router-outlet>
      <app-footer></app-footer>
    `,
    imports: [RouterOutlet, NavbarComponent, SidebarComponent, FooterComponent]
})
export class AdminLayoutComponent {}
