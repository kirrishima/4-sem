import React from "react";
import Colors from "../Colors";
import { ContentContainerStyle, ContainerStyle } from "../Colors";
import Button, { ButtonVariant } from "./Button";

interface CommentsProps {
  name: string;
  country: string;
  src: string;
  content: string;
}

function Comment({ name, country, src, content }: CommentsProps) {
  return (
    <div
      style={{
        ...ContentContainerStyle,
        gap: 16,
        padding: 16,
        width: 469,
        borderRadius: 10,
        minHeight: 161,
        backgroundColor: Colors.UIBackground,
      }}
    >
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          alignItems: "center",
          gap: 8,
        }}
      >
        <img src={src} style={{ width: 56 }} />
        <div style={{ display: "flex", flexDirection: "column", gap: 4 }}>
          <strong>{name}</strong>
          <span>{country}</span>
        </div>
      </div>

      {content}
    </div>
  );
}

export default function Comments() {
  return (
    <div style={{ ...ContainerStyle, gap: 16 }}>
      <h2>Остались вопросы? Возможно, вы найдете ответы на них в отзывах других пользователей</h2>
      <div style={{ ...ContainerStyle, gap: 16, flexDirection: "row" }}>
        <Comment
          name="Эмили Смит"
          src="/images/reviewers/emily.png"
          country="США"
          content="Невероятный отель с потрясающими видами на Токио! Наша Artist Room была настоящим произведением искусства, а сервис – безупречный. Обязательно вернёмся!"
        ></Comment>{" "}
        <Comment
          name="Хироши Накамура "
          src="/images/reviewers/nakamura.png"
          country="Япония"
          content="Удобное расположение рядом с метро и вкусный завтрак. Немного тесновато в Superior Room, но атмосфера и арт-декор компенсируют это."
        ></Comment>
        <Comment
          name="Мария Лопес "
          src="/images/reviewers/women.png"
          country="Испания"
          content="Лучшее сочетание дизайна и комфорта! Особенно понравились СПА-услуги и лаунж на 25-м этаже. Рекомендую для романтической поездки."
        ></Comment>
      </div>
      <Button text="Читать все отзывы" appearance={ButtonVariant.Tertiary}></Button>
    </div>
  );
}
