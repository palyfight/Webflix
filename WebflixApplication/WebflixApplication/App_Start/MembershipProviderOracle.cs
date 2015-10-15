//Adapted from: http://msdn2.microsoft.com/en-us/library/6tc47t75(VS.80).aspx
using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

// This provider works with the following schema for the table of user data.
// 
//CREATE TABLE [dbo].[Users](
//	[UserID] [uniqueidentifier] NOT NULL,
//	[Username] [nvarchar](255) NOT NULL,
//	[ApplicationName] [nvarchar](255) NOT NULL,
//	[Email] [nvarchar](128) NOT NULL,
//	[Comment] [nvarchar](255) NULL,
//	[Password] [nvarchar](128) NOT NULL,
//	[PasswordQuestion] [nvarchar](255) NULL,
//	[PasswordAnswer] [nvarchar](255) NULL,
//	[IsApproved] [bit] NULL,
//	[LastActivityDate] [datetime] NULL,
//	[LastLoginDate] [datetime] NULL,
//	[LastPasswordChangedDate] [datetime] NULL,
//	[CreationDate] [datetime] NULL,
//	[IsOnLine] [bit] NULL,
//	[IsLockedOut] [bit] NULL,
//	[LastLockedOutDate] [datetime] NULL,
//	[FailedPasswordAttemptCount] [int] NULL,
//	[FailedPasswordAttemptWindowStart] [datetime] NULL,
//	[FailedPasswordAnswerAttemptCount] [int] NULL,
//	[FailedPasswordAnswerAttemptWindowStart] [datetime] NULL,
//PRIMARY KEY CLUSTERED 
//(
//	[UserID] ASC
//)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
//) ON [PRIMARY]
// 

namespace WebflixApplication.App_Start
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

    }
}