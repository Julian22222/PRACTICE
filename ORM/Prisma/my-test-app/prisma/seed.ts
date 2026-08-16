import { PrismaPg } from '@prisma/adapter-pg';
import { PrismaClient } from 'generated/prisma/client.js';

const adapter = new PrismaPg({
  connectionString: process.env.DATABASE_URL,
});

const prisma = new PrismaClient({
  adapter,
});

const userData = [
  {
    name: 'Alice',
    email: 'alice@example.com',
    posts: {
      create: [
        {
          title: 'Hello World',
          content: 'This is my first post',
        },
        {
          title: 'My second post',
          content: 'This is my second post',
        },
      ],
    },
  },
  {
    name: 'Bob',
    email: 'bob@example.com',
    posts: {
      create: [
        {
          title: 'My first post',
          content: "This is Bob's first post",
        },
      ],
    },
  },
];

export async function main() {
  console.log(`Start seeding ...`);
  for (const u of userData) {
    const user = await prisma.user.create({
      data: u,
    });
    console.log(`Created user with id: ${user.id}`);
  }
  console.log(`Seeding finished.`);
}

main()
  .catch((e) => {
    console.error(e);
    process.exit(1);
  })
  .finally(async () => {
    await prisma.$disconnect();
  });
