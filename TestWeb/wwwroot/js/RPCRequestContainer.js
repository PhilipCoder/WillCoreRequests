import {request,globalTokens} from "./request/request.js";
import {PersonModel} from "./models/PersonModel.js";
import {ReceiptModel} from "./models/ReceiptModel.js";
import {ProductModel} from "./models/ProductModel.js";

class RPCRequestContainer 
{
  /**
  * RPCRequestContainer. Use instance to make requests to: api/[controller]/[action]
  * @param { String } baseURL
  */
  constructor(baseUrl) 
    {
      /**
      * @type String
      */
      this.baseUrl = baseUrl;
      
      this._GetAllPersons = new request(this.baseUrl,"api/RPC/GetAllPersons","GET",{}, PersonModel);
      
      this._GetPersonById = new request(this.baseUrl,"api/RPC/GetPerson","GET",{id:"QUERY"}, PersonModel);
      
      this._GetReceiptByPersonidAndReceiptid = new request(this.baseUrl,"api/RPC/GetReceipt","GET",{personid:"QUERY",receiptid:"QUERY"}, ReceiptModel);
      
      this._AddReceipt = new request(this.baseUrl,"api/RPC/AddReceipt","POST",{value:"BODY"}, PersonModel);
      
      this._UpdateReceiptById = new request(this.baseUrl,"api/RPC/UpdateReceipt","PUT",{id:"QUERY",value:"BODY"}, PersonModel);
      
      this._DeletePersonById = new request(this.baseUrl,"api/RPC/DeletePerson","DELETE",{id:"QUERY"}, PersonModel);
      
    }
    setHttpHeaders(headerObject)
    {
      for (var key in headerObject){
      globalTokens[key] = headerObject[key];
        }
    }
    /**
    * Method used to invoke request of type: GET to URL: api/RPC/GetAllPersons.
    * @return {PromiseLike<Person[]>}
    */
    GetAllPersons ()
    {
      return this._GetAllPersons.ExecuteRequest({});
    }
    
    /**
    * Method used to invoke request of type: GET to URL: api/RPC/GetPerson.
    * @param { Number } id
    * @return {PromiseLike<Person>}
    */
    GetPerson (id)
    {
      return this._GetPerson.ExecuteRequest({id:id});
    }
    
    /**
    * Method used to invoke request of type: GET to URL: api/RPC/GetReceipt.
    * @param { Number } personid
    * @param { Number } receiptid
    * @return {PromiseLike<Receipt>}
    */
    GetReceipt (personid,receiptid)
    {
      return this._GetReceipt.ExecuteRequest({personid:personid,receiptid:receiptid});
    }
    
    /**
    * Method used to invoke request of type: POST to URL: api/RPC/AddReceipt.
    * @param { Person } value
    * @return {PromiseLike<Person>}
    */
    AddReceipt (value)
    {
      return this._AddReceipt.ExecuteRequest({value:value});
    }
    
    /**
    * Method used to invoke request of type: PUT to URL: api/RPC/UpdateReceipt.
    * @param { Number } id
    * @param { Person } value
    * @return {PromiseLike<Person>}
    */
    UpdateReceipt (id,value)
    {
      return this._UpdateReceipt.ExecuteRequest({id:id,value:value});
    }
    
    /**
    * Method used to invoke request of type: DELETE to URL: api/RPC/DeletePerson.
    * @param { Number } id
    * @return {PromiseLike<Person>}
    */
    DeletePerson (id)
    {
      return this._DeletePerson.ExecuteRequest({id:id});
    }
    
}

export {PersonModel,ReceiptModel,ProductModel,RPCRequestContainer};
