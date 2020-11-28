using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public abstract class TypedEvent<T> : ScriptableObject
    {
        private readonly List<TypedEventListener<T>> _eventListeners =
            new List<TypedEventListener<T>>();

        public void Raise(T args)
        {
            for (var i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(args);
        }

        public void RegisterListener(TypedEventListener<T> listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(TypedEventListener<T> listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}