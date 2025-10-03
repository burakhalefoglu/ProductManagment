import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LoginUser } from './model/login-user';
import { AuthService } from './Services/Auth.service';
import { LocalStorageService } from '../../../services/local-storage.service';
import { LookUpService } from '../../../services/LookUp.service';
import { LookUp } from '../../../models/LookUp';
import { environment } from '../../../../../environments/environment';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    standalone: false
})
export class LoginComponent implements OnInit {

  username:string="";
  loginUser:LoginUser=new LoginUser();
  langugelookUp!:LookUp[];


  constructor(private auth:AuthService,
    private storageService:LocalStorageService,
    private lookupService:LookUpService,
    public translateService:TranslateService,
    private httpClient:HttpClient) { }

  ngOnInit() {

    this.username = this.auth.userName ?? "";
    this.httpClient.get<LookUp[]>(environment.getApiUrl +"/languages/codes").subscribe(data=>{
      this.langugelookUp=data;
    })
    
  }

  getUserName(){
    return this.username;
  }

  login(){
    this.auth.login(this.loginUser);
  }

  logOut(){
      this.storageService.removeToken();
      this.storageService.removeItem("lang");
  }

  changeLang(lang: string | undefined){
    localStorage.setItem("lang",lang ?? "tr-TR");
    this.translateService.use(lang ?? "tr-TR");
  }

}
