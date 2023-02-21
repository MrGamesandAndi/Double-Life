using ShopSystem;
using UnityEngine;

namespace GridSystem
{
    public class BuildingGhost : MonoBehaviour
    {
        private Transform visual;
        private FurnitureItem placedObjectTypeSO;

        private void Start()
        {
            RefreshVisual();
            GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        }

        private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
        {
            RefreshVisual();
        }

        private void LateUpdate()
        {
            Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
            targetPosition.y = 1f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

            transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
        }

        private void RefreshVisual()
        {
            if (visual != null)
            {
                Destroy(visual.gameObject);
                visual = null;
            }

            FurnitureItem placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();

            if (placedObjectTypeSO != null)
            {
                visual = Instantiate(placedObjectTypeSO.model, Vector3.zero, Quaternion.identity);
                visual.parent = transform;
                visual.localPosition = Vector3.zero;
                visual.localEulerAngles = Vector3.zero;
                SetLayerRecursive(visual.gameObject, 11);
            }
        }

        private void SetLayerRecursive(GameObject targetGameObject, int layer)
        {
            targetGameObject.layer = layer;
            foreach (Transform child in targetGameObject.transform)
            {
                SetLayerRecursive(child.gameObject, layer);
            }
        }
    }
}
