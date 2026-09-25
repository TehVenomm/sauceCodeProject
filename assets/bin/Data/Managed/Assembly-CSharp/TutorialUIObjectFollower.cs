// Decompiled with JetBrains decompiler
// Type: TutorialUIObjectFollower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TutorialUIObjectFollower : MonoBehaviour
{
  private Transform _transform;
  private UISprite sprite;
  private Vector3 offset;
  private bool isInScrollView;
  private Rect scrollViewSize;
  private UIScrollView scroll;

  public Transform target { get; private set; }

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this.sprite = ((Component) this).GetComponent<UISprite>();
  }

  public void Setup(Transform target, Vector2 offset)
  {
    this.target = target;
    this.offset = offset.ToVector3XY();
    this.scroll = ((Component) target).GetComponentInParent<UIScrollView>();
    if (!Object.op_Implicit((Object) this.scroll))
      return;
    this.isInScrollView = true;
    this.CalcScrollRect(this.scroll);
  }

  private void CalcScrollRect(UIScrollView scroll)
  {
    UIPanel component = ((Component) scroll).GetComponent<UIPanel>();
    this.scrollViewSize = new Rect(Vector2.op_Addition(Vector2.op_Multiply(new Vector2(component.finalClipRegion.x - component.finalClipRegion.z * 0.5f, component.finalClipRegion.y - component.finalClipRegion.w * 0.5f), ((Component) scroll).transform.lossyScale.x), ((Component) scroll).transform.position.ToVector2XY()), Vector2.op_Multiply(new Vector2(component.finalClipRegion.z, component.finalClipRegion.w), ((Component) scroll).transform.lossyScale.x));
  }

  private void LateUpdate()
  {
    if (!Object.op_Implicit((Object) this.target))
      TutorialMessage.RemoveCursor(this._transform);
    else if (!((Component) this.target).gameObject.activeInHierarchy)
    {
      ((Behaviour) this.sprite).enabled = false;
    }
    else
    {
      if (!((Behaviour) this.sprite).enabled)
        ((Behaviour) this.sprite).enabled = true;
      if (this.isInScrollView)
      {
        Vector3 vector3 = Vector3.op_Addition(this.target.position, this.offset);
        if ((double) vector3.y > (double) ((Rect) ref this.scrollViewSize).yMax || (double) vector3.y < (double) ((Rect) ref this.scrollViewSize).yMin)
        {
          ((Behaviour) this.sprite).enabled = false;
          this.CalcScrollRect(this.scroll);
          return;
        }
        if (!((Behaviour) this.sprite).enabled)
          ((Behaviour) this.sprite).enabled = true;
      }
      this._transform.position = Vector3.op_Addition(this.target.position, this.offset);
    }
  }
}
