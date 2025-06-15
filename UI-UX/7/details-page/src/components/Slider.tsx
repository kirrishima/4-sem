import React, { useState, CSSProperties } from "react";

interface SliderProps {
  images: string[];
  width?: number;
  height?: number;
}

export default function Slider({ images, width = 469, height = 310 }: SliderProps) {
  const [current, setCurrent] = useState(0);
  const last = images.length - 1;

  const containerStyle: CSSProperties = {
    position: "relative",
    width: `${width}px`,
    height: `${height}px`,
    overflow: "hidden",
    borderRadius: 10,
  };

  const trackStyle: CSSProperties = {
    display: "flex",
    width: `${width * images.length}px`,
    height: "100%",
    transform: `translateX(-${current * width}px)`,
    transition: "transform 0.3s ease",
  };

  const slideStyle: CSSProperties = {
    width: `${width}px`,
    height: "100%",
    flexShrink: 0,
    objectFit: "cover",
  };

  const arrowBase: CSSProperties = {
    position: "absolute",
    top: "50%",
    transform: "translateY(-50%)",
    width: 28,
    height: 28,
    cursor: "pointer",
    background: "none",
    border: "none",
    padding: 0,
  };

  const prevStyle: CSSProperties = {
    ...arrowBase,
    left: 12,
  };
  const nextStyle: CSSProperties = {
    ...arrowBase,
    right: 12,
  };

  const paginationStyle: CSSProperties = {
    position: "absolute",
    bottom: 12,
    left: "50%",
    transform: "translateX(-50%)",
    display: "flex",
    gap: 8,
  };

  return (
    <div style={containerStyle}>
      <div style={trackStyle}>
        {images.map((src, idx) => (
          <img key={idx} src={src} alt="" style={slideStyle} />
        ))}
      </div>

      <button
        style={prevStyle}
        onClick={() => setCurrent(current === 0 ? last : current - 1)}
        aria-label="Previous slide"
      >
        <svg width="28" height="28" viewBox="0 0 28 28" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M0 8C0 3.58172 3.58172 0 8 0H20C24.4183 0 28 3.58172 28 8V20C28 24.4183 24.4183 28 20 28H8C3.58172 28 0 24.4183 0 20V8Z"
            fill="#ECF2F3"
          />
          <path d="M16 9L12 14L16 19" stroke="#040909" strokeWidth="2" />
        </svg>
      </button>
 
      <button style={nextStyle} onClick={() => setCurrent(current === last ? 0 : current + 1)} aria-label="Next slide">
        <svg width="28" height="28" viewBox="0 0 28 28" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M0 8C0 3.58172 3.58172 0 8 0H20C24.4183 0 28 3.58172 28 8V20C28 24.4183 24.4183 28 20 28H8C3.58172 28 0 24.4183 0 20V8Z"
            fill="#ECF2F3"
          />
          <path d="M12 9L16 14L12 19" stroke="#040909" strokeWidth="2" />
        </svg>
      </button>

      <div style={paginationStyle}>
        {images.map((_, idx) =>
          idx === current ? (
            <svg key={idx} width="12" height="12" viewBox="0 0 12 12" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="6" cy="6" r="5" fill="#040909" stroke="#ECF2F3" strokeWidth="2" />
            </svg>
          ) : (
            <svg key={idx} width="12" height="12" viewBox="0 0 12 12" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="6" cy="6" r="5" fill="#ECF2F3" stroke="#ECF2F3" strokeWidth="2" />
            </svg>
          )
        )}
      </div>
    </div>
  );
}
