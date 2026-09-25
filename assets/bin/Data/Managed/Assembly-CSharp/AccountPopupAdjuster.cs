// Decompiled with JetBrains decompiler
// Type: AccountPopupAdjuster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class AccountPopupAdjuster : GameSection
{
  protected List<string> popAdjustBeforeList;

  public string PopupTextAdjust(UILabel lbl, string pop_text)
  {
    if (this.popAdjustBeforeList == null)
      this.popAdjustBeforeList = new List<string>();
    this.popAdjustBeforeList.Add(pop_text);
    char[] charArray1 = pop_text.ToCharArray();
    Array.Reverse((Array) charArray1);
    string text = new string(charArray1);
    int offsetToFit = lbl.CalculateOffsetToFit(text);
    string empty = string.Empty;
    string str;
    if (offsetToFit > 0)
    {
      char[] charArray2 = ("…" + text.Substring(offsetToFit - 1)).ToCharArray();
      Array.Reverse((Array) charArray2);
      str = new string(charArray2);
    }
    else
      str = pop_text;
    return str;
  }

  public string GetAdjustBeforeText(int index)
  {
    return this.popAdjustBeforeList == null || this.popAdjustBeforeList.Count <= index || index < 0 ? string.Empty : this.popAdjustBeforeList[index];
  }
}
