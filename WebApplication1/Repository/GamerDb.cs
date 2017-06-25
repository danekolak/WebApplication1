using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{

    public class GamerDb
    {

        MySqlConnection connection;
        string conString = "SERVER=localhost; Database=gamer; UID=root;password=root; ";

        public List<Gamer> GetGamers()
        {
            connection = new MySqlConnection(conString);
            string selQuery = "SELECT * FROM gamers";
            MySqlCommand cmd = new MySqlCommand(selQuery, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<Gamer> listGamers = new List<Gamer>();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Gamer g1 = new Gamer()
                {
                    Id = Convert.ToInt32(dr["id"]),
                    FirstName = Convert.ToString(dr["firstname"]),
                    LastName = Convert.ToString(dr["lastname"]),
                    UserName = Convert.ToString(dr["username"]),
                    Password = Convert.ToString(dr["password"]),
                    RepeatPassword = Convert.ToString(dr["repeat_password"])
                };
                listGamers.Add(g1);
            }
            return listGamers;
        }


        public List<Gamer> GetGamersLogin()
        {
            connection = new MySqlConnection(conString);
            string selQuery = "SELECT username,password FROM gamers";
            MySqlCommand cmd = new MySqlCommand(selQuery, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<Gamer> listGamersLogin = new List<Gamer>();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Gamer g1 = new Gamer()
                {

                    UserName = Convert.ToString(dr["username"]),
                    Password = Convert.ToString(dr["password"]),

                };
                listGamersLogin.Add(g1);
            }
            return listGamersLogin;
        }






        public bool InsertGamer(Gamer gamer)
        {
            connection = new MySqlConnection(conString);
            string selQuery = "insert into gamers values(@id,@firstname,@lastname,@username,@password,@repeat_password)";
            MySqlCommand cmd = new MySqlCommand(selQuery, connection);
            cmd.Parameters.AddWithValue("@id", gamer.Id);
            cmd.Parameters.AddWithValue("@firstname", gamer.FirstName);
            cmd.Parameters.AddWithValue("@lastname", gamer.LastName);
            cmd.Parameters.AddWithValue("@username", gamer.UserName);
            cmd.Parameters.AddWithValue("@password", gamer.Password);
            cmd.Parameters.AddWithValue("@repeat_password", gamer.RepeatPassword);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1) return true; else return false;

        }

        public bool DeleteGamer(int id)
        {
            connection = new MySqlConnection(conString);
            string delQuery = "delete from gamers where id=@id";
            MySqlCommand cmd = new MySqlCommand(delQuery, connection);
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i >= 1) return true; else return false;
        }




    }
}
