import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {AuthService} from '../components/public/login/Services/Auth.service';


@Injectable()
export class LoginGuard implements CanActivate {

    constructor(private router: Router, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot,
                state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        console.log('this.authService.loggedIn(): ' + this.authService.loggedIn());
        if (this.authService.loggedIn()) {
            return true;
        }
        this.router.navigate(['/public/login']);
        return false;

    }


}
