using System;
using GestureRecognizer;
using UnityEngine.Events;

namespace Events
{
    [Serializable]
    public class UnityRecognitionEvent : UnityEvent<RecognitionResult>
    {
    }

    [Serializable]
    public class RecognizedEventListener : TypedEventListener<RecognitionResult>
    {
        public RecognizedEvent eventReference;
        public UnityRecognitionEvent responseReference;

        public override TypedEvent<RecognitionResult> Event => eventReference;

        public override UnityEvent<RecognitionResult> Response => responseReference;
    }
}