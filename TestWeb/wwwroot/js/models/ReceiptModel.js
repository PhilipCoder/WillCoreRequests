import {ProductModel} from "./ProductModel.js";

class ReceiptModel 
{
  /**
  * POCO class $ReceiptModel
  * @param { String } id
  * @param { Number } byteValue
  * @param { Number } sByteValue
  * @param { String } charValue
  * @param { Number } decimalValue
  * @param { Number } doubleValue
  * @param { Number } floatValue
  * @param { Number } intValue
  * @param { Number } uintValue
  * @param { Number } longValue
  * @param { String } stringValue
  * @param { Product[] } products
  */
  constructor(id,byteValue,sByteValue,charValue,decimalValue,doubleValue,floatValue,intValue,uintValue,longValue,stringValue,products) 
    {
      /**
      * @type String
      */
      this.id = id;
      
      /**
      * @type Number
      */
      this.byteValue = byteValue;
      
      /**
      * @type Number
      */
      this.sByteValue = sByteValue;
      
      /**
      * @type String
      */
      this.charValue = charValue;
      
      /**
      * @type Number
      */
      this.decimalValue = decimalValue;
      
      /**
      * @type Number
      */
      this.doubleValue = doubleValue;
      
      /**
      * @type Number
      */
      this.floatValue = floatValue;
      
      /**
      * @type Number
      */
      this.intValue = intValue;
      
      /**
      * @type Number
      */
      this.uintValue = uintValue;
      
      /**
      * @type Number
      */
      this.longValue = longValue;
      
      /**
      * @type String
      */
      this.stringValue = stringValue;
      
      /**
      * @type Product[]
      */
      this.products = typeof products !== "undefined" ? products.map(x=> new ProductModel(x.name,x.description)) : [];
    }
    _loadFromObject(dataObject)
    {
      if (typeof dataObject === "undefined") return;
      /**
      * @type String
      */
      this.id = dataObject.id;
      
      /**
      * @type Number
      */
      this.byteValue = dataObject.byteValue;
      
      /**
      * @type Number
      */
      this.sByteValue = dataObject.sByteValue;
      
      /**
      * @type String
      */
      this.charValue = dataObject.charValue;
      
      /**
      * @type Number
      */
      this.decimalValue = dataObject.decimalValue;
      
      /**
      * @type Number
      */
      this.doubleValue = dataObject.doubleValue;
      
      /**
      * @type Number
      */
      this.floatValue = dataObject.floatValue;
      
      /**
      * @type Number
      */
      this.intValue = dataObject.intValue;
      
      /**
      * @type Number
      */
      this.uintValue = dataObject.uintValue;
      
      /**
      * @type Number
      */
      this.longValue = dataObject.longValue;
      
      /**
      * @type String
      */
      this.stringValue = dataObject.stringValue;
      
      /**
      * @type Product[]
      */
      this.products = typeof dataObject !== "undefined" &&  typeof dataObject.products !== "undefined" ? dataObject.products.map(row => ( row => { let newObj = new ProductModel(); newObj._loadFromObject(row); return newObj; })(row)) : [];
      
    }
}

export {ReceiptModel};
