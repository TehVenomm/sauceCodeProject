// Decompiled with JetBrains decompiler
// Type: FixedNGUIThrowIphoneX
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FixedNGUIThrowIphoneX : MonoBehaviour
{
  public bool LockRatioPosition;

  private void Start()
  {
    if (!Object.op_Equality((Object) ((Component) this).GetComponent<UIVirtualScreen>(), (Object) null))
      return;
    double num1 = (double) Screen.width / (double) Screen.height;
    FixedNGUIThrowIphoneX component = ((Component) this).GetComponent<FixedNGUIThrowIphoneX>();
    Vector2 vector2_1 = new Vector2();
    Vector2 vector2_2 = !FixedPanelNGUI.IsIphoneX() ? FixedPanelNGUI.GetResolutionFixed() : (!Object.op_Inequality((Object) component, (Object) null) ? FixedPanelNGUI.GetResolutionFixed() : FixedPanelNGUI.GetResolutionFixed(component.LockRatioPosition));
    float num2 = vector2_2.x / vector2_2.y / FixedPanelNGUI.BASERATIO;
    double num3 = num1 / (double) FixedPanelNGUI.BASERATIO;
    if (num1 >= (double) FixedPanelNGUI.BASERATIO)
      return;
    Vector3 localScale = ((Component) this).transform.parent.localScale;
    ((Component) this).transform.parent.localScale = new Vector3(localScale.x, localScale.y / num2, localScale.z);
  }

  private void Update()
  {
  }
}
