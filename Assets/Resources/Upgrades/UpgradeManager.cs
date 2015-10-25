using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {
    public static UpgradeManager Instance { get; private set; }

    public AbilityManager.AbilityTemplate[] abilityTemplates;
    public AbilityManager abilityMan = new AbilityManager();

    void Awake() {
        if (UpgradeManager.Instance == null) {
            UpgradeManager.Instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        abilityMan.InitAbilities(this.abilityTemplates);
        abilityMan.AddAbilityValue("pinSpeed", new AbilityManager.AbilityValue(2.50f));
    }
}
