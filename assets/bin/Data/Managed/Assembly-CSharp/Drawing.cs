using UnityEngine;

public class Drawing
{
	public static Texture2D lineTex;

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(cam, pointA, pointB, GUI.get_contentColor(), 1f);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, Color color)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(cam, pointA, pointB, color, 1f);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, float width)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(cam, pointA, pointB, GUI.get_contentColor(), width);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, Color color, float width)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = Vector2.get_zero();
		Vector3 val = cam.WorldToScreenPoint(pointA);
		zero.x = val.x;
		Vector3 val2 = cam.WorldToScreenPoint(pointA);
		zero.y = val2.y;
		Vector2 zero2 = Vector2.get_zero();
		Vector3 val3 = cam.WorldToScreenPoint(pointB);
		zero2.x = val3.x;
		Vector3 val4 = cam.WorldToScreenPoint(pointB);
		zero2.y = val4.y;
		DrawLine(zero, zero2, color, width);
	}

	public static void DrawLine(Rect rect)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(rect, GUI.get_contentColor(), 1f);
	}

	public static void DrawLine(Rect rect, Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(rect, color, 1f);
	}

	public static void DrawLine(Rect rect, float width)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(rect, GUI.get_contentColor(), width);
	}

	public static void DrawLine(Rect rect, Color color, float width)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(new Vector2(rect.get_x(), rect.get_y()), new Vector2(rect.get_x() + rect.get_width(), rect.get_y() + rect.get_height()), color, width);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(pointA, pointB, GUI.get_contentColor(), 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(pointA, pointB, color, 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DrawLine(pointA, pointB, GUI.get_contentColor(), width);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		pointA.x = (float)(int)pointA.x;
		pointA.y = (float)(int)pointA.y;
		pointB.x = (float)(int)pointB.x;
		pointB.y = (float)(int)pointB.y;
		if (!Object.op_Implicit(lineTex))
		{
			lineTex = new Texture2D(1, 1);
		}
		Color color2 = GUI.get_color();
		GUI.set_color(color);
		Matrix4x4 matrix = GUI.get_matrix();
		float num = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180f / 3.14159274f;
		Vector2 val = pointA - pointB;
		float magnitude = val.get_magnitude();
		GUIUtility.RotateAroundPivot(num, pointA);
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, magnitude, width), lineTex);
		GUI.set_matrix(matrix);
		GUI.set_color(color2);
	}
}
