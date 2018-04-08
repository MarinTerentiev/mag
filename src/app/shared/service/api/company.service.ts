import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { BaseService } from '../base.service';
import { Company } from '../../model/entity/company.model';
import { LoaderService } from '../inner/loader.service';


@Injectable()
export class CompanyService extends BaseService {
    protected controllerUrl = this.apiUrl + 'company';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    public get(): Promise<Company[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get`;
        return this.request(this.http.get(url)).then((data) => {
            const companies = [];
            for(const company of data.companies) {
                companies.push(this.castCompany(company));
            }
            this.loaderService.stop();
            return companies;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getById(id: number): Promise<Company> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castCompany(data.company);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public save(data: any): Promise<Company> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/post`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return this.castCompany(data.company);

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

    getForCatalog(): Promise<Company[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getForCatalog`;
        return this.request(this.http.get(url)).then((data) => {
            const companies = [];
            for (const dt of data.companies) {
                companies.push(this.castCompanyForCatalog(dt));
            }
            this.loaderService.stop();
            return companies;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    private castCompany(data: Company): Company {
        const company = new Company();
        company.Created = data.Created;
        company.DealerId = data.DealerId;
        company.Enable = data.Enable;
        company.EnableStr = data.Enable ? 'Yes' : 'No';
        company.Id = data.Id;
        company.Name = data.Name;
        company.Status = data.Status;
        company.Updated = data.Updated;

        return company;
    }

    private castCompanyForCatalog(data: Company): Company {
        const company = new Company();
        company.DealerId = data.DealerId;
        company.Id = data.Id;
        company.Name = data.Name;

        return company;
    }
}
