# Microservices

We will take as example my - BANK application

# 🧠 There are actually 3 common ways to connect services

```JS
🟢 1. Simple HTTP (most common starting point)

Your main app calls chat service like:

POST http://chat-service/messages

👉 Pros:

-simple
-easy to debug
-no extra infrastructure
👉 This is how most systems start

🔵 2. WebSockets (for real-time chat)

Chat service exposes:

-WebSocket server
-Main app doesn’t “call” it directly:

frontend connects to chat service
👉 Very common for chat systems

🟣 3. Message brokers (advanced)

Using:
-RabbitMQ
-Apache Kafka

👉 Used when:
-high scale
-async processing
-event-driven systems

Example:
“message sent” event → notify other services


// “Microservices = must use Kafka”
// ❌ Wrong
// 👉 You only introduce message brokers when:
// you need async processing
// or scaling demands it



RabbitMQ / Kafka send messages/data between your main application and chat application
👉 Yes — but only in a specific way

They don’t replace normal API calls, and they’re not the primary way chat messages themselves flow.

🧠 What RabbitMQ / Apache Kafka actually do

They are used for:
👉 asynchronous communication via events

Not:
👉 direct request/response like REST




// 🧠 Simple analogy
// HTTP/WebSocket = phone call (instant answer)
// RabbitMQ/Kafka = email/newsletter (you react later)

//RabbitMQ / Apache Kafka can pass data between services
// ⚡ When you actually need RabbitMQ / Kafka
// Use them when:
// -systems must be decoupled
// -you don’t need immediate response
// -you have multiple consumers of the same event
// -high scalability is required

// 🚫 When NOT to use them
// Don’t use for:
// -sending chat messages
// -fetching conversations
// -basic CRUD
👉 That’s what APIs are for
```

```JS
🔄 Two types of communication (very important)

🟢 1. Synchronous (normal flow)

Used when you need an immediate response.

Example:

Frontend → Chat Service → save message → return OK

👉 Uses:
    - HTTP (REST)
    - or WebSockets

🟣 2. Asynchronous (event-driven)

- Used for side effects.

Example:
Chat Service → "message_sent" event → RabbitMQ/Kafka → other services react
```

```JS
🔥 Real example in your system

User sends a message

-Step 1 (main flow)

Frontend → Chat Service (HTTP/WebSocket)
           → save message in chat DB

👉 NO RabbitMQ/Kafka here


-Step 2 (optional events)
Chat service publishes event:

"message_sent"

Then:

Notification service → sends email/SMS
Analytics service → logs activity
Audit service → records action
👉 THIS is where RabbitMQ / Apache Kafka are used
```

💡 One more subtle point.
When you split services:

👉 You lose JOINs across services
So instead of:

JOIN customers + messages
You do:

- API call to get customer
- API call to get messages

```JS
//Recommended architecture (practical)
Bank/
  bankapp (Next.js)
  bank-api (Nest.js - main API / gateway)
  chat-service (Nest.js microservice)

// Flow:
// Frontend → bank-api
// bank-api → chat-service (HTTP or messaging)
// chat-service handles WebSockets for real-time chat



// User identity sync problem
// Your chat-service “doesn’t manage users” — good.
// But then: 👉 How does it know users exist?

// You need one of these:

// -Option A (simplest)
//     -chat-service trusts JWT only
//     -extracts userId from token
//     -does NOT store users locally
// -Option B (better for scale)
//     -bank-api emits events:
//         -UserCreated
//         -UserDeleted
// -chat-service maintains its own lightweight user table
// 👉 This avoids calling bank-api constantly.
```

# Overall microservice structure

1. You need to create separate/independent folder – for microservice project.

- Microservice - is its own Nest.js app. separate application (e.g. another NestJS app)

2. Then create Nest.js application –( BankChat-api) using HTTP calls, WebSockets (Nest.js supports this), Or libraries like:Socket.IO and Async communication (events) messengers (RabbitMQ, Apache Kafka)

Think of chat-service as: Its own mini-product or application With:

- its own API
- its own DataBase

