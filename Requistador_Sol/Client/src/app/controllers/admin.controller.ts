import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';

@Injectable()
export class AdminController {
    constructor(private http: HttpClient) {}

    // login(user: IAppUserDto): Observable<IAppUserDto> {
    //     const url = urls.Accounts_Login;
    //     return this.http.post<IAppUserDto>(url, user);
    // }
}