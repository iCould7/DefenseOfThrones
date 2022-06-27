#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer
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
            DrawReachableSegmentGizmos();
            DrawFaultySegmentGizmos();
            DrawWayPointGizmos();
            DrawStartingPointGizmo();
        }

        private void DrawReachableSegmentGizmos()
        {
            foreach (var reachableSegment in PathGridLayer.OrderedReachableSegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(reachableSegment.Rect), ReachableSegmentFaceColor, ReachableSegmentLineColor);
            }
        }

        private void DrawFaultySegmentGizmos()
        {
            foreach (var faultySegment in PathGridLayer.FaultySegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(faultySegment.Rect), FaultySegmentFaceColor, FaultySegmentLineColor);
            }
        }

        private void DrawWayPointGizmos()
        {
            foreach (var waypoint in PathGridLayer.Waypoints)
            {
                Gizmos.DrawSphere(waypoint, 0.1f);
            }
        }

        private void DrawStartingPointGizmo()
        {
            if (PathGridLayer.Waypoints.Count > 0)
            {
                var oldGizmoColor = Gizmos.color;
                Gizmos.color = Color.green;

                Gizmos.DrawSphere(PathGridLayer.Waypoints[0], 0.2f);

                Gizmos.color = oldGizmoColor;
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