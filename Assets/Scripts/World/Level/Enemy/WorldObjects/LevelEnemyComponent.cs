using System.Collections;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects
{
    // TODO: My logic here for moving enemies is wrong. This code is frame dependent.
    //       Maybe use physics, fixedDeltaTime, or own handling method inside update cycles
    public class LevelEnemyComponent : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        [SerializeField] private Transform _MyTransform;

        private float _moveSpeed;
        private List<Transform> _waypoints;
        private Transform _currentDestination;
        private int _currentDestinationIndex;
        private float _hp;

        public Transform MyTransform => _MyTransform;

        public const string RESOURCES_PATH = "WorldObjects/LevelEnemy";

        public void Init(float moveSpeed, float hp, List<Transform> waypoints)
        {
            _moveSpeed = moveSpeed;
            _hp = hp;
            _waypoints = waypoints;
        }

        public void StartMove()
        {
            _MyTransform.position = _waypoints[0].position;
            _currentDestinationIndex = 0;
            StartCoroutine(StartMoveCoroutine());
        }

        public void TakeDamage(float damage)
        {
            _hp -= damage;
            if (IsDead())
            {
                _signalBus.Fire(new LevelEnemyDiedSignal(this));
                Destroy(gameObject);
            }
        }

        public bool IsDead()
        {
            return _hp <= 0f;
        }

        private IEnumerator StartMoveCoroutine()
        {
            while(_currentDestinationIndex < _waypoints.Count)
            {
                _currentDestination = _waypoints[_currentDestinationIndex];
                yield return StartCoroutine(MoveToCurrentDestination());
                _currentDestinationIndex++;
            }

            _signalBus.Fire(new LevelEnemyReachedEndOfPathSignal(this));
            Destroy(gameObject);
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