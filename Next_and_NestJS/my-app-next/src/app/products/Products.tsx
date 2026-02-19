"use client"; // This component is a client component, allowing it to use hooks like useRouter

import { useRouter } from "next/navigation";
import styles from "./products.module.css"; // Importing CSS module for styling
import Image from "next/image"; // Importing Image component from Next.js

export function Products() {
  const {} = useRouter(); // Using useRouter hook for navigation, though not used in this component

  return (
    <div>
      <h1 className={styles.products}>Hello from Products</h1>
      <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
    </div>
  );
}
