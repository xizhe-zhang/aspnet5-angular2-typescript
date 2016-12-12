export class Product {
    SkuId: number;
    SkuCode: string;
    SkuName: string;
    SkuPrice: number;
    BarCode: string;
    StoreId: string;
    StoreName: string;
    PicUrl: string;

    constructor(Id: number,
        Code: string,
        Name: string,
        Price: number,
        Barcode: string,
        StoreId: string,
        StoreName: string,
        PicUrl: string) {
        this.SkuId = Id;
        this.SkuCode = Code;
        this.SkuName = Name;
        this.SkuPrice = Price;
        this.BarCode = Barcode;
        this.StoreId = StoreId;
        this.StoreName = StoreName;
        this.PicUrl = PicUrl;
    }
}