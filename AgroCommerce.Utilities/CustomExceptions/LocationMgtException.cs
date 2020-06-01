using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class LocationMgtSaveErrorException : Exception
    {
        public LocationMgtSaveErrorException()
        {
        }

        public LocationMgtSaveErrorException(string message)
            : base(message)
        {
        }

        public LocationMgtSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class LocationMgtDeleteErrorException : Exception
    {
        public LocationMgtDeleteErrorException()
        {
        }

        public LocationMgtDeleteErrorException(string message)
            : base(message)
        {
        }

        public LocationMgtDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class LocationMgtUpdateErrorException : Exception
    {
        public LocationMgtUpdateErrorException()
        {
        }

        public LocationMgtUpdateErrorException(string message)
            : base(message)
        {
        }

        public LocationMgtUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoLocationMgtFoundException : Exception
    {
        public NoLocationMgtFoundException()
        {
        }

        public NoLocationMgtFoundException(string message)
            : base(message)
        {
        }

        public NoLocationMgtFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
