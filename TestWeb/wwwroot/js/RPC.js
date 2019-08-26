import {request, globalTokens} from "./request/request.js";
import {Person} from "./models/Person.js";
import {Receipt} from "./models/Receipt.js";

/**
* RPCContext. Used to make requests to the RPC controller.
*/
class RPCRequestContainer {
    /**
    * Creates Instance Of RPCContext.
    * @param {String} baseURL
    */
    constructor(baseUrl) {
        /**
        * The base URL used for all requests on the class.
        * @type {String}
        */
        this._baseUrl = baseUrl;
        /**
        * Reqeust api/RPC/GetAllPersons
        */
        this._GetAllPersons = new request(this._baseUrl,"api/RPC/GetAllPersons","GET",{},Person);
        /**
        * Reqeust api/RPC/GetPerson
        */
        this._GetPerson = new request(this._baseUrl,"api/RPC/GetPerson","GET",{id:"QUERY"},Person);
        /**
        * Reqeust api/RPC/GetReceipt
        */
        this._GetReceipt = new request(this._baseUrl,"api/RPC/GetReceipt","GET",{personid:"QUERY",receiptid:"QUERY"},Receipt);
        /**
        * Reqeust api/RPC/AddReceipt
        */
        this._AddReceipt = new request(this._baseUrl,"api/RPC/AddReceipt","POST",{value:"BODY"},null);
        /**
        * Reqeust api/RPC/UpdateReceipt
        */
        this._UpdateReceipt = new request(this._baseUrl,"api/RPC/UpdateReceipt","PUT",{id:"QUERY",value:"BODY"},null);
        /**
        * Reqeust api/RPC/DeletePerson
        */
        this._DeletePerson = new request(this._baseUrl,"api/RPC/DeletePerson","DELETE",{id:"QUERY"},null);
    }
    /**
    * Sets the request headers for all requests.
    * @param {Object} headerObject
    */
    setHttpHeaders(headerObject){
        for (var key in headerObject){
            globalTokens[key] = headerObject[key];
        }
    }
    /**
    * Method to invoke request to api/RPC/GetAllPersons. Method: GET.
    * @return {PromiseLike<Person[]>}
    */
    async GetAllPersons (){
        return this._GetAllPersons.ExecuteRequest({}, globalTokens);
    }

    /**
    * Method to invoke request to api/RPC/GetPerson. Method: GET.
    * @param {Number} id
    * @return {PromiseLike<Person>}
    */
    async GetPerson (id){
        return this._GetPerson.ExecuteRequest({id:id}, globalTokens);
    }

    /**
    * Method to invoke request to api/RPC/GetReceipt. Method: GET.
    * @param {Number} personid
    * @param {Number} receiptid
    * @return {PromiseLike<Receipt>}
    */
    async GetReceipt (personid,receiptid){
        return this._GetReceipt.ExecuteRequest({personid:personid,receiptid:receiptid}, globalTokens);
    }

    /**
    * Method to invoke request to api/RPC/AddReceipt. Method: POST.
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    async AddReceipt (value){
        return this._AddReceipt.ExecuteRequest({value:value}, globalTokens);
    }

    /**
    * Method to invoke request to api/RPC/UpdateReceipt. Method: PUT.
    * @param {Number} id
    * @param {Person} value
    * @return {PromiseLike<String[]>}
    */
    async UpdateReceipt (id,value){
        return this._UpdateReceipt.ExecuteRequest({id:id,value:value}, globalTokens);
    }

    /**
    * Method to invoke request to api/RPC/DeletePerson. Method: DELETE.
    * @param {Number} id
    * @return {PromiseLike<String[]>}
    */
    async DeletePerson (id){
        return this._DeletePerson.ExecuteRequest({id:id}, globalTokens);
    }

}

export {RPCRequestContainer};
