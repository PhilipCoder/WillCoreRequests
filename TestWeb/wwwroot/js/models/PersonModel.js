import {ReceiptModel} from "./ReceiptModel.js";

class PersonModel 
{
  /**
  * POCO class $PersonModel
  * @param { String } id
  * @param { String } name
  * @param { String } surname
  * @param { Date } dateOfBirth
  * @param { Number[] } profileImage
  * @param { Receipt[] } receipts
  */
  constructor(id,name,surname,dateOfBirth,profileImage,receipts) 
    {
      /**
      * @type String
      */
      this.id = id;
      
      /**
      * @type String
      */
      this.name = name;
      
      /**
      * @type String
      */
      this.surname = surname;
      
      /**
      * @type Date
      */
      this.dateOfBirth = dateOfBirth;
      
      /**
      * @type Number[]
      */
      this.profileImage = profileImage;
      
      /**
      * @type Receipt[]
      */
      this.receipts = typeof receipts !== "undefined" ? receipts.map(x=> new ReceiptModel(x.id,x.byteValue,x.sByteValue,x.charValue,x.decimalValue,x.doubleValue,x.floatValue,x.intValue,x.uintValue,x.longValue,x.stringValue,x.products)) : [];
    }
    _loadFromObject(dataObject)
    {
      if (typeof dataObject === "undefined") return;
      /**
      * @type String
      */
      this.id = dataObject.id;
      
      /**
      * @type String
      */
      this.name = dataObject.name;
      
      /**
      * @type String
      */
      this.surname = dataObject.surname;
      
      /**
      * @type Date
      */
      this.dateOfBirth = dataObject.dateOfBirth;
      
      /**
      * @type Number[]
      */
      this.profileImage = dataObject.profileImage;
      
      /**
      * @type Receipt[]
      */
      this.receipts = typeof dataObject !== "undefined" &&  typeof dataObject.receipts !== "undefined" ? dataObject.receipts.map(row => ( row => { let newObj = new ReceiptModel(); newObj._loadFromObject(row); return newObj; })(row)) : [];
      
    }
}

export {PersonModel};
