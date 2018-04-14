using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        // Private fields, which we will need in the class
        private string input;
        private string[] data;
        
        protected Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        // Public "Input" property, working with the private "input" field
        public string Input
        {
            get { return this.input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        // Public "Data" property, working with the private "data" field
        public string[] Data
        {
            get { return this.data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        // Public abstract method "Execute", receiving no params, which has to be overridden in every derived subclass
        public abstract void Execute();
    }
}
