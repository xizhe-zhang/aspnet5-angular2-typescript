/*orderline*/
function DTOOrderLine(skuName, reqQty, PPrice, sku)
{
    this.SkuName = skuName;
    this.ReqQty = reqQty;
    this.PPrice = PPrice;
    this.Sku = sku;
}

function OrderLine()
{
    this.lines = new Array();s
}

OrderLine.prototype.AddOrderLine = function (skuName, reqQty, PPrice, sku) {
    this.lines.push(new DTOOrderLine(skuName, reqQty, PPrice, sku));
}


/*cust*/
function DTOCust(custId, custNo, custName,orderLines) {
    this.CustId = custId;
    this.CustNo = custNo;
    this.CustName = custName;
    this.OrderLines = orderLines;
}
function Cust() {
    this.custs = new Array();
    this.activeCust = null;
    this.currentPageIndex = 0;
    this.loadDone = false;
}

Cust.prototype.changePageIndex = function (pageIndex) {
    this.currentPageIndex = pageIndex;
}

Cust.prototype.AddCust = function (custId, custNo, custName, OrderLine) {
    this.custs.push(new DTOCust(custId, custNo, custName, OrderLine));
}

Cust.prototype.setActiveCust = function (custId) {
    for (var i = 0; i < this.custs.length; i++) {
        var s = this.custs[i];
        if (s.CustId == custId) {
            this.activeCust = s;
            return true;
        }
    }

    return false;
}

Cust.prototype.GetCount = function () {
    return this.custs.length;
}

/*SKU*/
function DTOSku(skuId, skuCode, skuName, skuPrice,barCode) {
    this.SkuId = skuId;
    this.SkuCode = skuCode;
    this.SkuName = skuName;
    this.SkuPrice = skuPrice;
    this.BarCode = barCode;
}
function Sku() {
    this.skus = new Array();
    this.currentPageIndex = 0;
    this.loadDone = false;
}
Sku.prototype.changePageIndex = function (pageIndex) {
    this.currentPageIndex = pageIndex;
}

Sku.prototype.AddSku = function (skuId, skuCode, skuName, skuPrice, barCode) {
    this.skus.push(new DTOSku(skuId, skuCode, skuName, skuPrice, barCode))
}

Sku.prototype.GetCount = function () {
    return this.skus.length;
}

Sku.prototype.GetById = function (skuId) {
    for (var i = 0; i < this.skus.length; i++) {
        var s = this.skus[i];
        if (s.SkuId == skuId) {
            return s;
        }
    }
    return false;
}
Sku.prototype.GetSkuByBarCode = function (barCode) {
    for (var i = 0; i < this.skus.length; i++) {
        var s = this.skus[i];
        if (s.BarCode == barCode) {
            return s;
        }
    }
    return null;
}

/*SKU*/

/*ShoppingCart*/

function CartSku(dtoSku, qty) {
    this.SkuId = dtoSku.SkuId;
    this.SkuCode = dtoSku.SkuCode;
    this.SkuName = dtoSku.SkuName;


    this._OrigOffRate = 100;
    this.SPrice = dtoSku.SkuPrice;
    this.OffRate = 100;
    this.PPrice = 0.00;
    this.Qty = qty;
    this.PAmt = 0;
    this.PPriceInput = 0;

}

CartSku.prototype.GetPAmt = function () {
    return this.PPrice * this.Qty;
}

CartSku.prototype.SetPrice = function () {
    if (this.OffRate == -20)
    {
        return;
    }
    this.PPrice = this.SPrice * this.OffRate / 100;
}

CartSku.prototype.SetOffRate = function (offRate)
{
    if (offRate == -20)
    {
        this.OffRate = offRate;
        return;
    }

    if (offRate == 0)
    {
        offRate = 100;
    }
    this.OffRate = offRate;
    this.PPrice = Number(this.SPrice * this.OffRate / 100).toFixed(2);

}

if (typeof ShoppingCartOperator == "undefined") {
    var ShoppingCartOperator = {};
    ShoppingCartOperator.Qty = 0;
    ShoppingCartOperator.Discount = 1;
    ShoppingCartOperator.Price = 2;
    ShoppingCartOperator.Multiple = 3;
    this.currentMultiple = 1;
}

