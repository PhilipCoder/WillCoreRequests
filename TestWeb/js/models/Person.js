import {Receipt} from "./Receipt.js";

//===============================================
//<summary>Result class Person.</summary>
//===============================================
class Person {
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
        //<returns>String</returns>
        //===============================================
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.surname = typeof(data.surname) !== "undefined" ? data.surname : null;
        //===============================================
        //<returns>Date</returns>
        //===============================================
        this.dateOfBirth = typeof(data.dateOfBirth) !== "undefined" ? data.dateOfBirth : null;
        //===============================================
        //<returns>Number[]</returns>
        //===============================================
        this.profileImage = typeof(data.profileImage) !== "undefined" ? data.profileImage : null;
        //===============================================
        //<returns>Receipt[]</returns>
        //===============================================
        this.receipts = typeof(data.receipts) !== "undefined" ? data.receipts.map(dataRow => new receipts(dataRow)) : null;
    }

}

export {Person};
