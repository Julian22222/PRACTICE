import React, { FC } from "react";
import { IWaitingList } from "../Types/types";

interface IWaitingListProps {
  user: IWaitingList;
}

const WaitingUser_Item: FC<IWaitingListProps> = ({ user }) => {
  return (
    <div key={user.id} style={{ padding: 15, border: "1px solid gray" }}>
      {user.id} {user.name} {user.phone}
    </div>
  );
};

export default WaitingUser_Item;
