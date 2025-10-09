import type { Metadata } from "next";
//can find font names in google by searching google fonts
import { Geist, Geist_Mono } from "next/font/google"; // Importing Geist and Geist_Mono fonts
import "./globals.css"; // Importing global styles, connects to the globals.css file in the same folder
import "./myhome/style.css"; // Importing local styles css
import { Header } from "@/components/Header";
import { Providers } from "@/components/Providers";

//adding fonts from next/font/google
// it includes global styles and font settings
const geistSans = Geist({
  subsets: ["latin"],
  variable: "--font-geist-sans",
  display: "swap",
  // loading normal font then swaping to the mono font
  // can add other options like weight, style, etc.
  // weight: ["400", "500", "600", "700"], // you can specify the weights you want to use
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  // weight: ["400", "500", "600", "700"], // you can specify the weights you want to use
  subsets: ["latin"],
  display: "swap", //loading normal font then swaping to the mono font
});

//work with SEO
// adding Metadata for the application
export const metadata: Metadata = {
  title: "X App",
  // title: { template: "%s - X App", default: ""}, // adding a template for the title
  description: "Front-end insights, styled like X.com",
  //can add other tags like keywords, authors, etc., See list of tegs--> ctr + space
};

// RootLayout component that wraps the application
// This is the main layout file for the Next.js application
//Next automatically don't add HTML and body tags, so we need to add them manually in the RootLayout component, at least we need to have 1 RootLayout component in the app directory
export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`} //adding font to your app body
        // className={`${geistSans.className} ${geistMono.variable} antialiased`}
        //className={geistSans.className}
      >
        {/* you need to wrap it to Providers if you add some components as Header, contexts, theme,etc.  */}
        <Providers>
          <Header />
          {children}{" "}
          {/* Rendering the children components inside the body, children - it is a certain page from our application */}
        </Providers>
      </body>
    </html>
  );
}
