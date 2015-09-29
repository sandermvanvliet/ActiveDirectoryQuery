using CommandLine;

namespace ActiveDirectoryQuery
{
    [Verb("groupfind", HelpText = "Find group principals in Active Directory")]
    public class GroupFindOptions : DefaultOptions
    {
    }
}