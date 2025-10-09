type Mypost = {
  id: number;
  title: string;
};

interface Props {}

export default async function page({}: Props) {
  //we use json-server library to fetch data from local server
  const data = await fetch("http://localhost:3001/myposts");
  const myposts = await data.json();

  return (
    <>
      <h1>Local Server Page</h1>

      <ul>
        {myposts.map((post: Mypost) => (
          <li key={post.id}>
            <h2>{post.title}</h2>
          </li>
        ))}
      </ul>
    </>
  );
}