```JS
Each microservice should have its own DB.

Example:

bank-api → PostgreSQL (accounts, transactions)
chat-service → MongoDB or PostgreSQL (messages)
👉 Never share the same DB between services.
```

- its own deployment pipeline (Independently deployable)

```JS
You should be able to:
- deploy chat-service without touching bank-api

it means:
-You can deploy chat-service without redeploying bank-api
-If chat crashes, banking still works
-You can scale chat independently (e.g., more instances)
// 👉 This is the real goal.
```

- its own scaling
- Loosely coupled to other services

```JS
A service can change, deploy, or even fail without breaking other services

✅ Tightly coupled - If changing your chat-service forces you to edit and redeploy bank-api, they are tightly coupled—even if they’re in different folders.

✅ Loose coupling - actually looks like-  Interaction only through contracts (APIs)

Your services should only know each other through well-defined interfaces, not internal code.


❌ Tight coupling
// bank-api imports chat-service code directly
import { ChatService } from '../chat-service';

✅ Loose coupling
POST /chat/message

- bank-api doesn’t know how chat works internally
- It only knows: “I send this request, I get this response”
- chat-service owns its DB
- Others must go through its API

You can change one service without breaking others


// For Example: you change ->
// message schema
// internal logic
// database type (Postgres → MongoDB)

✅ If loosely coupled:
    -No other service breaks

❌If tightly coupled:
    -Everything breaks 😄
    👉 If multiple services touch the same DB → they are tightly coupled.


Failure isolation: If chat-service goes down:
❌ Tight coupling
    · bank-api crashes or blocks

✅ Loose coupling
    · banking still works
    · chat features degrade gracefully



🔥 Concrete example (your project)

❌ Tightly coupled version (bad)
-bank-api:
    -writes directly to chat DB
    -imports chat models
-chat-service:
    -depends on bank DB
👉 This is basically one system split artificially.

✅ Loosely coupled version (good)
-bank-api:
    -handles users, auth
    -calls chat-service via API
-chat-service:
    -handles messages only
    -validates JWT
    -owns its DB
👉 Each service = independent unit



⚖️ Trade-offs (important)
Loose coupling is not “free”. You get your Pros and cons.

✅ You gain:
    · flexibility
    · scalability
    · independent deployment

❌ You lose:
    · simplicity
    · debugging ease
    · performance (network calls)
```

- Communicating over network (HTTP, messaging, etc.)

```JS
It exposes its own API (HTTP or otherwise)

Instead of direct HTTP calls:
    · bank-api emits event: UserCreated
    · chat-service listens and creates chat profile

Tools:
    · RabbitMQ
    · Apache Kafka

// “Uses HTTP methods” → not the full picture
// Your chat-service can expose REST endpoints like:
// POST /messages
// GET /conversations


// But for a chat system, HTTP alone is not enough.
// 👉 You’ll almost certainly need real-time communication via:
// WebSockets
// or libraries like Socket.IO
// Otherwise, users would need to constantly refresh to see new messages (bad UX).


connect microservices using HTTP
That’s fine to start, but microservices often evolve to:

HTTP (simple, synchronous)
or messaging via:
-RabbitMQ
-Apache Kafka

Example:
bank-api emits UserCreated
chat-service automatically creates chat profile
```

# Communication patterns

1. HTTP

```JS
//❌ this works but it is not the best option, When bankapp(Frontend - your Next.js) call api of your services
bankapp → calls bank-api
bankapp → calls chat-service


//✅ Better approach. When your Nest.js uses api of your services. This keeps frontend cleaner/simpler and centralizes logic, Easier to evolve later.
bankapp → bank-api
bank-api → chat-service

// Schema:
// Frontend(bankapp)
//    → bank-api (API Gateway)
//           → chat-service

    // API Gateway (optional but useful)
    //     -Instead of frontend calling multiple services:
    //     -Use an API gateway (could be your bank-api or a separate layer)


// API Gateway responsibilities
// Your bank-api should not just “forward requests”.

// It should handle:
// - Authentication (JWT issuing)
// - Authorization (who can message whom)
// - Request validation
// - Rate limiting (important for chat spam)
// - Aggregation (combine data from services)
// 👉 Think of it as the brain, not just a router.
```

