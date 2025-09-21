import NewPostForm from "@/components/NewPostForm";
import { redirect } from "next/navigation";

interface Props {}

export default function page({}: Props) {
  return (
    <div style={{ display: "flex", justifyContent: "center" }}>
      <h1>Create new post</h1>
      <NewPostForm
        onSuccess={async (id) => {
          "use server";
          redirect(`/blog/${id}`);
        }}
      />
      {/* passing new async function - onSuccess as a prop, id is passed as a parametr to the function */}
    </div>
  );
}
