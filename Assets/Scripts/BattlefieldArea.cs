using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BattlefieldArea : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI areaTypeText;
    public Transform castleTowerParent;
    public Image castleTowerImage;
    public Transform castleGateParent;
    public GameObject forestObject;
    public GameObject playerUnitObject;
    public GameObject deploymentImage;
    public GameObject gridLineRight;
    public GameObject gridLineTop;

    [HideInInspector]
    public AreaType areaType;
    [HideInInspector]
    public PlayerUnit playerUnit;
    public float hideDeploymentImageAfter;
    private float deploymentImageTimer;
    private bool deploymentImageActive;

    private float gridLinesTimer;
    private bool gridLinesActive;

    void Update() {
        if (deploymentImageActive) {
            if (deploymentImageTimer >= hideDeploymentImageAfter) {
                deploymentImageTimer = 0f;
                deploymentImageActive = false;
                deploymentImage.SetActive(false);
            } else {
                deploymentImageTimer += Time.deltaTime;
            }
        }
    }

    public void PlacePlayerUnit(PlayerUnit playerUnit) {
        deploymentImage.SetActive(false);
        playerUnitObject.SetActive(true);
        this.playerUnit = playerUnit;
        playerUnitObject.GetComponent<Image>().sprite = Player.instance.GetUnitStats(playerUnit.unitType).sprite;
    }

    public void SetAreaType(AreaType areaType) {
        this.areaType = areaType;

        switch (areaType)
        {
            case AreaType.CastleGate:  castleGateParent.gameObject.SetActive(true);  break;
            case AreaType.CastleTower: castleTowerParent.gameObject.SetActive(true); break;
            case AreaType.EnemySpawn: break;
            case AreaType.NotUsed: forestObject.SetActive(true); break;
        }
    }

    public void ShowDeploymentImage() {
        if (!deploymentImageActive) {
            deploymentImageTimer = 0f;
            deploymentImageActive = true;
            deploymentImage.SetActive(true);
        }
    }

    public void ShowGridLines(bool showRight, bool showTop) {
        gridLineRight.SetActive(showRight);
        gridLineTop.SetActive(showTop);
    }
}
