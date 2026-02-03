"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

export default function ErrorWrapper({ error }: { error: Error }) {
  //receive error as a prop
  return (
    <div>
      <h2 className="post-header">Error Page</h2>
      <p>Something went wrong. Please try again later.</p>
      <p>Error details: {error.message}</p>
    </div>
  );
}
