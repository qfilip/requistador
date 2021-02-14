import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';
import { IApiAdminRequestDto, IAppStateDto } from "../_generated/interfaces";

@Injectable()
export class AdminController {
    constructor(private http: HttpClient) {}

    getAppConfiguration(): Observable<IAppStateDto> {
        const url = urls.Admins_GetAppConfiguration;
        return this.http.get<IAppStateDto>(url);
    }

    handleAdminRequest(dto: IAppStateDto): Observable<IApiAdminRequestDto> {
        const url = urls.Admins_HandleAdminRequest;
        return this.http.post<IApiAdminRequestDto>(url, dto);
    }

    getLogFile(filename: string): Observable<IAppStateDto> {
        let params = new HttpParams().set('filename', filename);
        const url = urls.Admins_GetLogFile;
        return this.http.get<IAppStateDto>(url, { params: params });
    }
}