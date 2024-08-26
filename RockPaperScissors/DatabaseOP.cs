using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace RockPaperScissors
{
    internal class DatabaseOP
    {
        public async static void CheckUser(long userId, string username)
        { 
            using (var context = new BotDBContext())
            {
                var users = await context.Users.ToListAsync();

                bool found = false;
                foreach (var user in users)
                {
                    if (user.UserID == userId)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DBAddUser(userId, username, context);
                }
            }
        }

        private static async void DBAddUser(long userId, string username, BotDBContext context)
        {

            var user = new User();
            user.UserID = userId;
            user.Username = username;
            user.Score = 0;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public static async void AddScore(long userId)
        {
            using (var context = new BotDBContext())
            {
                var user = context.Users.SingleOrDefault(u => u.UserID == userId);

                user.Score++;

                await context.SaveChangesAsync();
            }
        }
    }
}
