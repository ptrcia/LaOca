using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static CellManager instance;
    public Transform[] cells;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
