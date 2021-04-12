using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler //, IBeginDragHandler, IDragHandler, IEndDragHandler,
{
    public int index;

    private Vector3 slotInitialPosition;
    private Vector3 mouseInitialPosition;
    private int unitIndex;
    private bool dragging = false;
    private Player player;

    void Update() {
        if (dragging) {
            transform.position = slotInitialPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseInitialPosition;
            Player.instance.DragUnitAt(index, transform.position);
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
        if (eventData.button == PointerEventData.InputButton.Left) {
            dragging = true;
            Player.instance.dragging = true;
            slotInitialPosition = transform.position;
            mouseInitialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // show placeable areas
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            dragging = false;
            Player.instance.dragging = false;
            Player.instance.PlayUnitAt(index, transform.position);
            transform.position = slotInitialPosition;
        }
    }
}
