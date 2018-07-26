using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
using SimpleJson;

public class SelectAllPoints : ScriptableWizard
{
    private List<WrapPoint> listPoint = new List<WrapPoint>();

    [MenuItem("Tools/Save Points")]
    static void SellectAllPointsTool()
    {
        ScriptableWizard.DisplayWizard<SelectAllPoints>("Save Points", "Select/Serialization");
    }
    void OnWizardCreate()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] allPoints = allObjects.Where(x => x.GetComponent(typeof(Point)) != null).ToArray();
        Selection.objects = allPoints;
        Serialization(allPoints);
    }

    void Serialization(GameObject[] objectPoints)
    {
        foreach (var objPoint in objectPoints)
        {
            WrapPoint newWrapPoint = new WrapPoint(objPoint);
            Debug.Log(newWrapPoint.x);
            listPoint.Add(newWrapPoint);
        }

        string json = JsonHelper.ToJson<WrapPoint>(listPoint.ToArray(), true);
        //Debug.Log(json);
        var path = EditorUtility.SaveFilePanel("Serialize Points to JSON", "", "Serialization.json", "json");
        File.WriteAllLines(path, new[] { json });
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}

