"use client"; //if we use usePathname

import Link from "next/link";
import { usePathname } from "next/navigation"; // Importing usePathname to get the current path

type NavLink = {
  href: string;
  label: string;
};

interface Props {
  navLinks: NavLink[];
}

export function Navigation({ navLinks }: Props) {
  const pathname = usePathname(); // Using usePathname to get the current URL path where the user is currently located

  return (
    <nav className="flex gap-6 text-white/80">
      {navLinks.map((link) => {
        const isActive = pathname === link.href; // Check if the current path matches the link's href

        return (
          <Link
            key={link.href}
            href={link.href}
            className={isActive ? "text-white font-bold" : ""}
          >
            {link.label}
          </Link>
        );
      })}
    </nav>
  );
}
