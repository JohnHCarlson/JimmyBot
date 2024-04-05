using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JamieBot {
    public class ConfigCommandsModule : InteractionModuleBase<SocketInteractionContext>{

        private readonly DiscordSocketClient _client;

        public ConfigCommandsModule(DiscordSocketClient client) {

            ArgumentNullException.ThrowIfNull(client);
            
            _client = client;
        }

        [SlashCommand("ping", description: "Tests bot connectivity.", runMode: RunMode.Async)]
        public async Task Ping() {
            await DeferAsync(ephemeral: true).ConfigureAwait(false);
            await FollowupAsync("pong", ephemeral: true).ConfigureAwait(false);
        }

        [SlashCommand("set-channel", description: "Sets the bot channel for status messages.", runMode: RunMode.Async)]
        public async Task SetChannel(SocketChannel channel) {

            await DeferAsync(ephemeral: true).ConfigureAwait(false);

            ServerChannel newChannel = new ServerChannel(Context.Guild.Id, channel.Id);
            ServerChannel.Serialize(newChannel);

            await FollowupAsync($"Set {channel.ToString()} as bot channel.");

        }
    }

    public class ServerChannel {

        public ulong serverId { get; set; }

        public ulong channelId { get; set; }

        private const string path = "server_config.json";

        public ServerChannel(ulong serverId, ulong channelId) {
            this.serverId = serverId;
            this.channelId = channelId;
        }

        public static void Serialize(ServerChannel newServerChannel) {

            List<ServerChannel> serverChannels = Deserialize();

            bool serverExists = false;
            foreach (ServerChannel currentChannel in serverChannels) {
                if(currentChannel.serverId == newServerChannel.serverId) { //Chgecks if server already has set bot channel
                    
                    currentChannel.channelId = newServerChannel.channelId;
                    serverExists = true;
                    break;
                }
            }

            if(!serverExists || serverChannels.Count == 0) {
                serverChannels.Add(newServerChannel);
            }
            string json = JsonSerializer.Serialize(serverChannels);
            File.WriteAllText(path, json);
        }

        public static List<ServerChannel> Deserialize() { 
        
            if(!File.Exists(path)) {
                return new List<ServerChannel>();
            }
            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<ServerChannel>>(json);

        }

        public static ulong GetBotChannel(ulong serverId) {

            List<ServerChannel> serverChannels = Deserialize();

            bool serverExists = false;
            foreach (ServerChannel currentChannel in serverChannels) {
                if (currentChannel.serverId == serverId) {
                    return currentChannel.channelId;
                }
            }
            return 0;
        }
    }
}
