using Dapper;
using DapperTest.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace DapperTest
{
    public class Program
    {
        static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GameDB;Trusted_Connection=True";

        public static List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            var conn = ConnectionString;

            using (var connection = new SqlConnection(conn))
            {
                var data = connection.Query<Player>("SELECT * FROM Players");
                players = data.ToList();
                return players;
            }
        }

        public static Player GetPlayerById(int id)
        {
            var conn = ConnectionString;
            using (var connection = new SqlConnection(conn))
            {
                var player = connection.QueryFirstOrDefault<Player>($"SELECT * FROM Players WHERE Id=@PId",
                    new { PId = id });
                return player;
            }
        }

        public static void Delete(Player player)
        {
            var conn = ConnectionString;
            using (var connection = new SqlConnection(conn))
            {
                var rowAffected = connection.Execute(@"DELETE FROM Players WHERE Id=@PId",
                    new { PId = player.Id });
                if (rowAffected > 0) Console.WriteLine("Player updated succesfully");
            }
        }

        public static void Insert(Player player)
        {
            var conn = ConnectionString;

            using (var connection = new SqlConnection(conn))
            {
                var rowAffected = connection.Execute(@"
        INSERT INTO Players(Name,Score,IsStar)
        VALUES(@PName,@PScore,@PIsStar)
", new { PName = player.Name, PScore = player.Score, PIsStar = player.IsStar });

                if (rowAffected > 0)
                {
                    Console.WriteLine("Player added successfully");
                }

            }

        }


        public static void Update(Player player)
        {
            var conn = ConnectionString;
            using (var connection = new SqlConnection(conn))
            {
                var rowAffected = connection.Execute(@"
            UPDATE Players
            SET Name=@PName,Score=@PScore,IsStar=@PIsStar
            WHERE Id=@PId
        ", new { PName = player.Name, PScore = player.Score, PIsStar = player.IsStar, PId = player.Id });

                if (rowAffected > 0)
                {
                    Console.WriteLine("Player updated successfully");
                }
            };
        }

        static void Main(string[] args)
        {
            var player = GetPlayerById(4);
            if (player != null)
            {
                player.Name = "John Johnlu";
                player.Score = 67;
                player.IsStar = false;

                Update(player);
            }

            var players = GetPlayers();
            foreach (var p in players)
            {
                Console.WriteLine($"ID : {p.Id}");
                Console.WriteLine($"Name : {p.Name}");
                Console.WriteLine($"Score : {p.Score}");
                Console.WriteLine($"Is Star : {(p.IsStar ? "Yes" : "No")}");
            }

            //var player = new Player
            //{
            //    Name = "Kobby Brian",
            //    IsStar = true,
            //    Score = 100
            //};

            //Insert(player);

            //var player = GetPlayerById(1);
            //Console.WriteLine(player.Id);
            //Console.WriteLine(player.Name);


        }
    }
}
