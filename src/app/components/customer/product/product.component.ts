import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../../../shared/base.component';
import { ProductCatalog } from '../../../shared/model/data/product-catalog.model';
import { CardService } from '../../../shared/service/inner/card.service';
import { ProductCatalogService } from '../../../shared/service/api/product-catalog.service';
import { ProductCard } from '../../../shared/model/entity/product-card.model';

@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.less'],
    providers: [ProductCatalogService]
})
export class ProductComponent extends BaseComponent implements OnInit {
    public selected: ProductCatalog = new ProductCatalog();

    constructor(
        private productCatalogService: ProductCatalogService,
        private cardService: CardService,
        private route: ActivatedRoute
    ){
        super();

        this.getParameters();
        this.getData();
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getParameters(): void {
        const sub: any = this.route
            .params
            .subscribe(params => {
                this.selected.Id = params['id'];
            })
            .unsubscribe();
    }

    private getData(): void {
        this.productCatalogService.getProduct(this.selected.Id).then(data => {
            this.selected = data;
        });
    }

    getPhoto(): string {
        let path = '../../assets/images/no-image-slide.png';
        if (this.selected.PhotoPath !== '') {
            path = this.selected.PhotoPath;
        }
        return path;
    }

    add(): void {
        const productCard = new ProductCard();
        productCard.Count = 1;
        productCard.Price = this.selected.Price;
        productCard.ProductId = this.selected.Id;
        productCard.Name = this.selected.Name;
        productCard.DealerId = this.selected.DealerId;

        this.cardService.add(productCard);
    }
}
