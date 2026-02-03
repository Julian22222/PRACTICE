import Link from "next/link";

interface Props {
  id: string | null;
  post: {
    title: string;
    body: string;
  } | null;
}

export function Post_element({ id, post }: Props) {
  return (
    <div>
      <Link href="/posts" className="post-back-btn">
        Back
      </Link>
      <br />
      <br />
      <div className="post-container">
        {id && <p>Id: {id}</p>}
        {/* Displaying the post ID from the URL, name id should be the same as we have in -->[id] */}

        {post && (
          <>
            <p>Title: {post.title}</p>
            <p>Body: {post.body}</p>
          </>
        )}
      </div>
    </div>
  );
}
