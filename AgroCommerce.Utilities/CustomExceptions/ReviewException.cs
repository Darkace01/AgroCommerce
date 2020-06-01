using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class ReviewSaveErrorException : Exception
    {
        public ReviewSaveErrorException()
        {
        }

        public ReviewSaveErrorException(string message)
            : base(message)
        {
        }

        public ReviewSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class ReviewDeleteErrorException : Exception
    {
        public ReviewDeleteErrorException()
        {
        }

        public ReviewDeleteErrorException(string message)
            : base(message)
        {
        }

        public ReviewDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class ReviewUpdateErrorException : Exception
    {
        public ReviewUpdateErrorException()
        {
        }

        public ReviewUpdateErrorException(string message)
            : base(message)
        {
        }

        public ReviewUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoReviewFoundException : Exception
    {
        public NoReviewFoundException()
        {
        }

        public NoReviewFoundException(string message)
            : base(message)
        {
        }

        public NoReviewFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
