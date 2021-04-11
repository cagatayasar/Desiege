using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
    Archer,
    Cavalry,
    Hero,
    Knight,
    Swordsman
}

public class AllyUnit
{
    public UnitType unitType;
    public int hp;
    public int maxHp;
    public int dmg;

    public AllyUnit(UnitType unitType) {
        
    }

    public void UpdateUI() {
        
    }
}