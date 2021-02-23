using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yagiri {
	public class AutoSinMove : MonoBehaviour {

		[SerializeField]
		private float Radius = 1.0f;

		private Vector3 firstPos;

		// Use this for initialization
		void Start() {
			firstPos = transform.localPosition;
		}

		// Update is called once per frame
		void Update() {
			float angle = Time.time;

			// FIXME 係数をSirializeFieldに
			transform.localPosition = firstPos
				+ Radius * new Vector3(
					Mathf.Sin(angle * 1.0f + 2.0f),
					Mathf.Sin(angle * 0.7f + 4.0f),
					Mathf.Sin(angle * 0.5f + 6.0f)
					);


		}
	}
}