using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObjectToSpawn : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = _spawnPoint.position;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }
}
