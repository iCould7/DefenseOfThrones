#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer
{
    public class PathGridLayerGizmo : MonoBehaviour
    {
        [SerializeField] private PathGridLayer PathGridLayer;
        [SerializeField] private Color ReachableSegmentFaceColor;
        [SerializeField] private Color ReachableSegmentLineColor;


        private void OnDrawGizmos()
        {
            foreach (var reachableSegment in PathGridLayer.OrderedReachableSegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(reachableSegment.Rect), ReachableSegmentFaceColor, ReachableSegmentLineColor);
            }
        }

        private Rect GetWorldRect(RectInt rectInt)
        {
            var offsetVector = PathGridLayer.MyTransform.position;
            var rectX = offsetVector.x + rectInt.x;
            var rectY = offsetVector.y + rectInt.y;
            return new Rect(rectX, rectY, rectInt.width, rectInt.height);
        }
    }
}

#endif