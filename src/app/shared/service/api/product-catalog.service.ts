import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { Http } from '@angular/http';
import { LoaderService } from '../inner/loader.service';
import { ProductCatalog } from '../../model/data/product-catalog.model';
import { SearchProduct } from '../../model/data/search-product.model';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProductCatalogService extends BaseService {
    protected controllerUrl = this.apiUrl + 'productCatalog';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    public search(data: SearchProduct): Promise<ProductCatalog[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/search`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            const products = [];
            for(const product of data.products) {
                products.push(this.castProductCatalog(product));
            }
            this.loaderService.stop();
            return products;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getCountProduct(data: SearchProduct): Promise<number> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getCountProduct`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return +data.count;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getProduct(id: number): Promise<ProductCatalog> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castProductCatalog(data.product);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    private castProductCatalog(data: ProductCatalog): ProductCatalog {
        const productCatalog = new ProductCatalog();
        productCatalog.CategoryId = data.CategoryId;
        productCatalog.CategoryName = data.CategoryName;
        productCatalog.Created = data.Created;
        productCatalog.Description = data.Description;
        productCatalog.Enable = data.Enable;
        productCatalog.EnableStr = data.EnableStr;
        productCatalog.Id = data.Id;
        productCatalog.Name = data.Name;
        productCatalog.Price = data.Price;
        productCatalog.Status = data.Status;
        productCatalog.Updated = data.Updated;
        productCatalog.DealerName = data.DealerName;
        productCatalog.DealerId = data.DealerId;
        productCatalog.CompanyName = data.CompanyName;
        productCatalog.CompanyId = data.CategoryId;
        productCatalog.PhotoPath = data.PhotoPath;

        return productCatalog;
    }
}
