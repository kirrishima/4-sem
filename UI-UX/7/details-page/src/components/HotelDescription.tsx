import React from "react";
import RatingButton from "./RatingButton";
import { ContentContainerStyle, ContainerStyle } from "../Colors";
import Amenity from "./Amenity";

export default function HotelDescription() {
  return (
    <div style={{ display: "flex", gap: 24, justifyContent: "space-between" }}>
      <div style={{ ...ContainerStyle, maxWidth: 1024 }}>
        <div style={ContentContainerStyle}>
          <h2>Park Hotel Tokyo</h2>
          <div>
            <RatingButton rating={"8.8"}></RatingButton>
          </div>
        </div>
        <div style={ContentContainerStyle}>
          <h2>Об этом отеле</h2>
          <div>
            Park Hotel Tokyo занимает этажи с 25 по 34 в Shiodome Media Tower и сочетает современный комфорт с японской
            эстетикой: проект ART colours превращает каждый этаж в художественную галерею, отражающую сезоны и традиции
            Японии. В лобби на 25-м этаже расположены ресепшн, ресторан и бар, а лаунж для гостей находится выше, с
            панорамными видами на город. Отель открыт в 2003 г. и регулярно обновляет дизайны Artist Rooms, приглашая на
            сотрудничество ведущих современных мастеров
          </div>
        </div>
        <div style={{ display: "flex", flexWrap: "wrap", gap: 8, width: 710 }}>
          <Amenity icon="wifi.png" text="Wi-Fi"></Amenity>
          <Amenity icon="fan.png" text="Фен"></Amenity>
          <Amenity icon="tv.png" text="Телевизор"></Amenity>
          <Amenity icon="cleaning.png" text="Ежедневная уборка номера"></Amenity>
          <Amenity icon="privacy.png" text="Сейф"></Amenity>
          <Amenity icon="spa.png" text="Заказ массажа в номер"></Amenity>
        </div>
      </div>

      <div style={{ ...ContainerStyle, gap: "16px", whiteSpace: "nowrap", alignSelf: "flex-end" }}>
        <h2>Исследуйте территорию</h2>
        <img src="/images/Просмотреть на карте.png" style={{ width: "264px", height: "155px" }}></img>
        <div style={ContentContainerStyle}>
          {" "}
          <Amenity icon="location.png" text="Станция метро Shiodome, 100м"></Amenity>
          <Amenity icon="location.png" text="Станиция Shinbashi, 300м"></Amenity>
          <Amenity icon="location.png" text="Hamarikyu Gardens, 10 минут ходьбы"></Amenity>
          <Amenity icon="location.png" text="Рядом множество кафе и баров"></Amenity>
        </div>
      </div>
    </div>
  );
}
