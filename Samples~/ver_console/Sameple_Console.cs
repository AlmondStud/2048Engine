using UnityEngine;

namespace Almond._2048 {
	public class Sameple_Console : MonoBehaviour {
		private GridController gridController;
		private int score;
		private void Start() {
			gridController = new GridController();
			score = 0;
			gridController.InitializeGrid(4, 4, true, (t) => score += t * 2);
		}
		private void Update() {
			if(gridController == null)
				return;
			if(gridController.GameEnd)
				return;

			if(Input.GetKeyDown(KeyCode.UpArrow)) {
				if(gridController.Move(MoveWay.Up)) {
					gridController.CreateNewTile();
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow)) {
				if(gridController.Move(MoveWay.Down)) {
					gridController.CreateNewTile();
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				if(gridController.Move(MoveWay.Left)) {
					gridController.CreateNewTile();
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow)) {
				if(gridController.Move(MoveWay.Right)) {
					gridController.CreateNewTile();
					gridController.CheckGameOver();
				}
			}
		}
	}
}