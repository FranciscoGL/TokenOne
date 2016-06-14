using System.ComponentModel.DataAnnotations;

namespace Authorization.Api.Models
{
    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string SecretKey { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}