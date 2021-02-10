import { NgModule } from '@angular/core';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { StorageService } from 'src/app/services/storage.service';
import { AuthGuard } from './guards';

import * as urls from '../../_generated/api.endpoints';
import * as constants from '../../_generated/constants';

export function tokenGetter() {
    return localStorage.getItem(constants.Ls_Jwt_Key);
}

@NgModule({
    declarations: [],
    imports: [
        JwtModule.forRoot({
            config: {
                tokenGetter: tokenGetter,
                allowedDomains: [urls.Api_Root],
                disallowedRoutes: []
            }
        })
    ],
    providers: [
        JwtHelperService,
        StorageService,
        AuthGuard
    ]
})
export class IdentityModule { }
