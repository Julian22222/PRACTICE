import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import WineCard from "./WineCard";
import Context from "./Context";
import { useContext } from "react";
import { useParams } from "react-router-dom";
// import ButtonForEachCard from "./ButtonForEachCard";
import SearchBar from "./SearchBar";

const Home = () => {
  const value = useContext(Context);
  const [cardId, setCardId] = useState([]);

  const { id } = useParams();

  const handleCard = (event) => {
    // alert("You clicked me");
  };

  useEffect(() => {
    fetch(`https://api.sampleapis.com/wines/${value.wineType}`)
      .then((allWines) => allWines.json())
      .then((data) => {
        value.setWineList(data);
      });
  }, [value.wineType, value.SortBy, value.order]);

  if (value.SortBy === "date" && value.order === "asc") {
    value.wineList.sort((a, b) => {
      return Number(a.date.slice(-4)) > Number(b.date.slice(-4)) ? -1 : 1;
    });
  }

  if (value.SortBy === "date" && value.order === "desc") {
    value.wineList.sort((a, b) => {
      return Number(a.date.slice(-4)) > Number(b.date.slice(-4)) ? 1 : -1;
    });
  }

  if (value.sortBy === "reviews" && value.order === "asc") {
    value.wineList.sort((a, b) => {
      return Number(a.rating.reviews.split(" ")[0]) >
        Number(b.rating.reviews.split(" ")[0])
        ? -1
        : 1;
    });
  }

  if (value.SortBy === "reviews" && value.order === "desc") {
    value.wineList.sort((a, b) => {
      return Number(a.rating.reviews.split(" ")[0]) >
        Number(b.rating.reviews.split(" ")[0])
        ? 1
        : -1;
    });
  }

  return (
    <div>
      <SearchBar />
      <ul>
        {value.wineList.map((item) => {
          return (
            <Link to={`/wines/${value.wineType}/${item.id}`} className="Border">
              <button onClick={handleCard}>
                <li key={(item.wine, item.id)}>
                  <WineCard
                    winery={item.winery}
                    wine={item.wine}
                    rating={item.rating.average}
                    reviews={item.rating.reviews}
                    location={item.location}
                    image={item.image}
                    id={item.id}
                  />
                </li>
              </button>
            </Link>
            // <button>Like</button>
            // <button>Dislike</button>
          );
        })}
      </ul>
    </div>
  );
};

export default Home;
