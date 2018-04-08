import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import 'rxjs/add/operator/toPromise';
import { User } from '../../model/entity/user.model';
import { Http, Response } from '@angular/http';
import { Router } from '@angular/router';
import { Auth } from '../../model/data/auth.model';
import { Role } from '../../enum';
import { LoaderService } from '../inner/loader.service';


@Injectable()
export class AuthService extends BaseService {
    protected controllerUrl = this.apiUrl + 'Identity';
    private user: User;

    constructor(
        private http: Http,
        private router: Router,
        public loaderService: LoaderService 
    ) {
        super();
    }
    
    get isAuthenticated(): boolean {
        const res = (this.getToken() != null);
        const auth = (this.getAuthError() != null);
    
        if (res && auth) {
            this.removeAuthError();
            this.logout();
        }

        return res;
    }

    get isAdmin(): boolean {
        return this.isUserInRole(Role.Admin);
    }

    get isDealer(): boolean {
        return this.isUserInRole(Role.Dealer);
    }

    login(auth: Auth): Promise<boolean> {
        const url = `${this.controllerUrl}/login`;
        this.loaderService.start();
        return this.request(this.http.post(url, JSON.stringify(auth), this.options)).then((data) => {
            const token = data.token;

            this.loaderService.stop();
            if (token !== '' && typeof token !== 'undefined') {
                this.user = data.user;
                this.setToken(this.user, token);

                return true;
            }
            return false;
        }).catch((error) => {
            this.loaderService.stop();
            return false;
        });
    }
    logout(): void {
        const url = `${this.controllerUrl}/logout`;
        this.loaderService.start();
        this.request(this.http.post(url, {}, this.options)).then((data) => {
            this.removeToken();
            this.removeAuthError();
            this.user = null;
            this.loaderService.stop();
            this.router.navigate(['/home']);

        }).catch((error) => {
            this.loaderService.stop();
        });
    }

    getName(): string {
        if (!this.isAuthenticated) {
            return '';
        }

        if (this.user === null || typeof this.user === 'undefined') {
            this.setUser();
        } 

        return this.user.Name;
    }
    getId(): number {
        if (this.user === null || typeof this.user === 'undefined') {
            this.setUser();
        } 

        return this.user.Id;
    }
    isUserInRole(role: Role): boolean {
        if (this.user === null || typeof this.user === 'undefined') {
            this.setUser();
        } 

        if (typeof this.user.ListRoles === 'undefined') {
            return false;
        }
        const ret = this.user.ListRoles.includes(role);
        return ret;
    }


    private setUser(): void {
        this.user = new User();
        const json = this.getToken();
        if (json != null) {
            const obj = JSON.parse(json);

            if (typeof obj.roles !== 'undefined') {
                this.user.ListRoles = obj.roles;
            }

            this.user.Id = +obj.nr;
            this.user.Name = obj.name;
        }
    }

    public setToken(user: User, token: string): void {
        localStorage.setItem('currentUser', JSON.stringify({
            name: user.Name,
            token: token,
            roles: user.ListRoles,
            nr: user.Id
        }));
    }
    private getToken(): string {
        return localStorage.getItem('currentUser');
    }
    private removeToken(): void {
        localStorage.removeItem('currentUser');
    }

    private getAuthError(): string {
        // this error set in BaseService
        return localStorage.getItem('authError');
    }
    private removeAuthError(): void {
        localStorage.removeItem('authError');
    }
}