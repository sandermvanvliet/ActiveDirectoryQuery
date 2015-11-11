using System;
using CommandLine;

namespace ActiveDirectoryQuery
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments(args, typeof (GroupFindOptions), typeof (UserFindOptions))
                .MapResult(
                    (GroupFindOptions options) => FindGroup(options),
                    (UserFindOptions options) => FindUser(options),
                    errors => 1);
        }

        private static int FindUser(UserFindOptions options)
        {
            try
            {
                new UserPrincipalSearcher(options).FindGroup(options.Name);

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);

                return 1;
            }
        }

        private static int FindGroup(GroupFindOptions options)
        {
            try
            {
                new GroupPrincipalSearcher(options).FindGroup(options.Name);

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);

                return 1;
            }
        }
    }
}