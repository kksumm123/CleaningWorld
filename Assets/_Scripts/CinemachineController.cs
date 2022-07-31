using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] Transform playerFollower;

    private void Start()
    {
        playerFollower.SetParent(Player.Instance.transform);
    }
}
