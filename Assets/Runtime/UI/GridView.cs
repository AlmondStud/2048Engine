using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Almond._2048 {
	public class GridView : MonoBehaviour {
		[SerializeField] private Vector2 tileSize;
		[SerializeField] private int gridRowCount;
		[SerializeField] private int gridColumnCount;

		public void InitView() {

		}


		protected virtual void OnDrawGizmos() {
			if(gridRowCount == 0 || gridColumnCount == 0)
				return;

			for(int i = 0; i < gridRowCount; i++) {
				for(int j = 0; j < gridColumnCount; j++) {
					var pos = transform.position;
					pos.x += tileSize.x * i;
					pos.y += tileSize.y * j;

					Gizmos.DrawWireCube(pos, new Vector3(tileSize.x, tileSize.y, 0));
				}
			}
		}
	}
}