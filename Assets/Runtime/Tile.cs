using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Almond._2048 {
	public class Tile {
		public static float ScaleTime = 0.05f;
		private int level;
		public int Level => level;

		public void InitTile(int startLevel) {
			level = startLevel;
		}

		public bool CheckSameLevel(int otherLevel) => level == otherLevel;
		public void LevelUp() {
			level++;
		}
	}
}