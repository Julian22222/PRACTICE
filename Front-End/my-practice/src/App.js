import { BrowserRouter, Routes, Route, useParams } from "react-router-dom";
import { React, useState } from "react";
import "./App.css";
import Homepage from "./components/Homepage";
import NavBar from "./components/NavBar";
import Home from "./components/Home";
import SearchBar from "./components/SearchBar";
import Basket from "./components/Basket";
import Requests from "./components/Requests";
import SingleCard from "./components/SingleCard";
import Context from "./components/Context";

function App() {
  const [wineList, setWineList] = useState([]);
  const [wineType, setWineType] = useState("reds");
  const [SortBy, setSortBy] = useState("reviews");
  const [order, setOrder] = useState("asc");
  const [basketList, setBasketList] = useState([]);

  const value = {
    wineList,
    setWineList,
    wineType,
    setWineType,
    SortBy,
    setSortBy,
    order,
    setOrder,
    basketList,
    setBasketList,
  };

  return (
    <BrowserRouter>
      <Context.Provider value={value}>
        <div className="App-header">
          <Homepage />
          {/* <SearchBar /> */}
          <NavBar />
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/wines/:type/:id" element={<SingleCard />} />
            <Route
              path="/basket"
              element={<Basket basketList={basketList} />}
            />
            <Route path="/requests" element={<Requests />} />
          </Routes>
        </div>
      </Context.Provider>
    </BrowserRouter>
  );
}

export default App;
