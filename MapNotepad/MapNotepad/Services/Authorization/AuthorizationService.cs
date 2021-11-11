using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapNotepad.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {

        #region -- Private properties --

        private const string _patternNumbers = @"[0-9]";

        private const string _patternUppercaseLetter = @"[A-Z]";

        private const string _patternEmail = @"^[^@\s]{1,64}@[^@\s]+\.[^@\s]+";

        private IRepositoryService _repository;

        #endregion

        public AuthorizationService(IRepositoryService repository)
        {
            _repository = repository;
        }

        #region -- IAuthorizationService implementation --

        private bool _status;
        public bool Status { get => _status; }

        private User _profile;
        public User Profile { get => _profile; }

        public bool CheckEmailForUse(string email)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                User user = SearchUserByEmail(email);
                if (user == null)
                {
                    result = true;
                }
            } 

            return result;
        }

        public bool EmailMatching(string email)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length <= 129)
                {
                    if (Regex.IsMatch(email.Trim(), _patternEmail))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool PasswordMatching(string password)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password.Length >= 6)
                {
                    if (Regex.IsMatch(password.Trim(), _patternNumbers)
                        && Regex.IsMatch(password.Trim(), _patternUppercaseLetter))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool Registration(User newUser)
        {
            bool result = false;

            User user = SearchUserByEmail(newUser.Email);
            if (user == null)
            {
                newUser.CreationDate = DateTime.Now;

                Task<int> response = _repository.InsertAsync(newUser);
                if (response != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool Login(string email, string password)
        {
            _status = false;
            _profile = null;

            User user = SearchUserByEmail(email);
            if (user != null)
            {
                if (user.Password == password)
                {
                    _status = true;
                    _profile = user;
                }
            }

            return _status;
        }

        public void LogOut()
        {
            _status = false;
            _profile = null;
        }

        #endregion

        #region -- Private methods --

        private User SearchUserByEmail(string email)
        {
            User result = null;

            if (!string.IsNullOrWhiteSpace(email))
            {
                Task<List<User>> response = _repository.GetAllRowsAsync<User>();

                if (response != null)
                {
                    if (response.Result != null)
                    {
                        result = response.Result.Where(row => row.Email == email).FirstOrDefault();
                    }
                }
            }

            return result;
        }

        #endregion

    }
}