function ShoppingCart() {
    this.cartSkus;
    this.ActiveCartSku;
    this.currentMultiple;
    this.OldCartSku;
    this.ActiveOperator;
    this.OldOperator;
    this.OldCmd;
    
    this.Init();
}

ShoppingCart.prototype.ChangeOperator = function (ShoppingCartOperator)
{
    if (this.OldOperator == null || this.OldOperator != ShoppingCartOperator)
    {
        this.OldCmd = "";
    }

    this.OldOperator = ShoppingCartOperator;
    this.ActiveOperator = ShoppingCartOperator;
}

ShoppingCart.prototype.SetQtyOperator = function ()
{
    this.ChangeOperator(ShoppingCartOperator.Qty);
}
ShoppingCart.prototype.SetDiscountOperator = function () {
    this.ChangeOperator(ShoppingCartOperator.Discount);
}
ShoppingCart.prototype.SetPriceOperator = function () {
    this.ChangeOperator(ShoppingCartOperator.Price);
}
ShoppingCart.prototype.SetMultipleOperator = function () {
    this.ChangeOperator(ShoppingCartOperator.Multiple);
}


ShoppingCart.prototype.IsQtyOperator = function () {
    if (this.ActiveOperator == ShoppingCartOperator.Qty)
    {
        return true;
    }
    return false;
}
ShoppingCart.prototype.IsDiscountOperator = function () {
    if (this.ActiveOperator == ShoppingCartOperator.Discount) {
        return true;
    }
    return false;
}
ShoppingCart.prototype.IsPriceOperator = function () {
    if (this.ActiveOperator == ShoppingCartOperator.Price) {
        return true;
    }
    return false;
}
ShoppingCart.prototype.IsMultipleOperator = function () {
    if (this.ActiveOperator == ShoppingCartOperator.Multiple) {
        return true;
    }
    return false;
}


ShoppingCart.prototype.OperatorCmd = function(cmdval)
{
    if (this.ActiveOperator == ShoppingCartOperator.Multiple) {
        //数字
        if (!isNaN(cmdval)) {

            this.OldCmd = this.OldCmd + cmdval.toString();
            this.currentMultiple = Number(this.OldCmd);
            if (this.currentMultiple > 99) {
                this.currentMultiple = 99;
                this.OldCmd = this.currentMultiple.toString();
            }
        }
        else if (cmdval == 'd')
        {
            this.OldCmd = this.OldCmd.substr(0, this.OldCmd.length - 1);
            this.currentMultiple = Number(this.OldCmd);
            if (this.currentMultiple > 99) {
                this.currentMultiple = 99;
                
            } else if (this.currentMultiple <= 0)
            {
                this.currentMultiple = 1;
            }
            this.OldCmd = this.currentMultiple.toString();
        }
        return;
    }

    if (this.ActiveCartSku == null)
    {
        return;
    }

    //操作数量
    if (this.ActiveOperator == ShoppingCartOperator.Qty)
    {
        //数字
        if (!isNaN(cmdval)) {
            this.OldCmd = this.OldCmd + cmdval.toString();
            this.ActiveCartSku.Qty = Number(this.OldCmd);
            if (this.ActiveCartSku.Qty > 999)
            {
                this.ActiveCartSku.Qty = 999
            }
            
        }
        //delete
        else if (cmdval == 'd')
        {
            if (this.ActiveCartSku.Qty == 0)
            {
                this.Remove(this.ActiveCartSku.SkuId);
                return;
            }


            this.OldCmd = this.OldCmd.substr(0, this.OldCmd.length - 1);
            this.ActiveCartSku.Qty = Number(this.OldCmd);
            if (this.ActiveCartSku.Qty > 999) {
                this.ActiveCartSku.Qty = 999
            }
        }
        //this.ActiveCartSku.SetPrice();
    }
    else if (this.ActiveOperator == ShoppingCartOperator.Price)
    {
        if (!isNaN(cmdval))
        {
            this.OldCmd = this.OldCmd + cmdval.toString();
            
        }
        else if (cmdval == 'p') {
            if (this.OldCmd.indexOf(".", 0) > 0) {
                return;
            }
            else {
                this.OldCmd = this.OldCmd + ".";
            }
        }
        else if (cmdval == 'd')
        {
            this.OldCmd = this.OldCmd.substr(0, this.OldCmd.length - 1);
        }

        var pPrice = Number(this.OldCmd).toFixed(2);
        this.ActiveCartSku.PPrice = pPrice;
        this.ActiveCartSku.SetOffRate(-20);//手输价格，rate=-20
    }
    else if (this.ActiveOperator == ShoppingCartOperator.Discount) {
        if (!isNaN(cmdval)) {
            this.OldCmd = this.OldCmd + cmdval.toString();

        }
        else if (cmdval == 'd')
        {
            this.OldCmd = this.OldCmd.substr(0, this.OldCmd.length - 1);
        }
        var rate = Number(this.OldCmd);
        this.ActiveCartSku.SetOffRate(rate);//输入rate ，pprice计算出来
    }
    

}

