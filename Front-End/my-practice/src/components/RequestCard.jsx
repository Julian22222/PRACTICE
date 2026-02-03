const RequestCard = (props) => {
  return (
    <div className="RequestCard">
      <p>User: {props.user_name}</p>
      <p>City / Region.: {props.regionDetails}</p>
      <p>Client comment: {props.comments}</p>
    </div>
  );
};

export default RequestCard;
