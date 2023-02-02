const RequestCard = (props) => {
  return (
    <div className="RequestCard">
      <p>Name: {props.fullName}</p>
      <p>Tel.nr.: {props.contact}</p>
      <p>Client comment: {props.comments}</p>
    </div>
  );
};

export default RequestCard;
