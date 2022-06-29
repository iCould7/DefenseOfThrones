using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects
{
    // TODO: My logic here for moving enemies is wrong. This code is frame dependent.
    //       Maybe use physics, fixedDeltaTime, or own handling method inside update cycles
    public class LevelEnemyComponent : MonoBehaviour
    {
        [SerializeField] private Transform _MyTransform;

        private float _moveSpeed;
        private List<Transform> _waypoints;
        private Transform _currentDestination;
        private int _currentDestinationIndex;

        public const string RESOURCES_PATH = "WorldObjects/LevelEnemy";

        public void Init(float moveSpeed, List<Transform> waypoints)
        {
            _moveSpeed = moveSpeed;
            _waypoints = waypoints;
        }

        public void StartMove()
        {
            _MyTransform.position = _waypoints[0].position;
            _currentDestinationIndex = 0;
            StartCoroutine(StartMoveCoroutine());
        }

        private IEnumerator StartMoveCoroutine()
        {
            while(_currentDestinationIndex < _waypoints.Count)
            {
                _currentDestination = _waypoints[_currentDestinationIndex];
                yield return StartCoroutine(MoveToCurrentDestination());
                _currentDestinationIndex++;
            }

            //TODO: Fire enemy reached end of path signal
        }

        private IEnumerator MoveToCurrentDestination()
        {
            while (_MyTransform.position != _currentDestination.position)
            {
                var moveUnits = _moveSpeed * Time.deltaTime;
                _MyTransform.position = Vector3.MoveTowards(_MyTransform.position, _currentDestination.position, moveUnits);
                yield return null;
            }
        }
    }
}