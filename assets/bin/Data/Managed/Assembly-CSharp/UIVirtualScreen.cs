// Decompiled with JetBrains decompiler
// Type: UIVirtualScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("ProjectUI/UIVirtualScreen")]
[RequireComponent(typeof (UIWidget))]
public class UIVirtualScreen : MonoBehaviour
{
  public const float BASE_SCREEN_HEIGHT = 854f;
  public const float BASE_SCREEN_WIDTH = 480f;
  public static float screenHeight = 854f;
  public static float screenWidth = 480f;
  public static float screenHeightFull = 854f;
  public static float screenWidthFull = 480f;
  public bool IsOverSafeArea;

  public float ScreenWidthFull => UIVirtualScreen.screenWidthFull;

  public float ScreenHeightFull => UIVirtualScreen.screenHeightFull;

  private void Awake() => this.InitWidget();

  public static void InitUIRoot(UIRoot root)
  {
    float width = (float) Screen.width;
    float height = (float) Screen.height;
    if ((double) width > (double) height)
    {
      UIVirtualScreen.screenWidth = 854f;
      UIVirtualScreen.screenHeight = (float) ((double) height / (double) width * 854.0);
    }
    else
    {
      UIVirtualScreen.screenHeight = 854f;
      UIVirtualScreen.screenWidth = (float) ((double) width / (double) height * 854.0);
    }
    UIVirtualScreen.screenHeightFull = UIVirtualScreen.screenHeight;
    UIVirtualScreen.screenWidthFull = UIVirtualScreen.screenWidth;
    UIVirtualScreen.RefleshScreenSizeForSpecialDevice();
    root.scalingStyle = UIRoot.Scaling.Constrained;
    root.manualHeight = (int) UIVirtualScreen.screenHeight;
    root.manualWidth = (int) UIVirtualScreen.screenWidth;
    root.fitHeight = true;
    root.fitWidth = true;
  }

  public void InitWidget()
  {
    float width = UIVirtualScreen.screenWidth;
    float height = UIVirtualScreen.screenHeight;
    UIVirtualScreen.RefleshScreenSizeForSpecialDevice();
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea && this.IsOverSafeArea)
    {
      width = UIVirtualScreen.screenWidthFull;
      height = UIVirtualScreen.screenHeightFull;
    }
    UIWidget uiWidget = ((Component) this).gameObject.GetComponent<UIWidget>();
    if (Object.op_Equality((Object) uiWidget, (Object) null))
      uiWidget = ((Component) this).gameObject.AddComponent<UIWidget>();
    if ((double) uiWidget.width == (double) width && (double) uiWidget.height == (double) height)
      return;
    Vector3 localPosition = ((Component) uiWidget).transform.localPosition;
    uiWidget.SetRect(width * -0.5f, height * -0.5f, width, height);
    ((Component) uiWidget).transform.localPosition = localPosition;
    uiWidget.SetDirty();
  }

  public static void RefleshScreenSizeForSpecialDevice()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    EdgeInsets safeArea = specialDeviceInfo.SafeArea;
    if (Screen.width > Screen.height)
    {
      UIVirtualScreen.screenWidth = 854f;
      UIVirtualScreen.screenWidthFull = (float) (854.0 / ((double) safeArea.SafeWidth / (double) Screen.width));
      UIVirtualScreen.screenHeight = (float) ((double) safeArea.SafeHeight / (double) safeArea.SafeWidth * 854.0);
      UIVirtualScreen.screenHeightFull = (float) (854.0 * ((double) Screen.height / (double) Screen.width));
      if (!specialDeviceInfo.NeedModifyVirtualScreenRatio)
        return;
      UIVirtualScreen.screenWidth *= specialDeviceInfo.RatioVirtualScreenLandscape;
      UIVirtualScreen.screenWidthFull *= specialDeviceInfo.RatioVirtualScreenLandscape;
      UIVirtualScreen.screenHeight *= specialDeviceInfo.RatioVirtualScreenLandscape;
      UIVirtualScreen.screenHeightFull *= specialDeviceInfo.RatioVirtualScreenLandscape;
    }
    else
    {
      UIVirtualScreen.screenHeight = (float) (480.0 * ((double) safeArea.SafeHeightMax / (double) safeArea.SafeWidthMax));
      UIVirtualScreen.screenHeightFull = (float) (480.0 * ((double) Screen.height / (double) Screen.width));
      UIVirtualScreen.screenWidth = 480f;
      UIVirtualScreen.screenWidthFull = 480f;
      if (!specialDeviceInfo.NeedModifyVirtualScreenRatio)
        return;
      UIVirtualScreen.screenWidth *= specialDeviceInfo.RatioVirtualScreenPortrait;
      UIVirtualScreen.screenWidthFull *= specialDeviceInfo.RatioVirtualScreenPortrait;
      UIVirtualScreen.screenHeight *= specialDeviceInfo.RatioVirtualScreenPortrait;
      UIVirtualScreen.screenHeightFull *= specialDeviceInfo.RatioVirtualScreenPortrait;
    }
  }
}
