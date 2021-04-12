using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetCoordinate {
    public int x;
    public int y;
    
    public TargetCoordinate(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

[CreateAssetMenu(menuName = "Player Unit Stats", fileName = "PlayerUnitStats")]
public class PlayerUnitStats : ScriptableObject
{
    public Sprite sprite;
    public string unitName;
    public int hp;
    public int damage;
    public bool targetsOnlyTheFirst;
    [Tooltip("(0, 0) is the center.")]
    public List<TargetCoordinate> targets;
}
