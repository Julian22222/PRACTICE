import React, { FC } from "react";
import { IWaitingUser } from "../Types/types";

interface IWaitingListProps {
  user: IWaitingUser;
}

const WaitingUser_Item: FC<IWaitingListProps> = ({ user }) => {
  return (
    <div style={{ padding: 15, border: "1px solid gray" }}>
      {user.id} {user.name} {user.phone}
    </div>
  );
};

export default WaitingUser_Item;
