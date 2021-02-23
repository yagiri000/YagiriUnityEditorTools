#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 上空から等間隔にレイを飛ばし、あたった点に線を表示
/// </summary>
namespace Yagiri
{
    public class ShowingCollisionOnGridRay : MonoBehaviour
    {
        [SerializeField]
        private float areaSize = 20.0f;

        [SerializeField]
        private int num = 3;

        [SerializeField]
        private LayerMask mask = -1;

        [SerializeField]
        private Vector3 rayDif = new Vector3(0.0f, 0.5f, 0.0f);

        private Vector3[] GetTakingPoints()
        {
            var result = new List<Vector3>();

            var center = transform.position;
            var rayY = 20.0f;

            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    Vector3 delta = new Vector3(
                            Mathf.Lerp(-areaSize, areaSize, (float)i / (num - 1)),
                            rayY,
                            Mathf.Lerp(-areaSize, areaSize, (float)j / (num - 1)));

                    var ray = new Ray(center + delta, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 9999.0f, mask))
                    {
                        result.Add(hit.point + rayDif);
                    }
                }
            }
            return result.ToArray();
        }

        private Vector3 ShotRay(Vector3 firstPos)
        {
            var result = Vector3.zero;
            var ray = new Ray(firstPos, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 9999.0f, mask))
            {
                result = hit.point;
            }

            return result;
        }


        // Scene ビュー上方「Gizmos」メニューから可視化/不可視化
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1.0f, 0.25f, 0.25f);
            foreach (var basePos in GetTakingPoints())
            {
                const float dif = 0.3f;
                Gizmos.DrawLine(basePos + Vector3.up * dif, basePos - Vector3.up * dif);
                Gizmos.DrawLine(basePos + Vector3.right * dif, basePos - Vector3.right * dif);
                Gizmos.DrawLine(basePos + Vector3.forward * dif, basePos - Vector3.forward * dif);
            }
        }

    }
}
#endif
