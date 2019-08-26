

//===============================================
//<summary>Result class Product.</summary>
//===============================================
class Product {
    //===============================================
    //<summary>Creates Instance Of The Result Class.</summary>
    //<param>CodeBulder.JS.Types.JSObject</param>
    //<typeparam>Object</typeparam>
    //===============================================
    constructor(data) {
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        //===============================================
        //<returns>String</returns>
        //===============================================
        this.description = typeof(data.description) !== "undefined" ? data.description : null;
    }

}

export {Product};
