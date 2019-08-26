import {request, globalTokens} from "./request/request.js";
import {Person} from "./models/Person.js";
import {Receipt} from "./models/Receipt.js";

//===============================================
//<summary>RPCContext. Used to make requests to the RPC controller.</summary>
//===============================================
class RPCRequestContainer {
    //===============================================
    //<summary>Creates Instance Of RPCContext.</summary>
    //<param>CodeBulder.JS.Types.JSString</param>
    //<typeparam>String</typeparam>
    //===============================================
    constructor(baseUrl) {
        //===============================================
        //<summary>The base URL used for all requests on the class.</summary>
        //<returns>String</returns>
        //===============================================
        this._baseUrl = baseUrl;
        //===============================================
        //<summary>Reqeust api/RPC/GetAllPersons</summary>
        //===============================================
        this._GetAllPersons = new request(this._baseUrl,"api/RPC/GetAllPersons","GET",{},Person);
        //===============================================
        //<summary>Reqeust api/RPC/GetPerson</summary>
        //===============================================
        this._GetPerson = new request(this._baseUrl,"api/RPC/GetPerson","GET",{id:"QUERY"},Person);
        //===============================================
        //<summary>Reqeust api/RPC/GetReceipt</summary>
        //===============================================
        this._GetReceipt = new request(this._baseUrl,"api/RPC/GetReceipt","GET",{personid:"QUERY",receiptid:"QUERY"},Receipt);
        //===============================================
        //<summary>Reqeust api/RPC/AddReceipt</summary>
        //===============================================
        this._AddReceipt = new request(this._baseUrl,"api/RPC/AddReceipt","POST",{value:"BODY"},null);
        //===============================================
        //<summary>Reqeust api/RPC/UpdateReceipt</summary>
        //===============================================
        this._UpdateReceipt = new request(this._baseUrl,"api/RPC/UpdateReceipt","PUT",{id:"QUERY",value:"BODY"},null);
        //===============================================
        //<summary>Reqeust api/RPC/DeletePerson</summary>
        //===============================================
        this._DeletePerson = new request(this._baseUrl,"api/RPC/DeletePerson","DELETE",{id:"QUERY"},null);
    }
    //===============================================
    //<summary>Sets the request headers for all requests.</summary>
    //<param>CodeBulder.JS.Types.JSObject</param>
    //<typeparam>Object</typeparam>
    //===============================================
    setHttpHeaders(headerObject){
        for (var key in headerObject){
            globalTokens[key] = headerObject[key];
        }
    }
    //===============================================
    //<summary>Method to invoke request to api/RPC/GetAllPersons. Method: GET.</summary>
    //<returns>PromiseLike<Person[]></returns>
    //===============================================
    async GetAllPersons (){
        return this._GetAllPersons.ExecuteRequest({}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/RPC/GetPerson. Method: GET.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<Person></returns>
    //===============================================
    async GetPerson (id){
        return this._GetPerson.ExecuteRequest({id:id}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/RPC/GetReceipt. Method: GET.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<Receipt></returns>
    //===============================================
    async GetReceipt (personid,receiptid){
        return this._GetReceipt.ExecuteRequest({personid:personid,receiptid:receiptid}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/RPC/AddReceipt. Method: POST.</summary>
    //<param>CodeBulder.JS.Types.JSClassType</param>
    //<typeparam>Person</typeparam>
    //<returns>PromiseLike<String></returns>
    //===============================================
    async AddReceipt (value){
        return this._AddReceipt.ExecuteRequest({value:value}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/RPC/UpdateReceipt. Method: PUT.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<param>CodeBulder.JS.Types.JSClassType</param>
    //<typeparam>Person</typeparam>
    //<returns>PromiseLike<String[]></returns>
    //===============================================
    async UpdateReceipt (id,value){
        return this._UpdateReceipt.ExecuteRequest({id:id,value:value}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/RPC/DeletePerson. Method: DELETE.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<String[]></returns>
    //===============================================
    async DeletePerson (id){
        return this._DeletePerson.ExecuteRequest({id:id}, globalTokens);
    }

}

export {RPCRequestContainer};
