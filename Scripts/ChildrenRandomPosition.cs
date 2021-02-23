#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Yagiri {
	public class ChildrenRandomPosition : MonoBehaviour {

		[SerializeField]
		private Vector3 areaMin = new Vector3(-3.0f, -3.0f, -3.0f);
		[SerializeField]
		private Vector3 areaMax = new Vector3(3.0f, 3.0f, 3.0f);

		public void DoChildrenRandomPosition() {
			Transform _transform = GetComponent<Transform>();
			int count = 0;
			foreach (Transform i in _transform) {
				i.transform.localPosition =
					new Vector3(
						Random.Range(areaMin.x, areaMax.x),
						Random.Range(areaMin.y, areaMax.y),
						Random.Range(areaMin.z, areaMax.z));
				count++;
			}
		}

		[CanEditMultipleObjects]
		[CustomEditor(typeof(ChildrenRandomPosition))]
		public class ChildrenRandomPositionEditor : Editor {
			public override void OnInspectorGUI() {
				base.OnInspectorGUI();
				if (GUILayout.Button("ChildrenRandomPosition")) {
					(target as ChildrenRandomPosition).DoChildrenRandomPosition();
				}
			}
		}
	}
}
#endif
