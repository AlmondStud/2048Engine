using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Almond._2048 {
	public class GridController {
		public static float MoveAnimTime = 0.1f;

		private System.Action<int> onTileMerged;
		private int gridRowCount = 4;
		private int gridColumnCount = 4;

		private bool writeLog;
		private bool gameEnd;
		public bool GameEnd => gameEnd;
		private int[] tiles;
		public int[] Tiles => tiles;

		private StringBuilder stringBuilder;
		public void InitializeGrid(int column, int row, bool log, System.Action<int> onMerged) {
			onTileMerged = onMerged;
			gameEnd = false;
			gridRowCount = row;
			gridColumnCount = column;
			tiles = new int[gridRowCount * gridColumnCount];
			writeLog = log;
			CreateNewTile(1);
			if(writeLog) {
				stringBuilder = new StringBuilder();
				WriteTiles(false);
			}
		}
		public void WriteTiles(bool move) {
			stringBuilder.Clear();
			for(var c = gridColumnCount - 1; c >= 0; c--) {
				for(var r = 0; r < gridRowCount; r++) {
					if(GetTile(c, r) == 0) {
						stringBuilder.Append(0);
					}
					else {
						stringBuilder.Append(GetTile(c, r));
					}
				}
				stringBuilder.Append("\n");
			}
			if(move)
				Debug.LogWarning(stringBuilder.ToString());
			else
				Debug.Log(stringBuilder.ToString());
		}
		public void CreateNewTile(int level = 0) {
			if(level <= 0) {
				level = Random.Range(1, 3);
			}
			var empty = new List<int>();
			for(var i = 0; i < tiles.Length; i++) {
				if(tiles[i] == 0)
					empty.Add(i);
			}
			if(empty.Count == 0) {
				return;
			}
			tiles[empty[Random.Range(0, empty.Count)]] = level;
			if(writeLog) {
				WriteTiles(false);
			}
		}
		private int GetTile(int c, int r) {
			if(c < 0 || r < 0)
				return 0;
			if(c >= gridColumnCount)
				return 0;
			if(r >= gridRowCount)
				return 0;

			return tiles[c * gridRowCount + r];
		}
		private int GetTile(Vector2Int index) {
			return GetTile(index.y, index.x);
		}
		#region Move Func
		/// <summary>
		/// Move tiles
		/// </summary>
		/// <param name="moveWay"></param>
		public bool Move(MoveWay moveWay) {
			if(GameEnd)
				return false;

			switch(moveWay) {
				case MoveWay.Up:
				return MoveUp();
				case MoveWay.Down:
				return MoveDown();
				case MoveWay.Left:
				return MoveLeft();
				case MoveWay.Right:
				return MoveRight();
			}
			if(writeLog) {
				WriteTiles(true);
			}
			return true;
		}
		private bool MoveLeft() {
			var isMoved = false;
			for(var c = 0; c < gridColumnCount; c++) {
				var currentR = 0;
				while(currentR < gridRowCount) {
					var current = GetTile(c, currentR);
					var next = GetNextIndex(currentR, c, MoveWay.Left);
					if(next < 0) {
						break;
					}

					if(current == 0) {
						MoveTile(c, currentR, c, next);
						isMoved = true;
						continue;
					}
					if(CheckAndMerge(c, currentR, c, next))
						isMoved = true;
					currentR++;
				}
			}
			return isMoved;
		}
		private bool MoveRight() {
			var isMoved = false;
			for(var c = 0; c < gridColumnCount; c++) {
				var currentR = gridRowCount - 1;
				while(currentR >= 0) {
					var current = GetTile(c, currentR);
					var next = GetNextIndex(currentR, c, MoveWay.Right);
					if(next < 0) {
						break;
					}

					if(current == 0) {
						MoveTile(c, currentR, c, next);
						isMoved = true;
						continue;
					}
					if(CheckAndMerge(c, currentR, c, next))
						isMoved = true;
					currentR--;
				}
			}
			return isMoved;
		}
		private bool MoveUp() {
			var isMoved = false;
			for(var r = 0; r < gridRowCount; r++) {
				var currentC = gridColumnCount - 1;
				while(currentC >= 0) {
					var current = GetTile(currentC, r);
					var next = GetNextIndex(currentC, r, MoveWay.Up);
					if(next < 0) {
						break;
					}

					if(current == 0) {
						MoveTile(currentC, r, next, r);
						isMoved = true;
						continue;
					}
					if(CheckAndMerge(currentC, r, next, r))
						isMoved = true;
					currentC--;
				}
			}
			return isMoved;
		}
		private bool MoveDown() {
			var isMoved = false;
			for(var r = 0; r < gridRowCount; r++) {
				var currentC = 0;
				while(currentC < gridColumnCount) {
					var current = GetTile(currentC, r);
					var next = GetNextIndex(currentC, r, MoveWay.Down);
					if(next < 0) {
						break;
					}

					if(current == 0) {
						MoveTile(currentC, r, next, r);
						isMoved = true;
						continue;
					}
					if(CheckAndMerge(currentC, r, next, r))
						isMoved = true;
					currentC++;
				}
			}
			return isMoved;
		}
		private void MoveTile(int currentC, int currentR, int nextC, int nextR) {
			tiles[currentC * gridRowCount + currentR] = tiles[nextC * gridRowCount + nextR];
			tiles[nextC * gridRowCount + nextR] = 0;
		}
		private void MergeTile(int currentC, int currentR, int nextC, int nextR) {

			onTileMerged?.Invoke(tiles[currentC * gridRowCount + currentR]);
			tiles[currentC * gridRowCount + currentR]++;
			tiles[nextC * gridRowCount + nextR] = 0;
		}
		private int GetNextIndex(int current, int sub, MoveWay moveWay) {
			switch(moveWay) {
				case MoveWay.Up: {
					for(int i = current - 1; i >= 0; i--) {
						if(GetTile(i, sub) != 0)
							return i;
					}
					break;
				}
				case MoveWay.Down: {
					for(int i = current + 1; i < gridColumnCount; i++) {
						if(GetTile(i, sub) != 0)
							return i;
					}
					break;
				}
				case MoveWay.Left: {
					for(int i = current + 1; i < gridRowCount; i++) {
						if(GetTile(sub, i) != 0)
							return i;
					}
					break;
				}
				case MoveWay.Right: {
					for(int i = current - 1; i >= 0; i--) {
						if(GetTile(sub, i) != 0)
							return i;
					}
					break;
				}
			}
			return -1;
		}
		#endregion
		private bool CheckAndMerge(int currentC, int currentR, int nextC, int nextR) {
			if(tiles[currentC * gridRowCount + currentR] == tiles[nextC * gridRowCount + nextR]) {
				MergeTile(currentC, currentR, nextC, nextR);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Check game over :: Check can't move
		/// </summary>
		/// <returns></returns>
		public bool CheckGameOver() {
			if(GameEnd)
				return true;

			for(int c = 0; c < gridColumnCount; c++) {
				for(int r = 0; r < gridRowCount; r++) {
					var tile = GetTile(c, r);
					if(tile == 0) {
						return false;
					}

					var index = new Vector2Int(r, c);
					var up = GetTile(index + Vector2Int.up);
					if(up != 0 && tile == up) {
						return false;
					}
					var down = GetTile(index + Vector2Int.down);
					if(down != 0 && tile == down) {
						return false;
					}
					var left = GetTile(index + Vector2Int.left);
					if(left != 0 && tile == left) {
						return false;
					}
					var right = GetTile(index + Vector2Int.right);
					if(right != 0 && tile == right) {
						return false;
					}
				}
			}
			Debug.LogWarning("End");
			return true;
		}
	}
}