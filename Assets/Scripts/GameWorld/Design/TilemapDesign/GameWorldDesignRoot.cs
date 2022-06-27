using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.TowerLayer;
using UnityEngine;

#if UNITY_EDITOR

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign
{
    public class GameWorldDesignRoot : MonoBehaviour
    {
        [SerializeField] private PathGridLayer PathGridLayer;
        [SerializeField] private TowerGridLayer TowerGridLayer;

        public bool IsPositionInPlayArea(Vector2Int position)
        {
            return true; //TODO implement logic
        }
    }
}

#endif