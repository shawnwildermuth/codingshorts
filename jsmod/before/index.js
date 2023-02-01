"use strict";

const taxRates = {
  Georgia: 0.085,
  Alabama: 0.075,
  Florida: 0.025,
};

const invoices = [
  {
    id: 1,
    amount: 410.82,
    invoiceDate: "03/04/2022",
    invoiceNumber: 1111,
    customer: "Thoughtstorm",
    customerEmail: "ebattista0@domainmarket.com",
    state: "Georgia",
  },
  {
    id: 2,
    amount: 631.39,
    invoiceDate: "05/11/2022",
    invoiceNumber: 1410,
    customer: "Ozu",
    customerEmail: "bcoultard1@cbslocal.com",
    state: "Florida",
  },
  {
    id: 3,
    amount: 277.05,
    invoiceDate: "04/21/2022",
    invoiceNumber: 1782,
    customer: "Skibox",
    customerEmail: "cagiolfinger2@earthlink.net",
    state: "Florida",
  },
  {
    id: 4,
    amount: 810.17,
    invoiceDate: "06/30/2022",
    invoiceNumber: 1955,
    customer: "Eire",
    customerEmail: "fattock3@springer.com",
    state: "Georgia",
  },
  {
    id: 5,
    amount: 148.57,
    invoiceDate: "11/15/2022",
    invoiceNumber: 1803,
    customer: "Skinte",
    customerEmail: "msimenet4@irs.gov",
    state: "Florida",
  },
];

function calculateTotal(invoice) {
  const rate = taxRates[invoice.state];
  const total = invoice.amount + (invoice.amount * rate);
  return {
    rate,
    total
  }
}

invoices.forEach(i => {
  const { rate, total } = calculateTotal(i);
  console.log(`Invoice: ${i.invoiceNumber}, Date: ${i.invoiceDate}
  Gross:    $${i.amount}
  Tax Rate: ${rate * 100}% 
  Net:      $${total}`);
});

