// This file defines the URL paths for different pages in the application.
// It exports an object with methods that return the URL for specific pages.

export const PAGES = {
  PROFILE: (username: string) => `/user/${username}`, //PROFILE takes a username parameter and returns the URL for the user profile page
};
