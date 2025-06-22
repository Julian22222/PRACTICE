import React, { useEffect, useState } from "react";
import "./Styles/App.css";
import "./Styles/CurrentUsers.css";
import Card, { CardVariant } from "./Components/Card";
import { ICurrentUsers, IWaitingList } from "./Types/types";
import WaitingList from "./Components/WaitingList";
import Currentusers from "./Components/CurrentUsers";
import Header from "./Components/Header";
import axios from "axios";
import { Route, BrowserRouter, Routes } from "react-router-dom";
import CurrentUsers from "./Components/CurrentUsers";
import WaitingUser from "./Components/WaitingUser";
import CurrentUser from "./Components/CurrentUser";

function App() {
  const [users, setUsers] = useState<IWaitingList[]>([]); //control what type we receive from the API - <IWaitingList> - array of IWaitingList

  const [currentCustomers, setCurrentCustomers] = useState<ICurrentUsers[]>([]); //define type for this state - <ICurrentUsers[]> - array of ICurrentUsers

  useEffect(() => {
    fetchUsers();

    fetchDeliveryList();
  }, []);

  const fetchUsers = async () => {
    try {
      // const response = await fetch(
      //   "https://jsonplaceholder.typicode.com/users"
      // );
      // if (!response.ok) {
      //   throw new Error("Network response was not ok");
      // }
      // const receivedUsers = await response.json();
      const response = await axios.get<IWaitingList[]>( //controll what type we receive from the API
        "https://jsonplaceholder.typicode.com/users"
      );

      setUsers(response.data); // Set the fetched users to state

      console.log(users);
    } catch (error) {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );

      alert(error);
    }
  };

  const fetchDeliveryList = async () => {
    try {
      const response = await fetch("https://car-shop-back-end.onrender.com/");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const deliveryData: ICurrentUsers[] = await response.json();
      console.log(deliveryData);
      setCurrentCustomers(deliveryData); // Set the fetched delivery data to state
    } catch (error) {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );
    }
  };

  return (
    <div className="App">
      <BrowserRouter>
        <Header />
        <Routes>
          <Route
            path="/"
            element={<Currentusers customers={currentCustomers} />}
          />
          <Route path="/current_user/:id" element={<CurrentUser />} />
          <Route path="/waiting-list" element={<WaitingList users={users} />} />
          <Route path="/waiting-list/:id" element={<WaitingUser />} />
        </Routes>
      </BrowserRouter>

      <Card
        variant={CardVariant.outlined}
        width="200px"
        height="200px"
        myFunc={(num: number) => console.log("Clicked", num)}
      >
        <button>Button</button>
        <p>Hello World!!</p>
      </Card>
      {/* <WaitingList users={users} /> */}

      {/* <Currentusers customers={currentCustomers} /> */}
    </div>
  );
}

export default App;
