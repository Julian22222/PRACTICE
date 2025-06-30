# React + TypeScript

```JS
//in the component

import ( IProduct ) from '../Types/types.tsx'  //import model type

interface ProductProps {  //props that this component will receive
    product: IProduct
    onCreate: ()=> void   //function that doesnt return anythig (usually used for useState functions, when set a value)
}

const submit=()=>{
  ///some code

  onCreate();  //calling the function
}

export function Product ({product}: ProductProps){     //or export function Product (props: ProductProps){ ... {props.product.title} ....}
    return(
        <div>
            {product.title}

            <button onClick={submit}>click</button>
        </div>
    )
}



//types.tsx
export interface IProduct {
    id: number,
    title: string,
    price: number,
    description: string
    category: string,
    image: string
}

```

```JS
//use State - if details === true -> show component

{details &&
    <div>
        <p> {product.description}
    </div>
    }

```

```JS

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

////////////////////////////////The same component
export function Card: FC<CardProps> ({ width, height, children, variant, myFunc }) {
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


```
