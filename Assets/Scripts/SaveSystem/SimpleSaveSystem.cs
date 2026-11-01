using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class SimpleSaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "SaveData.xml");

    public static void SaveXML(SaveData data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        using (FileStream stream = new FileStream(SavePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }

        Debug.Log("Gra została zapisana: " + SavePath);
    }

    public static SaveData LoadXML()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("Brak pliku zapisu.");
            return null;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        using (FileStream stream = new FileStream(SavePath, FileMode.Open))
        {
            SaveData data = (SaveData)serializer.Deserialize(stream);

            Debug.Log("Gra została wczytana.");
            return data;
        }
    }
}
