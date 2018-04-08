import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Company } from '../../../shared/model/entity/company.model';
import { CompanyService } from '../../../shared/service/api/company.service';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { ModalTempConfig } from '../../../shared/model/inner/modal-temp-config.model';
import { ModalTempType } from '../../../shared/enum';
import { ModalConfirmService } from '../../../shared/service/inner/modal-confirm.service';
import { ModalConfirmConfig } from '../../../shared/model/inner/modal-confirm-config.model';
declare var window: any;

@Component({
    selector: 'app-companies',
    templateUrl: './companies.component.html',
    styleUrls: ['./companies.component.less'],
    providers: [CompanyService]
})
export class CompaniesComponent extends BaseComponent implements OnInit  {
    public rows: Company[] = [];
    public selected: Company = new Company();

    constructor(
        private companyService: CompanyService,
        private modalTempService: ModalTempService,
        public modalConfirmService: ModalConfirmService
    ){
        super();

        this.getData();

        const handler = {
            context: this,
            ok: this.delete
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this company?', handler);
        this.modalConfirmService.init(modalConfirmConfig);
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getData(): void {
        this.companyService.get().then(data => {
            this.rows = data;
        });
    }

    create(): void {
        this.selected = new Company();
        this.openModal();
    }

    edit(row: Company): void {
        this.companyService.getById(row.Id).then(data => {
            this.selected = data;
            this.openModal();
        });
    }

    save(): void {
        const isNew = this.selected.Id === -1;
        this.companyService.save(this.selected).then(data => {
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

    confirmDelete(row: Company): void {
        this.modalConfirmService.open();
        this.selected = row;
    }
    delete(context: CompaniesComponent, row: Company): void {
        context.companyService.delete(context.selected.Id).then(data => {
            const deletedRow = context.rows.find(x => x.Id === context.selected.Id);
            const index = context.rows.indexOf(deletedRow, 0);
            if (index > -1) {
                context.rows.splice(index, 1);
            }
            context.modalConfirmService.close();
            context.modalTempService.showSuccess();
        }).catch(error => {
            context.modalTempService.showDanger();
        })
    }

    private openModal(): void {
        window.showModal('#dealer-company');
    }
    private closeModal(): void {
        window.closeModal('#dealer-company');
    }
}