namespace Session03.presentationLayer.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password And ConFirmPassword Doesn't Match")]
		public string ConfirmPassword { get; set; }
        public string Email { get; set; }
		public string Token { get; set; }

	}
}
