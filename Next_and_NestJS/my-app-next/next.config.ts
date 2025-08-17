import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  // output: "export", // This option is used to export the application as a static site
  env: {}, //or don't use env here but use NEXT_PUBLIC_ prefix for public environment variables
  experimental: {
    optimizePackageImports: ["@material-ui/core"], // This option is used to optimize package imports, reducing bundle size
  },
};

export default nextConfig;
