using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Point : MonoBehaviour {

    public float Param1;
    public float Param2;

    public float x { get { return transform.position.x; } }
    public float y { get { return transform.position.y; } }
    public float z { get { return transform.position.z; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
public class WrapPoint
{

    public float WrapParam1;
    public float WrapParam2;
    public float x;
    public float y;
    public float z;

    public WrapPoint(Point pointToWrap)
    {
        this.WrapParam1 = pointToWrap.Param1;
        this.WrapParam2 = pointToWrap.Param2;
        this.x = pointToWrap.x;
        this.y = pointToWrap.y;
        this.z = pointToWrap.z;
    }
}




