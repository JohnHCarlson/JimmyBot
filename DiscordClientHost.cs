namespace Jamie;

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Lavalink4NET;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class DiscordClientHost : IHostedService {

    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public DiscordClientHost(DiscordSocketClient client, InteractionService service, IServiceProvider serviceProvider, IConfigurationRoot configuration) {

        _client = client;
        _interactionService = service;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        _client.InteractionCreated += InteractionCreated;
        _client.Ready += ClientReady;

        await _client
            .LoginAsync(TokenType.Bot, getToken())
            .ConfigureAwait(false);

        await _client
            .StartAsync()
            .ConfigureAwait(false);
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        _client.InteractionCreated -= InteractionCreated;
        _client.Ready -= ClientReady;

        await _client
            .StopAsync()
            .ConfigureAwait(false);
    }

    private Task InteractionCreated(SocketInteraction interaction) {
        var interactionContext = new SocketInteractionContext(_client, interaction);
        return _interactionService!.ExecuteCommandAsync(interactionContext, _serviceProvider);
    }

    private async Task ClientReady() {

        await _interactionService
            .AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider)
            .ConfigureAwait(false);



#if DEBUG
        try {
            await _interactionService
                .RegisterCommandsToGuildAsync(ulong.Parse(_configuration.GetSection("TestInfo")["Guild"]))
                .ConfigureAwait(false);
        }
        catch(Exception ex) {
            Console.WriteLine(ex.ToString());
        }
#else
        try {
            await _interactionService
                .RegisterCommandsGloballyAsync(true)
                .ConfigureAwait(false);
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
#endif

    }


    private string getToken() {

        return _configuration.GetSection("Bot")["Token"];
    }
}
