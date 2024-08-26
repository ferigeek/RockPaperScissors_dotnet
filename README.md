A simple rock paper scissors telegram bot written with C#/.NET.

# Goals of this project

- Working with [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot) library
- Implement logging
    - Logging in the console
    - Logging in a file
- Practice Asynchronous programming
- Working with a database
    - Use an ORM
    - Use dotnet Entity Framework
    - Use SQLite

---

# Requirements

- .NET 8
- [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot)

---

# Setting up

1. Change the `__token__` in the `Program.cs` to yours.
2. Build the project.
3. At the same directory as the output file, make a `data` directory and copy the `botDB.sqlite` at the `project/data` to the created directory.
Done!