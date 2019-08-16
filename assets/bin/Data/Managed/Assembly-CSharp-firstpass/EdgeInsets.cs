using UnityEngine;

public class EdgeInsets
{
	public float top;

	public float bottom;

	public float left;

	public float right;

	public float width;

	public float height;

	public static EdgeInsets zero = new EdgeInsets(0f, 0f, 0f, 0f);

	public float ScreenWidth
	{
		get
		{
			if (width == 0f)
			{
				return Screen.get_width();
			}
			return width;
		}
	}

	public float ScreenHeight
	{
		get
		{
			if (height == 0f)
			{
				return Screen.get_height();
			}
			return height;
		}
	}

	public float SafeHeightMax
	{
		get
		{
			float num = Mathf.Max(top, bottom);
			return ScreenHeight - num * 2f;
		}
	}

	public float SafeHeight => ScreenHeight - top - bottom;

	public float SafeWidthMax
	{
		get
		{
			float num = Mathf.Max(left, right);
			return ScreenWidth - num * 2f;
		}
	}

	public float SafeWidth => ScreenWidth - left - right;

	public EdgeInsets(float top, float left, float bottom, float right, float width = 0f, float height = 0f)
	{
		Set(top, left, bottom, right, width, height);
	}

	public void Set(float top, float left, float bottom, float right, float width = 0f, float height = 0f)
	{
		this.top = top;
		this.left = left;
		this.bottom = bottom;
		this.right = right;
		this.width = width;
		this.height = height;
	}

	public override string ToString()
	{
		return $"[EdgeInsets: ({top},{left},{bottom},{right})]";
	}

	public bool IsZero()
	{
		if (top == 0f && bottom == 0f && left == 0f && right == 0f)
		{
			return true;
		}
		return false;
	}
}
