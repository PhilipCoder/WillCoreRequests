var globalTokens = {};
/**
 * A very simple and lightweight cross browser polyfill for a JS promise.
 * Author : Philip Schoeman
 * @param {Function} func
 */
var corePromise = function (func) {
    var promiseScope = this;
    this.funcToRun = null;
    this.funcCatch = null;
    this.then = function (funcThen, funcCatch) {
        promiseScope.funcToRun = funcThen;
        if (funcCatch) {
            promiseScope.funcCatch = funcCatch;
        }
    };
    this.catch = function (funcCatch) { promiseScope.funcCatch = funcCatch; };
    this.resolve = function (data) {
        if (promiseScope.funcToRun) {
            promiseScope.funcToRun(data);
        }
    };
    this.reject = function (data) {
        if (promiseScope.funcCatch) {
            promiseScope.funcCatch(data);
        }
    };

    func(promiseScope.resolve, promiseScope.reject);
};

/**
 * A simple and cross platform XMLXHR wrapper
 * @param {String} baseURL
 * @param {String} url
 * @param {String} method
 * @param {Object} requestBindings
 * @param {Object} resultType
 */
var request = function (baseURL, url, method, requestBindings, resultType) {
    if (!baseURL.endsWith("/")) {
        baseURL = baseURL + "/";
    }
    this.BaseURL = baseURL;
    this.URL = this.BaseURL + url;
    this.Method = method;
    this.RequestBindings = requestBindings;
    this.ResultType = resultType;
};

request.prototype.ExecuteRequest = function (parms) {
    var requestBody = {};
    var requestParameters = {};
    var requestURL = this.URL;

    var hasQueryParamters = false;
    var requestBody = null;
    for (var key in this.RequestBindings) {
        var binding = this.RequestBindings[key];
        if (typeof (parms[key]) === "undefined") {
            throw `Parameter ${key} is not supplied!`;
        }
        if (binding === "URL") {
            requestURL = requestURL.replace(`{${key}}`, parms[key]);
        } else if (binding === "QUERY") {
            requestParameters[key] = parms[key];
        } else if (binding === "BODY") {
            requestBody = JSON.stringify(parms[key]);
        } else {
            throw `Unsupport parameter type ${binding}.`;
        }
    }
    if (Object.keys(requestParameters).length > 0) {
        requestURL = requestURL + "?" + Object.keys(obj).map(function (key) { return key + "=" + encodeURIComponent(obj[key]) }).join("&")
    }
    var xhrRequest = new XMLHttpRequest();
    xhrRequest.open(this.Method, requestURL, true);
    xhrRequest.setRequestHeader("Content-Type", "application/json");
    for (var key in globalTokens) {
        xhrRequest.setRequestHeader(key, globalTokens[key]);
    }
    return new corePromise(function (resolve, reject) {
        xhrRequest.onreadystatechange = function () {
            if (xhrRequest.readyState === 4) {
                if (xhrRequest.status == 200) {
                    try {
                        resolve(JSON.parse(xhrRequest.responseText));
                    } catch (exeption) {
                        reject(exeption);
                    }
                } else {
                    reject({ errorCode: xhrRequest.status, error: xhrRequest.error });
                }
            }
        };
        xhrRequest.send(requestBody);
    });
};


/**
* Creates Instance Of The Result Class.
* @param {Object} data
*/
function Product (data){
        /**
        * @type {String}
        */
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        /**
        * @type {String}
        */
        this.description = typeof(data.description) !== "undefined" ? data.description : null;
}




/**
* Creates Instance Of The Result Class.
* @param {Object} data
*/
function Receipt (data){
        /**
        * @type {String}
        */
        this.id = typeof(data.id) !== "undefined" ? data.id : null;
        /**
        * @type {Number}
        */
        this.byteValue = typeof(data.byteValue) !== "undefined" ? data.byteValue : null;
        /**
        * @type {Number}
        */
        this.sByteValue = typeof(data.sByteValue) !== "undefined" ? data.sByteValue : null;
        /**
        * @type {String}
        */
        this.charValue = typeof(data.charValue) !== "undefined" ? data.charValue : null;
        /**
        * @type {Number}
        */
        this.decimalValue = typeof(data.decimalValue) !== "undefined" ? data.decimalValue : null;
        /**
        * @type {Number}
        */
        this.doubleValue = typeof(data.doubleValue) !== "undefined" ? data.doubleValue : null;
        /**
        * @type {Number}
        */
        this.floatValue = typeof(data.floatValue) !== "undefined" ? data.floatValue : null;
        /**
        * @type {Number}
        */
        this.intValue = typeof(data.intValue) !== "undefined" ? data.intValue : null;
        /**
        * @type {Number}
        */
        this.uintValue = typeof(data.uintValue) !== "undefined" ? data.uintValue : null;
        /**
        * @type {Number}
        */
        this.longValue = typeof(data.longValue) !== "undefined" ? data.longValue : null;
        /**
        * @type {String}
        */
        this.stringValue = typeof(data.stringValue) !== "undefined" ? data.stringValue : null;
        /**
        * @type {Product[]}
        */
        this.products = typeof(data.products) !== "undefined" ? data.products.map(function(dataRow){ return new products(dataRow);}) : null;
}




/**
* Creates Instance Of The Result Class.
* @param {Object} data
*/
function Person (data){
        /**
        * @type {String}
        */
        this.id = typeof(data.id) !== "undefined" ? data.id : null;
        /**
        * @type {String}
        */
        this.name = typeof(data.name) !== "undefined" ? data.name : null;
        /**
        * @type {String}
        */
        this.surname = typeof(data.surname) !== "undefined" ? data.surname : null;
        /**
        * @type {Date}
        */
        this.dateOfBirth = typeof(data.dateOfBirth) !== "undefined" ? data.dateOfBirth : null;
        /**
        * @type {Number[]}
        */
        this.profileImage = typeof(data.profileImage) !== "undefined" ? data.profileImage : null;
        /**
        * @type {Receipt[]}
        */
        this.receipts = typeof(data.receipts) !== "undefined" ? data.receipts.map(function(dataRow){ return new receipts(dataRow);}) : null;
}




