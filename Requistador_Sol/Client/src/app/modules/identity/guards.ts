import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from "rxjs";
import * as Constants from '../../_generated/constants';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private jwtHelper: JwtHelperService
    ) {}
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const token = localStorage.getItem(Constants.Ls_Jwt_Key);
        console.log('yay');
        if(token && !this.jwtHelper.isTokenExpired(token)) {
            this.router.navigate(['home']);
            return true;
        }
        
        this.router.navigate(['login']);
        return false;
    }

}