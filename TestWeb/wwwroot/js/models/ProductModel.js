

class ProductModel 
{
  /**
  * POCO class $ProductModel
  * @param { String } name
  * @param { String } description
  */
  constructor(name,description) 
    {
      /**
      * @type String
      */
      this.name = name;
      
      /**
      * @type String
      */
      this.description = description;
      
    }
    _loadFromObject(dataObject)
    {
      if (typeof dataObject === "undefined") return;
      /**
      * @type String
      */
      this.name = dataObject.name;
      
      /**
      * @type String
      */
      this.description = dataObject.description;
      
    }
}

export {ProductModel};
