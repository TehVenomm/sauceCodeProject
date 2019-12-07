using System;
using UnityEngine;

public class Drawing
{
	public static Texture2D lineTex;

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB)
	{
		DrawLine(cam, pointA, pointB, GUI.contentColor, 1f);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, Color color)
	{
		DrawLine(cam, pointA, pointB, color, 1f);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, float width)
	{
		DrawLine(cam, pointA, pointB, GUI.contentColor, width);
	}

	public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, Color color, float width)
	{
		Vector2 zero = Vector2.zero;
		zero.x = cam.WorldToScreenPoint(pointA).x;
		zero.y = cam.WorldToScreenPoint(pointA).y;
		Vector2 zero2 = Vector2.zero;
		zero2.x = cam.WorldToScreenPoint(pointB).x;
		zero2.y = cam.WorldToScreenPoint(pointB).y;
		DrawLine(zero, zero2, color, width);
	}

	public static void DrawLine(Rect rect)
	{
		DrawLine(rect, GUI.contentColor, 1f);
	}

	public static void DrawLine(Rect rect, Color color)
	{
		DrawLine(rect, color, 1f);
	}

	public static void DrawLine(Rect rect, float width)
	{
		DrawLine(rect, GUI.contentColor, width);
	}

	public static void DrawLine(Rect rect, Color color, float width)
	{
		DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB)
	{
		DrawLine(pointA, pointB, GUI.contentColor, 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
	{
		DrawLine(pointA, pointB, color, 1f);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, float width)
	{
		DrawLine(pointA, pointB, GUI.contentColor, width);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		pointA.x = (int)pointA.x;
		pointA.y = (int)pointA.y;
		pointB.x = (int)pointB.x;
		pointB.y = (int)pointB.y;
		if (!lineTex)
		{
			lineTex = new Texture2D(1, 1);
		}
		Color color2 = GUI.color;
		GUI.color = color;
		Matrix4x4 matrix = GUI.matrix;
		float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180f / (float)Math.PI;
		float magnitude = (pointA - pointB).magnitude;
		GUIUtility.RotateAroundPivot(angle, pointA);
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, magnitude, width), lineTex);
		GUI.matrix = matrix;
		GUI.color = color2;
	}
}
