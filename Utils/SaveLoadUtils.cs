using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class SaveLoadUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/
        
        public static void SaveToBinFile(object obj, string path)
        {
            Stream stream = null;
            try
            {
                string fullPath = string.Concat(Application.persistentDataPath, path);
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, obj);
            }
            finally
            {
                stream?.Close();
            }
        }

        public static object LoadFromBinFile(string path)
        {
            string fullPath = string.Concat(Application.persistentDataPath, path);
            if (File.Exists(fullPath))
            {
                Stream stream = null;
                object obj;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                    obj = formatter.Deserialize(stream);
                }
                finally
                {
                    stream?.Close();
                }

                return obj;
            }

            return null;
        }

        public static void SaveToJsonFile(object obj, string path)
        {
            Stream file = null;
            try
            {
                string saveData = JsonUtility.ToJson(obj, true);
                IFormatter formatter = new BinaryFormatter();
                file = File.Create(string.Concat(Application.persistentDataPath, path));
                formatter.Serialize(file, saveData);
            }
            finally
            {
                file?.Close();
            }
        }

        public static void LoadFromJsonFile(object obj, string path)
        {
            string fullPath = string.Concat(Application.persistentDataPath, path);
            if (File.Exists(fullPath))
            {
                Stream file = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    file = File.Open(fullPath, FileMode.Open);
                    JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), obj);
                }
                finally
                {
                    file?.Close();
                }
            }
        }
    }
}