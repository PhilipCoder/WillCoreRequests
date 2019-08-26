import {request, globalTokens} from "./request/request.js";
import {Person} from "./models/Person.js";
import {Receipt} from "./models/Receipt.js";

//===============================================
//<summary>PersonContext. Used to make requests to the Person controller.</summary>
//===============================================
class PersonRequestContainer {
    //===============================================
    //<summary>Creates Instance Of PersonContext.</summary>
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
        //<summary>Reqeust api/Person</summary>
        //===============================================
        this._Get = new request(this._baseUrl,"api/Person","GET",{},Person);
        //===============================================
        //<summary>Reqeust api/Person/{id}</summary>
        //===============================================
        this._GetById = new request(this._baseUrl,"api/Person/{id}","GET",{id:"URL"},Person);
        //===============================================
        //<summary>Reqeust api/Person/{personId}/{receiptId}</summary>
        //===============================================
        this._GetByPersonIdAndReceiptId = new request(this._baseUrl,"api/Person/{personId}/{receiptId}","GET",{personId:"URL",receiptId:"URL"},Receipt);
        //===============================================
        //<summary>Reqeust api/Person</summary>
        //===============================================
        this._Post = new request(this._baseUrl,"api/Person","POST",{value:"BODY"},null);
        //===============================================
        //<summary>Reqeust api/Person/{id}</summary>
        //===============================================
        this._PutById = new request(this._baseUrl,"api/Person/{id}","PUT",{id:"URL",value:"BODY"},null);
        //===============================================
        //<summary>Reqeust api/Person/{id}</summary>
        //===============================================
        this._DeleteById = new request(this._baseUrl,"api/Person/{id}","DELETE",{id:"URL"},null);
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
    //<summary>Method to invoke request to api/Person. Method: GET.</summary>
    //<returns>PromiseLike<Person[]></returns>
    //===============================================
    async Get (){
        return this._Get.ExecuteRequest({}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/Person/{id}. Method: GET.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<Person></returns>
    //===============================================
    async GetById (id){
        return this._GetById.ExecuteRequest({id:id}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/Person/{personId}/{receiptId}. Method: GET.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<Receipt></returns>
    //===============================================
    async GetByPersonIdAndReceiptId (personId,receiptId){
        return this._GetByPersonIdAndReceiptId.ExecuteRequest({personId:personId,receiptId:receiptId}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/Person. Method: POST.</summary>
    //<param>CodeBulder.JS.Types.JSClassType</param>
    //<typeparam>Person</typeparam>
    //<returns>PromiseLike<String></returns>
    //===============================================
    async Post (value){
        return this._Post.ExecuteRequest({value:value}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/Person/{id}. Method: PUT.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<param>CodeBulder.JS.Types.JSClassType</param>
    //<typeparam>Person</typeparam>
    //<returns>PromiseLike<String></returns>
    //===============================================
    async PutById (id,value){
        return this._PutById.ExecuteRequest({id:id,value:value}, globalTokens);
    }

    //===============================================
    //<summary>Method to invoke request to api/Person/{id}. Method: DELETE.</summary>
    //<param>CodeBulder.JS.Types.JSNumber</param>
    //<typeparam>Number</typeparam>
    //<returns>PromiseLike<String></returns>
    //===============================================
    async DeleteById (id){
        return this._DeleteById.ExecuteRequest({id:id}, globalTokens);
    }

}

export {PersonRequestContainer};
