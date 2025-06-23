import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ICurrentUsers } from "../Types/types";

const CurrentUser: FC = () => {
  const [currentgUser, setCurrentUser] = useState<ICurrentUsers>();

  const { car_id } = useParams<{ car_id: string }>();

  useEffect(() => {
    if (car_id) getUser(car_id);
  }, [car_id]);

  const getUser = async (car_id: string) => {
    const response = await axios.get(
      `https://car-shop-back-end.onrender.com/${car_id}`
    );

    setCurrentUser(response.data);
  };

  return (
    <div>
      <p>Car Id: {currentgUser?.car_id}</p>
      <p>Name: {currentgUser?.brand}</p>
      <p>Seats: {currentgUser?.seats}</p>
      <p>Year: {currentgUser?.date}</p>
      <p>Fuel typ:{currentgUser?.fuel}</p>
    </div>
  );
};

export default CurrentUser;
