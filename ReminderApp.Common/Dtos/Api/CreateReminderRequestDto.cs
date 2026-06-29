using ReminderApp.Common.Validation_attributes;
using System.ComponentModel.DataAnnotations;

namespace ReminderApp.Common.Dtos.Api
{
    public class CreateReminderRequestDto
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [MaxLength(512)]
        [MinLength(1)]
        [Required]
        public required string Message { get; set; }

        [Required]
        [FutureDateAttribute]
        public required DateTime SendAt { get; set; }
    }
}
