import React from "react";
import "../Styles/App.css";
import Navbar from "./Navbar";

function Header() {
  return (
    <div className="App">
      <h1 style={{ marginTop: "20px" }}>Car Repair Shop</h1>
      <img
        src="/IMG/img1.jpg"
        style={{ width: "100%", height: "auto" }}
        alt="header-img"
      />
      <p>A place where broken or damaged items are fixed or restored</p>
      <Navbar />
      <hr style={{ marginLeft: "25%", marginRight: "25%" }} />
    </div>
  );
}

export default Header;
