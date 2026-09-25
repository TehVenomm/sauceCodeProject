// Decompiled with JetBrains decompiler
// Type: NGUIText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Diagnostics;
using System.Text;
using UnityEngine;

#nullable disable
public static class NGUIText
{
  public static UIFont bitmapFont;
  public static Font dynamicFont;
  public static NGUIText.GlyphInfo glyph = new NGUIText.GlyphInfo();
  public static int fontSize = 16 /*0x10*/;
  public static float fontScale = 1f;
  public static float pixelDensity = 1f;
  public static FontStyle fontStyle = (FontStyle) 0;
  public static NGUIText.Alignment alignment = NGUIText.Alignment.Left;
  public static Color tint = Color.white;
  public static int rectWidth = 1000000;
  public static int rectHeight = 1000000;
  public static int regionWidth = 1000000;
  public static int regionHeight = 1000000;
  public static int maxLines = 0;
  public static bool gradient = false;
  public static Color gradientBottom = Color.white;
  public static Color gradientTop = Color.white;
  public static bool encoding = false;
  public static float spacingX = 0.0f;
  public static float spacingY = 0.0f;
  public static bool premultiply = false;
  public static NGUIText.SymbolStyle symbolStyle;
  public static int finalSize = 0;
  public static float finalSpacingX = 0.0f;
  public static float finalLineHeight = 0.0f;
  public static float baseline = 0.0f;
  public static bool useSymbols = false;
  private static Color mInvisible = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  private static BetterList<Color> mColors = new BetterList<Color>();
  private static float mAlpha = 1f;
  private static CharacterInfo mTempChar;
  private static BetterList<float> mSizes = new BetterList<float>();
  private static Color32 s_c0;
  private static Color32 s_c1;
  private static float[] mBoldOffset = new float[8]
  {
    -0.25f,
    0.0f,
    0.25f,
    0.0f,
    0.0f,
    -0.25f,
    0.0f,
    0.25f
  };

  public static void Update() => NGUIText.Update(true);

  public static void Update(bool request)
  {
    NGUIText.finalSize = Mathf.RoundToInt((float) NGUIText.fontSize / NGUIText.pixelDensity);
    NGUIText.finalSpacingX = NGUIText.spacingX * NGUIText.fontScale;
    NGUIText.finalLineHeight = ((float) NGUIText.fontSize + NGUIText.spacingY) * NGUIText.fontScale;
    NGUIText.useSymbols = Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null) && NGUIText.bitmapFont.hasSymbols && NGUIText.encoding && NGUIText.symbolStyle != 0;
    if (!(Object.op_Inequality((Object) NGUIText.dynamicFont, (Object) null) & request))
      return;
    NGUIText.dynamicFont.RequestCharactersInTexture(")_-", NGUIText.finalSize, NGUIText.fontStyle);
    if (!NGUIText.dynamicFont.GetCharacterInfo(')', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle) || (double) ((CharacterInfo) ref NGUIText.mTempChar).maxY == 0.0)
    {
      NGUIText.dynamicFont.RequestCharactersInTexture("A", NGUIText.finalSize, NGUIText.fontStyle);
      if (!NGUIText.dynamicFont.GetCharacterInfo('A', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
      {
        NGUIText.baseline = 0.0f;
        return;
      }
    }
    float maxY = (float) ((CharacterInfo) ref NGUIText.mTempChar).maxY;
    float minY = (float) ((CharacterInfo) ref NGUIText.mTempChar).minY;
    NGUIText.baseline = Mathf.Round(maxY + (float) (((double) NGUIText.finalSize - (double) maxY + (double) minY) * 0.5));
  }

  public static void Prepare(string text)
  {
    if (!Object.op_Inequality((Object) NGUIText.dynamicFont, (Object) null))
      return;
    NGUIText.dynamicFont.RequestCharactersInTexture(text, NGUIText.finalSize, NGUIText.fontStyle);
  }

  public static BMSymbol GetSymbol(string text, int index, int textLength)
  {
    return !Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null) ? (BMSymbol) null : NGUIText.bitmapFont.MatchSymbol(text, index, textLength);
  }

