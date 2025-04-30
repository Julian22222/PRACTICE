const WineCard = (props) => {
  return (
    <>
      <div className="Card">
        {/* <p> Winery: {props.winery}</p> */}
        <p>Wine: {props.wine.slice(0, -4)}</p>
        <p>Year: {props.wine.slice(-4)}</p>
        <p>Wine Rating: {props.rating}</p>
        <p>Wine votes: {props.reviews.split(" ")[0]}</p>
        {/* <p>Location: {props.location}</p> */}
      </div>
      <img
        src={props.image}
        alt="different wines"
        width="50"
        height="200"
        className="ClassImage"
      />
    </>
  );
};

export default WineCard;
