"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

import Link from "next/link";
import { useState } from "react";
import { Me } from "./Me";

export default function page() {
  return (
    <div>
      <Me />

      <Link href="/" className="posts-link">
        Go to Home
      </Link>
      <br />
      <Link className="posts-link" href="/user/1234">
        Products Page
      </Link>
    </div>
  );
}
