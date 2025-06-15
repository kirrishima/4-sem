import React from "react";
import "./App.css";
import ImageGrid from "./components/ImageGrid";
import Navbar from "./components/Navbar";
import HotelDescription from "./components/HotelDescription";
import Slider from "./components/Slider";
import RoomCard from "./components/RoomCard";
import Rooms3 from "./components/3Rooms";
import Comments from "./components/Comments";
import Footer from "./components/Footer";

function App() {
  return (
    <div className="layout">
      <Navbar></Navbar>

      <div className="container">
        <main className="content">
          <ImageGrid />
          <HotelDescription></HotelDescription>
          <Rooms3></Rooms3>
          <Comments></Comments>
        </main>
      </div>

      <Footer></Footer>
    </div>
  );
}

export default App;
