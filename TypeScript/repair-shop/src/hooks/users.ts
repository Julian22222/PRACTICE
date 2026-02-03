import { useEffect, useState } from "react";
import { ICurrentUser, IWaitingUser } from "../Types/types";
import axios from "axios";

// All Logic from Home page - from App.tsx
export function useUsers() {
  const [users, setUsers] = useState<IWaitingUser[]>([]); //control what type we receive from the API - <IWaitingList> - array of IWaitingList

  const [currentCustomers, setCurrentCustomers] = useState<ICurrentUser[]>([]); //define type for this state - <ICurrentUsers[]> - array of ICurrentUsers
  const [error, setError] = useState(false); // state to handle errors

  useEffect(() => {
    fetchUsers();

    fetchCurrentList();
  }, [currentCustomers]);

  const fetchUsers = async () => {
    try {
      setError(false); // Reset error state before fetching

      // const response = await fetch(
      //   "https://jsonplaceholder.typicode.com/users"
      // );
      // if (!response.ok) {
      //   throw new Error("Network response was not ok");
      // }
      // const receivedUsers = await response.json();
      const response = await axios.get<IWaitingUser[]>( //controll what type we receive from the API, we will get an array of IWaitingList
        "https://jsonplaceholder.typicode.com/users"
      );

      setUsers(response.data); // Set the fetched users to state

      // console.log(users);
    } catch (error) {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );
      setError(true); // Set error state to true if there's an error
      //const  err = error as AxiosError;;
      // setError(error.message);

      // alert(error);
    }
  };

  const fetchCurrentList = async () => {
    try {
      const response = await fetch("https://car-shop-back-end.onrender.com/");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const deliveryData: ICurrentUser[] = await response.json();
      // console.log(deliveryData);

      setCurrentCustomers(deliveryData); // Set the fetched delivery data to state
    } catch (error) {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );
    }
  };

  return { users, currentCustomers, error }; //return all the data and functions we need from this hook
}
