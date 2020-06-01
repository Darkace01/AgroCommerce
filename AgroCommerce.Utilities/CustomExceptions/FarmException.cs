using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class FarmSaveErrorException : Exception
    {
        public FarmSaveErrorException()
        {
        }

        public FarmSaveErrorException(string message)
            : base(message)
        {
        }

        public FarmSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class FarmDeleteErrorException : Exception
    {
        public FarmDeleteErrorException()
        {
        }

        public FarmDeleteErrorException(string message)
            : base(message)
        {
        }

        public FarmDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class FarmUpdateErrorException : Exception
    {
        public FarmUpdateErrorException()
        {
        }

        public FarmUpdateErrorException(string message)
            : base(message)
        {
        }

        public FarmUpdateErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoFarmFoundException : Exception
    {
        public NoFarmFoundException()
        {
        }

        public NoFarmFoundException(string message)
            : base(message)
        {
        }

        public NoFarmFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
