// Decompiled with JetBrains decompiler
// Type: ScreenSafeArea
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

#nullable disable
public static class ScreenSafeArea
{
  private static bool successGetSafeArea;
  private static EdgeInsets edgeInsets;

  private static string _getSafeArea() => "";

  public static bool HasSafeArea()
  {
    ScreenSafeArea.syncSafeAreaData();
    return ScreenSafeArea.successGetSafeArea && ScreenSafeArea.edgeInsets != null && !ScreenSafeArea.edgeInsets.IsZero();
  }

  public static EdgeInsets GetSafeArea()
  {
    ScreenSafeArea.syncSafeAreaData();
    return ScreenSafeArea.edgeInsets;
  }

  private static void syncSafeAreaData()
  {
    string safeArea = ScreenSafeArea._getSafeArea();
    if (!string.IsNullOrEmpty(safeArea) && ScreenSafeArea.ConvertEdgeInsetsFromString(safeArea))
      ScreenSafeArea.successGetSafeArea = true;
    else
      ScreenSafeArea.successGetSafeArea = false;
  }

  private static bool ConvertEdgeInsetsFromString(string str)
  {
    str = str.Replace("{", "").Replace("}", "");
    string[] strArray = str.Split(',');
    float result1;
    float result2;
    float result3;
    float result4;
    if (strArray.Length == 4 && float.TryParse(strArray[0], out result1) && float.TryParse(strArray[1], out result2) && float.TryParse(strArray[2], out result3) && float.TryParse(strArray[3], out result4))
    {
      if (ScreenSafeArea.edgeInsets == null)
        ScreenSafeArea.edgeInsets = new EdgeInsets(result1, result2, result3, result4);
      else
        ScreenSafeArea.edgeInsets.Set(result1, result2, result3, result4);
      return true;
    }
    ScreenSafeArea.edgeInsets = (EdgeInsets) null;
    return false;
  }
}
