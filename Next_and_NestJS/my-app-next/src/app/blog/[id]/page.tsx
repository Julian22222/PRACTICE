import { blogPosts } from "@/shared/data/blogposts.data";
import { revalidatePath } from "next/cache";
import Link from "next/link";
import { redirect } from "next/navigation";

interface Props {}

async function deletePost(id: string) {
  "use server"; // Marking the function to be a server action

  const postIndex = blogPosts.findIndex((post) => post.id === Number(id));
  //await fetch(`http://...`,{method:"DELETE", headers:{"Context-Type": "application/json"},}) <-- use this option to delete the data from database

  if (postIndex !== -1) {
    blogPosts.splice(postIndex, 1); // Remove the post from the static data array
    console.log(`Post with id ${id} deleted.`);
  }

  revalidatePath("/blog"); // Revalidating the /posts path to reflect the deleted post
  redirect("/blog"); // Redirecting to the /posts page after deletion

  //revalidatePath and redirect are used in in Next.js Server Actions or Route Handlers when you perform a mutation (like creating, updating, or deleting data) and want to control how the UI updates afterward.
  //Use revalidatePath(path) when you have changed data and you need to ensure the Next.js cache for a specific page is updated, so the next visitor (or the current user) sees the new data
  //This is essential after any server action that changes data that is displayed on another page
  //very common to use both together in a Server Action
  // Use redirect(url) when you want to navigate the user to a different page immediately after a server action is completed. This is useful for improving user experience by taking them to a relevant page after an operation, such as going back to the list view after deleting an item.
  //Revalidate: Clear the cache for the page(s) that display the changed data.
  //// Redirect: Navigate the user to a different page after the action is complete.
}

// export default function Post({ params: { id }}:Props) {
export default function Post({ params }: { params: { id: string } }) {
  const singlePost = blogPosts?.find((post) => post.id === Number(params.id)); // Find the post with the matching ID from the static data

  return (
    <div
      style={{
        border: "1px solid white",
        width: "400px",
        marginBottom: "10px",
        padding: "15px",
      }}
    >
      <p>Get Id form params: {params?.id}</p>
      <br />
      <hr />

      <div>
        <p>Id: {singlePost?.id}</p>
        <p>Title: {singlePost?.title}</p>
        <p>Body: {singlePost?.body}</p>

        <form action={deletePost.bind(null, params.id)}>
          {/* Binding the deletePost function to the form action, bind method allow you to pass correct post id*/}
          {/* null - is passing context in bind method,we don't have any context therefore it is null */}
          {/* params.id - is a string  */}
          <input
            type="submit"
            value="Delete Post"
            style={{
              background: "red",
              color: "white",
              padding: "5px",
              borderRadius: "5px",
              border: "none",
              cursor: "pointer",
            }}
          />
        </form>
        <br />
        <Link
          href={`/blog/${params.id}/edit`}
          style={{
            background: "blue",
            color: "white",
            padding: "5px",
            borderRadius: "5px",
            border: "none",
            cursor: "pointer",
          }}
        >
          Edit Post
        </Link>
      </div>
    </div>
  );
}
