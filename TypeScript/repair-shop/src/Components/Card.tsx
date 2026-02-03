import React, { FC, useState } from "react";
import "../Styles/App.css";

export enum CardVariant {
  outlined = "outlined",
  primary = "primary",
}

interface CardProps {
  width?: string; //optional property
  height?: string; //optional property
  children?: React.ReactNode; //optional property to allow children elements
  variant: CardVariant;
  myFunc: (num: number) => void; // doesn't return anything, show in console log
}

const Card: FC<CardProps> = ({ width, height, children, variant, myFunc }) => {
  const [myNumber, setMyNumber] = useState(2);

  return (
    <div
      onClick={() => myFunc(myNumber)}
      style={{
        border: variant === CardVariant.outlined ? "1px solid red" : "none",
        background: variant === CardVariant.primary ? "lightgrey" : "",
        width,
        height,
      }}
    >
      {children}
    </div>
  );
};

export default Card;
