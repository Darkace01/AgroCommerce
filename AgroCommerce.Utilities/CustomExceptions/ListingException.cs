using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class ListingErrorException : Exception
    {
        public ListingErrorException()
        {
        }

        public ListingErrorException(string message)
            : base(message)
        {
        }

        public ListingErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class ListingDeleteErrorException : Exception
    {
        public ListingDeleteErrorException()
        {
        }

        public ListingDeleteErrorException(string message)
            : base(message)
        {
        }

        public ListingDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class ListingUpdateErrorException : Exception
    {
        public ListingUpdateErrorException()
        {
        }

        public ListingUpdateErrorException(string message)
            : base(message)
        {
        }

        public ListingUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoListingFoundException : Exception
    {
        public NoListingFoundException()
        {
        }

        public NoListingFoundException(string message)
            : base(message)
        {
        }

        public NoListingFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
