using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (CharacterManager.Instance.IsInputEnabled)
            {
                var obj = Instantiate(
                    _obstaclePrefab,
                    new Vector3(0, 7.5f, CharacterManager.Instance.transform.position.z + 200f),
                    Quaternion.Euler(0, 0, Random.Range(0, 360)),
                    transform
                );
                obj.AddComponent<SelfDestruct>();
            }
            var distanceTravelled = CharacterManager.Instance.DistanceTravelled;
            if (distanceTravelled < 1000)
            {
                yield return new WaitForSeconds(1);
            }
            else if (distanceTravelled < 2000)
            {
                yield return new WaitForSeconds(0.8f);
            }
            else if (distanceTravelled < 3000)
            {
                yield return new WaitForSeconds(0.6f);
            }
            else if (distanceTravelled < 4000)
            {
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
