import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { User } from '../../../shared/model/entity/user.model';
import { ModalConfirmService } from '../../../shared/service/inner/modal-confirm.service';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { ModalConfirmConfig } from '../../../shared/model/inner/modal-confirm-config.model';
import { CustomerService } from '../../../shared/service/api/customer.service';
declare var window: any;

@Component({
    selector: 'app-customer',
    templateUrl: './customers.component.html',
    styleUrls: ['./customers.component.less'],
    providers: [CustomerService]
})
export class CustomersComponent extends BaseComponent implements OnInit {
    public rows: User[] = [];
    public selected: User = new User();

    constructor(
        private customerService: CustomerService,
        public modalConfirmService: ModalConfirmService,
        private modalTempService: ModalTempService
    ){
        super();

        this.getData();

        const handler = {
            context: this,
            ok: this.delete
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this customer?', handler);
        this.modalConfirmService.init(modalConfirmConfig);
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getData(): void {
        this.customerService.get().then(data => {
            this.rows = data;
        });
    }

    edit(row: User): void {
        this.customerService.getById(row.Id).then(data => {
            this.selected = data;
            this.openModal();
        });
    }

    
    confirmDelete(row: User): void {
        this.modalConfirmService.open();
        this.selected = row;
    }
    delete(context: CustomersComponent, row: User): void {
        context.customerService.delete(context.selected.Id).then(data => {
            const deletedRow = context.rows.find(x => x.Id === context.selected.Id);
            const index = context.rows.indexOf(deletedRow, 0);
            if (index > -1) {
                context.rows.splice(index, 1);
            }
            context.modalConfirmService.close();
            context.modalTempService.showSuccess();
        }).catch(error => {
            context.modalTempService.showDanger();
        });
    }

    private openModal(): void {
        window.showModal('#admin-customer');
    }
    private closeModal(): void {
        window.closeModal('#admin-customer');
    }
}