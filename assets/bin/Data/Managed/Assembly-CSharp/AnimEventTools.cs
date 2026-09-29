// Decompiled with JetBrains decompiler
// Type: AnimEventTools
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimEventTools : MonoBehaviour
{
  public AnimEventTools.TOOL_MODE toolMode;
  public bool targetAll = true;
  public AnimEventData animEventData;
  public AnimEventFormat.ID eventID;

  public enum TOOL_MODE
  {
    EVENT_ID_CHECK,
    EVENT_ID_DELETE,
  }
}
