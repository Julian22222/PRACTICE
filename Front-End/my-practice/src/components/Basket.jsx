import { useState } from "react";
import Context from "./Context";
import { useContext } from "react";
import WineCard from "./WineCard";
import RemoveItem from "./RemoveItem";

const Basket = () => {
  const value = useContext(Context);
  // console.log(value);
  // console.log(value.basketList);
  // console.log(value.basketList.wine);

  const [msgSendOrder, setMsgSendOrder] = useState(false);

  const hadleOrder = () => {
    value.setBasketList([]);
    setMsgSendOrder(true);
  };

  let num = 1;

  return (
    <div className="Basket">
      <h1>My Basket</h1>
      <p className="TotalBasket">
        Total items in my basket: {value.basketList.length}
      </p>
      <ul>
        {value.basketList.map((item) => {
          return (
            <li key={(item.id, item.image)} className="basketItems">
              <WineCard
                winery={item.winery}
                wine={item.wine}
                rating={item.rating.average}
                reviews={item.rating.reviews}
                location={item.location}
                image={item.image}
                id={item.id}
              />
              <p>quantity: {num}</p>

              <RemoveItem item={item} />
            </li>
          );
        })}
      </ul>
      {value.basketList.length > 0 ? (
        <button className="OrderBtn" onClick={hadleOrder}>
          Order
        </button>
      ) : null}
      {msgSendOrder ? (
        <p className="MsgOrderHasBeenSent">your order has been sent</p>
      ) : null}
      {/* <p> button Order appears when basketList.length more than 0 </p>
      <p>when you click on Order button - basketList - become empty, show message your order has been sent</p> */}
    </div>
  );
};

export default Basket;
