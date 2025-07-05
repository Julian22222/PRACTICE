import { FC, useContext } from "react";
import CurrentUser_Item from "../Components/CurrentUser_Item";
import { ICurrentUser } from "../Types/types";
import DragAndDropSchedule from "./DraggableImage";
import { Link } from "react-router-dom";
import Modal from "./Modal";
import { ModalContext } from "../context/ModalContext";

const emplPictures: Picture[] = [
  { id: "pic1", url: "/IMG/Emp/emp1.jpg", name: "John Doe" },
  { id: "pic2", url: "/IMG/Emp/emp2.jpg", name: "Tom Smith" },
  { id: "pic3", url: "/IMG/Emp/emp3.jpg", name: "Mike Johnson" },
  { id: "pic4", url: "/IMG/Emp/emp4.jpg", name: "Chris Brown" },
];

interface CurrentUsersProps {
  customers: ICurrentUser[]; // Array of delivery list items
}

type Picture = { id: string; url: string; name: string };

const CurrentUsers: FC<CurrentUsersProps> = ({ customers }) => {
  // const [modal, setModal] = useState<boolean>(false); // state for modal
  const { modal, openModal, closeModal } = useContext(ModalContext); //destructuring modal state and functions from ModalContext

  // const dropFunc = (event: DragEndEvent) => {
  //   console.log("Dropped item:", event.active.id);
  // };

  return (
    <div>
      <div className="container-home">
        <div className="left-flexbox-container">
          <h5>Cars That Are Currently in the Repair Shop</h5>
          <button
            style={{ marginBottom: "30px", padding: "10px" }}
            // onClick={() => setModal(true)}
            onClick={() => openModal()}
          >
            Add a car to garage
          </button>

          {customers.length === 0 ? (
            <div>
              <img
                src="/IMG/arrow.gif"
                alt="loading gif"
                style={{ width: "70%" }}
              />
              <p>Loading...</p>
            </div>
          ) : (
            <div>
              {customers.map((customer) => (
                <Link
                  key={customer.car_id}
                  to={`/current-user/${customer.car_id}`}
                  style={{ textDecoration: "none" }}
                >
                  <CurrentUser_Item key={customer.car_id} customer={customer} />
                </Link>
              ))}
            </div>
          )}
        </div>
        <div className="right-flexbox-container">
          {/* <h5>Mechanics of our Garage</h5>
        
            <p>
            Each customer is identified by their car ID, brand, number of seats,
            year of manufacture, and fuel type.
            </p> */}
          <div className="emp-container">
            {/* <p>
              Our team of mechanics is dedicated to providing top-notch service
              and ensuring that your vehicle is in the best condition possible.
              </p>
              <p>
              If you have any questions or need assistance, feel free to contact
              us.
              </p> */}

            <p>Meet Our Team</p>
            <p>We are here to help you with your car repairs!</p>
            <div
              className="container-pic"
              // onDragEnd={handleDragEnd}
            >
              {emplPictures.map((picture) => (
                <img
                  key={picture.id}
                  src={picture.url}
                  id={picture.id}
                  style={{ width: "20%" }}
                  alt={picture.id}
                />
              ))}
            </div>
            <div className="container-table">
              <table>
                <thead>
                  <tr className="table-header1">
                    <th className="name-col">Name</th>
                    <th className="pos-col">Position</th>
                    <th className="exp-col">Experience</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td style={{ paddingRight: "60px" }}>John Doe</td>
                    <td>Lead Mechanic</td>
                    <td style={{ paddingLeft: "60px" }}>10 years</td>
                  </tr>
                  <tr>
                    <td style={{ paddingRight: "60px" }}>Tom Smith</td>
                    <td>Assistant Mechanic</td>
                    <td style={{ paddingLeft: "60px" }}>5 years</td>
                  </tr>
                  <tr>
                    <td style={{ paddingRight: "60px" }}>Mike Johnson</td>
                    <td>Tire Specialist,Brake Technician</td>
                    <td style={{ paddingLeft: "60px" }}>3 years</td>
                  </tr>
                  <tr>
                    <td style={{ paddingRight: "60px" }}>Chris Brown</td>
                    <td>Electrical Specialist</td>
                    <td style={{ paddingLeft: "60px" }}>6 years</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      {/* ///////////////////////// */}

      <DragAndDropSchedule />
      <br />
      <br />

      {modal && <Modal setModal={closeModal} header="Add new car" />}
    </div>
  );
};

export default CurrentUsers;
