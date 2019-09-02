<< ClassConstructorComment >>
function << name >> (<< constructorParameters >>)
{
<< DirectAssignmentProperty >>
<< RunRequestProperty >>
    this.setHttpHeaders = function (headerObject)
    {
        for (var key in headerObject)
        {
              globalTokens[key] = headerObject[key];
        }
    }
}
<< RunMethodRequest >>

