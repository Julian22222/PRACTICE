import React, { FC, useState } from "react";
import {
  DndContext,
  closestCenter,
  useDraggable,
  useDroppable,
  DragEndEvent,
} from "@dnd-kit/core";

type Picture = { id: string; url: string; name: string };

const emplPictures: Picture[] = [
  { id: "pic1", url: "/IMG/Emp/emp1.jpg", name: "John Doe" },
  { id: "pic2", url: "/IMG/Emp/emp2.jpg", name: "Tom Smith" },
  { id: "pic3", url: "/IMG/Emp/emp3.jpg", name: "Mike Johnson" },
  { id: "pic4", url: "/IMG/Emp/emp4.jpg", name: "Chris Brown" },
];

const days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

const DraggableImage = ({ picture }: { picture: Picture }) => {
  const { attributes, listeners, setNodeRef } = useDraggable({
    id: picture.id,
  });

  return (
    <img
      ref={setNodeRef}
      {...listeners}
      {...attributes}
      src={picture.url}
      alt={picture.name}
      style={{ width: "80px", cursor: "grab", margin: "5px" }}
    />
  );
};

const DroppableCell = ({
  day,
  assignedPictures,
}: {
  day: string;
  assignedPictures: Picture[];
}) => {
  const { isOver, setNodeRef } = useDroppable({
    id: day,
  });

  return (
    <td
      ref={setNodeRef}
      style={{
        border: "1px solid black",
        height: "150px",
        backgroundColor: isOver ? "#e0ffe0" : "white",
        verticalAlign: "top",
      }}
    >
      <div style={{ padding: "4px" }}>
        {assignedPictures.map((pic) => (
          <div key={pic.id}>{pic.name}</div>
        ))}
      </div>
    </td>
  );
};

const DragAndDropSchedule: FC = () => {
  const [assignments, setAssignments] = useState<{ [day: string]: Picture[] }>(
    {}
  );

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (over) {
      const day = over.id as string;
      const picture = emplPictures.find((pic) => pic.id === active.id);
      if (picture) {
        setAssignments((prev) => ({
          ...prev,
          [day]: prev[day] ? [...prev[day], picture] : [picture],
        }));
      }
    }
  };

  return (
    <DndContext collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
      <div style={{ display: "flex", marginBottom: "20px" }}>
        {emplPictures.map((pic) => (
          <DraggableImage key={pic.id} picture={pic} />
        ))}
      </div>

      <table style={{ width: "100%" }}>
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
              />
            ))}
          </tr>
        </tbody>
      </table>
    </DndContext>
  );
};

export default DragAndDropSchedule;