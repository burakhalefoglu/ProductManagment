import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { ComponentsModule } from './core/modules/components.module';
import { AdminLayoutComponent } from './core/components/app/layouts/admin-layout/admin-layout.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { LoginGuard } from './core/guards/login-guard';
import { AuthInterceptorService } from './core/interceptors/auth-interceptor.service';
import { HttpEntityRepositoryService } from './core/services/http-entity-repository.service';
import { TranslationService } from './core/services/Translation.service';


// i18 kullanıclak ise aşağıdaki metod aktif edilecek

//  export function HttpLoaderFactory(http: HttpClient) {
//    
//    var asd=new TranslateHttpLoader(http, '../../../../assets/i18n/', '.json'); 
//    return asd;
//  }


export function tokenGetter() {
  return localStorage.getItem("token");
}


@NgModule({ declarations: [
        AppComponent,
        AdminLayoutComponent
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA], imports: [BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        ComponentsModule,
        RouterModule,
        AppRoutingModule,
        NgMultiSelectDropDownModule.forRoot(),
        SweetAlert2Module.forRoot(),
        NgbModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                //useFactory:HttpLoaderFactory, //i18 kullanılacak ise useClass kapatılıp yukarıda bulunan HttpLoaderFactory ve bu satır aktif edilecek
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })], providers: [
        LoginGuard,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptorService,
            multi: true,
        },
        HttpEntityRepositoryService,
        provideHttpClient(withInterceptorsFromDi()),
    ] })
export class AppModule { }
