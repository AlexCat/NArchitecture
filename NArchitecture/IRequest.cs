namespace NArchitecture
{
    public interface IRequest : IMessage
    {
    }

    public interface IRequest<out TResponse> : IRequest
    {
    }
}
