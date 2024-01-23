using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Almond._2048 {
	public class GridView : MonoBehaviour {
		[SerializeField] private Vector2 tileSize;
		[SerializeField] private int gridRowCount;
		[SerializeField] private int gridColumnCount;
		[SerializeField] private TileView tileViewTemplate;
		[SerializeField] private Text scoreView;
		private List<TileView> tileViews;
		public void InitView(int column, int row) {
			gridColumnCount = column;
			gridRowCount = row;

			CreateTileViews();
			SetTilesPosition();
		}
		private void CreateTileViews() {
			if(tileViewTemplate == null) {
				tileViewTemplate = FindAnyObjectByType<TileView>();
			}
			tileViews ??= new List<TileView>();
			var count = gridColumnCount * gridRowCount;
			if(tileViews.Count < count) {
				var gap = count - tileViews.Count;
				for(int i = 0; i < gap; i++) {
					var newTile = Instantiate(tileViewTemplate);
					newTile.InitTileView(tileSize);
					newTile.transform.SetParent(transform);
					tileViews.Add(newTile);
				}
			}
			else if(tileViews.Count > count) {
				var gap = tileViews.Count - count;
				for(int i = 0; i < gap; i++) {
					Destroy(tileViews[i].gameObject);
					tileViews[i] = null;
				}
				tileViews.RemoveAll((a) => a == null);
			}
			tileViewTemplate.gameObject.SetActive(false);
		}
		private void SetTilesPosition() {
			var startPosition = transform.position;
			startPosition.y -= (gridColumnCount / 2) * tileSize.y;
			if(gridColumnCount % 2 == 0)
				startPosition.y += tileSize.y * 0.5f;
			startPosition.x -= (gridRowCount / 2) * tileSize.x;
			if(gridRowCount % 2 == 0)
				startPosition.x += tileSize.x * 0.5f;
			
			var pos = startPosition;
			for(var c = 0; c < gridColumnCount; c++) {			
				for(var r = 0; r < gridRowCount; r++) {
					var tile = tileViews[c * gridRowCount + r];
					tile.transform.position = pos;
					pos.x += tileSize.x;
				}
				pos.x = startPosition.x;
				pos.y += tileSize.y;
			}
		}
		public void UpdateScore(int score) {
			scoreView.text = score.ToString();
		}
		public void RefreshGrid(int[] tiles) {
			if(tileViews.Count != tiles.Length) {
				Debug.LogError($"Count not match :: view = {tileViews.Count} // tiles = {tiles.Length}");
				return;
			}

			for(var i = 0; i < tileViews.Count; i++) {
				tileViews[i].RefreshTile(tiles[i]);
			}
		}
		protected virtual void OnDrawGizmos() {
			if(gridRowCount == 0 || gridColumnCount == 0)
				return;

			var startPosition = transform.position;
			startPosition.y -= (gridColumnCount / 2) * tileSize.y;
			if(gridColumnCount % 2 == 0) {
				startPosition.y += tileSize.y * 0.5f;
			}
			else {
				//startPosition.y
			}
			startPosition.x -= (gridRowCount / 2) * tileSize.x;
			if(gridRowCount % 2 == 0) {
				startPosition.x -= tileSize.x * 0.5f;
			}
			else {

			}

			var pos = startPosition;
			for(int i = 0; i < gridRowCount; i++) {
				for(int j = 0; j < gridColumnCount; j++) {
					pos.x += tileSize.x;
					Gizmos.DrawWireCube(pos, new Vector3(tileSize.x, tileSize.y, 0));
				}
				pos.x = startPosition.x;
				pos.y += tileSize.y;
			}
		}
	}
}