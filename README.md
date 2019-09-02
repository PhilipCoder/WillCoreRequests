# WillCore.Requests

If you have a .NET core WebAPI project and if you are tired of typing out AJAX request after request, WillCore.Requests is the framework that will brighten your day.

> It is like adding a WCF service reference to your JavaScript. But for WebAPI.

---

## Features
* Supports either REST or RPC.
* Simple initialization.
* Uses reflection to generate JavaScript methods, parameters and results to mirror your WebAPI controllers. These methods can then be used to make requests to the service.
* Full Visual Studio intellisense support via docx comments.
* Outputs either:
  * ES5
    * A single file containing all the functions for each controller, action, paramter class and result class.
    * Uses a simple polyfill for promises and XMLXHR requests under the hood.
  * ES6
    * Classes that are exported as ES6 modules are generated.
    * Uses FetchAPI under the hood.
* With full IOC, the API can easily be extended or functionality can be changed. 

###### Intellisense: Methods
![alt text](TestWeb/images/MethodIntellisense.png "Logo Title Text 1")

###### Intellisense: Result Properties
![alt text](TestWeb/images/ResultIntellisens.png "Logo Title Text 1")

---

## Supported Platforms
WillCore.Requests is only tested with .Net Core 2.2.

## Getting Started

WillCore.Requests is available as a Nuget Package. Simply install the package by running the following command in the Package Manager Console:

```javascript
Install-Package WillCore.Requests
```

Then add the following using statements:

```csharp
using CodeBuilder;
using CodeBuilder.JS;
using ContractExtractor;
```

To enable the JS code generation, add the WillCore.Request service to the service collection:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    //Adds the WillCore Service
    services.AddWillCoreRequests<JavaScript>();
}
```

To generate the JavaScript code, run the following method on the IApplicationBuilder instance in the configure method :
```csharp
app.WillCoreBuildMyCode<ControllerBase>();
```

If your controller inherit from Controller use the following line:
```csharp
  app.WillCoreBuildMyCode<Controller>();
```

When you run your solution, the JavaScript files will be generated in the \js folder inside your solution.

## WebAPI And Camel Casing
Microsoft must have had a good reason to change all the result JSON properties from the WebAPI controllers to camel casing. WillCore.Requests 
support the naming convention by default. However you can override the naming convention and force the API to return paschal casing. 
When your service is configured to return results with paschal named results, you should tell the code builder to use paschal casing else your results will be empty.

To configure WillCore.Requests to use pascal casing, add the following method in the Configure method:

```csharp
//The optional parameter (false) tells the code builder not to use camel casing.
 app.ConfigureJavascript((config, mappings) => {
    config.APIConfiguredForCamelCase = false;
});
```

---

## Excluding Controllers From The Code Generation

Sometimes a controller has to be excluded from the code generation (like MVC controllers that return views, or APIs not available for public access).

To exclude a controller, simply add a ExcludeFromAPIContract attribute to the controller.

```csharp
[ExcludeFromAPIContract]
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
}
```
---
## Consuming The Service
### ES6 Mode (default mode)
WillCore.Requests builds ES6 modules for each controller. These modules can simply be imported and used. The file name the module is contained in, is the same name as the controller. The request container is name controllerName + "RequestContainer".

To import a request container for PersonController:
```javascript
import { PersonRequestContainer } from "./js/PersonRequestContainer.js";
```

The request container is a class, to create an instance of the class, call the constructor with the base URL of your service:
```javascript
 let personRequests = new PersonRequestContainer("http://localhost:53964");
```

You can now call the actions on your service by simply invoking methods on the request container instance:

#### Rest

```javascript
//Import the request container for controller : PersonController
import { PersonRequestContainer } from "./js/Person.js";

