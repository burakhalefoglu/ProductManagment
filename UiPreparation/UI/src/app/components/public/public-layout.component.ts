// src/app/public/layout/public-layout.component.ts

import {ChangeDetectionStrategy, Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-public-layout',
    template: `
    <div class="wrapper">
      <div class="main-panel">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
    imports: [RouterOutlet]
})
export class PublicLayoutComponent {}
