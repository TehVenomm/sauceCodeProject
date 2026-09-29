// Decompiled with JetBrains decompiler
// Type: Drawing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Drawing
{
  public static Texture2D lineTex;

  public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB)
  {
    Drawing.DrawLine(cam, pointA, pointB, GUI.contentColor, 1f);
  }

  public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, Color color)
  {
    Drawing.DrawLine(cam, pointA, pointB, color, 1f);
  }

  public static void DrawLine(Camera cam, Vector3 pointA, Vector3 pointB, float width)
  {
    Drawing.DrawLine(cam, pointA, pointB, GUI.contentColor, width);
  }

  public static void DrawLine(
    Camera cam,
    Vector3 pointA,
    Vector3 pointB,
    Color color,
    float width)
  {
    Vector2 zero1 = Vector2.zero;
    zero1.x = cam.WorldToScreenPoint(pointA).x;
    zero1.y = cam.WorldToScreenPoint(pointA).y;
    Vector2 zero2 = Vector2.zero;
    zero2.x = cam.WorldToScreenPoint(pointB).x;
    zero2.y = cam.WorldToScreenPoint(pointB).y;
    Drawing.DrawLine(zero1, zero2, color, width);
  }

  public static void DrawLine(Rect rect) => Drawing.DrawLine(rect, GUI.contentColor, 1f);

  public static void DrawLine(Rect rect, Color color) => Drawing.DrawLine(rect, color, 1f);

  public static void DrawLine(Rect rect, float width)
  {
    Drawing.DrawLine(rect, GUI.contentColor, width);
  }

  public static void DrawLine(Rect rect, Color color, float width)
  {
    Drawing.DrawLine(new Vector2(((Rect) ref rect).x, ((Rect) ref rect).y), new Vector2(((Rect) ref rect).x + ((Rect) ref rect).width, ((Rect) ref rect).y + ((Rect) ref rect).height), color, width);
  }

  public static void DrawLine(Vector2 pointA, Vector2 pointB)
  {
    Drawing.DrawLine(pointA, pointB, GUI.contentColor, 1f);
  }

  public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
  {
    Drawing.DrawLine(pointA, pointB, color, 1f);
  }

  public static void DrawLine(Vector2 pointA, Vector2 pointB, float width)
  {
    Drawing.DrawLine(pointA, pointB, GUI.contentColor, width);
  }

  public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
  {
    pointA.x = (float) (int) pointA.x;
    pointA.y = (float) (int) pointA.y;
    pointB.x = (float) (int) pointB.x;
    pointB.y = (float) (int) pointB.y;
    if (!Object.op_Implicit((Object) Drawing.lineTex))
      Drawing.lineTex = new Texture2D(1, 1);
    Color color1 = GUI.color;
    GUI.color = color;
    Matrix4x4 matrix = GUI.matrix;
    double num = (double) Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180.0 / 3.1415927410125732;
    Vector2 vector2_1 = Vector2.op_Subtraction(pointA, pointB);
    float magnitude = ((Vector2) ref vector2_1).magnitude;
    Vector2 vector2_2 = pointA;
    GUIUtility.RotateAroundPivot((float) num, vector2_2);
    GUI.DrawTexture(new Rect(pointA.x, pointA.y, magnitude, width), (Texture) Drawing.lineTex);
    GUI.matrix = matrix;
    GUI.color = color1;
  }
}
