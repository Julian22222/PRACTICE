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

//The next.config.ts (or next.config.js if you're using JavaScript) file in a Next.js project is basically the central place where you configure and customize how Next.js behaves. It's like a config hub that lets you tweak many things about your app beyond the defaults.
//Main purposes of next.config.ts:
// 1. Customizing the Build & Runtime Behavior
// You can modify how Next.js builds and serves your app. For example, enabling or disabling features like:
// - Image Optimization settings (domains allowed for external images, device sizes, loader options)
// - Custom webpack configuration to add or tweak webpack plugins/loaders
// - Environment variables (though .env files are preferred nowadays)
// - Internationalization (i18n) settings
// 2. Enabling Experimental Features or Opting Into New Behavior
//Next.js often releases experimental features or new options that need to be enabled via this file.
// 3. Redirects, Rewrites, and Headers
//You can define URL redirects, rewrites, or custom headers that Next.js will use when serving requests.
// 4. Customizing Static Export Settings
// If you're using next export, you can control certain behaviors in this config.
// 5. Enabling TypeScript Support in the Config Itself
// Using .ts extension allows you to write the config file in TypeScript, which means you get type checking and autocomplete.

// Example snippet from a next.config.ts:
// import { NextConfig } from 'next';

// const nextConfig: NextConfig = {
//   reactStrictMode: true,
//   images: {
//     domains: ['example.com'], // allow loading images from external domains
//   },
//   async redirects() {
//     return [
//       {
//         source: '/old-route',
//         destination: '/new-route',
//         permanent: true,
//       },
//     ];
//   },
// };

// export default nextConfig;

//TL;DR:
//next.config.ts is where you tell Next.js how you want your app to behave, from image optimization and routing tweaks to enabling advanced features or customizing the build.

/////////////////////////////////////////////////////////////////////////////

//why we need next.config.ts file what is main purpose ?
//The next.config.ts file is a configuration file for Next.js applications. It allows developers to customize and optimize various aspects of their Next.js projects. Here are some of the main purposes and features of the next.config.ts file:
//1. Customization: Developers can customize the behavior of their Next.js application by modifying settings such as page extensions, asset prefixes, and more.
//2. Environment Variables: The file can be used to define environment variables that can be accessed throughout the application, allowing for different configurations based on the environment (development, production, etc.).
//3. Performance Optimization: Next.js provides various performance optimization options that can be configured in this file, such as image optimization, font optimization, and more.
//4. Build Configuration: Developers can customize the build process, including settings for webpack, Babel, and other build tools.
//5. Internationalization (i18n): The file can be used to configure internationalization settings, such as supported locales and default locale.
//6. Redirects and Rewrites: Developers can define custom redirects and rewrites for their application, allowing for better URL management and SEO.
//7. Middleware Configuration: The file can be used to configure middleware settings for the application.
//8. Experimental Features: Next.js often introduces experimental features that can be enabled or disabled through the next.config.ts file.
//Overall, the next.config.ts file is a powerful tool for developers to tailor their Next.js applications to meet specific requirements and improve performance.

//Note: In newer versions of Next.js, the configuration file can also be named next.config.mjs or next.config.js, depending on the module system being used. The .ts extension indicates that the file is written in TypeScript.
