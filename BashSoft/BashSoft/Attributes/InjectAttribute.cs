using System;

namespace BashSoft.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {

        }
    }
}
