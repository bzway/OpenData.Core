using System;

namespace Bzway.Module.Core
{
    public class User : EntityBase, IUser
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string PasswordHash
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    } 
    public interface IUser
    {
        string Name { get; set; }

        string UserName { get; set; }
        string PasswordHash { get; set; }
    }
}