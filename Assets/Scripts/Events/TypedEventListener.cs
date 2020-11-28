using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class TypedEventListener<T> : MonoBehaviour
    {
        public virtual TypedEvent<T> Event { get; }
        public virtual UnityEvent<T> Response { get; }

        public TypedEventListener(){}
        public TypedEventListener(UnityEvent<T> response, TypedEvent<T> @event)
        {
            Response = response;
            Event = @event;
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(T arg)
        {
            Response.Invoke(arg);
        }
    }
}