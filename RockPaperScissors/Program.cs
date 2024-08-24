//Rock Paper Scissors telegram bot

///<seealso cref="https://t.me/RockPaperScissors_dotnet_bot"/>

//Telegram.Bot imports
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
//ORM imports
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Text;


namespace RockPaperScissors
{
    internal class Program
    {
        private const string token = "__token__";

        static async Task Main(string[] args)
        {
            Log.CheckLogFile();

            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
            var me = await bot.GetMeAsync();

            await Log.LogInformation("Bot initialized and is running...");


            bot.OnMessage += OnMessage;
            bot.OnError += OnError;


            Console.ReadKey();

            await Log.LogWarning("Exiting...");

            using (var fs = new FileStream(@"./data/bot.log", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("\n*****\n");
                }
            }

            cts.Cancel();
        }



        static async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg == null)
            {
                await Log.LogError($"User({msg?.From}) sent a null message!");
                return;
            }

            var bot = new TelegramBotClient(token);

            switch (msg.Text)
            {
                case "/start":
                    {
                        await DatabaseOP.CheckUser(msg.From.Id, msg.From.Username);

                        await Log.LogInformation($"User({msg.From}) starts...");

                        await bot.SendTextMessageAsync(msg.Chat, "<b>Let's play Rock Paper Scissors!</b>\n" +
                            "Choose one of the options in the menu.\n/start | /rock | /paper | /scissors",
                            parseMode: ParseMode.Html,
                            protectContent: true,
                            replyParameters: msg.MessageId);

                        await Log.LogInformation($"Bot sent the /start instructions " +
                            $"to {msg.From}");
                        break;
                    }
                case "/rock":
                    {
                        await DatabaseOP.CheckUser(msg.From.Id, msg.From.Username);

                        await Log.LogInformation($"User({msg.From}) chose Rock.");

                        var random = new Random();
                        int botChoice = random.Next(1, 4);

                        switch (botChoice)
                        {
                            case 1:
                                {
                                    await Log.LogInformation("Bot chose Rock.");
                                    await Log.LogWarning("Tie!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\n" +
                                        "Bot's Choice: <b>Rock 🪨</b>\nResult: <b>Tie!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);


                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                            case 2:
                                {
                                    await Log.LogInformation("Bot Chose Paper.");
                                    await Log.LogWarning("Bot wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\n" +
                                        "Bot's Choice: <b>Paper 📃</b>\nResult: <b>Bot wins!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                            case 3:
                                {
                                    await Log.LogInformation("Bot chose Scissors.");
                                    await Log.LogWarning($"User({msg.From}) wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\n" +
                                        "Bot's Choice: <b>Scissors ✂️</b>\nResult: <b>You win!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation($"Sent result message to ({msg.From})");

                                    await DatabaseOP.AddScore(msg.From.Id);
                                    break;
                                }
                        }
                        break;
                    }
                case "/paper":
                    {
                        await DatabaseOP.CheckUser(msg.From.Id, msg.From.Username);

                        await Log.LogInformation($"User({msg.From}) chose Paper.");

                        var random = new Random();
                        int botChoice = random.Next(1, 4);

                        switch (botChoice)
                        {
                            case 1:
                                {
                                    await Log.LogInformation("Bot chose Rock.");
                                    await Log.LogWarning($"User({msg.From}) wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\n" +
                                        "Bot's Choice: <b>Rock 🪨</b>\nResult: <b>You win!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);


                                    await Log.LogInformation($"Sent result message to ({msg.From})");

                                    await DatabaseOP.AddScore(msg.From.Id);
                                    break;
                                }
                            case 2:
                                {
                                    await Log.LogInformation("Bot Chose Paper.");
                                    await Log.LogWarning("Tie!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\n" +
                                        "Bot's Choice: <b>Paper 📃</b>\nResult: <b>Tie!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                            case 3:
                                {
                                    await Log.LogInformation("Bot chose Scissors.");
                                    await Log.LogWarning("Bot wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\n" +
                                        "Bot's Choice: <b>Scissors ✂️</b>\nResult: <b>Bot wins!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                        }
                        break;
                    }
                case "/scissors":
                    {
                        await DatabaseOP.CheckUser(msg.From.Id, msg.From.Username);

                        await Log.LogInformation($"User({msg.From}) chose Scissors.");

                        var random = new Random();
                        int botChoice = random.Next(1, 4);

                        switch (botChoice)
                        {
                            case 1:
                                {
                                    await Log.LogInformation("Bot chose Rock.");
                                    await Log.LogWarning("Bot wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\n" +
                                        "Bot's Choice: <b>Rock 🪨</b>\nResult: <b>Bot wins!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);


                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                            case 2:
                                {
                                    await Log.LogInformation("Bot Chose Paper.");
                                    await Log.LogWarning($"User({msg.From}) wins!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\n" +
                                        "Bot's Choice: <b>Paper 📃</b>\nResult: <b>You win!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation("Sent result message");

                                    await DatabaseOP.AddScore(msg.From.Id);
                                    break;
                                }
                            case 3:
                                {
                                    await Log.LogInformation("Bot chose Scissors.");
                                    await Log.LogWarning("Tie!");

                                    await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\n" +
                                        "Bot's Choice: <b>Scissors ✂️</b>\nResult: <b>Tie!</b>",
                                        parseMode: ParseMode.Html,
                                        protectContent: true,
                                        replyParameters: msg.MessageId);

                                    await Log.LogInformation($"Sent result message to ({msg.From})");
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        await Log.LogError($"User({msg.From}) sent invalid message!");
                        await bot.SendTextMessageAsync(msg.Chat, "<b>You sent invalid message</b>\n" +
                            "Please use only the commands in the menu" +
                            "\n/start | /rock | /paper | /scissors",
                            parseMode: ParseMode.Html,
                            protectContent: true,
                            replyParameters: msg.MessageId);
                        break;
                    }
            }
        }

        static async Task OnError(Exception exception, HandleErrorSource source)
        {
            await Log.LogError($"{exception}");
        }
    }
}
