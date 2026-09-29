// Decompiled with JetBrains decompiler
// Type: SpringPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
  public static SpringPosition current;
  public Vector3 target = Vector3.zero;
  public float strength = 10f;
  public bool worldSpace;
  public bool ignoreTimeScale;
  public bool updateScrollView;
  public SpringPosition.OnFinished onFinished;
  [SerializeField]
  [HideInInspector]
  private GameObject eventReceiver;
  [SerializeField]
  [HideInInspector]
  public string callWhenFinished;
  private Transform mTrans;
  private float mThreshold;
  private UIScrollView mSv;

  private void Start()
  {
    this.mTrans = ((Component) this).transform;
    if (!this.updateScrollView)
      return;
    this.mSv = NGUITools.FindInParents<UIScrollView>(((Component) this).gameObject);
  }

  private void Update()
  {
    float deltaTime = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
    if (this.worldSpace)
    {
      if ((double) this.mThreshold == 0.0)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.target, this.mTrans.position);
        this.mThreshold = ((Vector3) ref vector3).sqrMagnitude * (1f / 1000f);
      }
      this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
      double mThreshold = (double) this.mThreshold;
      Vector3 vector3_1 = Vector3.op_Subtraction(this.target, this.mTrans.position);
      double sqrMagnitude = (double) ((Vector3) ref vector3_1).sqrMagnitude;
      if (mThreshold >= sqrMagnitude)
      {
        this.mTrans.position = this.target;
        this.NotifyListeners();
        ((Behaviour) this).enabled = false;
      }
    }
    else
    {
      if ((double) this.mThreshold == 0.0)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.target, this.mTrans.localPosition);
        this.mThreshold = ((Vector3) ref vector3).sqrMagnitude * 1E-05f;
      }
      this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
      double mThreshold = (double) this.mThreshold;
      Vector3 vector3_2 = Vector3.op_Subtraction(this.target, this.mTrans.localPosition);
      double sqrMagnitude = (double) ((Vector3) ref vector3_2).sqrMagnitude;
      if (mThreshold >= sqrMagnitude)
      {
        this.mTrans.localPosition = this.target;
        this.NotifyListeners();
        ((Behaviour) this).enabled = false;
      }
    }
    if (!Object.op_Inequality((Object) this.mSv, (Object) null))
      return;
    this.mSv.UpdateScrollbars(true);
  }

  private void NotifyListeners()
  {
    SpringPosition.current = this;
    if (this.onFinished != null)
      this.onFinished();
    if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.callWhenFinished))
      this.eventReceiver.SendMessage(this.callWhenFinished, (object) this, (SendMessageOptions) 1);
    SpringPosition.current = (SpringPosition) null;
  }

  public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
  {
    SpringPosition springPosition = go.GetComponent<SpringPosition>();
    if (Object.op_Equality((Object) springPosition, (Object) null))
      springPosition = go.AddComponent<SpringPosition>();
    springPosition.target = pos;
    springPosition.strength = strength;
    springPosition.onFinished = (SpringPosition.OnFinished) null;
    if (!((Behaviour) springPosition).enabled)
    {
      springPosition.mThreshold = 0.0f;
      ((Behaviour) springPosition).enabled = true;
    }
    return springPosition;
  }

  public delegate void OnFinished();
}
