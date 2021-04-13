using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUnitDescription : MonoBehaviour
{
    public static PlayerUnitDescription instance;

    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI deployDmgText;
    public TextMeshProUGUI deployHpText;
    public TextMeshProUGUI deployOtherText;
    public Image targetsImage;
    public TextMeshProUGUI orderDescriptionText;

    [HideInInspector]
    public CanvasGroup canvasGroup;
    [HideInInspector]
    public bool showing = true;
    [HideInInspector]
    public UnitType activeUnitType;

    void Awake() {
        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        showing = false;
        activeUnitType = UnitType.Null;
    }

    public void ShowDescription(UnitType unitType) {
        canvasGroup.alpha = 1f;
        var unitStats = Player.instance.GetUnitStats(unitType);
        unitNameText.text = unitStats.unitName;
        deployDmgText.text = unitStats.damage.ToString();
        deployHpText.text = unitStats.hp.ToString();
        deployOtherText.text = unitStats.deployOtherDescription;
        targetsImage.sprite = unitStats.targetsSprite;
        orderDescriptionText.text = unitStats.orderDescription;
    }

    public void HideDescription() {
        canvasGroup.alpha = 0f;
    }
}
