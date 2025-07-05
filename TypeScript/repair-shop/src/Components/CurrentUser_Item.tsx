import React, { FC } from "react";
import { ICurrentUser } from "../Types/types";

interface ICurentUser_ItemProps {
  customer: ICurrentUser;
}

const CurrentUser_Item: FC<ICurentUser_ItemProps> = ({ customer }) => {
  return (
    <div
      style={{
        padding: "10px",
        border: "1px solid gray",
        marginLeft: "5%",
        marginRight: "5%",
      }}
    >
      {customer?.car_id}. {customer?.brand}{" "}
      {customer?.date.slice(0, 10).split("-").reverse().join("/")}{" "}
      {/* // Format date from YYYY-MM-DD to DD/MM/YYYY */}
      {customer?.fuel}
    </div>
  );
};

export default CurrentUser_Item;
