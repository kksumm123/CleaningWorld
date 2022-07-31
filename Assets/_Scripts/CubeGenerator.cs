using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    IEnumerator Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var cube = FactoryManager.Instance.GetTestCube();
            cube.transform.position = Vector3.zero + Vector3.right * i;
            yield return new WaitForSeconds(1);
        }
    }
}
