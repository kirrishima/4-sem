using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ADONET
{
    public partial class MainWindow : Window
    {
        private string _selectedPhotoPath = null!;

        public MainWindow()
        {
            InitializeComponent();
            DBManager.InitializeDatabase();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var ds = new DataSet();
                var dtHotels = DBManager.GetHotels();
                dtHotels.TableName = "Hotels";

                var dtRooms = DBManager.GetRooms();
                dtRooms.TableName = "Rooms";

                var dtBookings = DBManager.GetBookings();
                dtBookings.TableName = "Bookings";

                var dtClients = DBManager.GetClients();
                dtClients.TableName = "Clients";

                ds.Tables.Add(dtHotels);
                ds.Tables.Add(dtRooms);
                ds.Tables.Add(dtBookings);
                ds.Tables.Add(dtClients);

                DataContext = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region отели

        private void btnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.webp)|*.png;*.jpg;*.jpeg;*.webp"
            };
            if (dlg.ShowDialog() == true)
            {
                _selectedPhotoPath = dlg.FileName;
                tbHotelPhotoPath.Text = _selectedPhotoPath;
            }
        }

        private void btnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHotelName.Text))
            {
                MessageBox.Show("Введите название отеля.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            byte[]? photoBytes = null;
            if (!string.IsNullOrEmpty(_selectedPhotoPath))
            {
                if (!File.Exists(_selectedPhotoPath))
                {
                    MessageBox.Show("Файл изображения не найден.", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    photoBytes = File.ReadAllBytes(_selectedPhotoPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось прочитать файл изображения:\n{ex.Message}",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            try
            {
                DBManager.AddHotel(
                    tbHotelName.Text.Trim(),
                    tbHotelAddress.Text.Trim(),
                    tbHotelPhone.Text.Trim(),
                    photoBytes
                );

                tbHotelPhotoPath.Clear();

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении отеля:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateHotel_Click(object sender, RoutedEventArgs e)
        {
            if (dgHotels.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Сначала выберите отель в таблице.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbHotelName.Text))
            {
                MessageBox.Show("Название отеля не может быть пустым.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)row["HotelID"];
            byte[]? photoBytes = null;
            if (!string.IsNullOrEmpty(_selectedPhotoPath))
            {
                if (!File.Exists(_selectedPhotoPath))
                {
                    MessageBox.Show("Файл изображения не найден.", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    photoBytes = File.ReadAllBytes(_selectedPhotoPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось прочитать файл изображения:\n{ex.Message}",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            try
            {
                DBManager.UpdateHotel(
                    id,
                    tbHotelName.Text.Trim(),
                    tbHotelAddress.Text.Trim(),
                    tbHotelPhone.Text.Trim(),
                    photoBytes
                );

                tbHotelPhotoPath.Clear();

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении отеля:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteHotel_Click(object sender, RoutedEventArgs e)
        {
            if (dgHotels.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Сначала выберите отель в таблице.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)row["HotelID"];
            if (MessageBox.Show("Вы действительно хотите удалить выбранный отель?",
                                "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes)
                return;

            try
            {
                DBManager.DeleteHotel(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении отеля:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshHotels_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region — Работа с номерами —

        private bool ValidateRoomInput(out int hotelId, out int roomNumber, out decimal price)
        {
            hotelId = 0; roomNumber = 0; price = 0;
            if (!int.TryParse(tbRoomHotelID.Text, out hotelId))
            {
                MessageBox.Show("ID отеля должен быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!DBManager.HotelExists(hotelId))
            {
                MessageBox.Show("Отель с таким ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!int.TryParse(tbRoomNumber.Text, out roomNumber))
            {
                MessageBox.Show("Номер комнаты должен быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!decimal.TryParse(tbRoomPrice.Text, out price))
            {
                MessageBox.Show("Цена должна быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите номер для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateRoomInput(out int hotelId, out int roomNumber, out decimal price)) return;

            int id = (int)row["RoomID"];
            try
            {
                DBManager.UpdateRoom(id, hotelId, roomNumber, tbRoomType.Text.Trim(), price);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении номера:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateRoomInput(out int hotelId, out int roomNumber, out decimal price)) return;

            try
            {
                DBManager.AddRoom(hotelId, roomNumber, tbRoomType.Text.Trim(), price);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении номера:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Сначала выберите номер в таблице.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)row["RoomID"];
            if (MessageBox.Show("Удалить выбранный номер?", "Подтверждение",
                                MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes)
                return;

            try
            {
                DBManager.DeleteRoom(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении номера:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshRooms_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region — Работа с бронированиями —

        private bool ValidateBookingInput(out int roomId, out int clientId, out DateTime start, out DateTime end)
        {
            roomId = clientId = 0; start = end = DateTime.MinValue;

            if (!int.TryParse(tbBookingRoomID.Text, out roomId) || !DBManager.RoomExists(roomId))
            {
                MessageBox.Show("Укажите существующий ID номера.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!int.TryParse(tbBookingClientID.Text, out clientId) || !DBManager.ClientExists(clientId))
            {
                MessageBox.Show("Укажите существующий ID клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!DateTime.TryParse(tbBookingStart.Text, out start))
            {
                MessageBox.Show("Введите корректную дату заезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!DateTime.TryParse(tbBookingEnd.Text, out end))
            {
                MessageBox.Show("Введите корректную дату выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (end <= start)
            {
                MessageBox.Show("Дата выезда должна быть позже даты заезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void btnUpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookings.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите бронирование для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateBookingInput(out int roomId, out int clientId, out DateTime start, out DateTime end)) return;

            int id = (int)row["BookingID"];
            try
            {
                DBManager.UpdateBooking(id, roomId, clientId, start, end);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении бронирования:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateBookingInput(out int roomId, out int clientId, out DateTime start, out DateTime end)) return;

            try
            {
                DBManager.AddBooking(roomId, clientId, start, end);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении бронирования:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookings.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Сначала выберите бронирование в таблице.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)row["BookingID"];
            if (MessageBox.Show("Удалить выбранное бронирование?", "Подтверждение",
                                MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes)
                return;

            try
            {
                DBManager.DeleteBooking(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении бронирования:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshBookings_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region — Работа с клиентами —

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateClientInput()) return;

            try
            {
                DBManager.AddClient(
                    tbClientName.Text.Trim(),
                    tbClientPhone.Text.Trim(),
                    tbClientEmail.Text.Trim()
                );
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateClientInput()
        {
            if (string.IsNullOrWhiteSpace(tbClientName.Text))
            {
                MessageBox.Show("Имя клиента не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btnUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите клиента для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateClientInput()) return;

            int id = (int)row["ClientID"];
            try
            {
                DBManager.UpdateClient(id, tbClientName.Text.Trim(), tbClientPhone.Text.Trim(), tbClientEmail.Text.Trim());
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении клиента:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Сначала выберите клиента в таблице.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)row["ClientID"];
            if (MessageBox.Show("Удалить выбранного клиента?", "Подтверждение",
                                MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes)
                return;

            try
            {
                DBManager.DeleteClient(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении клиента:\n{ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshClients_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion


        private void dgHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgHotels.SelectedItem is DataRowView row)
            {
                tbHotelName.Text = row["Name"]?.ToString();
                tbHotelAddress.Text = row["Address"]?.ToString();
                tbHotelPhone.Text = row["Phone"]?.ToString();
                tbHotelPhotoPath.Clear();
            }
        }

        private void dgRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRooms.SelectedItem is DataRowView row)
            {
                tbRoomHotelID.Text = row["HotelID"]?.ToString();
                tbRoomNumber.Text = row["RoomNumber"]?.ToString();
                tbRoomType.Text = row["Type"]?.ToString();
                tbRoomPrice.Text = row["Price"]?.ToString();
            }
        }

        private void dgBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookings.SelectedItem is DataRowView row)
            {
                tbBookingRoomID.Text = row["RoomID"]?.ToString();
                tbBookingClientID.Text = row["ClientID"]?.ToString();
                tbBookingStart.Text = row["StartDate"]?.ToString();
                tbBookingEnd.Text = row["EndDate"]?.ToString();
            }
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedItem is DataRowView row)
            {
                tbClientName.Text = row["Name"]?.ToString();
                tbClientPhone.Text = row["Phone"]?.ToString();
                tbClientEmail.Text = row["Email"]?.ToString();
            }
        }
    }
}
