export async function GET(request: Request) {
  //query we get from the url, like ?q=batman
  //in this case i hardcode the query
  const query = "batman";

  const API_KEY = process.env.OMDB_SECRET; // Store your OMDB API key in an environment variable for security

  const movies = await fetch(
    `https://www.omdbapi.com/?apikey=${API_KEY}&s=${query}` //here we need API_KEY and query
  ).then((res) => res.json());

  return new Response(JSON.stringify(movies));
  //return NextResponse.json(movies); //you can also use this
}
