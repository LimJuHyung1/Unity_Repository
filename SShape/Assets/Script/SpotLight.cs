using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpotLight : MonoBehaviour
{
    public string objectTag = "MyObject"; // 비추고자 하는 오브젝트 태그
                                          
    void Update()
    {
        // Spot Light가 비추는 태그 설정
        GetComponent<Light>().cullingMask = 1 << LayerMask.NameToLayer(objectTag);
    }
}
