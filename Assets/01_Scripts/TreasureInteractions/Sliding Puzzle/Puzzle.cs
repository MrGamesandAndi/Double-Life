using System.Collections.Generic;
using UnityEngine;

namespace SlidingPuzzle
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField] Texture2D _image;
        [SerializeField] int _blocksPerLine = 3;
        [SerializeField] int _shuffleLength = 20;
        [SerializeField] float _defaultMoveDuration = 0.2f;
        [SerializeField] float _shuffleMoveDuration = 0.1f;
        [SerializeField] GameObject _finishedScreen;

        PuzzleState _state;
        Block _emptyBlock;
        Block[,] _blocks;
        Queue<Block> _inputs;
        bool _blockIsMoving;
        int _shuffleMovesRemaining;
        Vector2Int _prevShuffleOffset;

        private void Start()
        {
            CreatePuzzle();
            StartShuffle();
        }

        private void CreatePuzzle()
        {
            _blocks = new Block[_blocksPerLine, _blocksPerLine];
            Texture2D[,] imageSlices = ImageSlicer.GetSlices(_image, _blocksPerLine);

            for (int y = 0; y < _blocksPerLine; y++)
            {
                for (int x = 0; x < _blocksPerLine; x++)
                {
                    GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    blockObject.transform.position = -Vector2.one * (_blocksPerLine - 1) * 0.5f + new Vector2(x, y);
                    blockObject.transform.parent = transform;
                    Block block = blockObject.AddComponent<Block>();
                    block.OnBlockPressed += PlayerMoveBlockInput;
                    block.OnFinishMoving += OnBlockFinishedMoving;
                    block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                    _blocks[x, y] = block;

                    if (y == 0 && x == _blocksPerLine - 1)
                    {
                        blockObject.SetActive(false);
                        _emptyBlock = block;
                    }
                }
            }

            Camera.main.orthographicSize = _blocksPerLine * 0.55f;
            _inputs = new Queue<Block>();
        }

        private void PlayerMoveBlockInput(Block blockToMove)
        {
            if (_state == PuzzleState.InPlay)
            {
                _inputs.Enqueue(blockToMove);
                MakeNextPlayerMove();
            }
        }

        private void MoveBlock(Block blockToMove, float duration)
        {
            if ((blockToMove.coord - _emptyBlock.coord).sqrMagnitude == 1)
            {
                _blocks[blockToMove.coord.x, blockToMove.coord.y] = _emptyBlock;
                _blocks[_emptyBlock.coord.x, _emptyBlock.coord.y] = blockToMove;
                Vector2Int targetCoord = _emptyBlock.coord;
                _emptyBlock.coord = blockToMove.coord;
                blockToMove.coord = targetCoord;
                Vector2 targetPosition = _emptyBlock.transform.position;
                _emptyBlock.transform.position = blockToMove.transform.position;
                blockToMove.MoveToPosition(targetPosition, duration);
                _blockIsMoving = true;
            }
        }

        private void OnBlockFinishedMoving()
        {
            _blockIsMoving = false;
            CheckIfSolved();

            if (_state == PuzzleState.InPlay)
            {
                MakeNextPlayerMove();
            }
            else if (_state == PuzzleState.Shuffling)
            {
                if (_shuffleMovesRemaining > 0)
                {
                    MakeNextShuffleMove();
                }
                else
                {
                    _state = PuzzleState.InPlay;
                }
            }
        }

        private void MakeNextPlayerMove()
        {
            while (_inputs.Count > 0 && !_blockIsMoving)
            {
                MoveBlock(_inputs.Dequeue(), _defaultMoveDuration);
            }
        }

        private void MakeNextShuffleMove()
        {
            Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
            int randomIndex = Random.Range(0, offsets.Length);

            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];

                if (offset != _prevShuffleOffset * -1)
                {
                    Vector2Int moveBlockCoord = _emptyBlock.coord + offset;

                    if (moveBlockCoord.x >= 0 && moveBlockCoord.x < _blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < _blocksPerLine)
                    {
                        MoveBlock(_blocks[moveBlockCoord.x, moveBlockCoord.y], _shuffleMoveDuration);
                        _shuffleMovesRemaining--;
                        _prevShuffleOffset = offset;
                        break;
                    }
                }
            }
        }

        private void StartShuffle()
        {
            _state = PuzzleState.Shuffling;
            _shuffleMovesRemaining = _shuffleLength;
            MakeNextShuffleMove();
        }

        private void CheckIfSolved()
        {
            foreach (Block block in _blocks)
            {
                if (!block.isAtStartingCoor())
                {
                    return;
                }
            }

            _state = PuzzleState.Solved;
            _emptyBlock.gameObject.SetActive(true);
            _finishedScreen.SetActive(true);
        }
    }
}
