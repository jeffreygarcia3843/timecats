﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace time_sucks.Models
{
    public class DBHelper
    {
        //TODO Make this a better system user
        static private MySqlConnectionStringBuilder connstring = new MySqlConnectionStringBuilder("" +
            "Server=cs4450.cj7o28wmyp47.us-east-2.rds.amazonaws.com;" +
            "UID=Logan;" +
            "password=password;" +
            "database=cs4450");

        public static User getUser(string username)
        {
            User user = null;
            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    //SQL and Parameters
                    cmd.CommandText = "SELECT * FROM users WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Runs once per record retrieved
                        while (reader.Read())
                        {
                            user = new User();
                            user.userID = reader.GetInt32("userID");
                            user.username = reader.GetString("username");
                            user.firstName = reader.GetString("firstName");
                            user.lastName = reader.GetString("lastName");
                            user.type = reader.GetChar("type");
                            user.isActive = reader.GetBoolean("isActive");
                        }
                    }
                }
            }
            return user;
        }

        public static User getUser(string username, string password)
        {
            User user = null;
            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using(MySqlCommand cmd = conn.CreateCommand())
                {
                    //SQL and Parameters
                    cmd.CommandText = "SELECT * FROM users WHERE username = @username AND password = @password";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Runs once per record retrieved
                        while (reader.Read())
                        {
                            user = new User();
                            user.userID = reader.GetInt32("userID");
                            user.username = reader.GetString("username");
                            user.firstName = reader.GetString("firstName");
                            user.lastName = reader.GetString("lastName");
                            user.type = reader.GetChar("type");
                            user.isActive = reader.GetBoolean("isActive");
                        }
                    }
                }
            }
            return user;
        }

        public static long addUser(User user)
        {
            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    //SQL and Parameters
                    cmd.CommandText = "INSERT INTO users (username, password, firstName, lastName, type, isActive)" +
                        "VALUES (@username, @password, @firstName, @lastName, 'S', 1)";
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);

                    //Return the last inserted ID if successful
                    if(cmd.ExecuteNonQuery() > 0) return cmd.LastInsertedId;

                    return 0;
                }
            }
        }

        public static List<User> getUsers()
        {
            List<User> user = new List<User>();

            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    //SQL and Parameters
                    cmd.CommandText = "SELECT * FROM users";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Runs once per record retrieved
                        while (reader.Read())
                        {

                            user.Add(new User()
                            {
                                userID = reader.GetInt32("userID"),
                                username = reader.GetString("username"),
                                firstName = reader.GetString("firstName"),
                                lastName = reader.GetString("lastName"),
                                type = reader.GetChar("type"),
                                isActive = reader.GetBoolean("isActive"),
                            });
                        }
                    }
                }
            }
            return user;
        }

        public static void deleteUserCourse(int userID, int courseID)
        {
            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM uCourse WHERE userID = @userID AND courseID = @courseID";
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@courseID", courseID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void changeUser(User user)
        {
            using (var conn = new MySqlConnection(connstring.ToString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    //SQL and Parameters
                    cmd.CommandText = "UPDATE users SET username = @username, firstName = @firstName, lastName = @lastName, type = @type, isActive = @isActive WHERE userID = @userID";
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@type", user.type);
                    cmd.Parameters.AddWithValue("@isActive", user.isActive);
                    cmd.Parameters.AddWithValue("@userID", user.userID);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
