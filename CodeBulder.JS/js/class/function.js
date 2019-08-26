<< function[headerComment] >>

<< function[constructorComment] >>
function << function[name] >> (<< function[constructorParameters] >>){
<< function[property] >>
    << function[httpHeaderFunctionComment] >>
    this.setHttpHeaders = function(headerObject) {
        for (var key in headerObject) {
            globalTokens[key] = headerObject[key];
        }
    }
<< function[method] >>
}


