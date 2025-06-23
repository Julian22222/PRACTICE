export interface IAddress {
  street: string;
  suite: string;
  city: string;
  zipcode: string;
}

export interface IWaitingList {
  id: number;
  name: string;
  username: string;
  email: string;
  address: IAddress;
  phone: string;
}

////////////////////////////////////////

export interface ICurrentUsers {
  car_id: number;
  brand: string;
  seats: number;
  date: string;
  fuel: string;
}
