import {Receipt} from "./Receipt.js";

/**
* Result class Person.
*/
class Person {
    /**
    * Creates Instance Of The Result Class.
    * @param {Object} data
    */
    constructor(data) {
        /**
        * @type {String}
        */
        this.id = typeof(data.id) !== "undefined" ? data.id : null;
        /**
        * @type {String}
        */
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        /**
        * @type {String}
        */
        this.surname = typeof(data.surname) !== "undefined" ? data.surname : null;
        /**
        * @type {Date}
        */
        this.dateOfBirth = typeof(data.dateOfBirth) !== "undefined" ? data.dateOfBirth : null;
        /**
        * @type {Number[]}
        */
        this.profileImage = typeof(data.profileImage) !== "undefined" ? data.profileImage : null;
        /**
        * @type {Receipt[]}
        */
        this.receipts = typeof(data.receipts) !== "undefined" ? data.receipts.map(dataRow => new receipts(dataRow)) : null;
    }

}

export {Person};
