#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.TowerLayer
{
    public class TowerGridLayerGizmo : MonoBehaviour
    {
        [SerializeField] private TowerGridLayer TowerGridLayer;
        [SerializeField] private Color TowerSlotFaceColor;
        [SerializeField] private Color TowerSlotLineColor;

        private void OnDrawGizmos()
        {
            foreach (var towerPos in TowerGridLayer.TowerSlotPositions)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(towerPos), TowerSlotFaceColor, TowerSlotLineColor);
            }
        }

        private Rect GetWorldRect(Vector2Int position)
        {
            var offsetVector = TowerGridLayer.MyTransform.position;
            var rectX = offsetVector.x + position.x;
            var rectY = offsetVector.y + position.y;
            return new Rect(rectX, rectY, 1f, 1f);
        }
    }
}

#endif