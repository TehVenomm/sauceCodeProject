// Decompiled with JetBrains decompiler
// Type: QuestResultDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class QuestResultDirector : MonoBehaviour, AnimationEventProxy.IEvent
{
  public float[] playerAnimTimings;
  public Animation cameraAnim;
  public Animation cameraAnimTrial;
  private bool skip;

  public PlayerLoader[] players { get; set; }

  public Animation targetAnim { get; private set; }

  private void Start()
  {
    this.targetAnim = !QuestManager.IsValidTrial() ? this.cameraAnim : this.cameraAnimTrial;
    ((Component) this.targetAnim).GetComponent<AnimationEventProxy>().listener = (AnimationEventProxy.IEvent) this;
    int index = 0;
    for (int length = this.playerAnimTimings.Length; index < length; ++index)
      this.targetAnim.clip.AddEvent(new AnimationEvent()
      {
        functionName = "OnEventInt",
        intParameter = index,
        time = this.playerAnimTimings[index]
      });
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.targetAnim, (Object) null))
      return;
    Transform transform = ((Component) this.targetAnim).transform;
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = transform.position;
    MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = transform.rotation;
    float x = transform.localScale.x;
    if ((double) x <= 0.0)
      return;
    MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = Utility.HorizontalToVerticalFOV(x);
  }

  void AnimationEventProxy.IEvent.OnEvent()
  {
  }

  void AnimationEventProxy.IEvent.OnEventStr(string str)
  {
  }

  void AnimationEventProxy.IEvent.OnEventInt(int i)
  {
    if (i >= this.players.Length || !Object.op_Inequality((Object) this.players[i], (Object) null))
      return;
    this.players[i].animator.Play(this.players[i].GetWinMotionState(), 0, 0.0f);
  }

  public void Skip()
  {
    if (this.skip)
      return;
    this.skip = true;
    this.StartCoroutine(this.DoSkip());
  }

  private IEnumerator DoSkip()
  {
    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
    Time.timeScale = 100f;
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.targetAnim, (Object) null) || !this.skip)
      return;
    this.targetAnim.clip.SampleAnimation(((Component) this.targetAnim).gameObject, this.targetAnim.clip.length);
    this.Update();
    Time.timeScale = 1f;
  }
}
