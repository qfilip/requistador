import { Injectable } from "@angular/core";
import * as constants from '../_generated/constants';

@Injectable()
export class StorageService {

    storeJwt(token: string) {
        localStorage.setItem(constants.Ls_Jwt_Key, token);
    }

    getJwt() {
        return localStorage.getItem(constants.Ls_Jwt_Key);
    }
}