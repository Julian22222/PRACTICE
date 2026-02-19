import { User as NextAuthUser } from "next-auth";

interface User extends NextAuthUser {
  email: string;
  password: string;
}

export const users: User[] = [
  {
    id: "1",
    name: "Julian",
    email: "julian@test.com",
    password: "123",
  },
  {
    id: "2",
    name: "Julian2",
    email: "julian2@test.com",
    password: "456",
  },
];
