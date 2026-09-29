// Decompiled with JetBrains decompiler
// Type: AudioListenerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AudioListenerManager : MonoBehaviourSingleton<AudioListenerManager>
{
  private AudioListenerManager.STATUS_FLAGS Status = AudioListenerManager.STATUS_FLAGS.INITIALIZE;
  private const AudioListenerManager.TRACE_FLAGS TRACE_BOTH = AudioListenerManager.TRACE_FLAGS.POSITION | AudioListenerManager.TRACE_FLAGS.ROTATION;
  private StageObject m_target;

  public bool HasFlag(AudioListenerManager.STATUS_FLAGS flg) => (this.Status & flg) == flg;

  public void SetFlag(AudioListenerManager.STATUS_FLAGS flg, bool isEnable)
  {
    if (isEnable)
      this.Status |= flg;
    else
      this.Status &= ~flg;
  }

  public void SetTargetObject(StageObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    this.m_target = obj;
    this.SetFlag(AudioListenerManager.STATUS_FLAGS.TARGET_OBJECT_ACTIVE, true);
  }

  public void ReSetTargetObject()
  {
    this.m_target = (StageObject) null;
    this.SetFlag(AudioListenerManager.STATUS_FLAGS.TARGET_OBJECT_ACTIVE, false);
  }

  protected override void Awake()
  {
    ((Component) this).gameObject.AddComponent<AudioListener>();
    base.Awake();
  }

  private void LateUpdate() => this.UpdateListener();

  private void UpdateListener()
  {
    if (this.Status == (AudioListenerManager.STATUS_FLAGS) 0)
      return;
    if (this.HasFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE))
      this.TraceIngameCamera();
    else if (this.HasFlag(AudioListenerManager.STATUS_FLAGS.TARGET_OBJECT_ACTIVE))
    {
      this.TraceObject();
    }
    else
    {
      if (!this.HasFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE))
        return;
      this.TraceMainCamera();
    }
  }

  private void Trace(Transform t, AudioListenerManager.TRACE_FLAGS flag)
  {
    if (!Object.op_Inequality((Object) t, (Object) null))
      return;
    if ((flag & AudioListenerManager.TRACE_FLAGS.POSITION) != (AudioListenerManager.TRACE_FLAGS) 0)
      this._transform.position = t.position;
    if ((flag & AudioListenerManager.TRACE_FLAGS.ROTATION) == (AudioListenerManager.TRACE_FLAGS) 0)
      return;
    this._transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, t.right));
  }

  private void TraceMainCamera(AudioListenerManager.TRACE_FLAGS flags = AudioListenerManager.TRACE_FLAGS.POSITION | AudioListenerManager.TRACE_FLAGS.ROTATION)
  {
    if (!MonoBehaviourSingleton<AppMain>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<AppMain>.I.mainCameraTransform, (Object) null))
      return;
    this.Trace(MonoBehaviourSingleton<AppMain>.I.mainCameraTransform, AudioListenerManager.TRACE_FLAGS.POSITION | AudioListenerManager.TRACE_FLAGS.ROTATION);
  }

  private void TraceObject()
  {
    this.TraceMainCamera(AudioListenerManager.TRACE_FLAGS.ROTATION);
    if (Object.op_Equality((Object) this.m_target, (Object) null) || Object.op_Equality((Object) this.m_target._transform, (Object) null))
      this.SetFlag(AudioListenerManager.STATUS_FLAGS.TARGET_OBJECT_ACTIVE, false);
    else
      this.Trace(this.m_target._transform, AudioListenerManager.TRACE_FLAGS.POSITION);
  }

  private void TraceIngameCamera(AudioListenerManager.TRACE_FLAGS flags = AudioListenerManager.TRACE_FLAGS.POSITION | AudioListenerManager.TRACE_FLAGS.ROTATION)
  {
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, (Object) null))
      this.Trace(MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, AudioListenerManager.TRACE_FLAGS.POSITION | AudioListenerManager.TRACE_FLAGS.ROTATION);
    else
      this.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_INGAME_ACTIVE, false);
  }

  [Flags]
  public enum STATUS_FLAGS
  {
    INITIALIZE = 1,
    CAMERA_MAIN_ACTIVE = 2,
    CAMERA_INGAME_ACTIVE = 4,
    TARGET_OBJECT_ACTIVE = 32, // 0x00000020
  }

  [Flags]
  public enum TRACE_FLAGS
  {
    POSITION = 1,
    ROTATION = 2,
  }
}
