import {request,globalTokens} from "./request/request.js";
<< RequestContextImport >>

class << name >> 
{
    << ClassConstructorComment >>
    constructor(<< constructorParameters >>) 
    {
<< DirectAssignmentProperty >>
<< RunRequestProperty >>
    }
    setHttpHeaders(headerObject)
    {
       for (var key in headerObject){
            globalTokens[key] = headerObject[key];
        }
    }
<< RunMethodRequest >>
}

<< ExportRequestContext >>