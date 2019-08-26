
/**
* Creates Instance Of The Result Class.
* @param {Object} data
*/
function Receipt (data){
        /**
        * @type {String}
        */
        this.id = typeof(data.id) !== "undefined" ? data.id : null;
        /**
        * @type {Number}
        */
        this.byteValue = typeof(data.byteValue) !== "undefined" ? data.byteValue : null;
        /**
        * @type {Number}
        */
        this.sByteValue = typeof(data.sByteValue) !== "undefined" ? data.sByteValue : null;
        /**
        * @type {String}
        */
        this.charValue = typeof(data.charValue) !== "undefined" ? data.charValue : null;
        /**
        * @type {Number}
        */
        this.decimalValue = typeof(data.decimalValue) !== "undefined" ? data.decimalValue : null;
        /**
        * @type {Number}
        */
        this.doubleValue = typeof(data.doubleValue) !== "undefined" ? data.doubleValue : null;
        /**
        * @type {Number}
        */
        this.floatValue = typeof(data.floatValue) !== "undefined" ? data.floatValue : null;
        /**
        * @type {Number}
        */
        this.intValue = typeof(data.intValue) !== "undefined" ? data.intValue : null;
        /**
        * @type {Number}
        */
        this.uintValue = typeof(data.uintValue) !== "undefined" ? data.uintValue : null;
        /**
        * @type {Number}
        */
        this.longValue = typeof(data.longValue) !== "undefined" ? data.longValue : null;
        /**
        * @type {String}
        */
        this.stringValue = typeof(data.stringValue) !== "undefined" ? data.stringValue : null;
        /**
        * @type {Product[]}
        */
        this.products = typeof(data.products) !== "undefined" ? data.products.map(function(dataRow){ return new products(dataRow);}) : null;
}


