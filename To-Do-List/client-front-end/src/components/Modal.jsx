import { useState } from "react";
// import { useCookies } from "react-cookie";

const Modal = ({ mode, setShowModal, getData, task, activeUser }) => {
  // const [cookies, setCookie, removeCookie] = useCookies(null);

  // const mode = "create";
  // if mode equals edit then it is true otherwise it's false
  const editMode = mode === "edit" ? true : false;

  const [data, setData] = useState({
    user_email: editMode ? task.user_email : activeUser,
    // user_email: editMode ? task.user_email : cookies.Email,
    title: editMode ? task.title : null,
    progress: editMode ? task.progress : 50,
    date: editMode ? task.date : new Date(),
  });

  const postData = async (e) => {
    e.preventDefault(); //prevent default action of the form, don't refreshe the page
    try {
      const response = await fetch(`${process.env.REACT_APP_SERVERURL}/todos`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });
      // console.log(response.status);
      if (response.status === 200) {
        // console.log("Worked");
        setShowModal(false);
        getData();
      }
    } catch (err) {
      console.log(err);
    }
  };

  const editData = async (e) => {
    e.preventDefault(); //prevent default action of the form, don't refreshe the page
    try {
      const response = await fetch(
        `${process.env.REACT_APP_SERVERURL}/todos/${task.todo_id}`,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          // JSON.stringify() <- use to send data to server,and database in string format, converting object to string
          body: JSON.stringify(data),
        }
      );

      if (response.status === 200) {
        setShowModal(false); //close modal
        getData(); //get new data from DB server
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target; // destructuring variables
    setData((data) => ({
      ...data,
      [name]: value,
    }));
    // console.log(data);
  };

  return (
    <div className="overlay">
      <div className="modal">
        <div className="form-title-container">
          <h3>Let's {mode} your task</h3>
          <button onClick={() => setShowModal(false)}>X</button>
        </div>

        <form>
          <input
            required //mandatory field
            maxLength={30}
            placeholder=" Your task goes here"
            name="title" //this name,must match with name from -->const [data, setData] = useState({..})
            value={data.title}
            onChange={handleChange}
          />
          {/* <br/> - break */}
          <br />

          <label for="range">Drag to select your current progeress</label>
          {/* for="range and id="range"--> links this <label> tag with input with id="range"  */}
          <input
            type="range"
            id="range"
            required
            min="0"
            max="100"
            name="progress" //this name,must match with name from -->const [data, setData] = useState({..})
            value={data.progress}
            onChange={handleChange}
          />
          <input
            className={mode}
            type="submit"
            onClick={editMode ? editData : postData} // if editMode is true then invoke editData function else postData function
          />
        </form>
      </div>
    </div>
  );
};

export default Modal;
