import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { Http } from '@angular/http';
import { LoaderService } from '../inner/loader.service';
import { User } from '../../model/entity/user.model';

@Injectable()
export class CustomerService extends BaseService {
    protected controllerUrl = this.apiUrl + 'customer';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    public get(): Promise<User[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get`;
        return this.request(this.http.get(url)).then((data) => {
            const users = [];
            for (const user of data.users) {
                users.push(this.castUser(user));
            }
            this.loaderService.stop();
            return users;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getById(id: number): Promise<User> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castUser(data.user);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public delete(id: number): Promise<string> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/delete/${id}`;
        return this.request(this.http.delete(url)).then((data) => {
            this.loaderService.stop();
            return data;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    private castUser(data: User): User {
        const user = new User();
        user.Email = data.Email;
        user.Id = data.Id;
        user.ListRoles = data.ListRoles;
        user.Name = data.Name;
        user.Status = data.Status;
        user.UserName = data.UserName;
         
        return user;
    }
}
