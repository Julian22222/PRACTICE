import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ICurrentUser } from "../Types/types";
import Modal from "./Modal";

const IndividualCurrentUser: FC = () => {
  const [currentUser, setCurrentUser] = useState<ICurrentUser>();

  console.log(Boolean([]));

  const { car_id } = useParams<{ car_id: string }>();

  const [modal, setModal] = useState<boolean>(false);

  useEffect(() => {
    if (car_id) {
      getUser(car_id);
    }
  }, [car_id]);

  useEffect(() => {
    console.log("currentUser updated:", currentUser);
  }, [currentUser]);

  const handleDelete = (e: React.FormEvent) => {
    e.preventDefault(); // prevent default form submission behavior

    try {
      if (currentUser) {
        axios
          .delete(
            `https://car-shop-back-end.onrender.com/${currentUser.car_id}`
          )
          .then((response) => {
            console.log("Delete successful:", response.data);
            setCurrentUser(undefined); // Clear current user after deletion
          })
          .catch((error) => {
            console.error("Error deleting user:", error);
            // Handle error appropriately, e.g., show a notification or alert
          });
      }
    } catch (error) {
      console.error("Error deleting user:", error);
    }
  };

  const getUser = async (car_id: string) => {
    try {
      // const response = await fetch(
      //   `https://car-shop-back-end.onrender.com/${car_id}`
      // );
      // if (!response.ok) {
      //   throw new Error("Network response was not ok");
      // }
      // const data: ICurrentUsers = await response.json();
      // console.log("data", data);
      // setCurrentUser(data);

      const response = await axios.get(
        `https://car-shop-back-end.onrender.com/${car_id}`
      );

      let res: ICurrentUser = response.data;

      const formattedDate = `${(res.created_at ?? "")
        .slice(0, 10)
        .split("-")
        .reverse()
        .join("/")} at ${(res.created_at ?? "").slice(11, 16)}`; // Format date from YYYY-MM-DD to DD/MM/YYYY at HH:MM

      setCurrentUser({ ...res, created_at: formattedDate }); // Format date from YYYY-MM-DD to DD/MM/YYYY
      console.log("currentUser", currentUser);
      console.log("response.data", response.data);
    } catch (error) {
      console.error("Error fetching user:", error);
      // Handle error appropriately, e.g., show a notification or alert
    } finally {
      console.log("Fetch attempt completed");
    }
  };

  return (
    <div className="eachCard-currentUser ">
      {currentUser ? (
        <>
          <p>Car Id: {currentUser.car_id}</p>
          <p>Car Brand: {currentUser.brand}</p>
          <p>Seats: {currentUser.seats}</p>
          <p>
            Car release:{" "}
            {currentUser.date.slice(0, 10).split("-").reverse().join("/")}
            {/* //Format date from YYYY-MM-DD to DD/MM/YYYY at HH:MM */}
          </p>
          <p>Fuel: {currentUser.fuel}</p>
          <p>Tel: {currentUser.phone}</p>
          <p>Created on: {currentUser.created_at}</p>
          <p>Serviced: {currentUser.serviceCheck ? "Yes" : "No"}</p>
          <input checked={currentUser.serviceCheck} type="checkbox" />
          <p>Specialists: {currentUser.involved}</p>
          <p>Notes: {currentUser.notes}</p>
          <div className="cuurentUser-container-btn">
            <button
              onClick={() => setModal(true)}
              className="edit-btn-currentUser"
            >
              Edit
            </button>
            <button
              onClick={(e) => handleDelete}
              className="edit-btn-currentUser"
            >
              Delete
            </button>
          </div>
        </>
      ) : (
        <p>Loading...</p>
      )}

      {modal && (
        <Modal
          setModal={setModal}
          header="Edit Car"
          currentUser={currentUser}
        />
      )}
    </div>
  );
};

export default IndividualCurrentUser;
