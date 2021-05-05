using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace Tetrakis.Modules.Moderation
{
    public static class ModerationData
    {
        public static async Task<bool> IsSuperRole(IEnumerable<DiscordRole> roles, DiscordGuild guild)
        {
            foreach (var role in roles)
            {
                if (await IsSuperRole(role.Id, guild.Id))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static async Task<bool> IsSuperRole(ulong roleId, ulong guildId)
        {
            try
            {
                var connection = new SQLiteConnection(Program.DBPath);
                connection.Open();
                var statement = $"SELECT * FROM SuperRoles WHERE guild = {guildId} AND role = {roleId}";
                var cmd = new SQLiteCommand(statement, connection);

                var reader = await cmd.ExecuteReaderAsync();
                connection.Close();
                return reader.HasRows;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Problem reading database: " + e.Message);
            }

            return false;
        }
    }
}