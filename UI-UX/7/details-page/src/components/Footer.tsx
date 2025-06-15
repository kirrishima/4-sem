import { ContentContainerStyle } from "../Colors";

export default function Footer() {
  return (
    <footer className="footer">
      <div>© {new Date().getFullYear()} - Компания, ООО или че там. Все права защищены. Адрес регистрации</div>
      <div style={{ ...ContentContainerStyle, flexDirection: "row", alignSelf: "center" }}>
        <a href="">
          <img src="/images/media/Telegram.png" className="mediaicon"></img>
        </a>
        <a href="">
          <img src="/images/media/x.png" className="mediaicon"></img>
        </a>
        <a href="">
          <img src="/images/media/Facebook.png" className="mediaicon"></img>
        </a>
        <a href="">
          <img src="/images/media/LinkedIn.png" className="mediaicon"></img>
        </a>
      </div>
    </footer>
  );
}
