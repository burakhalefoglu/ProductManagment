// src/app/public/layout/public-layout.component.ts

import {ChangeDetectionStrategy, Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-public-layout',
    template: `
        <router-outlet></router-outlet>
  `,
    imports: [RouterOutlet]
})
export class PublicLayoutComponent {}
