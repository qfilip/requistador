import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';
import { ICocktailDto } from "../_generated/interfaces";

@Injectable()
export class CocktailController {
    constructor(private http: HttpClient) {}

    getAll(): Observable<ICocktailDto[]> {
        const url = urls.CocktailController_GetAll;
        return this.http.get<ICocktailDto[]>(url);
    }
}