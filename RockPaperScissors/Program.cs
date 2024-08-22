using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace RockPaperScissors
{
    internal class Program
    {
        private const string token = "__token__";

        public static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        public static ILogger logger = factory.CreateLogger<Program>();

        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
            var me = await bot.GetMeAsync();

            logger.LogInformation("Bot initialized and is running...");


            bot.OnMessage += OnMessage;
            bot.OnError += OnError;
            bot.OnUpdate += OnUpdate;


            Console.ReadKey();
            logger.LogWarning("Exiting...");

            cts.Cancel();
        }



        static async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg == null)
            {
                logger.LogError($"User({msg.From}) sent a null message!");
                return;
            }

            var bot = new TelegramBotClient(token);


            if (msg.Text == "/start")
            {
                logger.LogInformation($"User({msg.From}) starts...");

                await bot.SendTextMessageAsync(msg.Chat, "<b>Let's play Rock Paper Scissors!</b>\nChoose one of the options in the menu.\n/start | /rock | /paper | /scissors",
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: msg.MessageId);

                logger.LogInformation($"Bot sent the /start instructions to {msg.From}");
            }
            else if (msg.Text == "/rock")
            {
                logger.LogInformation($"User({msg.From}) chose Rock.");

                var random = new Random();
                int botChoice = random.Next(1, 4);

                switch (botChoice)
                {
                    case 1:
                        {
                            logger.LogInformation("Bot chose Rock.");
                            logger.LogWarning("Tie!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\nBot's Choice: <b>Rock 🪨</b>\nResult: <b>Tie!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            
                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                    case 2:
                        {
                            logger.LogInformation("Bot Chose Paper.");
                            logger.LogWarning("Bot wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\nBot's Choice: <b>Paper 📃</b>\nResult: <b>Bot wins!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                    case 3:
                        {
                            logger.LogInformation("Bot chose Scissors.");
                            logger.LogWarning($"User({msg.From}) wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Rock 🪨</b>\nBot's Choice: <b>Scissors ✂️</b>\nResult: <b>You win!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                }
            }
            else if (msg.Text == "/paper")
            {
                logger.LogInformation($"User({msg.From}) chose Paper.");

                var random = new Random();
                int botChoice = random.Next(1, 4);

                switch (botChoice)
                {
                    case 1:
                        {
                            logger.LogInformation("Bot chose Rock.");
                            logger.LogWarning($"User({msg.From}) wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\nBot's Choice: <b>Rock 🪨</b>\nResult: <b>You win!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);


                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                    case 2:
                        {
                            logger.LogInformation("Bot Chose Paper.");
                            logger.LogWarning("Tie!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\nBot's Choice: <b>Paper 📃</b>\nResult: <b>Tie!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                    case 3:
                        {
                            logger.LogInformation("Bot chose Scissors.");
                            logger.LogWarning("Bot wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Paper 📃</b>\nBot's Choice: <b>Scissors ✂️</b>\nResult: <b>Bot wins!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                }
            }
            else if (msg.Text == "/scissors")
            {
                logger.LogInformation($"User({msg.From}) chose Scissors.");

                var random = new Random();
                int botChoice = random.Next(1, 4);

                switch (botChoice)
                {
                    case 1:
                        {
                            logger.LogInformation("Bot chose Rock.");
                            logger.LogWarning("Bot wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\nBot's Choice: <b>Rock 🪨</b>\nResult: <b>Bot wins!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);


                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                    case 2:
                        {
                            logger.LogInformation("Bot Chose Paper.");
                            logger.LogWarning($"User({msg.From}) wins!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\nBot's Choice: <b>Paper 📃</b>\nResult: <b>You win!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation("Sent result message");
                            break;
                        }
                    case 3:
                        {
                            logger.LogInformation("Bot chose Scissors.");
                            logger.LogWarning("Tie!");

                            await bot.SendTextMessageAsync(msg.Chat, "Your Choice: <b>Scissors ✂️</b>\nBot's Choice: <b>Scissors ✂️</b>\nResult: <b>Tie!</b>",
                                parseMode: ParseMode.Html,
                                protectContent: true,
                                replyParameters: msg.MessageId);

                            logger.LogInformation($"Sent result message to ({msg.From})");
                            break;
                        }
                }
            }
            else
            {
                logger.LogError($"User({msg.From}) sent invalid message!");
                await bot.SendTextMessageAsync(msg.Chat, "<b>You sent invalid message</b>\nPlease use only the commands in the menu\n/start | /rock | /paper | /scissors");
            }
        }

        static async Task OnError(Exception exception, HandleErrorSource source)
        {
            logger.LogError($"{exception}");
        }

        static async Task OnUpdate(Update update)
        {
            var bot = new TelegramBotClient(token);

            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                await bot.SendTextMessageAsync(query.Message!.Chat, "Invalid query recieved.");
                logger.LogWarning($"Recieved and Update! from {update.Id}");
            }
        }
    }
}
