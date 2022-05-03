using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    [SerializeField] KeyCode Code;
    [SerializeField] UnityEvent nEvent;
    void _Event()
    {
        nEvent.Invoke();
    }

    void Update()
    {
        if(Input.GetKeyDown(Code))
        {
            _Event();
        }
    }
}
