using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Configuration;
using System.Threading.Tasks;

using System.IdentityModel.Tokens;
using System.IdentityModel.Claims;
using Microsoft.Identity.Client;
using HappyOrSad.Models;
using HappyOrSad.TokenStorage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin;

namespace HappyOrSad
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        // The appId is used by the application to uniquely identify itself to Azure AD.
        // The appSecret is the application's password.
        // The redirectUri is where users are redirected after sign in and consent.
        // The graphScopes are the Microsoft Graph permission scopes that are used by this sample: User.Read Mail.Send
        private static string appId = ConfigurationManager.AppSettings["ida:AppId"];
        private static string appSecret = ConfigurationManager.AppSettings["ida:AppSecret"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        private static string graphScopes = ConfigurationManager.AppSettings["ida:GraphScopes"];

        public void ConfigureAuth(IAppBuilder app)
        {
           
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(HappyOrSadContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                },
                
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                // The `Authority` represents the Microsoft v2.0 authentication and authorization service.
                // The `Scope` describes the permissions that your app will need. See https://azure.microsoft.com/documentation/articles/active-directory-v2-scopes/                    
                ClientId = appId,
                Authority = "https://login.microsoftonline.com/common/v2.0",
                PostLogoutRedirectUri = redirectUri,
                RedirectUri = redirectUri,
                Scope = "openid email profile offline_access " + graphScopes,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    // In a real application you would use IssuerValidator for additional checks, 
                    // like making sure the user's organization has signed up for your app.
                    //     IssuerValidator = (issuer, token, tvp) =>
                    //     {
                    //         if (MyCustomTenantValidation(issuer)) 
                    //             return issuer;
                    //         else
                    //             throw new SecurityTokenInvalidIssuerException("Invalid issuer");
                    //     },
                },
                //ResponseType = "id_token token",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async (context) =>
                    {
                        var code = context.Code;
                        string signedInUserID = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                        ConfidentialClientApplication cca = new ConfidentialClientApplication(
                            appId,
                            redirectUri,
                            new ClientCredential(appSecret),
                            new SessionTokenCache(signedInUserID, context.OwinContext.Environment["System.Web.HttpContextBase"] as HttpContextBase));

                        string[] scopes = graphScopes.Split(new char[] { ' ' });

                        AuthenticationResult result = await cca.AcquireTokenByAuthorizationCodeAsync(scopes, code);
                    },
                    AuthenticationFailed = (context) =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Error?message=" + context.Exception.Message);
                        return Task.FromResult(0);
                    }
                }
            });

            //Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "14e52a51-0cb4-401d-9325-1a879798ef0a",
            //    clientSecret: "HXS7BbzPKbfWafjBgm8bVnq");
        }
    }
}