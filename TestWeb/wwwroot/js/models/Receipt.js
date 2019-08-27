import {Product} from "./Product.js";

/**
* Result class Receipt.
*/
class Receipt {
    /**
    * Creates Instance Of The Result Class.
    * @param {String} id
    * @param {Number} byteValue
    * @param {Number} sByteValue
    * @param {String} charValue
    * @param {Number} decimalValue
    * @param {Number} doubleValue
    * @param {Number} floatValue
    * @param {Number} intValue
    * @param {Number} uintValue
    * @param {Number} longValue
    * @param {String} stringValue
    * @param {Product[]} products
    */
    constructor(id,byteValue,sByteValue,charValue,decimalValue,doubleValue,floatValue,intValue,uintValue,longValue,stringValue,products) {
        /**
        * @type {String}
        */
        this.id = typeof(id._singleParameter) !== "undefined" ? id.id : id;
        /**
        * @type {Number}
        */
        this.byteValue = typeof(id._singleParameter) !== "undefined" ? id.byteValue : byteValue;
        /**
        * @type {Number}
        */
        this.sByteValue = typeof(id._singleParameter) !== "undefined" ? id.sByteValue : sByteValue;
        /**
        * @type {String}
        */
        this.charValue = typeof(id._singleParameter) !== "undefined" ? id.charValue : charValue;
        /**
        * @type {Number}
        */
        this.decimalValue = typeof(id._singleParameter) !== "undefined" ? id.decimalValue : decimalValue;
        /**
        * @type {Number}
        */
        this.doubleValue = typeof(id._singleParameter) !== "undefined" ? id.doubleValue : doubleValue;
        /**
        * @type {Number}
        */
        this.floatValue = typeof(id._singleParameter) !== "undefined" ? id.floatValue : floatValue;
        /**
        * @type {Number}
        */
        this.intValue = typeof(id._singleParameter) !== "undefined" ? id.intValue : intValue;
        /**
        * @type {Number}
        */
        this.uintValue = typeof(id._singleParameter) !== "undefined" ? id.uintValue : uintValue;
        /**
        * @type {Number}
        */
        this.longValue = typeof(id._singleParameter) !== "undefined" ? id.longValue : longValue;
        /**
        * @type {String}
        */
        this.stringValue = typeof(id._singleParameter) !== "undefined" ? id.stringValue : stringValue;
        /**
        * @type {Product[]}
        */
        this.products = id._singleParameter && id.products ? id.products.map(dataRow => new Product(dataRow)) : products ? products.map(dataRow => new Product(dataRow)) : null;
    }

}

export {Receipt};
