import doSomething from "./app.js";
class Person {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }
}
doSomething();
let myName = "Shawn";
let age = 44;
let birthday = new Date();
let lastName;
lastName = "Wildermuth";
//lastName = age;
function showName(theName = "") {
    // if (age === 44) {
    //   return "true";
    // }
    // return false;
    console.log(theName);
}
async function send(theName) {
    console.log(theName);
    return true;
}
send("Hello")
    .then(s => {
    console.log(`Success: ${s}}`);
});
const aPerson = new Person("Bob", "Smith");
function sendPerson(vector) {
}
//const x = new Person();
const aVector = {
    xCoord: 1,
    yCoord: 5
};
sendPerson(aVector);
function showMe() {
    if (aVector)
        return true;
    return "Apple";
}
// function getPerson(id: number) : Person{
//   // ... get the person
//   const person: Person = null;
//   if (person) return person;
//   return null;
// }
