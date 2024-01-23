using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Almond._2048 {
	public class GridController {
		public static int GridRowCount = 4;
		public static int GridColumnCount = 4;
		public static float MoveAnimTime = 0.1f;

		private Tile[] tiles;

		public IEnumerator InitializeGrid() {
			tiles = new Tile[GridRowCount * GridColumnCount];

			// 1개 랜덤 위치에서 생성
			var ramdomOne = Random.Range(0, tiles.Length);


			yield return null;
		}
		public void RefreshView() {

		}
		#region Move
		public void MoveLeft() { 
			for(var c = 0; c < GridColumnCount; c++) {
				var currentR = 0;
				for(var r = 1; r < GridRowCount; r++) {
					if(tiles[c * GridRowCount + currentR].CheckSameLevel(tiles[c * GridRowCount + r].Level)) {


					}
					else {

					}
						
					//tiles[c * GridRowCount + r]
				}
			}
		}
		public void MoveRight() { 

		}
		public void MoveUp() { 

		}
		public void MoveDown() { 

		}
		#endregion
	}
}