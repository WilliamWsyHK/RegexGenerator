# RegexGenerator - A language-independent regex pattern generator written in C# and .NET Standard 1.0.

### Please consider donate this project to support continue development.  
[![Donate](https://img.shields.io/badge/Donate-PayPal-blue.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=76EM5QKU7RP6W) Click the button!

![Donate](https://img.shields.io/badge/Donate-Bitcoin-yellow.svg)
Address: 19inR1nynLZ93Q72v1cqoVoDesN7xKbDu9

![Donate](https://img.shields.io/badge/Donate-Ethereum-blue.svg)
Address: 0x1226378d71DB5bf16DF0A2fe52C3c7917739da0A

## Introduction

![License](https://img.shields.io/badge/License-MIT-blue.svg)

This project is intended to help developers to generate complex regex pattern syntax by combining basic pattern blocks.

## Language Support

- .NET
- ECMAScript (Typically known implementation is JavaScript, and the superset TypeScript)
- (Others need your contributions!)

## Example

By default, you will generate regex pattern syntax for .NET.
``` C#
using System.Text.RegularExpressions;
using WilliamWsy.RegexGenerator;

// ...

var text = "2017";
var regexGenerator = new RegexGenerator(/*RegexLanguage.DotNet*/)
    .AddPositiveLookbehindAssertion(@"\b20")
    .Add(@"\d", min: 2, max: 2)
    .Add(@"\b");

// Check the generated regex pattern syntax.
var generatedPattern = regexGenerator.ToString(); // = @"(?<=\b20)\d{2}\b"

// Only for RegexLanguage.DotNet: Create a native .NET Regex object to test the pattern!
Regex generatedRegex = regexGenerator.Create();
var match = generatedRegex.Match(text);

// ...
```

And ECMAScript,
``` C#
using System.Text.RegularExpressions;
using WilliamWsy.RegexGenerator;

// ...

var text = "2017";
var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
    .AddPositiveLookbehindAssertion(@"\b20")
    .Add(@"\d", min: 2, max: 2)
    .Add(@"\b");

// Check the generated regex pattern syntax.
var generatedPattern = regexGenerator.ToString(); // = @"/(?<=\b20)\d{2}\b/"

// ...
```

## Contribute

Create issues / pull requests are all welcome. Please create test cases as appropriate in the mean time you add a new class / function.

## License

MIT License; Always welcome business / enterprise to register their name here when using this project!
