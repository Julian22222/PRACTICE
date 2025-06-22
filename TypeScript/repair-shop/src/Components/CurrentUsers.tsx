import { FC, useState } from "react";
import CurrentUser_Item from "../Components/CurrentUser_Item";
import { ICurrentUsers } from "../Types/types";
import {
  DndContext,
  closestCenter,
  useDraggable,
  useDroppable,
  DragEndEvent,
} from "@dnd-kit/core";
import DraggableImage from "./DraggableImage";
import DragAndDropSchedule from "./DraggableImage";
import { Link } from "react-router-dom";

const emplPictures: Picture[] = [
  { id: "pic1", url: "/IMG/Emp/emp1.jpg", name: "John Doe" },
  { id: "pic2", url: "/IMG/Emp/emp2.jpg", name: "Tom Smith" },
  { id: "pic3", url: "/IMG/Emp/emp3.jpg", name: "Mike Johnson" },
  { id: "pic4", url: "/IMG/Emp/emp4.jpg", name: "Chris Brown" },
];

interface CurrentUsersProps {
  customers: ICurrentUsers[]; // Array of delivery list items
}

type Picture = { id: string; url: string; name: string };

const CurrentUsers: FC<CurrentUsersProps> = ({ customers }) => {
  const dropFunc = (event: DragEndEvent) => {
    console.log("Dropped item:", event.active.id);
  };

  return (
    <div>
      <div className="container-home">
        <div className="left-flexbox-container">
          <h5>Cars That Are Currently in the Repair Shop</h5>

          <div>
            {customers.map((customer) => (
              <Link
                to={`/current-user/${customer.car_id}`}
                style={{ textDecoration: "none", color: "black" }}
              >
                <CurrentUser_Item key={customer.car_id} customer={customer} />
              </Link>
            ))}
          </div>
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
                  <tr style={{ paddingRight: "30px" }}>
                    <td>John Doe</td>
                    <td>Lead Mechanic</td>
                    <td>10 years</td>
                  </tr>
                  <tr style={{ paddingRight: "30px" }}>
                    <td>Tom Smith</td>
                    <td>Assistant Mechanic</td>
                    <td>5 years</td>
                  </tr>
                  <tr style={{ paddingRight: "30px" }}>
                    <td>Mike Johnson</td>
                    <td>Tire Specialist,Brake Technician</td>
                    <td>3 years</td>
                  </tr>
                  <tr>
                    <td>Chris Brown</td>
                    <td>Electrical Specialist</td>
                    <td>6 years</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      {/* ///////////////////////// */}

      <DragAndDropSchedule />
    </div>
  );
};

export default CurrentUsers;
