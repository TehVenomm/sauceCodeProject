// Decompiled with JetBrains decompiler
// Type: MiniMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MiniMap : MonoBehaviourSingleton<MiniMap>
{
  [SerializeField]
  protected Transform iconRoot;
  [SerializeField]
  protected Transform selfIcon;
  [SerializeField]
  protected float scaling = 0.1f;
  [SerializeField]
  protected float uiRadius = 150f;
  [SerializeField]
  protected GameObject[] iconPrefabs;
  [SerializeField]
  protected GameObject[] supplyIconPrefabs;
  [SerializeField]
  protected SimplePingPongAlpha alertAnim;
  private List<MiniMapIcon> icons = new List<MiniMapIcon>();
  private List<MiniMapIcon> playerIconStock = new List<MiniMapIcon>();
  private List<MiniMapIcon> enemyIconStock = new List<MiniMapIcon>();
  private List<MiniMapIcon> waveTargetIconStock = new List<MiniMapIcon>();
  private List<List<MiniMapIcon>> supplyIconStocks = new List<List<MiniMapIcon>>();
  private int updateCount;

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.SetActive(FieldManager.IsValidInGameNoQuest() && !MonoBehaviourSingleton<FieldManager>.I.isTutorialField || QuestManager.IsValidInGameWaveMatch());
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.SyncRotatePosition();
    for (int index = 0; index < this.supplyIconPrefabs.Length; ++index)
      this.supplyIconStocks.Add(new List<MiniMapIcon>());
  }

  protected override void OnDestroySingleton()
  {
    base.OnDestroySingleton();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnScreenRotate(bool is_portrait) => this.SyncRotatePosition();

  private void SyncRotatePosition()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyMinimapPosition)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    UIWidget component = ((Component) this).gameObject.GetComponent<UIWidget>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (SpecialDeviceManager.IsPortrait)
    {
      component.leftAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.left;
      component.rightAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.right;
      component.bottomAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.bottom;
      component.topAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.top;
    }
    else
    {
      component.leftAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.left;
      component.rightAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.right;
      component.bottomAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.bottom;
      component.topAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.top;
    }
    component.UpdateAnchors();
  }

  private void LateUpdate()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    Vector3 position = ((Component) self).transform.position;
    float x = position.x;
    float z = position.z;
    ((Component) this.iconRoot).transform.localEulerAngles = new Vector3(0.0f, 0.0f, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles.y);
    this.selfIcon.localEulerAngles = new Vector3(0.0f, 0.0f, -self._transform.localEulerAngles.y);
    --this.updateCount;
    if (this.updateCount > 0)
      return;
    int index = 0;
    for (int count = this.icons.Count; index < count; ++index)
      this.icons[index].UpdateIcon(x, z, this.scaling, this.uiRadius);
    this.updateCount = 3;
  }

  public void Attach(MonoBehaviour root_object)
  {
    if (root_object is Self)
      return;
    Transform transform = ((Component) root_object).transform;
    int index1 = 0;
    for (int count = this.icons.Count; index1 < count; ++index1)
    {
      if (Object.op_Equality((Object) this.icons[index1].target, (Object) transform))
        return;
    }
    string str = "";
    int index2 = -1;
    int index3 = -1;
    MiniMapIcon miniMapIcon = (MiniMapIcon) null;
    switch (root_object)
    {
      case PortalObject _:
        index2 = 0;
        break;
      case Player _:
        if (!(root_object as Player).isInitialized)
          return;
        index2 = 1;
        if (this.playerIconStock.Count > 0)
        {
          miniMapIcon = this.playerIconStock[0];
          this.playerIconStock.Remove(miniMapIcon);
          break;
        }
        break;
      case Enemy _:
        if (!GameSaveData.instance.enableMinimapEnemy && !QuestManager.IsValidInGameWaveMatch() || !(root_object as Enemy).isInitialized)
          return;
        index2 = 2;
        if (this.enemyIconStock.Count > 0)
        {
          miniMapIcon = this.enemyIconStock[0];
          this.enemyIconStock.Remove(miniMapIcon);
          break;
        }
        break;
      case FieldWaveTargetObject _:
        FieldWaveTargetObject waveTargetObject = root_object as FieldWaveTargetObject;
        if (!waveTargetObject.isInitialized)
          return;
        index2 = 3;
        if (this.waveTargetIconStock.Count > 0)
        {
          miniMapIcon = this.waveTargetIconStock[0];
          this.waveTargetIconStock.Remove(miniMapIcon);
        }
        string raderIconName = waveTargetObject.GetRaderIconName();
        if (!raderIconName.IsNullOrWhiteSpace())
        {
          str = "dp_radar_" + raderIconName;
          break;
        }
        break;
      case FieldSupplyGimmickObject _:
        FieldSupplyGimmickObject supplyGimmickObject = root_object as FieldSupplyGimmickObject;
        if (!supplyGimmickObject.IsSearchableNearest())
          return;
        index3 = supplyGimmickObject.modelIndex;
        if (this.supplyIconStocks[index3].Count > 0)
        {
          miniMapIcon = this.supplyIconStocks[index3][0];
          this.supplyIconStocks[index3].Remove(miniMapIcon);
          break;
        }
        break;
    }
    if (Object.op_Equality((Object) miniMapIcon, (Object) null))
    {
      if (index2 >= 0 && index2 < this.iconPrefabs.Length)
      {
        GameObject gameObject = ResourceUtility.Instantiate<GameObject>(this.iconPrefabs[index2]);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          miniMapIcon = gameObject.GetComponent<MiniMapIcon>();
      }
      if (index3 >= 0 && index3 < this.supplyIconPrefabs.Length)
      {
        GameObject gameObject = ResourceUtility.Instantiate<GameObject>(this.supplyIconPrefabs[index3]);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          miniMapIcon = gameObject.GetComponent<MiniMapIcon>();
      }
    }
    if (!Object.op_Inequality((Object) miniMapIcon, (Object) null))
      return;
    miniMapIcon.Initialize(root_object);
    if (!str.IsNullOrWhiteSpace())
      miniMapIcon.SetIconSprite(str);
    miniMapIcon.target = transform;
    Utility.Attach(this.iconRoot, miniMapIcon._trasform);
    this.icons.Add(miniMapIcon);
    this.updateCount = 0;
  }

  public void Detach(MonoBehaviour root_object)
  {
    Transform transform = ((Component) root_object).transform;
    int index = 0;
    for (int count = this.icons.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) this.icons[index].target, (Object) transform))
      {
        switch (root_object)
        {
          case Player _:
            this.playerIconStock.Add(this.icons[index]);
            ((Component) this.icons[index]).gameObject.SetActive(false);
            break;
          case Enemy _:
            this.enemyIconStock.Add(this.icons[index]);
            ((Component) this.icons[index]).gameObject.SetActive(false);
            break;
          case FieldWaveTargetObject _:
            this.waveTargetIconStock.Add(this.icons[index]);
            ((Component) this.icons[index]).gameObject.SetActive(false);
            break;
          case FieldSupplyGimmickObject _:
            this.supplyIconStocks[(root_object as FieldSupplyGimmickObject).modelIndex].Add(this.icons[index]);
            ((Component) this.icons[index]).gameObject.SetActive(false);
            break;
          default:
            Object.Destroy((Object) ((Component) this.icons[index]).gameObject);
            break;
        }
        this.icons.Remove(this.icons[index]);
        break;
      }
    }
  }

  public void ShowAlert() => this.alertAnim.Play(true);
}
