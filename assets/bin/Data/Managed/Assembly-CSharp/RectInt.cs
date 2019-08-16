using UnityEngine;

public struct RectInt
{
	public int top;

	public int bottom;

	public int left;

	public int right;

	public void Set(int left, int right, int bottom, int top)
	{
		this.left = left;
		this.right = right;
		this.bottom = bottom;
		this.top = top;
	}

	public void Setf(float left, float right, float bottom, float top)
	{
		this.left = Mathf.CeilToInt(left);
		this.right = Mathf.CeilToInt(right);
		this.bottom = Mathf.CeilToInt(bottom);
		this.top = Mathf.CeilToInt(top);
	}

	public void Scale(float x, float y)
	{
		top = Mathf.CeilToInt((float)top * y);
		bottom = Mathf.CeilToInt((float)bottom * y);
		left = Mathf.CeilToInt((float)left * x);
		right = Mathf.CeilToInt((float)right * x);
	}
}
