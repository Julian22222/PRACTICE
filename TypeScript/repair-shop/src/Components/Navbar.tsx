import React, { FC } from "react";
import { Link } from "react-router-dom";

const Navbar: FC = () => {
  const NavbarList: string[] = ["Home", "Waiting List", "Services", "Chat"];

  return (
    <div>
      {NavbarList.map((item, index) => {
        return item === "Home" ? (
          <span key={item} style={{ padding: 10, fontSize: 20 }}>
            <Link to={`/`} key={item} style={{ color: "red" }}>
              {" "}
              {item}
            </Link>
          </span>
        ) : (
          <span key={item} style={{ padding: 10, fontSize: 20 }}>
            <Link
              to={`/${item.toLowerCase().replace(" ", "-")}`}
              key={item}
              style={{ color: "red" }}
            >
              {item}
            </Link>
          </span>
        );
      })}
    </div>
  );
};

export default Navbar;
