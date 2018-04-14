using System;

namespace BashSoft.Exceptions
{
    public class DuplicateEntryInStructureException : Exception
    {
        private const string DuplicateEntry = "The {0} already exists in {1}.";

        // Main constructor of the class, using the base one
        public DuplicateEntryInStructureException() : base(DuplicateEntry)
        {

        }

        // Secondary constructor of the class, receiving two strings and it again uses the base constructor
        public DuplicateEntryInStructureException(string entry, string structure) : base(string.Format(DuplicateEntry, entry, structure))
        {

        }
    }
}
