# About

## What is JimmyBot?

JimmyBot is a custom discord bot that I wrote to add features and automate tasks for use in some small / local discord servers. Jimmy contains (and is planned to contain even more) features that are possibly useful in the server you and your friends hang out in and also some features that are probably not useful for you.

## Who is Jimmy?

Jimmy is just a robot with a name that begins with J. Any references to other people name Jimmy are purely coincidental and have no bearing on figures that exist outside of the internet. I reserve the right to rename Jimmy to Justin, Jordan, James, Jasmine, ~~Jackson~~, Jameson, or Josephine at any time.

## Features

The feature list below is broken down by modules, and further subdivided by command and/or functionality. Please note any features with stars (\*) or footnotes indicate features that are hyper specific to my discord server (and are probably implemented as guild-level commands anyway) and will likely not work on your server. Most of these features are based on inside jokes and/or references that are not worth explaining here.

### Music Module

The music module is Jimmy's most used feature. The module currently supports searching and links of videos and songs from YouTube and links from other music streaming services such as Spotify, SoundCloud, and Twitch.

Commands include:

- `/play [link / search query]` - plays or queues the requested track
- `/pause` - pauses or unpauses the current track
- `/skip` - skips the current track
- `/replay` - replays (rewinds to) the previous track
- `/queue` - displays the current queue of tracks
- `/leave` - exits the bot from the voice channel

All commands except for `/play` can be used via buttons on the now playing display in your bot channel instead of as slash commands.

### Configuration Module

The configuration module is a set of commands used to configure, test, and otherwise administer Jimmy.

Commands include:

- `/ping` - tests and confirms bot connectivity
- `/set-channel [channel]` - sets channel for bot related communications (music status, etc) (_REQUIRED BEFORE MUSIC MODULE USE_)

---

# Setup

## Libraries / Dependencies

Jimmy requires the following libraries:

- [Discord.Net](https://docs.discordnet.dev/)
- [Lavalink4Net](https://lavalink4net.angelobreuer.de/)
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Http

Jimmy also requires communication with an active Lavalink server, see [here](https://github.com/lavalink-devs/Lavalink) for installation and configuration information.

## Configuration

Jimmy requires a small amount of configuration before he is able to get up and running. Follow the basic instructions below and he should be all set.

1. Configure your `appsettings.json` file. Make sure the file contains a `Bot` object as well as a `Lavalink` object, with a `Token` and `Passphrase` string (respectively). Populate these objects with your configuration data from your Lavalink server and bot.
2. Make sure to run any configuration commands such as `/set-channel`. This command should create an additional configuration file required for the music bot to function correctly.

---

# Issues

## Bugs

For bug reporting please feel free to open an issue or submit a PR. I might even mention you in this readme (how special)!

## Features

For feature reporting also please feel free to open an issue or submit a PR. I love new ideas and might mention you again in this readme!

---

# Thanks and Credits

Thanks to the makers of all of the libraries and especially to Angelo from L4N who spent way too much time helping me through bug fixes.
