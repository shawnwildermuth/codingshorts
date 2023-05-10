import out from "./out.js";

const handler = {
  get(target, key, receiver) {
    out(`Reading with key: '${key}'`);
    return Reflect.get(target, key, receiver);
  }
};

console.clear();
out("Starting!");

const cust = {
  firstName: "Steve",
  lastName: "Johnson",
  companyName: "Acme Supplies",
  phone: "+1 404 555 1212",
  format() {
    return `${this.lastName}, ${this.firstName}`;
  }
};

var proxy = new Proxy(cust, handler);

out(proxy.firstName);
out(proxy.format());
out(proxy["lastName"]);

const custs = [
  { firstName: "Steve" },
  { firstName: "Shane" }
];

const arrayProxy = new Proxy(custs, handler);

out(arrayProxy[0]);
out(arrayProxy[1]);
out(arrayProxy["pop"]());
out(arrayProxy.length);

class Order {
  orderNumber;
  orderDate = new Date();
  terms;

  constructor(number, terms) {
    this.orderNumber = number;
    this.terms = terms;
  }

  send() {
    console.log("Sent...");
  }
}

const myOrder = new Order(1, "Net 15");

const orderProxy = new Proxy(myOrder, handler);

out(orderProxy);
orderProxy.send();

