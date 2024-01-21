using System;

namespace Assignment3.CustomException
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message):base(message) { }
    }
}
