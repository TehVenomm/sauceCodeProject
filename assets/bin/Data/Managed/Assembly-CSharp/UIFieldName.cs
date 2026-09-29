// Decompiled with JetBrains decompiler
// Type: UIFieldName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIFieldName : MonoBehaviour
{
  private UIFieldName.Phase phase = UIFieldName.Phase.FadeIn;
  private float waitSeconds;
  private bool initialized;
  private const float baseWidth = 120f;
  private float bgLineWidth;
  private float bgLineWidthMax;
  private float bgLineWidthStep;
  public UILabel nameLabelU;
  public UILabel nameLabelD;
  public Transform labelRoot;
  public UITweenCtrl tweenCtrl;
  public UIWidget bgLight;
  public UIWidget bgBlack;
  public UIWidget bgLine;
  public TweenAlpha tweenTop;

  private void Start()
  {
    if (Object.op_Equality((Object) null, (Object) this.nameLabelU) || Object.op_Equality((Object) null, (Object) this.nameLabelD) || Object.op_Equality((Object) null, (Object) this.labelRoot) || Object.op_Equality((Object) null, (Object) this.tweenCtrl) || Object.op_Equality((Object) null, (Object) this.bgLight) || Object.op_Equality((Object) null, (Object) this.bgBlack) || Object.op_Equality((Object) null, (Object) this.bgLine) || Object.op_Equality((Object) null, (Object) this.tweenTop))
      return;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (fieldMapData == null)
      ((Component) this).gameObject.SetActive(false);
    else if (string.Compare(fieldMapData.stageName, 0, "FI", 0, 2) != 0)
      ((Component) this).gameObject.SetActive(false);
    else if (QuestManager.IsValidInGameWaveMatch())
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      this.nameLabelU.text = fieldMapData.mapName;
      this.nameLabelD.text = fieldMapData.mapName;
      float width = (float) this.nameLabelU.width;
      Vector3 localPosition = this.labelRoot.localPosition;
      localPosition.x += width * 0.5f;
      this.labelRoot.localPosition = localPosition;
      float num = width - 120f;
      this.bgLight.width = (int) ((float) this.bgLight.width + num);
      this.bgBlack.width = (int) ((float) this.bgBlack.width + num);
      this.bgLineWidthMax = (float) this.bgLine.width + num;
      this.bgLineWidth = 0.0f;
      this.bgLine.width = (int) this.bgLineWidth;
      this.bgLineWidthStep = this.bgLineWidthMax / 12f;
      this.nameLabelU.fontStyle = (FontStyle) 2;
      this.nameLabelD.fontStyle = (FontStyle) 2;
      this.phase = UIFieldName.Phase.StartWait;
      this.waitSeconds = 0.8f;
      this.initialized = true;
    }
  }

  private void Phase_StartWait()
  {
    if (0.0 < (double) this.waitSeconds)
      return;
    this.phase = UIFieldName.Phase.FadeIn;
    this.waitSeconds = 1.2f;
    UITweenCtrl.Reset(((Component) this.tweenCtrl).transform);
    UITweenCtrl.Play(((Component) this.tweenCtrl).transform, is_input_block: false);
  }

  private void Phase_FadeIn()
  {
    this.bgLineWidth += this.bgLineWidthStep;
    if ((double) this.bgLineWidthMax <= (double) this.bgLineWidth)
      this.bgLineWidth = this.bgLineWidthMax;
    this.bgLine.width = (int) this.bgLineWidth;
    if (0.0 < (double) this.waitSeconds)
      return;
    this.phase = UIFieldName.Phase.Display;
    this.waitSeconds = 2f;
  }

  private void Phase_Display()
  {
    if (0.0 < (double) this.waitSeconds)
      return;
    this.phase = UIFieldName.Phase.FadeOut;
    this.waitSeconds = 1f;
    this.tweenTop.ResetToBeginning();
    this.tweenTop.PlayForward();
  }

  private void Phase_FadeOut()
  {
    if (0.0 < (double) this.waitSeconds)
      return;
    ((Component) this).gameObject.SetActive(false);
    this.phase = UIFieldName.Phase.None;
  }

  private void Update()
  {
    if (!this.initialized)
      return;
    this.waitSeconds -= Time.deltaTime;
    switch (this.phase)
    {
      case UIFieldName.Phase.StartWait:
        this.Phase_StartWait();
        break;
      case UIFieldName.Phase.FadeIn:
        this.Phase_FadeIn();
        break;
      case UIFieldName.Phase.Display:
        this.Phase_Display();
        break;
      case UIFieldName.Phase.FadeOut:
        this.Phase_FadeOut();
        break;
    }
  }

  private enum Phase
  {
    StartWait,
    FadeIn,
    Display,
    FadeOut,
    None,
  }
}
