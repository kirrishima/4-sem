using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace ADONET
{
    public static class DBManager
    {
        private static string GetConnectionString(string database = "ADONETDB")
        {
            var cs = ConfigurationManager.ConnectionStrings["ADONETDB"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(cs);
            builder.InitialCatalog = database;
            return builder.ConnectionString;
        }

        public static void InitializeDatabase()
        {
            string cs = ConfigurationManager.ConnectionStrings["ADONETDB"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(cs);
            string dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sqlCreateDB = $"IF DB_ID('{dbName}') IS NULL CREATE DATABASE {dbName}";
                using (SqlCommand cmd = new SqlCommand(sqlCreateDB, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            builder.InitialCatalog = dbName;
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sqlHotels = @"
                    IF OBJECT_ID('dbo.Hotels','U') IS NULL
                    CREATE TABLE dbo.Hotels (
                        HotelID INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100) NOT NULL,
                        Address NVARCHAR(200),
                        Phone NVARCHAR(20),
                        Photo VARBINARY(MAX)
                    )";
                using (SqlCommand cmd = new SqlCommand(sqlHotels, conn))
                    cmd.ExecuteNonQuery();
                string sqlClients = @"
                    IF OBJECT_ID('dbo.Clients','U') IS NULL
                    CREATE TABLE dbo.Clients (
                        ClientID INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100),
                        Phone NVARCHAR(20),
                        Email NVARCHAR(100)
                    )";
                using (SqlCommand cmd = new SqlCommand(sqlClients, conn))
                    cmd.ExecuteNonQuery();
                string sqlRooms = @"
                    IF OBJECT_ID('dbo.Rooms','U') IS NULL
                    CREATE TABLE dbo.Rooms (
                        RoomID INT PRIMARY KEY IDENTITY,
                        HotelID INT NOT NULL FOREIGN KEY REFERENCES dbo.Hotels(HotelID),
                        RoomNumber INT,
                        Type NVARCHAR(50),
                        Price DECIMAL(10,2)
                    )";
                using (SqlCommand cmd = new SqlCommand(sqlRooms, conn))
                    cmd.ExecuteNonQuery();
                string sqlBookings = @"
                    IF OBJECT_ID('dbo.Bookings','U') IS NULL
                    CREATE TABLE dbo.Bookings (
                        BookingID INT PRIMARY KEY IDENTITY,
                        RoomID INT NOT NULL FOREIGN KEY REFERENCES dbo.Rooms(RoomID),
                        ClientID INT FOREIGN KEY REFERENCES dbo.Clients(ClientID),
                        StartDate DATETIME,
                        EndDate DATETIME
                    )";
                using (SqlCommand cmd = new SqlCommand(sqlBookings, conn))
                    cmd.ExecuteNonQuery();


                string sqlProc = @"
                    IF OBJECT_ID('dbo.sp_AddHotel','P') IS NULL
                    EXEC('CREATE PROCEDURE dbo.sp_AddHotel 
                            @Name NVARCHAR(100), 
                            @Address NVARCHAR(200), 
                            @Phone NVARCHAR(20), 
                            @Photo VARBINARY(MAX)
                          AS 
                          INSERT INTO dbo.Hotels(Name,Address,Phone,Photo) 
                          VALUES(@Name,@Address,@Phone,@Photo)')";
                using (SqlCommand cmd = new SqlCommand(sqlProc, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        public static bool ClientExists(int id)
        {
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Clients WHERE ClientID=@ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public static bool RoomExists(int id)
        {
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Rooms WHERE RoomID=@ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public static void AddHotel(string name, string address, string phone, byte[]? photo)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_AddHotel", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Address", address);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            var param = cmd.Parameters.Add(
                                 "@Photo",
                                 SqlDbType.VarBinary,
                                 -1
                             );
                            param.Value = photo ?? (object)DBNull.Value;
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static DataTable GetHotels()
        {
            string cs = GetConnectionString();
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM dbo.Hotels";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }

        public static void UpdateHotel(int id, string name, string address, string phone, byte[]? photo)
        {
            using var conn = new SqlConnection(GetConnectionString());
            conn.Open();
            using var tx = conn.BeginTransaction();
            try
            {
                var sql = @"
            UPDATE dbo.Hotels 
               SET Name=@Name, Address=@Address, Phone=@Phone, Photo=@Photo
             WHERE HotelID=@ID";
                using var cmd = new SqlCommand(sql, conn, tx);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);
                var p = cmd.Parameters.Add("@Photo", SqlDbType.VarBinary, -1);
                p.Value = photo ?? (object)DBNull.Value;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public static void UpdateRoom(int id, int hotelId, int roomNumber, string type, decimal price)
        {
            string cs = GetConnectionString();
            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                    UPDATE dbo.Rooms
                       SET HotelID    = @HotelID,
                           RoomNumber = @RoomNumber,
                           Type       = @Type,
                           Price      = @Price
                     WHERE RoomID = @ID";
                        using (var cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@HotelID", hotelId);
                            cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                            cmd.Parameters.AddWithValue("@Type", type);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateBooking(int id, int roomId, int clientId, DateTime start, DateTime end)
        {
            string cs = GetConnectionString();
            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                    UPDATE dbo.Bookings
                       SET RoomID   = @RoomID,
                           ClientID = @ClientID,
                           StartDate = @StartDate,
                           EndDate   = @EndDate
                     WHERE BookingID = @ID";
                        using (var cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@RoomID", roomId);
                            cmd.Parameters.AddWithValue("@ClientID", clientId);
                            cmd.Parameters.AddWithValue("@StartDate", start);
                            cmd.Parameters.AddWithValue("@EndDate", end);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateClient(int id, string name, string phone, string email)
        {
            string cs = GetConnectionString();
            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                    UPDATE dbo.Clients
                       SET Name  = @Name,
                           Phone = @Phone,
                           Email = @Email
                     WHERE ClientID = @ID";
                        using (var cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void DeleteHotel(int id)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM dbo.Hotels WHERE HotelID=@ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool HotelExists(int id)
        {
            using var conn = new SqlConnection(GetConnectionString());
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Hotels WHERE HotelID=@ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public static void AddRoom(int hotelId, int number, string type, decimal price)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO dbo.Rooms (HotelID,RoomNumber,Type,Price) VALUES(@HotelID,@RoomNumber,@Type,@Price)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@HotelID", hotelId);
                            cmd.Parameters.AddWithValue("@RoomNumber", number);
                            cmd.Parameters.AddWithValue("@Type", type);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        public static DataTable GetRooms()
        {
            string cs = GetConnectionString();
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM dbo.Rooms";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }
        public static void DeleteRoom(int id)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM dbo.Rooms WHERE RoomID=@ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void AddBooking(int roomId, int clientId, DateTime start, DateTime end)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO dbo.Bookings (RoomID,ClientID,StartDate,EndDate) VALUES(@RoomID,@ClientID,@Start,@End)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@RoomID", roomId);
                            cmd.Parameters.AddWithValue("@ClientID", clientId);
                            cmd.Parameters.AddWithValue("@Start", start);
                            cmd.Parameters.AddWithValue("@End", end);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        public static DataTable GetBookings()
        {
            string cs = GetConnectionString();
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM dbo.Bookings";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }
        public static void DeleteBooking(int id)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM dbo.Bookings WHERE BookingID=@ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void AddClient(string name, string phone, string email)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO dbo.Clients (Name,Phone,Email) VALUES(@Name,@Phone,@Email)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        public static DataTable GetClients()
        {
            string cs = GetConnectionString();
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM dbo.Clients";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }
        public static void DeleteClient(int id)
        {
            string cs = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM dbo.Clients WHERE ClientID=@ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
