import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountController } from 'src/app/controllers/account.controller';
import { StorageService } from 'src/app/services/storage.service';
import { IAppUserDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    constructor(
        private router: Router,
        private controller: AccountController,
        private storage: StorageService
    ) { }

    ngOnInit(): void {
    }

    login() {
        this.controller.login({ username: 'bob', password: 'bobby' } as IAppUserDto)
        .subscribe(
            result => {
                console.log(result.jwt);
                this.storage.storeJwt(result.jwt);
                this.router.navigate(['home']);
            },
            error => console.group(error)
        )
    }

}