  public static float GetGlyphWidth(int ch, int prev)
  {
    if (Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null))
    {
      bool flag = false;
      if (ch == 8201)
      {
        flag = true;
        ch = 32 /*0x20*/;
      }
      BMGlyph glyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
      if (glyph != null)
      {
        int advance = glyph.advance;
        if (flag)
          advance >>= 1;
        return NGUIText.fontScale * (prev != 0 ? (float) (advance + glyph.GetKerning(prev)) : (float) glyph.advance);
      }
    }
    else if (Object.op_Inequality((Object) NGUIText.dynamicFont, (Object) null) && NGUIText.dynamicFont.GetCharacterInfo((char) ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
      return (float) ((CharacterInfo) ref NGUIText.mTempChar).advance * NGUIText.fontScale * NGUIText.pixelDensity;
    return 0.0f;
  }

  public static NGUIText.GlyphInfo GetGlyph(int ch, int prev)
  {
    if (Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null))
    {
      bool flag = false;
      if (ch == 8201)
      {
        flag = true;
        ch = 32 /*0x20*/;
      }
      BMGlyph glyph1 = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
      if (glyph1 != null)
      {
        int kerning = prev != 0 ? glyph1.GetKerning(prev) : 0;
        NGUIText.glyph.v0.x = prev != 0 ? (float) (glyph1.offsetX + kerning) : (float) glyph1.offsetX;
        NGUIText.glyph.v1.y = (float) -glyph1.offsetY;
        NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + (float) glyph1.width;
        NGUIText.glyph.v0.y = NGUIText.glyph.v1.y - (float) glyph1.height;
        NGUIText.glyph.u0.x = (float) glyph1.x;
        NGUIText.glyph.u0.y = (float) (glyph1.y + glyph1.height);
        NGUIText.glyph.u2.x = (float) (glyph1.x + glyph1.width);
        NGUIText.glyph.u2.y = (float) glyph1.y;
        NGUIText.glyph.u1.x = NGUIText.glyph.u0.x;
        NGUIText.glyph.u1.y = NGUIText.glyph.u2.y;
        NGUIText.glyph.u3.x = NGUIText.glyph.u2.x;
        NGUIText.glyph.u3.y = NGUIText.glyph.u0.y;
        int advance = glyph1.advance;
        if (flag)
          advance >>= 1;
        NGUIText.glyph.advance = (float) (advance + kerning);
        NGUIText.glyph.channel = glyph1.channel;
        if ((double) NGUIText.fontScale != 1.0)
        {
          NGUIText.GlyphInfo glyph2 = NGUIText.glyph;
          glyph2.v0 = Vector2.op_Multiply(glyph2.v0, NGUIText.fontScale);
          NGUIText.GlyphInfo glyph3 = NGUIText.glyph;
          glyph3.v1 = Vector2.op_Multiply(glyph3.v1, NGUIText.fontScale);
          NGUIText.glyph.advance *= NGUIText.fontScale;
        }
        return NGUIText.glyph;
      }
    }
    else if (Object.op_Inequality((Object) NGUIText.dynamicFont, (Object) null) && NGUIText.dynamicFont.GetCharacterInfo((char) ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
    {
      NGUIText.glyph.v0.x = (float) ((CharacterInfo) ref NGUIText.mTempChar).minX;
      NGUIText.glyph.v1.x = (float) ((CharacterInfo) ref NGUIText.mTempChar).maxX;
      NGUIText.glyph.v0.y = (float) ((CharacterInfo) ref NGUIText.mTempChar).maxY - NGUIText.baseline;
      NGUIText.glyph.v1.y = (float) ((CharacterInfo) ref NGUIText.mTempChar).minY - NGUIText.baseline;
      NGUIText.glyph.u0 = ((CharacterInfo) ref NGUIText.mTempChar).uvTopLeft;
      NGUIText.glyph.u1 = ((CharacterInfo) ref NGUIText.mTempChar).uvBottomLeft;
      NGUIText.glyph.u2 = ((CharacterInfo) ref NGUIText.mTempChar).uvBottomRight;
      NGUIText.glyph.u3 = ((CharacterInfo) ref NGUIText.mTempChar).uvTopRight;
      NGUIText.glyph.advance = (float) ((CharacterInfo) ref NGUIText.mTempChar).advance;
      NGUIText.glyph.channel = 0;
      NGUIText.glyph.v0.x = Mathf.Round(NGUIText.glyph.v0.x);
      NGUIText.glyph.v0.y = Mathf.Round(NGUIText.glyph.v0.y);
      NGUIText.glyph.v1.x = Mathf.Round(NGUIText.glyph.v1.x);
      NGUIText.glyph.v1.y = Mathf.Round(NGUIText.glyph.v1.y);
      float num = NGUIText.fontScale * NGUIText.pixelDensity;
      if ((double) num != 1.0)
      {
        NGUIText.GlyphInfo glyph4 = NGUIText.glyph;
        glyph4.v0 = Vector2.op_Multiply(glyph4.v0, num);
        NGUIText.GlyphInfo glyph5 = NGUIText.glyph;
        glyph5.v1 = Vector2.op_Multiply(glyph5.v1, num);
        NGUIText.glyph.advance *= num;
      }
      return NGUIText.glyph;
    }
    return (NGUIText.GlyphInfo) null;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static float ParseAlpha(string text, int index)
  {
    return Mathf.Clamp01((float) (NGUIMath.HexToDecimal(text[index + 1]) << 4 | NGUIMath.HexToDecimal(text[index + 2])) / (float) byte.MaxValue);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static Color ParseColor(string text, int offset) => NGUIText.ParseColor24(text, offset);

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static Color ParseColor24(string text, int offset)
  {
    int num1 = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
    int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
    int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
    float num4 = 0.003921569f;
    return new Color(num4 * (float) num1, num4 * (float) num2, num4 * (float) num3);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static Color ParseColor32(string text, int offset)
  {
    int num1 = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
    int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
    int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
    int num4 = NGUIMath.HexToDecimal(text[offset + 6]) << 4 | NGUIMath.HexToDecimal(text[offset + 7]);
    float num5 = 0.003921569f;
    return new Color(num5 * (float) num1, num5 * (float) num2, num5 * (float) num3, num5 * (float) num4);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string EncodeColor(Color c) => NGUIText.EncodeColor24(c);

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string EncodeColor(string text, Color c)
  {
    return $"[c][{NGUIText.EncodeColor24(c)}]{text}[-][/c]";
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string EncodeAlpha(float a)
  {
    return NGUIMath.DecimalToHex8(Mathf.Clamp(Mathf.RoundToInt(a * (float) byte.MaxValue), 0, (int) byte.MaxValue));
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string EncodeColor24(Color c)
  {
    return NGUIMath.DecimalToHex24(16777215 /*0xFFFFFF*/ & NGUIMath.ColorToInt(c) >> 8);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static string EncodeColor32(Color c) => NGUIMath.DecimalToHex32(NGUIMath.ColorToInt(c));

  public static bool ParseSymbol(string text, ref int index)
  {
    int sub = 1;
    bool bold = false;
    bool italic = false;
    bool underline = false;
    bool strike = false;
    bool ignoreColor = false;
    return NGUIText.ParseSymbol(text, ref index, (BetterList<Color>) null, false, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static bool IsHex(char ch)
  {
    if (ch >= '0' && ch <= '9' || ch >= 'a' && ch <= 'f')
      return true;
    return ch >= 'A' && ch <= 'F';
  }

  public static bool ParseSymbol(
    string text,
    ref int index,
    BetterList<Color> colors,
    bool premultiply,
    ref int sub,
    ref bool bold,
    ref bool italic,
    ref bool underline,
    ref bool strike,
    ref bool ignoreColor)
  {
    int length = text.Length;
    if (index + 3 > length || text[index] != '[')
      return false;
    if (text[index + 2] == ']')
    {
      if (text[index + 1] == '-')
      {
        if (colors != null && colors.size > 1)
          colors.RemoveAt(colors.size - 1);
        index += 3;
        return true;
      }
      switch (text.Substring(index, 3))
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
      return false;
    if (text[index + 3] == ']')
    {
      switch (text.Substring(index, 4))
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
        default:
          char ch1 = text[index + 1];
          char ch2 = text[index + 2];
          if (NGUIText.IsHex(ch1) && NGUIText.IsHex(ch2))
          {
            NGUIText.mAlpha = (float) (NGUIMath.HexToDecimal(ch1) << 4 | NGUIMath.HexToDecimal(ch2)) / (float) byte.MaxValue;
            index += 4;
            return true;
          }
          break;
      }
    }
    if (index + 5 > length)
      return false;
    if (text[index + 4] == ']')
    {
      switch (text.Substring(index, 5))
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
      return false;
    if (text[index + 5] == ']')
    {
      switch (text.Substring(index, 6))
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
      int num = text.IndexOf(']', index + 4);
      if (num != -1)
      {
        index = num + 1;
        return true;
      }
      index = text.Length;
      return true;
    }
    if (index + 8 > length)
      return false;
    if (text[index + 7] == ']')
    {
      Color c = NGUIText.ParseColor24(text, index + 1);
      if (NGUIText.EncodeColor24(c) != text.Substring(index + 1, 6).ToUpper())
        return false;
      if (colors != null)
      {
        c.a = colors[colors.size - 1].a;
        if (premultiply && (double) c.a != 1.0)
          c = Color.Lerp(NGUIText.mInvisible, c, c.a);
        colors.Add(c);
      }
      index += 8;
      return true;
    }
    if (index + 10 > length || text[index + 9] != ']')
      return false;
    Color c1 = NGUIText.ParseColor32(text, index + 1);
    if (NGUIText.EncodeColor32(c1) != text.Substring(index + 1, 8).ToUpper())
      return false;
    if (colors != null)
    {
      if (premultiply && (double) c1.a != 1.0)
        c1 = Color.Lerp(NGUIText.mInvisible, c1, c1.a);
      colors.Add(c1);
    }
    index += 10;
    return true;
  }

  public static string StripSymbols(string text)
  {
    if (text != null)
    {
      int num = 0;
      int length = text.Length;
      while (num < length)
      {
        if (text[num] == '[')
        {
          int sub = 0;
          bool bold = false;
          bool italic = false;
          bool underline = false;
          bool strike = false;
          bool ignoreColor = false;
          int index = num;
          if (NGUIText.ParseSymbol(text, ref index, (BetterList<Color>) null, false, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
          {
            text = text.Remove(num, index - num);
            length = text.Length;
            continue;
          }
        }
        ++num;
      }
    }
    return text;
  }

  public static void Align(
    BetterList<Vector3> verts,
    int indexOffset,
    float printedWidth,
    int elements = 4)
  {
    switch (NGUIText.alignment)
    {
      case NGUIText.Alignment.Center:
        float num1 = (float) (((double) NGUIText.rectWidth - (double) printedWidth) * 0.5);
        if ((double) num1 < 0.0)
          break;
        int num2 = Mathf.RoundToInt((float) NGUIText.rectWidth - printedWidth);
        int num3 = Mathf.RoundToInt((float) NGUIText.rectWidth);
        bool flag1 = (num2 & 1) == 1;
        bool flag2 = (num3 & 1) == 1;
        if (flag1 && !flag2 || !flag1 & flag2)
          num1 += 0.5f * NGUIText.fontScale;
        for (int index = indexOffset; index < verts.size; ++index)
          verts.buffer[index].x += num1;
        break;
      case NGUIText.Alignment.Right:
        float num4 = (float) NGUIText.rectWidth - printedWidth;
        if ((double) num4 < 0.0)
          break;
        for (int index = indexOffset; index < verts.size; ++index)
          verts.buffer[index].x += num4;
        break;
      case NGUIText.Alignment.Justified:
        if ((double) printedWidth < (double) NGUIText.rectWidth * 0.64999997615814209 || ((double) NGUIText.rectWidth - (double) printedWidth) * 0.5 < 1.0)
          break;
        int num5 = (verts.size - indexOffset) / elements;
        if (num5 < 1)
          break;
        float num6 = 1f / (float) (num5 - 1);
        float num7 = (float) NGUIText.rectWidth / printedWidth;
        int index1 = indexOffset + elements;
        int num8 = 1;
        while (index1 < verts.size)
        {
          float x1 = verts.buffer[index1].x;
          float x2 = verts.buffer[index1 + elements / 2].x;
          float num9 = x2 - x1;
          double num10 = (double) x1 * (double) num7;
          double num11 = num10 + (double) num9;
          float num12 = x2 * num7;
          float num13 = num12 - num9;
          float num14 = (float) num8 * num6;
          double num15 = (double) num12;
          double num16 = (double) num14;
          float num17 = Mathf.Lerp((float) num11, (float) num15, (float) num16);
          float num18 = Mathf.Round(Mathf.Lerp((float) num10, num13, num14));
          float num19 = Mathf.Round(num17);
          switch (elements)
          {
            case 1:
              verts.buffer[index1++].x = num18;
              break;
            case 2:
              Vector3[] buffer1 = verts.buffer;
              int index2 = index1;
              int num20 = index2 + 1;
              buffer1[index2].x = num18;
              Vector3[] buffer2 = verts.buffer;
              int index3 = num20;
              index1 = index3 + 1;
              buffer2[index3].x = num19;
              break;
            case 4:
              Vector3[] buffer3 = verts.buffer;
              int index4 = index1;
              int num21 = index4 + 1;
              buffer3[index4].x = num18;
              Vector3[] buffer4 = verts.buffer;
              int index5 = num21;
              int num22 = index5 + 1;
              buffer4[index5].x = num18;
              Vector3[] buffer5 = verts.buffer;
              int index6 = num22;
              int num23 = index6 + 1;
              buffer5[index6].x = num19;
              Vector3[] buffer6 = verts.buffer;
              int index7 = num23;
              index1 = index7 + 1;
              buffer6[index7].x = num19;
              break;
          }
          ++num8;
        }
        break;
    }
  }

  public static int GetExactCharacterIndex(
    BetterList<Vector3> verts,
    BetterList<int> indices,
    Vector2 pos)
  {
    for (int i1 = 0; i1 < indices.size; ++i1)
    {
      int i2 = i1 << 1;
      int i3 = i2 + 1;
      float x1 = verts[i2].x;
      if ((double) pos.x >= (double) x1)
      {
        float x2 = verts[i3].x;
        if ((double) pos.x <= (double) x2)
        {
          float y1 = verts[i2].y;
          if ((double) pos.y >= (double) y1)
          {
            float y2 = verts[i3].y;
            if ((double) pos.y <= (double) y2)
              return indices[i1];
          }
        }
      }
    }
    return 0;
  }

  public static int GetApproximateCharacterIndex(
    BetterList<Vector3> verts,
    BetterList<int> indices,
    Vector2 pos)
  {
    float num1 = float.MaxValue;
    float num2 = float.MaxValue;
    int i1 = 0;
    for (int i2 = 0; i2 < verts.size; ++i2)
    {
      float num3 = Mathf.Abs(pos.y - verts[i2].y);
      if ((double) num3 <= (double) num2)
      {
        float num4 = Mathf.Abs(pos.x - verts[i2].x);
        if ((double) num3 < (double) num2)
        {
          num2 = num3;
          num1 = num4;
          i1 = i2;
        }
        else if ((double) num4 < (double) num1)
        {
          num1 = num4;
          i1 = i2;
        }
      }
    }
    return indices[i1];
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  private static bool IsSpace(int ch)
  {
    return ch == 32 /*0x20*/ || ch == 8202 || ch == 8203 || ch == 8201;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static void EndLine(ref StringBuilder s)
  {
    int index = s.Length - 1;
    if (index > 0 && NGUIText.IsSpace((int) s[index]))
      s[index] = '\n';
    else
      s.Append('\n');
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  private static void ReplaceSpaceWithNewline(ref StringBuilder s)
  {
    int index = s.Length - 1;
    if (index <= 0 || !NGUIText.IsSpace((int) s[index]))
      return;
    s[index] = '\n';
  }

  public static Vector2 CalculatePrintedSize(string text)
  {
    Vector2 zero = Vector2.zero;
    if (!string.IsNullOrEmpty(text))
    {
      if (NGUIText.encoding)
        text = NGUIText.StripSymbols(text);
      NGUIText.Prepare(text);
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      int length = text.Length;
      int prev = 0;
      for (int index = 0; index < length; ++index)
      {
        int ch = (int) text[index];
        if (ch == 10)
        {
          if ((double) num1 > (double) num3)
            num3 = num1;
          num1 = 0.0f;
          num2 += NGUIText.finalLineHeight;
        }
        else if (ch >= 32 /*0x20*/)
        {
          BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index, length) : (BMSymbol) null;
          if (symbol == null)
          {
            float glyphWidth = NGUIText.GetGlyphWidth(ch, prev);
            if ((double) glyphWidth != 0.0)
            {
              float num4 = glyphWidth + NGUIText.finalSpacingX;
              if (Mathf.RoundToInt(num1 + num4) > NGUIText.regionWidth)
              {
                if ((double) num1 > (double) num3)
                  num3 = num1 - NGUIText.finalSpacingX;
                num1 = num4;
                num2 += NGUIText.finalLineHeight;
              }
              else
                num1 += num4;
              prev = ch;
            }
          }
          else
          {
            float num5 = NGUIText.finalSpacingX + (float) symbol.advance * NGUIText.fontScale;
            if (Mathf.RoundToInt(num1 + num5) > NGUIText.regionWidth)
            {
              if ((double) num1 > (double) num3)
                num3 = num1 - NGUIText.finalSpacingX;
              num1 = num5;
              num2 += NGUIText.finalLineHeight;
            }
            else
              num1 += num5;
            index += symbol.sequence.Length - 1;
            prev = 0;
          }
        }
      }
      zero.x = (double) num1 > (double) num3 ? num1 - NGUIText.finalSpacingX : num3;
      zero.y = num2 + NGUIText.finalLineHeight;
    }
    return zero;
  }

  public static int CalculateOffsetToFit(string text)
  {
    if (string.IsNullOrEmpty(text) || NGUIText.regionWidth < 1)
      return 0;
    NGUIText.Prepare(text);
    int length1 = text.Length;
    int prev = 0;
    int index1 = 0;
    for (int length2 = text.Length; index1 < length2; ++index1)
    {
      BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index1, length1) : (BMSymbol) null;
      if (symbol == null)
      {
        int ch = (int) text[index1];
        float glyphWidth = NGUIText.GetGlyphWidth(ch, prev);
        if ((double) glyphWidth != 0.0)
          NGUIText.mSizes.Add(NGUIText.finalSpacingX + glyphWidth);
        prev = ch;
      }
      else
      {
        NGUIText.mSizes.Add(NGUIText.finalSpacingX + (float) symbol.advance * NGUIText.fontScale);
        int num = 0;
        for (int index2 = symbol.sequence.Length - 1; num < index2; ++num)
          NGUIText.mSizes.Add(0.0f);
        index1 += symbol.sequence.Length - 1;
        prev = 0;
      }
    }
    float regionWidth = (float) NGUIText.regionWidth;
    int size = NGUIText.mSizes.size;
    while (size > 0 && (double) regionWidth > 0.0)
      regionWidth -= NGUIText.mSizes[--size];
    NGUIText.mSizes.Clear();
    if ((double) regionWidth < 0.0)
      ++size;
    return size;
  }

  public static string GetEndOfLineThatFits(string text)
  {
    int length = text.Length;
    int offsetToFit = NGUIText.CalculateOffsetToFit(text);
    return text.Substring(offsetToFit, length - offsetToFit);
  }

  public static bool WrapText(string text, out string finalText, bool wrapLineColors = false)
  {
    return NGUIText.WrapText(text, out finalText, false, wrapLineColors);
  }

  public static bool WrapText(
    string text,
    out string finalText,
    bool keepCharCount,
    bool wrapLineColors)
  {
    if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 1 || (double) NGUIText.finalLineHeight < 1.0)
    {
      finalText = "";
      return false;
    }
    float num1 = NGUIText.maxLines > 0 ? Mathf.Min((float) NGUIText.regionHeight, NGUIText.finalLineHeight * (float) NGUIText.maxLines) : (float) NGUIText.regionHeight;
    int num2 = Mathf.FloorToInt(Mathf.Min(NGUIText.maxLines > 0 ? (float) NGUIText.maxLines : 1000000f, num1 / NGUIText.finalLineHeight) + 0.01f);
    if (num2 == 0)
    {
      finalText = "";
      return false;
    }
    if (string.IsNullOrEmpty(text))
      text = " ";
    NGUIText.Prepare(text);
    StringBuilder s = new StringBuilder();
    int length1 = text.Length;
    float num3 = (float) NGUIText.regionWidth;
    int startIndex = 0;
    int index1 = 0;
    int num4 = 1;
    int prev = 0;
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = false;
    Color tint = NGUIText.tint;
    int sub = 0;
    bool bold = false;
    bool italic = false;
    bool underline = false;
    bool strike = false;
    bool ignoreColor = false;
    if (!NGUIText.useSymbols)
      wrapLineColors = false;
    if (wrapLineColors)
      NGUIText.mColors.Add(tint);
    for (; index1 < length1; ++index1)
    {
      char ch1 = text[index1];
      if (ch1 > '\u2FFF')
        flag3 = true;
      if (ch1 == '\n')
      {
        if (num4 != num2)
        {
          num3 = (float) NGUIText.regionWidth;
          if (startIndex < index1)
            s.Append(text.Substring(startIndex, index1 - startIndex + 1));
          else
            s.Append(ch1);
          if (wrapLineColors)
          {
            for (int index2 = 0; index2 < NGUIText.mColors.size; ++index2)
              s.Insert(s.Length - 1, "[-]");
            for (int i = 0; i < NGUIText.mColors.size; ++i)
            {
              s.Append("[");
              s.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
              s.Append("]");
            }
          }
          flag1 = true;
          ++num4;
          startIndex = index1 + 1;
          prev = 0;
        }
        else
          break;
      }
      else
      {
        if (NGUIText.encoding)
        {
          if (!wrapLineColors)
          {
            if (NGUIText.ParseSymbol(text, ref index1))
            {
              --index1;
              continue;
            }
          }
          else if (NGUIText.ParseSymbol(text, ref index1, NGUIText.mColors, NGUIText.premultiply, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
          {
            Color color;
            if (ignoreColor)
            {
              color = NGUIText.mColors[NGUIText.mColors.size - 1];
              color.a *= NGUIText.mAlpha * NGUIText.tint.a;
            }
            else
            {
              color = Color.op_Multiply(NGUIText.tint, NGUIText.mColors[NGUIText.mColors.size - 1]);
              color.a *= NGUIText.mAlpha;
            }
            int i = 0;
            for (int index3 = NGUIText.mColors.size - 2; i < index3; ++i)
              color.a *= NGUIText.mColors[i].a;
            --index1;
            continue;
          }
        }
        BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index1, length1) : (BMSymbol) null;
        float num5;
        if (symbol == null)
        {
          float glyphWidth = NGUIText.GetGlyphWidth((int) ch1, prev);
          if ((double) glyphWidth != 0.0)
            num5 = NGUIText.finalSpacingX + glyphWidth;
          else
            continue;
        }
        else
          num5 = NGUIText.finalSpacingX + (float) symbol.advance * NGUIText.fontScale;
        num3 -= num5;
        if (NGUIText.IsSpace((int) ch1) && !flag3 && startIndex < index1)
        {
          int length2 = index1 - startIndex + 1;
          if (num4 == num2 && (double) num3 <= 0.0 && index1 < length1)
          {
            char ch2 = text[index1];
            if (ch2 < ' ' || NGUIText.IsSpace((int) ch2))
              --length2;
          }
          s.Append(text.Substring(startIndex, length2));
          flag1 = false;
          startIndex = index1 + 1;
        }
        if (Mathf.RoundToInt(num3) < 0)
        {
          if (flag1 || num4 == num2)
          {
            s.Append(text.Substring(startIndex, Mathf.Max(0, index1 - startIndex)));
            bool flag4 = NGUIText.IsSpace((int) ch1);
            if (!flag4 && !flag3)
              flag2 = false;
            if (wrapLineColors && NGUIText.mColors.size > 0)
              s.Append("[-]");
            if (num4++ == num2)
            {
              startIndex = index1;
              break;
            }
            if (keepCharCount)
              NGUIText.ReplaceSpaceWithNewline(ref s);
            else
              NGUIText.EndLine(ref s);
            if (wrapLineColors)
            {
              for (int index4 = 0; index4 < NGUIText.mColors.size; ++index4)
                s.Insert(s.Length - 1, "[-]");
              for (int i = 0; i < NGUIText.mColors.size; ++i)
              {
                s.Append("[");
                s.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
                s.Append("]");
              }
            }
            flag1 = true;
            if (flag4)
            {
              startIndex = index1 + 1;
              num3 = (float) NGUIText.regionWidth;
            }
            else
            {
              startIndex = index1;
              num3 = (float) NGUIText.regionWidth - num5;
            }
            prev = 0;
          }
          else
          {
            flag1 = true;
            num3 = (float) NGUIText.regionWidth;
            index1 = startIndex - 1;
            prev = 0;
            if (num4++ != num2)
            {
              if (keepCharCount)
                NGUIText.ReplaceSpaceWithNewline(ref s);
              else
                NGUIText.EndLine(ref s);
              if (wrapLineColors)
              {
                for (int index5 = 0; index5 < NGUIText.mColors.size; ++index5)
                  s.Insert(s.Length - 1, "[-]");
                for (int i = 0; i < NGUIText.mColors.size; ++i)
                {
                  s.Append("[");
                  s.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
                  s.Append("]");
                }
                continue;
              }
              continue;
            }
            break;
          }
        }
        else
          prev = (int) ch1;
        if (symbol != null)
        {
          index1 += symbol.length - 1;
          prev = 0;
        }
      }
    }
    if (startIndex < index1)
      s.Append(text.Substring(startIndex, index1 - startIndex));
    if (wrapLineColors && NGUIText.mColors.size > 0)
      s.Append("[-]");
    finalText = s.ToString();
    NGUIText.mColors.Clear();
    if (!flag2)
      return false;
    return index1 == length1 || num4 <= Mathf.Min(NGUIText.maxLines, num2);
  }

  public static void Print(
    string text,
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    if (string.IsNullOrEmpty(text))
      return;
    int size1 = verts.size;
    NGUIText.Prepare(text);
    NGUIText.mColors.Add(Color.white);
    NGUIText.mAlpha = 1f;
    int prev = 0;
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    double finalSize = (double) NGUIText.finalSize;
    Color color1 = Color.op_Multiply(NGUIText.tint, NGUIText.gradientBottom);
    Color color2 = Color.op_Multiply(NGUIText.tint, NGUIText.gradientTop);
    Color32 color32_1 = Color32.op_Implicit(NGUIText.tint);
    int length = text.Length;
    Rect rect = new Rect();
    float num4 = 0.0f;
    float num5 = 0.0f;
    double pixelDensity = (double) NGUIText.pixelDensity;
    float num6 = (float) (finalSize * pixelDensity);
    bool flag = false;
    int sub = 0;
    bool bold = false;
    bool italic = false;
    bool underline = false;
    bool strike = false;
    bool ignoreColor = false;
    float num7 = 0.0f;
    if (Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null))
    {
      rect = NGUIText.bitmapFont.uvRect;
      num4 = ((Rect) ref rect).width / (float) NGUIText.bitmapFont.texWidth;
      num5 = ((Rect) ref rect).height / (float) NGUIText.bitmapFont.texHeight;
    }
    for (int index1 = 0; index1 < length; ++index1)
    {
      int ch = (int) text[index1];
      float num8 = num1;
      if (ch == 10)
      {
        if ((double) num1 > (double) num3)
          num3 = num1;
        if (NGUIText.alignment != NGUIText.Alignment.Left)
        {
          NGUIText.Align(verts, size1, num1 - NGUIText.finalSpacingX);
          size1 = verts.size;
        }
        num1 = 0.0f;
        num2 += NGUIText.finalLineHeight;
        prev = 0;
      }
      else if (ch < 32 /*0x20*/)
        prev = ch;
      else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref index1, NGUIText.mColors, NGUIText.premultiply, ref sub, ref bold, ref italic, ref underline, ref strike, ref ignoreColor))
      {
        Color color3;
        if (ignoreColor)
        {
          color3 = NGUIText.mColors[NGUIText.mColors.size - 1];
          color3.a *= NGUIText.mAlpha * NGUIText.tint.a;
        }
        else
        {
          color3 = Color.op_Multiply(NGUIText.tint, NGUIText.mColors[NGUIText.mColors.size - 1]);
          color3.a *= NGUIText.mAlpha;
        }
        color32_1 = Color32.op_Implicit(color3);
        int i = 0;
        for (int index2 = NGUIText.mColors.size - 2; i < index2; ++i)
          color3.a *= NGUIText.mColors[i].a;
        if (NGUIText.gradient)
        {
          color1 = Color.op_Multiply(NGUIText.gradientBottom, color3);
          color2 = Color.op_Multiply(NGUIText.gradientTop, color3);
        }
        --index1;
      }
      else
      {
        BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index1, length) : (BMSymbol) null;
        if (symbol != null)
        {
          float num9 = num1 + (float) symbol.offsetX * NGUIText.fontScale;
          float num10 = num9 + (float) symbol.width * NGUIText.fontScale;
          float num11 = (float) -((double) num2 + (double) symbol.offsetY * (double) NGUIText.fontScale);
          float num12 = num11 - (float) symbol.height * NGUIText.fontScale;
          if (Mathf.RoundToInt(num1 + (float) symbol.advance * NGUIText.fontScale) > NGUIText.regionWidth)
          {
            if ((double) num1 == 0.0)
              return;
            if (NGUIText.alignment != NGUIText.Alignment.Left && size1 < verts.size)
            {
              NGUIText.Align(verts, size1, num1 - NGUIText.finalSpacingX);
              size1 = verts.size;
            }
            num9 -= num1;
            num10 -= num1;
            num12 -= NGUIText.finalLineHeight;
            num11 -= NGUIText.finalLineHeight;
            num1 = 0.0f;
            num2 += NGUIText.finalLineHeight;
            num7 = 0.0f;
          }
          verts.Add(new Vector3(num9, num12));
          verts.Add(new Vector3(num9, num11));
          verts.Add(new Vector3(num10, num11));
          verts.Add(new Vector3(num10, num12));
          num1 += NGUIText.finalSpacingX + (float) symbol.advance * NGUIText.fontScale;
          index1 += symbol.length - 1;
          prev = 0;
          if (uvs != null)
          {
            Rect uvRect = symbol.uvRect;
            float xMin = ((Rect) ref uvRect).xMin;
            float yMin = ((Rect) ref uvRect).yMin;
            float xMax = ((Rect) ref uvRect).xMax;
            float yMax = ((Rect) ref uvRect).yMax;
            uvs.Add(new Vector2(xMin, yMin));
            uvs.Add(new Vector2(xMin, yMax));
            uvs.Add(new Vector2(xMax, yMax));
            uvs.Add(new Vector2(xMax, yMin));
          }
          if (cols != null)
          {
            if (NGUIText.symbolStyle == NGUIText.SymbolStyle.Colored)
            {
              for (int index3 = 0; index3 < 4; ++index3)
                cols.Add(color32_1);
            }
            else
            {
              Color32 color32_2 = Color32.op_Implicit(Color.white);
              color32_2.a = color32_1.a;
              for (int index4 = 0; index4 < 4; ++index4)
                cols.Add(color32_2);
            }
          }
        }
        else
        {
          NGUIText.GlyphInfo glyph1 = NGUIText.GetGlyph(ch, prev);
          if (glyph1 != null)
          {
            prev = ch;
            if (sub != 0)
            {
              glyph1.v0.x *= 0.75f;
              glyph1.v0.y *= 0.75f;
              glyph1.v1.x *= 0.75f;
              glyph1.v1.y *= 0.75f;
              if (sub == 1)
              {
                glyph1.v0.y -= (float) ((double) NGUIText.fontScale * (double) NGUIText.fontSize * 0.40000000596046448);
                glyph1.v1.y -= (float) ((double) NGUIText.fontScale * (double) NGUIText.fontSize * 0.40000000596046448);
              }
              else
              {
                glyph1.v0.y += (float) ((double) NGUIText.fontScale * (double) NGUIText.fontSize * 0.05000000074505806);
                glyph1.v1.y += (float) ((double) NGUIText.fontScale * (double) NGUIText.fontSize * 0.05000000074505806);
              }
            }
            float num13 = glyph1.v0.x + num1;
            float num14 = glyph1.v0.y - num2;
            float num15 = glyph1.v1.x + num1;
            float num16 = glyph1.v1.y - num2;
            float advance = glyph1.advance;
            if ((double) NGUIText.finalSpacingX < 0.0)
              advance += NGUIText.finalSpacingX;
            if (Mathf.RoundToInt(num1 + advance) > NGUIText.regionWidth)
            {
              if ((double) num1 == 0.0)
                return;
              if (NGUIText.alignment != NGUIText.Alignment.Left && size1 < verts.size)
              {
                NGUIText.Align(verts, size1, num1 - NGUIText.finalSpacingX);
                size1 = verts.size;
              }
              num13 -= num1;
              num15 -= num1;
              num14 -= NGUIText.finalLineHeight;
              num16 -= NGUIText.finalLineHeight;
              num1 = 0.0f;
              num2 += NGUIText.finalLineHeight;
              num8 = 0.0f;
            }
            if (NGUIText.IsSpace(ch))
            {
              if (underline)
                ch = 95;
              else if (strike)
                ch = 45;
            }
            num1 += sub == 0 ? NGUIText.finalSpacingX + glyph1.advance : (float) (((double) NGUIText.finalSpacingX + (double) glyph1.advance) * 0.75);
            if (!NGUIText.IsSpace(ch))
            {
              if (uvs != null)
              {
                if (Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null))
                {
                  glyph1.u0.x = ((Rect) ref rect).xMin + num4 * glyph1.u0.x;
                  glyph1.u2.x = ((Rect) ref rect).xMin + num4 * glyph1.u2.x;
                  glyph1.u0.y = ((Rect) ref rect).yMax - num5 * glyph1.u0.y;
                  glyph1.u2.y = ((Rect) ref rect).yMax - num5 * glyph1.u2.y;
                  glyph1.u1.x = glyph1.u0.x;
                  glyph1.u1.y = glyph1.u2.y;
                  glyph1.u3.x = glyph1.u2.x;
                  glyph1.u3.y = glyph1.u0.y;
                }
                int num17 = 0;
                for (int index5 = bold ? 4 : 1; num17 < index5; ++num17)
                {
                  uvs.Add(glyph1.u0);
                  uvs.Add(glyph1.u1);
                  uvs.Add(glyph1.u2);
                  uvs.Add(glyph1.u3);
                }
              }
              if (cols != null)
              {
                if (glyph1.channel == 0 || glyph1.channel == 15)
                {
                  if (NGUIText.gradient)
                  {
                    float num18 = num6 + glyph1.v0.y / NGUIText.fontScale;
                    float num19 = num6 + glyph1.v1.y / NGUIText.fontScale;
                    float num20 = num18 / num6;
                    float num21 = num19 / num6;
                    NGUIText.s_c0 = Color32.op_Implicit(Color.Lerp(color1, color2, num20));
                    NGUIText.s_c1 = Color32.op_Implicit(Color.Lerp(color1, color2, num21));
                    int num22 = 0;
                    for (int index6 = bold ? 4 : 1; num22 < index6; ++num22)
                    {
                      cols.Add(NGUIText.s_c0);
                      cols.Add(NGUIText.s_c1);
                      cols.Add(NGUIText.s_c1);
                      cols.Add(NGUIText.s_c0);
                    }
                  }
                  else
                  {
                    int num23 = 0;
                    for (int index7 = bold ? 16 /*0x10*/ : 4; num23 < index7; ++num23)
                      cols.Add(color32_1);
                  }
                }
                else
                {
                  Color color4 = Color.op_Multiply(Color32.op_Implicit(color32_1), 0.49f);
                  switch (glyph1.channel)
                  {
                    case 1:
                      color4.b += 0.51f;
                      break;
                    case 2:
                      color4.g += 0.51f;
                      break;
                    case 4:
                      color4.r += 0.51f;
                      break;
                    case 8:
                      color4.a += 0.51f;
                      break;
                  }
                  Color32 color32_3 = Color32.op_Implicit(color4);
                  int num24 = 0;
                  for (int index8 = bold ? 16 /*0x10*/ : 4; num24 < index8; ++num24)
                    cols.Add(color32_3);
                }
              }
              if (!bold)
              {
                if (!italic)
                {
                  verts.Add(new Vector3(num13, num14));
                  verts.Add(new Vector3(num13, num16));
                  verts.Add(new Vector3(num15, num16));
                  verts.Add(new Vector3(num15, num14));
                }
                else
                {
                  float num25 = (float) ((double) NGUIText.fontSize * 0.10000000149011612 * (((double) num16 - (double) num14) / (double) NGUIText.fontSize));
                  verts.Add(new Vector3(num13 - num25, num14));
                  verts.Add(new Vector3(num13 + num25, num16));
                  verts.Add(new Vector3(num15 + num25, num16));
                  verts.Add(new Vector3(num15 - num25, num14));
                }
              }
              else
              {
                for (int index9 = 0; index9 < 4; ++index9)
                {
                  float num26 = NGUIText.mBoldOffset[index9 * 2];
                  float num27 = NGUIText.mBoldOffset[index9 * 2 + 1];
                  float num28 = italic ? (float) ((double) NGUIText.fontSize * 0.10000000149011612 * (((double) num16 - (double) num14) / (double) NGUIText.fontSize)) : 0.0f;
                  verts.Add(new Vector3(num13 + num26 - num28, num14 + num27));
                  verts.Add(new Vector3(num13 + num26 + num28, num16 + num27));
                  verts.Add(new Vector3(num15 + num26 + num28, num16 + num27));
                  verts.Add(new Vector3(num15 + num26 - num28, num14 + num27));
                }
              }
              if (underline | strike)
              {
                NGUIText.GlyphInfo glyph2 = NGUIText.GetGlyph(strike ? 45 : 95, prev);
                if (glyph2 != null)
                {
                  if (uvs != null)
                  {
                    if (Object.op_Inequality((Object) NGUIText.bitmapFont, (Object) null))
                    {
                      glyph2.u0.x = ((Rect) ref rect).xMin + num4 * glyph2.u0.x;
                      glyph2.u2.x = ((Rect) ref rect).xMin + num4 * glyph2.u2.x;
                      glyph2.u0.y = ((Rect) ref rect).yMax - num5 * glyph2.u0.y;
                      glyph2.u2.y = ((Rect) ref rect).yMax - num5 * glyph2.u2.y;
                    }
                    float num29 = (float) (((double) glyph2.u0.x + (double) glyph2.u2.x) * 0.5);
                    int num30 = 0;
                    for (int index10 = bold ? 4 : 1; num30 < index10; ++num30)
                    {
                      uvs.Add(new Vector2(num29, glyph2.u0.y));
                      uvs.Add(new Vector2(num29, glyph2.u2.y));
                      uvs.Add(new Vector2(num29, glyph2.u2.y));
                      uvs.Add(new Vector2(num29, glyph2.u0.y));
                    }
                  }
                  float num31;
                  float num32;
                  if (flag & strike)
                  {
                    num31 = (float) ((-(double) num2 + (double) glyph2.v0.y) * 0.75);
                    num32 = (float) ((-(double) num2 + (double) glyph2.v1.y) * 0.75);
                  }
                  else
                  {
                    num31 = -num2 + glyph2.v0.y;
                    num32 = -num2 + glyph2.v1.y;
                  }
                  if (bold)
                  {
                    for (int index11 = 0; index11 < 4; ++index11)
                    {
                      float num33 = NGUIText.mBoldOffset[index11 * 2];
                      float num34 = NGUIText.mBoldOffset[index11 * 2 + 1];
                      verts.Add(new Vector3(num8 + num33, num31 + num34));
                      verts.Add(new Vector3(num8 + num33, num32 + num34));
                      verts.Add(new Vector3(num1 + num33, num32 + num34));
                      verts.Add(new Vector3(num1 + num33, num31 + num34));
                    }
                  }
                  else
                  {
                    verts.Add(new Vector3(num8, num31));
                    verts.Add(new Vector3(num8, num32));
                    verts.Add(new Vector3(num1, num32));
                    verts.Add(new Vector3(num1, num31));
                  }
                  if (NGUIText.gradient)
                  {
                    float num35 = num6 + glyph2.v0.y / NGUIText.fontScale;
                    float num36 = num6 + glyph2.v1.y / NGUIText.fontScale;
                    float num37 = num35 / num6;
                    float num38 = num36 / num6;
                    NGUIText.s_c0 = Color32.op_Implicit(Color.Lerp(color1, color2, num37));
                    NGUIText.s_c1 = Color32.op_Implicit(Color.Lerp(color1, color2, num38));
                    int num39 = 0;
                    for (int index12 = bold ? 4 : 1; num39 < index12; ++num39)
                    {
                      cols.Add(NGUIText.s_c0);
                      cols.Add(NGUIText.s_c1);
                      cols.Add(NGUIText.s_c1);
                      cols.Add(NGUIText.s_c0);
                    }
                  }
                  else
                  {
                    int num40 = 0;
                    for (int index13 = bold ? 16 /*0x10*/ : 4; num40 < index13; ++num40)
                      cols.Add(color32_1);
                  }
                }
              }
            }
          }
        }
      }
    }
    if (NGUIText.alignment != NGUIText.Alignment.Left && size1 < verts.size)
    {
      NGUIText.Align(verts, size1, num1 - NGUIText.finalSpacingX);
      int size2 = verts.size;
    }
    NGUIText.mColors.Clear();
  }

  public static void PrintApproximateCharacterPositions(
    string text,
    BetterList<Vector3> verts,
    BetterList<int> indices)
  {
    if (string.IsNullOrEmpty(text))
      text = " ";
    NGUIText.Prepare(text);
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    float num4 = (float) ((double) NGUIText.fontSize * (double) NGUIText.fontScale * 0.5);
    int length = text.Length;
    int size = verts.size;
    int prev = 0;
    for (int index = 0; index < length; ++index)
    {
      int ch = (int) text[index];
      verts.Add(new Vector3(num1, -num2 - num4));
      indices.Add(index);
      if (ch == 10)
      {
        if ((double) num1 > (double) num3)
          num3 = num1;
        if (NGUIText.alignment != NGUIText.Alignment.Left)
        {
          NGUIText.Align(verts, size, num1 - NGUIText.finalSpacingX, 1);
          size = verts.size;
        }
        num1 = 0.0f;
        num2 += NGUIText.finalLineHeight;
        prev = 0;
      }
      else if (ch < 32 /*0x20*/)
        prev = 0;
      else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref index))
      {
        --index;
      }
      else
      {
        BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index, length) : (BMSymbol) null;
        if (symbol == null)
        {
          float glyphWidth = NGUIText.GetGlyphWidth(ch, prev);
          if ((double) glyphWidth != 0.0)
          {
            float num5 = glyphWidth + NGUIText.finalSpacingX;
            if (Mathf.RoundToInt(num1 + num5) > NGUIText.regionWidth)
            {
              if ((double) num1 == 0.0)
                return;
              if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
              {
                NGUIText.Align(verts, size, num1 - NGUIText.finalSpacingX, 1);
                size = verts.size;
              }
              num1 = num5;
              num2 += NGUIText.finalLineHeight;
            }
            else
              num1 += num5;
            verts.Add(new Vector3(num1, -num2 - num4));
            indices.Add(index + 1);
            prev = ch;
          }
        }
        else
        {
          float num6 = (float) symbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;
          if (Mathf.RoundToInt(num1 + num6) > NGUIText.regionWidth)
          {
            if ((double) num1 == 0.0)
              return;
            if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
            {
              NGUIText.Align(verts, size, num1 - NGUIText.finalSpacingX, 1);
              size = verts.size;
            }
            num1 = num6;
            num2 += NGUIText.finalLineHeight;
          }
          else
            num1 += num6;
          verts.Add(new Vector3(num1, -num2 - num4));
          indices.Add(index + 1);
          index += symbol.sequence.Length - 1;
          prev = 0;
        }
      }
    }
    if (NGUIText.alignment == NGUIText.Alignment.Left || size >= verts.size)
      return;
    NGUIText.Align(verts, size, num1 - NGUIText.finalSpacingX, 1);
  }

  public static void PrintExactCharacterPositions(
    string text,
    BetterList<Vector3> verts,
    BetterList<int> indices)
  {
    if (string.IsNullOrEmpty(text))
      text = " ";
    NGUIText.Prepare(text);
    float num1 = (float) NGUIText.fontSize * NGUIText.fontScale;
    float num2 = 0.0f;
    float num3 = 0.0f;
    float num4 = 0.0f;
    int length = text.Length;
    int size = verts.size;
    int prev = 0;
    for (int index = 0; index < length; ++index)
    {
      int ch = (int) text[index];
      if (ch == 10)
      {
        if ((double) num2 > (double) num4)
          num4 = num2;
        if (NGUIText.alignment != NGUIText.Alignment.Left)
        {
          NGUIText.Align(verts, size, num2 - NGUIText.finalSpacingX, 2);
          size = verts.size;
        }
        num2 = 0.0f;
        num3 += NGUIText.finalLineHeight;
        prev = 0;
      }
      else if (ch < 32 /*0x20*/)
        prev = 0;
      else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref index))
      {
        --index;
      }
      else
      {
        BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index, length) : (BMSymbol) null;
        if (symbol == null)
        {
          float glyphWidth = NGUIText.GetGlyphWidth(ch, prev);
          if ((double) glyphWidth != 0.0)
          {
            float num5 = glyphWidth + NGUIText.finalSpacingX;
            if (Mathf.RoundToInt(num2 + num5) > NGUIText.regionWidth)
            {
              if ((double) num2 == 0.0)
                return;
              if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
              {
                NGUIText.Align(verts, size, num2 - NGUIText.finalSpacingX, 2);
                size = verts.size;
              }
              num2 = 0.0f;
              num3 += NGUIText.finalLineHeight;
              prev = 0;
              --index;
            }
            else
            {
              indices.Add(index);
              verts.Add(new Vector3(num2, -num3 - num1));
              verts.Add(new Vector3(num2 + num5, -num3));
              prev = ch;
              num2 += num5;
            }
          }
        }
        else
        {
          float num6 = (float) symbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;
          if (Mathf.RoundToInt(num2 + num6) > NGUIText.regionWidth)
          {
            if ((double) num2 == 0.0)
              return;
            if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
            {
              NGUIText.Align(verts, size, num2 - NGUIText.finalSpacingX, 2);
              size = verts.size;
            }
            num2 = 0.0f;
            num3 += NGUIText.finalLineHeight;
            prev = 0;
            --index;
          }
          else
          {
            indices.Add(index);
            verts.Add(new Vector3(num2, -num3 - num1));
            verts.Add(new Vector3(num2 + num6, -num3));
            index += symbol.sequence.Length - 1;
            num2 += num6;
            prev = 0;
          }
        }
      }
    }
    if (NGUIText.alignment == NGUIText.Alignment.Left || size >= verts.size)
      return;
    NGUIText.Align(verts, size, num2 - NGUIText.finalSpacingX, 2);
  }

  public static void PrintCaretAndSelection(
    string text,
    int start,
    int end,
    BetterList<Vector3> caret,
    BetterList<Vector3> highlight)
  {
    if (string.IsNullOrEmpty(text))
      text = " ";
    NGUIText.Prepare(text);
    int num1 = end;
    if (start > end)
    {
      end = start;
      start = num1;
    }
    float num2 = 0.0f;
    float num3 = 0.0f;
    float num4 = 0.0f;
    float num5 = (float) NGUIText.fontSize * NGUIText.fontScale;
    int size1 = caret != null ? caret.size : 0;
    int size2 = highlight != null ? highlight.size : 0;
    int length = text.Length;
    int index = 0;
    int prev = 0;
    bool flag1 = false;
    bool flag2 = false;
    Vector2 zero1 = Vector2.zero;
    Vector2 zero2 = Vector2.zero;
    for (; index < length; ++index)
    {
      if (caret != null && !flag2 && num1 <= index)
      {
        flag2 = true;
        caret.Add(new Vector3(num2 - 1f, -num3 - num5));
        caret.Add(new Vector3(num2 - 1f, -num3));
        caret.Add(new Vector3(num2 + 1f, -num3));
        caret.Add(new Vector3(num2 + 1f, -num3 - num5));
      }
      int ch = (int) text[index];
      if (ch == 10)
      {
        if ((double) num2 > (double) num4)
          num4 = num2;
        if (caret != null & flag2)
        {
          if (NGUIText.alignment != NGUIText.Alignment.Left)
            NGUIText.Align(caret, size1, num2 - NGUIText.finalSpacingX);
          caret = (BetterList<Vector3>) null;
        }
        if (highlight != null)
        {
          if (flag1)
          {
            flag1 = false;
            highlight.Add(Vector2.op_Implicit(zero2));
            highlight.Add(Vector2.op_Implicit(zero1));
          }
          else if (start <= index && end > index)
          {
            highlight.Add(new Vector3(num2, -num3 - num5));
            highlight.Add(new Vector3(num2, -num3));
            highlight.Add(new Vector3(num2 + 2f, -num3));
            highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
          }
          if (NGUIText.alignment != NGUIText.Alignment.Left && size2 < highlight.size)
          {
            NGUIText.Align(highlight, size2, num2 - NGUIText.finalSpacingX);
            size2 = highlight.size;
          }
        }
        num2 = 0.0f;
        num3 += NGUIText.finalLineHeight;
        prev = 0;
      }
      else if (ch < 32 /*0x20*/)
        prev = 0;
      else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref index))
      {
        --index;
      }
      else
      {
        BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, index, length) : (BMSymbol) null;
        float num6 = symbol != null ? (float) symbol.advance * NGUIText.fontScale : NGUIText.GetGlyphWidth(ch, prev);
        if ((double) num6 != 0.0)
        {
          float num7 = num2;
          float num8 = num2 + num6;
          float num9 = -num3 - num5;
          float num10 = -num3;
          if (Mathf.RoundToInt(num8 + NGUIText.finalSpacingX) > NGUIText.regionWidth)
          {
            if ((double) num2 == 0.0)
              return;
            if ((double) num2 > (double) num4)
              num4 = num2;
            if (caret != null & flag2)
            {
              if (NGUIText.alignment != NGUIText.Alignment.Left)
                NGUIText.Align(caret, size1, num2 - NGUIText.finalSpacingX);
              caret = (BetterList<Vector3>) null;
            }
            if (highlight != null)
            {
              if (flag1)
              {
                flag1 = false;
                highlight.Add(Vector2.op_Implicit(zero2));
                highlight.Add(Vector2.op_Implicit(zero1));
              }
              else if (start <= index && end > index)
              {
                highlight.Add(new Vector3(num2, -num3 - num5));
                highlight.Add(new Vector3(num2, -num3));
                highlight.Add(new Vector3(num2 + 2f, -num3));
                highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
              }
              if (NGUIText.alignment != NGUIText.Alignment.Left && size2 < highlight.size)
              {
                NGUIText.Align(highlight, size2, num2 - NGUIText.finalSpacingX);
                size2 = highlight.size;
              }
            }
            num7 -= num2;
            num8 -= num2;
            num9 -= NGUIText.finalLineHeight;
            num10 -= NGUIText.finalLineHeight;
            num2 = 0.0f;
            num3 += NGUIText.finalLineHeight;
          }
          num2 += num6 + NGUIText.finalSpacingX;
          if (highlight != null)
          {
            if (start > index || end <= index)
            {
              if (flag1)
              {
                flag1 = false;
                highlight.Add(Vector2.op_Implicit(zero2));
                highlight.Add(Vector2.op_Implicit(zero1));
              }
            }
            else if (!flag1)
            {
              flag1 = true;
              highlight.Add(new Vector3(num7, num9));
              highlight.Add(new Vector3(num7, num10));
            }
          }
          // ISSUE: explicit constructor call
          ((Vector2) ref zero1).\u002Ector(num8, num9);
          // ISSUE: explicit constructor call
          ((Vector2) ref zero2).\u002Ector(num8, num10);
          prev = ch;
        }
      }
    }
    if (caret != null)
    {
      if (!flag2)
      {
        caret.Add(new Vector3(num2 - 1f, -num3 - num5));
        caret.Add(new Vector3(num2 - 1f, -num3));
        caret.Add(new Vector3(num2 + 1f, -num3));
        caret.Add(new Vector3(num2 + 1f, -num3 - num5));
      }
      if (NGUIText.alignment != NGUIText.Alignment.Left)
        NGUIText.Align(caret, size1, num2 - NGUIText.finalSpacingX);
    }
    if (highlight == null)
      return;
    if (flag1)
    {
      highlight.Add(Vector2.op_Implicit(zero2));
      highlight.Add(Vector2.op_Implicit(zero1));
    }
    else if (start < index && end == index)
    {
      highlight.Add(new Vector3(num2, -num3 - num5));
      highlight.Add(new Vector3(num2, -num3));
      highlight.Add(new Vector3(num2 + 2f, -num3));
      highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
    }
    if (NGUIText.alignment == NGUIText.Alignment.Left || size2 >= highlight.size)
      return;
    NGUIText.Align(highlight, size2, num2 - NGUIText.finalSpacingX);
  }

  public enum Alignment
  {
    Automatic,
    Left,
    Center,
    Right,
    Justified,
  }

  public enum SymbolStyle
  {
    None,
    Normal,
    Colored,
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
}
