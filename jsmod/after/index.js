"use strict";
import { calculateTotal } from "./calculateTotal.js";
import invoices from "./invoices.js";
import lodash from "lodash"; // NPM Package
const { round } = lodash;

invoices.forEach(async i => {
  const { rate, total } = await calculateTotal(i);
  console.log(`Invoice: ${i.invoiceNumber}, Date: ${i.invoiceDate}
  Gross:    $${round(i.amount,2)}
  Tax Rate: ${rate * 100}% 
  Net:      $${round(total,2)}`);
});

