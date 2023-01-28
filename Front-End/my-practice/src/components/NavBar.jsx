import { Link } from "react-router-dom";

const NavBar = () => {
  const items = ["home", "requests", "basket"];

  return (
    <div className="NaviBar">
      <ul>
        {items.map((item) => {
          return item === "home" ? (
            <li key={item} className="NavBar">
              <Link to="/" className="NavBar">
                {" "}
                {item}
              </Link>
            </li>
          ) : item === "requests" ? (
            <li key={item} className="NavBar">
              <Link to="requests" className="NavBar">
                {item}
              </Link>
            </li>
          ) : (
            <li key={item} className="NavBar">
              <Link to="basket" className="NavBar">
                {item}
              </Link>
            </li>
          );
        })}
      </ul>
    </div>
  );
};

export default NavBar;
