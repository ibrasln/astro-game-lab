using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = transform.GetComponentInChildren<Canvas>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        transform.rotation = new Quaternion(0, 180, 0, 1);
    }
}
