
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollDerived : ScrollRect
{

    public override void OnDrag(PointerEventData eventData)
    {
        //with this override items will only slide on the scrollbar
    }
}
