using UnityEngine;

namespace Variables
{
    public abstract class Variable<T> : ScriptableObject
    {
        public T Value;

        protected Variable(){}
        protected Variable(T value)
        {
            Value = value;
        }

        public void SetValue(Variable<T> from)
        {
            Value = from.Value;
        }

        public void SetValue(T from)
        {
            Value = from;
        }
    }
}