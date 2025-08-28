//This is Layout component for all public pages
//Admin pages will have a different layout
// This is the main layout file for the Next.js application

import { Header } from "@/components/Header";
import { PropsWithChildren } from "react";

export default function Layout({ children }: PropsWithChildren<unknown>) {
  return (
    <div>
      {/* <Header /> */}
      {children}
    </div>
  );
}
