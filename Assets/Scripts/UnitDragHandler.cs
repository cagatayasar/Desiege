using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler //, IBeginDragHandler, IDragHandler, IEndDragHandler,
{
    private Vector3 slotInitialPosition;
    private Vector3 mouseInitialPosition;
    private int unitIndex;
    private bool dragging = false;

    void Update() {
        if (dragging) {
            transform.position = slotInitialPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseInitialPosition;
            // show hovered area using transform.position
        }
    }

    // public void OnBeginDrag(PointerEventData eventData) {
    //     slotInitialPosition = transform.position;
    //     mouseInitialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     // show placeable areas
    // }

    // public void OnDrag(PointerEventData eventData) {
    //     transform.position = slotInitialPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseInitialPosition;
    //     // show hovered area using transform.position
    // }

    // public void OnEndDrag(PointerEventData eventData) {
    //     transform.position = slotInitialPosition;
    // }

    public void OnPointerEnter(PointerEventData eventData) {
        // Debug.Log("Pointer entered " + unitIndex);
        
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Debug.Log("Pointer exited " + unitIndex);
    }

    public void OnPointerDown(PointerEventData eventData) {
        dragging = true;
        slotInitialPosition = transform.position;
        mouseInitialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // show placeable areas
    }

    public void OnPointerUp(PointerEventData eventData) {
        dragging = false;
        transform.position = slotInitialPosition;
        // BattlefieldManager.instance.
    }
}
