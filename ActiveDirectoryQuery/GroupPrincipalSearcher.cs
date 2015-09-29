using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectoryQuery
{
    public class GroupPrincipalSearcher
    {
        private readonly GroupFindOptions _options;
        private readonly IOutput _output;

        public GroupPrincipalSearcher(GroupFindOptions options)
        {
            _options = options;
            _output = OutputFactory.FromOptions(options);
        }

        public void FindGroup(string name)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain, _options.Domain))
            {
                var groupPrincipal = new GroupPrincipal(principalContext) { Name = name };

                var searcher = new PrincipalSearcher(groupPrincipal);

                var searchResult = searcher.FindAll();

                foreach (var group in searchResult.OfType<GroupPrincipal>())
                {
                    _output.WriteGroup(group);
                }
            }
        }
    }
}