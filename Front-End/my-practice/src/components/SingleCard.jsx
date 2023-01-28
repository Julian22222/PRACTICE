import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import Context from "./Context";
import { useContext } from "react";
import ButtonToBasket from "./ButtonToBasket";

const SingleCard = () => {
  const value = useContext(Context);

  const [eachCard, setEachCard] = useState({});

  const { type, id } = useParams();
  // console.log(type, id);

  useEffect(() => {
    fetch(`https://api.sampleapis.com/wines/${type}/${id}`)
      .then((topicData) => topicData.json())
      .then((data) => {
        console.log(data);
        console.log(setEachCard(data));
        setEachCard(data);
        console.log(eachCard);
      });
  }, []);

  return (
    <div className="App">
      {eachCard?(<h1>Add your favorite wines to the basket </h1>
      {/* <h1>{eachCard}</h1> */}
      <p>{JSON.stringify(eachCard)}</p>
      {/* <h1>Wine: {eachCard.wine.slice(0, -4)}</h1> */}
      <h2>Winery: {eachCard.winery}</h2>
      <p>Year: {eachCard.wine}</p>
      <p>Wine Rating: {eachCard?.rating.reviews}</p>
      <h2>Wine votes: {eachCard?.rating.reviews}</h2>
      <h2>Location: {eachCard.location}</h2>
      <img
        src={eachCard.image}
        alt="different wines"
        width="100"
        height="400"
      />
      <br></br>
      <ButtonToBasket eachCard={eachCard} />) : (<h1>is loading..</h1>)}
      
    </div>
  );
};

export default SingleCard;
