// Decompiled with JetBrains decompiler
// Type: UniqueStatusScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UniqueStatusScene : GameSection
{
  public override void Initialize()
  {
    RenderTargetCacher component = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Behaviour) component).enabled = true;
    MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivate(false);
    MonoBehaviourSingleton<StatusStageManager>.I.SetUniqueSmithCharacterActivate(true);
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalUniqueEquipSetData();
    base.Initialize();
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
    base.Exit();
  }
}
