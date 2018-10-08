using System.Diagnostics;
using UnityEngine;

public static class NGUIMath
{
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float Lerp(float from, float to, float factor)
	{
		return from * (1f - factor) + to * factor;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int ClampIndex(int val, int max)
	{
		return (val >= 0) ? ((val >= max) ? (max - 1) : val) : 0;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int RepeatIndex(int val, int max)
	{
		if (max < 1)
		{
			return 0;
		}
		while (val < 0)
		{
			val += max;
		}
		while (val >= max)
		{
			val -= max;
		}
		return val;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static float WrapAngle(float angle)
	{
		while (angle > 180f)
		{
			angle -= 360f;
		}
		while (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static float Wrap01(float val)
	{
		return val - (float)Mathf.FloorToInt(val);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
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
		{
			return 'F';
		}
		if (num < 10)
		{
			return (char)(48 + num);
		}
		return (char)(65 + num - 10);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string DecimalToHex8(int num)
	{
		num &= 0xFF;
		return num.ToString("X2");
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string DecimalToHex24(int num)
	{
		num &= 0xFFFFFF;
		return num.ToString("X6");
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string DecimalToHex32(int num)
	{
		return num.ToString("X8");
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int ColorToInt(Color c)
	{
		int num = 0;
		num |= Mathf.RoundToInt(c.r * 255f) << 24;
		num |= Mathf.RoundToInt(c.g * 255f) << 16;
		num |= Mathf.RoundToInt(c.b * 255f) << 8;
		return num | Mathf.RoundToInt(c.a * 255f);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color IntToColor(int val)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float num = 0.003921569f;
		Color black = Color.get_black();
		black.r = num * (float)((val >> 24) & 0xFF);
		black.g = num * (float)((val >> 16) & 0xFF);
		black.b = num * (float)((val >> 8) & 0xFF);
		black.a = num * (float)(val & 0xFF);
		return black;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string IntToBinary(int val, int bits)
	{
		string text = string.Empty;
		int num = bits;
		while (num > 0)
		{
			if (num == 8 || num == 16 || num == 24)
			{
				text += " ";
			}
			text += (((val & (1 << --num)) == 0) ? '0' : '1');
		}
		return text;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static Color HexToColor(uint val)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return IntToColor((int)val);
	}

	public static Rect ConvertToTexCoords(Rect rect, int width, int height)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Rect result = rect;
		if ((float)width != 0f && (float)height != 0f)
		{
			result.set_xMin(rect.get_xMin() / (float)width);
			result.set_xMax(rect.get_xMax() / (float)width);
			result.set_yMin(1f - rect.get_yMax() / (float)height);
			result.set_yMax(1f - rect.get_yMin() / (float)height);
		}
		return result;
	}

	public static Rect ConvertToPixels(Rect rect, int width, int height, bool round)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		Rect result = rect;
		if (round)
		{
			result.set_xMin((float)Mathf.RoundToInt(rect.get_xMin() * (float)width));
			result.set_xMax((float)Mathf.RoundToInt(rect.get_xMax() * (float)width));
			result.set_yMin((float)Mathf.RoundToInt((1f - rect.get_yMax()) * (float)height));
			result.set_yMax((float)Mathf.RoundToInt((1f - rect.get_yMin()) * (float)height));
		}
		else
		{
			result.set_xMin(rect.get_xMin() * (float)width);
			result.set_xMax(rect.get_xMax() * (float)width);
			result.set_yMin((1f - rect.get_yMax()) * (float)height);
			result.set_yMax((1f - rect.get_yMin()) * (float)height);
		}
		return result;
	}

	public static Rect MakePixelPerfect(Rect rect)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		rect.set_xMin((float)Mathf.RoundToInt(rect.get_xMin()));
		rect.set_yMin((float)Mathf.RoundToInt(rect.get_yMin()));
		rect.set_xMax((float)Mathf.RoundToInt(rect.get_xMax()));
		rect.set_yMax((float)Mathf.RoundToInt(rect.get_yMax()));
		return rect;
	}

	public static Rect MakePixelPerfect(Rect rect, int width, int height)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		rect = ConvertToPixels(rect, width, height, true);
		rect.set_xMin((float)Mathf.RoundToInt(rect.get_xMin()));
		rect.set_yMin((float)Mathf.RoundToInt(rect.get_yMin()));
		rect.set_xMax((float)Mathf.RoundToInt(rect.get_xMax()));
		rect.set_yMax((float)Mathf.RoundToInt(rect.get_yMax()));
		return ConvertToTexCoords(rect, width, height);
	}

	public static Vector2 ConstrainRect(Vector2 minRect, Vector2 maxRect, Vector2 minArea, Vector2 maxArea)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = Vector2.get_zero();
		float num = maxRect.x - minRect.x;
		float num2 = maxRect.y - minRect.y;
		float num3 = maxArea.x - minArea.x;
		float num4 = maxArea.y - minArea.y;
		if (num > num3)
		{
			float num5 = num - num3;
			minArea.x -= num5;
			maxArea.x += num5;
		}
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			minArea.y -= num6;
			maxArea.y += num6;
		}
		if (minRect.x < minArea.x)
		{
			zero.x += minArea.x - minRect.x;
		}
		if (maxRect.x > maxArea.x)
		{
			zero.x -= maxRect.x - maxArea.x;
		}
		if (minRect.y < minArea.y)
		{
			zero.y += minArea.y - minRect.y;
		}
		if (maxRect.y > maxArea.y)
		{
			zero.y -= maxRect.y - maxArea.y;
		}
		return zero;
	}

	public static Bounds CalculateAbsoluteWidgetBounds(Transform trans)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		if (trans != null)
		{
			UIWidget[] componentsInChildren = trans.GetComponentsInChildren<UIWidget>();
			if (componentsInChildren.Length == 0)
			{
				return new Bounds(trans.get_position(), Vector3.get_zero());
			}
			Vector3 val = default(Vector3);
			val._002Ector(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
			Vector3 val2 = default(Vector3);
			val2._002Ector(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				if (uIWidget.get_enabled())
				{
					Vector3[] worldCorners = uIWidget.worldCorners;
					for (int j = 0; j < 4; j++)
					{
						Vector3 val3 = worldCorners[j];
						if (val3.x > val2.x)
						{
							val2.x = val3.x;
						}
						if (val3.y > val2.y)
						{
							val2.y = val3.y;
						}
						if (val3.z > val2.z)
						{
							val2.z = val3.z;
						}
						if (val3.x < val.x)
						{
							val.x = val3.x;
						}
						if (val3.y < val.y)
						{
							val.y = val3.y;
						}
						if (val3.z < val.z)
						{
							val.z = val3.z;
						}
					}
				}
			}
			Bounds result = default(Bounds);
			result._002Ector(val, Vector3.get_zero());
			result.Encapsulate(val2);
			return result;
		}
		return new Bounds(Vector3.get_zero(), Vector3.get_zero());
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform trans)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return CalculateRelativeWidgetBounds(trans, trans, false, true);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform trans, bool considerInactive)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return CalculateRelativeWidgetBounds(trans, trans, considerInactive, true);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform relativeTo, Transform content)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return CalculateRelativeWidgetBounds(relativeTo, content, false, true);
	}

	public static Bounds CalculateRelativeWidgetBounds(Transform relativeTo, Transform content, bool considerInactive, bool considerChildren = true)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (content != null && relativeTo != null)
		{
			bool isSet = false;
			Matrix4x4 toLocal = relativeTo.get_worldToLocalMatrix();
			Vector3 vMin = default(Vector3);
			vMin._002Ector(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
			Vector3 vMax = default(Vector3);
			vMax._002Ector(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
			CalculateRelativeWidgetBounds(content, considerInactive, true, ref toLocal, ref vMin, ref vMax, ref isSet, considerChildren);
			if (isSet)
			{
				Bounds result = default(Bounds);
				result._002Ector(vMin, Vector3.get_zero());
				result.Encapsulate(vMax);
				return result;
			}
		}
		return new Bounds(Vector3.get_zero(), Vector3.get_zero());
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void CalculateRelativeWidgetBounds(Transform content, bool considerInactive, bool isRoot, ref Matrix4x4 toLocal, ref Vector3 vMin, ref Vector3 vMax, ref bool isSet, bool considerChildren)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		if (!(content == null) && (considerInactive || NGUITools.GetActive(content.get_gameObject())))
		{
			UIPanel uIPanel = (!isRoot) ? content.GetComponent<UIPanel>() : null;
			if (!(uIPanel != null) || uIPanel.get_enabled())
			{
				if (uIPanel != null && uIPanel.clipping != 0)
				{
					Vector3[] worldCorners = uIPanel.worldCorners;
					for (int i = 0; i < 4; i++)
					{
						Vector3 val = toLocal.MultiplyPoint3x4(worldCorners[i]);
						if (val.x > vMax.x)
						{
							vMax.x = val.x;
						}
						if (val.y > vMax.y)
						{
							vMax.y = val.y;
						}
						if (val.z > vMax.z)
						{
							vMax.z = val.z;
						}
						if (val.x < vMin.x)
						{
							vMin.x = val.x;
						}
						if (val.y < vMin.y)
						{
							vMin.y = val.y;
						}
						if (val.z < vMin.z)
						{
							vMin.z = val.z;
						}
						isSet = true;
					}
				}
				else
				{
					UIWidget component = content.GetComponent<UIWidget>();
					if (component != null && component.get_enabled())
					{
						Vector3[] worldCorners2 = component.worldCorners;
						for (int j = 0; j < 4; j++)
						{
							Vector3 val2 = toLocal.MultiplyPoint3x4(worldCorners2[j]);
							if (val2.x > vMax.x)
							{
								vMax.x = val2.x;
							}
							if (val2.y > vMax.y)
							{
								vMax.y = val2.y;
							}
							if (val2.z > vMax.z)
							{
								vMax.z = val2.z;
							}
							if (val2.x < vMin.x)
							{
								vMin.x = val2.x;
							}
							if (val2.y < vMin.y)
							{
								vMin.y = val2.y;
							}
							if (val2.z < vMin.z)
							{
								vMin.z = val2.z;
							}
							isSet = true;
						}
						if (!considerChildren)
						{
							return;
						}
					}
					int k = 0;
					for (int childCount = content.get_childCount(); k < childCount; k++)
					{
						CalculateRelativeWidgetBounds(content.GetChild(k), considerInactive, false, ref toLocal, ref vMin, ref vMax, ref isSet, true);
					}
				}
			}
		}
	}

	public static Vector3 SpringDampen(ref Vector3 velocity, float strength, float deltaTime)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (deltaTime > 1f)
		{
			deltaTime = 1f;
		}
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		float num3 = Mathf.Pow(num, (float)num2);
		Vector3 val = velocity * ((num3 - 1f) / Mathf.Log(num));
		velocity *= num3;
		return val * 0.06f;
	}

	public static Vector2 SpringDampen(ref Vector2 velocity, float strength, float deltaTime)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (deltaTime > 1f)
		{
			deltaTime = 1f;
		}
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		float num3 = Mathf.Pow(num, (float)num2);
		Vector2 val = velocity * ((num3 - 1f) / Mathf.Log(num));
		velocity *= num3;
		return val * 0.06f;
	}

