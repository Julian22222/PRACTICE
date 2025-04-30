import { useState } from "react";
import RequestList from "./RequestList";

const Requests = () => {
  const [allComments, setAllComments] = useState([
    {
      user: "John Travolta",
      region: "Manchester",
      comments:
        "A beautifully complex English sparkling from the 2010 vintage, featuring a yeasty nose of bruised apple, orange zest and honey-roasted hazelnuts. Savoury brioche and lemon curd on the palate with more bruised yellow fruits and nuts, followed by a long, butter roasted nuts on the finish. 10.5g/l of residual sugar...",
    },
    {
      user: "Amy Winehouse",
      region: "Leeds",
      comments:
        "A salivating, food-friendly Prosecco features salted citrus on the nose. Crunchy green fruits and a touch of ripe red apple peel on the palate. The 14.5g/l of sugar is refreshed by a solid line of acidity (5.8g/l), leading to a clean finish. ",
    },
  ]);

  const [msg, setMsg] = useState(false);

  const [custForm, setCustForm] = useState({
    user: "",
    region: "",
    comments: "",
  });

  const handleSubmit = (event) => {
    event.preventDefault();
    // console.log(custForm);

    setAllComments([...allComments, custForm]);
    // console.log(allComments);
    setCustForm({
      user: "",
      region: "",
      comments: "",
      // request: "",
    });
    setMsg(true);
  };

  const handleChange = (event) => {
    setCustForm((prevForm) => {
      return {
        ...prevForm,
        [event.target.name]: event.target.value,
      };
    });
  };

  const canSave = [...Object.values(custForm)].every(Boolean);
  //submit button will allow to press only when all object values in custForm - are true(have a value)

  return (
    <div className="Requests">
      <div className="RequestForm">
        <h2 className="RequestContactUS">
          Contact Us, make a request about future orders and etc.
        </h2>

        {/* could add some features to the form - Regex, hints what to input in diffrent field
add onBlur={onBlur} in the form */}

        <form className="SubmissionForm" onSubmit={handleSubmit}>
          <label>
            User name:<br></br>
            <input
              type="text"
              name="user"
              // pattern="([A-Z])[ \w\s.]{1,} "
              value={custForm.user}
              placeholder="Insert your user name"
              onChange={handleChange}
            ></input>
          </label>
          <label>
            Region / City :<br></br>
            <input
              type="text"
              name="region"
              // pattern="([A-Z])[ \w+.]{,1} "
              value={custForm.region}
              placeholder="Insert your city / region"
              onChange={handleChange}
            ></input>
          </label>
          <label>
            Comments:<br></br>
            <textarea
              className="TextArea"
              type="text"
              name="comments"
              value={custForm.comments}
              placeholder="Insert your text"
              onChange={handleChange}
            ></textarea>
          </label>
          <button
            className="RequestsBtn"
            type="submit"
            value="Submit"
            disabled={!canSave}
            //button submit is disabled untill all object values in custForm will be true(have a value)
          >
            {" "}
            Submit
          </button>
        </form>
      </div>
      {msg ? <p className="MsgRequest">Your request has been posted</p> : null}
      <RequestList allComments={allComments} />
    </div>
  );
};
export default Requests;