//In order to use await, we need an async function.
(async () => {
    //Creates an instance of the request container.
    let personRequests = new PersonRequestContainer("http://localhost:53964");
    //Calls a get method to get all the people
    var allPeople = await personRequests.Get();
    //Gets a single person
    var singlePerson = await personRequests.GetById(2);
    //Updates a record
    singlePerson.name = "My Name Is Bieber..whahaaha";
    await personRequests.PutById(8, singlePerson);
})();
```

#### RPC

```javascript
//Import the request container for controller : RPCController
import { RPCRequestContainer } from "./js/RPC.js";

//In order to use await, we need an async function.
(async () => {
    //Creates an instance of the request container.
    let personRequests = new RPCRequestContainer("http://localhost:53964");
    //Calls a get method to get all the instances
    var allPeople = await personRequests.GetAllPersons();
    //Gets a single person
    var singlePerson = await personRequests.GetPerson(6);
    //Updates a record
    singlePerson.name = "My Name Is Bieber..whahaaha";
    await personRequests.UpdateReceipt(8, singlePerson);
})();
```
---
### ES5 Mode
Some people are living in the past and are still using Internet Explorer. Unfortunately there are cases where we have to cater for those poor souls and ignore the awesomeness of ES6 and later versions of JavaScript. 

To enable ES5 mode, configure WillCore.Requests to generate ES5 code in the ConfigureJavascript method:

```csharp
app.ConfigureJavascript((config, mappings) =>
{
    config.ESMode = ESMode.ES5;
});
```

A file (requestContext.js) will be generated in the js folder of your solution. If the folder does not exist, it will be created. Simply import the file and start using it. 

> ES5 mode uses a polyfill for ES6 promises. The "then" function can be used as a callback when the request is done executing.


```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <h1>Test</h1>
    <!--include the generated JavaScript request context.-->
    <script src="js/requestContext.js"></script>
    <script>
        //Create a new instance of the request container
        var personRequests = new PersonRequestContainer("http://localhost:53964");
        //Use promises on the requests
        personRequests.Get().then(function (people) {
            console.log(people[0]);
            personRequests.PutById(8, people[0]).then(function (result) {
                console.log(result);
            });
            personRequests.Post(people[0]);
            personRequests.DeleteById(2);
        });
    </script>
</body>
</html>
```
---

## Submitting Complex Models
Models or complex types can be submitted via POST or PUT requests with full intellisense support. 

In order to submit a model, first import the model from the same file the request context is imported from:
*(The models are named the same as the C# models used as the action's parameters)*
```javascript
import { PersonRequestContainer, Person, Receipt } from "./js/Person.js";
```
An instance of the model has to be submitted:
```javascript
//Create a person with 2 receipts
var person = new Person(50, "John", "Foe", new Date(), null,
    [
        new Receipt(30, 60, "Some receipt"),
        new Receipt(31, 10, "A new receipt")
    ]);
