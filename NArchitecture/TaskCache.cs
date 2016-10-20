using System.Threading.Tasks;

namespace NArchitecture
{
    public static class TaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
