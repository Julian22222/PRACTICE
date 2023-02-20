const RemoveBtn = ({ item, setList }) => {
  const hadleRemove = (item) => {
    const newList = setList.filter((element) => element !== item);

    setList(newList);
  };

  return (
    <>
      <button onClick={() => hadleRemove(item)}>Remove</button>
    </>
  );
};

export default RemoveBtn;
