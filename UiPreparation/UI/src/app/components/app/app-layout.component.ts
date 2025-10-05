import {ChangeDetectionStrategy, Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavbarComponent} from '../base/navbar/navbar.component';
import {SidebarComponent} from '../base/sidebar/sidebar.component';
import {FooterComponent} from '../base/footer/footer.component';

@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-layout',
    template: `
        <app-navbar></app-navbar>
        <app-sidebar></app-sidebar>
        <div class="app-wrapper">
            <router-outlet></router-outlet>
            <app-footer></app-footer>
        </div>
    `,
    imports: [RouterOutlet, NavbarComponent, SidebarComponent, FooterComponent]
})
export class AppLayoutComponent {}
