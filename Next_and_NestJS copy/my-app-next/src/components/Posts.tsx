import Link from "next/link";

type Post = {
  id: number;
  title: string;
  body: string;
};
interface Props {
  posts: Post[];
}

export function Posts({ posts }: Props) {
  return (
    <div>
      <ul>
        {posts.map((post) => (
          <li key={post.id} className="post-element">
            <h2 style={{ color: "black" }}>{post.title}</h2>
            <p>{post.body}</p>
            <Link href={`#`} className="posts-link">
              Read More
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
