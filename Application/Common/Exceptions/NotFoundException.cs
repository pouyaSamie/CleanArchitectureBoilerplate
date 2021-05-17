﻿namespace Application.Common.Exceptions
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string name, object key)
         : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
