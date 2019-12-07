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

		public static Point zero
		{
			get
			{
				Point result = default(Point);
				result.x = 0;
				result.y = 0;
				return result;
			}
		}
	}

	[SerializeField]
	private UIWidget target;

	[SerializeField]
	private Point portrait = Point.zero;

	[SerializeField]
	private Point landscape = Point.zero;

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
