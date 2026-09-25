// Decompiled with JetBrains decompiler
// Type: UIGoGameVirtualScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIWidget))]
public class UIGoGameVirtualScreen : MonoBehaviour
{
  public const float BASE_SCREEN_HEIGHT = 854f;
  public const float BASE_SCREEN_WIDTH = 480f;
  public static float screenHeight = 854f;
  public static float screenWidth = 480f;

  private void Awake() => this.InitWidget();

  public static void InitUIRoot(UIRoot root, bool isThrowIPX = false)
  {
    if (isThrowIPX)
    {
      float width = (float) Screen.width;
      float height = (float) Screen.height;
      if ((double) width > (double) height)
      {
        UIGoGameVirtualScreen.screenWidth = 854f;
        UIGoGameVirtualScreen.screenHeight = (float) ((double) height / (double) width * 854.0);
      }
      else
      {
        UIGoGameVirtualScreen.screenHeight = 854f;
        UIGoGameVirtualScreen.screenWidth = (float) ((double) width / (double) height * 854.0);
      }
    }
    else
    {
      Vector2 resolutionFixed = FixedPanelNGUI.GetResolutionFixed();
      UIGoGameVirtualScreen.screenWidth = (float) (int) resolutionFixed.x;
      UIGoGameVirtualScreen.screenHeight = (float) (int) resolutionFixed.y;
    }
    root.scalingStyle = UIRoot.Scaling.Constrained;
    root.manualHeight = (int) UIGoGameVirtualScreen.screenHeight;
    root.manualWidth = (int) UIGoGameVirtualScreen.screenWidth;
    root.fitHeight = true;
    root.fitWidth = true;
  }

  public void InitWidget()
  {
    UIWidget uiWidget = ((Component) this).gameObject.GetComponent<UIWidget>();
    if (Object.op_Equality((Object) uiWidget, (Object) null))
      uiWidget = ((Component) this).gameObject.AddComponent<UIWidget>();
    if ((double) uiWidget.width == (double) UIGoGameVirtualScreen.screenWidth && (double) uiWidget.height == (double) UIGoGameVirtualScreen.screenHeight)
      return;
    Vector3 localPosition = ((Component) uiWidget).transform.localPosition;
    uiWidget.SetRect(UIGoGameVirtualScreen.screenWidth * -0.5f, UIGoGameVirtualScreen.screenHeight * -0.5f, UIGoGameVirtualScreen.screenWidth, UIGoGameVirtualScreen.screenHeight);
    ((Component) uiWidget).transform.localPosition = localPosition;
    uiWidget.SetDirty();
  }
}
