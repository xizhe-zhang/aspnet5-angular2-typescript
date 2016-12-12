import { Component, OnInit } from '@angular/core';
import { Product } from '../core/domain/product';
import { Paginated } from '../core/common/paginated';
import { DataService } from '../core/services/data.service';
import { FeedService } from '../core/services/feed.service';
import { SignalRConnectionStatus } from '../core/interfaces';
import { NotificationService } from '../core/services/notification.service';

@Component({
    selector: 'home',
    templateUrl: './app/components/home.component.html'
})
export class HomeComponent extends Paginated implements OnInit {
    private _productAPI: string = 'api/products/';
    private _products: Array<Product>;
    public sku: any;
    public cart: any;


    constructor(public productsService: DataService, public feedService: FeedService, public notificationService: NotificationService) {
        super(0, 0, 0);
        this.sku = new Sku();
        this.cart = new ShoppingCart();

        this.feedService.addFeed.subscribe(
            feed => {
                console.log(feed);
                if (feed.UnionId !== undefined && feed.UnionId !== null) {
                    let barCode = feed.UnionId
                    this.searchProduct();
                    let scanSku = this.sku.GetSkuByBarCode(barCode);
                    if (scanSku != null) {
                        this.cart.AddSku(scanSku, this.cart.GetMultiple());
                        this.notificationService.printSuccessMessage("barcode:" + barCode);
                        this.sku = new Sku();
                        for (let origSku of this._products) {
                            if (origSku.BarCode === barCode)
                                this.sku.AddSku(origSku.SkuId, origSku.SkuCode, origSku.SkuName, origSku.SkuPrice, origSku.BarCode);
                        }
                    }
                }
            }
        );
    }

    searchProduct(): void {
        this.sku = new Sku();
        for (let origSku of this._products) {
            this.sku.AddSku(origSku.SkuId, origSku.SkuCode, origSku.SkuName, origSku.SkuPrice, origSku.BarCode);
        }
    }

    ngOnInit() {
        this.productsService.set(this._productAPI, 12);
        this.getProducts();
    }

    getProducts(): void {
        let self = this;
        self.productsService.getProducts("1", self._page)
            .subscribe(res => {
                var data: any = res.json();
                self._products = [];
                for (let product of data.Items) {
                    self._products.push(new Product(product.Id, product.Code, product.Name, product.Price, product.Barcode, product.StoreId, product.StoreName, product.PicUrl));
                }
                for (let origSku of self._products) {
                    this.sku.AddSku(origSku.SkuId, origSku.SkuCode, origSku.SkuName, origSku.SkuPrice, origSku.BarCode);
                }
            },
            error => console.error('Error: ' + error));
    }

    addToCart(product: Product): void {
        this.cart.AddSku(this.sku.GetById(product.SkuId), this.cart.GetMultiple());
    }
}