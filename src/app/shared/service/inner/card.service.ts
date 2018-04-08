import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { Http } from '@angular/http';
import { LoaderService } from '../inner/loader.service';
import { ProductCard } from '../../model/entity/product-card.model';

@Injectable()
export class CardService {
    // private listProduct: ProductCard[] = [];

    constructor(
    ) {
    }

    public add(product: ProductCard): void {
        const productFind = this.getProduct(product.ProductId);
        if (typeof productFind !== 'undefined') {
            product.Count = productFind.Count + 1;
        }
        this.saveProduct(product);
    }

    public decrease(productId: number): void {
        const productFind = this.getProduct(productId);
        if (typeof productFind !== 'undefined') {
            productFind.Count--;
            if (productFind.Count > 0){
                this.saveProduct(productFind);
            } else {
                this.delete(productId);
            }
        }
    }

    public delete(productId: number): void {
        var products = this.getProducts();
        const productFind = products.find(x => x.ProductId === productId);
        const index = products.indexOf(productFind, 0);
        if (index > -1 && typeof productFind !== 'undefined') {
            products.splice(index, 1);

            this.setProducts(products);
        }
    }
    
    public getTotal(): number {
        let total = 0;
        var products = this.getProducts();
        for (const prod of products) {
            total += (prod.Count * prod.Price);
        }
        return total;
    }

    public getProducts(): ProductCard[] {
        let products = [];
        const json = sessionStorage.getItem('productCard');
        if (json !== null) {
            const obj = JSON.parse(json);

            if (typeof obj.products !== 'undefined') {
                products = obj.products;
            }
        }
        return products;
    }

    public cleanProducts(): void {
        const listProduct: ProductCard[] = [];
        this.setProducts(listProduct);
    }

    private saveProduct(product: ProductCard): void {
        var products = this.getProducts();
        var productFind = products.find(x => x.ProductId === product.ProductId);
        if (typeof productFind !== 'undefined') {
            productFind.Count = product.Count;
        } else {
            products.push(product);
        }
        
        this.setProducts(products);
    }
    private getProduct(productId: number): ProductCard {
        const products = this.getProducts();
        const product = products.find(x => x.ProductId === productId);
        return product;
    }

    private setProducts(products: ProductCard[]): void {
        sessionStorage.setItem('productCard', JSON.stringify({
            products: products
        }));
    }
    private removeToken(): void {
        sessionStorage.removeItem('productCard');
    }    
}
