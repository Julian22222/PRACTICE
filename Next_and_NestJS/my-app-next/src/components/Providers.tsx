"use client";

import { SessionProvider } from "next-auth/react";

//here we can keep different providers like Redux Provider, Theme Provider etc.
//we can wrap all providers here
//we can use this component in main layout.tsx to wrap the whole app with providers

export function Providers({ children }: { children: React.ReactNode }) {
  return <SessionProvider>{children}</SessionProvider>;
}
