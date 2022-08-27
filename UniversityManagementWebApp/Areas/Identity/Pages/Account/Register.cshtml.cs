// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using UniversityManagementWebApp.Areas.Identity.Data;

namespace UniversityManagementWebApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel

    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UniversityAppUser> _signInManager;
        private readonly UserManager<UniversityAppUser> _userManager;
        private readonly IUserStore<UniversityAppUser> _userStore;
        private readonly IUserEmailStore<UniversityAppUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            RoleManager<IdentityRole> roleManager,
            UserManager<UniversityAppUser> userManager,
            IUserStore<UniversityAppUser> userStore,
            SignInManager<UniversityAppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }



        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {

            public string RoleName { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            ///
            [Required]
            [EmailAddress]
            [StringLength(maximumLength:50, ErrorMessage = "Email Number not be more then 50 characters")]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Pan Number must be 10 characters")]
            public string PanNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void SendMail()
        {
            #region send-mail
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("University Web App", "hemant7331@gmail.com"));
            message.To.Add(MailboxAddress.Parse(Input.Email));

            message.Subject = "Your Registration is Successful";
            message.Body = new TextPart("plain")
            {
                Text = $"Dear {Input.Email} ! Thanks for registering with us.Please login using this link <https://localhost:7043/Identity/Account/Login>"

            };
            
            #region private data
            string email = "hemant7331@gmail.com";
            string password = "uoipowtazadknfrz";
            #endregion

            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(email, password);
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                smtpClient.Disconnect(true);
                smtpClient.Dispose();
            }
            #endregion
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
           
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

      

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.PanNumber = Input.PanNumber;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);


                if (result.Succeeded)
                {
                    SendMail();
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Operator");

                    var userId = await _userManager.GetUserIdAsync(user);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Registration success: Check you mail for confermation.");
                        return Page();
                        //TempData["registration"] = "register";
                        //return RedirectToAction("Index","Home");
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        // return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private UniversityAppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UniversityAppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UniversityAppUser)}'. " +
                    $"Ensure that '{nameof(UniversityAppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<UniversityAppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UniversityAppUser>)_userStore;
        }
    }
}