# 🧩 High-level architecture

```JS
bankapp (Next.js frontend)
        ↓
bank-api (Nest.js - API Gateway / main backend) -> Bank database
        ↓
chat-service (Nest.js microservice)
        ↓
chat database

// bank-api = handles auth, users, accounts
// chat-service = handles messages, conversations, real-time
```

# 🚀 main.ts (HTTP + WebSocket)

```JS
async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  app.enableCors();

  await app.listen(3002); // chat-service runs separately
}
bootstrap();
```

```JS
💬 chat.controller.ts (REST)

@Controller('chat')
export class ChatController {
  constructor(private chatService: ChatService) {}

  @Get('conversations/:userId')
  getUserConversations(@Param('userId') userId: string) {
    return this.chatService.getConversations(userId);
  }

  @Post('message')
  sendMessage(@Body() dto: SendMessageDto) {
    return this.chatService.sendMessage(dto);
  }
}
```

```JS
// ⚡ chat.gateway.ts (real-time)

Uses Socket.IO under the hood (default in NestJS).

@WebSocketGateway({
  cors: true,
})
export class ChatGateway {
  @WebSocketServer()
  server: Server;

  handleConnection(client: Socket) {
    console.log('User connected:', client.id);
  }

  @SubscribeMessage('send_message')
  handleMessage(client: Socket, payload: any) {
    // broadcast to recipient
    this.server.to(payload.to).emit('receive_message', payload);
  }
}


// 🧠 chat.service.ts
@Injectable()
export class ChatService {
  async getConversations(userId: string) {
    // fetch from DB
  }

  async sendMessage(dto: SendMessageDto) {
    // save message to DB
    // optionally notify via gateway
  }
}
```

```JS
🔐 2. Auth between services

You don’t want chat-service to manage users.

Flow:

User logs in via bank-api
Gets JWT
Sends JWT to chat-service
chat-service verifies it
jwt.guard.ts

@Injectable()
export class JwtAuthGuard implements CanActivate {
  canActivate(context: ExecutionContext): boolean {
    const request = context.switchToHttp().getRequest();
    const token = request.headers.authorization;

    // verify JWT (same secret as bank-api)
    return true;
  }
}

🔗 3. bank-api → chat-service communication

Option A: HTTP (simple, recommended first)

Use NestJS HTTP module:

@Injectable()
export class ChatClient {
  constructor(private http: HttpService) {}

  async sendMessage(data: any) {
    return this.http.post('http://chat-service:3002/chat/message', data);
  }
}

Option B: Message queue (advanced)

Using:

RabbitMQ
Example idea:

client.emit('message_sent', payload);

chat-service listens:

@EventPattern('message_sent')
handleMessage(data: any) {
  // process async
}

🌐 4. Frontend (Next.js)

Real-time connection

import { io } from "socket.io-client";

const socket = io("http://localhost:3002");

socket.emit("send_message", {
  to: "user2",
  message: "Hello",
});

socket.on("receive_message", (msg) => {
  console.log(msg);
});

🚀 5. Docker (important for microservices)

Each service runs independently.

docker-compose.yml

version: '3.8'

services:
  bank-api:
    build: ./bank-api
    ports:
      - "3001:3001"

  chat-service:
    build: ./chat-service
    ports:
      - "3002:3002"

  chat-db:
    image: mongo
    ports:
      - "27017:27017"

🧠 Key takeaways (this is the real microservice part)

What makes this a true microservice:

✅ Separate Nest.js app
✅ Separate database
✅ Own API (REST + WebSocket)
✅ Independent deployment
✅ Communicates over network (HTTP or messaging)

⚠️ Common mistakes to avoid

❌ Sharing database between services
❌ Calling chat logic directly from bank-api code (tight coupling)
❌ No auth validation in chat-service
❌ Using only HTTP for chat (no real-time)


////////////////////////////////////////////////////////



🧩 Practical rules you can follow

If you remember nothing else, follow these:

No shared DB
No direct code imports between services
Communicate via HTTP or events
Design for failure (services can be down)
Each service owns one business domain
🧠 Simple mental model

Think of services like separate companies:

They don’t access each other’s databases
They don’t read each other’s code
They communicate via contracts (APIs/events)
One company failing doesn’t shut down the others
```

