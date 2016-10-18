namespace NArchitecture.Security
{
    public interface IAuthorizeData
    {
        string Policy { get; }
        string Roles { get; }
    }
}
