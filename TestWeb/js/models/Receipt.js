import {Product} from "./Product.js";

//===============================================
//<summary>Result class Receipt.</summary>
//===============================================
class Receipt {
    //===============================================
    //<summary>Creates Instance Of The Result Class.</summary>
    //<param>CodeBulder.JS.Types.JSObject</param>
    //<typeparam>Object</typeparam>
    //===============================================
    constructor(data) {
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.id = typeof(data.id) !== "undefined" ? data.id : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.byteValue = typeof(data.byteValue) !== "undefined" ? data.byteValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.sByteValue = typeof(data.sByteValue) !== "undefined" ? data.sByteValue : null;
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.charValue = typeof(data.charValue) !== "undefined" ? data.charValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.decimalValue = typeof(data.decimalValue) !== "undefined" ? data.decimalValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.doubleValue = typeof(data.doubleValue) !== "undefined" ? data.doubleValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.floatValue = typeof(data.floatValue) !== "undefined" ? data.floatValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.intValue = typeof(data.intValue) !== "undefined" ? data.intValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.uintValue = typeof(data.uintValue) !== "undefined" ? data.uintValue : null;
        //===============================================
        //<returns>Number</returns>
        //===============================================
        this.longValue = typeof(data.longValue) !== "undefined" ? data.longValue : null;
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.stringValue = typeof(data.stringValue) !== "undefined" ? data.stringValue : null;
        //===============================================
        //<returns>Product[]</returns>
        //===============================================
        this.products = typeof(data.products) !== "undefined" ? data.products.map(dataRow => new products(dataRow)) : null;
    }

}

export {Receipt};
