<h1 align="center">
   🤖 Telegram.NET 💬
</h1>

<p align="center">
    Новая обертка для Telegram API!
</p>

<p align="center">
    <img src="https://github.com/denvot/TelegramNet/actions/workflows/dotnet.yml/badge.svg">
</p>

<p align="center">
    <a href="../README.md">
        <img src="../Images/BackToEngBut.png" width="50" title="Back to English language">
    </a>
</p>

Если Вы хотите протестировать библиотеку, скачайте ее на

`Менеджер пакетов (Package Manager)`:

```bash
Install-Package TelegramDotNet -Version 0.5.0
```

`.NET CLI`:

```bash
dotnet add package TelegramDotNet --version 0.5.0
```

<h2>
   🧰 Техническая информация 
</h2>

- Язык: C#
- Версия языка: 9.0
- Целевая платформа: .NET 5.0

<h2>
    ❓ Почему Telegram.NET?
</h2>

`Telegram.NET` - это универсальная и расширяемая обертка для Telegram API. Вы можете создавать собственные расширения для библиотеки без внесения изменений в код. Например, Вы можете создать собственный фреймворк для легкого создания команд. Если Вы не хоиите писать расширения, то можете взять их в репозиториях у <a href="https://github.com/denvot">DenVot</a>. В `Telegram.NET` каждый класс предоставляет удобный интерфейс для общения с Telegram API. Например, Вы можете отправить сообщение в определенный канал:

```csharp
await chat.SendMessageAsync("Это мое первое сообщение! Ура 👏"); //Очень легко!
```

<h2>

<h1 align="center">
    ✍️ Contributing ✍️
</h1>

Если Вы хотите добавить/изменить что-нибудь в `Telegram.NET`, Вы должны сделатьл пулл реквест для слияния веток. И да. **Не забудьте сделать тесты!**

<h1 align="center">
    🚀 Быстрый старт 🚀
</h1>

В первую очерель, мы должны инициализировать новый объект класса `TelegramClient` следующим образом:

```csharp
var client = new TelegramClient("<ВАШ_СЕКРЕТНЫЙ_ТОКЕН>");
```

We need to make sure everything working right. Try to get your client as `TelegramUser`.
Мы должны убедится, что все работает нормально. Давайте возьмем пользователя, который относится к данному клиенту.

```csharp
var me = client.Me;

Console.WriteLine(me);

/*
Выход:
    User thebestbot with id 777
*/
```

Если Вы хотите получать сообщения, то Вы должны использовать метод `TelegramClient.Start()`:

```csharp
client.Start();
```

Если хотите остановить принятие сообщений:

```csharp
client.Stop();
```

---

<h2>
💻 Полный код:
</h2>

```csharp
var client = new TelegramClient("<ВАШ_СЕКРЕТНЫЙ_ТОКЕН>");
var me = client.Me;

Console.WriteLine(me);
client.Start();

Console.WriteLine("Нажмите любую кнопку, чтобы выключить бота.");
Console.ReadKey();
client.Stop();
```

## <h2 align="center"> Поздравляю! Вы создали своего первого бота. 🥳 </h2>

---

<h1 align="center">
🤫 Больше интересных фишек 🤫
</h1>

Итак, мы включили нашего первого бота, класс. Но наш бот ничего не может! Давайте создадим простенького бота, который будет повторять за пользователем текст.

```csharp
namespace TheBestBotEverCreated
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            var client = new TelegramClient("<ВАШ_СЕКРЕТНЫЙ_ТОКЕН>");
            var me = client.Me;

            Console.WriteLine(me);
            client.Start();

            client.OnMessageReceived += OnMessage;

            Console.WriteLine("Нажмите любую кнопку, чтобы выключить бота.");
            Console.ReadKey();
            client.Stop();
        }

        public async Task OnMessage(TelegramMessage message)
        {
            var chat = message.Chat;

            await chat.SendMessageAsync(message.Text);
        }
    }
}
```

<p align="center">
    <img src="../Images/RepeatResult.png" width="500">
</p>
