using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;

using Microsoft.Extensions.Logging;


namespace RockPaperScissors
{
    internal class Program
    {
        private const string token = "_Token_";

        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
            var me = await bot.GetMeAsync();

            Console.WriteLine("Bot initialized and is running ...");

            bot.OnMessage += OnMessage;
            bot.OnError += OnError;
            bot.OnUpdate += OnUpdate;


            Console.ReadKey();
            Console.WriteLine("Exiting ...");

            cts.Cancel();
        }



        static async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg == null)
            {
                return;
            }

            var bot = new TelegramBotClient(token);

            await bot.SendTextMessageAsync(msg.Chat, "Hello There! ❤️");
        }

        static async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception);
        }

        static async Task OnUpdate(Update update)
        {
            var bot = new TelegramBotClient(token);

            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                await bot.SendTextMessageAsync(query.Message!.Chat, "Invalid query recieved.");
            }
        }
    }
}
