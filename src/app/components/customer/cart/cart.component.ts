import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { ProductCard } from '../../../shared/model/entity/product-card.model';
import { CardService } from '../../../shared/service/inner/card.service';
import { ModalConfirmService } from '../../../shared/service/inner/modal-confirm.service';
import { ModalConfirmConfig } from '../../../shared/model/inner/modal-confirm-config.model';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.less']
})
export class CartComponent extends BaseComponent {
    public listProducts: ProductCard[] = [];
    selected: ProductCard = new ProductCard();

    constructor(
        private cardService: CardService,
        public modalConfirmService: ModalConfirmService
    ){
        super();

        const handler = {
            context: this,
            ok: this.delete
        };
        const modalConfirmConfig = new ModalConfirmConfig('Delete.', 'Are you sure you want to delete this product?', handler);
        this.modalConfirmService.init(modalConfirmConfig);
        
        this.getData()
    }

    private getData(): void {
        this.listProducts = this.cardService.getProducts();
    }

    add(product: ProductCard): void {
        product.Count++;
        this.cardService.add(product);
    }

    decrease(product: ProductCard): void {
        product.Count--;
        if (product.Count > 0) {
            this.cardService.decrease(product.ProductId);
        } else {
            this.cardService.delete(product.ProductId);
            const index = this.listProducts.indexOf(product, 0);
            if (index > -1) {
                this.listProducts.splice(index, 1);
            }
        }      
    }

    confirmDelete(product: ProductCard): void {
        this.modalConfirmService.open();
        this.selected = product;
    }

    delete(context: CartComponent, row: ProductCard): void {
        context.cardService.delete(context.selected.ProductId);
        const index = context.listProducts.indexOf(context.selected, 0);
        if (index > -1) {
            context.listProducts.splice(index, 1);
        }
    }

    getTotal(): number {
        return this.cardService.getTotal();
    }
}
