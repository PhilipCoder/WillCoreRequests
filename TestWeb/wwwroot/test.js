//Import the request container for controller : RPCController
import { PersonRequestContainer, Person } from "./js/Person.js";

//In order to use await, we need an async function.
(async () => {
    //Creates an instance of the request container.
    let personRequests = new PersonRequestContainer("http://localhost:53964");
    personRequests.setHttpHeaders({ AuthToken: "TheTokenValue" })
    //Calls a get method to get all the instances
    var allPeople = await personRequests.Get();
    console.log(allPeople);
    //Gets a single person
    var singlePerson = await personRequests.GetById(6);
    console.log(singlePerson);
    //Updates a record
    singlePerson.name = "My Name Is Bieber..whahaaha";
    var person = new Person(singlePerson.id, "jannemane", "surname", new Date(), null, null);
    var putResult = await personRequests.PutById(82, person);
    console.log(putResult);
})();
