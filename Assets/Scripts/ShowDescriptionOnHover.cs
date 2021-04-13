using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowDescriptionOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public UnitType unitType;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!PlayerUnitDescription.instance.showing && PlayerUnitDescription.instance.activeUnitType != unitType) {
            PlayerUnitDescription.instance.ShowDescription(unitType);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        PlayerUnitDescription.instance.HideDescription();
    }
}
