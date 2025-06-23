import React, { FC } from "react";
import { ICurrentUsers } from "../Types/types";

interface ICurentUser_ItemProps {
  customer: ICurrentUsers;
}

const CurrentUser_Item: FC<ICurentUser_ItemProps> = ({ customer }) => {
  return (
    <div style={{ padding: 15, border: "1px solid gray" }}>
      {customer?.car_id} {customer?.brand} {customer?.date} {customer?.fuel}
    </div>
  );
};

export default CurrentUser_Item;
