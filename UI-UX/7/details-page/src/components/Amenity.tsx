interface AmenityProps {
  icon: string;
  text: string;
}

export default function Amenity({ icon, text }: AmenityProps) {
  let path = `/images/amenities/${icon}`;
  return (
    <div
      style={{
        display: "flex",
        alignItems: "center",
        verticalAlign: "center",
        padding: 0,
        margin: 0,
      }}
    >
      <img src={path} style={{ width: "24px", height: "24px", objectFit: "contain", marginRight: "8px" }}></img>
      {text}
    </div>
  );
}
