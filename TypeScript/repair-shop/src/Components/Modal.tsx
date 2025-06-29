import { create } from "domain";
import { FC, useState } from "react";
import { ICurrentUser } from "../Types/types";

interface IModalProps {
  setModal: (value: boolean) => void; // function to set modal visibility
  // setModal: React.Dispatch<React.SetStateAction<boolean>>;

  header: string; // header text for the modal
  currentUser?: ICurrentUser;
}

const Modal: FC<IModalProps> = ({ setModal, header, currentUser }) => {
  const editMode = header === "Edit Car" ? true : false; // check if the modal is in edit mode based on the header text

  const [data, setData] = useState({
    brand: editMode ? currentUser?.brand : "",
    seats: editMode ? currentUser?.seats : "",
    // input type="date" _> only accepts: YYYY-MM-DD format
    date: editMode ? currentUser?.date.slice(0, 10) : "",
    fuel: editMode ? currentUser?.fuel : "petrol", // default fuel type
    serviceCheck: editMode ? currentUser?.serviceCheck : false, // default service check status
    involved: editMode
      ? typeof currentUser?.involved === "string"
        ? currentUser.involved.split(", ").filter(Boolean)
        : []
      : [], // convert array to string for input
    notes: editMode ? currentUser?.notes : "",
    phone: editMode ? currentUser?.phone : "",
  }); // state to hold form data

  const createData = async (e: { preventDefault: () => void }) => {
    e.preventDefault(); // prevent default form submission behavior
    try {
      const response = await fetch("https://car-shop-back-end.onrender.com", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ ...data, involved: data.involved.join(", ") }), // send data as JSON, Convert array back to string before sending
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const result = await response.json();
      console.log("Data created successfully:", result);
      setModal(false); // close modal after successful creation
    } catch (error) {
      console.error("Error creating data:", error);
    }
  };

  // const editData = async (e: { preventDefault: () => void }) => {
  const editData = async (e: React.FormEvent) => {
    //React.KeyboardEvent - if it is keyboard event, React.MouseEvent - if it is mouse event
    e.preventDefault(); // prevent default form submission behavior
    try {
      const response = await fetch(
        `https://car-shop-back-end.onrender.com/${currentUser?.car_id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ ...data, involved: data.involved.join(", ") }), // send updated data as JSON, Convert array back to string before sending
        }
      );
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const result = await response.json();
      console.log("Data updated successfully:", result);
      setModal(false); // close modal after successful update
    } catch (error) {
      console.error("Error updating data:", error);
    }
  };

  return (
    <div className="modal-container">
      <div className="modal">
        <div className="modal-header">
          <h3>{header}</h3>
          <button className="modal-close" onClick={() => setModal(false)}>
            X
          </button>
        </div>

        <form className="form-container">
          <div className="label-input">
            <label htmlFor="brand" className="label-modal">
              Car Brand
            </label>
            <input
              id="brand" // links this <label> tag with input with id="brand"
              required //mandatory field
              maxLength={30}
              placeholder="insert car brand"
              name="brand" //this name,must match with name from -->const [data, setData] = useState({..})
              value={data.brand} // bind value to state
              onChange={(e) => setData({ ...data, brand: e.target.value })} // update state on change
            />
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="seats" className="label-modal">
              Seats
            </label>
            {/* for="range and id="range"--> links this <label> tag with input with id="range"  */}
            <input
              id="seats"
              type="number"
              placeholder="Insert number of seats"
              value={data.seats}
              onChange={(e) => setData({ ...data, seats: e.target.value })} // update state on change
            />
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="date" className="label-modal">
              Car release
            </label>
            <input
              id="date" // links this <label> tag with input with id="date"
              type="date"
              placeholder="Insert a year of release"
              required
              value={data.date} // bind value to state
              onChange={(e) => setData({ ...data, date: e.target.value })} // update state on change
            />
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="fuel" className="label-modal">
              Fuel
            </label>
            <select
              id="fuel" // links this <label> tag with select with id="fuel"
              className="modal-fuel"
              required
              value={data.fuel}
              onChange={(e) => setData({ ...data, fuel: e.target.value })} // update state on change
            >
              <option value="petrol">Petrol</option>
              <option value="diesel">Diesel</option>
              <option value="electric">Electric</option>
              <option value="hybrid">Hybrid</option>
            </select>
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="phone" className="label-modal">
              Tel.:
            </label>
            <input
              id="phone" // links this <label> tag with input with id="phone"
              type="text"
              value={data.phone} // bind value to state
              onChange={(e) => setData({ ...data, phone: e.target.value })} // update state on change
            />
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="serviceCheck" className="label-modal">
              ServiceCheck
            </label>
            <input
              id="serviceCheck" // links this <label> tag with input with id="serviceCheck"
              type="checkbox"
              value={data.serviceCheck ? "true" : "false"} // bind value to state
              onChange={
                (e) => setData({ ...data, serviceCheck: e.target.checked }) //convert to boolean

                // value={data.serviceCheck}
                // onChange={(e) => setData({...data, serviceCheck: !e.target.value})}
              } // update state on change
            />
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="staff" className="label-modal">
              Specialists
            </label>
            <br />

            <div>
              <input
                type="checkbox"
                id="John Doe"
                name="John Doe"
                value="John Doe"
                checked={data.involved.includes("John Doe")} // check if John Doe is involved
                onChange={(e) => {
                  const { checked, value } = e.target;
                  setData({
                    ...data,
                    involved: checked
                      ? [...data.involved, value] // add to array
                      : data.involved.filter(
                          (person: string) => person !== value
                        ), // remove from array
                  });
                }}
              />
              <label htmlFor="John Doe" className="label-modal">
                {" "}
                John Doe
              </label>
              <br />
              <input
                type="checkbox"
                id="Tom Smith"
                name="Tom Smith"
                value="Tom Smith"
                checked={data.involved.includes("Tom Smith")} // check if Tom Smith is involved
                onChange={(e) => {
                  const { checked, value } = e.target;
                  setData({
                    ...data,
                    involved: checked
                      ? [...data.involved, value]
                      : data.involved.filter((person) => person !== value),
                  });
                }}
              />
              <label htmlFor="Tom Smith" className="label-modal">
                {" "}
                Tom Smith
              </label>
              <br />
              <input
                type="checkbox"
                id="Mike Johnson"
                name="Mike Johnson"
                value="Mike Johnson"
                checked={data.involved.includes("Mike Johnson")} // check if Mike Johnson is involved
                onChange={(e) => {
                  const { checked, value } = e.target;
                  setData({
                    ...data,
                    involved: checked
                      ? [...data.involved, value]
                      : data.involved.filter((person) => person !== value),
                  });
                }}
              />
              <label htmlFor="Mike Johnson" className="label-modal">
                {" "}
                Mike Johnson
              </label>
              <br></br>
              <input
                type="checkbox"
                id="Chris Brown"
                name="Chris Brown"
                value="Chris Brown"
                checked={data.involved.includes("Chris Brown")} // checking whether the checkbox is ticked or not for Chris Brown, It checks if "Chris Brown" is inside the data.involved array
                onChange={(e) => {
                  //This runs when the checkbox is clicked.

                  const { checked, value } = e.target; //checked → true if checkbox is ticked, false if unticked., //value → the value of the checkbox, which is "Chris Brown" in this case.

                  setData({
                    ...data,
                    involved: checked // if the checkbox is ticked(true), it adds "Chris Brown" to the data.involved array, If checkbox is unticked(false), it removes "Chris Brown" from the data.involved array.
                      ? [...data.involved, value]
                      : data.involved.filter((person) => person !== value),
                  });
                }}
              />
              <label htmlFor="Chris Brown" className="label-modal">
                {" "}
                Chris Brown
              </label>

              {/* <input
                value={data.involved} // bind value to state
                onChange={(e) => setData({ ...data, involved: e.target.value })} // update state on change
              /> */}
            </div>
          </div>
          <br />

          <div className="label-input">
            <label htmlFor="notes" className="label-modal">
              Notes
            </label>
            <input
              id="notes" // links this <label> tag with input with id="notes"
              className="modal-area"
              required
              type="area"
              placeholder="Add your Notes..."
              value={data.notes} // bind value to state
              onChange={(e) => setData({ ...data, notes: e.target.value })} // update state on change
            />
          </div>

          <input
            className="submit-btn"
            type="submit"
            onClick={editMode ? editData : createData} // call appropriate function based on edit mode
          />
        </form>
      </div>
    </div>
  );
};

export default Modal;