//Creates the person
var postResult = await personRequests.Post(person);
//Updates the person
var putResult = await personRequests.PutById(50, person);
```

---

## Specifying HTTP Request Headers
HTTP request headers such as authentication tokens can be specified globally. Headers are set on a request container instance, however they apply globally for all future requests across all request containers.

The headers can be configured via the setHttpHeaders method on a request container:
```javascript
let personRequests = new RPCRequestContainer("http://localhost:53964");
personRequests.setHttpHeaders({ AuthToken: "The Token Value" })
```

---

## Configuring Code Generation

File generation configuration can be changed in the app.ConfigureJavascript method. To access the class in the Startup.cs:

```csharp
app.ConfigureJavascript((config, mappings) =>{
    config.<settingA> = ;
    config.<settingA> = ;
});
```

Settings that can be changed:

Property | Type | Default Value | Description
---- | ---- | ---- | ---- |
ESMode | Enum (ESMode) | ESMode.ES6 | Sets the version of JavaScript that will be generated.
ModelsFolder | String | "models" | The folder under the output directory that will contain the ES6 result modules.
SingleFileOutputName | String | "requestContext.js" | The file name of the generated ES5 JavaScript file containing all the request logic.
OutputDirectory | String | "js" | The folder where the generated JavaScript code will saved in.
RequestContextNameFactory | Func<string,string> | className => $"\{ className \}RequestContainer" | Factory action that returns the filenames for all generated RequestContexts.
ModelsNameFactory | Func<string,string> | className => $"\{ className \}" |  Factory action that returns the filenames for all generated models.
Comments | CommentConfiguration | Default instance of comment configuration |  Configuration for the generated comments.
Templates | Dictionary<string, string> | Default Builder Templates |   Dictionary containing the templates that will be used for code generation.
AdditionalFiles | Dictionary<string, string> |  {"request\\request.js", Resources.request } |  Additional files that will be copied to the output directory.
APIConfiguredForCamelCase | bool |  true |   Sets and indicates if the web service's JSON has been configured for camel or paschal casing.


---

#### Comment Configuration
The generated JSDoc comments can be changed via the config.Comments property:

Property | Type | Default Value | Description
---- | ---- | ---- | ---- |
ResultClassDescription | Func<string, string> | className => $"POCO class $\{className\}" | Sets the class or function comments generated for result objects.
RequestContainerDescription | Func<string, string> | className => $"Request Context." | Sets the class or function comments generated for request containers.
RequestContainerConstructor | Func<string, string, string> | (className, url) => $"\{className\}. Use instance to make requests to: \{url\}" | Sets the constructor comment for request containers.
RequestMethod | Func<string, string, string> | (httpMethod, url) => $"Method used to invoke request of type: \{httpMethod\} to URL: \{url\}." | Sets the method description comments used to fire requests.


---

#### Contract Extraction Configuration
You can change the way WillCore.Requests do recursion on your code base to find the classes and methods to build code from. This change be changed via the ConfigureWillCoreReflection method on the IApplicationBuilder instance in the Configure method:

```csharp
app.ConfigureWillCoreReflection(conf =>
{
    conf.AttributeExcludeFilterAttributes.Add(typeof(DisableRequestSizeLimitAttribute));
});
```

Fields that can be changed:

Property | Type | Description
---- | ---- | ---- |
ControllerType | Type |  The type of controller to extract
MaxRecursiveDepth | int |  Sets the maximum level of child result classes the recursion will allow
ClassIncludeFilterAttributes | List\<Type> |  Only classes with attributes in this array will be returned. Can be empty to return all classes.
AttributeIncludeFilterAttributes | List\<Type> |  Only attributes in this array will be returned.
MethodIncludeFilterAttributes | List\<Type> |  Only methods with attributes in this array will be returned. Can be empty to return all.
ParameterIncludeFilterAttributes | List\<Type> |  Only methods with attributes in this array will be returned. Can be empty to return all.
PropertyIncludeFilterAttributes | List\<Type> |  Only properties with attributes in this array will be returned. Can be empty to return all.
ClassExcludeFilterAttributes | List\<Type> |  Only classes with attributes not in this array will be returned. 
AttributeExcludeFilterAttributes | List\<Type> |  Only attributes in this array will be returned.
MethodExcludeFilterAttributes | List\<Type> |  Only methods with attributes not in this array will be returned
ParameterExcludeFilterAttributes | List\<Type> |  Only methods with attributes not in this array will be returned.
PropertyExcludeFilterAttributes | List\<Type> |  Only properties with attributes not in this array will be returned.



---

#### Swapping Code Generation Implementations

WillCore.Requests uses it's own IOC implentation under the hood. Implementations used for code generations can be swapped out via the dependency container. All code building functionality can be swapped out.

For more information on available modules, please see source code.

```csharp
 app.ConfigureJavascript((config, mappings) =>
{
    mappings.
    MapType<IInterfaceA, ImplentationA>().
    MapType<IInterfaceB, ImplentationB>();
});
```

### Licence
This project is licensed under the MIT License

### Author
Philip Schoeman