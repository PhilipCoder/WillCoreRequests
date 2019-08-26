
/**
* Creates Instance Of The Result Class.
* @param {Object} data
*/
function Product (data){
        /**
        * @type {String}
        */
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        /**
        * @type {String}
        */
        this.description = typeof(data.description) !== "undefined" ? data.description : null;
}


