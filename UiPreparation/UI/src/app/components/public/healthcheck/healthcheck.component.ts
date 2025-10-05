import {ChangeDetectionStrategy, Component} from '@angular/core';
@Component({
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-healthcheck',
    template: `
    <p>Sağlık Kontrolü Başarılı!</p>
`,
    imports: []
})
export class HealthcheckComponent {}
