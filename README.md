
# Paula's Cadenza
Paula's Cadenza is software that allows you to log in multiple "bots" concurrently in to Habbo Hotel (the flash version of the Hotel)

This was more-or-less a weekend "hackathon" project. I figured all of this out by using [AS3 Sorcerer](https://www.as3sorcerer.com/) to decompile the *Habbo.swf* flash file.

The DHM key exchange cryptography is a C# port of the [hurlant AS3 crypto library](https://crypto.hurlant.com/), specifically the *BigInteger* class library.

This is unfinished as Habbo Hotel is changing everything as they move away from Flash; There was no reason to continue working on it. But, some of you may like to take a look so I am going to keep it here.

The UI is WinForms/.NET Framework, all the other libraries are .NET Standard 2.0.