"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

import Link from "next/link";
import { useState } from "react";

export default function page() {
  return (
    <div>
      <p>page</p>
      <Link href="/">Home Page</Link>
      <br />
      <Link href="/user/1234">Products Page</Link>
    </div>
  );
}
