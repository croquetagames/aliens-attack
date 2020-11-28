using GestureRecognizer;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Recognized")]
    public class RecognizedEvent : TypedEvent<RecognitionResult>
    {
    }
}