# EcsR3.Monogame

This is the Monogame flavour of EcsR3!

[![License][license-image]][license-url]
[![Nuget Version][nuget-image]][nuget-url]
[![Join Discord Chat][discord-image]][discord-url]
[![Documentation][gitbook-image]][gitbook-url]

## What is it?

It is an ECS style framework which puts architecture, design and flexibility above most other concerns.

It builds on top of the existing [EcsR3](https://github.com/EcsR3/ecsr3) framework and adds conventions and bootstrappers for Monogame specific scenarios.

## Getting started

As with all EcsR3 engine integrations you will need to create your own application instance (`EcsR3MonoGameApplication`), and then you just use that instead of your normal `Game` instance, like so:

```c#
static void Main()
{
	// No longer need this, as we use applications with EcsR3
	//using (var game = new Game1()) { game.Run(); }

	using(new DemoApplication()){}
}
```

### WHAT HAVE YOU DONE WITH MY `Game`

There is still a `Game` instance under the hood, but we abstract it away, so in almost all scenarios you wont need to touch the game as you will treat the `Application` as your entry point.

There are custom versions of most common monogame objects that you can inject into any of your classes, such as:

- `IEcsR3ContentManager`
- `IEcsR3Game`
- `IEcsR3GraphicsDevice`
- `IEcsR3GraphicsDeviceManager`
- `IEcsR3SpriteBatch`

## Docs

There is a book available which covers the main parts for the core EcsR3 framework which can be found here:

[![Documentation][gitbook-image]][gitbook-url]

Will add monogame specific documentation within this repo as time goes on, but for the moment hop on discord to know more.

There is also a demo application within the `Asteroids` folder.

[nuget-image]: https://img.shields.io/nuget/v/ecsr3.monogame.svg
[nuget-url]: https://www.nuget.org/packages/EcsR3/
[discord-image]: https://img.shields.io/discord/488609938399297536.svg
[discord-url]: https://discord.gg/bS2rnGz
[license-image]: https://img.shields.io/github/license/ecsr3/ecsr3.monogame.svg
[license-url]: https://github.com/EcsR3/ecsr3.monogame/blob/master/LICENSE
[gitbook-image]: https://img.shields.io/static/v1.svg?label=Documentation&message=Read%20Now&color=Green&style=flat
[gitbook-url]: https://ecsr3.gitbook.io/project/