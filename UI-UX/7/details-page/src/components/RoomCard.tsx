import React from "react";
import RatingButton from "./RatingButton";
import Amenity from "./Amenity";
import Slider from "./Slider";
import { ContainerStyle, ContentContainerStyle } from "../Colors";
import Colors from "../Colors";
import Button, { ButtonVariant } from "./Button";

interface Amenity {
  iconPath: string;
  title: string;
}

interface RoomCardProps {
  images: string[];
  title: string;
  rating: string;
  reviews: number;
  area: number;
  floor: number;
  bed: string;
  cancelPolicy: string;
  dops: Amenity[];
  price: number;
}

export default function RoomCard({
  images,
  title,
  rating,
  reviews,
  area,
  floor,
  bed,
  cancelPolicy,
  dops,
  price,
}: RoomCardProps) {
  return (
    <div
      style={{
        borderRadius: 10,
        backgroundColor: Colors.UIBackground,
        display: "flex",
        flexDirection: "column",
        height: "100%",
        width: "fit-content",
      }}
    >
      <Slider images={images} />

      <div
        style={{
          padding: 16,
          display: "flex",
          flexDirection: "column",
          flexGrow: 1,
          gap: 16,
        }}
      >
        <div style={ContentContainerStyle}>
          <h2 style={{ margin: 0 }}>{title}</h2>
          <RatingButton rating={rating} reviewsCount={reviews} />
        </div>

        <div style={ContentContainerStyle}>
          <Amenity text={`${area} м²`} icon="area.png" />
          <Amenity text={`${floor}-й этаж`} icon="floor.png" />
          <Amenity text={bed} icon="bed.png" />
        </div>

        <div style={ContentContainerStyle}>
          <strong>Политика отмены бронирования</strong> {cancelPolicy}
        </div>

        <div style={ContentContainerStyle}>
          <strong>Дополнительные услуги</strong>
          {dops.map((amenity, idx) => (
            <Amenity key={idx} icon={amenity.iconPath} text={amenity.title} />
          ))}
        </div>

        <div style={{ display: "flex", flexDirection: "column", gap: 16, marginTop: "auto" }}>
          <div style={{ display: "flex", flexDirection: "column", alignItems: "flex-end", gap: 8 }}>
            <div style={{ fontSize: 18, fontWeight: "bold" }}>{`$${price}`}</div>
            <div style={{ fontSize: 14, color: Colors.Text }}>{`$${(price * 1.09).toFixed(2)}`}</div>
            <div style={{ fontSize: 14, color: Colors.Text }}>Включая налоги и сборы</div>
          </div>

          <div style={{ display: "flex", justifyContent: "center" }}>
            <Button text="Забронировать" appearance={ButtonVariant.Primary} />
          </div>
        </div>
      </div>
    </div>
  );
}
