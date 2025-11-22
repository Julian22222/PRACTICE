import { blogPosts } from "@/shared/data/blogposts.data";
import { redirect } from "next/navigation";

//If we want our NewPostForm behave differently, based on where it is used, we can pass props to it
//NewPostForm will create onSuccess function, it will receive id as a prop from the parent component,
//this function will not return anything, if the function is async it will return a promise - Promise<void>
export default function NewPostForm({
  onSuccess,
}: {
  onSuccess: (id?: number) => Promise<void> | void;
}) {
  //we use createPostfunction inside the NewPostForm component, because it is tightly coupled with the form
  //createPost function won't get to Frontend, it will stay on the server

  //Server Action is async function that can be used to handle form submissions or other server-side logic
  async function createPost(data: FormData) {
    //we use form data to get the form input values, we receive the "data" from the form submission,
    //FormData - is a data type, FormData is a built-in web API that provides a way to easily construct a set of key/value pairs representing form fields and their values
    //data contains - title and body fields from the form input fields below -> {title: "post title", body: "post body"}

    //when we use Server Actions, we need to make sure that the function is async, because Server Actions always return a promise
    //when we use Server Actions we need indicate that it is  a server component, by adding "use server" at the top of the file
    "use server"; //adding this directive to indicate that this is a server component
    //can create separate file for server actions, and import it here, if we have multiple server actions
    // or create separate server action - and write "use server" at the top of each file, as we are doing here

    //getting form data values
    const { title, body } = Object.fromEntries(data); //converting form data to an object, and destructuring the object to get title and body values

    //make DB request, call an API, or perform any other server-side logic here
    // we can use PRISMA or direct DB queries here- using SQL query, or call an external API

    ///////////////////////////////////////////////////////////////////////////////////////
    //example of making a POST request to an API to create a new post
    // we are not using this code, because we are using in-memory data - blogPosts array

    // ❌ It is working example, 🔥 but it is better to interact with your database using Server Actions
    //   const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
    //     method: "POST",
    //     headers: {
    //       "Content-Type": "application/json",
    //     },
    //     body: JSON.stringify({
    //       title,c
    //       body,
    //       userId: 1, //hardcoded userId, in real app we will get it from the session
    //     }),
    //   });

    //   const post = await response.json(); //getting the newly created post from the response

    ///////////////////////////////////////////////////////////////////////////////////////

    blogPosts.push({
      userId: 1,
      id: blogPosts.length + 1,
      title: title as string,
      body: body as string,
    });

    const post = blogPosts[blogPosts.length - 1]; //getting the newly created post from the response

    //after posting new post, we can redirect user to another page, or revalidate the cache to show the new post in the posts list page
    await onSuccess?.(post.id); //calling the onSuccess function passed as a prop from the parent component, and passing the newly created post id to it
  }

  return (
    // form submission will be handled by the createPost function - which is a Server Action
    <form
      action={createPost} //handling form submission using the createPost - it is server action
      className="form"
      style={{
        border: "1px solid white",
        display: "flex",
        justifyContent: "center",
        flexDirection: "column",
        gap: "10px",
        padding: "10px",
        borderRadius: "5px",
        maxWidth: "400px",
        margin: "0 auto",
      }}
    >
      <input type="text" placeholder="title" required name="title" />
      <textarea placeholder="body" required name="body" />
      <div>
        <input type="submit" value="Add post" />
      </div>
    </form>
  );
}
