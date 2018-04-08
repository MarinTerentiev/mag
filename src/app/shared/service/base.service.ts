import { Injectable } from '@angular/core';
import { Headers, Http, Response, RequestOptions, RequestMethod } from '@angular/http';
import { Router } from '@angular/router';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../../environments/environment.prod';

@Injectable()
export class BaseService {
    protected apiUrl =  environment.site + '/api/'; // todo for developer environment
    protected headers = new Headers({ 'Content-Type': 'application/json' });
    protected options = new RequestOptions({ method: RequestMethod.Post, headers: this.headers });

    constructor(){}

    protected extractData(res: Response): any {
        const body = res.json();
        return body.data || {};
    }

    protected handleError(error: any): Promise<any> {
        if (error.status === 401 || error.status === 403) {
            localStorage.setItem('authError', 'authError');
            return Promise.reject('Authentication error.');
        }

        console.log(error);
        return Promise.reject(error.json().Message || error.json().message || error);
    }

    protected request(data: any): Promise<any> {
        return data
            .toPromise()
            .then(this.extractData)
            .catch(this.handleError);
    }
}
