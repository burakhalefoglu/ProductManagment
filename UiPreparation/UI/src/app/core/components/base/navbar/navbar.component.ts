import {Component, OnInit, ElementRef, inject, Signal} from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {AuthService} from '../../public/login/Services/Auth.service';
import { SharedService } from '../../../services/shared.service';
import {TitleService} from '../../../services/title.service';
import {TranslatePipe} from '@ngx-translate/core';


@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css'],

    imports: [
        TranslatePipe
    ]
})
export class NavbarComponent implements OnInit {
    private titleService = inject(TitleService);
    public pageTitle: Signal<string> = this.titleService.pageTitle;

userName!: string;
clickEventSubscription: Subscription;
constructor(private authService: AuthService,
    private router: Router,
    private sharedService: SharedService) {
    this.clickEventSubscription = this.sharedService.getChangeUserNameClickEvent().subscribe(() => {
        this.setUserName();
    })
}

isLoggedIn(): boolean {

    return this.authService.loggedIn();
}

logOut() {
    this.authService.logOut();
    this.router.navigateByUrl('/public/login');

}


ngOnInit() {
    console.log(this.userName);
    this.userName = this.authService.getUserName();
}

setUserName() {

    this.userName = this.authService.getUserName();
}
}
