import axios from "axios";
import React, { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IWaitingList } from "../Types/types";

const WaitingUser: FC = () => {
  const [waitingUser, setWaitingUser] = useState<IWaitingList>();

  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    if (id) getUser(id);
  }, [id]);

  const getUser = async (id: string) => {
    const response = await axios.get(
      `https://jsonplaceholder.typicode.com/users/${id}`
    );

    setWaitingUser(response.data);
  };

  return (
    <div>
      <p>User Id: {waitingUser?.id}</p>
      <p>User Name: {waitingUser?.name}</p>
      <p>Username: {waitingUser?.username}</p>
      <p>Email: {waitingUser?.email}</p>
      <p>
        Address: {waitingUser?.address?.street}, {waitingUser?.address?.city}
      </p>
      <p>Tel.:{waitingUser?.phone}</p>
    </div>
  );
};

export default WaitingUser;
