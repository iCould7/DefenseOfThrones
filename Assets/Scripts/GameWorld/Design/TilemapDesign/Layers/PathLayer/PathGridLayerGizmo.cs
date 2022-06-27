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
        [SerializeField] private Color FaultySegmentFaceColor;
        [SerializeField] private Color FaultySegmentLineColor;

        private void OnDrawGizmos()
        {
            foreach (var reachableSegment in PathGridLayer.OrderedReachableSegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(reachableSegment.Rect), ReachableSegmentFaceColor, ReachableSegmentLineColor);
            }

            foreach (var faultySegment in PathGridLayer.FaultySegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(faultySegment.Rect), FaultySegmentFaceColor, FaultySegmentLineColor);
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