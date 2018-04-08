import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { SearchProduct } from '../../../shared/model/data/search-product.model';
import { ProductCatalog } from '../../../shared/model/data/product-catalog.model';
import { ProductCatalogService } from '../../../shared/service/api/product-catalog.service';
import { DealerService } from '../../../shared/service/api/dealer.service';
import { Dealer } from '../../../shared/model/entity/dealer.model';
import { Company } from '../../../shared/model/entity/company.model';
import { CompanyService } from '../../../shared/service/api/company.service';
import { Category } from '../../../shared/model/entity/category.model';
import { Product } from '../../../shared/model/entity/product.model';
import { ProductService } from '../../../shared/service/api/product.service';
import { CardService } from '../../../shared/service/inner/card.service';
import { ProductCard } from '../../../shared/model/entity/product-card.model';

@Component({
    selector: 'app-catalog',
    templateUrl: './catalog.component.html',
    styleUrls: ['./catalog.component.less'],
    providers: [ProductCatalogService, DealerService,CompanyService,ProductService]
})
export class CatalogComponent extends BaseComponent implements OnInit {
    public searchProduct = new SearchProduct();
    public productCatalog: ProductCatalog[] =[];
    public dealers: Dealer[] = [];
    public companies: Company[] = [];
    public companiesForDealer: Company[] = [];
    public categories: Category[] = [];
    public categoriesForCompany: Category[] = [];
    public pageNumbers = [];

    constructor(
        private productCatalogService: ProductCatalogService,
        private dealerService: DealerService,
        private companyService: CompanyService,
        private productService: ProductService,
        private cardService: CardService
    ){
        super();
        
        this.getData()
    }

    ngOnInit() {
        this.updateMaterial();
    }

    private getData(): void {
        this.search();

        this.dealerService.getForCatalog().then(data => {
            this.dealers = data;
        });

        this.companyService.getForCatalog().then(data => {
            this.companies = data;
            this.companiesForDealer = data;
        });

        this.productService.getCategoryForCatalog().then(data => {
            this.categories = data;
            this.categoriesForCompany = data;
        });
    }

    selectDealer(): void {
        this.companiesForDealer = this.companies.filter(x => x.DealerId == this.searchProduct.DealerId);
        this.searchProduct.CategoryId = -1;
        this.searchProduct.CompanyId = -1;
        this.searchProduct.PageNumber = 1;
    }

    selectCompany(): void {
        this.categoriesForCompany = this.categories.filter(x => x.CompanyId == this.searchProduct.CompanyId);
        this.searchProduct.CategoryId = -1;
        this.searchProduct.PageNumber = 1;
    }

    selectCategory(): void {
        this.searchProduct.PageNumber = 1;
    }

    search(): void {
        this.productCatalogService.search(this.searchProduct).then(data => {
            this.productCatalog = data;
        });
        this.productCatalogService.getCountProduct(this.searchProduct).then(data =>{
            this.pageNumbers = [];
            const count = Math.ceil(data / this.searchProduct.RowsPage);
            for(let i = 1; i <= count; i++) {
                this.pageNumbers.push(i);
            }
        });
    }
    
    searchByPage(page: number): void {
        this.searchProduct.PageNumber = page;
        this.search();
    }

    getPhoto(product: ProductCatalog): string {
        let path = '../../assets/images/no-image-slide.png';
        if (product.PhotoPath !== '') {
            path = product.PhotoPath;
        }
        return path;
    }

    add(product: ProductCatalog): void {
        const productCard = new ProductCard();
        productCard.Count = 1;
        productCard.Price = product.Price;
        productCard.ProductId = product.Id;
        productCard.Name = product.Name;
        productCard.DealerId = product.DealerId;

        this.cardService.add(productCard);
    }
}
