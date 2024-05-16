﻿using Register.Broker.Logging;
using Register.Broker.Storeage;
using Register.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Register.Service
{
    internal class UserService : IUserService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStoreageBroker storeageBroker;
        public UserService()
        {
            this.loggingBroker = new LoggingBroker();
            this.storeageBroker = new StoreageBroker();
        }

        public bool LogIn(Users user)
        {
            try
            {
                return user is null
                     ? LogInUserInvalid()
                     : LogInUserValidation(user);
            }
            catch (Exception exception)
            {

                this.loggingBroker.LogError(exception);
                return false;
            }

        }
        public Users SignUp(Users user)
        {
            try
            {
                return user is null
                    ? SignUpUsersInvalid()
                    : ValidationAndSignUpUser(user);
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError(exception);
                return new Users();
            }
        }

        private Users ValidationAndSignUpUser(Users user)
        {
            if (String.IsNullOrWhiteSpace(user.Email)
              || String.IsNullOrWhiteSpace(user.Password))
            {
                this.loggingBroker.LogError("Invalid user information.");
                return new Users();
            }
            else
            {
                var userInformation = this.storeageBroker.SignUpUser(user);
                if (userInformation is null)
                {
                    this.loggingBroker.LogError("Not added user info");
                }               
                else
                {
                    this.loggingBroker.LogInformation("Added user");
                }
                return userInformation;
            }
        }

        private Users SignUpUsersInvalid()
        {
            this.loggingBroker.LogError("User information is null.");
            return new Users();

        }
        private bool LogInUserInvalid()
        {
            this.loggingBroker.LogError("Sizning login yoki parolingiz noto'g'ri kiritilgan");
            return false;
        }
        private bool LogInUserValidation(Users user)
        {
            if (String.IsNullOrWhiteSpace(user.Email)
                || String.IsNullOrWhiteSpace(user.Password))
            {
                this.loggingBroker.LogError("Incoming data is incomplete");
                return false;
            }
            else
            {
                bool userInfo = this.storeageBroker.CheckoutUser(user);
                if (userInfo is true)
                {
                    this.loggingBroker.LogInformation("successful");
                }
                else
                {
                    this.loggingBroker.LogError("Not found");
                }
                return userInfo;
            }
        }

    }
}