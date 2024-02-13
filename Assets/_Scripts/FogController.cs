using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField]
    private float _zDistanceFromPlayer = 100f;

    private void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            CharacterManager.Instance.transform.position.z + _zDistanceFromPlayer
        );
    }
}
