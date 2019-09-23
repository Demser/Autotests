using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _365AutomatedTests.Framework
{
    public static class Config
    {
        /// <summary>
        /// Get the Database Server Host
        /// </summary>
        /// 


        public static string UserPassword
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("userSettings");
                return env["userPassword"];
            }

        }
        public static string UserBarCode
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("userSettings");
                return env["userBarCode"];
            }
        }

        public static string MSDbHost
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDbHost"];
            }
        }
        /// <summary>
        /// Get the Database Server Login
        /// </summary>
        public static string MSDbLogin
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDbLogin"];
            }
        }
        /// <summary>
        /// Get the Database Server Password
        /// </summary>
        public static string MSDbPassword
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDbPass"];
            }
        }
        /// <summary>
        /// Get the name of the BatchDropperStab database
        /// </summary>
        public static string MSDbBatchDropperStab
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDbBatchDropperStab"];
            }
        }
        /// <summary>
        /// Get the name of the BatchDropperStab database
        /// </summary>
        public static string MSDbLW
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDbLW"];
            }
        }

        public static string MSDb
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msDb"];
            }
        }

        public static string MSACGD
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msACGD"];
            }
        }

        public static string MSPreOrderV4
        {
            get
            {
                var env = (NameValueCollection)ConfigurationManager.GetSection("database");
                return env["msPreOrderV4"];
            }
        }
    }
}
