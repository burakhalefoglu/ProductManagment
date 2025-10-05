import { HttpClient } from '@angular/common/http';
import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {TranslateModule, TranslateService} from '@ngx-translate/core';
import { LoginUser } from './model/login-user';
import { AuthService } from './Services/Auth.service';
import {CommonModule} from '@angular/common';
import {RouterLink} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {environment} from '../../../../environments/environment';
import {LookUpService} from '../../../core/services/LookUp.service';
import {LookUp} from '../../../core/models/LookUp';
import {LocalStorageService} from '../../../core/services/local-storage.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        TranslateModule,
        RouterLink,
        FormsModule,
    ]
})
export class LoginComponent implements OnInit {

  username = '';
  loginUser: LoginUser = new LoginUser();
  langugelookUp!: LookUp[];


  constructor(private auth: AuthService,
    private storageService: LocalStorageService,
    private lookupService: LookUpService,
    public translateService: TranslateService,
    private httpClient: HttpClient) { }

  ngOnInit() {

    this.username = this.auth.userName ?? '';
    this.httpClient.get<LookUp[]>(environment.getApiUrl + '/languages/codes').subscribe(data => {
      this.langugelookUp = data;
    })

  }

  getUserName() {
    return this.username;
  }

  login() {
    this.auth.login(this.loginUser);
  }

  logOut() {
      this.storageService.removeToken();
      this.storageService.removeItem('lang');
  }

  changeLang(lang: string | undefined) {
    localStorage.setItem('lang', lang ?? 'tr-TR');
    this.translateService.use(lang ?? 'tr-TR');
  }

}
