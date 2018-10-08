using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class NGUIText
{
	public enum Alignment
	{
		Automatic,
		Left,
		Center,
		Right,
		Justified
	}

	public enum SymbolStyle
	{
		None,
		Normal,
		Colored
	}

	public class GlyphInfo
	{
		public Vector2 v0;

		public Vector2 v1;

		public Vector2 u0;

		public Vector2 u1;

		public Vector2 u2;

		public Vector2 u3;

		public float advance;

		public int channel;
	}

	public static UIFont bitmapFont;

	public static Font dynamicFont;

	public static GlyphInfo glyph = new GlyphInfo();

	public static int fontSize = 16;

	public static float fontScale = 1f;

	public static float pixelDensity = 1f;

	public static FontStyle fontStyle = 0;

	public static Alignment alignment = Alignment.Left;

	public static Color tint = Color.get_white();

	public static int rectWidth = 1000000;

	public static int rectHeight = 1000000;

	public static int regionWidth = 1000000;

	public static int regionHeight = 1000000;

	public static int maxLines = 0;

	public static bool gradient = false;

	public static Color gradientBottom = Color.get_white();

	public static Color gradientTop = Color.get_white();

	public static bool encoding = false;

	public static float spacingX = 0f;

	public static float spacingY = 0f;

	public static bool premultiply = false;

	public static SymbolStyle symbolStyle;

	public static int finalSize = 0;

	public static float finalSpacingX = 0f;

	public static float finalLineHeight = 0f;

	public static float baseline = 0f;

	public static bool useSymbols = false;

	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	private static BetterList<Color> mColors = new BetterList<Color>();

	private static float mAlpha = 1f;

	private static CharacterInfo mTempChar;

	private static BetterList<float> mSizes = new BetterList<float>();

	private static Color32 s_c0;

	private static Color32 s_c1;

	private static float[] mBoldOffset = new float[8]
	{
		-0.25f,
		0f,
		0.25f,
		0f,
		0f,
		-0.25f,
		0f,
		0.25f
	};

	public static void Update()
	{
		Update(true);
	}

	public static void Update(bool request)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		finalSize = Mathf.RoundToInt((float)fontSize / pixelDensity);
		finalSpacingX = spacingX * fontScale;
		finalLineHeight = ((float)fontSize + spacingY) * fontScale;
		useSymbols = (bitmapFont != null && bitmapFont.hasSymbols && encoding && symbolStyle != SymbolStyle.None);
		if (dynamicFont != null && request)
		{
			dynamicFont.RequestCharactersInTexture(")_-", finalSize, fontStyle);
			if (!dynamicFont.GetCharacterInfo(')', ref mTempChar, finalSize, fontStyle) || (float)mTempChar.get_maxY() == 0f)
			{
				dynamicFont.RequestCharactersInTexture("A", finalSize, fontStyle);
				if (!dynamicFont.GetCharacterInfo('A', ref mTempChar, finalSize, fontStyle))
				{
					baseline = 0f;
					return;
				}
			}
			float num = (float)mTempChar.get_maxY();
			float num2 = (float)mTempChar.get_minY();
			baseline = Mathf.Round(num + ((float)finalSize - num + num2) * 0.5f);
		}
	}

	public static void Prepare(string text)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (dynamicFont != null)
		{
			dynamicFont.RequestCharactersInTexture(text, finalSize, fontStyle);
		}
	}

	public static BMSymbol GetSymbol(string text, int index, int textLength)
	{
		return (!(bitmapFont != null)) ? null : bitmapFont.MatchSymbol(text, index, textLength);
	}

	public static float GetGlyphWidth(int ch, int prev)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (bitmapFont != null)
		{
			bool flag = false;
			if (ch == 8201)
			{
				flag = true;
				ch = 32;
			}
			BMGlyph bMGlyph = bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				int num = bMGlyph.advance;
				if (flag)
				{
					num >>= 1;
				}
				return fontScale * (float)((prev == 0) ? bMGlyph.advance : (num + bMGlyph.GetKerning(prev)));
			}
		}
		else if (dynamicFont != null && dynamicFont.GetCharacterInfo((char)ch, ref mTempChar, finalSize, fontStyle))
		{
			return (float)mTempChar.get_advance() * fontScale * pixelDensity;
		}
		return 0f;
	}

	public static GlyphInfo GetGlyph(int ch, int prev)
	{
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		if (bitmapFont != null)
		{
			bool flag = false;
			if (ch == 8201)
			{
				flag = true;
				ch = 32;
			}
			BMGlyph bMGlyph = bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				int num = (prev != 0) ? bMGlyph.GetKerning(prev) : 0;
				glyph.v0.x = (float)((prev == 0) ? bMGlyph.offsetX : (bMGlyph.offsetX + num));
				glyph.v1.y = (float)(-bMGlyph.offsetY);
				glyph.v1.x = glyph.v0.x + (float)bMGlyph.width;
				glyph.v0.y = glyph.v1.y - (float)bMGlyph.height;
				glyph.u0.x = (float)bMGlyph.x;
				glyph.u0.y = (float)(bMGlyph.y + bMGlyph.height);
				glyph.u2.x = (float)(bMGlyph.x + bMGlyph.width);
				glyph.u2.y = (float)bMGlyph.y;
				glyph.u1.x = glyph.u0.x;
				glyph.u1.y = glyph.u2.y;
				glyph.u3.x = glyph.u2.x;
				glyph.u3.y = glyph.u0.y;
				int num2 = bMGlyph.advance;
				if (flag)
				{
					num2 >>= 1;
				}
				glyph.advance = (float)(num2 + num);
				glyph.channel = bMGlyph.channel;
				if (fontScale != 1f)
				{
					GlyphInfo glyphInfo = glyph;
					glyphInfo.v0 *= fontScale;
					GlyphInfo glyphInfo2 = glyph;
					glyphInfo2.v1 *= fontScale;
					glyph.advance *= fontScale;
				}
				return glyph;
			}
		}
		else if (dynamicFont != null && dynamicFont.GetCharacterInfo((char)ch, ref mTempChar, finalSize, fontStyle))
		{
			glyph.v0.x = (float)mTempChar.get_minX();
			glyph.v1.x = (float)mTempChar.get_maxX();
			glyph.v0.y = (float)mTempChar.get_maxY() - baseline;
			glyph.v1.y = (float)mTempChar.get_minY() - baseline;
			glyph.u0 = mTempChar.get_uvTopLeft();
			glyph.u1 = mTempChar.get_uvBottomLeft();
			glyph.u2 = mTempChar.get_uvBottomRight();
			glyph.u3 = mTempChar.get_uvTopRight();
			glyph.advance = (float)mTempChar.get_advance();
			glyph.channel = 0;
			glyph.v0.x = Mathf.Round(glyph.v0.x);
			glyph.v0.y = Mathf.Round(glyph.v0.y);
			glyph.v1.x = Mathf.Round(glyph.v1.x);
			glyph.v1.y = Mathf.Round(glyph.v1.y);
			float num3 = fontScale * pixelDensity;
			if (num3 != 1f)
			{
				GlyphInfo glyphInfo3 = glyph;
				glyphInfo3.v0 *= num3;
				GlyphInfo glyphInfo4 = glyph;
				glyphInfo4.v1 *= num3;
				glyph.advance *= num3;
			}
			return glyph;
		}
		return null;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float ParseAlpha(string text, int index)
	{
		int num = (NGUIMath.HexToDecimal(text[index + 1]) << 4) | NGUIMath.HexToDecimal(text[index + 2]);
		return Mathf.Clamp01((float)num / 255f);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor(string text, int offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ParseColor24(text, offset);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor24(string text, int offset)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		int num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor32(string text, int offset)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		int num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
		int num4 = (NGUIMath.HexToDecimal(text[offset + 6]) << 4) | NGUIMath.HexToDecimal(text[offset + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string EncodeColor(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return EncodeColor24(c);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor(string text, Color c)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return "[c][" + EncodeColor24(c) + "]" + text + "[-][/c]";
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string EncodeAlpha(float a)
	{
		int num = Mathf.Clamp(Mathf.RoundToInt(a * 255f), 0, 255);
		return NGUIMath.DecimalToHex8(num);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static string EncodeColor24(Color c)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		int num = 0xFFFFFF & (NGUIMath.ColorToInt(c) >> 8);
		return NGUIMath.DecimalToHex24(num);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor32(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		int num = NGUIMath.ColorToInt(c);
		return NGUIMath.DecimalToHex32(num);
	}

	public static bool ParseSymbol(string text, ref int index)
	{
		int sub = 1;
		bool bold = false;
		bool italic = false;
		bool underline = false;
		bool strike = false;
		bool ignoreColor = false;
		return ParseSymbol(text, ref index, null, false, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor);
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static bool IsHex(char ch)
	{
		return (ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F');
	}

	public static bool ParseSymbol(string text, ref int index, BetterList<Color> colors, bool premultiply, ref int sub, ref bool bold, ref bool italic, ref bool underline, ref bool strike, ref bool ignoreColor)
	{
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		int length = text.Length;
		if (index + 3 > length || text[index] != '[')
		{
			return false;
		}
		if (text[index + 2] == ']')
		{
			if (text[index + 1] == '-')
			{
				if (colors != null && colors.size > 1)
				{
					colors.RemoveAt(colors.size - 1);
				}
				index += 3;
				return true;
			}
			string text2 = text.Substring(index, 3);
			switch (text2)
			{
			case "[b]":
				bold = true;
				index += 3;
				return true;
			case "[i]":
				italic = true;
				index += 3;
				return true;
			case "[u]":
				underline = true;
				index += 3;
				return true;
			case "[s]":
				strike = true;
				index += 3;
				return true;
			case "[c]":
				ignoreColor = true;
				index += 3;
				return true;
			}
		}
		if (index + 4 > length)
		{
			return false;
		}
		if (text[index + 3] == ']')
		{
			string text3 = text.Substring(index, 4);
			switch (text3)
			{
			case "[/b]":
				bold = false;
				index += 4;
				return true;
			case "[/i]":
				italic = false;
				index += 4;
				return true;
			case "[/u]":
				underline = false;
				index += 4;
				return true;
			case "[/s]":
				strike = false;
				index += 4;
				return true;
			case "[/c]":
				ignoreColor = false;
				index += 4;
				return true;
			}
			char ch = text[index + 1];
			char ch2 = text[index + 2];
			if (IsHex(ch) && IsHex(ch2))
			{
				int num = (NGUIMath.HexToDecimal(ch) << 4) | NGUIMath.HexToDecimal(ch2);
				mAlpha = (float)num / 255f;
				index += 4;
				return true;
			}
		}
		if (index + 5 > length)
		{
			return false;
		}
		if (text[index + 4] == ']')
		{
			string text4 = text.Substring(index, 5);
			switch (text4)
			{
			case "[sub]":
				sub = 1;
				index += 5;
				return true;
			case "[sup]":
				sub = 2;
				index += 5;
				return true;
			}
		}
		if (index + 6 > length)
		{
			return false;
		}
		if (text[index + 5] == ']')
		{
			string text5 = text.Substring(index, 6);
			switch (text5)
			{
			case "[/sub]":
				sub = 0;
				index += 6;
				return true;
			case "[/sup]":
				sub = 0;
				index += 6;
				return true;
			case "[/url]":
				index += 6;
				return true;
			}
		}
		if (text[index + 1] == 'u' && text[index + 2] == 'r' && text[index + 3] == 'l' && text[index + 4] == '=')
		{
			int num2 = text.IndexOf(']', index + 4);
			if (num2 != -1)
			{
				index = num2 + 1;
				return true;
			}
			index = text.Length;
			return true;
		}
		if (index + 8 > length)
		{
			return false;
		}
		if (text[index + 7] == ']')
		{
			Color val = ParseColor24(text, index + 1);
			if (EncodeColor24(val) != text.Substring(index + 1, 6).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				Color val2 = colors[colors.size - 1];
				val.a = val2.a;
				if (premultiply && val.a != 1f)
				{
					val = Color.Lerp(mInvisible, val, val.a);
				}
				colors.Add(val);
			}
			index += 8;
			return true;
		}
		if (index + 10 > length)
		{
			return false;
		}
		if (text[index + 9] == ']')
		{
			Color val3 = ParseColor32(text, index + 1);
			if (EncodeColor32(val3) != text.Substring(index + 1, 8).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				if (premultiply && val3.a != 1f)
				{
					val3 = Color.Lerp(mInvisible, val3, val3.a);
				}
				colors.Add(val3);
			}
			index += 10;
			return true;
		}
		return false;
	}

	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int num = 0;
			int length = text.Length;
			while (num < length)
			{
				char c = text[num];
				if (c == '[')
				{
					int sub = 0;
					bool bold = false;
					bool italic = false;
					bool underline = false;
					bool strike = false;
					bool ignoreColor = false;
					int index = num;
					if (ParseSymbol(text, ref index, null, false, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
					{
						text = text.Remove(num, index - num);
						length = text.Length;
						continue;
					}
				}
				num++;
			}
		}
		return text;
	}

	public static void Align(BetterList<Vector3> verts, int indexOffset, float printedWidth, int elements = 4)
	{
		switch (alignment)
		{
		case Alignment.Right:
		{
			float num23 = (float)rectWidth - printedWidth;
			if (!(num23 < 0f))
			{
				for (int j = indexOffset; j < verts.size; j++)
				{
					verts.buffer[j].x += num23;
				}
			}
			break;
		}
		case Alignment.Center:
		{
			float num20 = ((float)rectWidth - printedWidth) * 0.5f;
			if (!(num20 < 0f))
			{
				int num21 = Mathf.RoundToInt((float)rectWidth - printedWidth);
				int num22 = Mathf.RoundToInt((float)rectWidth);
				bool flag = (num21 & 1) == 1;
				bool flag2 = (num22 & 1) == 1;
				if ((flag && !flag2) || (!flag && flag2))
				{
					num20 += 0.5f * fontScale;
				}
				for (int i = indexOffset; i < verts.size; i++)
				{
					verts.buffer[i].x += num20;
				}
			}
			break;
		}
		case Alignment.Justified:
			if (!(printedWidth < (float)rectWidth * 0.65f))
			{
				float num = ((float)rectWidth - printedWidth) * 0.5f;
				if (!(num < 1f))
				{
					int num2 = (verts.size - indexOffset) / elements;
					if (num2 >= 1)
					{
						float num3 = 1f / (float)(num2 - 1);
						float num4 = (float)rectWidth / printedWidth;
						int num5 = indexOffset + elements;
						int num6 = 1;
						while (num5 < verts.size)
						{
							float x = verts.buffer[num5].x;
							float x2 = verts.buffer[num5 + elements / 2].x;
							float num7 = x2 - x;
							float num8 = x * num4;
							float num9 = num8 + num7;
							float num10 = x2 * num4;
							float num11 = num10 - num7;
							float num12 = (float)num6 * num3;
							x2 = Mathf.Lerp(num9, num10, num12);
							x = Mathf.Lerp(num8, num11, num12);
							x = Mathf.Round(x);
							x2 = Mathf.Round(x2);
							switch (elements)
							{
							case 4:
								verts.buffer[num5++].x = x;
								verts.buffer[num5++].x = x;
								verts.buffer[num5++].x = x2;
								verts.buffer[num5++].x = x2;
								break;
							case 2:
								verts.buffer[num5++].x = x;
								verts.buffer[num5++].x = x2;
								break;
							case 1:
								verts.buffer[num5++].x = x;
								break;
							}
							num6++;
						}
					}
				}
			}
			break;
		}
	}

	public static int GetExactCharacterIndex(BetterList<Vector3> verts, BetterList<int> indices, Vector2 pos)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < indices.size; i++)
		{
			int num = i << 1;
			int i2 = num + 1;
			Vector3 val = verts[num];
			float x = val.x;
			if (!(pos.x < x))
			{
				Vector3 val2 = verts[i2];
				float x2 = val2.x;
				if (!(pos.x > x2))
				{
					Vector3 val3 = verts[num];
					float y = val3.y;
					if (!(pos.y < y))
					{
						Vector3 val4 = verts[i2];
						float y2 = val4.y;
						if (!(pos.y > y2))
						{
							return indices[i];
						}
					}
				}
			}
		}
		return 0;
	}

	public static int GetApproximateCharacterIndex(BetterList<Vector3> verts, BetterList<int> indices, Vector2 pos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		float num = 3.40282347E+38f;
		float num2 = 3.40282347E+38f;
		int i = 0;
		for (int j = 0; j < verts.size; j++)
		{
			float y = pos.y;
			Vector3 val = verts[j];
			float num3 = Mathf.Abs(y - val.y);
			if (!(num3 > num2))
			{
				float x = pos.x;
				Vector3 val2 = verts[j];
				float num4 = Mathf.Abs(x - val2.x);
				if (num3 < num2)
				{
					num2 = num3;
					num = num4;
					i = j;
				}
				else if (num4 < num)
				{
					num = num4;
					i = j;
				}
			}
		}
		return indices[i];
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static bool IsSpace(int ch)
	{
		return ch == 32 || ch == 8202 || ch == 8203 || ch == 8201;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && IsSpace(s[num]))
		{
			s[num] = '\n';
		}
		else
		{
			s.Append('\n');
		}
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	private static void ReplaceSpaceWithNewline(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && IsSpace(s[num]))
		{
			s[num] = '\n';
		}
	}

	public static Vector2 CalculatePrintedSize(string text)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = Vector2.get_zero();
		if (!string.IsNullOrEmpty(text))
		{
			if (encoding)
			{
				text = StripSymbols(text);
			}
			Prepare(text);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = text.Length;
			int num4 = 0;
			int prev = 0;
			for (int i = 0; i < length; i++)
			{
				num4 = text[i];
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += finalLineHeight;
				}
				else if (num4 >= 32)
				{
					BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
					if (bMSymbol == null)
					{
						float glyphWidth = GetGlyphWidth(num4, prev);
						if (glyphWidth != 0f)
						{
							glyphWidth += finalSpacingX;
							if (Mathf.RoundToInt(num + glyphWidth) > regionWidth)
							{
								if (num > num3)
								{
									num3 = num - finalSpacingX;
								}
								num = glyphWidth;
								num2 += finalLineHeight;
							}
							else
							{
								num += glyphWidth;
							}
							prev = num4;
						}
					}
					else
					{
						float num5 = finalSpacingX + (float)bMSymbol.advance * fontScale;
						if (Mathf.RoundToInt(num + num5) > regionWidth)
						{
							if (num > num3)
							{
								num3 = num - finalSpacingX;
							}
							num = num5;
							num2 += finalLineHeight;
						}
						else
						{
							num += num5;
						}
						i += bMSymbol.sequence.Length - 1;
						prev = 0;
					}
				}
			}
			zero.x = ((!(num > num3)) ? num3 : (num - finalSpacingX));
			zero.y = num2 + finalLineHeight;
		}
		return zero;
	}

	public static int CalculateOffsetToFit(string text)
	{
		if (string.IsNullOrEmpty(text) || regionWidth < 1)
		{
			return 0;
		}
		Prepare(text);
		int length = text.Length;
		int num = 0;
		int prev = 0;
		int i = 0;
		for (int length2 = text.Length; i < length2; i++)
		{
			BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
			if (bMSymbol == null)
			{
				num = text[i];
				float glyphWidth = GetGlyphWidth(num, prev);
				if (glyphWidth != 0f)
				{
					mSizes.Add(finalSpacingX + glyphWidth);
				}
				prev = num;
			}
			else
			{
				mSizes.Add(finalSpacingX + (float)bMSymbol.advance * fontScale);
				int j = 0;
				for (int num2 = bMSymbol.sequence.Length - 1; j < num2; j++)
				{
					mSizes.Add(0f);
				}
				i += bMSymbol.sequence.Length - 1;
				prev = 0;
			}
		}
		float num3 = (float)regionWidth;
		int num4 = mSizes.size;
		while (num4 > 0 && num3 > 0f)
		{
			num3 -= mSizes[--num4];
		}
		mSizes.Clear();
		if (num3 < 0f)
		{
			num4++;
		}
		return num4;
	}

	public static string GetEndOfLineThatFits(string text)
	{
		int length = text.Length;
		int num = CalculateOffsetToFit(text);
		return text.Substring(num, length - num);
	}

	public static bool WrapText(string text, out string finalText, bool wrapLineColors = false)
	{
		return WrapText(text, out finalText, false, wrapLineColors);
	}

	public static bool WrapText(string text, out string finalText, bool keepCharCount, bool wrapLineColors)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		if (regionWidth < 1 || regionHeight < 1 || finalLineHeight < 1f)
		{
			finalText = string.Empty;
			return false;
		}
		float num = (maxLines <= 0) ? ((float)regionHeight) : Mathf.Min((float)regionHeight, finalLineHeight * (float)maxLines);
		int num2 = (maxLines <= 0) ? 1000000 : maxLines;
		num2 = Mathf.FloorToInt(Mathf.Min((float)num2, num / finalLineHeight) + 0.01f);
		if (num2 == 0)
		{
			finalText = string.Empty;
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		StringBuilder s = new StringBuilder();
		int length = text.Length;
		float num3 = (float)regionWidth;
		int num4 = 0;
		int i = 0;
		int num5 = 1;
		int prev = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		Color item = tint;
		int sub = 0;
		bool bold = false;
		bool italic = false;
		bool underline = false;
		bool strike = false;
		bool ignoreColor = false;
		if (!useSymbols)
		{
			wrapLineColors = false;
		}
		if (wrapLineColors)
		{
			mColors.Add(item);
		}
		for (; i < length; i++)
		{
			char c = text[i];
			if (c > '\u2fff')
			{
				flag3 = true;
			}
			if (c == '\n')
			{
				if (num5 == num2)
				{
					break;
				}
				num3 = (float)regionWidth;
				if (num4 < i)
				{
					s.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					s.Append(c);
				}
				if (wrapLineColors)
				{
					for (int j = 0; j < mColors.size; j++)
					{
						s.Insert(s.Length - 1, "[-]");
					}
					for (int k = 0; k < mColors.size; k++)
					{
						s.Append("[");
						s.Append(EncodeColor(mColors[k]));
						s.Append("]");
					}
				}
				flag = true;
				num5++;
				num4 = i + 1;
				prev = 0;
			}
			else
			{
				if (encoding)
				{
					if (!wrapLineColors)
					{
						if (ParseSymbol(text, ref i))
						{
							i--;
							continue;
						}
					}
					else if (ParseSymbol(text, ref i, mColors, premultiply, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
					{
						if (ignoreColor)
						{
							item = mColors[mColors.size - 1];
							item.a *= mAlpha * tint.a;
						}
						else
						{
							item = tint * mColors[mColors.size - 1];
							item.a *= mAlpha;
						}
						int l = 0;
						for (int num6 = mColors.size - 2; l < num6; l++)
						{
							float a = item.a;
							Color val = mColors[l];
							item.a = a * val.a;
						}
						i--;
						continue;
					}
				}
				BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
				float num7;
				if (bMSymbol == null)
				{
					float glyphWidth = GetGlyphWidth(c, prev);
					if (glyphWidth == 0f)
					{
						continue;
					}
					num7 = finalSpacingX + glyphWidth;
				}
				else
				{
					num7 = finalSpacingX + (float)bMSymbol.advance * fontScale;
				}
				num3 -= num7;
				if (IsSpace(c) && !flag3 && num4 < i)
				{
					int num8 = i - num4 + 1;
					if (num5 == num2 && num3 <= 0f && i < length)
					{
						char c2 = text[i];
						if (c2 < ' ' || IsSpace(c2))
						{
							num8--;
						}
					}
					s.Append(text.Substring(num4, num8));
					flag = false;
					num4 = i + 1;
					prev = c;
				}
				if (Mathf.RoundToInt(num3) < 0)
				{
					if (!flag && num5 != num2)
					{
						flag = true;
						num3 = (float)regionWidth;
						i = num4 - 1;
						prev = 0;
						if (num5++ == num2)
						{
							break;
						}
						if (keepCharCount)
						{
							ReplaceSpaceWithNewline(ref s);
						}
						else
						{
							EndLine(ref s);
						}
						if (wrapLineColors)
						{
							for (int m = 0; m < mColors.size; m++)
							{
								s.Insert(s.Length - 1, "[-]");
							}
							for (int n = 0; n < mColors.size; n++)
							{
								s.Append("[");
								s.Append(EncodeColor(mColors[n]));
								s.Append("]");
							}
						}
						continue;
					}
					s.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
					bool flag4 = IsSpace(c);
					if (!flag4 && !flag3)
					{
						flag2 = false;
					}
					if (wrapLineColors && mColors.size > 0)
					{
						s.Append("[-]");
					}
					if (num5++ == num2)
					{
						num4 = i;
						break;
					}
					if (keepCharCount)
					{
						ReplaceSpaceWithNewline(ref s);
					}
					else
					{
						EndLine(ref s);
					}
					if (wrapLineColors)
					{
						for (int num11 = 0; num11 < mColors.size; num11++)
						{
							s.Insert(s.Length - 1, "[-]");
						}
						for (int num12 = 0; num12 < mColors.size; num12++)
						{
							s.Append("[");
							s.Append(EncodeColor(mColors[num12]));
							s.Append("]");
						}
					}
					flag = true;
					if (flag4)
					{
						num4 = i + 1;
						num3 = (float)regionWidth;
					}
					else
					{
						num4 = i;
						num3 = (float)regionWidth - num7;
					}
					prev = 0;
				}
				else
				{
					prev = c;
				}
				if (bMSymbol != null)
				{
					i += bMSymbol.length - 1;
					prev = 0;
				}
			}
		}
		if (num4 < i)
		{
			s.Append(text.Substring(num4, i - num4));
		}
		if (wrapLineColors && mColors.size > 0)
		{
			s.Append("[-]");
		}
		finalText = s.ToString();
		mColors.Clear();
		return flag2 && (i == length || num5 <= Mathf.Min(maxLines, num2));
	}

	public static void Print(string text, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(text))
		{
			int size = verts.size;
			Prepare(text);
			mColors.Add(Color.get_white());
			mAlpha = 1f;
			int num = 0;
			int prev = 0;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = (float)finalSize;
			Color val = tint * gradientBottom;
			Color val2 = tint * gradientTop;
			Color32 val3 = Color32.op_Implicit(tint);
			int length = text.Length;
			Rect val4 = default(Rect);
			float num6 = 0f;
			float num7 = 0f;
			float num8 = num5 * pixelDensity;
			bool flag = false;
			int sub = 0;
			bool bold = false;
			bool italic = false;
			bool underline = false;
			bool strike = false;
			bool ignoreColor = false;
			float num9 = 0f;
			if (bitmapFont != null)
			{
				val4 = bitmapFont.uvRect;
				num6 = val4.get_width() / (float)bitmapFont.texWidth;
				num7 = val4.get_height() / (float)bitmapFont.texHeight;
			}
			for (int i = 0; i < length; i++)
			{
				num = text[i];
				num9 = num2;
				if (num == 10)
				{
					if (num2 > num4)
					{
						num4 = num2;
					}
					if (alignment != Alignment.Left)
					{
						Align(verts, size, num2 - finalSpacingX, 4);
						size = verts.size;
					}
					num2 = 0f;
					num3 += finalLineHeight;
					prev = 0;
				}
				else if (num < 32)
				{
					prev = num;
				}
				else if (encoding && ParseSymbol(text, ref i, mColors, premultiply, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
				{
					Color val5;
					if (ignoreColor)
					{
						val5 = mColors[mColors.size - 1];
						val5.a *= mAlpha * tint.a;
					}
					else
					{
						val5 = tint * mColors[mColors.size - 1];
						val5.a *= mAlpha;
					}
					val3 = Color32.op_Implicit(val5);
					int j = 0;
					for (int num10 = mColors.size - 2; j < num10; j++)
					{
						float a = val5.a;
						Color val6 = mColors[j];
						val5.a = a * val6.a;
					}
					if (gradient)
					{
						val = gradientBottom * val5;
						val2 = gradientTop * val5;
					}
					i--;
				}
				else
				{
					BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
					if (bMSymbol != null)
					{
						float num11 = num2 + (float)bMSymbol.offsetX * fontScale;
						float num12 = num11 + (float)bMSymbol.width * fontScale;
						float num13 = 0f - (num3 + (float)bMSymbol.offsetY * fontScale);
						float num14 = num13 - (float)bMSymbol.height * fontScale;
						if (Mathf.RoundToInt(num2 + (float)bMSymbol.advance * fontScale) > regionWidth)
						{
							if (num2 == 0f)
							{
								return;
							}
							if (alignment != Alignment.Left && size < verts.size)
							{
								Align(verts, size, num2 - finalSpacingX, 4);
								size = verts.size;
							}
							num11 -= num2;
							num12 -= num2;
							num14 -= finalLineHeight;
							num13 -= finalLineHeight;
							num2 = 0f;
							num3 += finalLineHeight;
							num9 = 0f;
						}
						verts.Add(new Vector3(num11, num14));
						verts.Add(new Vector3(num11, num13));
						verts.Add(new Vector3(num12, num13));
						verts.Add(new Vector3(num12, num14));
						num2 += finalSpacingX + (float)bMSymbol.advance * fontScale;
						i += bMSymbol.length - 1;
						prev = 0;
						if (uvs != null)
						{
							Rect uvRect = bMSymbol.uvRect;
							float xMin = uvRect.get_xMin();
							float yMin = uvRect.get_yMin();
							float xMax = uvRect.get_xMax();
							float yMax = uvRect.get_yMax();
							uvs.Add(new Vector2(xMin, yMin));
							uvs.Add(new Vector2(xMin, yMax));
							uvs.Add(new Vector2(xMax, yMax));
							uvs.Add(new Vector2(xMax, yMin));
						}
						if (cols != null)
						{
							if (symbolStyle == SymbolStyle.Colored)
							{
								for (int k = 0; k < 4; k++)
								{
									cols.Add(val3);
								}
							}
							else
							{
								Color32 item = Color32.op_Implicit(Color.get_white());
								item.a = val3.a;
								for (int l = 0; l < 4; l++)
								{
									cols.Add(item);
								}
							}
						}
					}
					else
					{
						GlyphInfo glyphInfo = GetGlyph(num, prev);
						if (glyphInfo != null)
						{
							prev = num;
							if (sub != 0)
							{
								glyphInfo.v0.x *= 0.75f;
								glyphInfo.v0.y *= 0.75f;
								glyphInfo.v1.x *= 0.75f;
								glyphInfo.v1.y *= 0.75f;
								if (sub == 1)
								{
									glyphInfo.v0.y -= fontScale * (float)fontSize * 0.4f;
									glyphInfo.v1.y -= fontScale * (float)fontSize * 0.4f;
								}
								else
								{
									glyphInfo.v0.y += fontScale * (float)fontSize * 0.05f;
									glyphInfo.v1.y += fontScale * (float)fontSize * 0.05f;
								}
							}
							float num11 = glyphInfo.v0.x + num2;
							float num14 = glyphInfo.v0.y - num3;
							float num12 = glyphInfo.v1.x + num2;
							float num13 = glyphInfo.v1.y - num3;
							float num15 = glyphInfo.advance;
							if (finalSpacingX < 0f)
							{
								num15 += finalSpacingX;
							}
							if (Mathf.RoundToInt(num2 + num15) > regionWidth)
							{
								if (num2 == 0f)
								{
									return;
								}
								if (alignment != Alignment.Left && size < verts.size)
								{
									Align(verts, size, num2 - finalSpacingX, 4);
									size = verts.size;
								}
								num11 -= num2;
								num12 -= num2;
								num14 -= finalLineHeight;
								num13 -= finalLineHeight;
								num2 = 0f;
								num3 += finalLineHeight;
								num9 = 0f;
							}
							if (IsSpace(num))
							{
								if (underline)
								{
									num = 95;
								}
								else if (strike)
								{
									num = 45;
								}
							}
							num2 += ((sub != 0) ? ((finalSpacingX + glyphInfo.advance) * 0.75f) : (finalSpacingX + glyphInfo.advance));
							if (!IsSpace(num))
							{
								if (uvs != null)
								{
									if (bitmapFont != null)
									{
										glyphInfo.u0.x = val4.get_xMin() + num6 * glyphInfo.u0.x;
										glyphInfo.u2.x = val4.get_xMin() + num6 * glyphInfo.u2.x;
										glyphInfo.u0.y = val4.get_yMax() - num7 * glyphInfo.u0.y;
										glyphInfo.u2.y = val4.get_yMax() - num7 * glyphInfo.u2.y;
										glyphInfo.u1.x = glyphInfo.u0.x;
										glyphInfo.u1.y = glyphInfo.u2.y;
										glyphInfo.u3.x = glyphInfo.u2.x;
										glyphInfo.u3.y = glyphInfo.u0.y;
									}
									int m = 0;
									for (int num16 = (!bold) ? 1 : 4; m < num16; m++)
									{
										uvs.Add(glyphInfo.u0);
										uvs.Add(glyphInfo.u1);
										uvs.Add(glyphInfo.u2);
										uvs.Add(glyphInfo.u3);
									}
								}
								if (cols != null)
								{
									if (glyphInfo.channel == 0 || glyphInfo.channel == 15)
									{
										if (gradient)
										{
											float num17 = num8 + glyphInfo.v0.y / fontScale;
											float num18 = num8 + glyphInfo.v1.y / fontScale;
											num17 /= num8;
											num18 /= num8;
											s_c0 = Color32.op_Implicit(Color.Lerp(val, val2, num17));
											s_c1 = Color32.op_Implicit(Color.Lerp(val, val2, num18));
											int n = 0;
											for (int num19 = (!bold) ? 1 : 4; n < num19; n++)
											{
												cols.Add(s_c0);
												cols.Add(s_c1);
												cols.Add(s_c1);
												cols.Add(s_c0);
											}
										}
										else
										{
											int num20 = 0;
											for (int num21 = (!bold) ? 4 : 16; num20 < num21; num20++)
											{
												cols.Add(val3);
											}
										}
									}
									else
									{
										Color val7 = Color32.op_Implicit(val3);
										val7 *= 0.49f;
										switch (glyphInfo.channel)
										{
										case 1:
											val7.b += 0.51f;
											break;
										case 2:
											val7.g += 0.51f;
											break;
										case 4:
											val7.r += 0.51f;
											break;
										case 8:
											val7.a += 0.51f;
											break;
										}
										Color32 item2 = Color32.op_Implicit(val7);
										int num22 = 0;
										for (int num23 = (!bold) ? 4 : 16; num22 < num23; num22++)
										{
											cols.Add(item2);
										}
									}
								}
								if (!bold)
								{
									if (!italic)
									{
										verts.Add(new Vector3(num11, num14));
										verts.Add(new Vector3(num11, num13));
										verts.Add(new Vector3(num12, num13));
										verts.Add(new Vector3(num12, num14));
									}
									else
									{
										float num24 = (float)fontSize * 0.1f * ((num13 - num14) / (float)fontSize);
										verts.Add(new Vector3(num11 - num24, num14));
										verts.Add(new Vector3(num11 + num24, num13));
										verts.Add(new Vector3(num12 + num24, num13));
										verts.Add(new Vector3(num12 - num24, num14));
									}
								}
								else
								{
									for (int num25 = 0; num25 < 4; num25++)
									{
										float num26 = mBoldOffset[num25 * 2];
										float num27 = mBoldOffset[num25 * 2 + 1];
										float num28 = (!italic) ? 0f : ((float)fontSize * 0.1f * ((num13 - num14) / (float)fontSize));
										verts.Add(new Vector3(num11 + num26 - num28, num14 + num27));
										verts.Add(new Vector3(num11 + num26 + num28, num13 + num27));
										verts.Add(new Vector3(num12 + num26 + num28, num13 + num27));
										verts.Add(new Vector3(num12 + num26 - num28, num14 + num27));
									}
								}
								if (underline || strike)
								{
									GlyphInfo glyphInfo2 = GetGlyph((!strike) ? 95 : 45, prev);
									if (glyphInfo2 != null)
									{
										if (uvs != null)
										{
											if (bitmapFont != null)
											{
												glyphInfo2.u0.x = val4.get_xMin() + num6 * glyphInfo2.u0.x;
												glyphInfo2.u2.x = val4.get_xMin() + num6 * glyphInfo2.u2.x;
												glyphInfo2.u0.y = val4.get_yMax() - num7 * glyphInfo2.u0.y;
												glyphInfo2.u2.y = val4.get_yMax() - num7 * glyphInfo2.u2.y;
											}
											float num29 = (glyphInfo2.u0.x + glyphInfo2.u2.x) * 0.5f;
											int num30 = 0;
											for (int num31 = (!bold) ? 1 : 4; num30 < num31; num30++)
											{
												uvs.Add(new Vector2(num29, glyphInfo2.u0.y));
												uvs.Add(new Vector2(num29, glyphInfo2.u2.y));
												uvs.Add(new Vector2(num29, glyphInfo2.u2.y));
												uvs.Add(new Vector2(num29, glyphInfo2.u0.y));
											}
										}
										if (flag && strike)
										{
											num14 = (0f - num3 + glyphInfo2.v0.y) * 0.75f;
											num13 = (0f - num3 + glyphInfo2.v1.y) * 0.75f;
										}
										else
										{
											num14 = 0f - num3 + glyphInfo2.v0.y;
											num13 = 0f - num3 + glyphInfo2.v1.y;
										}
										if (bold)
										{
											for (int num32 = 0; num32 < 4; num32++)
											{
												float num33 = mBoldOffset[num32 * 2];
												float num34 = mBoldOffset[num32 * 2 + 1];
												verts.Add(new Vector3(num9 + num33, num14 + num34));
												verts.Add(new Vector3(num9 + num33, num13 + num34));
												verts.Add(new Vector3(num2 + num33, num13 + num34));
												verts.Add(new Vector3(num2 + num33, num14 + num34));
											}
										}
										else
										{
											verts.Add(new Vector3(num9, num14));
											verts.Add(new Vector3(num9, num13));
											verts.Add(new Vector3(num2, num13));
											verts.Add(new Vector3(num2, num14));
										}
										if (gradient)
										{
											float num35 = num8 + glyphInfo2.v0.y / fontScale;
											float num36 = num8 + glyphInfo2.v1.y / fontScale;
											num35 /= num8;
											num36 /= num8;
											s_c0 = Color32.op_Implicit(Color.Lerp(val, val2, num35));
											s_c1 = Color32.op_Implicit(Color.Lerp(val, val2, num36));
											int num37 = 0;
											for (int num38 = (!bold) ? 1 : 4; num37 < num38; num37++)
											{
												cols.Add(s_c0);
												cols.Add(s_c1);
												cols.Add(s_c1);
												cols.Add(s_c0);
											}
										}
										else
										{
											int num39 = 0;
											for (int num40 = (!bold) ? 4 : 16; num39 < num40; num39++)
											{
												cols.Add(val3);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (alignment != Alignment.Left && size < verts.size)
			{
				Align(verts, size, num2 - finalSpacingX, 4);
				size = verts.size;
			}
			mColors.Clear();
		}
	}

	public static void PrintApproximateCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)fontSize * fontScale * 0.5f;
		int length = text.Length;
		int size = verts.size;
		int num5 = 0;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			num5 = text[i];
			verts.Add(new Vector3(num, 0f - num2 - num4));
			indices.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (alignment != Alignment.Left)
				{
					Align(verts, size, num - finalSpacingX, 1);
					size = verts.size;
				}
				num = 0f;
				num2 += finalLineHeight;
				prev = 0;
			}
			else if (num5 < 32)
			{
				prev = 0;
			}
			else if (encoding && ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
				if (bMSymbol == null)
				{
					float glyphWidth = GetGlyphWidth(num5, prev);
					if (glyphWidth != 0f)
					{
						glyphWidth += finalSpacingX;
						if (Mathf.RoundToInt(num + glyphWidth) > regionWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (alignment != Alignment.Left && size < verts.size)
							{
								Align(verts, size, num - finalSpacingX, 1);
								size = verts.size;
							}
							num = glyphWidth;
							num2 += finalLineHeight;
						}
						else
						{
							num += glyphWidth;
						}
						verts.Add(new Vector3(num, 0f - num2 - num4));
						indices.Add(i + 1);
						prev = num5;
					}
				}
				else
				{
					float num6 = (float)bMSymbol.advance * fontScale + finalSpacingX;
					if (Mathf.RoundToInt(num + num6) > regionWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (alignment != Alignment.Left && size < verts.size)
						{
							Align(verts, size, num - finalSpacingX, 1);
							size = verts.size;
						}
						num = num6;
						num2 += finalLineHeight;
					}
					else
					{
						num += num6;
					}
					verts.Add(new Vector3(num, 0f - num2 - num4));
					indices.Add(i + 1);
					i += bMSymbol.sequence.Length - 1;
					prev = 0;
				}
			}
		}
		if (alignment != Alignment.Left && size < verts.size)
		{
			Align(verts, size, num - finalSpacingX, 1);
		}
	}

	public static void PrintExactCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		float num = (float)fontSize * fontScale;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		int length = text.Length;
		int size = verts.size;
		int num5 = 0;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			num5 = text[i];
			if (num5 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (alignment != Alignment.Left)
				{
					Align(verts, size, num2 - finalSpacingX, 2);
					size = verts.size;
				}
				num2 = 0f;
				num3 += finalLineHeight;
				prev = 0;
			}
			else if (num5 < 32)
			{
				prev = 0;
			}
			else if (encoding && ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
				if (bMSymbol == null)
				{
					float glyphWidth = GetGlyphWidth(num5, prev);
					if (glyphWidth != 0f)
					{
						float num6 = glyphWidth + finalSpacingX;
						if (Mathf.RoundToInt(num2 + num6) > regionWidth)
						{
							if (num2 == 0f)
							{
								return;
							}
							if (alignment != Alignment.Left && size < verts.size)
							{
								Align(verts, size, num2 - finalSpacingX, 2);
								size = verts.size;
							}
							num2 = 0f;
							num3 += finalLineHeight;
							prev = 0;
							i--;
						}
						else
						{
							indices.Add(i);
							verts.Add(new Vector3(num2, 0f - num3 - num));
							verts.Add(new Vector3(num2 + num6, 0f - num3));
							prev = num5;
							num2 += num6;
						}
					}
				}
				else
				{
					float num7 = (float)bMSymbol.advance * fontScale + finalSpacingX;
					if (Mathf.RoundToInt(num2 + num7) > regionWidth)
					{
						if (num2 == 0f)
						{
							return;
						}
						if (alignment != Alignment.Left && size < verts.size)
						{
							Align(verts, size, num2 - finalSpacingX, 2);
							size = verts.size;
						}
						num2 = 0f;
						num3 += finalLineHeight;
						prev = 0;
						i--;
					}
					else
					{
						indices.Add(i);
						verts.Add(new Vector3(num2, 0f - num3 - num));
						verts.Add(new Vector3(num2 + num7, 0f - num3));
						i += bMSymbol.sequence.Length - 1;
						num2 += num7;
						prev = 0;
					}
				}
			}
		}
		if (alignment != Alignment.Left && size < verts.size)
		{
			Align(verts, size, num2 - finalSpacingX, 2);
		}
	}

	public static void PrintCaretAndSelection(string text, int start, int end, BetterList<Vector3> caret, BetterList<Vector3> highlight)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		int num = end;
		if (start > end)
		{
			end = start;
			start = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)fontSize * fontScale;
		int indexOffset = caret?.size ?? 0;
		int num6 = highlight?.size ?? 0;
		int length = text.Length;
		int i = 0;
		int num7 = 0;
		int prev = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 zero = Vector2.get_zero();
		Vector2 zero2 = Vector2.get_zero();
		for (; i < length; i++)
		{
			if (caret != null && !flag2 && num <= i)
			{
				flag2 = true;
				caret.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				caret.Add(new Vector3(num2 - 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			num7 = text[i];
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (alignment != Alignment.Left)
					{
						Align(caret, indexOffset, num2 - finalSpacingX, 4);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(Vector2.op_Implicit(zero2));
						highlight.Add(Vector2.op_Implicit(zero));
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, 0f - num3 - num5));
						highlight.Add(new Vector3(num2, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
					}
					if (alignment != Alignment.Left && num6 < highlight.size)
					{
						Align(highlight, num6, num2 - finalSpacingX, 4);
						num6 = highlight.size;
					}
				}
				num2 = 0f;
				num3 += finalLineHeight;
				prev = 0;
			}
			else if (num7 < 32)
			{
				prev = 0;
			}
			else if (encoding && ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!useSymbols) ? null : GetSymbol(text, i, length);
				float num8 = (bMSymbol == null) ? GetGlyphWidth(num7, prev) : ((float)bMSymbol.advance * fontScale);
				if (num8 != 0f)
				{
					float num9 = num2;
					float num10 = num2 + num8;
					float num11 = 0f - num3 - num5;
					float num12 = 0f - num3;
					if (Mathf.RoundToInt(num10 + finalSpacingX) > regionWidth)
					{
						if (num2 == 0f)
						{
							return;
						}
						if (num2 > num4)
						{
							num4 = num2;
						}
						if (caret != null && flag2)
						{
							if (alignment != Alignment.Left)
							{
								Align(caret, indexOffset, num2 - finalSpacingX, 4);
							}
							caret = null;
						}
						if (highlight != null)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(Vector2.op_Implicit(zero2));
								highlight.Add(Vector2.op_Implicit(zero));
							}
							else if (start <= i && end > i)
							{
								highlight.Add(new Vector3(num2, 0f - num3 - num5));
								highlight.Add(new Vector3(num2, 0f - num3));
								highlight.Add(new Vector3(num2 + 2f, 0f - num3));
								highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
							}
							if (alignment != Alignment.Left && num6 < highlight.size)
							{
								Align(highlight, num6, num2 - finalSpacingX, 4);
								num6 = highlight.size;
							}
						}
						num9 -= num2;
						num10 -= num2;
						num11 -= finalLineHeight;
						num12 -= finalLineHeight;
						num2 = 0f;
						num3 += finalLineHeight;
					}
					num2 += num8 + finalSpacingX;
					if (highlight != null)
					{
						if (start > i || end <= i)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(Vector2.op_Implicit(zero2));
								highlight.Add(Vector2.op_Implicit(zero));
							}
						}
						else if (!flag)
						{
							flag = true;
							highlight.Add(new Vector3(num9, num11));
							highlight.Add(new Vector3(num9, num12));
						}
					}
					zero._002Ector(num10, num11);
					zero2._002Ector(num10, num12);
					prev = num7;
				}
			}
		}
		if (caret != null)
		{
			if (!flag2)
			{
				caret.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				caret.Add(new Vector3(num2 - 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			if (alignment != Alignment.Left)
			{
				Align(caret, indexOffset, num2 - finalSpacingX, 4);
			}
		}
		if (highlight != null)
		{
			if (flag)
			{
				highlight.Add(Vector2.op_Implicit(zero2));
				highlight.Add(Vector2.op_Implicit(zero));
			}
			else if (start < i && end == i)
			{
				highlight.Add(new Vector3(num2, 0f - num3 - num5));
				highlight.Add(new Vector3(num2, 0f - num3));
				highlight.Add(new Vector3(num2 + 2f, 0f - num3));
				highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
			}
			if (alignment != Alignment.Left && num6 < highlight.size)
			{
				Align(highlight, num6, num2 - finalSpacingX, 4);
			}
		}
	}
}
