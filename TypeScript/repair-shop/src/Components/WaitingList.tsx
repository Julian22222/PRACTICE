import React, { FC } from "react";
import "../Styles/App.css";
import { IWaitingUser } from "../Types/types";
import WaitingUser_Item from "./WaitingUser_Item";
import { Link } from "react-router-dom";

interface WaitingListProps {
  // Define any props you want to pass to UserList
  users: IWaitingUser[]; // imported interface - Array of users
}

const WaitingList: FC<WaitingListProps> = ({ users }) => {
  return (
    <div>
      <p style={{ marginTop: "60px", marginBottom: "30px" }}>
        This is a Waiting list of customers who gave their contact details for
        repairs or maintenance.
      </p>
      {users.map((user) => (
        // <div key={user.id} style={{ padding: 15, border: "1px solid gray" }}>
        //   {user.id}. {user.name}
        // </div>
        <Link
          key={user.id}
          to={`/waiting-list/${user.id}`}
          style={{ textDecoration: "none" }}
        >
          <WaitingUser_Item key={user.id} user={user} />
        </Link>
      ))}
      <a
        href="/"
        style={{
          marginTop: "50px",
          marginBottom: "100px",
          display: "inline-block",
          padding: "10px 20px",
          backgroundColor: "#007BFF",
          color: "white",
          textAlign: "center",
          textDecoration: "none",
          borderRadius: "4px",
          border: "none",
          cursor: "pointer",
          fontSize: "16px",
        }}
      >
        Check the status of the cars that are currently in the repair shop
      </a>
    </div>
  );
};

export default WaitingList;
