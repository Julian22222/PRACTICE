import { useEffect, useState } from "react";
import { data } from "./data.js";
import "./App.css";

function App() {
  const [mySort, setMySort] = useState("");
  const [myOrder, setMyOrder] = useState("");
  const [mySearch, setMySearch] = useState(data);

  useEffect(() => {}, [mySort, myOrder]);

  if (mySort === "id" || (mySort === "id" && myOrder === "desc")) {
    data.sort((a, b) => {
      return a.id > b.id ? 1 : -1;
    });
  }

  if (mySort === "id" && myOrder === "asc") {
    data.sort((a, b) => {
      return a.id > b.id ? -1 : 1;
    });
  }

  if (mySort === "rating" && myOrder === "asc") {
    data.sort((a, b) => {
      return a.rating > b.rating ? -1 : 1;
    });
  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////functions

  const handleSortBy = (event) => {
    console.log("mySort", event.target.value);
    setMySort(event.target.value);
  };

  const handleOrder = (event) => {
    console.log(event.target.value);
    setMyOrder(event.target.value);
  };

  const myFilter = (e) => {
    console.log(e.target.value);

    setMySearch(
      data.filter((el) => {
        return el.first_name.toLowerCase().includes(e.target.value);
      })
    );
  };

  return (
    <div className="App">
      <label>SortBy:</label>
      <select onChange={handleSortBy} style={{ marginRight: 20 }}>
        <option>Select Value</option>
        <option value="id">id</option>
        <option value="name">name</option>
        <option value="rating">rating</option>
      </select>

      <label>Order:</label>
      <select onChange={handleOrder} value={myOrder}>
        <option></option>
        <option value="asc">Ascending</option>
        <option value="desc">Descending</option>
      </select>
      <br />
      <br />

      <input placeholder="Search by first name..." onChange={myFilter} />

      <ul>
        {mySearch.map((el) => {
          return (
            <li key={el.id}>
              {el.id} - {el.name} - {el.address} - {el.email} - {el.status} -{" "}
              {el.phone} - {el.rating}
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default App;
