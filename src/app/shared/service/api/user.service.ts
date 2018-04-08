import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { Http } from '@angular/http';
import { LoaderService } from '../inner/loader.service';
import { SignUp } from '../../model/data/sign-up.model';
import { AnswerExists } from '../../model/data/answer-exists.model';
import { UserExists } from '../../model/data/user-exists.model';
import { ResetPassword } from '../../model/data/reset-password.model';

@Injectable()
export class UserService extends BaseService {
    protected controllerUrl = this.apiUrl + 'user';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    signUp(signUp: SignUp): Promise<boolean> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/signUp`;
        return this.request(
            this.http.post(url, JSON.stringify(signUp), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return true;

        }).catch((error) => {
            this.loaderService.stop();
            return false;
        });
    }

    existData(userExist: UserExists): Promise<AnswerExists> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/existData`;
        return this.request(
            this.http.post(url, JSON.stringify(userExist), this.options)
        ).then((data) => {
            this.loaderService.stop();
            const answer = new AnswerExists(data.exist, data.message);
            return  answer;

        }).catch((error) => {
            this.loaderService.stop();
            const answer = new AnswerExists(true, 'Error');
            return  answer;
        });
    }

    resetPassword(resetPassword: ResetPassword): Promise<string> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/resetPassword`;
        return this.request(
            this.http.post(url, JSON.stringify(resetPassword), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return data;
            
        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }
}
