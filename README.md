Redis on Windows New Relic Plugin
=================================

A New Relic monitoring plugin for Redis instances running on Windows.

What's different about Windows, you ask? Mostly that Windows doesn't usually
have Python or Ruby installed, so installing them just to monitor Redis seems
excessive.


## Building

    git submodule update --init
    .\build.cmd

A new file `RedisWindowsNewRelicPlugin.zip` will appear like magic.


## License

Licensed under the MIT license. See `LICENSE.md`.
