import React, { FC, useState } from "react";
import {
  DndContext,
  closestCenter,
  useDraggable,
  useDroppable,
  DragEndEvent,
} from "@dnd-kit/core";

const LOCAL_STORAGE_KEY = "employee-assignments";

const saveToLocalStorage = (data: any) => {
  localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(data));
};

const loadFromLocalStorage = (): { [day: string]: Picture[] } => {
  const data = localStorage.getItem(LOCAL_STORAGE_KEY);
  return data ? JSON.parse(data) : {};
};

type Picture = { id: string; url: string; name: string };

const emplPictures: Picture[] = [
  { id: "pic1", url: "/IMG/Emp/emp1.jpg", name: "John Doe" },
  { id: "pic2", url: "/IMG/Emp/emp2.jpg", name: "Tom Smith" },
  { id: "pic3", url: "/IMG/Emp/emp3.jpg", name: "Mike Johnson" },
  { id: "pic4", url: "/IMG/Emp/emp4.jpg", name: "Chris Brown" },
];

const days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

const DraggableImage = ({ picture }: { picture: Picture }) => {
  //({ picture }) <--this component takes a picture prop of type Picture,  : { picture: Picture } <--  is TypeScript type checking.
  const { attributes, listeners, setNodeRef } = useDraggable({
    id: picture.id,
  });
  // = useDraggable({id: picture.id,}); <- useDraggable hook to make the image draggable, and it has a unique ID.
  //This tells the -> drag-and-drop library @dnd-kit/core, - "This item is draggable,
  //setNodeRef: <--This is a ref function you put on the element (like ref={setNodeRef}) to make it draggable. It connects the DOM element to the drag system.
  //listeners: <--This contains event handlers for mouse and touch events, like onMouseDown, onTouchStart, etc. You spread it onto the element to make it interactive. You spread these event hadlers into the element: ...listeners.
  //attributes: <--This contains additional attributes that the library might need to manage the drag state. You spread it onto the element as well: ...attributes.

  //You are saying: “Hey, this picture can be dragged! Here’s its ID. Now, connect it to the drag system using this ref, and attach the necessary event listeners and attributes to the image.

  return (
    <img
      ref={setNodeRef}
      {...listeners}
      {...attributes}
      src={picture.url}
      alt={picture.name}
      style={{
        width: "80px",
        cursor: "grab",
        margin: "5px",
        borderRadius: "8px",
        border: "2px solid #ccc",
      }}
    />
  );
};

//Another Component - DroppableCell, It represents one day cell in your weekly schedule table — like Monday, Tuesday, etc.
const DroppableCell = ({
  day, // The name of the day (like "Mon", "Tue", etc.)
  assignedPictures, //A list of pictures that are already assigned to this day
  onRemove,
}: {
  day: string; //TypeScript type checking for day, which is a string representing the day of the week.
  assignedPictures: Picture[]; //TypesScript type checking for assignedPictures, which is an array of Picture objects.
  onRemove: (day: string, pictureId: string) => void; //Function to handle removing a picture from the day cell
}) => {
  const { isOver, setNodeRef } = useDroppable({
    //useDroppable hook from @dnd-kit/core, This component is a drop zone. You can drop something onto it.
    id: day,
  });
  //id: day → gives each day cell a unique ID (like "Mon", "Tue"), so the drag-and-drop system knows which cell you dropped on.
  //setNodeRef → You must attach this to the HTML element you want to make droppable. It connects the cell to the drop logic.
  //isOver → This is true when a draggable item is being hovered over this cell (you can use it to change the background color or highlight the cell)

  return (
    <td
      ref={setNodeRef}
      style={{
        border: "1px solid white",
        height: "150px",
        backgroundColor: isOver ? "#282c34;" : "white",
        verticalAlign: "top",
      }}
    >
      {assignedPictures.map((pic) => (
        <div>
          <div
            key={pic.id}
            style={{
              color: "black",
              display: "inline-block",
              marginRight: "10px",
              alignItems: "center",
              justifyContent: "space-between",
              marginBottom: "5px",
              padding: "4px",
              borderRadius: "4px",
            }}
          >
            {pic.name}
          </div>
          <button
            onClick={() => onRemove(day, pic.id)}
            style={{
              display: "inline-block",
              cursor: "pointer",
              background: "red",
              color: "white",
              border: "none",
              borderRadius: "3px",
              padding: "2px 6px",
            }}
          >
            X
          </button>
        </div>
      ))}
    </td>
  );
};

//Another Component
const DragAndDropSchedule: FC = () => {
  const [assignments, setAssignments] = useState<{ [day: string]: Picture[] }>(
    loadFromLocalStorage()
  );

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (over) {
      const day = over.id as string;
      const picture = emplPictures.find((pic) => pic.id === active.id);
      if (picture) {
        setAssignments((prev) => {
          const alreadyAssigned = prev[day]?.some((p) => p.id === picture.id);
          if (alreadyAssigned) {
            return prev; // Prevent duplicate
          }

          const updated = {
            ...prev,
            [day]: prev[day] ? [...prev[day], picture] : [picture],
          };
          saveToLocalStorage(updated);

          return updated;
        });
      }
    }
  };

  const handleRemove = (day: string, pictureId: string) => {
    setAssignments((prev) => {
      const updated = {
        ...prev,
        [day]: prev[day]?.filter((pic) => pic.id !== pictureId) || [],
      };
      saveToLocalStorage(updated);
      return updated;
    });
  };

  return (
    <DndContext collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
      <p>Drag and Drop Shift Schedule for all week</p>
      <div style={{ display: "flex", marginBottom: "20px" }}>
        {emplPictures.map((pic) => (
          <DraggableImage key={pic.id} picture={pic} />
        ))}
      </div>

      <table style={{ width: "100%", borderCollapse: "collapse" }}>
        <thead>
          <tr>
            {days.map((day) => (
              <th key={day}>{day}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          <tr>
            {days.map((day) => (
              <DroppableCell
                key={day}
                day={day}
                assignedPictures={assignments[day] || []}
                onRemove={handleRemove}
              />
            ))}
          </tr>
        </tbody>
      </table>
    </DndContext>
  );
};

export default DragAndDropSchedule;
