using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {
    // Serializes an object and saves to persistentDataPath + path
    public static void SaveObject(string path, System.Object objectToSave) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + path, FileMode.OpenOrCreate);
        formatter.Serialize(fileStream, objectToSave);
        fileStream.Close();
    }

    // Returns a deserialized object from persistentDataPath + path
    public static System.Object LoadObject(string path) {
        string finalPath = Application.persistentDataPath + "/" + path;
        if (!File.Exists(finalPath)) return null;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(finalPath, FileMode.Open);
        System.Object loadedObject = formatter.Deserialize(fileStream);
        fileStream.Close();
        return loadedObject;
    }
}
