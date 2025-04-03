namespace HumanCapitalManagement.Web.Models.Accounts
{
    public class CreateAccountModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string RoleId { get; set; }
    }
}
