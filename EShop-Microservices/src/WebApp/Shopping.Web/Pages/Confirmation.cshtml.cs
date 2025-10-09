
namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;
        public void OnGetContact()
        {
            Message = "Your email was sent.";
        }

        public void OnOrderSubmitted() 
        {
            Message = "Your order was submitted successfully";
        }
    }
}
