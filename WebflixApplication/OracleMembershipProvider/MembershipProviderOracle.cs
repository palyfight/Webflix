//Adapted from: http://msdn2.microsoft.com/en-us/library/6tc47t75(VS.80).aspx
using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.OracleClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

// This provider works with the following schema for the table of user data.
// 
//CREATE TABLE [dbo].[Users](
//	[IDPERSONNE] [uniqueidentifier] NOT NULL,
//	[COURRIEL] [varchar2](50) NOT NULL,
//	[MOTDEPASSE] [varchar2](255) NOT NULL
// 

namespace OracleMembershipProvider
{

    public sealed class MembershipProviderOracle : MembershipProvider
    {

        #region Class Variables

        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;
        private MachineKeySection machineKey; //Used when determining encryption key values.

        #endregion

        #region Enums

        private enum FailureType
        {
            Password = 1,
            PasswordAnswer = 2
        }

        #endregion

        #region Properties

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return enablePasswordRetrieval;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return requiresUniqueEmail;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return passwordFormat;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegularExpression;
            }
        }

        #endregion

        #region Initialization

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (name == null || name.Length == 0)
            {
                name = "MembershipProviderOracle";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Oracle Membership Provider");
            }

            //Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionString"]];

            if ((ConnectionStringSettings == null) || (ConnectionStringSettings.ConnectionString.Trim() == String.Empty))
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = ConnectionStringSettings.ConnectionString;

            //Get encryption and decryption key information from the configuration.
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
            {
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                {
                    throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        #endregion

        #region Implemented Abstract Methods from MembershipProvider

        /// <summary>
        /// Change the user password.
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="oldPwd">Old password.</param>
        /// <param name="newPwd">New password.</param>
        /// <returns>T/F if password was changed.</returns>
        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the question and answer for a password validation.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="newPwdQuestion">New question text.</param>
        /// <param name="newPwdAnswer">New answer text.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="email">Email address.</param>
        /// <param name="passwordQuestion">Security quesiton for password.</param>
        /// <param name="passwordAnswer">Security quesiton answer for password.</param>
        /// <param name="isApproved"></param>
        /// <param name="userID">User ID</param>
        /// <param name="status"></param>
        /// <returns>MembershipUser</returns>

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(email, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser membershipUser = GetUser(username, false);

            if (membershipUser == null)
            {
                System.DateTime createDate = DateTime.Now;

                OracleConnection oracleConnection = new OracleConnection(connectionString);
                OracleCommand oracleCommand = new OracleCommand("User_Ins", oracleConnection);

                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Add("@returnValue", OracleType.Int32, 0).Direction = ParameterDirection.ReturnValue;
                //oracleCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username; ;
                //oracleCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                oracleCommand.Parameters.Add("@password", OracleType.VarChar, 255).Value = EncodePassword(password);
                oracleCommand.Parameters.Add("@email", OracleType.VarChar, 50).Value = email;
                //oracleCommand.Parameters.Add("@passwordQuestion", SqlDbType.NVarChar, 255).Value = passwordQuestion;
                //oracleCommand.Parameters.Add("@passwordAnswer", SqlDbType.NVarChar, 255).Value = EncodePassword(passwordAnswer);
                //oracleCommand.Parameters.Add("@isApproved", SqlDbType.Bit).Value = isApproved;
                //oracleCommand.Parameters.Add("@comment", SqlDbType.NVarChar, 255).Value = String.Empty;

                try
                {
                    oracleConnection.Open();

                    oracleCommand.ExecuteNonQuery();
                    if ((int)oracleCommand.Parameters["@returnValue"].Value == 0)
                    {

                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (OracleException e)
                {
                    //Add exception handling here.

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    oracleConnection.Close();
                }

                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="deleteAllRelatedData">Whether to delete all related data.</param>
        /// <returns>T/F if the user was deleted.</returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get a collection of users.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="totalRecords">Total # of records to retrieve.</param>
        /// <returns>Collection of MembershipUser objects.</returns>

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the number of users currently on-line.
        /// </summary>
        /// <returns>  /// # of users on-line.</returns>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// Get the password for a user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="answer">Answer to security question.</param>
        /// <returns>Password for the user.</returns>
        public override string GetPassword(string email, string answer)
        {

            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            OracleConnection oracleConnection = new OracleConnection(connectionString);
            OracleCommand oracleCommand = new OracleCommand("User_GetPassword", oracleConnection);

            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Parameters.Add("@email", OracleType.VarChar, 50).Value = email;
            //oracleCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            string password = String.Empty;
            string passwordAnswer = String.Empty;
            OracleDataReader oracleDataReader = null;

            try
            {
                oracleConnection.Open();

                oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.SingleRow & CommandBehavior.CloseConnection);

                if (oracleDataReader.HasRows)
                {
                    oracleDataReader.Read();

                    /*if (oracleDateReader.GetBoolean(2))
                    {
                        throw new MembershipPasswordException("The supplied user is locked out.");
                    }*/

                    password = oracleDataReader.GetString(0);
                    //passwordAnswer = oracleDateReader.GetString(1);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            }
            catch (OracleException e)
            {
                //Add exception handling here.
            }
            finally
            {
                if (oracleDataReader != null) { oracleDataReader.Close(); }
            }

            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                //UpdateFailureCount(username, FailureType.PasswordAnswer);

                throw new MembershipPasswordException("Incorrect password answer.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {

            OracleConnection oracleConnection = new OracleConnection(connectionString);
            OracleCommand oracleCommand = new OracleCommand("User_Sel", oracleConnection);

            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Parameters.Add("@email", OracleType.VarChar, 50).Value = email;
            //sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            MembershipUser membershipUser = null;
            OracleDataReader oracleDataReader = null;

            try
            {
                oracleConnection.Open();

                oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (oracleDataReader.HasRows)
                {
                    oracleDataReader.Read();
                    membershipUser = GetUserFromReader(oracleDataReader);

                    /*if (userIsOnline)
                    {
                        OracleCommand sqlUpdateCommand = new OracleCommand("User_UpdateActivityDate_ByUserName", sqlConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }*/
                }
            }
            catch (OracleException e)
            {
                //Add exception handling here.
            }
            finally
            {
                if (oracleDataReader != null) { oracleDataReader.Close(); }
            }

            return membershipUser;
        }
        /// <summary>
        /// Get a user based upon provider key and if they are on-line.
        /// </summary>
        /// <param name="userID">Provider key.</param>
        /// <param name="userIsOnline">T/F whether the user is on-line.</param>
        /// <returns></returns>
        public override MembershipUser GetUser(object userID, bool userIsOnline)
        {

            OracleConnection oracleConnection = new OracleConnection(connectionString);
            OracleCommand oracleCommand = new OracleCommand("User_SelByUserID", oracleConnection);

            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Parameters.Add("@userID", SqlDbType.UniqueIdentifier).Value = userID;

            MembershipUser membershipUser = null;
            OracleDataReader sqlDataReader = null;

            try
            {
                oracleConnection.Open();

                sqlDataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    membershipUser = GetUserFromReader(sqlDataReader);

                    /*if (userIsOnline)
                    {
                        OracleCommand sqlUpdateCommand = new OracleCommand("User_UpdateActivityDate_ByUserID", oracleConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@userID", SqlDbType.NVarChar, 255).Value = userID;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }*/
                }
            }
            catch (OracleException e)
            {
                //Add exception handling here.
            }
            finally
            {
                if (sqlDataReader != null) { sqlDataReader.Close(); }
            }

            return membershipUser;

        }

        /// <summary>
        /// Unlock a user.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <returns>T/F if unlocked.</returns>
        public override bool UnlockUser(string username)
        {
            throw new NotImplementedException();
        }


        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reset the user password.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="answer">Answer to security question.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the user information.
        /// </summary>
        /// <param name="_membershipUser">MembershipUser object containing data.</param>
        public override void UpdateUser(MembershipUser membershipUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate the user based upon username and password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>T/F if the user is valid.</returns>
        public override bool ValidateUser(string email, string password)
        {

            bool isValid = false;

            OracleConnection oracleConnection = new OracleConnection(connectionString);
            OracleCommand oracleCommand = new OracleCommand("User_Validate", oracleConnection);

            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Parameters.Add("@email", OracleType.VarChar, 50).Value = email;
            //sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            OracleDataReader oracleDataReader = null;
            bool isApproved = false;
            string storedPassword = String.Empty;

            try
            {
                oracleConnection.Open();
                oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (oracleDataReader.HasRows)
                {
                    oracleDataReader.Read();
                    storedPassword = oracleDataReader.GetString(0);
                    isApproved = oracleDataReader.GetBoolean(1);
                }
                else
                {
                    return false;
                }

                oracleDataReader.Close();

                if (CheckPassword(password, storedPassword))
                {
                    isValid = true;
                    /*if (isApproved)
                    {
                        isValid = true;

                        OracleCommand sqlUpdateCommand = new SqlCommand("User_UpdateLoginDate", sqlConnection);

                        sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                        sqlUpdateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255).Value = username;
                        sqlUpdateCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;
                        sqlUpdateCommand.ExecuteNonQuery();
                    }*/
                }
                else
                {
                    oracleConnection.Close();
                    //UpdateFailureCount(username, FailureType.Password);
                }
            }
            catch (OracleException e)
            {
                //Add exception handling here.
            }
            finally
            {
                if (oracleDataReader != null) { oracleDataReader.Close(); }
                if ((oracleConnection != null) && (oracleConnection.State == ConnectionState.Open)) { oracleConnection.Close(); }
            }

            return isValid;
        }
        /// <summary>
        /// Find all users matching a search string.
        /// </summary>
        /// <param name="usernameToMatch">Search string of user name to match.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords">Total records found.</param>
        /// <returns>Collection of MembershipUser objects.</returns>

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find all users matching a search string of their email.
        /// </summary>
        /// <param name="emailToMatch">Search string of email to match.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords">Total records found.</param>
        /// <returns>Collection of MembershipUser objects.</returns>

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Utility Functions"
        /// <summary>
        /// Create a MembershipUser object from a data reader.
        /// </summary>
        /// <param name="sqlDataReader">Data reader.</param>
        /// <returns>MembershipUser object.</returns>
        private MembershipUser GetUserFromReader(
      OracleDataReader oracleDataReader
        )
        {

            object idPersonne = oracleDataReader.GetValue(0);
            string courriel = oracleDataReader.GetString(1);
            string motDePasse = oracleDataReader.GetString(2);
            string username = String.Empty;

            string passwordQuestion = String.Empty;
            if (oracleDataReader.GetValue(3) != DBNull.Value)
            {
                passwordQuestion = oracleDataReader.GetString(3);
            }

            string comment = String.Empty;
            if (oracleDataReader.GetValue(4) != DBNull.Value)
            {
                comment = oracleDataReader.GetString(4);
            }

            bool isApproved = true;
            if (oracleDataReader.GetBoolean(5) != null)
            {
                isApproved = oracleDataReader.GetBoolean(5);
            }
            bool isLockedOut = oracleDataReader.GetBoolean(6);
            if (oracleDataReader.GetBoolean(6) != null)
            {
                isLockedOut = oracleDataReader.GetBoolean(6);
            }
            DateTime creationDate = new DateTime();
            if (oracleDataReader.GetValue(7) != DBNull.Value)
            {
                creationDate = oracleDataReader.GetDateTime(7);
            }

            DateTime lastLoginDate = new DateTime();
            if (oracleDataReader.GetValue(8) != DBNull.Value)
            {
                lastLoginDate = oracleDataReader.GetDateTime(8);
            }

            DateTime lastActivityDate = new DateTime();
            if (oracleDataReader.GetValue(9) != DBNull.Value)
            {
                lastActivityDate = oracleDataReader.GetDateTime(9);
            }
            DateTime lastPasswordChangedDate = new DateTime();
            if (oracleDataReader.GetValue(10) != DBNull.Value)
            {
                lastPasswordChangedDate = oracleDataReader.GetDateTime(10);
            }

            DateTime lastLockedOutDate = new DateTime();
            if (oracleDataReader.GetValue(11) != DBNull.Value)
            {
                lastLockedOutDate = oracleDataReader.GetDateTime(11);
            }

            MembershipUser membershipUser = new MembershipUser(
              this.Name,
             username,
             idPersonne,
             courriel,
             passwordQuestion,
             comment,
             isApproved,
             isLockedOut,
             creationDate,
             lastLoginDate,
             lastActivityDate,
             lastPasswordChangedDate,
             lastLockedOutDate
              );

            return membershipUser;

        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// Update password and answer failure information.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="failureType">Type of failure</param>
        /// <remarks></remarks>
        /*private void UpdateFailureCount(string username, FailureType failureType)
        {

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("Users_Sel_ByUserName", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@failureType", SqlDbType.Int, 0).Value = failureType;
            sqlCommand.Parameters.Add("@passwordAttempWindow", SqlDbType.DateTime, 0).Value = passwordAttemptWindow;
            sqlCommand.Parameters.Add("@maxInvalidPasswordAttempts", SqlDbType.Int, 0).Value = maxInvalidPasswordAttempts;
            sqlCommand.Parameters.Add("@userName", SqlDbType.NVarChar, 255).Value = username;
            sqlCommand.Parameters.Add("@applicationName", SqlDbType.NVarChar, 255).Value = applicationName;

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Add exception handling here.
            }

        }*/
        /// <summary>
        /// Check the password format based upon the MembershipPasswordFormat.
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="dbpassword"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Encode password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Encoded password.</returns>
        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        /// <summary>
        /// UnEncode password.
        /// </summary>
        /// <param name="encodedPassword">Password.</param>
        /// <returns>Unencoded password.</returns>
        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        #endregion
    }
}