import { Component, OnInit } from '@angular/core';
import { Dealer } from '../../../shared/model/entity/dealer.model';
import { DealerService } from '../../../shared/service/api/dealer.service';
import { BaseComponent } from '../../../shared/base.component';
import { ModalConfirmService } from '../../../shared/service/inner/modal-confirm.service';
import { ModalConfirmConfig } from '../../../shared/model/inner/modal-confirm-config.model';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
declare var window: any;

@Component({
    selector: 'app-dealers',
    templateUrl: './dealers.component.html',
    styleUrls: ['./dealers.component.less'],
    providers: [DealerService]
})
export class DealersComponent extends BaseComponent implements OnInit {
    public rows: Dealer[] = [];
    public selected: Dealer = new Dealer();

    constructor(
        private dealerService: DealerService,
        public modalConfirmService: ModalConfirmService,
        private modalTempService: ModalTempService
    ){
        super();

        this.getData();

        const handler = {
            context: this,
            ok: this.delete
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this dealer?', handler);
        this.modalConfirmService.init(modalConfirmConfig);
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getData(): void {
        this.dealerService.get().then(data => {
            this.rows = data;
        });
    }

    // create(): void {
    //     this.selected = new Dealer();
    //     this.openModal();
    // }

    edit(row: Dealer): void {
        this.dealerService.getById(row.Id).then(data => {
            this.selected = data;
            this.openModal();
        });
    }

    save(): void {
        const isNew = this.selected.Id === -1;
        this.dealerService.save(this.selected).then(data => {
            if (isNew) {
                this.rows.push(data);
            } else {
                const editedRow = this.rows.find(x => x.Id === data.Id);
                const index = this.rows.indexOf(editedRow, 0);
                this.rows[index] = data;
            }

            this.closeModal();
            this.modalTempService.showSuccess();
        }).catch(error => {
            this.modalTempService.showDanger();
        });
    }

    confirmDelete(row: Dealer): void {
        this.modalConfirmService.open();
        this.selected = row;
    }
    delete(context: DealersComponent, row: Dealer): void {
        context.dealerService.delete(context.selected.Id).then(data => {
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
        window.showModal('#admin-dealers');
        this.updateMaterial();
    }
    private closeModal(): void {
        window.closeModal('#admin-dealers');
    }
}
