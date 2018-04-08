import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { BaseService } from '../base.service';
import { Dealer } from '../../model/entity/dealer.model';
import { LoaderService } from '../inner/loader.service';
import { SignUpDealer } from '../../model/data/sign-up-dealer.model';
import { DealerExists } from '../../model/data/dealer-exists.model';
import { AnswerExists } from '../../model/data/answer-exists.model';

@Injectable()
export class DealerService extends BaseService {
    protected controllerUrl = this.apiUrl + 'dealer';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    public get(): Promise<Dealer[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get`;
        return this.request(this.http.get(url)).then((data) => {
            const dealers = [];
            for (const dt of data.dealers) {
                dealers.push(this.castDealer(dt));
            }
            this.loaderService.stop();
            return dealers;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getById(id: number): Promise<Dealer> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castDealer(data.dealer);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public save(data: any): Promise<Dealer> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/post`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return this.castDealer(data.dealer);

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

    signUpDealer(signUp: SignUpDealer): Promise<boolean> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/signUpDealer`;
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

    existData(dealerExist: DealerExists): Promise<AnswerExists> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/existData`;
        return this.request(
            this.http.post(url, JSON.stringify(dealerExist), this.options)
        ).then((data) => {
            this.loaderService.stop();
            const answer = new AnswerExists(data.exist, data.message);
            return answer;

        }).catch((error) => {
            this.loaderService.stop();
            const answer = new AnswerExists(true, 'Error');
            return answer;
        });
    }

    getForCatalog(): Promise<Dealer[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getForCatalog`;
        return this.request(this.http.get(url)).then((data) => {
            const dealers = [];
            for (const dt of data.dealers) {
                dealers.push(this.castDealerForCatalog(dt));
            }
            this.loaderService.stop();
            return dealers;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    private castDealer(data: Dealer): Dealer {
        const dealer = new Dealer();
        dealer.Amount = data.Amount;
        dealer.Created = data.Created;
        dealer.Enable = data.Enable;
        dealer.EnableStr = data.Enable ? 'Yes' : 'No';
        dealer.Id = data.Id;
        dealer.Name = data.Name;
        dealer.Status = data.Status;
        dealer.Updated = data.Updated;
        dealer.UserId = data.UserId;

        return dealer;
    }

    private castDealerForCatalog(data: Dealer): Dealer {
        const dealer = new Dealer();
        dealer.Id = data.Id;
        dealer.Name = data.Name;

        return dealer;
    }
}
