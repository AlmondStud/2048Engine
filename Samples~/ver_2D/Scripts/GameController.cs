using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Almond._2048 {
	public class GameController : MonoBehaviour {
		[SerializeField][Range(2,6)] private int gridRowCount;
		[SerializeField][Range(2,6)] private int gridColumnCount;
		[SerializeField] private bool showLog;
		[SerializeField] private GridView gridView;
		private GridController gridController;
		private int gameScore;
		void Start() {
			InitGame();
		}

		private void InitGame() {
			gameScore = 0;
			gridController ??= new GridController();
			gridController.InitializeGrid(gridColumnCount, gridRowCount, showLog, UpdateScore);
			if(gridView == null) {
				gridView = FindObjectOfType<GridView>();
			}
			gridView.InitView(gridColumnCount, gridRowCount);
			gridView.RefreshGrid(gridController.Tiles);
			gridView.UpdateScore(gameScore);

			void UpdateScore(int score) {
				gameScore += score * 2;
				gridView.UpdateScore(gameScore);
			}
		}
		void Update() {
			if(gridController == null)
				return;
			if(gridController.GameEnd)
				return;

			if(Input.GetKeyDown(KeyCode.UpArrow)) {
				if(gridController.Move(MoveWay.Up)) {
					gridController.CreateNewTile();
					gridView.RefreshGrid(gridController.Tiles);
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow)) {
				if(gridController.Move(MoveWay.Down)) {
					gridController.CreateNewTile();
					gridView.RefreshGrid(gridController.Tiles);
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				if(gridController.Move(MoveWay.Left)) {
					gridController.CreateNewTile();
					gridView.RefreshGrid(gridController.Tiles);
					gridController.CheckGameOver();
				}
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow)) {
				if(gridController.Move(MoveWay.Right)) {
					gridController.CreateNewTile();
					gridView.RefreshGrid(gridController.Tiles);
					gridController.CheckGameOver();
				}
			}
		}
	}
}