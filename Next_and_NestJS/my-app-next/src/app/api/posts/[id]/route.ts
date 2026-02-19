export async function DELETE(
  request: Request,
  { params }: { params: { id: string } }
) {
  //get id from params to delete specific post

  const id = params.id;

  const response = await fetch(
    `https://jsonplaceholder.typicode.com/posts/${id}`,
    {
      method: "DELETE",
    }
  );

  if (response.ok) {
    return new Response(id, { status: 204 }); // returnn 204 status code for successful deletion with id of deleted post
  } else {
    return new Response("Failed to delete the post", { status: 500 });
  }
}