ShoppingCart.prototype.Init = function ()
{
    this.cartSkus = new Array();
    this.ActiveCartSku = null;
    this.ActiveOperator = ShoppingCartOperator.Qty;

    this.OldCartSku = null;
    this.OldOperator = null;
    this.OldCmd = "";
    this.currentMultiple = 1;
}

ShoppingCart.prototype.GetCount = function () {
    return this.cartSkus.length;
}

ShoppingCart.prototype._GetSkuById = function (skuId) {
    for (var i = 0; i < this.cartSkus.length; i++) {
        var s = this.cartSkus[i];
        if (s.SkuId == skuId) {
            return s;
        }
    }
    return null;
}


 
ShoppingCart.prototype.AddSku = function (dtoSku, qty) {
    if (dtoSku == null) {
        return;
    }
    var targetCartSku = this._GetSkuById(dtoSku.SkuId);
    if (targetCartSku == null) {
        //add new
        targetCartSku = new CartSku(dtoSku, qty);
        this.cartSkus.push(targetCartSku);
    }
    else {
        //add qty
        targetCartSku.Qty = parseInt(targetCartSku.Qty) + qty;
        if (targetCartSku.Qty <= 0) {
            this.Remove(targetCartSku.SkuId);
        }
    }
    targetCartSku.SetPrice();
    this.ActiveCartSku = targetCartSku;
    if (this.OldCartSku == null || this.OldCartSku.SkuId != this.ActiveCartSku.SkuId) {
        this.OldCmd = "";
    }

    this.OldCartSku = this.ActiveCartSku;
    this.currentMultiple = 1;
}
ShoppingCart.prototype.Remove = function (skuId) {
    if (skuId == null) {
        return;
    }

    for (var i = 0; i < this.cartSkus.length; i++) {
        var s = this.cartSkus[i];
        if (s.SkuId == skuId) {
            this.cartSkus.splice(i, 1);
            continue;
        }
    }

    //如果全部移除
    if (this.cartSkus.length == 0)
    {
        this.ActiveCartSku = null;
        return;
    }
    //否则选中最后一个

    this.SelectedActiveCartSku(this.cartSkus[this.cartSkus.length -1].SkuId);

}

ShoppingCart.prototype.Clear = function ()
{
    this.Init();
}

ShoppingCart.prototype.GetMultiple = function () {
    
    return this.currentMultiple;
}

ShoppingCart.prototype.GetTotalAmt = function (skuId) {
    var totalAmt = 0;
    for (var i = 0; i < this.cartSkus.length; i++) {
        totalAmt += this.cartSkus[i].GetPAmt();
    }

    return totalAmt;
}

ShoppingCart.prototype.IsActiveCartSku = function (skuId)
{
    if (this.ActiveCartSku != null && this.ActiveCartSku.SkuId == skuId)
    {
        return true;
    }
    return false;
}

ShoppingCart.prototype.SelectedActiveCartSku = function (skuId)
{
    var targetCartSku = this._GetSkuById(skuId);
    if (targetCartSku == null) {
        return;
    }
    else {
        this.ActiveCartSku = targetCartSku;
        if (this.OldCartSku == null || this.OldCartSku.SkuId != this.ActiveCartSku.SkuId)
        {
            this.OldCmd = "";
        }

        this.OldCartSku = this.ActiveCartSku;
    }
}

/*ShoppingCart*/
