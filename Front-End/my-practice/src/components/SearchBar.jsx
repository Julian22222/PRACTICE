import Context from "./Context";
import { useContext } from "react";

const SearchBar = () => {
  const value = useContext(Context);

  const handleType = (event) => {
    value.setWineType(event.target.value);
    console.log(event.target.value);
  };

  const handleSortBy = (event) => {
    console.log(event.target.value);
    value.setSortBy(event.target.value);
  };

  const handleOrder = (event) => {
    console.log(event.target.value);
    value.setOrder(event.target.value);
  };

  return (
    <div className="SearchBar">
      <label>Wine Type: </label>
      <select onChange={handleType} name="SortBy" className="SortBy">
        <option value="reds">Red</option>
        <option value="whites">White</option>
        <option value="sparkling">Sparkling</option>
        <option value="rose">Rose</option>
        <option value="dessert">Dessert</option>
      </select>
      <span> Sort By: </span>
      <select onChange={handleSortBy}>
        <option value="reviews">Reviews</option>
        <option value="date">Date</option>
      </select>

      <span> Order: </span>
      <select onChange={handleOrder}>
        <option value="asc">Ascending</option>
        <option value="desc">Descending</option>
      </select>
    </div>
  );
};

export default SearchBar;
