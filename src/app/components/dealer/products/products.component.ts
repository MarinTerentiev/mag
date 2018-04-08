import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Product } from '../../../shared/model/entity/product.model';
import { ProductService } from '../../../shared/service/api/product.service';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';
import { ModalConfirmService } from '../../../shared/service/inner/modal-confirm.service';
import { ActivatedRoute } from '@angular/router';
import { ModalConfirmConfig } from '../../../shared/model/inner/modal-confirm-config.model';
import { CompanyService } from '../../../shared/service/api/company.service';
import { Company } from '../../../shared/model/entity/company.model';
import { Category } from '../../../shared/model/entity/category.model';

declare var window: any;

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.less'],
    providers: [ProductService,CompanyService]
})
export class ProductsComponent extends BaseComponent implements OnInit  {
    public rows: Product[] = [];
    public selected: Product = new Product();
    public company: Company = new Company();
    public categories: Category[] = [];
    public category: Category = new Category();
    public deletedCategory: Category = new Category();
    @ViewChild('productPhoto') productPhoto: ElementRef;

    constructor(
        private productService: ProductService,
        private modalTempService: ModalTempService,
        public modalConfirmService: ModalConfirmService,
        private route: ActivatedRoute,
        private companyService: CompanyService
    ){
        super();

        this.getParameters();
        this.getData();
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getParameters(): void {
		let sub: any = this.route
			.params
			.subscribe(params => {
				this.company.Id = params['id'];
			})
			.unsubscribe();
    }
    
    private getData(): void {
        this.companyService.getById(this.company.Id).then(data => {
            this.company = data;
        });
        const that = this;
        this.productService.getCategory(this.company.Id).then(data => {
            this.categories = data;
            this.productService.getByCompanyId(that.company.Id).then(data => {
                that.rows = data;
                for(const row of that.rows) {
                    row.EnableStr = row.Enable ? 'Yes' : 'No';
                    row.CategoryName = this.categories.find(x => x.Id === row.CategoryId).Name;
                }
            });
        });
    }

    create(): void {
        this.setSelected(new Product());
        this.openModal();
    }

    edit(row: Product): void {
        this.productService.getById(row.Id).then(data => {
            this.setSelected(data);
            this.openModal();
        });
    }

    save(): void {
        const isNew = this.selected.Id === -1;
        this.productService.save(this.selected).then(data => {
            data.EnableStr = data.Enable ? 'Yes' : 'No';
            data.CategoryName = this.categories.find(x => x.Id === data.CategoryId).Name;

            if (isNew) {    
                this.rows.push(data);
            } else {
                const editedRow = this.rows.find(x => x.Id === data.Id);
                const index = this.rows.indexOf(editedRow, 0);
                this.rows[index] = data;
            }

            this.productPhoto.nativeElement.value = null;

            this.closeModal();
            this.modalTempService.showSuccess();
        }).catch(error => {
            this.modalTempService.showDanger();
        });
    }

    confirmDelete(row: Product): void {
        const handler = {
            context: this,
            ok: this.delete
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this product?', handler);
        this.modalConfirmService.init(modalConfirmConfig);

        this.modalConfirmService.open();
        this.selected = row;
    }
    delete(context: ProductsComponent, row: Product): void {
        context.productService.delete(context.selected.Id).then(data => {
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

    confirmDeleteCategory(category: Category): void {
        const handler = {
            context: this,
            ok: this.deleteCategory
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this category?', handler);
        this.modalConfirmService.init(modalConfirmConfig);

        this.modalConfirmService.open();
        this.deletedCategory = category;
    }
    deleteCategory(context: ProductsComponent, row: Product): void {
        context.productService.deleteCategory(context.deletedCategory.Id).then(data => {
            const deletedCategory = context.categories.find(x => x.Id === context.deletedCategory.Id);
            const index = context.categories.indexOf(deletedCategory, 0);
            if (index > -1) {
                context.categories.splice(index, 1);
            }
            context.modalConfirmService.close();
            context.modalTempService.showSuccess();
        }).catch(error => {
            context.modalTempService.showDanger();
        });
    }

    createCategory(): void {
        this.category.CompanyId = this.company.Id;
        this.productService.saveCategory(this.category).then(data => {
            this.categories.push(data);
            this.category = new Category();
            this.modalTempService.showSuccess();
        }).catch(error => {
            this.modalTempService.showDanger();
        });
    }

    public triggerFalseClick() {
        const el: HTMLElement = this.productPhoto.nativeElement as HTMLElement;
        el.click();
    }

    loadImg(event: any) {
        if (event.target.files && event.target.files[0]) {
            const reader = new FileReader();

            reader.onload = (event2: any) => {
                this.selected.PhotoPath = event2.target.result;
            };

            reader.readAsDataURL(event.target.files[0]);
        }
    }


    private openModal(): void {
        window.showModal('#dealer-product');
        this.updateMaterial();
    }
    private closeModal(): void {
        window.closeModal('#dealer-product');
    }
    private setSelected(data: Product): void {
        this.selected.CategoryId = data.CategoryId;
        this.selected.CategoryName = data.CategoryName;
        this.selected.Created = data.Created;
        this.selected.Description = data.Description;
        this.selected.Enable = data.Enable;
        this.selected.EnableStr = data.EnableStr;
        this.selected.Id = data.Id;
        this.selected.Name = data.Name;
        this.selected.Price = data.Price;
        this.selected.Status = data.Status;
        this.selected.Updated = data.Updated;
        this.selected.PhotoPath = data.PhotoPath;
    }
}