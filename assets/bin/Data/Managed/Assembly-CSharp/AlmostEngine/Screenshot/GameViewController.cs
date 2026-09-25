// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.GameViewController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public static class GameViewController
{
  public static void SaveCurrentGameViewSize()
  {
    Debug.LogError((object) "GAMEVIEW_RESIZING capture mode is only available  for Editor and Windows Standalone.");
  }

  public static void SetGameViewSize(int width, int height)
  {
    Debug.LogError((object) "GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");
  }

  public static void RestoreGameViewSize()
  {
    Debug.LogError((object) "GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");
  }

  public static Vector2 GetCurrentGameViewSize()
  {
    return new Vector2((float) Screen.width, (float) Screen.height);
  }
}
