using System;
using UnityEngine;

namespace SlidingPuzzle
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> OnBlockPressed;
        public event Action OnFinishMoving;
        public Vector2Int coord;

        Vector2Int _startingCoord;

        public void Init(Vector2Int startingCoord, Texture2D image)
        {
            _startingCoord = startingCoord;
            coord = startingCoord;
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Block");
            GetComponent<MeshRenderer>().material.mainTexture = image;
        }

        public void MoveToPosition(Vector2 target, float duration)
        {
            AnimateMove(target, duration);
        }

        private void OnMouseDown()
        {
            OnBlockPressed?.Invoke(this);
        }

        private void AnimateMove(Vector2 target, float duration)
        {
            LeanTween.move(gameObject, target, duration).setOnComplete(OnFinishMoving);
        }

        public bool isAtStartingCoor()
        {
            return coord == _startingCoord;
        }
    }
}
