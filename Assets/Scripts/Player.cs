using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Transform handTransform;
    public Transform actionPointImagesParent;
    public GameObject passButton;
    public Material actionPointEmptyMat;
    public Material actionPointFullMat;
    public List<PlayerUnitStats> playerUnitStats;
    // todo: take this from a game attributes scriptable object or json
    [Range(2, 5)]
    public int handSize;
    [Range(3, 5)]
    public int maxActionPoints;

    private List<UnitType> hand;
    private int actionPoints;
    private int actionPointsToBeUsed;
    private PlayerUnitStats draggedUnitStats;
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

        actionPointsToBeUsed = 0;
        dragging = false;
    }

    void Start() {
        for (int i = 0; i < handSize; i++) {
            handTransform.GetChild(i).gameObject.SetActive(true);
            handTransform.GetChild(i).GetComponent<Image>().sprite = GetUnitStats(hand[i]).sprite;
            handTransform.GetChild(i).GetComponent<ShowDescriptionOnHover>().unitType = hand[i];
        }

        for (int i = maxActionPoints; i < 5; i++) {
            actionPointImagesParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            actionPointsToBeUsed++;
            if (actionPointsToBeUsed > maxActionPoints) actionPointsToBeUsed = maxActionPoints;
            UpdateActionPointImages();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            actionPointsToBeUsed--;
            if (actionPointsToBeUsed < 0) actionPointsToBeUsed = 0;
            UpdateActionPointImages();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            actionPoints--;
            if (actionPoints < 0) actionPoints = 0;
            UpdateActionPointImages();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            actionPoints++;
            if (actionPoints > maxActionPoints) actionPoints = maxActionPoints;
            UpdateActionPointImages();
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            actionPoints = 2;
            actionPointsToBeUsed = 1;
            UpdateActionPointImages();
        }
    }

    public void PlayDeploymentPhase() {
        if (actionPoints < maxActionPoints) {
            actionPoints++;
            UpdateActionPointImages();
            passButton.SetActive(true);
        }
    }

    public void PassTurn() {
        passButton.SetActive(false);
    }

    public bool StartDragging(int index) {
        draggedUnitStats = GetUnitStats(hand[index]);
        if (actionPoints >= draggedUnitStats.cost) {
            actionPointsToBeUsed = draggedUnitStats.cost;
            UpdateActionPointImages();
            return true;
        }
        return false;
    }

    public void DragUnitAt(int index, Vector3 position) {
        Battlefield.instance.DragPlayerUnitAt(hand[index], position);
    }

    public void PlayUnitAt(int index, Vector3 position) {
        bool success = Battlefield.instance.PlacePlayerUnitAt(hand[index], position);
        if (success) actionPoints -= actionPointsToBeUsed;
        actionPointsToBeUsed = 0;
        UpdateActionPointImages();

        List<UnitType> nextUnitPool = new List<UnitType>();
        // todo: take this from Globals instead
        var unitTypes = System.Enum.GetValues(typeof(UnitType));
        for (int i = 0; i < unitTypes.Length; i++) {
            var unitType = (UnitType) unitTypes.GetValue(i);
            if (unitType != UnitType.Null && unitType != hand[0] && unitType != hand[1]) {
                nextUnitPool.Add(unitType);
            }
        }
        hand[index] = nextUnitPool[Random.Range(0, nextUnitPool.Count)];
        handTransform.GetChild(index).GetComponent<Image>().sprite = GetUnitStats(hand[index]).sprite;
        handTransform.GetChild(index).GetComponent<ShowDescriptionOnHover>().unitType = hand[index];
    
        passButton.SetActive(false);
    }

    public void UpdateActionPointImages() {
        for (int i = 0; i < maxActionPoints; i++) {
            var imageParent = actionPointImagesParent.GetChild(i);
            if (actionPoints > i) {
                imageParent.GetChild(1).gameObject.SetActive(true);
                if (actionPoints >= actionPointsToBeUsed && i >= (actionPoints - actionPointsToBeUsed)) {
                    imageParent.GetChild(1).GetComponent<Image>().material = actionPointEmptyMat;
                } else {
                    imageParent.GetChild(1).GetComponent<Image>().material = actionPointFullMat;
                }
            } else {
                imageParent.GetChild(1).gameObject.SetActive(false);
            }
        }
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
