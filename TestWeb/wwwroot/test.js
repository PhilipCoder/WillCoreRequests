import { PersonRequestContainer } from "./js/Person.js";

//(async () => {
//    var personRequests = new PersonRequestContainer("http://localhost:53964");
//    var person = await personRequests.Get();
//    await personRequests.PutById(8,person[0]);
//})();

var personRequests = new PersonRequestContainer("http://localhost:53964");
personRequests.Get().then(function (person) {
    console.log(person);
    personRequests.PutById(8, person[0]).then(function (result) {
        console.log(result);
    });
    personRequests.Post(person[0]);
    personRequests.DeleteById(2);
}, function (error) {
    console.log(error);
});
