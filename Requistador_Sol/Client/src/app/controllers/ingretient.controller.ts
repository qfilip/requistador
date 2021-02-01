import { Injectable } from "@angular/core";

import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import * as urls from '../_generated/api.endpoints';
import { IIngredientDto } from "../_generated/interfaces";

@Injectable()
export class IngredientController {
    constructor(private http: HttpClient) {}

    getAll(): Observable<IIngredientDto[]> {
        const url = urls.IngredientController_GetAll;
        return this.http.get<IIngredientDto[]>(url);
    }

    create(dto: IIngredientDto): Observable<IIngredientDto> {
        const url = urls.IngredientController_Create;
        return this.http.post<IIngredientDto>(url, dto);
    }
}