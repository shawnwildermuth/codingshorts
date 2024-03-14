export interface Customer {
  id: number;
  companyName: string | null;
  contact: string | null;
  phoneNumber: string | null;
  email: string | null;
  addressLine1: string | null;
  addressLine2: string | null;
  addressLine3: string | null;
  city: string | null;
  stateProvince: string | null;
  postalCode: string | null;
  country: string | null;
}

export interface Project {
  id: number;
  projectName: string | null;
  startDate: string | null;
  endDate: string | null;
}