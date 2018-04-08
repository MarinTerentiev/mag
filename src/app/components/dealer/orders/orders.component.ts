import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { OrderService } from '../../../shared/service/api/order.service';
import { Order } from '../../../shared/model/entity/order.model';
declare var window: any;

@Component({
    selector: 'app-dealer-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.less'],
    providers: [OrderService]
})
export class OrdersDealerComponent extends BaseComponent implements OnInit {
    public rows: Order[] = [];
    public selected: Order = new Order();

    constructor(
        private orderService: OrderService
    ){
        super();

        this.getData();
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getData(): void {
        this.orderService.getForDealer().then(data => {
            this.rows = data;
        });
    }

    edit(row: Order): void {
        this.orderService.getForDealerById(row.Id).then(data => {
            this.selected = data;
            this.openModal();
        });
    }

    private openModal(): void {
        window.showModal('#dealer-order');
    }
    private closeModal(): void {
        window.closeModal('#dealer-order');
    }
}
