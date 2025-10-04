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
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { LoginGuard } from './core/guards/login-guard';
import { AuthInterceptorService } from './core/interceptors/auth-interceptor.service';
import { HttpEntityRepositoryService } from './core/services/http-entity-repository.service';
import { TranslationService } from './core/services/Translation.service';
import {SidebarComponent} from './core/components/base/sidebar/sidebar.component';
import {NavbarComponent} from './core/components/base/navbar/navbar.component';
import {FooterComponent} from './core/components/base/footer/footer.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({ declarations: [
        AppComponent,
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA], imports: [BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRoutingModule,
        NgMultiSelectDropDownModule.forRoot(),
        SweetAlert2Module.forRoot(),
        NgbModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        }), SidebarComponent, NavbarComponent, FooterComponent], providers: [
        LoginGuard,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptorService,
            multi: true,
        },
        HttpEntityRepositoryService,
        provideHttpClient(withInterceptorsFromDi()),
        ComponentsModule,

    ] })
export class AppModule { }
