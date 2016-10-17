using System.ComponentModel.DataAnnotations;

namespace NArchitecture.Tests.Validation
{
    public class SimpleMessage : IMessage
    {
        [Required]
        public string RequiredAttribute { get; set; }
    }
}
