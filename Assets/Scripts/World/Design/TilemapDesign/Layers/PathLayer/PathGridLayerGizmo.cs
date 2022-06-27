#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer
{
    public class PathGridLayerGizmo : MonoBehaviour
    {
        [SerializeField] private PathGridLayer _PathGridLayer;
        [SerializeField] private Color _ReachableSegmentFaceColor;
        [SerializeField] private Color _ReachableSegmentLineColor;
        [SerializeField] private Color _FaultySegmentFaceColor;
        [SerializeField] private Color _FaultySegmentLineColor;

        private void OnDrawGizmos()
        {
            DrawReachableSegmentGizmos();
            DrawFaultySegmentGizmos();
            DrawWayPointGizmos();
            DrawStartingPointGizmo();
        }

        private void DrawReachableSegmentGizmos()
        {
            foreach (var reachableSegment in _PathGridLayer.OrderedReachableSegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(reachableSegment._Rect), _ReachableSegmentFaceColor, _ReachableSegmentLineColor);
            }
        }

        private void DrawFaultySegmentGizmos()
        {
            foreach (var faultySegment in _PathGridLayer.FaultySegments)
            {
                Handles.DrawSolidRectangleWithOutline(GetWorldRect(faultySegment._Rect), _FaultySegmentFaceColor, _FaultySegmentLineColor);
            }
        }

        private void DrawWayPointGizmos()
        {
            foreach (var waypoint in _PathGridLayer.Waypoints)
            {
                Gizmos.DrawSphere(waypoint, 0.1f);
            }
        }

        private void DrawStartingPointGizmo()
        {
            if (_PathGridLayer.Waypoints.Count > 0)
            {
                var oldGizmoColor = Gizmos.color;
                Gizmos.color = Color.green;

                Gizmos.DrawSphere(_PathGridLayer.Waypoints[0], 0.2f);

                Gizmos.color = oldGizmoColor;
            }
        }

        private Rect GetWorldRect(RectInt rectInt)
        {
            var offsetVector = _PathGridLayer.MyTransform.position;
            var rectX = offsetVector.x + rectInt.x;
            var rectY = offsetVector.y + rectInt.y;
            return new Rect(rectX, rectY, rectInt.width, rectInt.height);
        }
    }
}

#endif