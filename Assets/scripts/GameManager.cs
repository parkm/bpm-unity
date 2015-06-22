using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

    public Level[] availableLevels;

    Level currentLevel = null;

    void Awake() {
        //setCurrentLevel(availableLevels[1]);
        setCurrentLevel("spinnyLevel");
    }

    public void setCurrentLevel(Level level) {
        if (!availableLevels.Contains(level)) return;
        this.currentLevel = level;
        Instantiate(level);
    }

    public void setCurrentLevel(string levelName) {
        foreach (Level level in availableLevels) {
            if (level.name == levelName) {
                setCurrentLevel(level);
                return;
            }
        }
        Debug.LogError("Level '" + levelName + "' not found.");
    }

}
