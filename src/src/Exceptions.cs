using System;

namespace Buzagmod
{
    public class CannotOpenModArchiveException : Exception
    {
        public CannotOpenModArchiveException() { }
        public CannotOpenModArchiveException(string message) : base(message) { }
        public CannotOpenModArchiveException(string message, Exception inner) : base(message, inner) { }
    }

    public class InvalidMetadataException : Exception
    {
        public InvalidMetadataException() { }
        public InvalidMetadataException(string message) : base(message) { }
        public InvalidMetadataException(string message, Exception inner) : base(message, inner) { }
    }

    public class ModArchiveContentException : Exception
    {
        public ModArchiveContentException() { }
        public ModArchiveContentException(string message) : base(message) { }
        public ModArchiveContentException(string message, Exception inner) : base(message, inner) { }
    }

    public class ModCollisionException : Exception
    {
        public ModCollisionException() { }
        public ModCollisionException(string message) : base(message) { }
        public ModCollisionException(string message, Exception inner) : base(message, inner) { }
    }
}