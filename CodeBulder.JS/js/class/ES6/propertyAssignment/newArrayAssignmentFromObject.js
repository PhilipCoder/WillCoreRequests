<< PropertyComment >>
this.<< propertyName >> = typeof dataObject !== "undefined" &&  typeof dataObject.<< propertyName >> !== "undefined" ? dataObject.<< propertyName >>.map(row => ( row => { let newObj = new << type >>(); newObj._loadFromObject(row); return newObj; })(row)) : [];
