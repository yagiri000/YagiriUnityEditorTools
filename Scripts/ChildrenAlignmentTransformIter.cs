#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// アタッチしたオブジェクトの子オブジェクトたちの位置、角度をずらしながら整列する
/// </summary>
namespace Yagiri
{
    public class ChildrenAlignmentTransformIter : MonoBehaviour
    {
        [SerializeField]
        public Vector3 firstPos;
        [SerializeField]
        public Vector3 difPos = new Vector3(2.0f, 0.0f, 0.0f);
        [SerializeField]
        public Vector3 firstRot;
        [SerializeField]
        public Vector3 difRot = new Vector3(0.0f, 60.0f, 0.0f);

        public void DoChildrenAlignmentTransformIter()
        {
            Vector3 pos = firstPos;
            Quaternion rot = Quaternion.Euler(firstRot);

            foreach (Transform child in transform)
            {
                child.transform.localPosition = pos;
                child.transform.localRotation = rot;
                pos += difPos;
                pos = Quaternion.Euler(difRot) * pos;
                rot *= Quaternion.Euler(difRot);
            }
        }

        [CanEditMultipleObjects]
        [CustomEditor(typeof(ChildrenAlignmentTransformIter))]
        public class ChildrenAlignmentTransformIterEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("子オブジェクトを整列\r\n nextPos = (nowPos + difPos) * difRot"))
                {
                    (target as ChildrenAlignmentTransformIter).DoChildrenAlignmentTransformIter();
                }
            }
        }
    }
}
#endif
