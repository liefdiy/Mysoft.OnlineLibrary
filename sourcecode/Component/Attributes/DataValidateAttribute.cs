using System;
using System.ComponentModel.DataAnnotations;

namespace Mysoft.Platform.Component.Attributes
{
	public class DataValidateAttribute : ValidationAttribute
	{
		public int MinValue { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				int val = -1;
				Int32.TryParse(value.ToString(), out val);
				if (val < MinValue)
				{
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			}
			return ValidationResult.Success;
		}
	}
}