﻿using DSPrima.WcfUserSession.Proxy;
using PCHI.WcfServices.API.PCHIServices.InterfaceContracts.Model;
using PCHI.WcfServices.API.PCHIServices.InterfaceContracts.Users;
using PCHI.WcfServices.API.PCHIServices.InterfaceProxies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHI.WcfServices.API.PCHIServices.InterfaceProxies.Users
{
    /// <summary>
    /// Proxy class for the UserService
    /// </summary>
    public class UserClient : BaseClient<IUserService>, IUserService
    {
        /// <summary>
        /// Logs the user in
        /// If Two factor authentication is required the authentication code for that is automatically send to the user
        /// </summary>
        /// <param name="userName">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>An operation indicating success with the Data variable indicating if Two factor authentication is required (true) or not (false)</returns>
        public OperationResultAsBool Login(string userName, string password)
        {
            return this.Channel.Login(userName, password);
        }

        /// <summary>
        /// Authenticates the second step in the authentication process
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="code">The authentication code</param>
        /// <returns>True if success, false otherwise</returns>
        public bool TwoFactorAuthenticate(string username, string code)
        {
            return this.Channel.TwoFactorAuthenticate(username, code);
        }

        /// <summary>
        /// Checks if the user has multiple roles and/or patients.
        /// If the user has 1 or more Patients and no additional roles, the PatientProxy role is automatically selected and Authentication/Login is automatically completed. It is up to the website to determine which Patient the user is using/wants to use.
        /// If the user has only 1 Role, this Role is automatically selected for the user and Authentication/Login is automatically completed.
        /// If the user has more then 1 Role or 1 Role in addition to the patients. These roles are in the Strings variable and the User has to select which Role to use.
        /// </summary>
        /// <param name="username">The username of the user. If the username is null, the current authenticated User for the session is used instead.</param>
        /// <returns>An operationResult inidicating success or failure. The Strings variable is filled with Role Name(s) the user has access to.</returns>
        public OperationResultAsLists UserHasMultipleRoles(string username)
        {
            return this.Channel.UserHasMultipleRoles(username);
        }

        /// <summary>
        /// Selects the Role to be used
        /// </summary>
        /// <param name="username">The name of the user to set the Patient for</param>
        /// <param name="role">The role name the user has selected</param>
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResult SelectRole(string username, string role)
        {
            return this.Channel.SelectRole(username, role);
        }

        /// <summary>
        /// Gets a list of all available patients for the user in the current Session
        /// </summary>
        /// <returns>An operation Result indicating success or failure with the list of available users fill in the Strings  variable</returns>
        public OperationResultAsDictionary GetPatientsForUser()
        {
            return this.Channel.GetPatientsForUser();
        }

        /// <summary>
        /// Logs the user out and ends the session
        /// </summary>
        public void Logout()
        {
            this.Channel.Logout();
        }

        /// <summary>
        /// To be called to complete the Registration of a user
        /// </summary>        
        /// <param name="confirmationToken">The confirmation token that was send to the user</param>
        /// <param name="userName">The new user name </param>
        /// <param name="password">The password for the user</param>
        /// <param name="email">The new email</param>
        /// <param name="mobileNumber">The new mobile number</param>
        /// <param name="provider">The provider to set for the user</param>
        /// <param name="securityQuestion">The Security question</param>
        /// <param name="securityAnswer">The security Answer</param>
        /// <returns>The result of completing indicating success or failure</returns>        
        public OperationResult CompleteRegistration(string confirmationToken, string userName, string password, string email, string mobileNumber, string provider, string securityQuestion, string securityAnswer)
        {
            return this.Channel.CompleteRegistration(confirmationToken, userName, password, email, mobileNumber, provider, securityQuestion, securityAnswer);
        }

        /// <summary>
        /// Checks if the user as encrypted in the confirmation token has already completed the registration process or not.
        /// The confirmation token is the token created and send to the user's email when the user is created. This token is reusable and never expires.
        /// </summary>
        /// <param name="confirmationToken">The token as created by the user creation process</param>
        /// <returns>True if the user has completed registration. False if the user doesn't exist or has not completed the registration process.</returns>
        public OperationResultAsBool UserCompletedRegistration(string confirmationToken)
        {
            return this.Channel.UserCompletedRegistration(confirmationToken);
        }

        /// <summary>
        /// Sends a two stage authentication code to the current user if, and only if, the user has a stwo stage authentication provider set.
        /// The Data variable in the OperationResultAsBool indicates if the authentication code has been send (true) or not (false).
        /// </summary>        
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResultAsBool SendAuthenticationCode()
        {
            return this.Channel.SendAuthenticationCode();
        }

        /// <summary>
        /// Changes the password
        /// </summary>
        /// <param name="currentPassword">The current password</param>
        /// <param name="newPassword">The new password</param>
        /// <param name="authenticationToken">The Second Stage Authentication token send to the user. This is only needed if <see cref="M:SendAuthenticationCode(string userId)"/> returns True in the Data variable</param>
        /// <returns>An operation result indicating success and the errors. The Data variable can be ignored</returns>
        public OperationResult ChangePassword(string currentPassword, string newPassword, string authenticationToken)
        {
            return this.Channel.ChangePassword(currentPassword, newPassword, authenticationToken);
        }

        /// <summary>
        /// Gets a Dictionary of all usernames and displays names of all users inside the database
        /// </summary>
        /// <returns>A dictionary with the key being the userName and the value being the display name of each user</returns>
        public OperationResultAsLists FindUsers()
        {
            return this.Channel.FindUsers();
        }

        /// <summary>
        /// Gets the details of the current user
        /// </summary>
        /// <returns>The OperationResults continaining user Settings (if successful)</returns>
        public OperationResultAsUserDetails GetDetailsForCurrentUser()
        {
            return this.Channel.GetDetailsForCurrentUser();
        }

        /// <summary>
        /// Updates the user settings
        /// </summary>
        /// <param name="settings">The settings of the user</param>
        /// <param name="password">The password for checking the user is actually the correct user</param>
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResult SaveCurrentUserSettings(UserDetails settings, string password)
        {
            return this.Channel.SaveCurrentUserSettings(settings, password);
        }

        /// <summary>
        /// Sends a two stage authentication code to the user for the given provider for testing purposes
        /// Either the confirmation token send out as part of the registration process or the currently logged in user is used instead
        /// </summary>                
        /// <param name="provider">The provider selected by the user</param>
        /// <param name="confirmationToken">The confirmation token of the user during registration. If not provided, the test is done for the currently logged in user</param>
        /// <returns>An operation result indicating success or failure</returns>        
        public OperationResult SendTwoStageAuthenticationForTest(string provider, string confirmationToken)
        {
            return this.Channel.SendTwoStageAuthenticationForTest(provider, confirmationToken);
        }

        /// <summary>
        /// Verifies if the supplied two stage authentication code is correct for the given user
        /// Either the confirmation token send out as part of the registration process or the currently logged in user is used instead
        /// </summary>
        /// <param name="provider">The provider for the test</param>        
        /// <param name="code">The authentication code send to the user</param>
        /// <param name="confirmationToken">The confirmation token of the user during registration. If not provided, the test is done for the currently logged in user</param>
        /// <returns>An operation result indicating success or failure</returns>        
        public OperationResult VerifyTwoStageAuthenticationForTest(string provider, string code, string confirmationToken)
        {
            return this.Channel.VerifyTwoStageAuthenticationForTest(provider, code, confirmationToken);
        }

        /// <summary>
        /// Sends a forgotten password token by email to the user for resetting the password
        /// </summary>
        /// <param name="username">The name of the user</param>
        /// <returns>An operation result indicating success or failure</returns>        
        public OperationResult ForgottenPassword(string username)
        {
            return this.Channel.ForgottenPassword(username);
        }

        /// <summary>
        /// Resets the password for the user with the given User name
        /// </summary>
        /// <param name="username">The user name of the user to reset the password for</param>
        /// <param name="newPassword">The new password to set</param>
        /// <param name="token">The reset token</param>
        /// <param name="securityAnswer">The answer to the security question</param>
        /// <returns>An Operation Result indicating success or failure</returns>
        public OperationResult ResetPassword(string username, string newPassword, string token, string securityAnswer)
        {
            return this.Channel.ResetPassword(username, newPassword, token, securityAnswer);
        }

        /// <summary>
        /// Gets the security question for the user with the given Password reset token
        /// </summary>
        /// <param name="passwordResetToken">The password reset token given to the user by calling "ForgottenPassword"</param>
        /// <returns>An OperationResultAsString indicating success or failure with the Security Question filled in the data variable</returns>        
        public OperationResultAsString GetSecurityQuestion(string passwordResetToken)
        {
            return this.Channel.GetSecurityQuestion(passwordResetToken);
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="userName">The name of the user</param>
        /// <param name="password">The password</param>
        /// <param name="isExternalUser">If true, the user has an external source which needs to be checked for the password</param>
        /// <param name="title">The title of the user</param>
        /// <param name="firstName">The firstname of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="email">The email of the user</param>
        /// <param name="phoneNumber">The mobile phone of the user</param>
        /// <param name="externalId">The external id of the user</param>
        /// <param name="role">The role to assign to the user</param>
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResult CreateUser(string userName, string password, bool isExternalUser, string title, string firstName, string lastName, string email, string phoneNumber, string externalId, string role)
        {
            return this.Channel.CreateUser(userName, password, isExternalUser, title, firstName, lastName, email, phoneNumber, externalId, role);
        }

        /// <summary>
        /// Finds and returns all practitioners.
        /// </summary>
        /// <returns>And operation Result as Dictionary indicating success or failure. StringDictionary contains the Practitioners, with Key being the External Id and Value being the Display Name</returns>        
        public OperationResultAsDictionary GetPractitioners()
        {
            return this.Channel.GetPractitioners();
        }

        /// <summary>
        /// Gets the audit trail for the Current User
        /// </summary>
        /// <returns>An operation result indicating success or failure</returns>        
        public OperationResultAsLists GetUserAuditTrail()
        {
            return this.Channel.GetUserAuditTrail();
        }

        /// <summary>
        /// Resends registration token for the given username
        /// </summary>
        /// <param name="username">Username of the account to resend the registration token</param>
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResult ResendRegistrationToken(string username)
        {
            return this.Channel.ResendRegistrationToken(username);
        }

        /// <summary>
        /// Resends two factor authentication token for the given username
        /// </summary>
        /// <param name="username">Username of the account to resend the two factor authentication token</param>
        /// <returns>An operation result indicating success or failure</returns>
        public OperationResult ResendTwoFactorAuthentication(string username)
        {
            return this.Channel.ResendTwoFactorAuthentication(username);
        }

        /// <summary>
        /// Checks if the username is available or not
        /// </summary>
        /// <param name="username">The username to test</param>
        /// <param name="confirmationToken">The confirmation token of the user doing the registration for ensuring that when the username is in use it doesn't belongs to the current user. If null or empty, this test is done against the logged in user</param>
        /// <returns>True is the username is available, false otherwise</returns>        
        public bool UserNameAvailable(string username, string confirmationToken)
        {
            return this.Channel.UserNameAvailable(username, confirmationToken);
        }

        /// <summary>
        /// Validates if the password is valid or not
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>True if the passwords validates, false otherwise</returns>        
        public bool ValidatePassword(string password)
        {
            return this.Channel.ValidatePassword(password);
        }
    }
}
