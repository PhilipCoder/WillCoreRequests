var globalTokens = {};

class request {
    constructor(baseURL, url, method, requestBindings, resultType) {
        if (!baseURL.endsWith("/")) {
            baseURL = baseURL + "/";
        }
        this.BaseURL = baseURL;
        this.URL = this.BaseURL + url;
        this.Method = method;
        this.RequestBindings = requestBindings;
        this.ResultType = resultType;
    }

    async ExecuteRequest(parms) {
        var that = this;
        var requestBody = {};
        var requestParameters = {};
        var headers = {};
        var requestURL = this.URL;

        headers['Content-Type'] = 'application/json';

        for (var key in globalTokens) {
            headers[key] = globalTokens[key];
        }

        var requestObject = {
            method: this.Method,
            mode: 'cors',
            insecure: true,
            rejectUnauthorized: false,
            headers: new Headers(headers)
        };
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
                requestObject.body = JSON.stringify(parms[key]);
            } else {
                throw `Unsupport parameter type ${binding}.`;
            }
        }

        var url = new URL(requestURL);
        var search = new URLSearchParams(requestParameters)
        url.search = search;
        return new Promise(async (resolve, reject) => {
            var promiseCall = fetch(url, requestObject).then(async response => {
                response = await response.json();
                if (that.ResultType) {
                    response._singleParameter = true;
                    if (Array.isArray(response)) {
                        response = response.map(x => (() => { x._singleParameter = true; return new that.ResultType(x);})());
                    } else {
                        response = new that.ResultType(response);
                    }
                }
                resolve(response);
            }).catch(error => {
                reject(error);
            });
        });
    }
};
export { request, globalTokens };