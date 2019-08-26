
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
}
/**
* Sets the request headers for all requests.
* @param {Object} headerObject
*/
PersonRequestContainer.protype.setHttpHeaders = function(headerObject){
        for (var key in headerObject){
            globalTokens[key] = headerObject[key];
    }
}
    /**
    * Method to invoke request to api/Person. Method: GET.
    * @return {PromiseLike<Person[]>}
    */
    async Get (){
        return this._Get.ExecuteRequest({}, globalTokens);
    }

    /**
    * Method to invoke request to api/Person/{id}. Method: GET.
    * @param {Number} id
    * @return {PromiseLike<Person>}
    */
    async GetById (id){
        return this._GetById.ExecuteRequest({id:id}, globalTokens);
    }

    /**
    * Method to invoke request to api/Person/{personId}/{receiptId}. Method: GET.
    * @param {Number} personId
    * @param {Number} receiptId
    * @return {PromiseLike<Receipt>}
    */
    async GetByPersonIdAndReceiptId (personId,receiptId){
        return this._GetByPersonIdAndReceiptId.ExecuteRequest({personId:personId,receiptId:receiptId}, globalTokens);
    }

    /**
    * Method to invoke request to api/Person. Method: POST.
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    async Post (value){
        return this._Post.ExecuteRequest({value:value}, globalTokens);
    }

    /**
    * Method to invoke request to api/Person/{id}. Method: PUT.
    * @param {Number} id
    * @param {Person} value
    * @return {PromiseLike<String>}
    */
    async PutById (id,value){
        return this._PutById.ExecuteRequest({id:id,value:value}, globalTokens);
    }

    /**
    * Method to invoke request to api/Person/{id}. Method: DELETE.
    * @param {Number} id
    * @return {PromiseLike<String>}
    */
    async DeleteById (id){
        return this._DeleteById.ExecuteRequest({id:id}, globalTokens);
    }

