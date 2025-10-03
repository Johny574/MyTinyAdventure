using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.IO;
#endif

namespace FletcherLibraries
{
    public static class AssetDatabaseHelpers
    {
#if UNITY_EDITOR
        public static T[] GetAtPath<T>(string path, bool requireMainAssetType = true) where T : Object
        {
            List<T> list = new List<T>();
            getAllAtPathRecursive<T>(path, "", list, requireMainAssetType);
            return list.ToArray();
        }

        public static void GetAtPath<T>(string path, List<T> outList, bool requireMainAssetType = true) where T : Object
        {
            getAllAtPathRecursive<T>(path, "", outList, requireMainAssetType);
        }

        public static T GetFirstAtPath<T>(string path) where T : Object
        {
            return getFirstAtPathRecursive<T>(path, "", true);
        }

        private static void getAllAtPathRecursive<T>(string path, string namePrefix, List<T> list, bool requireMainAssetType) where T : Object
        {
            string fullPath = Application.dataPath.Replace("Assets", "") + path;
            string[] folderEntries = Directory.GetDirectories(fullPath);
            
            foreach (string folderName in folderEntries)
            {
                int index = folderName.LastIndexOf("/");
                string name = folderName.Substring(index + 1);
                
                getAllAtPathRecursive<T>(path + name + "/", name + "/", list, requireMainAssetType);
            }

            string[] fileEntries = Directory.GetFiles(fullPath);
            foreach (string fileName in fileEntries)
            {
                int index = fileName.LastIndexOf("/");
                string name = fileName.Substring(index + 1);
                string localPath = path + name;

                if (!requireMainAssetType || AssetDatabase.GetMainAssetTypeAtPath(localPath) == typeof(T))
                {
                    T t = AssetDatabase.LoadAssetAtPath<T>(localPath);
                    if (t != null)
                        list.Add(t);
                }
            }
        }

        private static T getFirstAtPathRecursive<T>(string path, string namePrefix, bool requireMainAssetType) where T : Object
        {
            string fullPath = Application.dataPath.Replace("Assets", "") + path;
            string[] folderEntries = Directory.GetDirectories(fullPath);
            
            foreach (string folderName in folderEntries)
            {
                int index = folderName.LastIndexOf("/");
                string name = folderName.Substring(index + 1);
                
                T retVal = getFirstAtPathRecursive<T>(path + name + "/", name + "/", requireMainAssetType);

                if (retVal != null)
                    return retVal;
            }

            string[] fileEntries = Directory.GetFiles(fullPath);
            foreach (string fileName in fileEntries)
            {
                int index = fileName.LastIndexOf("/");
                string name = fileName.Substring(index + 1);
                string localPath = path + name;

                if (!requireMainAssetType || AssetDatabase.GetMainAssetTypeAtPath(localPath) == typeof(T))
                {
                    T t = AssetDatabase.LoadAssetAtPath<T>(localPath);
                    if (t != null)
                        return t;
                }
            }

            return null;
        }
#endif
    }
}
