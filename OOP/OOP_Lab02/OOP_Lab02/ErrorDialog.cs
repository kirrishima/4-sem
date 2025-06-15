using System;
using System.Drawing;
using System.Windows.Forms;

public class ErrorDialog : Form
{
    private Label lblMessage;
    private TextBox txtDetails;
    private Button btnClose;

    // Конструктор, принимающий объект исключения.
    public ErrorDialog(Exception ex) : this(ex.Message, ex.ToString())
    {
    }

    // Конструктор, принимающий строку сообщения.
    public ErrorDialog(string message) : this(message, null)
    {
    }

    // Приватный конструктор, который настраивает форму, используется обоими конструкторами.
    private ErrorDialog(string message, string details)
    {
        // Настройка основных параметров формы
        this.Text = "Ошибка";
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ClientSize = details == null ? new Size(400, 150) : new Size(400, 300);

        // Метка для отображения основного сообщения
        lblMessage = new Label
        {
            Text = message,
            Dock = DockStyle.Top,
            Height = 50,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = false
        };
        this.Controls.Add(lblMessage);

        // Если переданы подробности (например, стек вызовов), добавляем текстовое поле для их отображения
        if (!string.IsNullOrEmpty(details))
        {
            txtDetails = new TextBox
            {
                Text = details,
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Consolas", 9)
            };
            this.Controls.Add(txtDetails);
        }

        // Кнопка для закрытия диалога
        btnClose = new Button
        {
            Text = "Закрыть",
            Dock = DockStyle.Bottom,
            Height = 30,
            DialogResult = DialogResult.OK
        };
        this.Controls.Add(btnClose);
        this.AcceptButton = btnClose;
    }
}
