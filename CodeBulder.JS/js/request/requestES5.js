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
