import { Product } from '../entity/product.model';

export class ProductCatalog extends Product {
    public DealerName: string;
    public DealerId: number;
	public CompanyName: string;
	public CompanyId: number;
}
