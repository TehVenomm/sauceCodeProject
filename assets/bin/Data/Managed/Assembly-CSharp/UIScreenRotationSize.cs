using System;
using UnityEngine;

[Serializable]
public class UIScreenRotationSize : UIScreenRotationHandler
{
	[Serializable]
	private struct Point
	{
		public int x;

		public int y;
	}

	[SerializeField]
	private UIWidget target;

	[SerializeField]
	private Point portrait;

	[SerializeField]
	private Point landscape;

	protected override void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			target.width = portrait.x;
			target.height = portrait.y;
		}
		else
		{
			target.width = landscape.x;
			target.height = landscape.y;
		}
	}
}
