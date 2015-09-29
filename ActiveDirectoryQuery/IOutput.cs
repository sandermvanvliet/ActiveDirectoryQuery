using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryQuery
{
    public interface IOutput
    {
        void WriteGroup(GroupPrincipal group);
        void WriteUser(UserPrincipal user);
    }
}