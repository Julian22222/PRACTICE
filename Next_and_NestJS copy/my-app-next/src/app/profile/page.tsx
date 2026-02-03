import { authConfig } from "@/config/auth";
import { getServerSession } from "next-auth/next";

interface Props {}

export default async function Profile({}: Props) {
  const session = await getServerSession(authConfig); //it is from NextAuth library. we can use session data to show/hide links based on auth status. it will help to show is the user Authorized or not.
  //receive authConfig from config/auth.ts

  return (
    <div className="profile-header">
      <h1>Hello {session?.user?.name}</h1>
      {session?.user?.image && (
        <img
          src={session.user.image}
          alt={session.user.name || "User Image"}
          width={200}
          height={200}
        />
      )}
      {/* //if user has image, then only show the img tag */}
    </div>
  );
}
