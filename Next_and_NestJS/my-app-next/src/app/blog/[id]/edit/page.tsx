import NewPostForm from "@/components/NewPostForm";
import { blogPosts } from "@/shared/data/blogposts.data";
import { revalidatePath } from "next/cache";
import { redirect } from "next/navigation";

type Props = {
  params: {
    id: string;
  };
};

async function updatePost(data: FormData) {
  "use server";

  const { title, body, id } = Object.fromEntries(data);

  // const response = await fetch(`http://localhost:3300/posts/${id}`, {
  //   method: "PATCH",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ title, body }),
  // });

  // const post = await response.json();

  const postIndex = blogPosts.findIndex((post) => post.id === Number(id));
  if (postIndex !== -1) {
    //if post found
    blogPosts[postIndex] = {
      ...blogPosts[postIndex],
      title: title as string,
      body: body as string,
    };
  }

  //helpers to revalidate the cache and redirect to the updated post page
  //revalidate updated post page and redirect to it
  revalidatePath(`/blog/${id}`); // Revalidating the /posts/[id] path to show the updated post
  redirect(`/blog/${id}`); // Redirecting to the updated post page
}

export default function Edit({ params: { id } }: Props) {
  const post = blogPosts.find((post) => post.id === Number(id)); // Find the post with the matching ID from the static data

  return (
    <div
      style={{
        border: "1px solid white",
        width: "400px",
        margin: "0 auto",
        padding: "15px",
      }}
    >
      <h1>Edit Post</h1>
      <br />
      <h1 style={{ marginBottom: "20px" }}>Profile of {post?.title}</h1>
      <hr />
      <form className="form" action={updatePost}>
        <input
          style={{ marginBottom: "10px" }}
          type="text"
          placeholder="title"
          required
          name="title"
          defaultValue={post?.title}
        />

        <textarea
          style={{ minHeight: "400px", width: "100%" }}
          placeholder="content"
          required
          name="body"
          defaultValue={post?.body}
        />
        <input type="hidden" name="id" value={post?.id} />
        {/* use hidden input to use it later */}
        <div>
          <input
            style={{
              backgroundColor: "blue",
              border: "1px solid white",
              padding: "5px",
              color: "white",
              cursor: "pointer",
              borderRadius: "5px",
              marginTop: "10px",
              width: "100%",
              fontWeight: "bold",
              fontSize: "16px",
              boxShadow: "2px 2px 5px rgba(0, 0, 0, 0.3)",
              transition: "background-color 0.3s, color 0.3s",
              textAlign: "center",
            }}
            type="submit"
            value="Update post"
          />
          {/* button to sen data to Server actions -> updatePost */}
        </div>
      </form>
    </div>
  );
}
