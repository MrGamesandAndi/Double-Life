using UnityEngine;

namespace Buildings.Apartments.Rooms
{
    public class ReturnObjectToSpawn : MonoBehaviour
    {
        [SerializeField] Transform _spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            other.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, other.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