	public static float SpringLerp(float strength, float deltaTime)
	{
		if (deltaTime > 1f)
		{
			deltaTime = 1f;
		}
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, 1f, deltaTime);
		}
		return num2;
	}

	public static float SpringLerp(float from, float to, float strength, float deltaTime)
	{
		if (deltaTime > 1f)
		{
			deltaTime = 1f;
		}
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		for (int i = 0; i < num; i++)
		{
			from = Mathf.Lerp(from, to, deltaTime);
		}
		return from;
	}

	public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return Vector3.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return Quaternion.Slerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static float RotateTowards(float from, float to, float maxAngle)
	{
		float num = WrapAngle(to - from);
		if (Mathf.Abs(num) > maxAngle)
		{
			num = maxAngle * Mathf.Sign(num);
		}
		return from + num;
	}

	private static float DistancePointToLineSegment(Vector2 point, Vector2 a, Vector2 b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = b - a;
		float sqrMagnitude = val.get_sqrMagnitude();
		if (sqrMagnitude == 0f)
		{
			Vector2 val2 = point - a;
			return val2.get_magnitude();
		}
		float num = Vector2.Dot(point - a, b - a) / sqrMagnitude;
		if (num < 0f)
		{
			Vector2 val3 = point - a;
			return val3.get_magnitude();
		}
		if (num > 1f)
		{
			Vector2 val4 = point - b;
			return val4.get_magnitude();
		}
		Vector2 val5 = a + num * (b - a);
		Vector2 val6 = point - val5;
		return val6.get_magnitude();
	}

	public static float DistanceToRectangle(Vector2[] screenPoints, Vector2 mousePos)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		int val = 4;
		for (int i = 0; i < 5; i++)
		{
			Vector3 val2 = Vector2.op_Implicit(screenPoints[RepeatIndex(i, 4)]);
			Vector3 val3 = Vector2.op_Implicit(screenPoints[RepeatIndex(val, 4)]);
			if (val2.y > mousePos.y != val3.y > mousePos.y && mousePos.x < (val3.x - val2.x) * (mousePos.y - val2.y) / (val3.y - val2.y) + val2.x)
			{
				flag = !flag;
			}
			val = i;
		}
		if (!flag)
		{
			float num = -1f;
			for (int j = 0; j < 4; j++)
			{
				Vector3 val4 = Vector2.op_Implicit(screenPoints[j]);
				Vector3 val5 = Vector2.op_Implicit(screenPoints[RepeatIndex(j + 1, 4)]);
				float num2 = DistancePointToLineSegment(mousePos, Vector2.op_Implicit(val4), Vector2.op_Implicit(val5));
				if (num2 < num || num < 0f)
				{
					num = num2;
				}
			}
			return num;
		}
		return 0f;
	}

	public static float DistanceToRectangle(Vector3[] worldPoints, Vector2 mousePos, Camera cam)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Vector2[] array = (Vector2[])new Vector2[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = Vector2.op_Implicit(cam.WorldToScreenPoint(worldPoints[i]));
		}
		return DistanceToRectangle(array, mousePos);
	}

	public static Vector2 GetPivotOffset(UIWidget.Pivot pv)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = Vector2.get_zero();
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
			zero.x = 0f;
			break;
		}
		switch (pv)
		{
		case UIWidget.Pivot.Left:
		case UIWidget.Pivot.Center:
		case UIWidget.Pivot.Right:
			zero.y = 0.5f;
			break;
		case UIWidget.Pivot.TopLeft:
		case UIWidget.Pivot.Top:
		case UIWidget.Pivot.TopRight:
			zero.y = 1f;
			break;
		default:
			zero.y = 0f;
			break;
		}
		return zero;
	}

	public static UIWidget.Pivot GetPivot(Vector2 offset)
	{
		if (offset.x == 0f)
		{
			if (offset.y == 0f)
			{
				return UIWidget.Pivot.BottomLeft;
			}
			if (offset.y == 1f)
			{
				return UIWidget.Pivot.TopLeft;
			}
			return UIWidget.Pivot.Left;
		}
		if (offset.x == 1f)
		{
			if (offset.y == 0f)
			{
				return UIWidget.Pivot.BottomRight;
			}
			if (offset.y == 1f)
			{
				return UIWidget.Pivot.TopRight;
			}
			return UIWidget.Pivot.Right;
		}
		if (offset.y == 0f)
		{
			return UIWidget.Pivot.Bottom;
		}
		if (offset.y == 1f)
		{
			return UIWidget.Pivot.Top;
		}
		return UIWidget.Pivot.Center;
	}

	public static void MoveWidget(UIRect w, float x, float y)
	{
		MoveRect(w, x, y);
	}

	public static void MoveRect(UIRect rect, float x, float y)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int num = Mathf.FloorToInt(x + 0.5f);
		int num2 = Mathf.FloorToInt(y + 0.5f);
		Transform cachedTransform = rect.cachedTransform;
		Transform obj = cachedTransform;
		obj.set_localPosition(obj.get_localPosition() + new Vector3((float)num, (float)num2));
		int num3 = 0;
		if (Object.op_Implicit(rect.leftAnchor.target))
		{
			num3++;
			rect.leftAnchor.absolute += num;
		}
		if (Object.op_Implicit(rect.rightAnchor.target))
		{
			num3++;
			rect.rightAnchor.absolute += num;
		}
		if (Object.op_Implicit(rect.bottomAnchor.target))
		{
			num3++;
			rect.bottomAnchor.absolute += num2;
		}
		if (Object.op_Implicit(rect.topAnchor.target))
		{
			num3++;
			rect.topAnchor.absolute += num2;
		}
		if (num3 != 0)
		{
			rect.UpdateAnchors();
		}
	}

	public static void ResizeWidget(UIWidget w, UIWidget.Pivot pivot, float x, float y, int minWidth, int minHeight)
	{
		ResizeWidget(w, pivot, x, y, 2, 2, 100000, 100000);
	}

	public static void ResizeWidget(UIWidget w, UIWidget.Pivot pivot, float x, float y, int minWidth, int minHeight, int maxWidth, int maxHeight)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (pivot == UIWidget.Pivot.Center)
		{
			int num = Mathf.RoundToInt(x - (float)w.width);
			int num2 = Mathf.RoundToInt(y - (float)w.height);
			num -= (num & 1);
			num2 -= (num2 & 1);
			if ((num | num2) != 0)
			{
				num >>= 1;
				num2 >>= 1;
				AdjustWidget(w, (float)(-num), (float)(-num2), (float)num, (float)num2, minWidth, minHeight);
			}
		}
		else
		{
			Vector3 val = default(Vector3);
			val._002Ector(x, y);
			val = Quaternion.Inverse(w.cachedTransform.get_localRotation()) * val;
			switch (pivot)
			{
			case UIWidget.Pivot.Center:
				break;
			case UIWidget.Pivot.BottomLeft:
				AdjustWidget(w, val.x, val.y, 0f, 0f, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.Left:
				AdjustWidget(w, val.x, 0f, 0f, 0f, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.TopLeft:
				AdjustWidget(w, val.x, 0f, 0f, val.y, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.Top:
				AdjustWidget(w, 0f, 0f, 0f, val.y, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.TopRight:
				AdjustWidget(w, 0f, 0f, val.x, val.y, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.Right:
				AdjustWidget(w, 0f, 0f, val.x, 0f, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.BottomRight:
				AdjustWidget(w, 0f, val.y, val.x, 0f, minWidth, minHeight, maxWidth, maxHeight);
				break;
			case UIWidget.Pivot.Bottom:
				AdjustWidget(w, 0f, val.y, 0f, 0f, minWidth, minHeight, maxWidth, maxHeight);
				break;
			}
		}
	}

	public static void AdjustWidget(UIWidget w, float left, float bottom, float right, float top)
	{
		AdjustWidget(w, left, bottom, right, top, 2, 2, 100000, 100000);
	}

	public static void AdjustWidget(UIWidget w, float left, float bottom, float right, float top, int minWidth, int minHeight)
	{
		AdjustWidget(w, left, bottom, right, top, minWidth, minHeight, 100000, 100000);
	}

	public static void AdjustWidget(UIWidget w, float left, float bottom, float right, float top, int minWidth, int minHeight, int maxWidth, int maxHeight)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Expected O, but got Unknown
		Vector2 pivotOffset = w.pivotOffset;
		Transform cachedTransform = w.cachedTransform;
		Quaternion localRotation = cachedTransform.get_localRotation();
		int num = Mathf.FloorToInt(left + 0.5f);
		int num2 = Mathf.FloorToInt(bottom + 0.5f);
		int num3 = Mathf.FloorToInt(right + 0.5f);
		int num4 = Mathf.FloorToInt(top + 0.5f);
		if (pivotOffset.x == 0.5f && (num == 0 || num3 == 0))
		{
			num = num >> 1 << 1;
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f && (num2 == 0 || num4 == 0))
		{
			num2 = num2 >> 1 << 1;
			num4 = num4 >> 1 << 1;
		}
		Vector3 val = localRotation * new Vector3((float)num, (float)num4);
		Vector3 val2 = localRotation * new Vector3((float)num3, (float)num4);
		Vector3 val3 = localRotation * new Vector3((float)num, (float)num2);
		Vector3 val4 = localRotation * new Vector3((float)num3, (float)num2);
		Vector3 val5 = localRotation * new Vector3((float)num, 0f);
		Vector3 val6 = localRotation * new Vector3((float)num3, 0f);
		Vector3 val7 = localRotation * new Vector3(0f, (float)num4);
		Vector3 val8 = localRotation * new Vector3(0f, (float)num2);
		Vector3 zero = Vector3.get_zero();
		if (pivotOffset.x == 0f && pivotOffset.y == 1f)
		{
			zero.x = val.x;
			zero.y = val.y;
		}
		else if (pivotOffset.x == 1f && pivotOffset.y == 0f)
		{
			zero.x = val4.x;
			zero.y = val4.y;
		}
		else if (pivotOffset.x == 0f && pivotOffset.y == 0f)
		{
			zero.x = val3.x;
			zero.y = val3.y;
		}
		else if (pivotOffset.x == 1f && pivotOffset.y == 1f)
		{
			zero.x = val2.x;
			zero.y = val2.y;
		}
		else if (pivotOffset.x == 0f && pivotOffset.y == 0.5f)
		{
			zero.x = val5.x + (val7.x + val8.x) * 0.5f;
			zero.y = val5.y + (val7.y + val8.y) * 0.5f;
		}
		else if (pivotOffset.x == 1f && pivotOffset.y == 0.5f)
		{
			zero.x = val6.x + (val7.x + val8.x) * 0.5f;
			zero.y = val6.y + (val7.y + val8.y) * 0.5f;
		}
		else if (pivotOffset.x == 0.5f && pivotOffset.y == 1f)
		{
			zero.x = val7.x + (val5.x + val6.x) * 0.5f;
			zero.y = val7.y + (val5.y + val6.y) * 0.5f;
		}
		else if (pivotOffset.x == 0.5f && pivotOffset.y == 0f)
		{
			zero.x = val8.x + (val5.x + val6.x) * 0.5f;
			zero.y = val8.y + (val5.y + val6.y) * 0.5f;
		}
		else if (pivotOffset.x == 0.5f && pivotOffset.y == 0.5f)
		{
			zero.x = (val5.x + val6.x + val7.x + val8.x) * 0.5f;
			zero.y = (val7.y + val8.y + val5.y + val6.y) * 0.5f;
		}
		minWidth = Mathf.Max(minWidth, w.minWidth);
		minHeight = Mathf.Max(minHeight, w.minHeight);
		int num5 = w.width + num3 - num;
		int num6 = w.height + num4 - num2;
		Vector3 zero2 = Vector3.get_zero();
		int num7 = num5;
		if (num5 < minWidth)
		{
			num7 = minWidth;
		}
		else if (num5 > maxWidth)
		{
			num7 = maxWidth;
		}
		if (num5 != num7)
		{
			if (num != 0)
			{
				zero2.x -= Mathf.Lerp((float)(num7 - num5), 0f, pivotOffset.x);
			}
			else
			{
				zero2.x += Mathf.Lerp(0f, (float)(num7 - num5), pivotOffset.x);
			}
			num5 = num7;
		}
		int num8 = num6;
		if (num6 < minHeight)
		{
			num8 = minHeight;
		}
		else if (num6 > maxHeight)
		{
			num8 = maxHeight;
		}
		if (num6 != num8)
		{
			if (num2 != 0)
			{
				zero2.y -= Mathf.Lerp((float)(num8 - num6), 0f, pivotOffset.y);
			}
			else
			{
				zero2.y += Mathf.Lerp(0f, (float)(num8 - num6), pivotOffset.y);
			}
			num6 = num8;
		}
		if (pivotOffset.x == 0.5f)
		{
			num5 = num5 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num6 = num6 >> 1 << 1;
		}
		Vector3 localPosition = cachedTransform.get_localPosition() + zero + localRotation * zero2;
		cachedTransform.set_localPosition(localPosition);
		w.SetDimensions(num5, num6);
		if (w.isAnchored)
		{
			cachedTransform = cachedTransform.get_parent();
			float num9 = localPosition.x - pivotOffset.x * (float)num5;
			float num10 = localPosition.y - pivotOffset.y * (float)num6;
			if (Object.op_Implicit(w.leftAnchor.target))
			{
				w.leftAnchor.SetHorizontal(cachedTransform, num9);
			}
			if (Object.op_Implicit(w.rightAnchor.target))
			{
				w.rightAnchor.SetHorizontal(cachedTransform, num9 + (float)num5);
			}
			if (Object.op_Implicit(w.bottomAnchor.target))
			{
				w.bottomAnchor.SetVertical(cachedTransform, num10);
			}
			if (Object.op_Implicit(w.topAnchor.target))
			{
				w.topAnchor.SetVertical(cachedTransform, num10 + (float)num6);
			}
		}
	}

	public static int AdjustByDPI(float height)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Invalid comparison between Unknown and I4
		float num = Screen.get_dpi();
		RuntimePlatform platform = Application.get_platform();
		if (num == 0f)
		{
			num = (((int)platform != 11 && (int)platform != 8) ? 96f : 160f);
		}
		int num2 = Mathf.RoundToInt(height * (96f / num));
		if ((num2 & 1) == 1)
		{
			num2++;
		}
		return num2;
	}

	public static Vector2 ScreenToPixels(Vector2 pos, Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		int layer = relativeTo.get_gameObject().get_layer();
		Camera val = NGUITools.FindCameraForLayer(layer);
		if (val == null)
		{
			Debug.LogWarning((object)("No camera found for layer " + layer));
			return pos;
		}
		Vector3 val2 = val.ScreenToWorldPoint(Vector2.op_Implicit(pos));
		return Vector2.op_Implicit(relativeTo.InverseTransformPoint(val2));
	}

	public static Vector2 ScreenToParentPixels(Vector2 pos, Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		int layer = relativeTo.get_gameObject().get_layer();
		if (relativeTo.get_parent() != null)
		{
			relativeTo = relativeTo.get_parent();
		}
		Camera val = NGUITools.FindCameraForLayer(layer);
		if (val == null)
		{
			Debug.LogWarning((object)("No camera found for layer " + layer));
			return pos;
		}
		Vector3 val2 = val.ScreenToWorldPoint(Vector2.op_Implicit(pos));
		return Vector2.op_Implicit((!(relativeTo != null)) ? val2 : relativeTo.InverseTransformPoint(val2));
	}

	public static Vector3 WorldToLocalPoint(Vector3 worldPos, Camera worldCam, Camera uiCam, Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		worldPos = worldCam.WorldToViewportPoint(worldPos);
		worldPos = uiCam.ViewportToWorldPoint(worldPos);
		if (relativeTo == null)
		{
			return worldPos;
		}
		relativeTo = relativeTo.get_parent();
		if (relativeTo == null)
		{
			return worldPos;
		}
		return relativeTo.InverseTransformPoint(worldPos);
	}

	public static void OverlayPosition(this Transform trans, Vector3 worldPos, Camera worldCam, Camera myCam)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		worldPos = worldCam.WorldToViewportPoint(worldPos);
		worldPos = myCam.ViewportToWorldPoint(worldPos);
		Transform val = trans.get_parent();
		trans.set_localPosition((!(val != null)) ? worldPos : val.InverseTransformPoint(worldPos));
	}

	public static void OverlayPosition(this Transform trans, Vector3 worldPos, Camera worldCam)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Camera val = NGUITools.FindCameraForLayer(trans.get_gameObject().get_layer());
		if (val != null)
		{
			trans.OverlayPosition(worldPos, worldCam, val);
		}
	}

	public static void OverlayPosition(this Transform trans, Transform target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Camera val = NGUITools.FindCameraForLayer(trans.get_gameObject().get_layer());
		Camera val2 = NGUITools.FindCameraForLayer(target.get_gameObject().get_layer());
		if (val != null && val2 != null)
		{
			trans.OverlayPosition(target.get_position(), val2, val);
		}
	}
}
