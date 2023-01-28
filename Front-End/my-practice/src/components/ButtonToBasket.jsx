import Context from "./Context";
import { useContext } from "react";
// import { useState } from "react";

const ButtonToBasket = (props) => {
  const value = useContext(Context);

  //   const [basketList, setBasketList] = useState([]);

  const handleBtn = () => {
    // event.preventDefault();

    // setBasketList((currList) => {
    //   [props.eachCard, ...currList];
    // });
    value.setBasketList(props.eachCard);
    console.log(value.basketList);
  };

  return (
    <>
      <button onClick={handleBtn} className="ButtonForCard">
        Add wine
      </button>
    </>
  );
};

export default ButtonToBasket;
