// Decompiled with JetBrains decompiler
// Type: ExploreMapFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ExploreMapFrame : MonoBehaviour
{
  private static readonly int MAP_HORIZONTAL_MARGIN_FOR_OUTER_FRAME = 6;
  [SerializeField]
  private UIWidget frameWidget;
  [SerializeField]
  private UIWidget mapFrameTop;
  [SerializeField]
  private UIWidget mapFrameBottom;
  [SerializeField]
  private UILabel captionLabel;
  private ExploreMapRoot mapRoot;
  private UIScreenRotationHandler[] rotationHandler;

  private void Awake()
  {
    this.rotationHandler = ((Component) this).GetComponentsInChildren<UIScreenRotationHandler>(true);
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    this.OnScreenRotate(true);
  }

  private void OnEnable()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnDisable()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  public void OnScreenRotate(bool isPortrait)
  {
    for (int index = 0; index < this.rotationHandler.Length; ++index)
      this.rotationHandler[index].InvokeRotate();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.UpdateMap());
  }

  public void SetMap(ExploreMapRoot map)
  {
    this.mapRoot = map;
    this.UpdateMap();
  }

  public void SetCaption(string text)
  {
    if (!Object.op_Inequality((Object) null, (Object) this.captionLabel))
      return;
    this.captionLabel.text = text;
  }

  private void UpdateMap()
  {
    if (Object.op_Equality((Object) this.mapRoot, (Object) null))
      return;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
    {
      float mapScale = this.mapRoot.GetMapScale();
      ((Component) this.mapRoot).gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(mapScale, mapScale));
      Vector2 sonarScale = this.mapRoot.GetSonarScale();
      if (Object.op_Inequality((Object) this.mapRoot.directionSonar, (Object) null))
        this.mapRoot.directionSonar.transform.localScale = Vector2.op_Implicit(sonarScale);
    }
    Rect mapAreaRect = this.CalcMapAreaRect();
    this.UpdateMapCenter(mapAreaRect);
    this.UpdateMapVisibleArea(mapAreaRect);
  }

  private Rect CalcMapAreaRect()
  {
    Vector3 localPosition1 = this.mapFrameTop.cachedTransform.localPosition;
    Vector3 localPosition2 = this.mapFrameBottom.cachedTransform.localPosition;
    float num1 = (float) this.frameWidget.width - (float) (ExploreMapFrame.MAP_HORIZONTAL_MARGIN_FOR_OUTER_FRAME * 2);
    float num2 = localPosition1.y - localPosition2.y;
    Vector2 vector2 = Vector2.op_Multiply(new Vector2(localPosition1.x + localPosition2.x, localPosition1.y + localPosition2.y), 0.5f);
    float num3 = num1 * 0.5f;
    float num4 = num2 * 0.5f;
    return new Rect(vector2.x - num3, vector2.y - num4, num1, num2);
  }

  private void UpdateMapCenter(Rect mapAreaRect)
  {
    ((Component) this.mapRoot).transform.localPosition = ((Rect) ref mapAreaRect).center.ToVector3XY();
  }

  private void UpdateMapVisibleArea(Rect mapAreaRect)
  {
    UITexture mapTexture = this.mapRoot.mapTexture;
    float num1 = mapTexture.cachedTransform.localScale.x * ((Component) this.mapRoot).gameObject.transform.localScale.x;
    float num2 = (float) mapTexture.width * num1;
    double num3 = (double) mapTexture.height * (double) num1;
    float num4 = (float) (((double) num2 - (double) ((Rect) ref mapAreaRect).width) / (double) num1 * 0.5);
    double height = (double) ((Rect) ref mapAreaRect).height;
    float num5 = (float) ((num3 - height) / (double) num1 * 0.5);
    Vector3 localPosition = mapTexture.cachedTransform.localPosition;
    this.mapRoot.mapTexture.border = new Vector4(num4 - localPosition.x, num5 - localPosition.y, num4 + localPosition.x, num5 + localPosition.y);
  }
}
