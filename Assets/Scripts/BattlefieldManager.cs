using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum AreaType {
    CastleTower,
    CastleGate,
    InsideCastle,
    OutsideCastle,
    EnemySpawn,
    NotUsed
}

public class BattlefieldManager : MonoBehaviour
{
    public static BattlefieldManager instance;

    public GameObject battlefieldRowPrefab;
    public GameObject battlefieldAreaPrefab;
    public Material gridLineMaterial;
    public float gridLineAlpha;
    // Change these to change the grid structure:
    // battlefieldRowCount, battlefieldColCount, battlefieldStructure,
    // transform->width & height, camera->contentWidth & contentHeight
    // todo: take this from a game attributes scriptable object or json
    public int battlefieldRowCount;
    public int battlefieldColCount;
    public List<string> battlefieldStructure;
    [HideInInspector]
    public List<List<BattlefieldArea>> areas;

    private bool boundariesHasBeenSet;
    private float boundaryLeft;
    private float boundaryRight;
    private float boundaryBottom;
    private float boundaryTop;
    private Vector2 areaSize;

    void Awake() {
        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        // Initialize the battlefield grid
        if (battlefieldStructure.Count != battlefieldRowCount) {
            Debug.LogError("BattlefieldStructure list should have a size of: " + battlefieldRowCount);
        } else {
            areas = new List<List<BattlefieldArea>>();
            for (int i = 0; i < battlefieldRowCount; i++) {
                var rowAreas = new List<BattlefieldArea>();
                GameObject row = GameObject.Instantiate(battlefieldRowPrefab);
                row.transform.SetParent(transform, false);
                row.name = "Battlefield Row " + (i+1);
                for (int j = 0; j < battlefieldColCount; j++) {
                    GameObject area = GameObject.Instantiate(battlefieldAreaPrefab);
                    area.transform.SetParent(row.transform, false);
                    area.name = "Battlefield Row " + (i+1) + " Area " + (j+1);

                    if (battlefieldStructure[i].Length != battlefieldColCount) {
                        Debug.LogError("BattlefieldStructure string (i,j=" + i + "," + j + ") should have a length of: " + battlefieldColCount);
                        continue;
                    }
                    var areaType = ConvertToAreaType(battlefieldStructure[i][j]);
                    area.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battlefieldStructure[i][j].ToString();
                    var battlefieldArea = area.GetComponent<BattlefieldArea>();
                    battlefieldArea.SetAreaType(areaType);
                    battlefieldArea.ShowGridLines(j != (battlefieldColCount - 1), i != 0);
                    gridLineMaterial.color = new Color(1f, 1f, 1f, 0f);
                    rowAreas.Add(battlefieldArea);
                }
                areas.Add(rowAreas);
            }
        }
    }

    void Start() {
    }

    public void PlacePlayerUnitAt(UnitType unitType, Vector3 position)
    {
        gridLineMaterial.color = new Color(1f, 1f, 1f, 0f);

        if (position.x > boundaryLeft   &&
            position.x < boundaryRight  &&
            position.y > boundaryBottom &&
            position.y < boundaryTop)
        {
            int colIndex = (int) ((position.x - boundaryLeft) / areaSize.x);
            int rowIndex = battlefieldRowCount - 1 - (int) ((position.y - boundaryBottom) / areaSize.y);
            var area = areas[rowIndex][colIndex];
            if (area.areaType != AreaType.CastleGate &&
                area.areaType != AreaType.NotUsed &&
                area.playerUnit == null)
            {
                var playerUnit = new PlayerUnit(unitType);
                area.PlacePlayerUnit(playerUnit);
            }
        }
    }

    public void DragPlayerUnitAt(UnitType unitType, Vector3 position)
    {
        if (!boundariesHasBeenSet) {
            boundariesHasBeenSet = true;
            Vector3 topLeftAreaPos = areas[0][0].transform.position;
            Vector3 bottomRightAreaPos = areas[battlefieldRowCount - 1][battlefieldColCount - 1].transform.position;
            areaSize.x = (bottomRightAreaPos.x - topLeftAreaPos.x) / (battlefieldColCount - 1);
            areaSize.y = (topLeftAreaPos.y - bottomRightAreaPos.y) / (battlefieldRowCount - 1);
            boundaryLeft   = topLeftAreaPos.x     - areaSize.x / 2f;
            boundaryRight  = bottomRightAreaPos.x + areaSize.x / 2f;
            boundaryBottom = bottomRightAreaPos.y - areaSize.y / 2f;
            boundaryTop    = topLeftAreaPos.y     + areaSize.y / 2f;
        }

        gridLineMaterial.color = new Color(1f, 1f, 1f, gridLineAlpha);

        if (position.x > boundaryLeft   &&
            position.x < boundaryRight  &&
            position.y > boundaryBottom &&
            position.y < boundaryTop)
        {
            int colIndex = (int) ((position.x - boundaryLeft) / areaSize.x);
            int rowIndex = battlefieldRowCount - 1 - (int) ((position.y - boundaryBottom) / areaSize.y);
            var area = areas[rowIndex][colIndex];
            if (area.areaType != AreaType.CastleGate &&
                area.areaType != AreaType.NotUsed &&
                area.playerUnit == null) {
                area.ShowDeploymentImage();
            }
            // Debug.Log("Drag to: (" + (rowIndex+1) + ", " + (colIndex+1) + ")");
        }
    }

    public AreaType ConvertToAreaType(char areaChar) {
        switch (areaChar)
        {
            case 't': return AreaType.CastleTower;
            case 'g': return AreaType.CastleGate;
            case 'i': return AreaType.InsideCastle;
            case 'o': return AreaType.OutsideCastle;
            case 'e': return AreaType.EnemySpawn;
            case '-': return AreaType.NotUsed;
            default: return AreaType.NotUsed;
        }
    }
}
