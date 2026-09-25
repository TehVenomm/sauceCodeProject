// Decompiled with JetBrains decompiler
// Type: MaterialInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MaterialInfo : MonoBehaviour
{
  public UILabel lbl;
  public UIWidget widget;
  public string nowSectionName;
  private bool isEnableParentScroll;

  public void Initialize(string section_name)
  {
    this.nowSectionName = section_name;
    this.SetEnableInfo(false);
  }

  public void SetText(string text) => this.lbl.text = text;

  public void SetEnableInfo(bool is_enable)
  {
    ((Component) this.lbl).gameObject.SetActive(is_enable);
    ((Component) this.widget).gameObject.SetActive(is_enable);
  }

  public void Send(bool is_touch, Transform button, string item_name, Transform parent_scroll)
  {
    if (Object.op_Equality((Object) this.lbl, (Object) null))
      return;
    this.SetEnableInfo(is_touch);
    if (!is_touch)
      return;
    this.SetText(item_name);
    Transform transform1 = ((Component) this).transform;
    transform1.parent = button.parent;
    transform1.localScale = Vector3.one;
    UIScrollView componentInParent1 = ((Component) button).GetComponentInParent<UIScrollView>();
    BoxCollider component1 = ((Component) button).GetComponent<BoxCollider>();
    UIWidget widget = this.widget;
    Transform transform2 = ((Component) this).transform;
    Vector3 position = transform2.position;
    UIRoot componentInParent2 = ((Component) this).gameObject.GetComponentInParent<UIRoot>();
    float num1 = (float) ((int) ((double) component1.size.y * 0.5) + widget.height / 2) * ((Component) componentInParent2).transform.localScale.y + ((Component) component1).transform.position.y;
    // ISSUE: explicit constructor call
    ((Vector3) ref position).\u002Ector(transform2.position.x, num1);
    if (Object.op_Inequality((Object) componentInParent1, (Object) null))
    {
      UIPanel component2 = ((Component) componentInParent1).GetComponent<UIPanel>();
      Vector3 worldPos = Vector3.op_Addition(position, new Vector3(0.0f, (float) (widget.height / 2) * ((Component) componentInParent2).transform.localScale.y));
      if (!component2.IsVisible(worldPos))
        position.y = ((Component) componentInParent1).transform.position.y + ((float) (((double) component2.GetViewSize().y - (double) widget.height) / 2.0) + component2.clipOffset.y) * ((Component) componentInParent2).transform.localScale.y;
    }
    float num2 = ((Component) component1).transform.position.x;
    double num3 = (double) num2 * (1.0 / (double) ((Component) componentInParent2).transform.localScale.x);
    float num4 = UIVirtualScreen.screenWidth * 0.5f;
    int num5 = widget.width / 2;
    float num6 = (float) num3 - (float) num5;
    float num7 = (float) num3 + (float) num5;
    if (-(double) num4 > (double) num6)
      num2 = (-num4 + (float) num5) * ((Component) componentInParent2).transform.localScale.x;
    else if ((double) num4 < (double) num7)
      num2 = (num4 - (float) num5) * ((Component) componentInParent2).transform.localScale.x;
    // ISSUE: explicit constructor call
    ((Vector3) ref position).\u002Ector(num2, position.y);
    ((Component) transform1).transform.position = position;
    this.isEnableParentScroll = Object.op_Inequality((Object) componentInParent1, (Object) null);
    if (Object.op_Inequality((Object) componentInParent1, (Object) null))
    {
      transform1.parent = parent_scroll ?? ((Component) componentInParent1).transform.parent;
      transform1.localScale = Vector3.one;
    }
    ((Component) transform1).gameObject.SetActive(false);
    ((Component) transform1).gameObject.SetActive(true);
  }

  private void adjustPosY(Transform button)
  {
    Vector3 zero = Vector3.zero;
    UIScrollView componentInParent1 = ((Component) button).GetComponentInParent<UIScrollView>();
    BoxCollider component1 = ((Component) button).GetComponent<BoxCollider>();
    UIWidget component2 = ((Component) this).GetComponent<UIWidget>();
    Transform transform = ((Component) this).transform;
    UIRoot componentInParent2 = ((Component) this).gameObject.GetComponentInParent<UIRoot>();
    float num = (float) ((int) ((double) component1.size.y * 0.5) + component2.height / 2) * ((Component) componentInParent2).transform.localScale.y + ((Component) component1).transform.position.y;
    // ISSUE: explicit constructor call
    ((Vector3) ref zero).\u002Ector(transform.position.x, num);
    if (Object.op_Inequality((Object) componentInParent1, (Object) null))
    {
      UIPanel component3 = ((Component) componentInParent1).GetComponent<UIPanel>();
      Vector3 worldPos = Vector3.op_Addition(zero, new Vector3(0.0f, (float) (component2.height / 2) * ((Component) componentInParent2).transform.localScale.y));
      if (!component3.IsVisible(worldPos))
        zero.y = ((Component) componentInParent1).transform.position.y + (component3.GetViewSize().y / 2f + component3.clipOffset.y) * ((Component) componentInParent2).transform.localScale.y;
    }
    transform.position = zero;
  }

  public void UpdatePosision(Transform button)
  {
    if (!this.isEnableParentScroll)
      return;
    UIScrollView componentInParent = ((Component) button).GetComponentInParent<UIScrollView>();
    if (!Object.op_Inequality((Object) componentInParent, (Object) null) || !componentInParent.isDragging)
      return;
    this.isEnableParentScroll = false;
    this.SetEnableInfo(false);
  }
}
