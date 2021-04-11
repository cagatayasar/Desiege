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
    public List<string> battlefieldStructure;
    public int battlefieldRowCount;
    public int battlefieldColCount;

    void Awake() {
        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
            return;
        } else {
            instance = this;
        } 
    }

    void Start()
    {
        // make these global
        if (battlefieldStructure.Count != battlefieldRowCount) {
            Debug.LogError("BattlefieldStructure list should have a size of: " + battlefieldRowCount);
        } else {
            for (int i = 0; i < battlefieldRowCount; i++) {
                Transform row = transform.GetChild(i);
                for (int j = 0; j < battlefieldColCount; j++) {
                    if (battlefieldStructure[i].Length != battlefieldColCount) {
                        Debug.LogError("BattlefieldStructure string (i,j=" + i + "," + j + ") should have a length of: " + battlefieldColCount);
                        continue;
                    }
                    var areaType = ConvertToAreaType(battlefieldStructure[i][j]);
                    Transform area = row.GetChild(j);
                    area.GetChild(0).GetComponent<TextMeshProUGUI>().text = battlefieldStructure[i][j].ToString();
                    if (areaType == AreaType.CastleTower) {
                        area.GetChild(1).gameObject.SetActive(true);
                    } else if (areaType == AreaType.CastleGate) {
                        area.GetChild(2).gameObject.SetActive(true);
                    } else if (areaType == AreaType.OutsideCastle) {
                        area.GetChild(3).gameObject.SetActive(true);
                    } else if (areaType == AreaType.NotUsed) {
                        area.GetChild(4).gameObject.SetActive(true);
                    } else if (areaType == AreaType.InsideCastle) {
                        // area.GetChild(5).gameObject.SetActive(true);
                    }
                }
            }
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
