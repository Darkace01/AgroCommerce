using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class AnimalTypeSaveErrorException : Exception
    {
        public AnimalTypeSaveErrorException()
        {
        }

        public AnimalTypeSaveErrorException(string message)
            : base(message)
        {
        }

        public AnimalTypeSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class AnimalTypeDeleteErrorException : Exception
    {
        public AnimalTypeDeleteErrorException()
        {
        }

        public AnimalTypeDeleteErrorException(string message)
            : base(message)
        {
        }

        public AnimalTypeDeleteErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoAnimalTypeFoundException : Exception
    {
        public NoAnimalTypeFoundException()
        {
        }

        public NoAnimalTypeFoundException(string message)
            : base(message)
        {
        }

        public NoAnimalTypeFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
