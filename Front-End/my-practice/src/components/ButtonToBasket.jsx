import Context from "./Context";
import { useContext } from "react";
// import { useState } from "react";

const ButtonToBasket = (props) => {
  const value = useContext(Context);

  //   const [quantity, setQuantity] = useState(false);

  const handleBtn = (event) => {
    event.preventDefault();
    // console.log(event);
    value.setBasketList([props.eachCard, ...value.basketList]);
    // console.log(props.eachCard);
    // value.setBasketList(props.eachCard);
    props.setQuantity(true);
    console.log(props.quantity);
  };
  //   console.log(value.basketList);

  return (
    <div>
      <button onClick={handleBtn} className="ButtonForCard">
        Add wine
      </button>
      {/* {props.quantity ? <p>Hello</p> : null}
      {props.quantity ? <p>True</p> : <p>False</p>} */}
    </div>
  );
};

export default ButtonToBasket;
