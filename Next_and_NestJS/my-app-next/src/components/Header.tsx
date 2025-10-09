import Link from "next/link";
import Image from "next/image";
import { Navigation } from "./Navigation";

export function Header() {
  //can get some data from Database, like user data, etc. and then pass it to the Navigation component as props
  const navItems = [
    { href: "/", label: "Home" },
    { href: "/products", label: "Products" },
    { href: "/posts", label: "Posts" },
    { href: "/posts2", label: "Posts2" },
    { href: "/posts3", label: "Posts3" },
    { href: "/myhome", label: "Page" },
    { href: "/blog", label: "Blog" },
  ];

  return (
    <header className="border-b border-white/10 px-6 py-4 flex items-center justify-between bg-black">
      <Link href="/" className="flex items-center gap-3">
        <Image src="/x-logo.svg" alt="X Logo" width={28} height={28} priority />
        {/* // Image tag works as <img /> tag in HTML, Using priority to load the logo image faster, will load image first, can't use priority for all images!!! It will cause additional load on server, will cause latency */}
        {/* Image width and height is mandatory */}
      </Link>

      <Navigation navLinks={navItems} />
      {/* <nav className="flex gap-6 text-white/80">
        <Link href="/">Home</Link>
        <Link href="/flights">Flights</Link>
        <Link href="/products">Products</Link>
        <Link href="/posts">Posts</Link>
        <Link href="/myhome">Page</Link>
      </nav> */}
    </header>
  );
}

//Different componet example:
// const Header  =() => {
//     return (<header>
//         <p>Hello World</p>
//         <strong>header component</strong>
//         </header>)
// }

// export default Header
