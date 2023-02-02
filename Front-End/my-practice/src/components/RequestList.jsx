import RequestCard from "./RequestCard";

const RequestList = ({ allComments }) => {
  return (
    <>
      <h3 className=" RequestListArea">
        List of all requests/ suggestions/ comments:
      </h3>
      <ul>
        {allComments.map((el) => {
          return (
            <li key={(el, el.name)}>
              <RequestCard
                fullName={el.name}
                contact={el.contact}
                comments={el.comments}
              />
            </li>
          );
        })}
      </ul>
    </>
  );
};

export default RequestList;
