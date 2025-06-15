import "../styles/navbar.css";

interface ImageCircleProps {
  src: string;
  radius?: number;
}

function ImageCircle({ src, radius = 28 }: ImageCircleProps) {
  const size = radius * 2;
  const style: React.CSSProperties = {
    width: `${size}px`,
    height: `${size}px`,
    borderRadius: "50%",
    backgroundImage: `url(${src})`,
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat",
    overflow: "hidden",
    display: "inline-block",
  };

  return <div style={style} />;
}

export default function Navbar() {
  return (
    <header className="navbar">
      <div className="container nav">
        <h1 className="logo">Moonglow</h1>
        <nav>
          <ul className="nav-list">
            <li>
              <a href="/">Главная</a>
            </li>
            <li>
              <a href="/">Поддержка</a>
            </li>
            <li>
              <a href="/">Сохраненные</a>
            </li>
          </ul>
        </nav>
        <ImageCircle radius={28} src="/images/profile.png"></ImageCircle>
      </div>
    </header>
  );
}
