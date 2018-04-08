import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { Http } from '@angular/http';
import { LoaderService } from '../inner/loader.service';
import { Order } from '../../model/entity/order.model';
import { ProductCard } from '../../model/entity/product-card.model';

@Injectable()
export class OrderService extends BaseService {
    protected controllerUrl = this.apiUrl + 'Order';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }
    
    public save(data: Order): Promise<string> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/post`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return data;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public get(): Promise<Order[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get`;
        return this.request(this.http.get(url)).then((data) => {
            const orders = [];
            for (const order of data.orders) {
                orders.push(this.castOrder(order));
            }
            this.loaderService.stop();
            return orders;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getById(id: number): Promise<Order> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castOrder(data.order);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getForDealer(): Promise<Order[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getForDealer`;
        return this.request(this.http.get(url)).then((data) => {
            const orders = [];
            for (const order of data.orders) {
                orders.push(this.castOrder(order));
            }
            this.loaderService.stop();
            return orders;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getForDealerById(id: number): Promise<Order> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getForDealer/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castOrder(data.order);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    private castOrder(data: Order): Order {
        const order = new Order();
        order.Address = data.Address;
        order.Amount = data.Amount;
        order.Details = data.Details;
        order.UserName = data.UserName;
        order.UserId = data.UserId;
        order.Id = data.Id;
        order.Created = data.Created;
        order.Status = data.Status;
        order.Updated = data.Updated;

        const cards = [];
        for (let card of data.ProductCards) {
            cards.push(this.castProductCard(card));
        }
        order.ProductCards = cards;

        return order;
    }

    private castProductCard(data: ProductCard): ProductCard {
        const card = new ProductCard();
        card.Count = data.Count;
        card.DealerId = data.DealerId;
        card.Name = data.Name;
        card.Price = data.Price;
        card.ProductId = data.ProductId;
        card.CompanyName = data.CompanyName;
        card.Id = data.Id;
        card.Created = data.Created;
        card.Status = data.Status;
        card.Updated = data.Updated;

        return card;
    }
}
