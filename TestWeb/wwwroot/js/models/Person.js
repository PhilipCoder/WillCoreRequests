import {Receipt} from "./Receipt.js";

/**
* Result class Person.
*/
class Person {
    /**
    * Creates Instance Of The Result Class.
    * @param {String} id
    * @param {String} name
    * @param {String} surname
    * @param {Date} dateOfBirth
    * @param {Number[]} profileImage
    * @param {Receipt[]} receipts
    */
    constructor(id,name,surname,dateOfBirth,profileImage,receipts) {
        /**
        * @type {String}
        */
        this.id = typeof(id._singleParameter) !== "undefined" ? id.id : id;
        /**
        * @type {String}
        */
        this.name = typeof(id._singleParameter) !== "undefined" ? id.name : name;
        /**
        * @type {String}
        */
        this.surname = typeof(id._singleParameter) !== "undefined" ? id.surname : surname;
        /**
        * @type {Date}
        */
        this.dateOfBirth = typeof(id._singleParameter) !== "undefined" ? id.dateOfBirth : dateOfBirth;
        /**
        * @type {Number[]}
        */
        this.profileImage = typeof(id._singleParameter) !== "undefined" ? id.profileImage : profileImage;
        /**
        * @type {Receipt[]}
        */
        this.receipts = id._singleParameter && id.receipts ? id.receipts.map(dataRow => new Receipt(dataRow)) : receipts ? receipts.map(dataRow => new Receipt(dataRow)) : null;
    }

}

export {Person};
