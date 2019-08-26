<< class[headerComment] >>
<< class[import] >>

<< class[comment] >>
class << class[name] >><< class[extends] >> {
    << class[constructorComment] >>
    constructor(<< class[constructorParameters] >>) {
<< class[property] >>
    }
    << class[httpHeaderFunctionComment] >>
    setHttpHeaders(headerObject){
        for (var key in headerObject){
            globalTokens[key] = headerObject[key];
        }
    }
<< class[method] >>
}

<< class[exports] >>