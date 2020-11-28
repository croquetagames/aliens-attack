using System;
using UnityEngine;

namespace Variables
{
    public abstract class Reference<T>
    {
        public bool UseConstant;
        public T ConstantValue;

        public virtual Variable<T> Variable
        {
            get;
        }
        
        public Reference() { }

        public Reference(T value): this() {
            UseConstant = true;
            ConstantValue = value;
        }
        
        public T Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
        }
        
        public static implicit operator T(Reference<T> reference)
        {
            return reference.Value;
        }
    }
}