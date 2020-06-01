using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class UserSaveErrorException : Exception
    {
        public UserSaveErrorException()
        {
        }

        public UserSaveErrorException(string message)
            : base(message)
        {
        }

        public UserSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class UserDeleteErrorException : Exception
    {
        public UserDeleteErrorException()
        {
        }

        public UserDeleteErrorException(string message)
            : base(message)
        {
        }

        public UserDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class UserUpdateErrorException : Exception
    {
        public UserUpdateErrorException()
        {
        }

        public UserUpdateErrorException(string message)
            : base(message)
        {
        }

        public UserUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoUserFoundException : Exception
    {
        public NoUserFoundException()
        {
        }

        public NoUserFoundException(string message)
            : base(message)
        {
        }

        public NoUserFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }



   
}
