using System.ComponentModel.DataAnnotations;

namespace NArchitecture.Tests
{
    public class SimpleMessage : IMessage
    {
        [Required]
        public string RequiredAttribute { get; set; }
    }
}
