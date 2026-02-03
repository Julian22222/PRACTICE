import React, { FC, useState } from "react";
import ChatModal from "./ChatModal";
import { IMessage } from "../Types/types";

const Chat: FC = () => {
  const [chatModal, setChatModal] = useState<boolean>(false);

  class TextBlock {
    constructor(
      public sender: string,
      public content: string,
      public timestamp: string
    ) {}
  }

  let arrayMessages: TextBlock[] = [
    new TextBlock(
      "John Doe",
      "I’ve taken a look at Porsche Panamera 2023 and wanted to give a quick update. We’ve identified a worn-out brake pad and a slight oil leak. I recommend we take care of this soon to avoid further damage or safety concerns.",
      "2025-06-01T10:00:00Z"
    ),
    new TextBlock(
      "Mike Johnson",
      "I’ve inspected BMW 5 tires and noticed that they're getting close to the recommended replacement mark. For your safety and better performance, I'd suggest replacing them this week.Need to check the availability of tires in our inventory.",
      "2025-06-01T10:05:00Z"
    ),
    new TextBlock(
      "Chris Brown",
      "I’ve checked the electrical system of Tesla Model S 2022 and everything looks good. No issues found, car is running smoothly. Car can be returned to the customer.",
      "2025-06-01T10:10:00Z"
    ),
    new TextBlock(
      "Tom Smith",
      "I’ve completed the oil change for Toyota Camry 2020. Everything is running smoothly now, Car can be returned to the customer.",
      "2025-06-01T10:15:00Z"
    ),
    new TextBlock(
      "Mike Johnson",
      "I noticed that Audi tires are getting worn out. Needs to be replaced soon for better performance and safety.",
      "2025-06-01T10:20:00Z"
    ),
  ];

  const [messages, setMessages] = useState<IMessage[]>([
    ...arrayMessages.map((msg) => ({
      sender: msg.sender,
      content: msg.content,
      timestamp: msg.timestamp,
    })),
  ]);

  const hadleChatModal = () => {
    setChatModal(!chatModal);
  };

  return (
    <div className="chat-container">
      <div className="left-flex-container">
        <h2 style={{ textAlign: "center", marginBottom: 20 }}>Chat</h2>
        <p style={{ textAlign: "center", color: "grey" }}>
          Welcome to the chat! Feel free to ask questions or share your
          thoughts.
        </p>
        <button className="add-message" onClick={() => hadleChatModal()}>
          Add message
        </button>
      </div>
      <div className="right-flex-container">
        {messages
          .slice()
          .reverse()
          .map((message, index) => {
            return (
              <div style={{ border: "1px solid grey", marginBottom: 10 }}>
                <div className="msg-chat-container">
                  <p className="msg-chat-left">{message.sender}</p>
                  <p className="msg-chat-right">
                    {message.timestamp.split("T")[1].slice(0, 5) +
                      " " +
                      message.timestamp
                        .slice(0, 10)
                        .split("-")
                        .reverse()
                        .join("/")}
                  </p>
                </div>

                <p style={{ padding: "0px 50px 0px 100px" }}>
                  {message.content}
                </p>
              </div>
            );
          })}
      </div>
      {chatModal && (
        <ChatModal
          setChatModal={setChatModal}
          messages={messages}
          setMessages={setMessages}
        />
      )}
    </div>
  );
};

export default Chat;
