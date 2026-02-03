"use client";
import { useFormStatus } from "react-dom";

export default function SearchButton() {
  const status = useFormStatus(); // it is spe1cial hook to get the form submission status, it has pending value, if pending = true when submitting the form or false when not submitting

  return (
    <button type="submit">{status.pending ? "Searching..." : "Search"}</button> // Change button text based on form submission status. If form is being submitted (pending = true), show "Searching...", otherwise show "Search"
  );
}
