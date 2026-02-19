import Link from "next/link";

type Props = {
  data: Promise<Post[]>;
};

type Post = {
  id: number;
  title: string;
  body: string;
};

export default async function PostsComponent({ data }: Props) {
  return (
    <div>
      <ul>
        {(await data).map((post: Post) => (
          <li key={post.id} className="post-element">
            <h2 style={{ color: "black" }}>{post.title}</h2>
            <p>{post.body}</p>
            <Link href={`/posts/${post.id}`} className="posts-link">
              Read More
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
