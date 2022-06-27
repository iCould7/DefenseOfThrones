#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.TowerLayer
{
    public class TowerGridLayerGizmo : MonoBehaviour
    {
        [SerializeField] private TowerGridLayer _TowerGridLayer;
        [SerializeField] private Color _TowerSlotFaceColor;
        [SerializeField] private Color _TowerSlotLineColor;

        private void OnDrawGizmos()
        {
            foreach (var towerPos in _TowerGridLayer.TowerSlotPositions)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(towerPos), _TowerSlotFaceColor, _TowerSlotLineColor);
            }
        }

        private Rect GetWorldRect(Vector2Int position)
        {
            var offsetVector = _TowerGridLayer.MyTransform.position;
            var rectX = offsetVector.x + position.x;
            var rectY = offsetVector.y + position.y;
            return new Rect(rectX, rectY, 1f, 1f);
        }
    }
}

#endif