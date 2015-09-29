using System;
using CommandLine;

namespace ActiveDirectoryQuery
{
    public class DefaultOptions
    {
        public DefaultOptions()
        {
            Domain = Environment.GetEnvironmentVariable("USERDOMAIN");
        }

        [Option(Required = true, HelpText = "Name of the principal to find, use * for wildcard search")]
        public string Name { get; set; }

        [Option('o', "output", HelpText = "Output type", Default = "console")]
        public string Output { get; set; }

        [Option('d', "domain", HelpText = "The Active Directory domain to use")]
        public string Domain { get; set; }

        [Option("detailed", HelpText = "Show principal details", Default = false)]
        public bool Detailed { get; set; }

        [Option("showmembership", HelpText = "If a principal was found, display it's members or membership")]
        public bool ShowMembership { get; set; }
    }
}