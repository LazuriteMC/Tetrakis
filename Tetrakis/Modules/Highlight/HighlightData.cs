using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Tetrakis.Modules.Highlight
{
    public static class HighlightData
    {
        public static async Task NewHighlight(ulong userId, ulong guildId, string word)
        {
            var connection = new SQLiteConnection(Program.DBPath);
            connection.Open();

            var cmd = new SQLiteCommand(connection)
            {
                CommandText = $"INSERT INTO Highlights (user, guild, word) VALUES ({userId}, {guildId}, '{word}');"
            };

            await cmd.ExecuteNonQueryAsync();
            connection.Close();
        }
        
        public static async Task<List<string>> GetWords(ulong userId, ulong guildId)
        {
            var words = new List<string>();
            
            try
            {
                var connection = new SQLiteConnection(Program.DBPath);
                connection.Open();
                var statement = $"SELECT * FROM Highlights WHERE user = {userId} AND guild = {guildId}";
                var cmd = new SQLiteCommand(statement, connection);
                    
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        words.Add(reader["word"].ToString());
                    }
                }
                
                connection.Close();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Problem reading database: " + e.Message);
            }

            return words;
        }
        
        public static async Task RemoveUser(ulong userId) {
            var connection = new SQLiteConnection(Program.DBPath);
            connection.Open();

            var cmd = new SQLiteCommand(connection)
            {
                CommandText = $"DELETE FROM Highlights WHERE user = {userId};"
            };

            await cmd.ExecuteNonQueryAsync();
            connection.Close(); 
        }
    }
}