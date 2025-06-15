import React from "react";
const parentStyle: React.CSSProperties = {
  display: "grid",
  gridTemplateColumns: "repeat(4, 358px)",
  gridTemplateRows: "repeat(2, 251px)",
  gridColumnGap: "2px",
  gridRowGap: "2px",
};

const childAreas = [
  "1 / 1 / 3 / 3",
  "1 / 3 / 2 / 4",
  "1 / 4 / 2 / 5",
  "2 / 4 / 3 / 5",
  "2 / 3 / 3 / 4", 
];

export default function ImageGrid() {
  const imgsArray = ["/grid/1.png", "/grid/2.png", "/grid/3.png",  "/grid/5.png","/grid/4.png",];

  return (
    <div style={parentStyle}>
      {imgsArray.map((src, idx) => (
        <div
          key={idx}
          style={{
            gridArea: childAreas[idx],
            overflow: "hidden",
          }}
        >
          <img
            src={src}
            alt={`Image ${idx + 1}`}
            style={{
              width: "100%",
              height: "100%",
              objectFit: "cover",
              objectPosition: "center",
            }}
          />
        </div>
      ))}
    </div>
  );
}
