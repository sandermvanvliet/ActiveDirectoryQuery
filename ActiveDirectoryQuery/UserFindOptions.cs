using CommandLine;

namespace ActiveDirectoryQuery
{
    [Verb("userfind", HelpText = "Find user principals in Active Directory")]
    public class UserFindOptions : DefaultOptions
    {
    }
}