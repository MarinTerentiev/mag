import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { OrderService } from '../../../shared/service/api/order.service';
import { Order } from '../../../shared/model/entity/order.model';
declare var window: any;

@Component({
    selector: 'app-admin-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.less'],
    providers: [OrderService]
})
export class OrdersComponent extends BaseComponent implements OnInit {
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
        this.orderService.get().then(data => {
            this.rows = data;
        });
    }

    edit(row: Order): void {
        this.orderService.getById(row.Id).then(data => {
            this.selected = data;
            this.openModal();
        });
    }

    private openModal(): void {
        window.showModal('#admin-order');
    }
    private closeModal(): void {
        window.closeModal('#admin-order');
    }
}
