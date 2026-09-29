// Decompiled with JetBrains decompiler
// Type: NGUIMath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Diagnostics;
using UnityEngine;

#nullable disable
public static class NGUIMath
{
  [DebuggerHidden]
  [DebuggerStepThrough]
  public static float Lerp(float from, float to, float factor)
  {
    return (float) ((double) from * (1.0 - (double) factor) + (double) to * (double) factor);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int ClampIndex(int val, int max)
  {
    if (val < 0)
      return 0;
    return val >= max ? max - 1 : val;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int RepeatIndex(int val, int max)
  {
    if (max < 1)
      return 0;
    while (val < 0)
      val += max;
    while (val >= max)
      val -= max;
    return val;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static float WrapAngle(float angle)
  {
    while ((double) angle > 180.0)
      angle -= 360f;
    while ((double) angle < -180.0)
      angle += 360f;
    return angle;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static float Wrap01(float val) => val - (float) Mathf.FloorToInt(val);

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int HexToDecimal(char ch)
  {
    switch (ch)
    {
      case '0':
        return 0;
      case '1':
        return 1;
      case '2':
        return 2;
      case '3':
        return 3;
      case '4':
        return 4;
      case '5':
        return 5;
      case '6':
        return 6;
      case '7':
        return 7;
      case '8':
        return 8;
      case '9':
        return 9;
      case 'A':
      case 'a':
        return 10;
      case 'B':
      case 'b':
        return 11;
      case 'C':
      case 'c':
        return 12;
      case 'D':
      case 'd':
        return 13;
      case 'E':
      case 'e':
        return 14;
      case 'F':
      case 'f':
        return 15;
      default:
        return 15;
    }
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static char DecimalToHexChar(int num)
  {
    if (num > 15)
      return 'F';
    return num < 10 ? (char) (48 /*0x30*/ + num) : (char) (65 + num - 10);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string DecimalToHex8(int num)
  {
    num &= (int) byte.MaxValue;
    return num.ToString("X2");
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string DecimalToHex24(int num)
  {
    num &= 16777215 /*0xFFFFFF*/;
    return num.ToString("X6");
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string DecimalToHex32(int num) => num.ToString("X8");

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int ColorToInt(Color c)
  {
    return 0 | Mathf.RoundToInt(c.r * (float) byte.MaxValue) << 24 | Mathf.RoundToInt(c.g * (float) byte.MaxValue) << 16 /*0x10*/ | Mathf.RoundToInt(c.b * (float) byte.MaxValue) << 8 | Mathf.RoundToInt(c.a * (float) byte.MaxValue);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static Color IntToColor(int val)
  {
    float num = 0.003921569f;
    Color black = Color.black;
    black.r = num * (float) (val >> 24 & (int) byte.MaxValue);
    black.g = num * (float) (val >> 16 /*0x10*/ & (int) byte.MaxValue);
    black.b = num * (float) (val >> 8 & (int) byte.MaxValue);
    black.a = num * (float) (val & (int) byte.MaxValue);
    return black;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string IntToBinary(int val, int bits)
  {
    string binary = "";
    for (int index = bits; index > 0; binary += ((val & 1 << --index) != 0 ? '1' : '0').ToString())
    {
      if (index == 8 || index == 16 /*0x10*/ || index == 24)
        binary += " ";
    }
    return binary;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static Color HexToColor(uint val) => NGUIMath.IntToColor((int) val);

  public static Rect ConvertToTexCoords(Rect rect, int width, int height)
  {
    Rect texCoords = rect;
    if ((double) width != 0.0 && (double) height != 0.0)
    {
      ((Rect) ref texCoords).xMin = ((Rect) ref rect).xMin / (float) width;
      ((Rect) ref texCoords).xMax = ((Rect) ref rect).xMax / (float) width;
      ((Rect) ref texCoords).yMin = (float) (1.0 - (double) ((Rect) ref rect).yMax / (double) height);
      ((Rect) ref texCoords).yMax = (float) (1.0 - (double) ((Rect) ref rect).yMin / (double) height);
    }
    return texCoords;
  }

  public static Rect ConvertToPixels(Rect rect, int width, int height, bool round)
  {
    Rect pixels = rect;
    if (round)
    {
      ((Rect) ref pixels).xMin = (float) Mathf.RoundToInt(((Rect) ref rect).xMin * (float) width);
      ((Rect) ref pixels).xMax = (float) Mathf.RoundToInt(((Rect) ref rect).xMax * (float) width);
      ((Rect) ref pixels).yMin = (float) Mathf.RoundToInt((1f - ((Rect) ref rect).yMax) * (float) height);
      ((Rect) ref pixels).yMax = (float) Mathf.RoundToInt((1f - ((Rect) ref rect).yMin) * (float) height);
    }
    else
    {
      ((Rect) ref pixels).xMin = ((Rect) ref rect).xMin * (float) width;
      ((Rect) ref pixels).xMax = ((Rect) ref rect).xMax * (float) width;
      ((Rect) ref pixels).yMin = (1f - ((Rect) ref rect).yMax) * (float) height;
      ((Rect) ref pixels).yMax = (1f - ((Rect) ref rect).yMin) * (float) height;
    }
    return pixels;
  }

  public static Rect MakePixelPerfect(Rect rect)
  {
    ((Rect) ref rect).xMin = (float) Mathf.RoundToInt(((Rect) ref rect).xMin);
    ((Rect) ref rect).yMin = (float) Mathf.RoundToInt(((Rect) ref rect).yMin);
    ((Rect) ref rect).xMax = (float) Mathf.RoundToInt(((Rect) ref rect).xMax);
    ((Rect) ref rect).yMax = (float) Mathf.RoundToInt(((Rect) ref rect).yMax);
    return rect;
  }

  public static Rect MakePixelPerfect(Rect rect, int width, int height)
  {
    rect = NGUIMath.ConvertToPixels(rect, width, height, true);
    ((Rect) ref rect).xMin = (float) Mathf.RoundToInt(((Rect) ref rect).xMin);
    ((Rect) ref rect).yMin = (float) Mathf.RoundToInt(((Rect) ref rect).yMin);
    ((Rect) ref rect).xMax = (float) Mathf.RoundToInt(((Rect) ref rect).xMax);
    ((Rect) ref rect).yMax = (float) Mathf.RoundToInt(((Rect) ref rect).yMax);
    return NGUIMath.ConvertToTexCoords(rect, width, height);
  }

  public static Vector2 ConstrainRect(
    Vector2 minRect,
    Vector2 maxRect,
    Vector2 minArea,
    Vector2 maxArea)
  {
    Vector2 zero = Vector2.zero;
    float num1 = maxRect.x - minRect.x;
    float num2 = maxRect.y - minRect.y;
    float num3 = maxArea.x - minArea.x;
    float num4 = maxArea.y - minArea.y;
    if ((double) num1 > (double) num3)
    {
      float num5 = num1 - num3;
      minArea.x -= num5;
      maxArea.x += num5;
    }
    if ((double) num2 > (double) num4)
    {
      float num6 = num2 - num4;
      minArea.y -= num6;
      maxArea.y += num6;
    }
    if ((double) minRect.x < (double) minArea.x)
      zero.x += minArea.x - minRect.x;
    if ((double) maxRect.x > (double) maxArea.x)
      zero.x -= maxRect.x - maxArea.x;
    if ((double) minRect.y < (double) minArea.y)
      zero.y += minArea.y - minRect.y;
    if ((double) maxRect.y > (double) maxArea.y)
      zero.y -= maxRect.y - maxArea.y;
    return zero;
  }

  public static Bounds CalculateAbsoluteWidgetBounds(Transform trans)
  {
    if (!Object.op_Inequality((Object) trans, (Object) null))
      return new Bounds(Vector3.zero, Vector3.zero);
    UIWidget[] componentsInChildren = ((Component) trans).GetComponentsInChildren<UIWidget>();
    if (componentsInChildren.Length == 0)
      return new Bounds(trans.position, Vector3.zero);
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(float.MaxValue, float.MaxValue, float.MaxValue);
    Vector3 vector3_2;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_2).\u002Ector(float.MinValue, float.MinValue, float.MinValue);
    int index1 = 0;
    for (int length = componentsInChildren.Length; index1 < length; ++index1)
    {
      UIWidget uiWidget = componentsInChildren[index1];
      if (((Behaviour) uiWidget).enabled)
      {
        Vector3[] worldCorners = uiWidget.worldCorners;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Vector3 vector3_3 = worldCorners[index2];
          if ((double) vector3_3.x > (double) vector3_2.x)
            vector3_2.x = vector3_3.x;
          if ((double) vector3_3.y > (double) vector3_2.y)
            vector3_2.y = vector3_3.y;
          if ((double) vector3_3.z > (double) vector3_2.z)
            vector3_2.z = vector3_3.z;
          if ((double) vector3_3.x < (double) vector3_1.x)
            vector3_1.x = vector3_3.x;
          if ((double) vector3_3.y < (double) vector3_1.y)
            vector3_1.y = vector3_3.y;
          if ((double) vector3_3.z < (double) vector3_1.z)
            vector3_1.z = vector3_3.z;
        }
      }
    }
    Bounds absoluteWidgetBounds;
    // ISSUE: explicit constructor call
    ((Bounds) ref absoluteWidgetBounds).\u002Ector(vector3_1, Vector3.zero);
    ((Bounds) ref absoluteWidgetBounds).Encapsulate(vector3_2);
    return absoluteWidgetBounds;
  }

  public static Bounds CalculateRelativeWidgetBounds(Transform trans)
  {
    return NGUIMath.CalculateRelativeWidgetBounds(trans, trans, false);
  }

  public static Bounds CalculateRelativeWidgetBounds(Transform trans, bool considerInactive)
  {
    return NGUIMath.CalculateRelativeWidgetBounds(trans, trans, considerInactive);
  }

  public static Bounds CalculateRelativeWidgetBounds(Transform relativeTo, Transform content)
  {
    return NGUIMath.CalculateRelativeWidgetBounds(relativeTo, content, false);
  }

  public static Bounds CalculateRelativeWidgetBounds(
    Transform relativeTo,
    Transform content,
    bool considerInactive,
    bool considerChildren = true)
  {
    if (Object.op_Inequality((Object) content, (Object) null) && Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      bool isSet = false;
      Matrix4x4 worldToLocalMatrix = relativeTo.worldToLocalMatrix;
      Vector3 vMin;
      // ISSUE: explicit constructor call
      ((Vector3) ref vMin).\u002Ector(float.MaxValue, float.MaxValue, float.MaxValue);
      Vector3 vMax;
      // ISSUE: explicit constructor call
      ((Vector3) ref vMax).\u002Ector(float.MinValue, float.MinValue, float.MinValue);
      NGUIMath.CalculateRelativeWidgetBounds(content, considerInactive, true, ref worldToLocalMatrix, ref vMin, ref vMax, ref isSet, considerChildren);
      if (isSet)
      {
        Bounds relativeWidgetBounds;
        // ISSUE: explicit constructor call
        ((Bounds) ref relativeWidgetBounds).\u002Ector(vMin, Vector3.zero);
        ((Bounds) ref relativeWidgetBounds).Encapsulate(vMax);
        return relativeWidgetBounds;
      }
    }
    return new Bounds(Vector3.zero, Vector3.zero);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  private static void CalculateRelativeWidgetBounds(
    Transform content,
    bool considerInactive,
    bool isRoot,
    ref Matrix4x4 toLocal,
    ref Vector3 vMin,
    ref Vector3 vMax,
    ref bool isSet,
    bool considerChildren)
  {
    if (Object.op_Equality((Object) content, (Object) null) || !considerInactive && !NGUITools.GetActive(((Component) content).gameObject))
      return;
    UIPanel component1 = isRoot ? (UIPanel) null : ((Component) content).GetComponent<UIPanel>();
    if (Object.op_Inequality((Object) component1, (Object) null) && !((Behaviour) component1).enabled)
      return;
    if (Object.op_Inequality((Object) component1, (Object) null) && component1.clipping != UIDrawCall.Clipping.None)
    {
      Vector3[] worldCorners = component1.worldCorners;
      for (int index = 0; index < 4; ++index)
      {
        Vector3 vector3 = ((Matrix4x4) ref toLocal).MultiplyPoint3x4(worldCorners[index]);
        if ((double) vector3.x > (double) vMax.x)
          vMax.x = vector3.x;
        if ((double) vector3.y > (double) vMax.y)
          vMax.y = vector3.y;
        if ((double) vector3.z > (double) vMax.z)
          vMax.z = vector3.z;
        if ((double) vector3.x < (double) vMin.x)
          vMin.x = vector3.x;
        if ((double) vector3.y < (double) vMin.y)
          vMin.y = vector3.y;
        if ((double) vector3.z < (double) vMin.z)
          vMin.z = vector3.z;
        isSet = true;
      }
    }
    else
    {
      UIWidget component2 = ((Component) content).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled)
      {
        Vector3[] worldCorners = component2.worldCorners;
        for (int index = 0; index < 4; ++index)
        {
          Vector3 vector3 = ((Matrix4x4) ref toLocal).MultiplyPoint3x4(worldCorners[index]);
          if ((double) vector3.x > (double) vMax.x)
            vMax.x = vector3.x;
          if ((double) vector3.y > (double) vMax.y)
            vMax.y = vector3.y;
          if ((double) vector3.z > (double) vMax.z)
            vMax.z = vector3.z;
          if ((double) vector3.x < (double) vMin.x)
            vMin.x = vector3.x;
          if ((double) vector3.y < (double) vMin.y)
            vMin.y = vector3.y;
          if ((double) vector3.z < (double) vMin.z)
            vMin.z = vector3.z;
          isSet = true;
        }
        if (!considerChildren)
          return;
      }
      int num = 0;
      for (int childCount = content.childCount; num < childCount; ++num)
        NGUIMath.CalculateRelativeWidgetBounds(content.GetChild(num), considerInactive, false, ref toLocal, ref vMin, ref vMax, ref isSet, true);
    }
  }

  public static Vector3 SpringDampen(ref Vector3 velocity, float strength, float deltaTime)
  {
    if ((double) deltaTime > 1.0)
      deltaTime = 1f;
    float num1 = (float) (1.0 - (double) strength * (1.0 / 1000.0));
    int num2 = Mathf.RoundToInt(deltaTime * 1000f);
    float num3 = Mathf.Pow(num1, (float) num2);
    Vector3 vector3 = Vector3.op_Multiply(velocity, (num3 - 1f) / Mathf.Log(num1));
    velocity = Vector3.op_Multiply(velocity, num3);
    return Vector3.op_Multiply(vector3, 0.06f);
  }

  public static Vector2 SpringDampen(ref Vector2 velocity, float strength, float deltaTime)
  {
    if ((double) deltaTime > 1.0)
      deltaTime = 1f;
    float num1 = (float) (1.0 - (double) strength * (1.0 / 1000.0));
    int num2 = Mathf.RoundToInt(deltaTime * 1000f);
    float num3 = Mathf.Pow(num1, (float) num2);
    Vector2 vector2 = Vector2.op_Multiply(velocity, (num3 - 1f) / Mathf.Log(num1));
    velocity = Vector2.op_Multiply(velocity, num3);
    return Vector2.op_Multiply(vector2, 0.06f);
  }

  public static float SpringLerp(float strength, float deltaTime)
  {
    if ((double) deltaTime > 1.0)
      deltaTime = 1f;
    int num1 = Mathf.RoundToInt(deltaTime * 1000f);
    deltaTime = 1f / 1000f * strength;
    float num2 = 0.0f;
    for (int index = 0; index < num1; ++index)
      num2 = Mathf.Lerp(num2, 1f, deltaTime);
    return num2;
  }

  public static float SpringLerp(float from, float to, float strength, float deltaTime)
  {
    if ((double) deltaTime > 1.0)
      deltaTime = 1f;
    int num = Mathf.RoundToInt(deltaTime * 1000f);
    deltaTime = 1f / 1000f * strength;
    for (int index = 0; index < num; ++index)
      from = Mathf.Lerp(from, to, deltaTime);
    return from;
  }

  public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
  {
    return Vector2.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
  }

  public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
  {
    return Vector3.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
  }

  public static Quaternion SpringLerp(
    Quaternion from,
    Quaternion to,
    float strength,
    float deltaTime)
  {
    return Quaternion.Slerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
  }

  public static float RotateTowards(float from, float to, float maxAngle)
  {
    float num = NGUIMath.WrapAngle(to - from);
    if ((double) Mathf.Abs(num) > (double) maxAngle)
      num = maxAngle * Mathf.Sign(num);
    return from + num;
  }

  private static float DistancePointToLineSegment(Vector2 point, Vector2 a, Vector2 b)
  {
    Vector2 vector2_1 = Vector2.op_Subtraction(b, a);
    float sqrMagnitude = ((Vector2) ref vector2_1).sqrMagnitude;
    if ((double) sqrMagnitude == 0.0)
    {
      Vector2 vector2_2 = Vector2.op_Subtraction(point, a);
      return ((Vector2) ref vector2_2).magnitude;
    }
    float num = Vector2.Dot(Vector2.op_Subtraction(point, a), Vector2.op_Subtraction(b, a)) / sqrMagnitude;
    if ((double) num < 0.0)
    {
      Vector2 vector2_3 = Vector2.op_Subtraction(point, a);
      return ((Vector2) ref vector2_3).magnitude;
    }
    if ((double) num > 1.0)
    {
      Vector2 vector2_4 = Vector2.op_Subtraction(point, b);
      return ((Vector2) ref vector2_4).magnitude;
    }
    Vector2 vector2_5 = Vector2.op_Addition(a, Vector2.op_Multiply(num, Vector2.op_Subtraction(b, a)));
    Vector2 vector2_6 = Vector2.op_Subtraction(point, vector2_5);
    return ((Vector2) ref vector2_6).magnitude;
  }

  public static float DistanceToRectangle(Vector2[] screenPoints, Vector2 mousePos)
  {
    bool flag = false;
    int val1 = 4;
    for (int val2 = 0; val2 < 5; ++val2)
    {
      Vector3 vector3_1 = Vector2.op_Implicit(screenPoints[NGUIMath.RepeatIndex(val2, 4)]);
      Vector3 vector3_2 = Vector2.op_Implicit(screenPoints[NGUIMath.RepeatIndex(val1, 4)]);
      if ((double) vector3_1.y > (double) mousePos.y != (double) vector3_2.y > (double) mousePos.y && (double) mousePos.x < ((double) vector3_2.x - (double) vector3_1.x) * ((double) mousePos.y - (double) vector3_1.y) / ((double) vector3_2.y - (double) vector3_1.y) + (double) vector3_1.x)
        flag = !flag;
      val1 = val2;
    }
    if (flag)
      return 0.0f;
    float rectangle = -1f;
    for (int index = 0; index < 4; ++index)
    {
      Vector3 vector3_3 = Vector2.op_Implicit(screenPoints[index]);
      Vector3 vector3_4 = Vector2.op_Implicit(screenPoints[NGUIMath.RepeatIndex(index + 1, 4)]);
      float lineSegment = NGUIMath.DistancePointToLineSegment(mousePos, Vector2.op_Implicit(vector3_3), Vector2.op_Implicit(vector3_4));
      if ((double) lineSegment < (double) rectangle || (double) rectangle < 0.0)
        rectangle = lineSegment;
    }
    return rectangle;
  }

  public static float DistanceToRectangle(Vector3[] worldPoints, Vector2 mousePos, Camera cam)
  {
    Vector2[] screenPoints = new Vector2[4];
    for (int index = 0; index < 4; ++index)
      screenPoints[index] = Vector2.op_Implicit(cam.WorldToScreenPoint(worldPoints[index]));
    return NGUIMath.DistanceToRectangle(screenPoints, mousePos);
  }

  public static Vector2 GetPivotOffset(UIWidget.Pivot pv)
  {
    Vector2 zero = Vector2.zero;
    switch (pv)
    {
      case UIWidget.Pivot.Top:
      case UIWidget.Pivot.Center:
      case UIWidget.Pivot.Bottom:
        zero.x = 0.5f;
        break;
      case UIWidget.Pivot.TopRight:
      case UIWidget.Pivot.Right:
      case UIWidget.Pivot.BottomRight:
        zero.x = 1f;
        break;
      default:
        zero.x = 0.0f;
        break;
    }
    switch (pv)
    {
      case UIWidget.Pivot.TopLeft:
      case UIWidget.Pivot.Top:
      case UIWidget.Pivot.TopRight:
        zero.y = 1f;
        break;
      case UIWidget.Pivot.Left:
      case UIWidget.Pivot.Center:
      case UIWidget.Pivot.Right:
        zero.y = 0.5f;
        break;
      default:
        zero.y = 0.0f;
        break;
    }
    return zero;
  }

  public static UIWidget.Pivot GetPivot(Vector2 offset)
  {
    if ((double) offset.x == 0.0)
    {
      if ((double) offset.y == 0.0)
        return UIWidget.Pivot.BottomLeft;
      return (double) offset.y == 1.0 ? UIWidget.Pivot.TopLeft : UIWidget.Pivot.Left;
    }
    if ((double) offset.x == 1.0)
    {
      if ((double) offset.y == 0.0)
        return UIWidget.Pivot.BottomRight;
      return (double) offset.y == 1.0 ? UIWidget.Pivot.TopRight : UIWidget.Pivot.Right;
    }
    if ((double) offset.y == 0.0)
      return UIWidget.Pivot.Bottom;
    return (double) offset.y == 1.0 ? UIWidget.Pivot.Top : UIWidget.Pivot.Center;
  }

  public static void MoveWidget(UIRect w, float x, float y) => NGUIMath.MoveRect(w, x, y);

  public static void MoveRect(UIRect rect, float x, float y)
  {
    int num1 = Mathf.FloorToInt(x + 0.5f);
    int num2 = Mathf.FloorToInt(y + 0.5f);
    Transform cachedTransform = rect.cachedTransform;
    cachedTransform.localPosition = Vector3.op_Addition(cachedTransform.localPosition, new Vector3((float) num1, (float) num2));
    int num3 = 0;
    if (Object.op_Implicit((Object) rect.leftAnchor.target))
    {
      ++num3;
      rect.leftAnchor.absolute += num1;
    }
    if (Object.op_Implicit((Object) rect.rightAnchor.target))
    {
      ++num3;
      rect.rightAnchor.absolute += num1;
    }
    if (Object.op_Implicit((Object) rect.bottomAnchor.target))
    {
      ++num3;
      rect.bottomAnchor.absolute += num2;
    }
    if (Object.op_Implicit((Object) rect.topAnchor.target))
    {
      ++num3;
      rect.topAnchor.absolute += num2;
    }
    if (num3 == 0)
      return;
    rect.UpdateAnchors();
  }

  public static void ResizeWidget(
    UIWidget w,
    UIWidget.Pivot pivot,
    float x,
    float y,
    int minWidth,
    int minHeight)
  {
    NGUIMath.ResizeWidget(w, pivot, x, y, 2, 2, 100000, 100000);
  }

  public static void ResizeWidget(
    UIWidget w,
    UIWidget.Pivot pivot,
    float x,
    float y,
    int minWidth,
    int minHeight,
    int maxWidth,
    int maxHeight)
  {
    if (pivot == UIWidget.Pivot.Center)
    {
      int num1 = Mathf.RoundToInt(x - (float) w.width);
      int num2 = Mathf.RoundToInt(y - (float) w.height);
      int num3 = num1 - (num1 & 1);
      int num4 = num2 - (num2 & 1);
      if ((num3 | num4) == 0)
        return;
      int right = num3 >> 1;
      int top = num4 >> 1;
      NGUIMath.AdjustWidget(w, (float) -right, (float) -top, (float) right, (float) top, minWidth, minHeight);
    }
    else
    {
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(x, y);
      Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Inverse(w.cachedTransform.localRotation), vector3_1);
      switch (pivot)
      {
        case UIWidget.Pivot.TopLeft:
          NGUIMath.AdjustWidget(w, vector3_2.x, 0.0f, 0.0f, vector3_2.y, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.Top:
          NGUIMath.AdjustWidget(w, 0.0f, 0.0f, 0.0f, vector3_2.y, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.TopRight:
          NGUIMath.AdjustWidget(w, 0.0f, 0.0f, vector3_2.x, vector3_2.y, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.Left:
          NGUIMath.AdjustWidget(w, vector3_2.x, 0.0f, 0.0f, 0.0f, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.Right:
          NGUIMath.AdjustWidget(w, 0.0f, 0.0f, vector3_2.x, 0.0f, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.BottomLeft:
          NGUIMath.AdjustWidget(w, vector3_2.x, vector3_2.y, 0.0f, 0.0f, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.Bottom:
          NGUIMath.AdjustWidget(w, 0.0f, vector3_2.y, 0.0f, 0.0f, minWidth, minHeight, maxWidth, maxHeight);
          break;
        case UIWidget.Pivot.BottomRight:
          NGUIMath.AdjustWidget(w, 0.0f, vector3_2.y, vector3_2.x, 0.0f, minWidth, minHeight, maxWidth, maxHeight);
          break;
      }
    }
  }

  public static void AdjustWidget(UIWidget w, float left, float bottom, float right, float top)
  {
    NGUIMath.AdjustWidget(w, left, bottom, right, top, 2, 2, 100000, 100000);
  }

  public static void AdjustWidget(
    UIWidget w,
    float left,
    float bottom,
    float right,
    float top,
    int minWidth,
    int minHeight)
  {
    NGUIMath.AdjustWidget(w, left, bottom, right, top, minWidth, minHeight, 100000, 100000);
  }

  public static void AdjustWidget(
    UIWidget w,
    float left,
    float bottom,
    float right,
    float top,
    int minWidth,
    int minHeight,
    int maxWidth,
    int maxHeight)
  {
    Vector2 pivotOffset = w.pivotOffset;
    Transform cachedTransform = w.cachedTransform;
    Quaternion localRotation = cachedTransform.localRotation;
    int num1 = Mathf.FloorToInt(left + 0.5f);
    int num2 = Mathf.FloorToInt(bottom + 0.5f);
    int num3 = Mathf.FloorToInt(right + 0.5f);
    int num4 = Mathf.FloorToInt(top + 0.5f);
    if ((double) pivotOffset.x == 0.5 && (num1 == 0 || num3 == 0))
    {
      num1 = num1 >> 1 << 1;
      num3 = num3 >> 1 << 1;
    }
    if ((double) pivotOffset.y == 0.5 && (num2 == 0 || num4 == 0))
    {
      num2 = num2 >> 1 << 1;
      num4 = num4 >> 1 << 1;
    }
    Vector3 vector3_1 = Quaternion.op_Multiply(localRotation, new Vector3((float) num1, (float) num4));
    Vector3 vector3_2 = Quaternion.op_Multiply(localRotation, new Vector3((float) num3, (float) num4));
    Vector3 vector3_3 = Quaternion.op_Multiply(localRotation, new Vector3((float) num1, (float) num2));
    Vector3 vector3_4 = Quaternion.op_Multiply(localRotation, new Vector3((float) num3, (float) num2));
    Vector3 vector3_5 = Quaternion.op_Multiply(localRotation, new Vector3((float) num1, 0.0f));
    Vector3 vector3_6 = Quaternion.op_Multiply(localRotation, new Vector3((float) num3, 0.0f));
    Vector3 vector3_7 = Quaternion.op_Multiply(localRotation, new Vector3(0.0f, (float) num4));
    Vector3 vector3_8 = Quaternion.op_Multiply(localRotation, new Vector3(0.0f, (float) num2));
    Vector3 zero1 = Vector3.zero;
    if ((double) pivotOffset.x == 0.0 && (double) pivotOffset.y == 1.0)
    {
      zero1.x = vector3_1.x;
      zero1.y = vector3_1.y;
    }
    else if ((double) pivotOffset.x == 1.0 && (double) pivotOffset.y == 0.0)
    {
      zero1.x = vector3_4.x;
      zero1.y = vector3_4.y;
    }
    else if ((double) pivotOffset.x == 0.0 && (double) pivotOffset.y == 0.0)
    {
      zero1.x = vector3_3.x;
      zero1.y = vector3_3.y;
    }
    else if ((double) pivotOffset.x == 1.0 && (double) pivotOffset.y == 1.0)
    {
      zero1.x = vector3_2.x;
      zero1.y = vector3_2.y;
    }
    else if ((double) pivotOffset.x == 0.0 && (double) pivotOffset.y == 0.5)
    {
      zero1.x = vector3_5.x + (float) (((double) vector3_7.x + (double) vector3_8.x) * 0.5);
      zero1.y = vector3_5.y + (float) (((double) vector3_7.y + (double) vector3_8.y) * 0.5);
    }
    else if ((double) pivotOffset.x == 1.0 && (double) pivotOffset.y == 0.5)
    {
      zero1.x = vector3_6.x + (float) (((double) vector3_7.x + (double) vector3_8.x) * 0.5);
      zero1.y = vector3_6.y + (float) (((double) vector3_7.y + (double) vector3_8.y) * 0.5);
    }
    else if ((double) pivotOffset.x == 0.5 && (double) pivotOffset.y == 1.0)
    {
      zero1.x = vector3_7.x + (float) (((double) vector3_5.x + (double) vector3_6.x) * 0.5);
      zero1.y = vector3_7.y + (float) (((double) vector3_5.y + (double) vector3_6.y) * 0.5);
    }
    else if ((double) pivotOffset.x == 0.5 && (double) pivotOffset.y == 0.0)
    {
      zero1.x = vector3_8.x + (float) (((double) vector3_5.x + (double) vector3_6.x) * 0.5);
      zero1.y = vector3_8.y + (float) (((double) vector3_5.y + (double) vector3_6.y) * 0.5);
    }
    else if ((double) pivotOffset.x == 0.5 && (double) pivotOffset.y == 0.5)
    {
      zero1.x = (float) (((double) vector3_5.x + (double) vector3_6.x + (double) vector3_7.x + (double) vector3_8.x) * 0.5);
      zero1.y = (float) (((double) vector3_7.y + (double) vector3_8.y + (double) vector3_5.y + (double) vector3_6.y) * 0.5);
    }
    minWidth = Mathf.Max(minWidth, w.minWidth);
    minHeight = Mathf.Max(minHeight, w.minHeight);
    int w1 = w.width + num3 - num1;
    int h = w.height + num4 - num2;
    Vector3 zero2 = Vector3.zero;
    int num5 = w1;
    if (w1 < minWidth)
      num5 = minWidth;
    else if (w1 > maxWidth)
      num5 = maxWidth;
    if (w1 != num5)
    {
      if (num1 != 0)
        zero2.x -= Mathf.Lerp((float) (num5 - w1), 0.0f, pivotOffset.x);
      else
        zero2.x += Mathf.Lerp(0.0f, (float) (num5 - w1), pivotOffset.x);
      w1 = num5;
    }
    int num6 = h;
    if (h < minHeight)
      num6 = minHeight;
    else if (h > maxHeight)
      num6 = maxHeight;
    if (h != num6)
    {
      if (num2 != 0)
        zero2.y -= Mathf.Lerp((float) (num6 - h), 0.0f, pivotOffset.y);
      else
        zero2.y += Mathf.Lerp(0.0f, (float) (num6 - h), pivotOffset.y);
      h = num6;
    }
    if ((double) pivotOffset.x == 0.5)
      w1 = w1 >> 1 << 1;
    if ((double) pivotOffset.y == 0.5)
      h = h >> 1 << 1;
    Vector3 vector3_9 = Vector3.op_Addition(Vector3.op_Addition(cachedTransform.localPosition, zero1), Quaternion.op_Multiply(localRotation, zero2));
    cachedTransform.localPosition = vector3_9;
    w.SetDimensions(w1, h);
    if (!w.isAnchored)
      return;
    Transform parent = cachedTransform.parent;
    float localPos1 = vector3_9.x - pivotOffset.x * (float) w1;
    float localPos2 = vector3_9.y - pivotOffset.y * (float) h;
    if (Object.op_Implicit((Object) w.leftAnchor.target))
      w.leftAnchor.SetHorizontal(parent, localPos1);
    if (Object.op_Implicit((Object) w.rightAnchor.target))
      w.rightAnchor.SetHorizontal(parent, localPos1 + (float) w1);
    if (Object.op_Implicit((Object) w.bottomAnchor.target))
      w.bottomAnchor.SetVertical(parent, localPos2);
    if (!Object.op_Implicit((Object) w.topAnchor.target))
      return;
    w.topAnchor.SetVertical(parent, localPos2 + (float) h);
  }

  public static int AdjustByDPI(float height)
  {
    float num1 = Screen.dpi;
    RuntimePlatform platform = Application.platform;
    if ((double) num1 == 0.0)
      num1 = platform == 11 || platform == 8 ? 160f : 96f;
    int num2 = Mathf.RoundToInt(height * (96f / num1));
    if ((num2 & 1) == 1)
      ++num2;
    return num2;
  }

  public static Vector2 ScreenToPixels(Vector2 pos, Transform relativeTo)
  {
    int layer = ((Component) relativeTo).gameObject.layer;
    Camera cameraForLayer = NGUITools.FindCameraForLayer(layer);
    if (Object.op_Equality((Object) cameraForLayer, (Object) null))
    {
      Debug.LogWarning((object) ("No camera found for layer " + (object) layer));
      return pos;
    }
    Vector3 worldPoint = cameraForLayer.ScreenToWorldPoint(Vector2.op_Implicit(pos));
    return Vector2.op_Implicit(relativeTo.InverseTransformPoint(worldPoint));
  }

  public static Vector2 ScreenToParentPixels(Vector2 pos, Transform relativeTo)
  {
    int layer = ((Component) relativeTo).gameObject.layer;
    if (Object.op_Inequality((Object) relativeTo.parent, (Object) null))
      relativeTo = relativeTo.parent;
    Camera cameraForLayer = NGUITools.FindCameraForLayer(layer);
    if (Object.op_Equality((Object) cameraForLayer, (Object) null))
    {
      Debug.LogWarning((object) ("No camera found for layer " + (object) layer));
      return pos;
    }
    Vector3 worldPoint = cameraForLayer.ScreenToWorldPoint(Vector2.op_Implicit(pos));
    return Vector2.op_Implicit(Object.op_Inequality((Object) relativeTo, (Object) null) ? relativeTo.InverseTransformPoint(worldPoint) : worldPoint);
  }

  public static Vector3 WorldToLocalPoint(
    Vector3 worldPos,
    Camera worldCam,
    Camera uiCam,
    Transform relativeTo)
  {
    worldPos = worldCam.WorldToViewportPoint(worldPos);
    worldPos = uiCam.ViewportToWorldPoint(worldPos);
    if (Object.op_Equality((Object) relativeTo, (Object) null))
      return worldPos;
    relativeTo = relativeTo.parent;
    return Object.op_Equality((Object) relativeTo, (Object) null) ? worldPos : relativeTo.InverseTransformPoint(worldPos);
  }

  public static void OverlayPosition(
    this Transform trans,
    Vector3 worldPos,
    Camera worldCam,
    Camera myCam)
  {
    worldPos = worldCam.WorldToViewportPoint(worldPos);
    worldPos = myCam.ViewportToWorldPoint(worldPos);
    Transform parent = trans.parent;
    trans.localPosition = Object.op_Inequality((Object) parent, (Object) null) ? parent.InverseTransformPoint(worldPos) : worldPos;
  }

  public static void OverlayPosition(this Transform trans, Vector3 worldPos, Camera worldCam)
  {
    Camera cameraForLayer = NGUITools.FindCameraForLayer(((Component) trans).gameObject.layer);
    if (!Object.op_Inequality((Object) cameraForLayer, (Object) null))
      return;
    trans.OverlayPosition(worldPos, worldCam, cameraForLayer);
  }

  public static void OverlayPosition(this Transform trans, Transform target)
  {
    Camera cameraForLayer1 = NGUITools.FindCameraForLayer(((Component) trans).gameObject.layer);
    Camera cameraForLayer2 = NGUITools.FindCameraForLayer(((Component) target).gameObject.layer);
    if (!Object.op_Inequality((Object) cameraForLayer1, (Object) null) || !Object.op_Inequality((Object) cameraForLayer2, (Object) null))
      return;
    trans.OverlayPosition(target.position, cameraForLayer2, cameraForLayer1);
  }
}
