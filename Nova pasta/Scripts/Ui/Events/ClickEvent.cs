using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent _Click;
    public void OnPointerClick(PointerEventData eventData)
    {
        _Click.Invoke();
    }
}
