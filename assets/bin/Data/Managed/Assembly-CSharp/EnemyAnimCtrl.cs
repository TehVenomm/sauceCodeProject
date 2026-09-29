// Decompiled with JetBrains decompiler
// Type: EnemyAnimCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyAnimCtrl : MonoBehaviour, IAnimEvent
{
  private System.Action onSignal;
  private AnimEventProcessor animEvent;
  private EnemyLoader loader;
  private Camera renderCamera;
  private bool isFieldQuest;
  protected CharacterStampCtrl stepCtrl;
  protected bool enableEventMove;
  protected Vector3 eventMoveVelocity = Vector3.zero;

  public Transform blurNode { get; protected set; }

  public void Init(EnemyLoader _loader, Camera render_camrea, bool is_field_quest = false)
  {
    this.loader = _loader;
    this.renderCamera = render_camrea;
    this.isFieldQuest = is_field_quest;
    this.animEvent = new AnimEventProcessor(_loader.animEventData, _loader.animator, (IAnimEvent) this);
    ((Component) this.loader.body).gameObject.AddComponent<EnemyAnimCtrlProxy>().enemyAnimCtrl = this;
    if (!this.isFieldQuest)
      return;
    EnemyParam componentInChildren = ((Component) this).gameObject.GetComponentInChildren<EnemyParam>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    if (componentInChildren.stampInfos != null && componentInChildren.stampInfos.Length != 0)
    {
      this.stepCtrl = ((Component) this).gameObject.AddComponent<CharacterStampCtrl>();
      this.stepCtrl.Init(componentInChildren.stampInfos, (Character) null, true);
      this.stepCtrl.stampDistance = 999f;
      this.stepCtrl.effectLayer = 18;
    }
    Object.DestroyImmediate((Object) componentInChildren);
  }

  public void OnAnimatorMove()
  {
    if (!Object.op_Inequality((Object) this.loader, (Object) null) || !Object.op_Inequality((Object) this.loader.animator, (Object) null) || !this.loader.animator.applyRootMotion)
      return;
    Transform transform1 = ((Component) this).transform;
    transform1.position = Vector3.op_Addition(transform1.position, this.loader.animator.deltaPosition);
    Transform transform2 = ((Component) this).transform;
    transform2.rotation = Quaternion.op_Multiply(transform2.rotation, this.loader.animator.deltaRotation);
  }

  private void Update()
  {
    if (this.animEvent != null)
      this.animEvent.Update();
    if (!this.enableEventMove)
      return;
    Transform transform = ((Component) this).transform;
    transform.position = Vector3.op_Addition(transform.position, Vector3.op_Multiply(this.eventMoveVelocity, Time.deltaTime));
  }

  public void OnAnimEvent(AnimEventData.EventData data)
  {
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null) && this.stepCtrl.OnAnimEvent(data))
      return;
    switch (data.id)
    {
      case AnimEventFormat.ID.MOVE_FORWARD_START:
        float floatArg1 = data.floatArgs[0];
        this.enableEventMove = true;
        this.eventMoveVelocity = Vector3.op_Multiply(Vector3.forward, floatArg1);
        break;
      case AnimEventFormat.ID.MOVE_END:
        this.enableEventMove = false;
        this.eventMoveVelocity = Vector3.zero;
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_START:
        float floatArg2 = data.floatArgs[0];
        float floatArg3 = data.floatArgs[1];
        string stringArg = data.stringArgs[0];
        bool flag = data.intArgs[0] != 0;
        Transform body = Utility.Find(this.loader.body, stringArg);
        if (Object.op_Equality((Object) body, (Object) null))
          body = this.loader.body;
        if (Object.op_Equality((Object) this.renderCamera, (Object) null) && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
        {
          if (flag)
            MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(floatArg2, floatArg3, body);
          else
            MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(floatArg2, floatArg3, body.position);
        }
        else
        {
          Vector2 center = Vector2.op_Implicit(this.renderCamera.WorldToScreenPoint(body.position));
          center.x /= (float) Screen.width;
          center.y = Mathf.Lerp(0.5f, 1f, center.y / (float) Screen.height);
          MonoBehaviourSingleton<FilterManager>.I.StartTubulanceFilter(floatArg3, center, (System.Action) null);
        }
        if (this.isFieldQuest)
          break;
        this.loader.animator.speed = 0.0f;
        if (this.onSignal == null)
          break;
        this.onSignal();
        this.onSignal = (System.Action) null;
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_CHANGE:
        float floatArg4 = data.floatArgs[0];
        float floatArg5 = data.floatArgs[1];
        if ((double) floatArg5 <= 0.0)
        {
          MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(floatArg4);
          break;
        }
        MonoBehaviourSingleton<InGameCameraManager>.I.ChangeRadialBlurFilter(floatArg4, floatArg5);
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_END:
        MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(data.floatArgs[0]);
        break;
      case AnimEventFormat.ID.SE_ONESHOT:
        int intArg = data.intArgs[0];
        if (intArg == 0)
          break;
        SoundManager.PlayOneShotSE(intArg, parent: MonoBehaviourSingleton<AppMain>.I.mainCameraTransform);
        break;
      case AnimEventFormat.ID.FIELD_QUEST_UI_OPEN:
        if (!this.isFieldQuest || this.onSignal == null)
          break;
        this.onSignal();
        this.onSignal = (System.Action) null;
        break;
    }
  }

  public void PlayQuestStartAnim(System.Action on_complete)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || Object.op_Equality((Object) this.loader.animEventData, (Object) null) || !((Object) this.loader.animEventData).name.Contains("QENM"))
    {
      if (on_complete == null)
        return;
      on_complete();
    }
    else
    {
      this.onSignal = on_complete;
      if (this.isFieldQuest)
        this.loader.animator.CrossFade("ATTACK_FIELD", 0.0f);
      else
        this.loader.animator.CrossFade("ATTACK", 0.1f);
    }
  }
}
