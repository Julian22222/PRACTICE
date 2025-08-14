import { PAGES } from "@/config/pages.config";
import { ITweet } from "@/shared/types/tweet.interface";
import Image from "next/image"; // Ensure you import Image from next/image
import Link from "next/link";

interface Props {
  tweet: ITweet;
}

export function Tweet({ tweet }: Props) {
  //{tweet} from props,line 4
  return (
    <div className="border border-white/10 rounded-xl p-4 bg-black text-white shadow-md">
      <div className="flex items-center gap-3 mb-2">
        <Image src="/x-logo.svg" alt="X Logo" width={24} height={24} />
        {/* // Image tag works as <img /> tag in HTML, Using priority to load the logo image faster, will load image first, can't use priority for all images!!! It will cause additional load on server, will cause latency */}
        {/* Image width and height is mandatory */}
        <Link href={PAGES.PROFILE(tweet.author)} className="font-semibold">
          {/* href={PAGES.PROFILE(tweet.author)} --> uses object from config/pages.config.ts, defines the URL paths  */}
          @{tweet.author}
        </Link>
        <p className="text-white/90">{tweet.text}</p>
      </div>
    </div>
  );
}
