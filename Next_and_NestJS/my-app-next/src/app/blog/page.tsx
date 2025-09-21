interface Props {}
import { Ipost } from "@/shared/types/posts.interface";
import { blogPosts } from "@/shared/data/blogposts.data";
import NewPostForm from "@/components/NewPostForm";
import { revalidatePath } from "next/cache";

export default function page({}: Props) {
  return (
    <div>
      <a
        href="/blog/new"
        type="button"
        style={{
          background: "lightBlue",
          padding: "5px",
          borderRadius: "5px",
          marginLeft: "10px",
          textDecoration: "none",
          color: "black",
          display: "inline-block",
          marginBottom: "10px",
          marginTop: "10px",
          fontWeight: "bold",
          border: "2px solid black",
          boxShadow: "2px 2px 5px rgba(0, 0, 0, 0.3)",
          transition: "background-color 0.3s, color 0.3s",
          cursor: "pointer",
          textAlign: "center",
          width: "120px",
        }}
      >
        Add new post
      </a>
      {/* <br />
      <hr /> */}

      <NewPostForm
        onSuccess={async () => {
          //make component behave differently than NewPostForm from blog/new/page.tsx
          "use server";
          revalidatePath("/blog"); // Revalidating the /posts path to show the newly added post
          //revalidatePath will re-fetch the data from the server and update the cache for the specified path
          //revalidate will allow to see the new post in the posts list page without refreshing the page
        }}
      />
      <br />
      <hr style={{ width: "80vh", margin: "0 auto" }} />
      <br />
      <ul
        style={{
          display: "flex",
          justifyContent: "center",
          flexDirection: "column",
          width: "400px",
          margin: "0 auto",
        }}
      >
        {blogPosts.map((post: Ipost) => (
          <li
            key={post.id}
            className="post-element"
            style={{
              border: "1px solid white",
              width: "400px",
              marginBottom: "10px",
              padding: "10px",
              position: "relative",
            }}
          >
            <h2>{post.title}</h2>
            <br />
            <br />
            <a
              href={`/blog/${post.id}`}
              className="posts-link"
              style={{
                background: "lightGreen",
                padding: "5px",
                textDecoration: "none",
                color: "black",
                borderRadius: "5px",
                position: "absolute",
                bottom: "10px" /* Distance from bottom */,
                right: "10px" /* Distance from right */,
              }}
            >
              Read More
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
}
