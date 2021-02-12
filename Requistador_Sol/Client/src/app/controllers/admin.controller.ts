import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';
import { IAppStateDto } from "../_generated/interfaces";

@Injectable()
export class AdminController {
    constructor(private http: HttpClient) {}

    getAppConfiguration(): Observable<IAppStateDto> {
        const url = urls.Admins_GetAppConfiguration;
        return this.http.get<IAppStateDto>(url);
    }

    setProcessingInterval(dto: IAppStateDto): Observable<IAppStateDto> {
        const url = urls.Admins_SetRequestProcessingInterval;
        return this.http.post<IAppStateDto>(url, dto);
    }
}