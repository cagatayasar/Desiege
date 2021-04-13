using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattlePhase {
    Null,
    Deployment,
    PlayerAction,
    EnemyAction,
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [HideInInspector]
    public int waveCount;
    [HideInInspector]
    public BattlePhase battlePhase;

    void Awake() {
        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        waveCount = 1;
        battlePhase = BattlePhase.Null;
    }

    void Start() {
        NextPhase();
    }

    public void NextPhase() {
        if (battlePhase == BattlePhase.Null) {
            battlePhase = BattlePhase.Deployment;
            Player.instance.PlayDeploymentPhase();
        } else if (battlePhase == BattlePhase.Deployment) {
        } else if (battlePhase == BattlePhase.PlayerAction) {
        } else if (battlePhase == BattlePhase.EnemyAction) {
            battlePhase = BattlePhase.Deployment;
            Player.instance.PlayDeploymentPhase();
        }
    }
}
