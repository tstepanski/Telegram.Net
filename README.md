<h1 align="center">
   🤖 Telegram.NET 💬
</h1>

<p align="center">
    Hey! This is new Telegram API wrapper for .NET!
</p>

<h2>
   🧰 Tech info 
</h2>

- Language: C#
- Vesrion of language: 9.0
- Target framework: .NET 5.0

<h2>
    ❓ Why Telegram.NET?
</h2>

`Telegram.NET` is a flexible API wrapper. You can create your own extesions for library without contributing. For example, you can create a command service to create command easier. Also, if you don't want to create your own extensions, you can get <a href="https://github.com/denvot">DenVot</a>'s extensions for library. With `Telgram.NET` you can use classes wich provides interfaces for Telegram API. For example, if you have a `ITelegramChannel` instance, you can send a message to specified channel:

```csharp
await chat.SendMessageAsync("This is my first message! Yeah 👏"); //Realy easy!
```

<h1 align="center">
    🚀 Quick start 🚀
</h1>

First of all, we should initilize new instance of `TelegramClient` like this:

```csharp
var client = new TelegramClient("<YOUR_SECRET_TOKEN_HERE>");
```

We need to make sure everything working right. Try to get your client as `TelegramUser`.

```csharp
var me = client.Me;

Console.WriteLine(me);

/*
Output:
    User thebestbot with id 777
*/
```

If you want get messages you should use method `TelegramClient.Start()` like this:

```csharp
client.Start();
```

If you want stop receiving:

```csharp
client.Stop();
```

Full code:

```csharp
var client = new TelegramClient("<YOUR_SECRET_TOKEN_HERE>");
var me = client.Me;

Console.WriteLine(me);
```
