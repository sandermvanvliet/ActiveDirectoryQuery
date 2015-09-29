using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectoryQuery
{
    public class UserPrincipalSearcher
    {
        private readonly UserFindOptions _options;
        private readonly IOutput _output;

        public UserPrincipalSearcher(UserFindOptions options)
        {
            _options = options;
            _output = OutputFactory.FromOptions(options);
        }

        public void FindGroup(string name)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain, _options.Domain))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalContext, name);

                if (userPrincipal != null)
                {
                    _output.WriteUser(userPrincipal);
                }
            }
        }

        private void DumpUserPrincipal(UserPrincipal userPrincipal)
        {
            Console.WriteLine(userPrincipal.Name);

            if (_options.ShowMembership)
            {
                var groups = userPrincipal.GetAuthorizationGroups();

                try
                {
                    var allGroups = groups.ToList();

                    foreach (var group in allGroups.OrderBy(g => g.Name))
                    {
                        Console.WriteLine(group.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Environment.ExitCode = 1;
                }
            }
        }
    }
}