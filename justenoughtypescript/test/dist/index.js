import normalize from "./normalize.js";
class Person {
    constructor(firstName = "", lastName = "") {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }
}
console.log("Hello World");
let myName = "Shawn";
myName = "1234";
const fixed = normalize(myName);
async function showName(name) {
    if (name) {
        console.log(name);
        return true;
    }
    return false;
}
const me = new Person("Shawn", "Wildermuth");
showName(me)
    .then(() => console.log("Success"));
showName({
    firstName: "Bob",
    lastName: "Smith"
})
    .then(() => console.log("Success"));
