import React from "react";
import RoomCard from "./RoomCard";

export default function Rooms3() {
  let images = ["/grid/1.png", "/grid/2.png", "/grid/3.png", "/grid/4.png", "/grid/5.png"];

  const sampleRoom = {
    images: ["/grid/1/0.png", "/grid/1/1.jpg", "/grid/1/2.jpg"],
    title: "Artist King Room",
    rating: "9.2",
    reviews: 218,
    area: 33,
    floor: 31,
    bed: "1 King-bed",
    cancelPolicy: "Бесплатно за 2 дня до заезда",
    dops: [
      {
        iconPath: "breakfast.png",
        title: "Бесплатный завтрак",
      },
      {
        iconPath: "spa.png",
        title: "Массаж в номере (платно)",
      },
    ],
    price: 180,
  };

  const sampleRoom1 = {
    images: ["/grid/2/0.png", "/grid/2/1.jpg", "/grid/2/3.jpg", "/grid/2/2.jpg"],
    title: "Deluxe Twin Room",
    rating: "8.6",
    reviews: 180,
    area: 43,
    floor: 28,
    bed: "2 односпальные кровати",
    cancelPolicy: "Бесплатно за 2 дня до заезда",
    dops: [
      {
        iconPath: "breakfast.png",
        title: "Бесплатный завтрак",
      },
      {
        iconPath: "parking.png",
        title: "Бесплатная парковка позле отеля",
      },
      {
        iconPath: "spa.png",
        title: "Массаж в номере (платно)",
      },
    ],
    price: 200,
  };

  const sampleRoom2 = {
    images: ["/grid/3/0.png", "/grid/3/1.jpg", "/grid/3/3.jpg", "/grid/3/2.jpg"],
    title: "Superior Room",
    rating: "8.4",
    reviews: 264,
    area: 29,
    floor: 34,
    bed: "1 Quuen-bed",
    cancelPolicy: "Бесплатно за 2 дня до заезда",
    dops: [
      {
        iconPath: "breakfast.png",
        title: "Завтрак (платно)",
      },
    ],
    price: 160,
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "row",
        alignItems: "stretch",
        gap: 16,
      }}
    >
      <div style={{ flex: 1 }}>
        <RoomCard {...sampleRoom} />
      </div>
      <div style={{ flex: 1 }}>
        <RoomCard {...sampleRoom1} />
      </div>
      <div style={{ flex: 1 }}>
        <RoomCard {...sampleRoom2} />
      </div>
    </div>
  );
}
