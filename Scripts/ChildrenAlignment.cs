#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// アタッチしたオブジェクトの子オブジェクトたちの位置、角度をずらしながら整列する
/// </summary>
namespace Yagiri
{
    public class ChildrenAlignment : MonoBehaviour
    {

        [SerializeField]
        public Vector3 positionDif = new Vector3(1.0f, 0.0f, 0.0f);
        [SerializeField]
        public Vector3 angleDif;

        public void DoChildrenAlignment()
        {
            Transform _transform = GetComponent<Transform>();
            int count = 0;
            foreach (Transform i in _transform)
            {
                i.transform.localPosition = count * positionDif;
                var eularAng = count * angleDif;
                i.transform.localRotation = Quaternion.Euler(eularAng);
                count++;
            }
        }

        [CanEditMultipleObjects]
        [CustomEditor(typeof(ChildrenAlignment))]
        public class ChildrenAlignmentEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("ChildrenAlignment"))
                {
                    (target as ChildrenAlignment).DoChildrenAlignment();
                }
            }
        }
    }
}
#endif
