import {Component, OnInit, Signal} from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {AuthService} from '../../public/login/Services/Auth.service';
import {TranslatePipe} from '@ngx-translate/core';
import {SharedService} from '../../../core/services/shared.service';
import {TitleService} from '../../../core/services/title.service';


@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css'],

    imports: [
        TranslatePipe
    ]
})
export class NavbarComponent implements OnInit {
    userName!: string;
    clickEventSubscription: Subscription;
    public pageTitle!: Signal<string>;

    constructor(private authService: AuthService,
    private router: Router,
    private sharedService: SharedService,
    private titleService: TitleService) {
    this.clickEventSubscription = this.sharedService.getChangeUserNameClickEvent().subscribe(() => {
        this.setUserName();
        this.pageTitle = this.titleService.pageTitle;
    })
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
