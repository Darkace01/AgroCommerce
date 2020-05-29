using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Utilities.CustomExceptions
{
    [Serializable]
    public class FileSaveErrorException : Exception
    {
        public FileSaveErrorException()
        {
        }

        public FileSaveErrorException(string message)
            : base(message)
        {
        }

        public FileSaveErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class NoFileFoundException : Exception
    {
        public NoFileFoundException()
        {
        }

        public NoFileFoundException(string message)
            : base(message)
        {
        }

        public NoFileFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }



    [Serializable]
    public class FileNotAnImageException : Exception
    {
        public FileNotAnImageException()
        {
        }

        public FileNotAnImageException(string message)
            : base(message)
        {
        }

        public FileNotAnImageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class FileTooLargeException : Exception
    {
        public FileTooLargeException()
        {
        }

        public FileTooLargeException(string message)
            : base(message)
        {
        }

        public FileTooLargeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
