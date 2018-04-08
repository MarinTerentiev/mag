export class SearchProduct {
    constructor() {
        this.DealerId = -1;
        this.CompanyId = -1;
        this.CategoryId = -1;
        this.PageNumber = 1;
        this.RowsPage = 20;
    }

    public DealerId: number;
    public CompanyId: number;
    public CategoryId: number;
    public PageNumber: number;
    public RowsPage: number;
}