```JS
1. Service discovery / environment handling

Right now you’re hardcoding:

http://chat-service:3002

That works locally (Docker), but in real environments you’ll want:

environment-based URLs (process.env.CHAT_SERVICE_URL)
or service discovery (Kubernetes, Docker DNS, etc.)

👉 Otherwise deployments become painful.

2. API Gateway responsibilities (this is big)

Your bank-api should not just “forward requests”.

It should handle:

Authentication (JWT issuing)
Authorization (who can message whom)
Request validation
Rate limiting (important for chat spam)
Aggregation (combine data from services)

👉 Think of it as the brain, not just a router.

3. WebSocket auth (very commonly missed)

You mentioned JWT validation for HTTP — good.

But WebSockets ALSO need auth.

In your gateway:

handleConnection(client: Socket) {
  const token = client.handshake.auth.token;
  // validate JWT
}

👉 Otherwise anyone can connect and send messages.

4. User identity sync problem

Your chat-service “doesn’t manage users” — good.

But then:

👉 How does it know users exist?

You need one of these:

Option A (simplest)
chat-service trusts JWT only
extracts userId from token
does NOT store users locally
Option B (better for scale)
bank-api emits events:
UserCreated
UserDeleted
chat-service maintains its own lightweight user table

👉 This avoids calling bank-api constantly.

5. Message delivery guarantees (very important for chat)

Right now your design is:

send → save → emit via WebSocket

But what if:

user is offline?
socket disconnects?
service crashes mid-send?

You need:

persistence first (DB write before emit)
message status:
sent
delivered
read

👉 Otherwise messages can silently disappear.

6. Scaling WebSockets (this is where things break)

Single instance works fine.

Multiple instances → problem:

user connected to instance A
message handled by instance B

👉 message never reaches user

Solution:

Redis adapter for Socket.IO
or message broker (RabbitMQ / Kafka)

Example:

Socket.IO + Redis adapter

👉 This is mandatory if you scale horizontally.

7. Event-driven communication (you mentioned it, but it’s key)

You can start with HTTP:

bank-api → chat-service

But for real systems:

Use events for:

UserCreated
UserBlocked
TransactionCompleted → trigger notifications

👉 This decouples services even more.

8. Error handling & fallback strategy

You mentioned failure isolation — good.

Now make it real:

If chat-service is down:

bank-api should NOT crash
return:
partial response
or "chat unavailable"

Example:

try {
  await chatClient.sendMessage(...)
} catch (e) {
  // log only, don't break banking
}
9. Data modeling for chat (often underestimated)

Think early about:

conversations (1–1 vs group)
message pagination
indexing (very important for performance)
attachments (images/files)

👉 Chat grows fast — bad schema = pain later.

10. Security (critical for banking app)

You need more than basic JWT:

rate limiting (prevent spam / abuse)
message size limits
input sanitization (XSS)
audit logging (who sent what)

👉 Especially important in financial apps.

⚖️ One thing I’d push back on

“Never share the same DB between services”

That’s correct in principle, but:

👉 Early-stage projects sometimes overdo microservices.

If your app is still small:

microservices add complexity
debugging becomes harder
deployment overhead increases

Honest advice:

Your design is good
But only go full microservices if you actually need:
independent scaling
team separation
high traffic

Otherwise, a modular monolith can be enough.

🧠 Final refined architecture
Frontend (Next.js)
   ↓
bank-api (Nest.js - API Gateway)
   - auth
   - rate limiting
   - orchestration
   ↓
chat-service (Nest.js)
   - REST API
   - WebSocket gateway
   - message persistence
   - JWT validation
   ↓
chat DB

+ Redis (for WebSocket scaling)
+ (optional) RabbitMQ/Kafka (events)
🔥 Bottom line

You’re 90% there. The real upgrades you need:

WebSocket authentication
Redis adapter for scaling
Event-driven communication (not just HTTP)
Proper failure handling
Thoughtful message persistence design
```
