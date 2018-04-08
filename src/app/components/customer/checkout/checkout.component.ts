import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Order } from '../../../shared/model/entity/order.model';
import { CardService } from '../../../shared/service/inner/card.service';
import { OrderService } from '../../../shared/service/api/order.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-checkout',
    templateUrl: './checkout.component.html',
    styleUrls: ['./checkout.component.less'],
    providers: [OrderService]
})
export class CheckoutComponent extends BaseComponent {
    order: Order = new Order();
    hasOrder = false;

    constructor(
        private orderService: OrderService,
        private cardService: CardService,
        private router: Router
    ){
        super();
    }

    save(): void {
        const that = this;
        this.order.ProductCards = this.cardService.getProducts();
        this.orderService.save(this.order).then(data => {
            this.hasOrder = true;
            this.cardService.cleanProducts();
        });
    }
}
