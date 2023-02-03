"use strict";

export async function calculateTotal(invoice) {
  const { taxRates } = await import("./taxRates.js");
  const rate = taxRates[invoice.state];
  const total = invoice.amount + (invoice.amount * rate);
  return {
    rate,
    total
  };
}
