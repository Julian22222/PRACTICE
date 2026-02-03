export interface IAddress {
  street: string;
  suite: string;
  city: string;
  zipcode: string;
}

export interface IWaitingUser {
  id: number;
  name: string;
  username: string;
  email: string;
  address: IAddress;
  phone: string;
}

////////////////////////////////////////

export interface ICurrentUser {
  car_id?: number;
  brand: string;
  seats: number;
  date: string;
  fuel: string;
  created_at?: string;
  serviceCheck: boolean;
  involved: string | string[];
  notes: string;
  phone: string;
}

///////////////////////////

export interface IMessage {
  sender: string;
  content: string;
  timestamp: string;
}
