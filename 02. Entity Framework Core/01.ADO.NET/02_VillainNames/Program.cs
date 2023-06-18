using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Security;

namespace _02_VillainNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection conection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            conection.Open();
            string query = "SELECT CONCAT(v.Name, ' - ', COUNT(mv.VillainId)) AS Output FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)";
            SqlCommand cmd = new SqlCommand(query, conection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string output = (string)reader["Output"];
                Console.WriteLine(output);
            }
        }
    }
}