using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Almond._2048 {
	public class TileView : MonoBehaviour {
		[SerializeField] private RectTransform rect;
		[SerializeField] private Image tileBG;
		[SerializeField] private Text levelView;
		[SerializeField] private int level;

		public void InitTileView(Vector2 size) {
			rect ??= GetComponent<RectTransform>();
			rect.sizeDelta = size;
		}
		public virtual void RefreshTile(int _level) {
			if(levelView != null) {
				levelView.text = _level.ToString();
			}
		}
	}
}