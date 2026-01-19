using PharmacyEmergencySystem.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PharmacyEmergencySystem.Services
{
    public class UserService
    {
        private string connectionString = "server=localhost;user=root;password=dontspeak7;database=pharmacydb;";

        public void AddUser(User user)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO users (Name, Location, Number, Gender, Age, Email, OTP, IsVerified, Role)
                                VALUES (@Name, @Location, @Number, @Gender, @Age, @Email, @OTP, @IsVerified, @Role)";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Location", user.Location);
            cmd.Parameters.AddWithValue("@Number", user.Number);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@Age", user.Age);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@OTP", user.OTP);
            cmd.Parameters.AddWithValue("@IsVerified", user.IsVerified);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.ExecuteNonQuery();
        }

        public User GetUserByEmailOrNumber(string emailOrNumber)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE Email=@EmailOrNumber OR Number=@EmailOrNumber";
            cmd.Parameters.AddWithValue("@EmailOrNumber", emailOrNumber);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Location = reader.GetString("Location"),
                    Number = reader.GetString("Number"),
                    Gender = reader.GetString("Gender"),
                    Age = reader.GetInt32("Age"),
                    Email = reader.GetString("Email"),
                    OTP = reader.GetString("OTP"),
                    IsVerified = reader.GetBoolean("IsVerified"),
                    Role = reader.GetString("Role")
                };
            }
            return null;
        }

        public void VerifyUser(string emailOrNumber)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE users SET IsVerified=1, OTP='' WHERE Email=@EmailOrNumber OR Number=@EmailOrNumber";
            cmd.Parameters.AddWithValue("@EmailOrNumber", emailOrNumber);
            cmd.ExecuteNonQuery();
        }

        public void UpdateOTP(string emailOrNumber, string otp)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE users SET OTP=@OTP WHERE Email=@EmailOrNumber OR Number=@EmailOrNumber";
            cmd.Parameters.AddWithValue("@OTP", otp);
            cmd.Parameters.AddWithValue("@EmailOrNumber", emailOrNumber);
            cmd.ExecuteNonQuery();
        }
    }
}
