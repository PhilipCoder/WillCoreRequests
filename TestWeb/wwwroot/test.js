//Import the request container for controller : RPCController
import { PersonRequestContainer, Person, Receipt } from "./js/Person.js";

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
    console.log(putResult);
})();
