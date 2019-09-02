import {request,globalTokens} from "./request/request.js";
import {PersonModel} from "./models/PersonModel.js";
import {ReceiptModel} from "./models/ReceiptModel.js";
import {ProductModel} from "./models/ProductModel.js";

class PersonRequestContainer 
{
  /**
  * PersonRequestContainer. Use instance to make requests to: api/[controller]
  * @param { String } baseURL
  */
  constructor(baseUrl) 
    {
      /**
      * @type String
      */
      this.baseUrl = baseUrl;
      
      this._Get = new request(this.baseUrl,"api/Person","GET",{}, PersonModel);
      
      this._GetById = new request(this.baseUrl,"api/Person/{id}","GET",{id:"URL"}, PersonModel);
      
      this._GetByPersonIdAndReceiptId = new request(this.baseUrl,"api/Person/{personId}/{receiptId}","GET",{personId:"URL",receiptId:"URL"}, ReceiptModel);
      
      this._Post = new request(this.baseUrl,"api/Person","POST",{value:"BODY"}, null);
      
      this._PutById = new request(this.baseUrl,"api/Person/{id}","PUT",{id:"URL",value:"BODY"}, null);
      
      this._DeleteById = new request(this.baseUrl,"api/Person/{id}","DELETE",{id:"URL"}, null);
      
    }
    setHttpHeaders(headerObject)
    {
      for (var key in headerObject){
      globalTokens[key] = headerObject[key];
        }
    }
    /**
    * Method used to invoke request of type: GET to URL: api/Person.
    * @return {PromiseLike<Person[]>}
    */
    Get ()
    {
      return this._Get.ExecuteRequest({});
    }
    
    /**
    * Method used to invoke request of type: GET to URL: api/Person/{id}.
    * @param { Number } id
    * @return {PromiseLike<Person>}
    */
    GetById (id)
    {
      return this._GetById.ExecuteRequest({id:id});
    }
    
    /**
    * Method used to invoke request of type: GET to URL: api/Person/{personId}/{receiptId}.
    * @param { Number } personId
    * @param { Number } receiptId
    * @return {PromiseLike<Receipt>}
    */
    GetByPersonIdAndReceiptId (personId,receiptId)
    {
      return this._GetByPersonIdAndReceiptId.ExecuteRequest({personId:personId,receiptId:receiptId});
    }
    
    /**
    * Method used to invoke request of type: POST to URL: api/Person.
    * @param { Person } value
    * @return {PromiseLike<Boolean>}
    */
    Post (value)
    {
      return this._Post.ExecuteRequest({value:value});
    }
    
    /**
    * Method used to invoke request of type: PUT to URL: api/Person/{id}.
    * @param { Number } id
    * @param { Person } value
    * @return {PromiseLike<Boolean>}
    */
    PutById (id,value)
    {
      return this._PutById.ExecuteRequest({id:id,value:value});
    }
    
    /**
    * Method used to invoke request of type: DELETE to URL: api/Person/{id}.
    * @param { Number } id
    * @return {PromiseLike<Boolean>}
    */
    DeleteById (id)
    {
      return this._DeleteById.ExecuteRequest({id:id});
    }
    
}

export {PersonModel,ReceiptModel,ProductModel,PersonRequestContainer};
