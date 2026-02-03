import React, { FC, useState } from "react";
import { IMessage } from "../Types/types";

interface IChatModalProps {
  setChatModal: (isOpen: boolean) => void;
  messages: IMessage[];
  setMessages: (messages: IMessage[]) => void;
}

const header = "Work Chat";

const ChatModal: FC<IChatModalProps> = ({
  setChatModal,
  messages,
  setMessages,
}) => {
  const [newMessage, setNewMessage] = useState<IMessage>({
    sender: "",
    content: "",
    timestamp: new Date().toISOString(), // default to current time
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault(); // prevent default form submission behavior

    setMessages([...messages, newMessage]); // append newMessage to messages array

    setChatModal(false);
  };

  return (
    <div className="modal-container">
      <div
        className="modal"
        style={{
          border: "whitesmoke 1px solid",
          padding: "0px 20px",
          paddingBottom: "20px",
          boxShadow: "2px 2px 6px white",
        }}
      >
        <div className="modal-header">
          <h3>{header}</h3>
          <button className="modal-close" onClick={() => setChatModal(false)}>
            X
          </button>
        </div>
        <form onSubmit={handleSubmit}>
          <div className="label-input">
            <label htmlFor="sender" className="label-modal-chat">
              Sender
            </label>
            <select
              id="sender" // links this <label> tag with select with id="sender"
              className="modal-sender"
              required
              value={newMessage.sender}
              onChange={(e) =>
                setNewMessage({ ...newMessage, sender: e.target.value })
              } // update state on change
            >
              <option value="John Doe">John Doe</option>
              <option value="Tom Smith">Tom Smith</option>
              <option value="Mike Johnson">Mike Johnson</option>
              <option value="Chris Brown">Chris Brown</option>
            </select>
          </div>
          <br />

          <textarea
            value={newMessage.content}
            onChange={(e) =>
              setNewMessage({ ...newMessage, content: e.target.value })
            }
            className="modal-textarea-chat"
            placeholder="Type your message here..."
            rows={5}
            required
          ></textarea>

          <br />
          <br />
          <button className="submit-btn" type="submit">
            Submit
          </button>
        </form>
      </div>
    </div>
  );
};

export default ChatModal;
