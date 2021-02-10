import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';
import { IAppUserDto, ICocktailDto } from "../_generated/interfaces";

@Injectable()
export class AccountController {
    constructor(private http: HttpClient) {}

    login(user: IAppUserDto): Observable<IAppUserDto> {
        const url = urls.Accounts_Login;
        return this.http.post<IAppUserDto>(url, user);
    }
}