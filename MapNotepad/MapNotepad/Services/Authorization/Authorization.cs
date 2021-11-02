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
    public class Authorization : IAuthorization
    {
        private IRepository _repository;
        public Authorization(IRepository repository)
        {
            _repository = repository;
        }

        private bool _status;
        private User _profile;

        public bool Status { get => _status; }
        public User Profile { get => _profile; }

        private User SearchUserByEmail(string email)
        {
            User result = null;

            if (!string.IsNullOrWhiteSpace(email))
            {
                Task<List<User>> all = _repository.GetAllRowsAsync<User>();
                if (all != null)
                {
                    if (all.Result != null)
                    {
                        result = all.Result.Where(row => row.Email == email).FirstOrDefault();
                    }
                }
            }

            return result;
        }

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
                    //[^@\s] - Match one or more occurrences of any character other than the @ character or whitespace.
                    if (Regex.IsMatch(email.Trim(), @"^[^@\s]{1,64}@[^@\s]+\.[^@\s]+"))
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
                    if (Regex.IsMatch(password.Trim(), @"[0-9]")
                        && Regex.IsMatch(password.Trim(), @"[A-Z]"))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool Registration(string email, string password)
        {
            bool result = false;

            User user = SearchUserByEmail(email);
            if (user == null)
            {
                var newUser = new User()
                {
                    Email = email,
                    Password = password,
                    TimeCreating = DateTime.Now
                };

                ///
                ////
                ///
                ///
                _repository.InsertAsync(newUser);

                result = true;
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
    }
}
