using System;

namespace Exercise.Domain.Exceptions
{
    public class ExerciseBaseException : Exception
    {
        public ExerciseBaseException()
        { }

        public ExerciseBaseException(string message)
            : base(message)
        { }

        public ExerciseBaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
