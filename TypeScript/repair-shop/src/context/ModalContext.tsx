import { Children, createContext, useState } from "react";

//Interfaace to define the structure of the ModalContext
interface IModalContextProps {
  modal: boolean;
  openModal: () => void;
  closeModal: () => void;
}

//createContext method is used to create a context for managing modal state
export const ModalContext = createContext<IModalContextProps>({
  modal: false,
  openModal: () => {},
  closeModal: () => {},
});

export const ModalState = ({ children }: { children: React.ReactNode }) => {
  const [modal, setModal] = useState<boolean>(false); // state to manage modal visibility

  const openModal = () => {
    setModal(true);
  }; // function to open the modal

  const closeModal = () => {
    setModal(false);
  }; // function to close the modal

  return (
    <ModalContext.Provider value={{ modal, openModal, closeModal }}>
      {/* modal, open, close will be available for all children */}
      {/* this Children components will get access to all properties from ModelState */}
      {children}
    </ModalContext.Provider>
  );
};

// Then you can use this ModalState component to wrap your application or specific components where you want to use the modal context.

//For example: use it in index.tsx fle -->
//root.render(
//    <ModalState>
//    {/* // Wrap the App component with ModalState to provide modal context */}
//    <App />
//  </ModalState>
