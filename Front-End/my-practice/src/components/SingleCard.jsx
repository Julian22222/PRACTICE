import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import Context from "./Context";
import { useContext } from "react";
import ButtonToBasket from "./ButtonToBasket";

const SingleCard = () => {
  const value = useContext(Context);
  const [quantity, setQuantity] = useState(false);
  const [eachCard, setEachCard] = useState({});

  const { type, id } = useParams();
  // console.log(type, id);

  useEffect(() => {
    fetch(`https://api.sampleapis.com/wines/${type}/${id}`)
      .then((topicData) => topicData.json())
      .then((data) => {
        console.log(data);
        // console.log(setEachCard(data));
        setEachCard(data);
        console.log(eachCard);
      });
  }, [quantity]);

  return (
    <div>
      {/* //put ternary operator ->  eachCard ? <p>......<p> : <p>Page is Loading</p> */}
      {/* <p>Wine: {eachCard.wine?.slice(0, -4)}</p> */}
      {/* {eachCard.wine ?  */}
      <h4 className="Header">Add your favorite wines to the basket </h4>

      <div className="SingleCard">
        {eachCard.wine ? (
          <div>
            {/* <p>{JSON.stringify(eachCard)}</p> */}
            <p>Wine: {eachCard.wine?.slice(0, -4)}</p>
            <p>Winery: {eachCard.winery}</p>
            <p>Year: {eachCard.wine}</p>
            <p>Wine Rating: {eachCard.rating.average}</p>
            <p>Wine votes: {eachCard.rating.reviews}</p>
            <p>Location: {eachCard.location}</p>
          </div>
        ) : (
          <p>Page is loading...</p>
        )}

        {/* <p>{JSON.stringify(eachCard)}</p> */}
        {/* <p>Wine: {eachCard.wine?.slice(0, -4)}</p>
        <p>Winery: {eachCard?.winery}</p>
        <p>Year: {eachCard?.wine}</p>
        <p>Wine Rating: {eachCard.rating?.average}</p>
        <p>Wine votes: {eachCard.rating?.reviews}</p>
        <p>Location: {eachCard?.location}</p> */}

        <img
          src={eachCard.image}
          alt="different wines"
          width="80"
          height="300"
        />
      </div>
      <br></br>
      <ButtonToBasket
        eachCard={eachCard}
        quantity={quantity}
        setQuantity={setQuantity}
      />
      {quantity ? (
        <p className="SingleCardMsgBasket">
          This Wine has been added to the basket
        </p>
      ) : null}

      {/* : <p>Page is Loading ...</p> */}
    </div>
  );
};

export default SingleCard;
