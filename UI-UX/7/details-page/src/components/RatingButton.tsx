import React from "react";
import Colors from "../Colors";

interface RatingButtonProps {
  rating: string; 
  reviewsCount?: number;
}

export default function RatingButton({ rating, reviewsCount }: RatingButtonProps) {
  const numericRating = parseFloat(rating);
  const textLabel = numericRating <= 8.6 ? "Хорошо" : "Отлично";
  const hasReviews = typeof reviewsCount === "number" && reviewsCount > 0;

  return (
    <div
      style={{
        display: "flex",
        alignItems: "center",
        gap: 8,
      }}
    >
      <div
        style={{
          width: 28,
          height: 20,
          borderRadius: 5,
          fontSize: 12,
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          color: Colors.Background,
          backgroundColor: Colors.Primary,
          padding: 0,
          margin: 0,
        }}
      >
        {rating}
      </div>

      {hasReviews ? (
        <div style={{ display: "flex", flexDirection: "column", lineHeight: 1.2 }}>
          <span style={{ color: Colors.Text, fontWeight: "bold", fontSize: 16 }}>{textLabel}</span>
          <span style={{ color: Colors.Text, fontSize: 12 }}>{reviewsCount} отзывов</span>
        </div>
      ) : (
        <span style={{ color: Colors.Text, fontWeight: "bold" }}>{textLabel}</span>
      )}
    </div>
  );
}
