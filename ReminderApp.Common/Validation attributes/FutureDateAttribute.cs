using System.ComponentModel.DataAnnotations;

namespace ReminderApp.Common.Validation_attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "Due date must be in the future.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            if (value is DateTime date)
            {
                return date > DateTime.UtcNow;
            }

            return false;
        }
    }
}
