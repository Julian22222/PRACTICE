import React, { FC } from "react";
import { Link, useLocation } from "react-router-dom";

const Navbar: FC = () => {
  const location = useLocation(); // Get the current location from React Router
  //location.pathname tells you the current path (e.g., /, /chat, etc.).

  const NavbarList: string[] = ["Home", "Waiting List", "Services", "Chat"];

  return (
    <div>
      {NavbarList.map((item) => {
        const path =
          item === "Home" ? "/" : `/${item.toLowerCase().replace(" ", "-")}`; //If the item is "Home", the path is /. For other items, it converts the text to lowercase, replaces spaces with -, and adds a / in front.

        const isActive = location.pathname === path;

        return (
          <span key={item} style={{ padding: 10, fontSize: 20 }}>
            <Link
              to={path}
              style={{
                color: isActive ? "blue" : "red",
                textDecoration: isActive ? "underline" : "none",
              }}
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
