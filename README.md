# Getting started

There you'll learn how to create your first telegram bot with Telegram.NET.

First, you need to install the Telegram.NET NuGet package \(id: TelegramDotNet\).

**Package manager:**

```bash
Install-Package TelegramDotNet -Version 0.5.4
```

**.NET CLI:**

```bash
dotnet add package TelegramDotNet --version 0.5.4
```

We've installed Telegram.NET, nice! Let's start coding.

The first thing we need to do is decide on task that our bot will perform. Our bot will get users' profiles from GitHub API.

So, let's create a new class `Bot` where will be method `RunAsync`.

```csharp
using System.Threading.Tasks;
using TelegramNet;

namespace TelegramNetApplication
{
    public class Bot
    {
        private readonly string _token;
        
        public Bot(string token) //Bot token
        {
            _token = token;
        }

        public async Task RunAsync()
        {
            //Initializing new TelegramClient instance, wich provide powerful interface to Telegram API
            var client = new TelegramClient(_token, true); 
        
            //Start pooling (getting updates from Telegram server).
            client.Start();
            
            await Task.Delay(-1);
        }
    }
}
```

Add the following code to the `Main`

```csharp
namespace TelegramNetApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Bot("<YOUR_TOKEN_HERE>")
                .RunAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}
```

If you've done all right, in the console you'll see the following text:

```bash
  _____      _                                  _  _  ___  _____
 |_   _|___ | | ___  __ _  _ _  __ _  _ __     | \| || __||_   _|
   | | / -_)| |/ -_)/ _` || '_|/ _` || '  \  _ | .` || _|   | |
   |_| \___||_|\___|\__, ||_|  \__,_||_|_|_|(_)|_|\_||___|  |_|
                    |___/
[TELEGRAM API 13:26] First update response got. Count: 0.
```

## Events

We've connected to Telegram API server, nice. Let's subscribe events

#### Events support for `TelegramClient`

| Event name | Description |
| :--- | :--- |
| OnMessageReceived | Fires when the client has received a new message. |
| OnMessageEdited | Fires when the message has edited. |
| OnChannelPost | Fires when the client has received a new post. |
| OnChannelPostEdited | Fires when the post has edited. |

All events have the same delegate:

```csharp
public delegate Task MessageActionHandler(TelegramMessage message);
```

Let's subscribe on a `OnMessageReceived`event.

```csharp
using System.Threading.Tasks;
using TelegramNet;
using TelegramNet.Entities;

namespace TelegramNetApplication
{
    public class Bot
    {
        private readonly string _token;
        
        public Bot(string token)
        {
            _token = token;
        }

        public async Task RunAsync()
        {
            var client = new TelegramClient(_token, true);
            
            client.Start();
            
            client.OnMessageReceived += OnMessage; //Subscribing on the event
            
            await Task.Delay(-1);
        }

        private async Task OnMessage(TelegramMessage message)
        {
            //If message. Chat is instance of the TelegramChat.
            if(message.Chat is TelegramChat chat)
            {
                //If the message hase a text.
                if (message.Text.HasValue)
                {
                    //Send message with a same text.
                    await chat.SendMessageAsync(message.Text.Value);
                }
                else //Else we send an error notification.
                {
                    await chat.SendMessageAsync("There is no text!");
                }
            }
        }
    }
}
```

![Result](.gitbook/assets/image%20%281%29.png)

Now we need to create a method that will get user info from the GitHub server. There is the code below:

```csharp
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelegramNetApplication
{
    public static class GitHubRequester
    {
        public static async Task<GitHubUser> GetUserAsync(string username)
        {
            //Initialize a new HttpClient instance
            var client = new HttpClient();
            //Configuring the User-Agent
            client.DefaultRequestHeaders.Add("User-Agent", "Telegram.NET Agent");
            //Building request-uri
            string url = "https://api.github.com/users/" + username;
            
            //GET request
            var response = await client.GetAsync(url);
            
            //Let's read server's response as string.
            string asString = await response.Content.ReadAsStringAsync();

            try
            {
                //Deserializing...
                return JsonSerializer.Deserialize<GitHubUser>(asString);
            }
            catch (Exception)
            {
                return null;
            }
            //Nice :)
        }
    }

    public class GitHubUser //GitHub user representation
    {
        [JsonPropertyName("login")]
        public string Username { get; set; }
        
        [JsonPropertyName("followers")]
        public int Followers { get; set; }
        
        [JsonPropertyName("following")]
        public int Following { get; set; }
        
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
        
        public bool IsValid() => Username != null;
    }
}
```

Let's rewrite the OnMessage method like this:

```csharp
private async Task OnMessage(TelegramMessage message)
{
    //If message. Chat is instance of the TelegramChat.
    if(message.Chat is TelegramChat chat)
        if (message.Text.HasValue)
        {
            //Getting the GitHub user's info
            var user = await GitHubRequester.GetUserAsync(message.Text.Value);

            //If no user has found
            if (!user.IsValid())
            {
                await chat.SendMessageAsync("**User not found**");
                return;
            }
            //Sending response
            await chat.SendMessageAsync(
                $"**Username:** {user.Username}\n**Followers:** {user.Followers}\n**Following:** {user.Following}");
        }
        else
        {
            await chat.SendMessageAsync("There is no text!");
        }
}
```

![Success result](.gitbook/assets/image%20%282%29.png)

![Error result](.gitbook/assets/image%20%284%29.png)

