// Decompiled with JetBrains decompiler
// Type: StatusSmithCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class StatusSmithCharacter : MonoBehaviour
{
  private const int SMITH_NPC_ID = 4;
  private const int UNIQUE_SMITH_NPC_ID = 36;
  public bool isUnique;
  protected Animator animator;
  protected PlayerAnimCtrl animCtrl;
  protected bool isComplete;
  protected bool isActive;

  public Transform _transform { get; private set; }

  public ModelLoaderBase loader { get; protected set; }

  private void Awake() => this._transform = ((Component) this).transform;

  private IEnumerator Start()
  {
    this.isComplete = false;
    this.loader = this.LoadModel();
    while (this.loader.IsLoading())
      yield return (object) null;
    ((Component) this).gameObject.SetActive(this.isActive);
    this.animator = this.loader.GetAnimator();
    if (!Object.op_Equality((Object) this.animator, (Object) null))
    {
      ((Component) this.animator).gameObject.AddComponent<RootMotionProxy>();
      this.InitAnim();
      OutGameSettingsManager.StatusScene statusScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene;
      this._transform.position = statusScene.smithNPCPos;
      this._transform.eulerAngles = statusScene.smithNPCRot;
      this._transform.localScale = Vector3.op_Multiply(Vector3.one, statusScene.smithSize);
      this.isComplete = true;
    }
  }

  protected ModelLoaderBase LoadModel()
  {
    return this.isUnique ? Singleton<NPCTable>.I.GetNPCData(36).LoadModel(((Component) this).gameObject, true, true, (Action<Animator>) null, false) : Singleton<NPCTable>.I.GetNPCData(4).LoadModel(((Component) this).gameObject, true, true, (Action<Animator>) null, false);
  }

  protected void InitAnim() => this.animCtrl = PlayerAnimCtrl.Get(this.animator, PLCA.IDLE_02);

  public void SetActive(bool active)
  {
    if (!this.isComplete)
      this.isActive = active;
    else
      ((Component) this).gameObject.SetActive(active);
  }
}
