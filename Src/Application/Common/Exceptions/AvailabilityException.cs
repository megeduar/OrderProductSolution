using System;

namespace Application.Common.Exceptions
{
    public class AvailabilityException : Exception
    {
        public AvailabilityException(string name, object key)
            : base($"Entity \"{name}\" ({key}) is out of stock.")
        {
        }
    }
}
