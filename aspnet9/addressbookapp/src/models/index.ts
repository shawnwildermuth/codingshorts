export interface AddressModel {
  id: number;
  name: string | null;
  line1: string | null;
  line2: string | null;
  line3: string | null;
  cityTown: string | null;
  stateProvince: string | null;
  postalCode: string | null;
  country: string | null;
}

export interface EntryModel {
  id: number;
  companyName: string | null;
  firstName: string | null;
  middleName: string | null;
  lastName: string | null;
  dateOfBirth: string;
  gender: string | null;
  homePhone: string | null;
  workPhone: string | null;
  cellPhone: string | null;
  email: string | null;
  addresses: AddressModel[];
}

export interface EntryLookupModel
{
  id: number;
  displayName: string;
}

export interface BookModel {
  bookEntries: EntryModel[];
}