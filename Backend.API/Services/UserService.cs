using PharmacyEmergencySystem.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace PharmacyEmergencySystem.Services
{
    public class UserService
    {
        private readonly string _connectionString;

        // Constructor – merr connection string nga appsettings.json
        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // CREATE USER (SIGNUP)
        public void AddUser(User user)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO users 
                (Name, Location, Number, Gender, Age, Email, OTP, OtpExpiry, IsVerified, Role)
                VALUES
                (@Name, @Location, @Number, @Gender, @Age, @Email, @OTP, @OtpExpiry, @IsVerified, @Role)
            ";

            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Location", user.Location);
            cmd.Parameters.AddWithValue("@Number", user.Number);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@Age", user.Age);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@OTP", user.OTP);
            cmd.Parameters.AddWithValue("@OtpExpiry", user.OtpExpiry);
            cmd.Parameters.AddWithValue("@IsVerified", user.IsVerified);
            cmd.Parameters.AddWithValue("@Role", user.Role);

            cmd.ExecuteNonQuery();
        }

        // GET USER BY EMAIL OR NUMBER
        public User GetUserByEmailOrNumber(string emailOrNumber)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT * FROM users 
                WHERE Email = @Value OR Number = @Value
            ";
            cmd.Parameters.AddWithValue("@Value", emailOrNumber);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return new User
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Location = reader.GetString("Location"),
                Number = reader.GetString("Number"),
                Gender = reader.GetString("Gender"),
                Age = reader.GetInt32("Age"),
                Email = reader.GetString("Email"),
                OTP = reader.IsDBNull(reader.GetOrdinal("OTP")) 
                        ? null 
                        : reader.GetString("OTP"),
                OtpExpiry = reader.IsDBNull(reader.GetOrdinal("OtpExpiry"))
                        ? null
                        : reader.GetDateTime("OtpExpiry"),
                IsVerified = reader.GetBoolean("IsVerified"),
                Role = reader.GetString("Role")
            };
        }

        //  UPDATE OTP (LOGIN OTP)
        public void UpdateOTP(string emailOrNumber, string otp)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE users 
                SET OTP = @OTP, OtpExpiry = @OtpExpiry
                WHERE Email = @Value OR Number = @Value
            ";

            cmd.Parameters.AddWithValue("@OTP", otp);
            cmd.Parameters.AddWithValue("@OtpExpiry", DateTime.UtcNow.AddMinutes(5));
            cmd.Parameters.AddWithValue("@Value", emailOrNumber);

            cmd.ExecuteNonQuery();
        }

        // VERIFY USER (AFTER SIGNUP)
        public void VerifyUser(string emailOrNumber)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE users 
                SET IsVerified = 1, OTP = NULL, OtpExpiry = NULL
                WHERE Email = @Value OR Number = @Value
            ";

            cmd.Parameters.AddWithValue("@Value", emailOrNumber);
            cmd.ExecuteNonQuery();
        }
    }
}
