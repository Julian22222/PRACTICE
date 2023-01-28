// import { useState, useEffect } from "react";
import Context from "./Context";
import { useContext } from "react";
import WineCard from "./WineCard";

const Basket = () => {
  const value = useContext(Context);
  console.log(value);
  console.log(value.basketList);
  return (
    <div className="Basket">
      <h1>My Basket</h1>
      {/* <ul>
        {value.basketList.map((item) => {
          return (
            <li>
              <WineCard
                winery={item.winery}
                wine={item.wine}
                rating={item.rating.average}
                reviews={item.rating.reviews}
                location={item.location}
                image={item.image}
                id={item.id}
              />
            </li>
          );
        })}
      </ul> */}
    </div>
  );
};

export default Basket;
