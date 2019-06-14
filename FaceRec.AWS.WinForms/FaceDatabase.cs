using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace FaceRec.AWS.WinForms
{
    public static class FaceDatabase
    {
        private static readonly string DbFileName = "FaceDatabase.facerecdb";
        private static readonly ConcurrentDictionary<string, string> DbInMemory = new ConcurrentDictionary<string, string>();

        public static string GetFaceName(string faceId)
        {
            if (DbInMemory.ContainsKey(faceId))
            {
                return DbInMemory[faceId];
            }

            return null;
        }

        public static void Add(string faceId, string faceName)
        {
            DbInMemory[faceId] = faceName;
        }

        public static void Load()
        {
            if (!File.Exists(DbFileName)) return;

            using (StreamReader reader = new StreamReader(DbFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] faceIdName = line.Split(';');
                    if (faceIdName.Length < 2) continue;

                    DbInMemory[faceIdName[0]] = faceIdName[1];
                }
            }
        }

        public static void Save()
        {
            File.Delete(DbFileName);

            using (StreamWriter writer = File.CreateText(DbFileName))
            {
                foreach (KeyValuePair<string, string> item in DbInMemory)
                {
                    writer.WriteLine($"{item.Key};{item.Value}");
                }
            }
        }
    }
}
