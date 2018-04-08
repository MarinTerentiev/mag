import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { BaseService } from '../base.service';
import { Product } from '../../model/entity/product.model';
import { LoaderService } from '../inner/loader.service';
import { Category } from '../../model/entity/category.model';


@Injectable()
export class ProductService extends BaseService {
    protected controllerUrl = this.apiUrl + 'product';

    constructor(
        private http: Http,
        public loaderService: LoaderService
    ) {
        super();
    }

    public get(): Promise<Product[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get`;
        return this.request(this.http.get(url)).then((data) => {
            const products = [];
            for(const dt of data.products) {
                products.push(this.castProduct(dt));
            }
            this.loaderService.stop();
            return products;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getByCompanyId(companyId: number): Promise<Product[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getByCompanyId/${companyId}`;
        return this.request(this.http.get(url)).then((data) => {
            const products = [];
            for(const dt of data.products) {
                products.push(this.castProduct(dt));
            }
            this.loaderService.stop();
            return products;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getById(id: number): Promise<Product> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/get/${id}`;
        return this.request(this.http.get(url)).then((data) => {
            this.loaderService.stop();
            return this.castProduct(data.product);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public save(data: any): Promise<Product> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/post`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return this.castProduct(data.product);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public delete(id: number): Promise<string> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/delete/${id}`;
        return this.request(this.http.delete(url)).then((data) => {
            this.loaderService.stop();
            return data;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public getCategory(companyId: number): Promise<Category[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getCategory/${companyId}`;
        return this.request(this.http.get(url)).then((data) => {
            const categories = [];
            for(const ct of data.category) {
                categories.push(this.castCategory(ct));
            }
            this.loaderService.stop();
            return categories;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public saveCategory(data: any): Promise<Category> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/saveCategory`;
        return this.request(
            this.http.post(url, JSON.stringify(data), this.options)
        ).then((data) => {
            this.loaderService.stop();
            return this.castCategory(data.category);

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    public deleteCategory(id: number): Promise<string> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/deleteCategory/${id}`;
        return this.request(this.http.delete(url)).then((data) => {
            this.loaderService.stop();
            return data;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }

    getCategoryForCatalog(): Promise<Category[]> {
        this.loaderService.start();
        const url = `${this.controllerUrl}/getCategoryForCatalog`;
        return this.request(this.http.get(url)).then((data) => {
            const categories = [];
            for (const dt of data.categories) {
                categories.push(this.castCategory(dt));
            }
            this.loaderService.stop();
            return categories;

        }).catch((error) => {
            this.loaderService.stop();
            return error;
        });
    }
    
    private castProduct(data: Product): Product {
        const product = new Product();
        product.CategoryId = data.CategoryId;
        product.CategoryName = data.CategoryName;
        product.Created = data.Created;
        product.Description = data.Description;
        product.Enable = data.Enable;
        product.EnableStr = data.EnableStr;
        product.Id = data.Id;
        product.Name = data.Name;
        product.Price = data.Price;
        product.Status = data.Status;
        product.Updated = data.Updated;
        product.PhotoPath = data.PhotoPath;

        return product;
    }

    private castProductForCatalog(data: Product): Product {
        const product = new Product();
        product.CategoryId = data.CategoryId;
        product.Id = data.Id;
        product.Name = data.Name;

        return product;
    }

    private castCategory(data: Category): Category {
        const category = new Category();
        category.CompanyId = data.CompanyId;
        category.Created = data.Created;
        category.Id = data.Id;
        category.Name = data.Name;
        category.Status = data.Status;
        category.Updated = data.Updated;

        return  category;
    }
}
