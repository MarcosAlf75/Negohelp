
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Negohelp.Comun.Models
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class EmailAddressAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return true; // It's not required, so it's valid when null
			}

			string email = value.ToString();

			// Replace this regular expression with a more comprehensive one for email validation.
			string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

			return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PasswordFormatAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value is string password)
			{
				//Patron para tener almenos una Mayuscula en el string
				string upperCase = @"[A-Z]";
				//Patron para tener almenos una minuscula en el string
				string lowerCase = @"[a-z]";
				//Patron para tener almenos un numero en el string
				string number = @"[0-9]";

				if (!string.IsNullOrWhiteSpace(password) &&
					password.Length >= 8 &&
					Regex.IsMatch(password, upperCase) &&
					Regex.IsMatch(password, lowerCase) &&
					Regex.IsMatch(password, number))
				{
					return true;
				}


			}

			return false;
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class UserNameFormatAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value is string userName)
			{
				//Patron para tener almenos una Mayuscula en el string
				string upperCase = @"[A-Z]";
				//Patron para tener almenos una minuscula en el string
				string lowerCase = @"[a-z]";
				//Patron para tener almenos un numero en el string
				//string number = @"[0-9]";
				//Patron para caracteres especiales
				string patternCharacteres = @"[\W_]+";

				return !string.IsNullOrWhiteSpace(userName) &&
					userName.Length >= 5 &&
					userName.Length <= 10 &&
					Regex.IsMatch(userName, upperCase) &&
					Regex.IsMatch(userName, lowerCase) &&
					!Regex.IsMatch(userName, patternCharacteres);

			}

			return false;
		}
	}
}
