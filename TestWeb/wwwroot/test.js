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
