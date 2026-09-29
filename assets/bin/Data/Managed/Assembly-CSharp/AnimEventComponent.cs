// Decompiled with JetBrains decompiler
// Type: AnimEventComponent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimEventComponent : MonoBehaviour
{
  public AnimEventData animEventData;
  public Animator animator;
  public IAnimEvent listener;
  private AnimEventProcessor processer;

  private void Start()
  {
    if (Object.op_Equality((Object) this.animator, (Object) null))
      this.animator = ((Component) this).gameObject.GetComponent<Animator>();
    this.Run();
  }

  private void Update()
  {
    if (this.processer == null)
      return;
    this.processer.Update();
  }

  public void Run()
  {
    if (!Object.op_Inequality((Object) this.animator, (Object) null) || !Object.op_Inequality((Object) this.animEventData, (Object) null) || this.listener == null)
      return;
    this.processer = new AnimEventProcessor(this.animEventData, this.animator, this.listener);
  }
}
