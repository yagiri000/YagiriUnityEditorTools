#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Yagiri {
	public class ChildrenRandomScale : MonoBehaviour {

		[SerializeField]
		private float min = 0.1f;
		[SerializeField]
		private float max = 1.0f;

		public void DoChildrenRandomScale() {
			Transform _transform = GetComponent<Transform>();
			int count = 0;
			foreach (Transform i in _transform) {
				i.transform.localScale = Vector3.one * Random.Range(min, max);
				count++;
			}
		}

		[CanEditMultipleObjects]
		[CustomEditor(typeof(ChildrenRandomScale))]
		public class ChildrenRandomScaleEditor : Editor {
			public override void OnInspectorGUI() {
				base.OnInspectorGUI();
				if (GUILayout.Button("ChildrenRandomScale")) {
					(target as ChildrenRandomScale).DoChildrenRandomScale();
				}
			}
		}
	}
}
#endif
