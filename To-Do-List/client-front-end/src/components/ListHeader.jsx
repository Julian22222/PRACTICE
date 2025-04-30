// import { useCookies } from "react-cookie";
import { useState } from "react";
import Modal from "./Modal";

const ListHeader = ({
  listName,
  getData,
  activeUser,
  setActiveUser,
  setShowAuth,
}) => {
  const [showModal, setShowModal] = useState(false);

  const signOut = () => {
    // console.log("signout");

    setShowAuth(true);

    setActiveUser(null);
  };

  return (
    <div className="list-header">
      <h1>{listName}</h1>
      <div className="button-container">
        <button className="create" onClick={() => setShowModal(true)}>
          ADD NEW
        </button>
        <button className="signout" onClick={signOut}>
          SIGN OUT
        </button>
      </div>
      {showModal ? (
        <Modal
          mode={"create"}
          setShowModal={setShowModal}
          getData={getData}
          activeUser={activeUser}
        />
      ) : null}
      {/* the same - is showModal is true ->show Modal component */}
      {/* {showModal && <Modal mode={"create"} setShowModal={setShowModal} /> } */}
    </div>
  );
};

export default ListHeader;
