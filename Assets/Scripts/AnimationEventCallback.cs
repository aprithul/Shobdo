using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventCallback : MonoBehaviour
{
    public UnityEvent[] events;
    
    public void call_event(int id)
    {
        events[id].Invoke();
    }

}
