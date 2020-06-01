using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class TransactionSaveErrorException : Exception
    {
        public TransactionSaveErrorException()
        {
        }

        public TransactionSaveErrorException(string message)
            : base(message)
        {
        }

        public TransactionSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class TransactionDeleteErrorException : Exception
    {
        public TransactionDeleteErrorException()
        {
        }

        public TransactionDeleteErrorException(string message)
            : base(message)
        {
        }

        public TransactionDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class TransactionUpdateErrorException : Exception
    {
        public TransactionUpdateErrorException()
        {
        }

        public TransactionUpdateErrorException(string message)
            : base(message)
        {
        }

        public TransactionUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoTransactionFoundException : Exception
    {
        public NoTransactionFoundException()
        {
        }

        public NoTransactionFoundException(string message)
            : base(message)
        {
        }

        public NoTransactionFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
