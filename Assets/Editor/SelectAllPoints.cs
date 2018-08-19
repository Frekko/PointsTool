using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;

public class SelectAllPoints : ScriptableWizard
{
    [MenuItem("Tools/Save Points")]
    static void SellectAllPointsTool()
    {
        ScriptableWizard.DisplayWizard<SelectAllPoints>("Save Points", "Select/Serialization");
    }
    void OnWizardCreate()
    {
        Point[] allPoints = FindObjectsOfType<Point>();

        //для выделения на сцене
        GameObject[] allObjects = allPoints.Select(x => x.gameObject).ToArray();
        Selection.objects = allObjects;

        Debug.Log(Vector3.Distance(GameObject.Find("Point1").transform.position, GameObject.Find("Point2").transform.position));
        Debug.Log(Math.Sqrt(Math.Pow((-7.03 - 8.67), 2) + Math.Pow((-2.65 - 5.61), 2) + Math.Pow((6.67 - -4.13),2)));
        Serialization(allPoints);
    }

    void Serialization(Point[] Points)
    {
        List<WrapPoint> listPoint = new List<WrapPoint>();
        foreach (var point in Points)
        {
            WrapPoint newWrapPoint = new WrapPoint(point);
            listPoint.Add(newWrapPoint);
        }
        string json = JsonHelper.ToJson<WrapPoint>(listPoint.ToArray());
        var path = EditorUtility.SaveFilePanel("Serialize Points to JSON", "", "Serialization.json", "json");
        File.WriteAllLines(path, new[] { json });
    }

    //ToJson не съедает массивы, helper - его обёртка
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
            return JsonUtility.ToJson(wrapper, true);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}

