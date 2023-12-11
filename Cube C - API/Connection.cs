using System;
using MySql.Data.MySqlClient;

namespace Cube_C___API;

public class Connection
{
    string connectionString = "Server=;Database=;User Id=;Password=;";
    
    public MySqlConnection GetConnection()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            Console.WriteLine("Connexion à la base de données réussie!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur de la connection " + ex.Message);
        }

        return connection;
    }
}