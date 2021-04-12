using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
    Archer,
    Cavalry,
    Knight,
    Prince,
    Swordsman
}

public class PlayerUnit
{
    public UnitType unitType;
    public int hpLeft;
    public PlayerUnitStats stats;

    public PlayerUnit(UnitType unitType) {
        this.unitType = unitType;
        this.stats = Player.instance.GetUnitStats(unitType);
        this.hpLeft = stats.hp;
    }

    public void UpdateUI() {}
}