// Decompiled with JetBrains decompiler
// Type: StatusScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class StatusScene : GameSection
{
  public override void Initialize()
  {
    RenderTargetCacher component = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Behaviour) component).enabled = true;
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalVisualEquipData();
    MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivate(true);
    MonoBehaviourSingleton<StatusStageManager>.I.SetUniqueSmithCharacterActivate(false);
    base.Initialize();
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
    base.Exit();
  }
}
