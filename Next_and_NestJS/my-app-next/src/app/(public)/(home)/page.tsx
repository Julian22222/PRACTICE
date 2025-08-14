import Image from "next/image";
import { Tweet } from "./Tweet";
import { text } from "stream/consumers";
import { Tweets } from "@/shared/data/tweets.data";

export default function Home() {
  return (
    <div className="min-h-screen flex justify-center px-4 py-8">
      <div className="w-full max-w-xl">
        <h1 className="text-3xl font-bold mb-6">Home</h1>
        <div className="space-y-3">
          {Tweets.map((tweet, index) => (
            <Tweet key={tweet.author} tweet={tweet} />
          ))}
        </div>
      </div>
    </div>
  );
}
