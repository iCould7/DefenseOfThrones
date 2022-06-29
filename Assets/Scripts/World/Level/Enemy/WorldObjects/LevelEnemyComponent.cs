using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects
{
    public class LevelEnemyComponent : MonoBehaviour
    {
        [SerializeField] private Transform _MyTransform;

        private float _moveSpeed;
        private List<Transform> _waypoints;
        private Transform _currentDestination;
        private int _currentDestinationIndex;

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