// Decompiled with JetBrains decompiler
// Type: QuestUnlockDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestUnlockDialog : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    this.PlayTween((Enum) QuestUnlockDialog.UI.OBJ_UNLOCK_PORTAL_ROOT);
  }

  protected enum UI
  {
    OBJ_UNLOCK_PORTAL_ROOT,
  }
}