/**
* Creates Instance Of PersonContext.
* @param {String} baseURL
*/
function PersonRequestContainer (baseUrl){
        /**
        * The base URL used for all requests on the class.
        * @type {String}
        */
        this._baseUrl = baseUrl;
        /**
        * Reqeust api/Person
        */
        this._Get = new request(this._baseUrl,"api/Person","GET",{},Person);
        /**
        * Reqeust api/Person/{id}
        */
        this._GetById = new request(this._baseUrl,"api/Person/{id}","GET",{id:"URL"},Person);
        /**
        * Reqeust api/Person/{personId}/{receiptId}
        */
        this._GetByPersonIdAndReceiptId = new request(this._baseUrl,"api/Person/{personId}/{receiptId}","GET",{personId:"URL",receiptId:"URL"},Receipt);
        /**
        * Reqeust api/Person
        */
        this._Post = new request(this._baseUrl,"api/Person","POST",{value:"BODY"},null);
        /**
        * Reqeust api/Person/{id}
        */
        this._PutById = new request(this._baseUrl,"api/Person/{id}","PUT",{id:"URL",value:"BODY"},null);
        /**
        * Reqeust api/Person/{id}
        */
        this._DeleteById = new request(this._baseUrl,"api/Person/{id}","DELETE",{id:"URL"},null);
    /**
    * Sets the request headers for all requests.
    * @param {Object} headerObject
    */
    this.setHttpHeaders = function(headerObject) {
        for (var key in headerObject) {
            globalTokens[key] = headerObject[key];
        }
    }
    /**
    * Method to invoke request to api/Person. Method: GET.
    * @return {PromiseLike<Person[]>}
    */
    this.Get = function(){
        return this._Get.ExecuteRequest({}, globalTokens);
    };

    /**
    * Method to invoke request to api/Person/{id}. Method: GET.
    * @param {Number} id
    * @return {PromiseLike<Person>}
    */
    this.GetById = function(id){
        return this._GetById.ExecuteRequest({id:id}, globalTokens);
    };

    /**
    * Method to invoke request to api/Person/{personId}/{receiptId}. Method: GET.
    * @param {Number} personId
    * @param {Number} receiptId
    * @return {PromiseLike<Receipt>}
    */
    this.GetByPersonIdAndReceiptId = function(personId,receiptId){
        return this._GetByPersonIdAndReceiptId.ExecuteRequest({personId:personId,receiptId:receiptId}, globalTokens);
    };

    /**
    * Method to invoke request to api/Person. Method: POST.
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    this.Post = function(value){
        return this._Post.ExecuteRequest({value:value}, globalTokens);
    };

    /**
    * Method to invoke request to api/Person/{id}. Method: PUT.
    * @param {Number} id
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    this.PutById = function(id,value){
        return this._PutById.ExecuteRequest({id:id,value:value}, globalTokens);
    };

    /**
    * Method to invoke request to api/Person/{id}. Method: DELETE.
    * @param {Number} id
    * @return {PromiseLike<String>}
    */
    this.DeleteById = function(id){
        return this._DeleteById.ExecuteRequest({id:id}, globalTokens);
    };

}




/**
* Creates Instance Of RPCContext.
* @param {String} baseURL
*/
function RPCRequestContainer (baseUrl){
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
    /**
    * Sets the request headers for all requests.
    * @param {Object} headerObject
    */
    this.setHttpHeaders = function(headerObject) {
        for (var key in headerObject) {
            globalTokens[key] = headerObject[key];
        }
    }
    /**
    * Method to invoke request to api/RPC/GetAllPersons. Method: GET.
    * @return {PromiseLike<Person[]>}
    */
    this.GetAllPersons = function(){
        return this._GetAllPersons.ExecuteRequest({}, globalTokens);
    };

    /**
    * Method to invoke request to api/RPC/GetPerson. Method: GET.
    * @param {Number} id
    * @return {PromiseLike<Person>}
    */
    this.GetPerson = function(id){
        return this._GetPerson.ExecuteRequest({id:id}, globalTokens);
    };

    /**
    * Method to invoke request to api/RPC/GetReceipt. Method: GET.
    * @param {Number} personid
    * @param {Number} receiptid
    * @return {PromiseLike<Receipt>}
    */
    this.GetReceipt = function(personid,receiptid){
        return this._GetReceipt.ExecuteRequest({personid:personid,receiptid:receiptid}, globalTokens);
    };

    /**
    * Method to invoke request to api/RPC/AddReceipt. Method: POST.
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    this.AddReceipt = function(value){
        return this._AddReceipt.ExecuteRequest({value:value}, globalTokens);
    };

    /**
    * Method to invoke request to api/RPC/UpdateReceipt. Method: PUT.
    * @param {Number} id
    * @param {Person} value
    * @return {PromiseLike<String[]>}
    */
    this.UpdateReceipt = function(id,value){
        return this._UpdateReceipt.ExecuteRequest({id:id,value:value}, globalTokens);
    };

    /**
    * Method to invoke request to api/RPC/DeletePerson. Method: DELETE.
    * @param {Number} id
    * @return {PromiseLike<String[]>}
    */
    this.DeletePerson = function(id){
        return this._DeletePerson.ExecuteRequest({id:id}, globalTokens);
    };

}



