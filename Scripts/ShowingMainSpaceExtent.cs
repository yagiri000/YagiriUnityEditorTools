#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
namespace Yagiri
{
    public class ShowingMainSpaceExtent : MonoBehaviour
    {
        [SerializeField]
        private Vector3 RayFirstPosDir = new Vector3(0.0f, 1.4f, 0.0f);

        [SerializeField]
        private float RayDist = 10.0f;

        [SerializeField]
        private LayerMask mask = -1;

        // Scene ビュー上方「Gizmos」メニューから可視化/不可視化
        void OnDrawGizmosSelected()
        {
            var center = transform.position + RayFirstPosDir;

            int DivNum = 12;
            for (int i = 0; i < DivNum; i++)
            {
                float hitDist = RayDist;

                float ni = (float)i / DivNum;
                float angle = Mathf.PI * 2.0f * ni;
                Vector3 dir = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));

                // 横方向(XZ)にレイを飛ばす。ヒットしたらそこを境界とする
                var ray = new Ray(center, dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RayDist, mask))
                {
                    hitDist = (center - hit.point).magnitude;
                }

                // 移動しながら下方向にレイを飛ばし、段差が来たら境界とする
                float preHeight = 0.0f;
                for (int j = 0; j < 12; j++)
                {
                    float nj = (float)j / 12.0f;
                    var rayFirst = center + dir * RayDist * nj;
                    ray = new Ray(rayFirst, Vector3.down);
                    if (Physics.Raycast(ray, out hit, 5.0f, mask))
                    {
                        if (j != 0 && Mathf.Abs(preHeight - hit.point.y) > 0.1f)
                        {
                            hitDist = Mathf.Min(hitDist, (center - rayFirst).magnitude);
                            break;
                        }
                        preHeight = hit.point.y;
                    }
                    else
                    {
                        hitDist = Mathf.Min(hitDist, (center - rayFirst).magnitude);
                        break;
                    }
                }

                DrawResult(center, center + dir * RayDist, hitDist / RayDist);
            }
        }

        private void DrawCursor(Vector3 p)
        {
            Gizmos.color = new Color(1.0f, 0.1f, 0.1f);
            const float dif = 0.3f;
            Gizmos.DrawLine(p + Vector3.up * dif, p - Vector3.up * dif);
            Gizmos.DrawLine(p + Vector3.right * dif, p - Vector3.right * dif);
            Gizmos.DrawLine(p + Vector3.forward * dif, p - Vector3.forward * dif);
        }

        private void DrawUpDownLine(Vector3 p)
        {
            Gizmos.color = Color.gray;
            const float dif = 0.3f;
            Gizmos.DrawLine(p + Vector3.up * dif, p - Vector3.up * dif);
        }

        private void DrawResult(Vector3 start, Vector3 end, float rate)
        {
            Color nearCol = Color.red;
            Color farCol = Color.green;
            Gizmos.color = Color.Lerp(nearCol, farCol, rate);
            Vector3 hitP = Vector3.Lerp(start, end, rate);
            Gizmos.DrawSphere(hitP, 0.4f);
            Gizmos.DrawLine(start, hitP);
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(hitP, end);
            for (int i = 0; i < 12; i++)
            {
                float nj = i / 12.0f;
                DrawUpDownLine(Vector3.Lerp(start, end, nj));
            }
        }
    }
}
#endif
