import doSomething from "./app.js";

class Person {
  constructor(private firstName: string, 
    private lastName: string) {}

  get fullName() {
    return `${this.firstName} ${this.lastName}`;
  }
}

doSomething();

interface Vector {
  xCoord: number;
  yCoord: number;
}

let myName = "Shawn";
let age = 44;
let birthday = new Date();

let lastName: string;

lastName = "Wildermuth";
//lastName = age;

function showName(theName = "") {
  // if (age === 44) {
  //   return "true";
  // }
  // return false;
  console.log(theName);
}

async function send(theName: string) {
  console.log(theName);
  return true;
}

send("Hello")
  .then(s => {
    console.log(`Success: ${s}}`)
  });

const aPerson = new Person("Bob", "Smith");

function sendPerson(vector: Vector) {

}
//const x = new Person();

const aVector = {
  xCoord: 1,
  yCoord: 5
};

sendPerson(aVector);

function showMe() {
  if (aVector) return true;
  return "Apple";
}

// function getPerson(id: number) : Person{
//   // ... get the person
//   const person: Person = null;
//   if (person) return person;
//   return null;
// }

