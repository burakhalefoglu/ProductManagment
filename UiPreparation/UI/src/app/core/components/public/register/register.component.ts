import { HttpClient } from '@angular/common/http';
import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {TranslateModule, TranslateService} from '@ngx-translate/core';
import { LookUp } from '../../../models/LookUp';
import { LookUpService } from '../../../services/LookUp.service';
import {RegisterUser} from '../login/model/register-user';
import {environment} from '../../../../../environments/environment';
import {AuthService} from '../login/Services/Auth.service';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ['./register.component.scss'],
    imports: [
        CommonModule,
        TranslateModule,
        FormsModule,
        RouterLink,
    ]
})
export class RegisterComponent implements OnInit {

    registerUser: RegisterUser = new RegisterUser();
    langugelookUp!: LookUp[];

    constructor(
        private lookupService: LookUpService,
        public translateService: TranslateService,
        private httpClient: HttpClient,
        private auth: AuthService,
    ) { }

    ngOnInit() {
        this.httpClient.get<LookUp[]>(environment.getApiUrl + '/languages/codes').subscribe(data => {
            this.langugelookUp = data;
        });
        this.registerUser.lang = localStorage.getItem('lang') || 'tr-TR';
        this.translateService.use(this.registerUser.lang);
    }
    register() {
        if (this.registerUser.password !== this.registerUser.confirmPassword) {
            console.error('Şifreler eşleşmiyor!');
            return;
        }
        this.auth.register(this.registerUser);
    }
    changeLang(lang: string | undefined) {
        localStorage.setItem('lang', lang ?? 'tr-TR');
        this.translateService.use(lang ?? 'tr-TR');
    }

}
