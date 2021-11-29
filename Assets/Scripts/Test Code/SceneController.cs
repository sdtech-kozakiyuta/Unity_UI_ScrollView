using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField, Range(0, 9999)] int contentNum = 1000;
    
    void Awake()
    {
        TestContentDatabase.SetupDatabase(contentNum);
    }
}
