

/**
* Result class Product.
*/
class Product {
    /**
    * Creates Instance Of The Result Class.
    * @param {String} name
    * @param {String} description
    */
    constructor(name,description) {
        /**
        * @type {String}
        */
        this.name = typeof(name._singleParameter) !== "undefined" ? name.name : name;
        /**
        * @type {String}
        */
        this.description = typeof(name._singleParameter) !== "undefined" ? name.description : description;
    }

}

export {Product};
