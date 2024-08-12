import type { EntryModel } from "@/models";

export function formatName(entry?: EntryModel) {
  if (entry?.firstName && entry.lastName && entry.middleName) {
    return `${entry.lastName}, ${entry.firstName} ${entry.middleName}`;
  } else if (entry?.firstName && entry.lastName) {
    return `${entry.lastName}, ${entry.firstName}`;
  } else if (entry?.firstName) {
    return entry.firstName;
  } else if (entry?.lastName) {
    return entry.lastName;
  } else if (entry?.companyName) {
    return entry.companyName;
  } else {
    return "Undefined Name";
  }
}