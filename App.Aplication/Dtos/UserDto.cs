namespace App.Application.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool RememberMe { get; set; }

        public string FgPwActivationKey { get; set; }
    }
}