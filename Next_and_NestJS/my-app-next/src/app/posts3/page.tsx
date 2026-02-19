import PostsComponent from "@/components/PostsComponent";
import SearchButton from "@/components/SearchButton";
import Form from "next/form"; //New in Next.js 15 - we can use Form tag from next/form to handle form submissions on the server

type Post = {
  id: number;
  title: string;
  body: string;
};

const baseUrl = "https://jsonplaceholder.typicode.com/posts?_limit=10";

export default async function PostPage({
  //will get the search params from the URL
  searchParams,
}: {
  searchParams: Promise<{ [key: string]: string | string[] | undefined }>;
}) {
  // get title parametr value from search params, if not present set it to an empty string
  //if we have ?title=something in the URL, we will get that value here
  //title will be used to filter the posts, if we have title in the search params
  //title comes from Form--> <input type="text" name="title" />
  const title = (await searchParams).title || ""; // Get the title from search params, if parametrs not present set it to an empty string

  console.log("Title from search params:", title);

  const url = title ? `${baseUrl}&title_like=${title}` : baseUrl; //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the baseUrl
  //title_like is a query param that jsonplaceholder API supports to filter posts by title
  //title_like does a partial match, so if we search for "sunt" it will return all posts that have "sunt" in the title
  //if we want exact match, we can use title=somevalue, but it will return only one post that matches the title exactly
  //title_like is case insensitive
  //title_like is a query param that jsonplaceholder API supports to filter posts by title

  const data: Promise<Post[]> = fetch(
    url //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the
    // baseUrl
    //filtering posts by title using title_like query param
    //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the baseUrl to get all posts
  ).then((res) => res.json());

  return (
    <>
      <h1>Here we use new approach with Form tag from Next.js 15</h1>

      {/* action="/posts3" <-- we stay on the same page, we don't do redirect to different page after Form submission*/}
      {/* by submission this Form we make URL ->  https://jsonplaceholder.typicode.com/posts +?title=somevalue*/}
      <Form action="/posts3">
        <input type="text" name="title" placeholder="Enter your name" />
        {/* <button type="submit">Search</button>  //<-- usual button tag*/}
        <SearchButton />
      </Form>

      <PostsComponent data={data} />
    </>
  );
}
