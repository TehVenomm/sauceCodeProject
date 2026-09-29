// Decompiled with JetBrains decompiler
// Type: SpringPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
  public static SpringPanel current;
  public Vector3 target = Vector3.zero;
  public float strength = 10f;
  public SpringPanel.OnFinished onFinished;
  private UIPanel mPanel;
  private Transform mTrans;
  private UIScrollView mDrag;

  private void Start()
  {
    this.mPanel = ((Component) this).GetComponent<UIPanel>();
    this.mDrag = ((Component) this).GetComponent<UIScrollView>();
    this.mTrans = ((Component) this).transform;
  }

  private void Update() => this.AdvanceTowardsPosition();

  protected virtual void AdvanceTowardsPosition()
  {
    float deltaTime = RealTime.deltaTime;
    bool flag = false;
    Vector3 localPosition = this.mTrans.localPosition;
    Vector3 vector3_1 = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, this.target);
    if ((double) ((Vector3) ref vector3_2).sqrMagnitude < 0.0099999997764825821)
    {
      vector3_1 = this.target;
      ((Behaviour) this).enabled = false;
      flag = true;
    }
    this.mTrans.localPosition = vector3_1;
    Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, localPosition);
    Vector2 clipOffset = this.mPanel.clipOffset;
    clipOffset.x -= vector3_3.x;
    clipOffset.y -= vector3_3.y;
    this.mPanel.clipOffset = clipOffset;
    if (Object.op_Inequality((Object) this.mDrag, (Object) null))
      this.mDrag.UpdateScrollbars(false);
    if (!flag || this.onFinished == null)
      return;
    SpringPanel.current = this;
    this.onFinished();
    SpringPanel.current = (SpringPanel) null;
  }

  public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
  {
    SpringPanel springPanel = go.GetComponent<SpringPanel>();
    if (Object.op_Equality((Object) springPanel, (Object) null))
      springPanel = go.AddComponent<SpringPanel>();
    springPanel.target = pos;
    springPanel.strength = strength;
    springPanel.onFinished = (SpringPanel.OnFinished) null;
    ((Behaviour) springPanel).enabled = true;
    return springPanel;
  }

  public delegate void OnFinished();
}
