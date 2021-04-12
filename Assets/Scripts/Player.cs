using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector]
    public List<UnitType> hand;
    public Transform handTransform;
    // todo: take this from a game attributes scriptable object or json
    public int handSize;
    public List<PlayerUnitStats> playerUnitStats;

    [HideInInspector]
    public bool dragging;

    void Awake() {
        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        hand = new List<UnitType>();
        hand.Add(UnitType.Swordsman);
        hand.Add(UnitType.Prince);

        dragging = false;
    }

    void Start() {
        for (int i = 0; i < handSize; i++) {
            handTransform.GetChild(i).gameObject.SetActive(true);
            handTransform.GetChild(i).GetComponent<Image>().sprite = GetUnitStats(hand[i]).sprite;
        }
    }

    public void PlayUnitAt(int index, Vector3 position) {
        BattlefieldManager.instance.PlacePlayerUnitAt(hand[index], position);
    }

    public void DragUnitAt(int index, Vector3 position) {
        BattlefieldManager.instance.DragPlayerUnitAt(hand[index], position);
    }

    public PlayerUnitStats GetUnitStats(UnitType unitType) {
        switch (unitType)
        {
            case UnitType.Archer:    return playerUnitStats[0];
            case UnitType.Cavalry:   return playerUnitStats[1];
            case UnitType.Knight:    return playerUnitStats[2];
            case UnitType.Prince:    return playerUnitStats[3];
            case UnitType.Swordsman: return playerUnitStats[4];
            default: return null;
        }
    }
}
