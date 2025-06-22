import React, { FC } from "react";
import { ICurrentUsers } from "../Types/types";

interface ICuurentUser_ItemProps {
  customer: ICurrentUsers;
}

const CurrentUser_Item: FC<ICuurentUser_ItemProps> = ({ customer }) => {
  return (
    <div style={{ padding: 15, border: "1px solid gray" }}>
      {customer.car_id} {customer.brand} {customer.year.slice(0, 4)}{" "}
      {customer.fuel}
    </div>
  );
};

export default CurrentUser_Item;
