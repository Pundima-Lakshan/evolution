using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSave
{
    static string path = Application.dataPath + "/Resources/player.gpd";
    public static void SaveCreature(Creature creature) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        CreatureData data = new CreatureData(creature);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CreatureData LoadCreature() { 
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CreatureData data = formatter.Deserialize(stream) as CreatureData;
            stream.Close();

            return data;
        } else {
            Debug.Log("Save Not Found");
            return null;
        }
    }
}
