// Decompiled with JetBrains decompiler
// Type: ResolutionFixInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class ResolutionFixInfo
{
  public string Name;
  public GameObject PanelFix;
  public int DepthParent = 1;
  public FixedPanelAction FixedPanelAction = FixedPanelAction.FIX_SIZE;
  public List<FixOffsetPosition> FixOffsetHeightPosition;
  public List<string> LockPosition;
  public List<string> LockScaleUI;
  public List<string> ScaleUIChild;
  public List<LockAnchorPath> LockAnchorRoot;
  public List<string> ListUpdateAnchor;
  public List<string> ListObjectFix;
  public List<string> UIStaticUnLock;
  public string Note;
  public string Path = "";
  public bool IsLockMyObj;
  public bool IsEditMain;
  public bool IsEditOffsetHeighPosition;
  public bool IsEditLockPosition;
  public bool IsEditLockScale;
  public bool IsEditScaleUIChild;
  public bool IsObjectFix;
  public bool IsLockAnchorRoot;
  public bool IsUIStaticUnLock;
  public bool IsUpdateAnchorAfterFix;

  public bool IsAdd
  {
    get
    {
      return Object.op_Inequality((Object) this.PanelFix, (Object) null) && Object.op_Implicit((Object) this.PanelFix.GetComponent<FixedPanelNGUI>());
    }
  }
}
