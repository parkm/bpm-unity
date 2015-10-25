using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityManager {

    public interface IAbility {
        float GetValue();
    }

    public class AbilityValue {
        public float value;
        public AbilityValue(float value) {
            this.value = value;
        }
    }

    // An ability that can have stacked values.
    public class StackedAbility : IAbility {
        public List<AbilityValue> values = new List<AbilityValue>();
        public float GetValue() {
            float total = 0;
            foreach (AbilityValue abilityValue in this.values) {
                total += abilityValue.value;
            }
            return total;
        }
    }
    // An ability that can only choose one value.
    public class SingularAbility : IAbility {
        public List<AbilityValue> values = new List<AbilityValue>();
        public float GetValue() {
            float greatestValue = 0;
            foreach (AbilityValue abilityValue in this.values) {
                if (abilityValue.value > greatestValue) {
                    greatestValue = abilityValue.value;
                }
            }
            return greatestValue;
        }
    }

    public Dictionary<string, StackedAbility> stackedAbilities = new Dictionary<string, StackedAbility>();
    public Dictionary<string, SingularAbility> singularAbilities = new Dictionary<string, SingularAbility>();
    public Dictionary<string, IAbility> abilities = new Dictionary<string, IAbility>();

    [System.Serializable]
    public class AbilityTemplate {
        public string key;
        public string description;
        public bool stacked;
    }
    public Dictionary<string, AbilityTemplate> abilityTemplateByKey = new Dictionary<string, AbilityTemplate>();

    public void InitAbilities(AbilityTemplate[] templates) {
        foreach (AbilityTemplate template in templates) {
            this.abilityTemplateByKey.Add(template.key, template);
            IAbility ability;
            if (template.stacked) {
                ability = new StackedAbility();
                this.stackedAbilities.Add(template.key, (StackedAbility) ability);
            } else {
                ability = new SingularAbility();
                this.singularAbilities.Add(template.key, (SingularAbility) ability);
            }
            this.abilities.Add(template.key, ability);
        }
    }

    public void AddAbilityValue(string id, AbilityValue value) {
        AbilityTemplate template = this.abilityTemplateByKey[id];
        if (template.stacked) {
            this.stackedAbilities[id].values.Add(value);
        } else {
            this.singularAbilities[id].values.Add(value);
        }
    }

    public void RemoveAbilityValue(string id, AbilityValue value) {
        AbilityTemplate template = this.abilityTemplateByKey[id];
        if (template.stacked) {
            this.stackedAbilities[id].values.Remove(value);
        } else {
            this.singularAbilities[id].values.Remove(value);
        }
    }

    public float GetAbilityValue(string id) {
        return abilities[id].GetValue();
    }

    // Converts float ability value to boolean
    public bool GetAbilityBoolValue(string id) {
        float val = abilities[id].GetValue();
        if (val == 0) return false;
        else if (val == 1) return true;
        else {
            Debug.LogError("Value: '" + val + "' is not bool.");
            return false;
        }
    }

}
