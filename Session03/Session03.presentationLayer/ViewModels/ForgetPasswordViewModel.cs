namespace Session03.presentationLayer.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; }
	}
}
