import PostClient from "./PostClient";
import "./post.css"; //import css file for styling

///////////////////////////////////////////////
// Dynamic metadata generation for each post page
export async function generateMetadata({
  params,
  searchParams,
}: {
  params: { id: string };
  searchParams: Record<string, string | string[]>;
}) {
  //receive 2 parameters
  const post = await getPost(params.id);
  return {
    //return metadata object
    title: `Post nr. ${post.id}`,
    description: post.body,
    //can add other metadata tags if you want
  };
}

async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );

  if (!result) {
    throw new Error("Post not found"); //use Error handling to throw an error if the post is not found, Error page will be shown with this message - error.tsx
  }

  return result;
}
//////////////////////////////////////////////////////

// params is a Promise — so we call .then() in a useEffect to resolve it.
// We store id in state — so it triggers the second useEffect for fetching.
// No async function component — avoids the original Next.js error

// interface Props {
//   params: Promise<{
//     id: string;
//   }>;
// }

export default function Post({ params }: { params: { id: string } }) {
  //{params} getting the params (id of the post) from the URL
  //   const id = { useParams };

  return (
    <div>
      <PostClient params={params} />
    </div>
  );
}
