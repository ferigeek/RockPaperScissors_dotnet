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
        public static ILogger logger = factory.CreateLogger("Program");

        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
            var me = await bot.GetMeAsync();

            logger.LogInformation("{Description} : Bot initialized and is running...", "Bot");


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
                logger.LogWarning("{Description} : User sent a null message!", "Bot");
                return;
            }

            var bot = new TelegramBotClient(token);

            logger.LogInformation("{Description} : " + msg.Text, "User");

            var message = "Hello there! ❤️";

            await bot.SendTextMessageAsync(msg.Chat, message);

            logger.LogInformation("{Description} : Sent message \"" + message.ToString() + "\".", "Bot");
        }

        static async Task OnError(Exception exception, HandleErrorSource source)
        {
            logger.LogError("{Description} : " + exception, "User");
        }

        static async Task OnUpdate(Update update)
        {
            var bot = new TelegramBotClient(token);

            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                await bot.SendTextMessageAsync(query.Message!.Chat, "Invalid query recieved.");
                logger.LogWarning("{Description} : User sent unrelated message!", "User");
            }
        }
    }
}
