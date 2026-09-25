// Decompiled with JetBrains decompiler
// Type: UIBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIBehaviour : MonoBehaviour
{
  private Transform[] ctrls;
  private UIBehaviour.STATE _state;
  private int _baseDepth = -1;
  protected bool uiUpdateInstant = true;
  private bool _uiVisible = true;
  private BetterList<UIBehaviour.PrefabData> prefabs;
  private static readonly string[] elementSpriteName = new string[7]
  {
    "elem_fire",
    "elem_water",
    "elem_thunder",
    "elem_soil",
    "elem_light",
    "elem_dark",
    "elem_all"
  };
  private static readonly string[] elementDefSpriteName = new string[7]
  {
    "EquipPalaElementDEF_Fire",
    "EquipPalaElementDEF_Water",
    "EquipPalaElementDEF_Thunder",
    "EquipPalaElementDEF_Soil",
    "EquipPalaElementDEF_Light",
    "EquipPalaElementDEF_Dark",
    "EquipPalaElement_None"
  };
  private static readonly string[] SKILL_ICON_SPRITE_NAME = new string[11]
  {
    "EquipBtnSlot_Circle_on",
    "EquipBtnSlot_Triangle_on",
    "EquipBtnSlot_Cross_on",
    null,
    null,
    null,
    null,
    "EquipBtnSlot_Square_on",
    null,
    null,
    null
  };
  private static readonly string[] EMPTY_SKILL_ICON_EQUIP_SPRITE_NAME = new string[11]
  {
    "EquipBtnSlot_Circle_off",
    "EquipBtnSlot_Triangle_off",
    "EquipBtnSlot_Cross_off",
    null,
    null,
    null,
    null,
    "EquipBtnSlot_Square_off",
    null,
    null,
    null
  };
  private static readonly string[] EMPTY_SKILL_ICON_SPRITE_NAME = new string[11]
  {
    "ItemIconSlot_Circle_off",
    "ItemIconSlot_Triangle_off",
    "ItemIconSlot_Cross_off",
    null,
    null,
    null,
    null,
    "ItemIconSlot_Square_off",
    null,
    null,
    null
  };
  private static readonly string[] EQUIP_INDEX_ICON_SP_NAME = new string[7]
  {
    "EquipMarkW01",
    "EquipMarkW02",
    "EquipMarkW03",
    "EquipMarkArmor",
    "EquipMarkHelm",
    "EquipMarkArm",
    "EquipMarkLeg"
  };
  private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[9]
  {
    "ItemIconKind_Sword",
    "ItemIconKind_Brade",
    "ItemIconKind_Lance",
    "ItemIconKind_Edge",
    "ItemIconKind_Arrow",
    "ItemIconKind_Armor",
    "ItemIconKind_Helm",
    "ItemIconKind_Arm",
    "ItemIconKind_Leg"
  };
  private static readonly string[] SKILL_TYPE_ICON_SPRITE_NAME = new string[11]
  {
    "ItemIconKind_Attack",
    "ItemIconKind_Support",
    "ItemIconKind_Heal",
    "ItemIconKind_",
    "ItemIconKind_",
    "ItemIconKind_",
    "ItemIconKind_",
    "ItemIconKind_Passive",
    "ItemIconKind_",
    "ItemIconKind_",
    "ItemIconKind_Fragment"
  };
  private static readonly string[] ITEM_TYPE_ICON_SPRITE_BG_NAME = new string[7]
  {
    "ItemIconKind_Base_BCD",
    "ItemIconKind_Base_BCD",
    "ItemIconKind_Base_BCD",
    "ItemIconKind_Base_A",
    "ItemIconKind_Base_S",
    "ItemIconKind_Base_SS",
    "ItemIconKind_Base_SS"
  };
  private static readonly string[] MAGI_TYPE_ICON_SPRITE_NAME = new string[6]
  {
    "MagiEquipIcon_Sword",
    "MagiEquipIcon_Brade",
    "MagiEquipIcon_Lance",
    "MagiEquipIcon_Edge",
    "MagiEquipIcon_Allow",
    "MagiEquipIcon_Armor"
  };
  private static readonly string ABILITY_DETAIL_ITEM_PREFAB_NAME = "AbilityDetailItem";
  private const string MATERIAL_INFO_PREFAB_NAME = "MaterialInfo";
  private static readonly Color32 buffGreen = new Color32((byte) 0, byte.MaxValue, (byte) 128 /*0x80*/, byte.MaxValue);
  private static readonly string[] enemyIconGradeFrameName = new string[7]
  {
    "MonsterFrame_C",
    "MonsterFrame_B",
    "MonsterFrame_A",
    "MonsterFrame_A",
    "MonsterFrame_S",
    "MonsterFrame_S",
    "MonsterFrame_SS"
  };
  private const string enemyIconNormalFrameName = "MonsterCircleN";

  public Transform _transform { get; private set; }

  public Transform collectUI { get; set; }

  public UIBehaviour transferUI { get; protected set; }

  public bool IsCtrlEmpty() => this.ctrls == null;

  public List<UIPanel> uiPanels { get; private set; }

  public int[] uiPanelDepths { get; private set; }

  public List<UITransition> transitions { get; private set; }

  public GameSceneTables.SectionData sectionData { get; set; }

  public ResourceLink resourceLink { get; private set; }

  public UIBehaviour.STATE state
  {
    get
    {
      return Object.op_Inequality((Object) this.transferUI, (Object) null) ? this.transferUI.state : this._state;
    }
  }

  public bool isOpen
  {
    get
    {
      return Object.op_Inequality((Object) this.transferUI, (Object) null) ? this.transferUI.isOpen : this._state == UIBehaviour.STATE.OPEN;
    }
  }

  public bool isClose
  {
    get
    {
      return Object.op_Inequality((Object) this.transferUI, (Object) null) ? this.transferUI.isClose : this._state == UIBehaviour.STATE.CLOSE;
    }
  }

  public virtual bool IsTransitioning()
  {
    return this.state == UIBehaviour.STATE.TO_OPEN || this.state == UIBehaviour.STATE.TO_CLOSE;
  }

  public int baseDepth
  {
    get
    {
      return Object.op_Inequality((Object) this.transferUI, (Object) null) ? this.transferUI.baseDepth : this._baseDepth;
    }
    set
    {
      if (Object.op_Inequality((Object) this.transferUI, (Object) null))
      {
        this.transferUI.baseDepth = value;
      }
      else
      {
        if (this._baseDepth == value)
          return;
        this._baseDepth = value;
        if (this.uiPanels == null || this.uiPanels.Count == 0)
          return;
        this.uiPanels[0].depth = value;
        int num = value + 1;
        int index = 1;
        for (int count = this.uiPanels.Count; index < count; ++index)
          this.uiPanels[index].depth = this.uiPanelDepths[index] + num;
      }
    }
  }

  public bool uiFirstUpdate { get; private set; }

  public bool uiVisible
  {
    get
    {
      return Object.op_Inequality((Object) this.transferUI, (Object) null) ? this.transferUI.uiVisible : this._uiVisible;
    }
    set
    {
      if (Object.op_Inequality((Object) this.transferUI, (Object) null))
      {
        this.transferUI.uiVisible = value;
      }
      else
      {
        if (this._uiVisible == value)
          return;
        this._uiVisible = value;
        this.SetUIVisible(this._uiVisible);
      }
    }
  }

  private void SetUIVisible(bool b)
  {
    if (Object.op_Equality((Object) this.collectUI, (Object) null) || ((Component) this.collectUI).gameObject.activeSelf == b)
      return;
    ((Component) this.collectUI).gameObject.SetActive(b);
    if (!b || !Object.op_Inequality((Object) this.collectUI, (Object) null))
      return;
    UIVirtualScreen componentInChildren = ((Component) this.collectUI).gameObject.GetComponentInChildren<UIVirtualScreen>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.InitWidget();
  }

  public void UpdateAnchors()
  {
    if (!Object.op_Inequality((Object) this.collectUI, (Object) null))
      return;
    UIUtility.UpdateAnchors(this.collectUI);
  }

  protected virtual void Awake()
  {
    this._transform = ((Component) this).transform;
    this.uiFirstUpdate = true;
    MonoBehaviourSingleton<UIManager>.I.uiList.Add(this);
  }

  public void InitUI()
  {
    if (this.ctrls != null || !this.uiFirstUpdate)
      return;
    ((Component) this).gameObject.GetComponentsInChildren<UIGameSceneEventSender>(Temporary.uiGameSceneEventSender);
    int index1 = 0;
    for (int count = Temporary.uiGameSceneEventSender.Count; index1 < count; ++index1)
      Temporary.uiGameSceneEventSender[index1].callback = new Action<string, object, string>(this.OnEvent);
    Temporary.uiGameSceneEventSender.Clear();
    this.transitions = new List<UITransition>();
    ((Component) this).gameObject.GetComponentsInChildren<UITransition>(this.transitions);
    if (Object.op_Equality((Object) this.collectUI, (Object) null))
    {
      string collectUiName = this.GetCollectUIName();
      this.collectUI = string.IsNullOrEmpty(collectUiName) ? this._transform : MonoBehaviourSingleton<UIManager>.I.Find(collectUiName);
    }
    if (Object.op_Equality((Object) this.collectUI, (Object) null))
      return;
    this.resourceLink = ((Component) this.collectUI).GetComponent<ResourceLink>();
    this.uiPanels = new List<UIPanel>();
    ((Component) this).gameObject.GetComponentsInChildren<UIPanel>(true, this.uiPanels);
    this.uiPanelDepths = new int[this.uiPanels.Count];
    int index2 = 0;
    for (int count = this.uiPanels.Count; index2 < count; ++index2)
      this.uiPanelDepths[index2] = this.uiPanels[index2].depth;
    System.Type type = System.Type.GetType(((Object) this.collectUI).name + "+UI");
    if (type == (System.Type) null && this.sectionData != (GameSceneTables.SectionData) null)
      type = System.Type.GetType(this.sectionData.sectionName + "+UI");
    if (type != (System.Type) null)
      this.CreateCtrlsArray(type);
    this.SetUIVisible(this._uiVisible);
  }

  protected void OnEvent(string event_name, object event_data, string check_app_ver)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (UIBehaviour), ((Component) this).gameObject, event_name, event_data, check_app_ver);
  }

  protected virtual void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    if (this.prefabs != null)
    {
      int index = 0;
      for (int size = this.prefabs.size; index < size; ++index)
      {
        UIBehaviour.PrefabData prefabData = this.prefabs.buffer[index];
        if (Object.op_Inequality((Object) prefabData.inactiveObject, (Object) null))
        {
          Object.DestroyImmediate((Object) prefabData.inactiveObject);
          prefabData.inactiveObject = (GameObject) null;
        }
      }
      this.prefabs.Release();
    }
    MonoBehaviourSingleton<UIManager>.I.uiList.Remove(this);
  }

  protected void SetTransferUI(string ui_name, System.Type enum_type)
  {
    Transform transform = MonoBehaviourSingleton<UIManager>.I.Find(ui_name);
    if (Object.op_Equality((Object) transform, (Object) null))
    {
      Log.Error(LOG.UI, ui_name + " is not found.");
    }
    else
    {
      this.transferUI = ((Component) transform).gameObject.GetComponent<UIBehaviour>();
      if (!Object.op_Inequality((Object) this.transferUI, (Object) null))
        return;
      this.transferUI.CreateCtrlsArray(enum_type);
      ((Component) this.transferUI).GetComponentsInChildren<UIGameSceneEventSender>(true, Temporary.uiGameSceneEventSender);
      int index = 0;
      for (int count = Temporary.uiGameSceneEventSender.Count; index < count; ++index)
        Temporary.uiGameSceneEventSender[index].callback = new Action<string, object, string>(this.OnEvent);
      Temporary.uiGameSceneEventSender.Clear();
    }
  }

  protected virtual string GetCollectUIName() => (string) null;

  public void CreateCtrlsArray(System.Type enum_type)
  {
    if (this.ctrls != null)
      Log.Error(LOG.UI, "Re CollectCtrls");
    else
      this.ctrls = new Transform[Enum.GetNames(enum_type).Length];
  }

  public Transform GetCtrl(Enum label_enum)
  {
    if (Object.op_Inequality((Object) this.transferUI, (Object) null))
      return this.transferUI.GetCtrl(label_enum);
    if (this.ctrls == null)
    {
      Log.Error(LOG.UI, "not collect ctrls.");
      return (Transform) null;
    }
    int int32 = Convert.ToInt32((object) label_enum);
    if (int32 < 0 || int32 >= this.ctrls.Length)
      return (Transform) null;
    if (Object.op_Equality((Object) this.ctrls[int32], (Object) null))
      this.ctrls[int32] = Utility.Find(this.collectUI, label_enum.ToString());
    return this.ctrls[int32];
  }

  public void AddPrefab(GameObject prefab, GameObject inactive_object)
  {
    if (this.prefabs == null)
      this.prefabs = new BetterList<UIBehaviour.PrefabData>();
    this.prefabs.Add(new UIBehaviour.PrefabData()
    {
      name = ((Object) prefab).name,
      prefab = prefab,
      inactiveObject = inactive_object
    });
  }

  private UIBehaviour.PrefabData GetPrefabData(string prefab_name)
  {
    if (this.prefabs != null && !string.IsNullOrEmpty(prefab_name))
    {
      int index = 0;
      for (int size = this.prefabs.size; index < size; ++index)
      {
        if (this.prefabs.buffer[index].name == prefab_name)
          return this.prefabs.buffer[index];
      }
    }
    return (UIBehaviour.PrefabData) null;
  }

  protected Transform SetPrefab(Enum parent_enum, string prefab_name)
  {
    return this.prefabs == null ? (Transform) null : this.SetPrefab(this.GetCtrl(parent_enum), prefab_name);
  }

  protected Transform SetPrefab(Transform parent, string prefab_name, bool check_panel = true)
  {
    if (this.prefabs == null || Object.op_Equality((Object) parent, (Object) null))
      return (Transform) null;
    Transform transform = parent.Find(prefab_name);
    return Object.op_Inequality((Object) transform, (Object) null) ? transform : this.Realizes(prefab_name, parent, check_panel);
  }

  protected Transform Realizes(string prefab_name, Transform parent, bool check_panel = true)
  {
    if (this.prefabs != null && !Object.op_Equality((Object) parent, (Object) null))
      return this._Realizes(this.GetPrefabData(prefab_name), parent, check_panel);
    Debug.LogWarning((object) "Relizesに失敗しました");
    return (Transform) null;
  }

  private Transform _Realizes(
    UIBehaviour.PrefabData prefab_data,
    Transform parent,
    bool check_panel)
  {
    if (prefab_data == null)
      return (Transform) null;
    Transform transform = prefab_data.Realizes(parent);
    if (Object.op_Equality((Object) transform, (Object) null))
      return (Transform) null;
    if (check_panel)
    {
      UIPanel componentInChildren = ((Component) transform).GetComponentInChildren<UIPanel>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        UIPanel componentInParent = ((Component) parent).GetComponentInParent<UIPanel>();
        if (Object.op_Inequality((Object) componentInParent, (Object) null))
          componentInChildren.depth = componentInParent.depth + 1;
      }
    }
    return transform;
  }

  protected Transform FindCtrl(Transform root, Enum enum_value)
  {
    return enum_value == null || Object.op_Equality((Object) root, (Object) null) ? root : Utility.Find(root, enum_value.ToString());
  }

  protected Transform GetChild(Enum ctrl_enum, int index)
  {
    return this.GetChild(this.GetCtrl(ctrl_enum), index);
  }

  protected Transform GetChild(Transform root, Enum ctrl_enum, int index)
  {
    return this.GetChild(this.FindCtrl(root, ctrl_enum), index);
  }

  protected Transform GetChild(Transform t, int index)
  {
    return Object.op_Equality((Object) t, (Object) null) ? (Transform) null : t.GetChild(index);
  }

  protected Transform GetChildSafe(Enum ctrl_enum, int index)
  {
    return this.GetChildSafe(this.GetCtrl(ctrl_enum), index);
  }

  protected Transform GetChildSafe(Transform root, Enum ctrl_enum, int index)
  {
    return this.GetChildSafe(this.FindCtrl(root, ctrl_enum), index);
  }

  protected Transform GetChildSafe(Transform t, int index)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return (Transform) null;
    return index < 0 || index >= t.childCount ? (Transform) null : t.GetChild(index);
  }

  protected void SetActive(Enum ctrl_enum, bool is_visible)
  {
    this.SetActive(this.GetCtrl(ctrl_enum), is_visible);
  }

  protected void SetActive(Transform root, Enum ctrl_enum, bool is_visible)
  {
    this.SetActive(this.FindCtrl(root, ctrl_enum), is_visible);
  }

  protected void SetActive(Transform t, bool is_visible)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).gameObject.SetActive(is_visible);
  }

  public bool IsActive(Enum ctrl_enum)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    return !Object.op_Equality((Object) ctrl, (Object) null) && ((Component) ctrl).gameObject.activeSelf;
  }

  protected void InitDeactive(Enum ctrl_enum) => this.InitDeactive(this.GetCtrl(ctrl_enum));

  protected void InitDeactive(Transform root, Enum ctrl_enum)
  {
    this.InitDeactive(this.FindCtrl(root, ctrl_enum));
  }

  protected void InitDeactive(Transform t)
  {
    if (!this.uiFirstUpdate || Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).gameObject.SetActive(false);
  }

  protected COMPONENT GetComponent<COMPONENT>(Enum ctrl_enum) where COMPONENT : Component
  {
    return this.GetComponent<COMPONENT>(this.GetCtrl(ctrl_enum));
  }

  protected COMPONENT GetComponent<COMPONENT>(Transform root, Enum ctrl_enum) where COMPONENT : Component
  {
    return this.GetComponent<COMPONENT>(this.FindCtrl(root, ctrl_enum));
  }

  protected COMPONENT GetComponent<COMPONENT>(Transform t) where COMPONENT : Component
  {
    return Object.op_Equality((Object) t, (Object) null) ? default (COMPONENT) : ((Component) t).GetComponent<COMPONENT>();
  }

  protected void SetEnabled<COMPONENT>(Enum ctrl_enum, bool is_enabled) where COMPONENT : MonoBehaviour
  {
    this.SetEnabled<COMPONENT>(this.GetCtrl(ctrl_enum), is_enabled);
  }

  protected void SetEnabled<COMPONENT>(Transform root, Enum ctrl_enum, bool is_enabled) where COMPONENT : MonoBehaviour
  {
    this.SetEnabled<COMPONENT>(this.FindCtrl(root, ctrl_enum), is_enabled);
  }

  protected void SetEnabled<COMPONENT>(Transform t, bool is_enabled) where COMPONENT : MonoBehaviour
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Behaviour) (object) ((Component) t).GetComponent<COMPONENT>()).enabled = is_enabled;
  }

  protected void SetDepth(Transform root, Enum panel_enum, int depth)
  {
    this.SetDepth(this.FindCtrl(root, panel_enum), depth);
  }

  protected void SetDepth(Enum panel_enum, int depth)
  {
    this.SetDepth(this.GetCtrl(panel_enum), depth);
  }

  protected void SetDepth(Transform t, int depth)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIPanel>().depth = depth;
  }

  protected void SetWidth(Enum button_enum, int width)
  {
    this.SetWidth(this.GetCtrl(button_enum), width);
  }

  protected void SetWidth(Transform root, Enum button_enum, int width)
  {
    this.SetWidth(this.FindCtrl(root, button_enum), width);
  }

  protected void SetWidth(Transform t, int width)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIWidget>().width = width;
  }

  protected int GetWidth(Enum ctrl_enum)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    return Object.op_Equality((Object) ctrl, (Object) null) ? 0 : ((Component) ctrl).GetComponent<UIWidget>().width;
  }

  protected int GetHeight(Enum ctrl_enum)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    return Object.op_Equality((Object) ctrl, (Object) null) ? 0 : ((Component) ctrl).GetComponent<UIWidget>().height;
  }

  protected int GetHeight(Transform root, Enum ctrl_enum)
  {
    Transform ctrl = this.FindCtrl(root, ctrl_enum);
    return Object.op_Equality((Object) ctrl, (Object) null) ? 0 : ((Component) ctrl).GetComponent<UIWidget>().height;
  }

  protected void SetHeight(Enum _enum, int height) => this.SetHeight(this.GetCtrl(_enum), height);

  protected void SetHeight(Transform root, Enum _enum, int height)
  {
    this.SetHeight(this.FindCtrl(root, _enum), height);
  }

  protected void SetHeight(Transform t, int height)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIWidget>().height = height;
  }

  protected void SetCellWidth(Enum ctrl_enum, int width, bool reposition = false)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIGrid component = ((Component) ctrl).GetComponent<UIGrid>();
    component.cellWidth = (float) width;
    if (!reposition)
      return;
    component.Reposition();
  }

  private UILabel _GetLabel(Transform root, Enum label_enum)
  {
    return ((Component) (Object.op_Equality((Object) root, (Object) null) ? this.GetCtrl(label_enum) : this.FindCtrl(root, label_enum)))?.GetComponent<UILabel>();
  }

  protected void SetSupportEncoding(Transform root, Enum label_enum, bool isEnable)
  {
    UILabel label = this._GetLabel(root, label_enum);
    if (label == null)
      return;
    label.supportEncoding = isEnable;
  }

  protected void SetSupportEncoding(Enum label_enum, bool isEnable)
  {
    UILabel label = this._GetLabel((Transform) null, label_enum);
    if (label == null)
      return;
    label.supportEncoding = isEnable;
  }

  protected bool IsSupportEncoding(Enum label_enum)
  {
    UILabel label = this._GetLabel((Transform) null, label_enum);
    return label != null && label.supportEncoding;
  }

  protected string GetLabel(Transform root, Enum label_enum)
  {
    UILabel label = this._GetLabel(root, label_enum);
    return Object.op_Equality((Object) label, (Object) null) ? string.Empty : label.text;
  }

  protected void SetLabelText(Enum label_enum, object obj)
  {
    this.SetLabelText(this.GetCtrl(label_enum), obj.ToString());
  }

  protected void SetLabelText(Enum label_enum, string text)
  {
    this.SetLabelText(this.GetCtrl(label_enum), text);
  }

  protected void SetLabelText(Transform root, Enum label_enum, string text)
  {
    this.SetLabelText(this.FindCtrl(root, label_enum), text);
  }

  protected void SetLabelText(Transform root, Enum label_enum, object obj)
  {
    this.SetLabelText(this.FindCtrl(root, label_enum), obj.ToString());
  }

  protected void SetLabelText(Transform t, string text)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UILabel>().text = text;
  }

  protected string GetLabelText(Enum label_enum) => this.GetLabelText(this.GetCtrl(label_enum));

  protected string GetLabelText(Transform root, Enum label_enum)
  {
    return this.GetLabelText(this.FindCtrl(root, label_enum));
  }

  protected string GetLabelText(Transform t)
  {
    return Object.op_Equality((Object) t, (Object) null) ? "" : ((Component) t).GetComponent<UILabel>().text;
  }

  protected void SetText(Enum label_enum, string key)
  {
    this.SetText(this.GetCtrl(label_enum), key);
  }

  protected void SetText(Transform root, Enum label_enum, string key)
  {
    this.SetText(this.FindCtrl(root, label_enum), key);
  }

  protected void SetText(Transform t, string key)
  {
    if (Object.op_Equality((Object) t, (Object) null) || this.sectionData == (GameSceneTables.SectionData) null)
      return;
    ((Component) t).GetComponent<UILabel>().text = this.sectionData.GetText(key);
  }

  protected void SetLevelText(Enum label_enum, int level, int digit = 3)
  {
    Transform ctrl = this.GetCtrl(label_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UILabel component = ((Component) ctrl).GetComponent<UILabel>();
    component.supportEncoding = true;
    string str = level.ToString();
    int count = digit - str.Length;
    if (count <= 0)
    {
      component.supportEncoding = false;
      component.text = str;
    }
    else
    {
      component.supportEncoding = true;
      component.text = new string('0', count) + str;
    }
  }

  protected void SetApplicationVersionText(Enum label_enum)
  {
    this.SetLabelText(label_enum, StringTable.Format(STRING_CATEGORY.COMMON, 3U, (object) NetworkNative.getNativeVersionName()));
  }

  protected void SetMaterialNumText(
    Transform root,
    Enum have_enum,
    Enum need_enum,
    int have_num,
    int need_num)
  {
    UIBehaviour.SetMaterialNumText(this.FindCtrl(root, have_enum), this.FindCtrl(root, need_enum), have_num, need_num);
  }

  protected void SetMaterialNumText(Enum have_enum, Enum need_enum, int have_num, int need_num)
  {
    UIBehaviour.SetMaterialNumText(this.GetCtrl(have_enum), this.GetCtrl(need_enum), have_num, need_num);
  }

  public static void SetMaterialNumText(
    Transform have_t,
    Transform need_t,
    int have_num,
    int need_num)
  {
    if (Object.op_Equality((Object) have_t, (Object) null) || Object.op_Equality((Object) need_t, (Object) null))
      return;
    UILabel component1 = ((Component) have_t).GetComponent<UILabel>();
    UILabel component2 = ((Component) need_t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component1, (Object) null) || Object.op_Equality((Object) component2, (Object) null))
      return;
    ((Component) component2).GetComponent<UIWidget>().color = have_num < need_num ? Color.red : Color32.op_Implicit(UIBehaviour.buffGreen);
    component1.text = have_num.ToString();
    component2.text = need_num.ToString();
  }

  protected void SetFontStyle(Transform root, Enum label_enum, FontStyle font_style)
  {
    this.SetFontStyle(this.FindCtrl(root, label_enum), font_style);
  }

  protected void SetFontStyle(Enum label_enum, FontStyle font_style)
  {
    this.SetFontStyle(this.GetCtrl(label_enum), font_style);
  }

  protected void SetFontStyle(Transform t, FontStyle font_style)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UILabel component = ((Component) t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.fontStyle = font_style;
  }

  protected void SetStatusBuffText(Enum label_enum, int value, bool expression_include)
  {
    this.SetStatusBuffText(this.GetCtrl(label_enum), value, expression_include);
  }

  protected void SetStatusBuffText(
    Transform root,
    Enum label_enum,
    int value,
    bool expression_include)
  {
    this.SetStatusBuffText(this.FindCtrl(root, label_enum), value, expression_include);
  }

  protected void SetStatusBuffText(Transform t, int value, bool expression_include)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    uint id = value < 0 ? 1U : 0U;
    if (!expression_include)
      id += 2U;
    string str = string.Format(StringTable.Get(STRING_CATEGORY.STATUS, id), (object) value.ToString());
    UILabel component = ((Component) t).GetComponent<UILabel>();
    component.text = str;
    if (value < 0)
      component.color = Color.red;
    else
      component.color = Color32.op_Implicit(UIBehaviour.buffGreen);
  }

  protected void SetInput(
    Enum input_enum,
    string text,
    int char_limit,
    EventDelegate.Callback on_change = null)
  {
    this.SetInput(this.GetCtrl(input_enum), text, char_limit, text, on_change);
  }

  protected void SetInput(
    Transform root,
    Enum input_enum,
    string text,
    int char_limit,
    EventDelegate.Callback on_change = null)
  {
    this.SetInput(this.FindCtrl(root, input_enum), text, char_limit, text, on_change);
  }

  protected void SetInput(
    Transform t,
    string text,
    int char_limit,
    string defaultText,
    EventDelegate.Callback on_change = null)
  {
    if (Object.op_Equality((Object) t, (Object) null) || !this.uiFirstUpdate)
      return;
    UIInput component = ((Component) t).GetComponent<UIInput>();
    component.value = text;
    component.defaultText = defaultText;
    component.characterLimit = char_limit;
    if (on_change == null)
      return;
    EventDelegate.Add(component.onChange, on_change);
    on_change();
  }

  protected void SetInputValue(Enum input_enum, string value)
  {
    this.SetInputValue(this.GetCtrl(input_enum), value);
  }

  protected void SetInputValue(Transform root, Enum input_enum, string value)
  {
    this.SetInputValue(this.FindCtrl(root, input_enum), value);
  }

  protected void SetInputValue(Transform t, string value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIInput>().value = value;
  }

  protected void SetInputLabel(Enum input_enum, string value)
  {
    this.SetInputLabel(this.GetCtrl(input_enum), value);
  }

  protected void SetInputLabel(Transform root, Enum input_enum, string value)
  {
    this.SetInputLabel(this.FindCtrl(root, input_enum), value);
  }

  protected void SetInputLabel(Transform t, string value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIInput component = ((Component) t).GetComponent<UIInput>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Inequality((Object) component.label, (Object) null))
      return;
    component.label.text = value;
  }

  protected void SetInputSubmitEvent(Enum elm, EventDelegate eventDelegate)
  {
    this.SetInputSubmitEvent(this.GetCtrl(elm), eventDelegate);
  }

  protected void SetInputSubmitEvent(Transform t, Enum elm, EventDelegate eventDelegate)
  {
    this.SetInputSubmitEvent(this.FindCtrl(t, elm), eventDelegate);
  }

  protected void SetInputSubmitEvent(Transform t, EventDelegate eventDelegate)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIInput component = ((Component) t).GetComponent<UIInput>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.onSubmit.Clear();
    component.onSubmit.Add(eventDelegate);
  }

  protected string GetInputValue(Enum input_enum) => this.GetInputValue(this.GetCtrl(input_enum));

  protected string GetInputValue(Transform root, Enum input_enum)
  {
    return this.GetInputValue(this.FindCtrl(root, input_enum));
  }

  protected string GetInputValue(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return string.Empty;
    UIInput component = ((Component) t).GetComponent<UIInput>();
    string inputValue = component.value;
    if (component.label.maxLineCount == 1)
      inputValue = inputValue.Replace("\\n", "").Replace("\n", "").Replace("\r", "");
    return inputValue;
  }

  protected void SetSliderValue(Enum input_enum, float value)
  {
    this.SetSliderValue(this.GetCtrl(input_enum), value);
  }

  protected void SetSliderValue(Transform root, Enum input_enum, float value)
  {
    this.SetSliderValue(this.FindCtrl(root, input_enum), value);
  }

  protected void SetSliderValue(Transform t, float value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UISlider>().value = value;
  }

  protected void SetColor(Enum label_enum, Color color)
  {
    this.SetColor(this.GetCtrl(label_enum), color);
  }

  protected void SetColor(Transform root, Enum label_enum, Color color)
  {
    this.SetColor(this.FindCtrl(root, label_enum), color);
  }

  protected void SetColor(Transform t, Color color)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIWidget>().color = color;
  }

  protected Color GetColor(Enum label_enum) => this.GetColor(this.GetCtrl(label_enum));

  protected Color GetColor(Transform root, Enum label_enum)
  {
    return this.GetColor(this.FindCtrl(root, label_enum));
  }

  protected Color GetColor(Transform t)
  {
    return Object.op_Equality((Object) t, (Object) null) ? Color.white : ((Component) t).GetComponent<UIWidget>().color;
  }

  protected void SetToggle(Enum toggle_enum, bool value)
  {
    this.SetToggle(this.GetCtrl(toggle_enum), value);
  }

  protected void SetToggle(Transform root, Enum toggle_enum, bool value)
  {
    this.SetToggle(this.FindCtrl(root, toggle_enum), value);
  }

  protected void SetToggle(Transform t, bool value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIToggle>().value = value;
  }

  protected void SetToggleGroup(Enum toggle_enum, int value)
  {
    this.SetToggleGroup(this.GetCtrl(toggle_enum), value);
  }

  protected void SetToggleGroup(Transform root, Enum toggle_enum, int value)
  {
    this.SetToggleGroup(this.FindCtrl(root, toggle_enum), value);
  }

  protected void SetToggleGroup(Transform t, int value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIToggle>().group = value;
  }

  protected void SetToggleStartsActive(Enum toggle_enum, bool value)
  {
    this.SetToggleStartsActive(this.GetCtrl(toggle_enum), value);
  }

  protected void SetToggleStartsActive(Transform root, Enum toggle_enum, bool value)
  {
    this.SetToggleStartsActive(this.FindCtrl(root, toggle_enum), value);
  }

  protected void SetToggleStartsActive(Transform t, bool value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIToggle>().startsActive = value;
  }

  protected void SetToggleButton(Enum toggle_enum, bool is_active, Action<bool> on_changed = null)
  {
    this.SetToggleButton(this.GetCtrl(toggle_enum), is_active, on_changed);
  }

  protected void SetToggleButton(
    Transform root,
    Enum toggle_enum,
    bool is_active,
    Action<bool> on_changed = null)
  {
    this.SetToggleButton(this.FindCtrl(root, toggle_enum), is_active, on_changed);
  }

  protected void SetToggleButton(Transform t, bool is_active, Action<bool> on_changed = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIToggleButton component = ((Component) t).GetComponent<UIToggleButton>();
    component.isActive = is_active;
    component.onChanged = on_changed;
    component.Initialize();
  }

  protected bool IsToggleActive(Enum toggle_enum) => this.IsToggleActive(this.GetCtrl(toggle_enum));

  protected bool IsToggleActive(Transform root, Enum toggle_enum)
  {
    return this.IsToggleActive(this.FindCtrl(root, toggle_enum));
  }

  protected bool IsToggleActive(Transform t)
  {
    return !Object.op_Equality((Object) t, (Object) null) && ((Component) t).GetComponent<UIToggleButton>().isActive;
  }

  protected void SetSprite(Enum sprite_enum, string sprite_name)
  {
    this.SetSprite(this.GetCtrl(sprite_enum), sprite_name);
  }

  protected void SetSprite(Transform root, Enum sprite_enum, string sprite_name)
  {
    this.SetSprite(this.FindCtrl(root, sprite_enum), sprite_name);
  }

  protected void SetSprite(Transform t, string sprite_name)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UISprite>().spriteName = sprite_name;
  }

  protected void SetButtonSprite(Enum sprite_enum, string sprite_name, bool with_press = false)
  {
    this.SetButtonSprite(this.GetCtrl(sprite_enum), sprite_name, with_press);
  }

  protected void SetButtonSprite(
    Transform root,
    Enum sprite_enum,
    string sprite_name,
    bool with_press = false)
  {
    this.SetButtonSprite(this.FindCtrl(root, sprite_enum), sprite_name, with_press);
  }

  protected void SetButtonSprite(Transform t, string sprite_name, bool with_press = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton component = ((Component) t).GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (with_press)
      component.pressedSprite = sprite_name;
    component.normalSprite = sprite_name;
  }

  protected void SetButtonEvent(Enum elm, EventDelegate eventDelegate)
  {
    this.SetButtonEvent(this.GetCtrl(elm), eventDelegate);
  }

  protected void SetButtonEvent(Transform t, Enum elm, EventDelegate eventDelegate)
  {
    this.SetButtonEvent(this.FindCtrl(t, elm), eventDelegate);
  }

  protected void SetButtonEvent(Transform t, EventDelegate eventDelegate)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton component = ((Component) t).GetComponent<UIButton>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.onClick.Clear();
    component.onClick.Add(eventDelegate);
  }

  protected void SetTexture(Enum texture_enum, Texture texture)
  {
    this.SetTexture(this.GetCtrl(texture_enum), texture);
  }

  protected void SetTexture(Transform root, Enum texture_enum, Texture texture)
  {
    this.SetTexture(this.FindCtrl(root, texture_enum), texture);
  }

  protected void SetTexture(Transform t, Texture texture)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UITexture>().mainTexture = texture;
  }

  protected void SetDownloadTexture(Enum texture_enum, string url)
  {
    this.SetDownloadTexture(this.GetCtrl(texture_enum), url);
  }

  protected void SetDownloadTexture(Transform root, Enum texture_enum, string url)
  {
    this.SetDownloadTexture(this.FindCtrl(root, texture_enum), url);
  }

  protected void SetDownloadTexture(Transform t, string url)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIDownloadTexture>().url = url;
  }

  protected void SetButtonEnabled(Enum button_enum, bool is_enabled)
  {
    this.SetButtonEnabled(this.GetCtrl(button_enum), is_enabled);
  }

  protected void SetButtonEnabled(Transform root, Enum button_enum, bool is_enabled)
  {
    this.SetButtonEnabled(this.FindCtrl(root, button_enum), is_enabled);
  }

  protected void SetButtonEnabled(Transform t, bool is_enabled)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton component = ((Component) t).gameObject.GetComponent<UIButton>();
    component.isEnabled = is_enabled;
    if (!this.uiUpdateInstant || is_enabled)
      return;
    component.UpdateColor(true);
  }

  protected void SetButtonEnabled(Enum button_enum, bool is_enabled, bool is_update_child_label)
  {
    this.SetButtonEnabled(this.GetCtrl(button_enum), is_enabled, is_update_child_label);
  }

  protected void SetButtonEnabled(
    Transform root,
    Enum button_enum,
    bool is_enabled,
    bool is_update_child_label)
  {
    this.SetButtonEnabled(this.FindCtrl(root, button_enum), is_enabled, is_update_child_label);
  }

  protected void SetButtonEnabled(Transform t, bool is_enabled, bool is_update_child_label)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton button = ((Component) t).gameObject.GetComponent<UIButton>();
    button.isEnabled = is_enabled;
    if (!this.uiUpdateInstant || is_enabled)
      return;
    button.UpdateColor(true);
    if (!is_update_child_label)
      return;
    Array.ForEach<UILabel>(((Component) t).GetComponentsInChildren<UILabel>(), (Action<UILabel>) (child => child.color = button.disabledColor));
  }

  protected void SetButtonColliderEnabled(Enum button_enum, bool is_enabled)
  {
    this.SetButtonColliderEnabled(this.GetCtrl(button_enum), is_enabled);
  }

  protected void SetButtonColliderEnabled(Transform root, Enum button_enum, bool is_enabled)
  {
    this.SetButtonColliderEnabled(this.FindCtrl(root, button_enum), is_enabled);
  }

  protected void SetButtonColliderEnabled(Transform t, bool is_enabled)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton component = ((Component) t).gameObject.GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.isEnabled = is_enabled;
    ((Behaviour) component).enabled = is_enabled;
    component.SetState(UIButtonColor.State.Normal, true);
  }

  protected void SetButtonColor(Enum button_enum, bool is_enabled, bool is_instant)
  {
    this.SetButtonColor(this.GetCtrl(button_enum), is_enabled, is_instant);
  }

  protected void SetButtonColor(
    Transform root,
    Enum button_enum,
    bool is_enabled,
    bool is_instant)
  {
    this.SetButtonColor(this.FindCtrl(root, button_enum), is_enabled, is_instant);
  }

  protected void SetButtonColor(Transform t, bool is_enabled, bool is_instant)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButton component = ((Component) t).gameObject.GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    ((Component) component).GetComponent<UIButtonColor>().SetState(component.state, false);
    if (is_enabled)
      component.ResetDefaultColor();
    else
      component.defaultColor = component.disabledColor;
    component.hover = component.defaultColor;
    component.UpdateColor(is_instant);
  }

  protected void SetLongTouch(Enum button_enum, string event_name, object event_data = null)
  {
    this.SetLongTouch(this.GetCtrl(button_enum), event_name, event_data);
  }

  protected void SetLongTouch(
    Transform root,
    Enum button_enum,
    string event_name,
    object event_data = null)
  {
    this.SetLongTouch(this.FindCtrl(root, button_enum), event_name, event_data);
  }

  protected void SetLongTouch(Transform t, string event_name, object event_data = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UILongTouch.Set(((Component) t).gameObject, event_name, event_data);
  }

  protected void SetRepeatButton(Enum button_enum, string event_name, object event_data = null)
  {
    this.SetRepeatButton(this.GetCtrl(button_enum), event_name, event_data);
  }

  protected void SetRepeatButton(
    Transform root,
    Enum button_enum,
    string event_name,
    object event_data = null)
  {
    this.SetRepeatButton(this.FindCtrl(root, button_enum), event_name, event_data);
  }

  protected void SetRepeatButton(Transform t, string event_name, object event_data = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButtonRepeater.SetRepeatButton(((Component) t).gameObject, event_name, event_data);
  }

  protected void TerminateRepeatButton(Enum button_enum)
  {
    this.TerminateRepeatButton(this.GetCtrl(button_enum));
  }

  protected void TerminateRepeatButton(Transform root, Enum button_enum)
  {
    this.TerminateRepeatButton(this.FindCtrl(root, button_enum));
  }

  protected void TerminateRepeatButton(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIButtonRepeater component = ((Component) t).GetComponent<UIButtonRepeater>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Terminate();
  }

  protected void SetTouchAndRelease(
    Enum button_enum,
    string touch_event_name,
    string release_event_name = null,
    object event_data = null)
  {
    this.SetTouchAndRelease(this.GetCtrl(button_enum), touch_event_name, release_event_name, event_data);
  }

  protected void SetTouchAndRelease(
    Transform t,
    string touch_event_name,
    string release_event_name = null,
    object event_data = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITouchAndRelease.Set(((Component) t).gameObject, touch_event_name, release_event_name, event_data);
  }

  protected void NoEventReleaseTouchAndRelease(Enum button_enum)
  {
    this.NoEventReleaseTouchAndRelease(this.GetCtrl(button_enum));
  }

  protected void NoEventReleaseTouchAndRelease(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITouchAndRelease.NoEventRelease(((Component) t).gameObject);
  }

  protected void SetFullScreenButton(Transform root, Enum button_enum)
  {
    this.SetFullScreenButton(this.FindCtrl(root, button_enum));
  }

  protected void SetFullScreenButton(Enum button_enum)
  {
    this.SetFullScreenButton(this.GetCtrl(button_enum));
  }

  protected void SetFullScreenButton(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIWidget>().autoResizeBoxCollider = false;
    BoxCollider component = ((Component) t).GetComponent<BoxCollider>();
    component.size = new Vector3((float) (MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth * 2), (float) (MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight * 2), component.size.z);
  }

  protected void SetBadge(
    Enum button_enum,
    int num,
    SpriteAlignment align,
    int offset_x = 5,
    int offset_y = 5,
    bool is_scale_normalize = false)
  {
    if (Object.op_Equality((Object) MonoBehaviourSingleton<UIManager>.I.common, (Object) null))
      return;
    this.SetBadge(this.GetCtrl(button_enum), num, align, offset_x, offset_y, is_scale_normalize);
  }

  protected void SetBadge(
    Transform t,
    int num,
    SpriteAlignment align,
    int offset_x = 5,
    int offset_y = 5,
    bool is_scale_normalize = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.common.AttachBadge(((Component) t).GetComponent<UIWidget>(), num, align, offset_x, offset_y, is_scale_normalize);
  }

  protected void SetVisibleWidgetEffect(Enum widget_enum, string ui_effect_name)
  {
    this.SetVisibleWidgetEffect((Transform) null, this.GetCtrl(widget_enum), ui_effect_name);
  }

  protected void SetVisibleWidgetEffect(Enum panel_enum, Enum widget_enum, string ui_effect_name)
  {
    this.SetVisibleWidgetEffect(this.GetCtrl(panel_enum), this.GetCtrl(widget_enum), ui_effect_name);
  }

  protected void SetVisibleWidgetEffect(
    Enum panel_enum,
    Transform root,
    Enum widget_enum,
    string ui_effect_name)
  {
    this.SetVisibleWidgetEffect(this.GetCtrl(panel_enum), this.FindCtrl(root, widget_enum), ui_effect_name);
  }

  protected void SetVisibleWidgetEffect(
    Transform t_panel,
    Transform t_widget,
    string ui_effect_name)
  {
    if (Object.op_Equality((Object) t_widget, (Object) null))
      return;
    UIVisibleWidgetEffect.Set(Object.op_Inequality((Object) t_panel, (Object) null) ? ((Component) t_panel).GetComponent<UIPanel>() : (UIPanel) null, ((Component) t_widget).GetComponent<UIWidget>(), ui_effect_name, this.sectionData != (GameSceneTables.SectionData) null ? this.sectionData.sectionName : (string) null);
  }

  protected void SetVisibleWidgetOneShotEffect(
    Transform t_panel,
    Transform t_widget,
    string ui_effect_name)
  {
    if (Object.op_Equality((Object) t_widget, (Object) null))
      return;
    UIVisibleWidgetEffect.OneShot(Object.op_Inequality((Object) t_panel, (Object) null) ? ((Component) t_panel).GetComponent<UIPanel>() : (UIPanel) null, ((Component) t_widget).GetComponent<UIWidget>(), ui_effect_name, this.sectionData != (GameSceneTables.SectionData) null ? this.sectionData.sectionName : (string) null);
  }

  protected void SetEventName(Enum ctrl_enum, string event_name)
  {
    this.SetEventName(this.GetCtrl(ctrl_enum), event_name);
  }

  protected void SetEventName(Transform root, Enum ctrl_enum, string event_name)
  {
    this.SetEventName(this.FindCtrl(root, ctrl_enum), event_name);
  }

  protected void SetEventName(Transform t, string event_name)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIGameSceneEventSender>().eventName = event_name;
  }

  protected void SetEvent(Enum ctrl_enum, string event_name, int event_data)
  {
    this.SetEvent(this.GetCtrl(ctrl_enum), event_name, event_data);
  }

  protected void SetEvent(Transform root, Enum ctrl_enum, string event_name, int event_data)
  {
    this.SetEvent(this.FindCtrl(root, ctrl_enum), event_name, event_data);
  }

  protected void SetEvent(Transform root, Enum ctrl_enum, string event_name, object event_data)
  {
    this.SetEvent(this.FindCtrl(root, ctrl_enum), event_name, event_data);
  }

  protected void SetEvent(Transform t, string event_name, int event_data)
  {
    this.SetEvent(t, event_name, (object) event_data);
  }

  protected void SetEvent(Transform t, string event_name, object event_data)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIGameSceneEventSender sceneEventSender = ((Component) t).GetComponent<UIGameSceneEventSender>();
    if (Object.op_Equality((Object) sceneEventSender, (Object) null))
      sceneEventSender = ((Component) t).gameObject.AddComponent<UIGameSceneEventSender>();
    sceneEventSender.eventName = event_name;
    sceneEventSender.eventData = event_data;
  }

  protected void MoveRelativeScrollView(Enum ctrl_enum, Vector3 value)
  {
    this.MoveRelativeScrollView(this.GetCtrl(ctrl_enum), value);
  }

  protected void MoveRelativeScrollView(Transform root, Enum ctrl_enum, Vector3 value)
  {
    this.MoveRelativeScrollView(this.FindCtrl(root, ctrl_enum), value);
  }

  protected void MoveRelativeScrollView(Transform t, Vector3 _pos)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIScrollView>().MoveRelative(_pos);
  }

  protected void SetScroll(Enum ctrl_enum, float value)
  {
    this.SetScroll(this.GetCtrl(ctrl_enum), value);
  }

  protected void SetScroll(Transform root, Enum ctrl_enum, float value)
  {
    this.SetScroll(this.FindCtrl(root, ctrl_enum), value);
  }

  protected void SetScroll(Transform t, float value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIScrollView>().Scroll(value);
  }

  protected void ScrollViewResetPosition(Enum ctrl_enum)
  {
    this.ScrollViewResetPosition(this.GetCtrl(ctrl_enum));
  }

  protected void ScrollViewResetPosition(Transform root, Enum ctrl_enum)
  {
    this.ScrollViewResetPosition(this.FindCtrl(root, ctrl_enum));
  }

  protected void ScrollViewResetPosition(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIScrollView component = ((Component) t).GetComponent<UIScrollView>();
    bool enabled = ((Behaviour) component).enabled;
    ((Behaviour) component).enabled = true;
    component.ResetPosition();
    ((Behaviour) component).enabled = enabled;
  }

  protected bool IsScrollDragging(Enum ctrl_enum) => this.IsScrollDragging(this.GetCtrl(ctrl_enum));

  protected bool IsScrollDragging(Transform root, Enum ctrl_enum)
  {
    return this.IsScrollDragging(this.FindCtrl(root, ctrl_enum));
  }

  protected bool IsScrollDragging(Transform t)
  {
    return !Object.op_Equality((Object) t, (Object) null) && ((Component) t).GetComponent<UIScrollView>().isDragging;
  }

  protected void SetProgressValue(Enum ctrl_enum, float value)
  {
    this.SetProgressValue(this.GetCtrl(ctrl_enum), value);
  }

  protected void SetProgressValue(Transform root, Enum ctrl_enum, float value)
  {
    this.SetProgressValue(this.FindCtrl(root, ctrl_enum), value);
  }

  protected void SetProgressValue(Transform t, float value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIProgressBar>().value = value;
  }

  protected void SetProgressSteps(Enum ctrl_enum, int value)
  {
    this.SetProgressSteps(this.GetCtrl(ctrl_enum), value);
  }

  protected void SetProgressSteps(Transform root, Enum ctrl_enum, int value)
  {
    this.SetProgressSteps(this.FindCtrl(root, ctrl_enum), value);
  }

  protected void SetProgressSteps(Transform t, int value)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).GetComponent<UIProgressBar>().numberOfSteps = value;
  }

  protected void SetProgressOnChange(Enum ctrl_enum, EventDelegate.Callback on_change)
  {
    this.SetProgressOnChange(this.GetCtrl(ctrl_enum), on_change);
  }

  protected void SetProgressOnChange(
    Transform root,
    Enum ctrl_enum,
    EventDelegate.Callback on_change)
  {
    this.SetProgressOnChange(this.FindCtrl(root, ctrl_enum), on_change);
  }

  protected void SetProgressOnChange(Transform t, EventDelegate.Callback on_change)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    EventDelegate.Add(((Component) t).GetComponent<UIProgressBar>().onChange, on_change);
  }

  protected void SetProgressInt(
    Enum ctrl_enum,
    int val,
    int min = -1,
    int max = -1,
    EventDelegate.Callback on_change = null)
  {
    this.SetProgressInt(this.GetCtrl(ctrl_enum), val, min, max, on_change);
  }

  protected void SetProgressInt(
    Transform root,
    Enum ctrl_enum,
    int val,
    int min = -1,
    int max = -1,
    EventDelegate.Callback on_change = null)
  {
    this.SetProgressInt(this.FindCtrl(root, ctrl_enum), val, min, max, on_change);
  }

  protected void SetProgressInt(
    Transform t,
    int val,
    int min = -1,
    int max = -1,
    EventDelegate.Callback on_change = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIProgressWork uiProgressWork = ((Component) t).GetComponent<UIProgressWork>();
    if (Object.op_Equality((Object) uiProgressWork, (Object) null))
      uiProgressWork = ((Component) t).gameObject.AddComponent<UIProgressWork>();
    if (max > -1)
      uiProgressWork.maxValue = max;
    if (min > -1)
      uiProgressWork.minValue = min;
    if (val > -1)
      uiProgressWork.value = val;
    if (on_change == null)
      return;
    EventDelegate.Add(uiProgressWork.progress.onChange, on_change);
    on_change();
  }

  protected int GetProgressInt(Enum ctrl_enum) => this.GetProgressInt(this.GetCtrl(ctrl_enum));

  protected int GetProgressInt(Transform root, Enum ctrl_enum)
  {
    return this.GetProgressInt(this.FindCtrl(root, ctrl_enum));
  }

  protected int GetProgressInt(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return 0;
    UIProgressWork component = ((Component) t).GetComponent<UIProgressWork>();
    return Object.op_Equality((Object) component, (Object) null) ? 0 : component.value;
  }

  public void SetCenterOnChildFunc(Enum ctrl_enum, UICenterOnChild.OnCenterCallback func)
  {
    this.SetCenterOnChildFunc(this.GetCtrl(ctrl_enum), func);
  }

  public void SetCenterOnChildFunc(
    Transform root,
    Enum ctrl_enum,
    UICenterOnChild.OnCenterCallback func)
  {
    this.SetCenterOnChildFunc(this.FindCtrl(root, ctrl_enum), func);
  }

  public void SetCenterOnChildFunc(Transform t, UICenterOnChild.OnCenterCallback func)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UICenterOnChildCtrl.Get(((Component) t).gameObject).onCenter = func;
  }

  protected void SetCenterOnChildFunc(Enum ctrl_enum, SpringPanel.OnFinished func)
  {
    this.SetCenterOnChildFunc(this.GetCtrl(ctrl_enum), func);
  }

  protected void SetCenterOnChildFunc(Transform root, Enum ctrl_enum, SpringPanel.OnFinished func)
  {
    this.SetCenterOnChildFunc(this.FindCtrl(root, ctrl_enum), func);
  }

  protected void SetCenterOnChildFunc(Transform t, SpringPanel.OnFinished func)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UICenterOnChildCtrl.Get(((Component) t).gameObject).onFinished = func;
  }

  protected void SetPopupListOnChange(
    Enum ctrl_enum,
    Enum ctrl_lbl_enum,
    EventDelegate.Callback call_back = null)
  {
    this.SetPopupListOnChange(this.GetCtrl(ctrl_enum), this.GetCtrl(ctrl_lbl_enum), call_back);
  }

  protected void SetPopupListOnChange(
    Transform t,
    Transform t_lbl,
    EventDelegate.Callback call_back = null)
  {
    if (Object.op_Equality((Object) t, (Object) null) || Object.op_Equality((Object) t_lbl, (Object) null))
      return;
    UIPopupList component1 = ((Component) t).GetComponent<UIPopupList>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return;
    UILabel component2 = ((Component) t_lbl).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component2, (Object) null))
      return;
    component1.onChange.Clear();
    EventDelegate.Add(component1.onChange, new EventDelegate.Callback(component2.SetCurrentSelection));
    if (call_back == null)
      return;
    EventDelegate.Add(component1.onChange, call_back);
  }

  protected void SetPopupListText(Enum ctrl_enum, List<string> string_list, int first_index = -1)
  {
    this.SetPopupListText(this.GetCtrl(ctrl_enum), string_list, first_index);
  }

  protected void SetPopupListText(Transform t, List<string> string_list, int first_index = -1)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIPopupList component = ((Component) t).GetComponent<UIPopupList>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (string_list == null)
    {
      component.items = new List<string>();
      component.value = string.Empty;
    }
    else
    {
      component.items = string_list;
      if (string_list.Count > 0)
      {
        if (first_index >= string_list.Count)
          first_index = string_list.Count - 1;
        if (first_index < 0)
          first_index = 0;
        component.value = string_list[first_index];
      }
      else
        component.value = string.Empty;
    }
  }

  protected void SetElementSprite(Transform root, Enum ctrl_enum, int elen_type)
  {
    this.SetElementSprite(this.FindCtrl(root, ctrl_enum), elen_type);
  }

  protected void SetElementSprite(Enum ctrl_enum, int elen_type)
  {
    this.SetElementSprite(this.GetCtrl(ctrl_enum), elen_type);
  }

  protected void SetElementSprite(Transform t, int elen_type)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UISprite component = ((Component) t).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    string elemSpriteName = UIBehaviour.GetElemSpriteName(elen_type);
    if (string.IsNullOrEmpty(elemSpriteName))
    {
      ((Behaviour) component).enabled = false;
    }
    else
    {
      ((Behaviour) component).enabled = true;
      this.SetSprite(t, elemSpriteName);
    }
  }

  protected void SetDefElementSprite(Transform root, Enum ctrl_enum, int elen_type)
  {
    this.SetDefElementSprite(this.FindCtrl(root, ctrl_enum), elen_type);
  }

  protected void SetDefElementSprite(Enum ctrl_enum, int elen_type)
  {
    this.SetDefElementSprite(this.GetCtrl(ctrl_enum), elen_type);
  }

  protected void SetDefElementSprite(Transform t, int elen_type)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UISprite component = ((Component) t).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    string elemDefSpriteName = UIBehaviour.GetElemDefSpriteName(elen_type);
    if (string.IsNullOrEmpty(elemDefSpriteName))
    {
      ((Behaviour) component).enabled = false;
    }
    else
    {
      ((Behaviour) component).enabled = true;
      this.SetSprite(t, elemDefSpriteName);
    }
  }

  public static string GetElemSpriteName(int elem_type)
  {
    if (elem_type == 6)
      return (string) null;
    if (elem_type >= 6)
      return (string) null;
    return elem_type == -1 ? UIBehaviour.elementSpriteName[UIBehaviour.elementSpriteName.Length - 1] : UIBehaviour.elementSpriteName[elem_type];
  }

  public static string GetElemDefSpriteName(int elem_type)
  {
    if (elem_type == 6)
      return (string) null;
    if (elem_type >= 6)
      return (string) null;
    return elem_type == -1 ? UIBehaviour.elementDefSpriteName[UIBehaviour.elementSpriteName.Length - 1] : UIBehaviour.elementDefSpriteName[elem_type];
  }

  protected bool SetCenter(Enum table_enum, int index, bool is_instant = false)
  {
    return this.SetCenter(this.GetCtrl(table_enum), index, is_instant);
  }

  protected bool SetCenter(Transform root, Enum table_enum, int index, bool is_instant = false)
  {
    return this.SetCenter(this.FindCtrl(root, table_enum), index, is_instant);
  }

  protected bool SetCenter(Transform t, int index, bool is_instant = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return false;
    UICenterOnChildCtrl centerOnChildCtrl = UICenterOnChildCtrl.Get(((Component) t).gameObject);
    if (Object.op_Equality((Object) centerOnChildCtrl, (Object) null) || index < 0 || t.childCount <= index)
      return false;
    if (this.uiUpdateInstant)
      is_instant = true;
    centerOnChildCtrl.Centering(t.GetChild(index), is_instant);
    return true;
  }

  protected Transform GetCenter(Enum table_enum) => this.GetCenter(this.GetCtrl(table_enum));

  protected bool SetCenter(Transform root, Enum table_enum)
  {
    return Object.op_Implicit((Object) this.GetCenter(this.FindCtrl(root, table_enum)));
  }

  protected Transform GetCenter(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return (Transform) null;
    UICenterOnChildCtrl centerOnChildCtrl = UICenterOnChildCtrl.Get(((Component) t).gameObject);
    return Object.op_Equality((Object) centerOnChildCtrl, (Object) null) ? (Transform) null : centerOnChildCtrl.lastTarget;
  }

  protected void SetScrollArrows(
    Transform parent,
    Enum prev_enum,
    Enum next_enum,
    int index,
    int length,
    Enum prev_btn_enum = null,
    Enum next_btn_enum = null)
  {
    Transform ctrl1 = prev_btn_enum != null ? this.FindCtrl(parent, prev_btn_enum) : (Transform) null;
    Transform ctrl2 = next_btn_enum != null ? this.FindCtrl(parent, next_btn_enum) : (Transform) null;
    this.SetScrollArrows(this.FindCtrl(parent, prev_enum), this.FindCtrl(parent, next_enum), index, length, ctrl1, ctrl2);
  }

  protected void SetScrollArrows(
    Enum prev_enum,
    Enum next_enum,
    int index,
    int length,
    Enum prev_btn_enum = null,
    Enum next_btn_enum = null)
  {
    Transform ctrl1 = prev_btn_enum != null ? this.GetCtrl(prev_btn_enum) : (Transform) null;
    Transform ctrl2 = next_btn_enum != null ? this.GetCtrl(next_btn_enum) : (Transform) null;
    this.SetScrollArrows(this.GetCtrl(prev_enum), this.GetCtrl(next_enum), index, length, ctrl1, ctrl2);
  }

  protected void SetScrollArrows(
    Transform prev,
    Transform next,
    int index,
    int length,
    Transform prev_btn = null,
    Transform next_btn = null)
  {
    bool is_enabled1 = index > 0;
    bool is_enabled2 = index + 1 < length;
    Transform t1 = prev_btn ?? prev;
    Transform t2 = next_btn ?? next;
    this.SetEnabled<UISprite>(prev, is_enabled1);
    this.SetButtonEnabled(t1, is_enabled1);
    this.SetEnabled<UISprite>(next, is_enabled2);
    this.SetButtonEnabled(t2, is_enabled2);
  }

  protected void SetActiveListIndex(Enum[] enums, int active_index)
  {
    if (enums == null || enums.Length == 0 || active_index >= enums.Length)
      return;
    int index = 0;
    for (int length = enums.Length; index < length; ++index)
      this.SetActive(enums[index], index == active_index);
  }

  public void SetTextTalk(
    Enum lbl_enum,
    List<string[]> texts,
    System.Action page_end_call_back = null,
    Action<string, string> tag_call_back = null,
    int num_per_sec = 0)
  {
    this.SetTextTalk(this.GetCtrl(lbl_enum), texts, page_end_call_back, tag_call_back, num_per_sec);
  }

  public void SetTextTalk(
    Transform root,
    Enum lbl_enum,
    List<string[]> texts,
    System.Action page_end_call_back = null,
    Action<string, string> tag_call_back = null,
    int num_per_sec = 0)
  {
    this.SetTextTalk(this.FindCtrl(root, lbl_enum), texts, page_end_call_back, tag_call_back, num_per_sec);
  }

  public void SetTextTalk(
    Transform t,
    List<string[]> texts,
    System.Action page_end_call_back = null,
    Action<string, string> tag_call_back = null,
    int num_per_sec = 0)
  {
    if (Object.op_Equality((Object) t, (Object) null) || Object.op_Equality((Object) ((Component) t).GetComponent<UILabel>(), (Object) null))
      return;
    TextTalk component = ((Component) t).GetComponent<TextTalk>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.Initialize(t, texts, page_end_call_back, tag_call_back, num_per_sec);
  }

  public TextTalk GetTextTalk(Enum lbl_enum) => this.GetTextTalk(this.GetCtrl(lbl_enum));

  public TextTalk GetTextTalk(Transform root, Enum lbl_enum)
  {
    return this.GetTextTalk(this.FindCtrl(root, lbl_enum));
  }

  public TextTalk GetTextTalk(Transform t) => ((Component) t).GetComponent<TextTalk>();

  protected void SetRenderPlayerModel(
    Enum ui_texture_enum,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    this.SetRenderPlayerModel(this.GetCtrl(ui_texture_enum), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void SetRenderPlayerModel(
    Transform root,
    Enum ui_texture_enum,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    this.SetRenderPlayerModel(this.FindCtrl(root, ui_texture_enum), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void SetRenderPlayerModel(
    Transform t,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitPlayer(((Component) t).GetComponent<UITexture>(), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void SetRenderPlayerModelOneShot(
    Transform root,
    Enum ui_texture_enum,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    Transform ctrl = this.FindCtrl(root, ui_texture_enum);
    if (!Object.op_Inequality((Object) ctrl, (Object) null))
      return;
    UIModelRenderTexture modelRenderTexture = UIModelRenderTexture.Get(ctrl);
    if (!Object.op_Inequality((Object) modelRenderTexture, (Object) null))
      return;
    modelRenderTexture.InitPlayerOneShot(((Component) ctrl).GetComponent<UITexture>(), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void ForceSetRenderPlayerModel(
    Enum ui_texture_enum,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    this.ForceSetRenderPlayerModel(this.GetCtrl(ui_texture_enum), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void ForceSetRenderPlayerModel(
    Transform root,
    Enum ui_texture_enum,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    this.ForceSetRenderPlayerModel(this.FindCtrl(root, ui_texture_enum), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void ForceSetRenderPlayerModel(
    Transform t,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
    {
      if (onload_callback == null)
        return;
      onload_callback((PlayerLoader) null);
    }
    else
      UIModelRenderTexture.Get(t).ForceInitPlayer(((Component) t).GetComponent<UITexture>(), info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  protected void SetRenderModel(Enum ui_texture_enum, SortCompareData data)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIModelRenderTexture.Get(ctrl).Init(((Component) ctrl).GetComponent<UITexture>(), data);
  }

  protected void SetRenderNPCModel(
    Enum ui_texture_enum,
    int npc_id,
    Vector3 pos,
    Vector3 rot,
    float fov = -1f,
    Action<NPCLoader> onload_callback = null)
  {
    this.SetRenderNPCModel(this.GetCtrl(ui_texture_enum), npc_id, pos, rot, fov, onload_callback);
  }

  protected void SetRenderNPCModel(
    Transform root,
    Enum ui_texture_enum,
    int npc_id,
    Vector3 pos,
    Vector3 rot,
    float fov = -1f,
    Action<NPCLoader> onload_callback = null)
  {
    this.SetRenderNPCModel(this.FindCtrl(root, ui_texture_enum), npc_id, pos, rot, fov, onload_callback);
  }

  protected void SetRenderNPCModel(
    Transform t,
    int npc_id,
    Vector3 pos,
    Vector3 rot,
    float fov = -1f,
    Action<NPCLoader> onload_callback = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitNPC(((Component) t).GetComponent<UITexture>(), npc_id, pos, rot, fov, onload_callback);
  }

  protected void SetRenderItemModel(Enum ui_texture_enum, uint item_id)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIModelRenderTexture.Get(ctrl).InitItem(((Component) ctrl).GetComponent<UITexture>(), item_id);
  }

  protected void SetRenderEquipModel(
    Transform root,
    Enum ui_texture_enum,
    uint equip_item_id,
    int sex_id = -1,
    int face_id = -1,
    float scale = 1f)
  {
    this.SetRenderEquipModel(this.FindCtrl(root, ui_texture_enum), equip_item_id, sex_id, face_id, scale);
  }

  protected void SetRenderEquipModel(
    Enum ui_texture_enum,
    uint equip_item_id,
    int sex_id = -1,
    int face_id = -1,
    float scale = 1f)
  {
    this.SetRenderEquipModel(this.GetCtrl(ui_texture_enum), equip_item_id, sex_id, face_id, scale);
  }

  protected void SetRenderEquipModel(
    Transform t,
    uint equip_item_id,
    int sex_id = -1,
    int face_id = -1,
    float scale = 1f)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitEquip(((Component) t).GetComponent<UITexture>(), equip_item_id, sex_id, face_id, scale);
  }

  protected void SetRenderSkillItemModel(
    Transform root,
    Enum ui_texture_enum,
    uint skill_item_id,
    bool rotation = true,
    bool light_rotation = false)
  {
    this.SetRenderSkillItemModel(this.FindCtrl(root, ui_texture_enum), skill_item_id, rotation, light_rotation);
  }

  protected void SetRenderSkillItemModel(
    Enum ui_texture_enum,
    uint skill_item_id,
    bool rotation = true,
    bool light_rotation = false)
  {
    this.SetRenderSkillItemModel(this.GetCtrl(ui_texture_enum), skill_item_id, rotation, light_rotation);
  }

  protected void SetRenderSkillItemModel(
    Transform t,
    uint skill_item_id,
    bool rotation = true,
    bool light_rotation = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitSkillItem(((Component) t).GetComponent<UITexture>(), skill_item_id, rotation, light_rotation);
  }

  protected void SetRenderSkillItemSymbolModel(
    Transform root,
    Enum ui_texture_enum,
    uint skill_item_id,
    bool rotation = true)
  {
    this.SetRenderSkillItemSymbolModel(this.FindCtrl(root, ui_texture_enum), skill_item_id, rotation);
  }

  protected void SetRenderSkillItemSymbolModel(
    Enum ui_texture_enum,
    uint skill_item_id,
    bool rotation = true)
  {
    this.SetRenderSkillItemSymbolModel(this.GetCtrl(ui_texture_enum), skill_item_id, rotation);
  }

  protected void SetRenderSkillItemSymbolModel(Transform t, uint skill_item_id, bool rotation = true)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITexture component = ((Component) t).GetComponent<UITexture>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitSkillItemSymbol(component, skill_item_id, rotation);
  }

  protected void SetRenderEnemyModel(
    Transform parent,
    Enum ui_texture_enum,
    uint enemy_id,
    string foundation_name,
    OutGameSettingsManager.EnemyDisplayInfo.SCENE target_scene,
    Action<bool, EnemyLoader> callback = null,
    UIModelRenderTexture.ENEMY_MOVE_TYPE moveType = UIModelRenderTexture.ENEMY_MOVE_TYPE.DEFULT,
    bool is_Howl = true)
  {
    this.SetRenderEnemyModel(this.FindCtrl(parent, ui_texture_enum), enemy_id, foundation_name, target_scene, callback, moveType, is_Howl);
  }

  protected void SetRenderEnemyModel(
    Enum ui_texture_enum,
    uint enemy_id,
    string foundation_name,
    OutGameSettingsManager.EnemyDisplayInfo.SCENE target_scene,
    Action<bool, EnemyLoader> callback = null,
    UIModelRenderTexture.ENEMY_MOVE_TYPE moveType = UIModelRenderTexture.ENEMY_MOVE_TYPE.DEFULT,
    bool is_Howl = true)
  {
    this.SetRenderEnemyModel(this.GetCtrl(ui_texture_enum), enemy_id, foundation_name, target_scene, callback, moveType, is_Howl);
  }

  protected void SetRenderEnemyModel(
    Transform t,
    uint enemy_id,
    string foundation_name,
    OutGameSettingsManager.EnemyDisplayInfo.SCENE target_scene,
    Action<bool, EnemyLoader> callback = null,
    UIModelRenderTexture.ENEMY_MOVE_TYPE moveType = UIModelRenderTexture.ENEMY_MOVE_TYPE.DEFULT,
    bool is_Howl = true)
  {
    if (Object.op_Equality((Object) t, (Object) null))
    {
      if (callback == null)
        return;
      callback(false, (EnemyLoader) null);
    }
    else
      UIModelRenderTexture.Get(t).InitEnemy(((Component) t).GetComponent<UITexture>(), enemy_id, foundation_name, target_scene, callback, moveType, is_Howl);
  }

  protected void SetRenderAccessoryModel(
    Transform root,
    Enum ui_texture_enum,
    uint accessory_id,
    float scale,
    bool rotation = true,
    bool light_rotation = false)
  {
    this.SetRenderAccessoryModel(this.FindCtrl(root, ui_texture_enum), accessory_id, scale, rotation, light_rotation);
  }

  protected void SetRenderAccessoryModel(
    Enum ui_texture_enum,
    uint accessory_id,
    float scale,
    bool rotation = true,
    bool light_rotation = false)
  {
    this.SetRenderAccessoryModel(this.GetCtrl(ui_texture_enum), accessory_id, scale, rotation, light_rotation);
  }

  protected void SetRenderAccessoryModel(
    Transform t,
    uint accessory_id,
    float scale,
    bool rotation = true,
    bool light_rotation = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture.Get(t).InitAccessory(((Component) t).GetComponent<UITexture>(), accessory_id, scale, rotation, light_rotation);
  }

  protected void ClearRenderModel(Transform parent, Enum ui_texture_enum)
  {
    this.ClearRenderModel(this.FindCtrl(parent, ui_texture_enum));
  }

  protected void ClearRenderModel(Enum ui_texture_enum)
  {
    this.ClearRenderModel(this.GetCtrl(ui_texture_enum));
  }

  protected void ClearRenderModel(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture component = ((Component) t).GetComponent<UIModelRenderTexture>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Clear();
  }

  protected void SetModel(Transform root, Enum ui_texture_enum, string name)
  {
    this.SetModel(this.FindCtrl(root, ui_texture_enum), name);
  }

  protected void SetModel(Enum ui_texture_enum, string name)
  {
    this.SetModel(this.GetCtrl(ui_texture_enum), name);
  }

  protected void SetModel(Transform t, string name)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModel.Get(t).Init(name);
  }

  protected void RemoveModel(Transform root, Enum ui_texture_enum)
  {
    this.RemoveModel(this.FindCtrl(root, ui_texture_enum));
  }

  protected void RemoveModel(Enum ui_texture_enum)
  {
    this.RemoveModel(this.GetCtrl(ui_texture_enum));
  }

  protected void RemoveModel(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModel.Get(t).Remove();
  }

  protected void SetActiveModel(Transform root, Enum ui_texture_enum, bool active)
  {
    this.SetActiveModel(this.FindCtrl(root, ui_texture_enum), active);
  }

  protected void SetActiveModel(Enum ui_texture_enum, bool active)
  {
    this.SetActiveModel(this.GetCtrl(ui_texture_enum), active);
  }

  protected void SetActiveModel(Transform t, bool active)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModel component = ((Component) t).GetComponent<UIModel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.SetActive(active);
  }

  public UIRenderTexture InitRenderTexture(Enum ui_texture_enum, float fov = -1f, bool link_main_camera = false)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return (UIRenderTexture) null;
    UIRenderTexture uiRenderTexture = UIRenderTexture.Get(((Component) ctrl).GetComponent<UITexture>(), fov, link_main_camera);
    if (Object.op_Inequality((Object) uiRenderTexture, (Object) null))
      uiRenderTexture.Disable();
    return uiRenderTexture;
  }

  protected void EnableRenderTexture(Enum ui_texture_enum)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIRenderTexture component = ((Component) ctrl).GetComponent<UIRenderTexture>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Enable();
  }

  protected void DeleteRenderTexture(Transform root, Enum ui_texture_enum)
  {
    this.DeleteRenderTexture(this.FindCtrl(root, ui_texture_enum));
  }

  protected void DeleteRenderTexture(Enum ui_texture_enum)
  {
    this.DeleteRenderTexture(this.GetCtrl(ui_texture_enum));
  }

  protected void DeleteRenderTexture(Transform t)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIModelRenderTexture component1 = ((Component) t).GetComponent<UIModelRenderTexture>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.Clear();
    UIRenderTexture component2 = ((Component) t).GetComponent<UIRenderTexture>();
    if (!Object.op_Inequality((Object) component2, (Object) null))
      return;
    Object.DestroyImmediate((Object) component2);
  }

  protected int GetRenderTextureLayer(Enum ui_texture_enum)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return 0;
    UIRenderTexture component = ((Component) ctrl).GetComponent<UIRenderTexture>();
    return Object.op_Equality((Object) component, (Object) null) ? 0 : component.renderLayer;
  }

  protected Transform GetRenderTextureModelTransform(Enum ui_texture_enum)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return (Transform) null;
    UIRenderTexture component = ((Component) ctrl).GetComponent<UIRenderTexture>();
    return Object.op_Equality((Object) component, (Object) null) ? (Transform) null : component.modelTransform;
  }

  protected void SetQuestLocationImage(
    Enum ui_texture_enum,
    int id,
    System.Action on_load_start = null,
    System.Action on_load_complete = null)
  {
    Transform ctrl = this.GetCtrl(ui_texture_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIQuestLocationImage.Set(((Component) ctrl).GetComponent<UITexture>(), id, on_load_start, on_load_complete);
  }

  protected void SetDirty(Enum ctrl_enum)
  {
    Transform ctrl = this.GetCtrl(ctrl_enum);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    ((Component) ctrl).gameObject.tag = "Dirty";
  }

  protected bool IsDirty(Enum ctrl_enum) => this.IsDirty(this.GetCtrl(ctrl_enum));

  protected bool IsDirty(Transform root, Enum ctrl_enum)
  {
    return this.IsDirty(this.FindCtrl(root, ctrl_enum));
  }

  protected bool IsDirty(Transform t)
  {
    return !Object.op_Equality((Object) t, (Object) null) && ((Component) t).gameObject.tag == "Dirty";
  }

  protected void SetSkillIconButton(
    Transform root,
    Enum ui_widget_enum,
    string skill_button_prefab_name,
    EquipItemTable.EquipItemData equip_item_table,
    SkillSlotUIData[] skill_tables,
    string button_event_name = "SKILL_ICON_BUTTON",
    int button_event_data = 0)
  {
    this.SettingSkillIconButton(this.FindCtrl(root, ui_widget_enum), skill_button_prefab_name, equip_item_table, skill_tables, button_event_name, button_event_data);
  }

  protected void SetSkillIconButton(
    Enum ui_widget_enum,
    string skill_button_prefab_name,
    EquipItemTable.EquipItemData equip_item_table,
    SkillSlotUIData[] skill_tables,
    string button_event_name = "SKILL_ICON_BUTTON",
    int button_event_data = 0)
  {
    this.SettingSkillIconButton(this.GetCtrl(ui_widget_enum), skill_button_prefab_name, equip_item_table, skill_tables, button_event_name, button_event_data);
  }

  protected void SettingSkillIconButton(
    Transform t,
    string prefab_name,
    EquipItemTable.EquipItemData equip_item_table,
    SkillSlotUIData[] slot_data,
    string button_event_name,
    int button_event_data)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).gameObject.SetActive(true);
    if (string.IsNullOrEmpty(prefab_name))
      return;
    Transform t1 = t.Find(prefab_name);
    if (Object.op_Equality((Object) t1, (Object) null))
    {
      UIBehaviour.PrefabData prefabData = this.GetPrefabData(prefab_name);
      if (prefabData == null)
      {
        Log.Error(LOG.UI, "{0} not found.", (object) prefab_name);
        return;
      }
      t1 = prefabData.Realizes(t);
    }
    Transform transform1 = t1.Find("SPR_BTN_ENABLE_BG");
    Transform transform2 = t1.Find("SPR_BTN_DISABLE_BG");
    Transform transform3 = transform1.Find("OBJ_SKILL_SLOT");
    if (equip_item_table == null || slot_data == null)
    {
      if (Object.op_Inequality((Object) t1, (Object) null))
      {
        this.SetEnabled<UIButton>(t1, false);
        int num = 0;
        for (int childCount = transform3.childCount; num < childCount; ++num)
        {
          Transform child = transform3.GetChild(num);
          if (Object.op_Inequality((Object) this.GetComponent<UISprite>(child), (Object) null))
            this.SetEnabled<UISprite>(child, false);
        }
      }
      ((Component) transform1).gameObject.SetActive(false);
      ((Component) transform2).gameObject.SetActive(true);
    }
    else
    {
      ((Component) transform1).gameObject.SetActive(true);
      ((Component) transform2).gameObject.SetActive(false);
      if (!string.IsNullOrEmpty(button_event_name))
      {
        this.SetEnabled<UIButton>(t1, true);
        this.SetEvent(t1, button_event_name, button_event_data);
        ((Collider) ((Component) t1).GetComponent<BoxCollider>()).enabled = true;
      }
      else
      {
        this.SetEnabled<UIButton>(t1, false);
        ((Collider) ((Component) t1).GetComponent<BoxCollider>()).enabled = false;
      }
      UIWidget component1 = this.GetComponent<UIWidget>(t1);
      if (!component1.isAnchored)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) (component1.width >> 1), (float) (component1.height >> 1));
        component1.SetAnchor(((Component) t).gameObject, (int) -(double) vector2.x, (int) -(double) vector2.y, (int) vector2.x, (int) vector2.y);
      }
      if (Object.op_Equality((Object) ((Component) t1).GetComponent<UIDragScrollView>(), (Object) null))
      {
        UIScrollView componentInParent = ((Component) t1).GetComponentInParent<UIScrollView>();
        if (Object.op_Inequality((Object) componentInParent, (Object) null))
        {
          ((Component) t1).gameObject.AddComponent<UIDragScrollView>().scrollView = componentInParent;
          if (Object.op_Equality((Object) ((Component) t1).GetComponent<UICenterOnClickChild>(), (Object) null))
            ((Component) t1).gameObject.AddComponent<UICenterOnClickChild>();
        }
      }
      int index = 0;
      for (int childCount = transform3.childCount; index < childCount; ++index)
      {
        Transform transform4 = transform3.Find(index.ToString());
        if (index < slot_data.Length)
        {
          ((Component) transform4).gameObject.SetActive(true);
          UISprite component2 = ((Component) transform4).GetComponent<UISprite>();
          ((Behaviour) component2).enabled = true;
          bool is_attached = slot_data[index].itemData != null && slot_data[index].slotData.slotType == slot_data[index].itemData.tableData.type;
          this.SetSkillIcon(component2, slot_data[index].slotData.slotType, is_attached, true);
        }
        else if (Object.op_Inequality((Object) transform4, (Object) null))
          ((Component) transform4).gameObject.SetActive(false);
      }
      UIGrid component3 = ((Component) transform3).GetComponent<UIGrid>();
      if (!Object.op_Inequality((Object) component3, (Object) null))
        return;
      component3.Reposition();
    }
  }

  public void SetSkillIcon(
    Enum ui_enum,
    SKILL_SLOT_TYPE slot_type,
    bool is_attached,
    bool is_button_icon)
  {
    this.SetSkillIcon(this.GetComponent<UISprite>(ui_enum), slot_type, is_attached, is_button_icon);
  }

  public void SetSkillIcon(
    Transform root,
    Enum ui_enum,
    SKILL_SLOT_TYPE slot_type,
    bool is_attached,
    bool is_button_icon)
  {
    this.SetSkillIcon(this.GetComponent<UISprite>(root, ui_enum), slot_type, is_attached, is_button_icon);
  }

  public void SetSkillIcon(
    UISprite sprite,
    SKILL_SLOT_TYPE slot_type,
    bool is_attached,
    bool is_button_icon)
  {
    if (Object.op_Equality((Object) sprite, (Object) null))
      return;
    sprite.spriteName = UIBehaviour.GetSkillIconSpriteName(slot_type, is_attached, is_button_icon);
  }

  public static string GetSkillIconSpriteName(
    SKILL_SLOT_TYPE slot_type,
    bool is_attached,
    bool is_button_icon)
  {
    string empty = string.Empty;
    int index = (int) (slot_type - 1);
    return !is_button_icon ? (!is_attached ? UIBehaviour.EMPTY_SKILL_ICON_SPRITE_NAME[index] : UIBehaviour.SKILL_ICON_SPRITE_NAME[index]) : (!is_attached ? UIBehaviour.EMPTY_SKILL_ICON_EQUIP_SPRITE_NAME[index] : UIBehaviour.SKILL_ICON_SPRITE_NAME[index]);
  }

  protected void SetEquipIndexIcon(Transform root, Enum _enum, int index)
  {
    this.SetEquipIndexIcon(this.FindCtrl(root, _enum), index);
  }

  protected void SetEquipIndexIcon(Enum _enum, int index)
  {
    this.SetEquipIndexIcon(this.GetCtrl(_enum), index);
  }

  protected void SetEquipIndexIcon(Transform t, int index)
  {
    if (Object.op_Equality((Object) t, (Object) null) || index >= UIBehaviour.EQUIP_INDEX_ICON_SP_NAME.Length)
      return;
    UISprite component = ((Component) t).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    switch (index)
    {
      case 3:
        index = 4;
        break;
      case 4:
        index = 3;
        break;
    }
    component.spriteName = UIBehaviour.EQUIP_INDEX_ICON_SP_NAME[index];
  }

  protected void SetEquipmentTypeIcon(
    Transform root,
    Enum enum_icon,
    Enum enum_bg,
    Enum enum_rarity,
    EquipItemTable.EquipItemData equip_table)
  {
    this.SetEquipmentTypeIcon(this.FindCtrl(root, enum_icon), this.FindCtrl(root, enum_bg), this.FindCtrl(root, enum_rarity), equip_table);
  }

  protected void SetEquipmentTypeIcon(
    Enum enum_icon,
    Enum enum_bg,
    Enum enum_rarity,
    EquipItemTable.EquipItemData equip_table)
  {
    this.SetEquipmentTypeIcon(this.GetCtrl(enum_icon), this.GetCtrl(enum_bg), this.GetCtrl(enum_rarity), equip_table);
  }

  protected void SetEquipmentTypeIcon(
    Transform t_icon,
    Transform t_bg,
    Transform t_rarity,
    EquipItemTable.EquipItemData equip_table)
  {
    if (Object.op_Equality((Object) t_icon, (Object) null) || Object.op_Equality((Object) t_bg, (Object) null) || Object.op_Equality((Object) t_rarity, (Object) null))
      return;
    bool is_visible = equip_table != null;
    this.SetActive(t_icon, is_visible);
    this.SetActive(t_bg, is_visible);
    this.SetActive(t_rarity, is_visible);
    if (equip_table == null)
      return;
    UISprite component1 = ((Component) t_icon).GetComponent<UISprite>();
    UISprite component2 = ((Component) t_bg).GetComponent<UISprite>();
    UISprite component3 = ((Component) t_rarity).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component1, (Object) null) || Object.op_Equality((Object) component2, (Object) null) || Object.op_Equality((Object) component3, (Object) null))
      return;
    component1.spriteName = this.GetTypeIconSpriteName(equip_table.type);
    this.SetTypeIconRaritySpriteName(equip_table.rarity, component2, component3, equip_table.getType);
  }

  protected void SetSkillSlotTypeIcon(
    Transform root,
    Enum enum_icon,
    Enum enum_bg,
    Enum enum_rarity,
    SkillItemTable.SkillItemData table)
  {
    this.SetSkillSlotTypeIcon(this.FindCtrl(root, enum_icon), this.FindCtrl(root, enum_bg), this.FindCtrl(root, enum_rarity), table);
  }

  protected void SetSkillSlotTypeIcon(
    Enum enum_icon,
    Enum enum_bg,
    Enum enum_rarity,
    SkillItemTable.SkillItemData table)
  {
    this.SetSkillSlotTypeIcon(this.GetCtrl(enum_icon), this.GetCtrl(enum_bg), this.GetCtrl(enum_rarity), table);
  }

  protected void SetSkillSlotTypeIcon(
    Transform t_icon,
    Transform t_bg,
    Transform t_rarity,
    SkillItemTable.SkillItemData table)
  {
    if (Object.op_Equality((Object) t_icon, (Object) null) || Object.op_Equality((Object) t_bg, (Object) null) || Object.op_Equality((Object) t_rarity, (Object) null))
      return;
    bool is_visible = table != null;
    this.SetActive(t_icon, is_visible);
    this.SetActive(t_bg, is_visible);
    this.SetActive(t_rarity, is_visible);
    if (table == null)
      return;
    UISprite component1 = ((Component) t_icon).GetComponent<UISprite>();
    UISprite component2 = ((Component) t_bg).GetComponent<UISprite>();
    UISprite component3 = ((Component) t_rarity).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component1, (Object) null) || Object.op_Equality((Object) component2, (Object) null) || Object.op_Equality((Object) component3, (Object) null))
      return;
    component1.spriteName = this.GetTypeIconSpriteName(table.type);
    this.SetTypeIconRaritySpriteName(table.rarity, component2, component3, GET_TYPE.PAY);
  }

  protected EQUIPMENT_TYPE GetEquipKindWeaponOrArmor(EQUIPMENT_TYPE type)
  {
    return type > EQUIPMENT_TYPE.ARMOR ? EQUIPMENT_TYPE.ARMOR : type;
  }

  protected void SetSkillEquipIconKind(
    Transform root,
    Enum enum_ctrl,
    EQUIPMENT_TYPE type,
    bool is_enable)
  {
    UIBehaviour.SetSkillEquipIconKind(this.FindCtrl(root, enum_ctrl), type, is_enable);
  }

  protected void SetSkillEquipIconKind(Enum enum_ctrl, EQUIPMENT_TYPE type, bool is_enable)
  {
    UIBehaviour.SetSkillEquipIconKind(this.GetCtrl(enum_ctrl), type, is_enable);
  }

  public static void SetSkillEquipIconKind(Transform t, EQUIPMENT_TYPE type, bool is_enable)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UISprite component = ((Component) t).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.spriteName = UIBehaviour.GetMagiIconSpriteName(type, is_enable);
  }

  protected void SetAccessoryRarityIcon(
    Transform t_bg,
    Transform t_rarity,
    AccessoryTable.AccessoryData table)
  {
    if (Object.op_Equality((Object) t_bg, (Object) null) || Object.op_Equality((Object) t_rarity, (Object) null))
      return;
    bool is_visible = table != null;
    this.SetActive(t_bg, is_visible);
    this.SetActive(t_rarity, is_visible);
    if (table == null)
      return;
    UISprite component1 = ((Component) t_bg).GetComponent<UISprite>();
    UISprite component2 = ((Component) t_rarity).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component1, (Object) null) || Object.op_Equality((Object) component2, (Object) null))
      return;
    this.SetTypeIconRaritySpriteName(table.rarity, component1, component2, table.getType);
  }

  private string GetTypeIconSpriteName(EQUIPMENT_TYPE type)
  {
    return UIBehaviour.ITEM_TYPE_ICON_SPRITE_NAME[UIBehaviour.GetEquipmentTypeIndex(type)];
  }

  private string GetTypeIconSpriteName(SKILL_SLOT_TYPE type)
  {
    return UIBehaviour.SKILL_TYPE_ICON_SPRITE_NAME[(int) (type - 1)];
  }

  private void SetTypeIconRaritySpriteName(
    RARITY_TYPE rarity,
    UISprite sp_bg,
    UISprite sp_rarity,
    GET_TYPE getType)
  {
    int index = (int) rarity;
    sp_bg.spriteName = UIBehaviour.ITEM_TYPE_ICON_SPRITE_BG_NAME[index];
    UIBehaviour.SetRarityColorType(rarity, (UIWidget) sp_bg);
    sp_rarity.spriteName = ItemIcon.GetRarityTextSpriteName(new RARITY_TYPE?(rarity), getType);
  }

  private static string GetMagiIconSpriteName(EQUIPMENT_TYPE type, bool is_enable)
  {
    int index = UIBehaviour.GetEquipmentTypeIndex(type);
    if (index >= UIBehaviour.MAGI_TYPE_ICON_SPRITE_NAME.Length)
      index = UIBehaviour.MAGI_TYPE_ICON_SPRITE_NAME.Length - 1;
    string str = UIBehaviour.MAGI_TYPE_ICON_SPRITE_NAME[index];
    return !is_enable ? $"{str}_off" : $"{str}_on";
  }

  public static int GetEquipmentTypeIndex(EQUIPMENT_TYPE type)
  {
    return Mathf.Max(0, (int) (ItemIcon.GetItemIconType(type) - 1));
  }

  public static void SetRarityColorType(RARITY_TYPE rarity, UIWidget w)
  {
    UIBehaviour.SetRarityColorType((int) rarity, w);
  }

  public static void SetRarityColorType(int rarity, UIWidget w)
  {
    if (Object.op_Equality((Object) w, (Object) null))
      return;
    if (rarity != 0)
    {
      if (rarity == 1)
        w.color = Color32.op_Implicit(new Color32(byte.MaxValue, (byte) 165, (byte) 131, byte.MaxValue));
      else
        w.color = Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
    }
    else
      w.color = Color32.op_Implicit(new Color32((byte) 184, (byte) 243, byte.MaxValue, byte.MaxValue));
  }

  protected void SetAbilityItemEvent(Transform t, int index, List<Transform> touchAndReleaseList)
  {
    this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_DATA_POPUP", "RELEASE_ABILITY", (object) new object[2]
    {
      (object) index,
      (object) t
    });
    touchAndReleaseList.Add(t);
  }

  public AbilityDetailPopUp CreateAndGetAbilityDetail(Enum parent_enum)
  {
    return this.CreateAndGetAbilityDetail(this.GetCtrl(parent_enum));
  }

  public AbilityDetailPopUp CreateAndGetAbilityDetail(Transform attachRoot)
  {
    return ((Component) this.SetPrefab(attachRoot, UIBehaviour.ABILITY_DETAIL_ITEM_PREFAB_NAME)).GetComponent<AbilityDetailPopUp>();
  }

  protected void NoEventReleaseTouchAndReleases(List<Transform> touchAndReleaseList)
  {
    if (touchAndReleaseList == null || touchAndReleaseList.Count <= 0)
      return;
    int index = 0;
    for (int count = touchAndReleaseList.Count; index < count; ++index)
    {
      if (!Object.op_Equality((Object) touchAndReleaseList[index], (Object) null))
      {
        UIButton componentInChildren = ((Component) touchAndReleaseList[index]).GetComponentInChildren<UIButton>();
        if (Object.op_Implicit((Object) componentInChildren))
          this.NoEventReleaseTouchAndRelease(((Component) componentInChildren).transform);
      }
    }
  }

  protected void SetMaterialInfo(
    Transform root,
    Enum btn_enum,
    REWARD_TYPE reward_type,
    uint id,
    Transform parent_scroll = null)
  {
    this.SetMaterialInfo(this.FindCtrl(root, btn_enum), reward_type, id, parent_scroll);
  }

  protected void SetMaterialInfo(
    Enum btn_enum,
    REWARD_TYPE reward_type,
    uint id,
    Transform parent_scroll = null)
  {
    this.SetMaterialInfo(this.GetCtrl(btn_enum), reward_type, id, parent_scroll);
  }

  protected void SetMaterialInfo(
    Transform btn_t,
    REWARD_TYPE reward_type,
    uint id,
    Transform parent_scroll = null)
  {
    if (Object.op_Equality((Object) btn_t, (Object) null))
      return;
    Transform materialInfo = this.CreateMaterialInfo(btn_t);
    MaterialInfoButton.Set(btn_t, materialInfo, reward_type, id, this.sectionData.sectionName, parent_scroll);
  }

  public Transform CreateMaterialInfo(Enum parent_enum)
  {
    return this.CreateMaterialInfo(this.GetCtrl(parent_enum));
  }

  public Transform CreateMaterialInfo(Transform parent)
  {
    Transform materialInfo = (Transform) null;
    GameObject gameObjectWithTag = GameObject.FindGameObjectWithTag("MaterialInfo");
    if (Object.op_Inequality((Object) gameObjectWithTag, (Object) null))
    {
      MaterialInfo component = gameObjectWithTag.GetComponent<MaterialInfo>();
      if (Object.op_Inequality((Object) component, (Object) null) && component.nowSectionName == this.sectionData.sectionName)
        materialInfo = gameObjectWithTag.transform;
    }
    if (Object.op_Equality((Object) materialInfo, (Object) null))
    {
      UIBehaviour.PrefabData prefabData = this.GetPrefabData("MaterialInfo");
      if (prefabData == null)
      {
        Log.Error(LOG.UI, "{0} not found.", (object) "MaterialInfo");
        return (Transform) null;
      }
      materialInfo = prefabData.Realizes(parent);
      ((Component) materialInfo).gameObject.tag = "MaterialInfo";
    }
    return materialInfo;
  }

  protected void DeleteMaterialInfo()
  {
    GameObject gameObjectWithTag = GameObject.FindGameObjectWithTag("MaterialInfo");
    if (!Object.op_Inequality((Object) gameObjectWithTag, (Object) null))
      return;
    MaterialInfo component = gameObjectWithTag.GetComponent<MaterialInfo>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !(component.nowSectionName == this.sectionData.sectionName))
      return;
    Object.DestroyImmediate((Object) gameObjectWithTag);
  }

  protected void SetLabelCompareParam(
    Transform root,
    Enum ui_enum,
    int value,
    Enum ui_after_enum,
    int after_value)
  {
    this.SetLabelCompareParam(this.FindCtrl(root, ui_enum), value, this.FindCtrl(root, ui_after_enum), after_value);
  }

  protected void SetLabelCompareParam(
    Enum ui_enum,
    int value,
    Enum ui_after_enum,
    int after_value)
  {
    this.SetLabelCompareParam(this.GetCtrl(ui_enum), value, this.GetCtrl(ui_after_enum), after_value);
  }

  protected void SetLabelCompareParam(
    Transform now_t,
    int value,
    Transform after_t,
    int after_value)
  {
    if (Object.op_Equality((Object) now_t, (Object) null))
      return;
    UILabel component = ((Component) now_t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.text = value.ToString();
    this.SetLabelCompareParam(after_t, after_value, value);
  }

  protected void SetLabelCompareParam(
    Transform root,
    Enum ui_after_enum,
    int after_value,
    int before_value,
    int set_after_value = -1)
  {
    this.SetLabelCompareParam(this.FindCtrl(root, ui_after_enum), after_value, before_value, set_after_value);
  }

  protected void SetLabelCompareParam(
    Enum ui_after_enum,
    int after_value,
    int before_value,
    int set_after_value = -1)
  {
    this.SetLabelCompareParam(this.GetCtrl(ui_after_enum), after_value, before_value, set_after_value);
  }

  protected void SetLabelCompareParam(
    Transform t,
    int after_value,
    int before_value,
    int set_after_value = -1)
  {
    if (set_after_value == -1)
      set_after_value = after_value;
    this.SetLabelCompareParam(t, after_value, before_value, set_after_value.ToString());
  }

  protected void SetLabelCompareParam(
    Transform root,
    Enum ui_after_enum,
    int after_value,
    int before_value,
    string set_after_value_string)
  {
    this.SetLabelCompareParam(this.FindCtrl(root, ui_after_enum), after_value, before_value, set_after_value_string);
  }

  protected void SetLabelCompareParam(
    Enum ui_after_enum,
    int after_value,
    int before_value,
    string set_after_value_string)
  {
    this.SetLabelCompareParam(this.GetCtrl(ui_after_enum), after_value, before_value, set_after_value_string);
  }

  protected void SetLabelCompareParam(
    Transform t_after,
    int after_value,
    int before_value,
    string set_after_value_string)
  {
    if (Object.op_Equality((Object) t_after, (Object) null))
      return;
    UILabel component = ((Component) t_after).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.text = set_after_value_string;
    if (before_value > after_value)
      ((Component) t_after).GetComponent<UIWidget>().color = Color.red;
    else if (before_value < after_value)
      ((Component) t_after).GetComponent<UIWidget>().color = Color32.op_Implicit(UIBehaviour.buffGreen);
    else
      ((Component) t_after).GetComponent<UIWidget>().color = Color.white;
  }

  protected void SetLabelDiffParam(
    Transform root,
    Enum ui_after_enum,
    int after_value,
    Enum ui_diff_enum,
    int before_value,
    Enum ui_default_enum,
    string diff_format = null)
  {
    this.SetLabelDiffParam(this.FindCtrl(root, ui_after_enum), after_value, this.FindCtrl(root, ui_diff_enum), before_value, this.FindCtrl(root, ui_default_enum), diff_format);
  }

  protected void SetLabelDiffParam(
    Enum ui_after_enum,
    int after_value,
    Enum ui_diff_enum,
    int before_value,
    Enum ui_default_enum,
    string diff_format = null)
  {
    this.SetLabelDiffParam(this.GetCtrl(ui_after_enum), after_value, this.GetCtrl(ui_diff_enum), before_value, this.GetCtrl(ui_default_enum), diff_format);
  }

  protected void SetLabelDiffParam(
    Transform now_t,
    int after_value,
    Transform diff_t,
    int before_value,
    Transform default_t,
    string diff_format = null)
  {
    int num = after_value - before_value;
    if (num == 0)
    {
      if (Object.op_Inequality((Object) now_t, (Object) null))
        this.SetActive(now_t, false);
      if (Object.op_Inequality((Object) diff_t, (Object) null))
        this.SetActive(diff_t, false);
      if (Object.op_Inequality((Object) default_t, (Object) null))
        this.SetActive(default_t, true);
      this.SetLabelText(default_t, after_value.ToString());
    }
    else
    {
      if (Object.op_Inequality((Object) now_t, (Object) null))
        this.SetActive(now_t, true);
      if (Object.op_Inequality((Object) diff_t, (Object) null))
        this.SetActive(diff_t, true);
      if (Object.op_Inequality((Object) default_t, (Object) null))
        this.SetActive(default_t, false);
      if (Object.op_Equality((Object) now_t, (Object) null))
        return;
      UILabel component1 = ((Component) now_t).GetComponent<UILabel>();
      if (Object.op_Equality((Object) component1, (Object) null))
        return;
      component1.text = after_value.ToString();
      if (Object.op_Equality((Object) diff_t, (Object) null))
        return;
      UILabel component2 = ((Component) diff_t).GetComponent<UILabel>();
      if (Object.op_Equality((Object) component2, (Object) null))
        return;
      string str = (num > 0 ? (object) "+" : (object) "").ToString() + (object) num;
      if (!string.IsNullOrEmpty(diff_format))
        str = string.Format(diff_format, (object) str);
      component2.text = str;
      this.SetActive(diff_t, true);
      if (num < 0)
      {
        component2.color = Color.red;
        component1.color = Color.red;
      }
      else
      {
        if (num <= 0)
          return;
        component2.color = Color32.op_Implicit(UIBehaviour.buffGreen);
        component1.color = Color32.op_Implicit(UIBehaviour.buffGreen);
      }
    }
  }

  protected void SetNextExpValue(
    Transform root,
    Enum ui_exp_next_enum,
    Enum ui_exp_next_root_enum,
    SkillItemInfo skill_item)
  {
    this.SetNextExpValue(this.FindCtrl(root, ui_exp_next_enum), this.FindCtrl(root, ui_exp_next_root_enum), skill_item);
  }

  protected void SetNextExpValue(
    Enum ui_exp_next_enum,
    Enum ui_exp_next_root_enum,
    SkillItemInfo skill_item)
  {
    this.SetNextExpValue(this.GetCtrl(ui_exp_next_enum), this.GetCtrl(ui_exp_next_root_enum), skill_item);
  }

  protected void SetNextExpValue(Transform t, Transform exp_root_t, SkillItemInfo skill_item)
  {
    if (Object.op_Equality((Object) t, (Object) null) || skill_item == null)
      return;
    UILabel component = ((Component) t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (skill_item.level == skill_item.tableData.GetMaxLv(skill_item.exceedCnt))
    {
      this.SetActive(exp_root_t, false);
    }
    else
    {
      this.SetActive(exp_root_t, true);
      component.text = (skill_item.expNext - skill_item.exp).ToString();
    }
  }

  protected string GetInputText(Enum ui_enum)
  {
    UIInput component = this.GetComponent<UIInput>(ui_enum);
    return Object.op_Equality((Object) component, (Object) null) ? string.Empty : component.value;
  }

  protected void SetNPCIcon(Transform root, Enum _enum, int icon_id, bool is_smile = false)
  {
    this.SetNPCIcon(this.FindCtrl(root, _enum), icon_id, is_smile);
  }

  protected void SetNPCIcon(Enum _enum, int icon_id, bool is_smile = false)
  {
    this.SetNPCIcon(this.GetCtrl(_enum), icon_id, is_smile);
  }

  protected void SetNPCIcon(Transform t, int icon_id, bool is_smile = false)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITexture component = ((Component) t).GetComponent<UITexture>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    ResourceLoad.LoadNPCIconTexture(component, icon_id, is_smile);
  }

  protected void SetEnemyIcon(Transform root, Enum _enum, int icon_id)
  {
    this.SetEnemyIcon(this.FindCtrl(root, _enum), icon_id);
  }

  protected void SetEnemyIcon(Enum _enum, int icon_id)
  {
    this.SetEnemyIcon(this.GetCtrl(_enum), icon_id);
  }

  protected void SetEnemyIcon(Transform t, int icon_id)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITexture component = ((Component) t).GetComponent<UITexture>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    ResourceLoad.LoadEnemyIconTexture(component, icon_id);
  }

  protected void SetEnemyIconGradeFrame(
    Transform root,
    Enum _enum,
    QuestTable.QuestTableData quest_table)
  {
    this.SetEnemyIconGradeFrame(this.FindCtrl(root, _enum), quest_table);
  }

  protected void SetEnemyIconGradeFrame(Enum _enum, QuestTable.QuestTableData quest_table)
  {
    this.SetEnemyIconGradeFrame(this.GetCtrl(_enum), quest_table);
  }

  protected void SetEnemyIconGradeFrame(Transform t, QuestTable.QuestTableData quest_table)
  {
    if (Object.op_Equality((Object) t, (Object) null) || quest_table == null)
      return;
    UISprite component = ((Component) t).GetComponent<UISprite>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    int rarity = (int) quest_table.rarity;
    if (quest_table.questType != QUEST_TYPE.ORDER)
      component.spriteName = "MonsterCircleN";
    else if (rarity < UIBehaviour.enemyIconGradeFrameName.Length)
      component.spriteName = UIBehaviour.enemyIconGradeFrameName[rarity];
    else
      component.spriteName = string.Empty;
  }

  public void SetPageNumText(Transform root, Enum enum_lbl, int page_num)
  {
    this.SetPageNumText(this.FindCtrl(root, enum_lbl), page_num);
  }

  public void SetPageNumText(Enum enum_lbl, int page_num)
  {
    this.SetPageNumText(this.GetCtrl(enum_lbl), page_num);
  }

  public void SetPageNumText(Transform t, int page_num)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UILabel component = ((Component) t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    string empty = string.Empty;
    string str = page_num <= 999 ? page_num.ToString("D3") : StringTable.Get(STRING_CATEGORY.PAGE_UI, 0U);
    component.text = str;
  }

  protected void SetTable(
    Enum table_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UITable>(this.GetCtrl(table_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetTable(
    Enum table_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UITable>(this.GetCtrl(table_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected IEnumerator SetTableAsync(
    Enum table_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    yield return (object) this.SetItemListAsync<UITable>(this.GetCtrl(table_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected void SetTable(
    Transform root,
    Enum table_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UITable>(this.FindCtrl(root, table_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetTable(
    Transform root,
    Enum table_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UITable>(this.FindCtrl(root, table_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected void SetGrid(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIGrid>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetGrid(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIGrid>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected void SetGrid(
    Transform root,
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIGrid>(this.FindCtrl(root, grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetGrid(
    Transform root,
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIGrid>(this.FindCtrl(root, grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected void SetDynamicList(
    Transform root,
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetDynamicList(this.FindCtrl(root, grid_ctrl_enum), item_prefab_name, item_num, reset, check_item_func, create_item_func, item_init_func);
  }

  protected void SetDynamicList(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetDynamicList(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, check_item_func, create_item_func, item_init_func);
  }

  protected void SetDynamicList(
    Transform t,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIGrid>(t, item_prefab_name, item_num, reset, check_item_func, create_item_func, item_init_func, true);
  }

  protected void SetWrapContent(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIWrapContent>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetWrapContent(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIWrapContent>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, create_item_func, item_init_func, false);
  }

  protected void SetWrapContent(
    Transform root,
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<UIWrapContent>(this.FindCtrl(root, grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  protected void SetSimpleContent(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    this.SetItemList<SimpleContent>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, check_item_func, create_item_func, item_init_func, false);
  }

  protected IEnumerator SetSimpleContentAsync(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> item_init_func)
  {
    yield return (object) this.SetItemListAsync<SimpleContent>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, check_item_func, create_item_func, item_init_func, false);
  }

  protected void SetWrapContentFilter(
    Enum grid_ctrl_enum,
    string item_prefab_name,
    int item_num,
    bool reset,
    Action<int, Transform, bool> item_init_func,
    Func<int, string, bool> filter_item_func)
  {
    this.SetItemList<UIWrapContentFilter>(this.GetCtrl(grid_ctrl_enum), item_prefab_name, item_num, reset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, item_init_func, false);
  }

  public void SetWrapContentFilterText(Transform root, Enum enum_lbl, string text)
  {
    this.SetWrapContentFilterText(this.FindCtrl(root, enum_lbl), text);
  }

  public void SetWrapContentFilterText(Enum enum_lbl, string text)
  {
    this.SetWrapContentFilterText(this.GetCtrl(enum_lbl), text);
  }

  public void SetWrapContentFilterText(Transform t, string text)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UIWrapContentFilter component = ((Component) t).GetComponent<UIWrapContentFilter>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.filter = text;
  }

  protected void AddItemList(
    Transform item_list_transform,
    string item_prefab_name,
    int item_num,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> init_item_func)
  {
    UIGrid component1 = ((Component) item_list_transform).gameObject.GetComponent<UIGrid>();
    UIBehaviour.PrefabData prefab_data = (UIBehaviour.PrefabData) null;
    if (!string.IsNullOrEmpty(item_prefab_name))
    {
      prefab_data = this.GetPrefabData(item_prefab_name);
      if (prefab_data == null)
      {
        Log.Error(LOG.UI, "{0} not found.", (object) item_prefab_name);
        return;
      }
    }
    UICenterOnChild component2 = ((Component) item_list_transform).GetComponent<UICenterOnChild>();
    UIScrollView component3 = ((Component) item_list_transform.parent).gameObject.GetComponent<UIScrollView>();
    UIPanel uiPanel = (UIPanel) null;
    if (Object.op_Inequality((Object) component3, (Object) null))
    {
      ((Behaviour) component3).enabled = true;
      uiPanel = ((Component) component3).GetComponent<UIPanel>();
    }
    for (int index = 0; index < item_num; ++index)
    {
      if (check_item_func == null || check_item_func(index))
      {
        Transform transform;
        if (prefab_data != null || create_item_func != null)
        {
          transform = (Transform) null;
          if (create_item_func != null)
            transform = create_item_func(index, item_list_transform);
          if (Object.op_Equality((Object) transform, (Object) null) && prefab_data != null)
            transform = this._Realizes(prefab_data, item_list_transform, true);
          if (Object.op_Inequality((Object) transform, (Object) null) && Object.op_Inequality((Object) uiPanel, (Object) null))
          {
            UIPanel componentInChildren = ((Component) transform).GetComponentInChildren<UIPanel>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              componentInChildren.depth = uiPanel.depth + 1;
          }
        }
        else
        {
          GameObject gameObject = new GameObject();
          gameObject.layer = 5;
          transform = gameObject.transform;
          transform.parent = item_list_transform;
          transform.localPosition = Vector3.zero;
          transform.localScale = Vector3.one;
          if (Object.op_Inequality((Object) component3, (Object) null))
            gameObject.AddComponent<UIDragScrollView>().scrollView = component3;
        }
        if (Object.op_Inequality((Object) component2, (Object) null))
          UIUtility.AddCenterOnClickChild(transform);
        if (Object.op_Equality((Object) transform, (Object) null))
          return;
        ((Object) transform).name = index.ToString();
        init_item_func(index, transform, false);
        UIUtility.UpdateAnchors(transform);
      }
    }
    component1.Reposition();
  }

  private void SetItemList<T>(
    Transform item_list_transform,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> init_item_func,
    bool is_dynamic)
    where T : Component
  {
    if (Object.op_Equality((Object) item_list_transform, (Object) null))
      return;
    T component1 = ((Component) item_list_transform).gameObject.GetComponent<T>();
    if (Object.op_Equality((Object) (object) component1, (Object) null))
      return;
    UITable uiTable = (object) component1 as UITable;
    UIGrid grid = (object) component1 as UIGrid;
    UIWrapContent uiWrapContent = (object) component1 as UIWrapContent;
    UIWrapContentFilter wrapContentFilter = (object) component1 as UIWrapContentFilter;
    UIBehaviour.PrefabData prefab_data = (UIBehaviour.PrefabData) null;
    if (!string.IsNullOrEmpty(item_prefab_name))
    {
      prefab_data = this.GetPrefabData(item_prefab_name);
      if (prefab_data == null)
      {
        Log.Error(LOG.UI, "{0} not found.", (object) item_prefab_name);
        return;
      }
    }
    UIScrollView component2 = ((Component) item_list_transform.parent).gameObject.GetComponent<UIScrollView>();
    UIPanel uiPanel = (UIPanel) null;
    if (Object.op_Inequality((Object) component2, (Object) null))
    {
      ((Behaviour) component2).enabled = true;
      uiPanel = ((Component) component2).GetComponent<UIPanel>();
    }
    UICenterOnChild component3 = ((Component) item_list_transform).GetComponent<UICenterOnChild>();
    if (((Component) item_list_transform).gameObject.tag == "Dirty")
    {
      ((Component) item_list_transform).gameObject.tag = "Untagged";
      reset = true;
    }
    else if (this.uiFirstUpdate)
      reset = true;
    UIDynamicList uiDynamicList = (UIDynamicList) null;
    if (is_dynamic)
      uiDynamicList = UIDynamicList.Set(component2, item_num, prefab_data?.prefab, Object.op_Inequality((Object) component3, (Object) null), create_item_func, init_item_func);
    int childCount = item_list_transform.childCount;
    int num = 0;
    int item_num1 = 0;
    for (int index = 0; index < item_num; ++index)
    {
      if (check_item_func == null || check_item_func(index))
      {
        bool flag;
        Transform transform;
        if (num >= childCount)
        {
          flag = false;
          if (Object.op_Inequality((Object) uiDynamicList, (Object) null))
          {
            GameObject gameObject = new GameObject();
            gameObject.layer = 5;
            UIWidget w = gameObject.AddComponent<UIWidget>();
            w.SetDimensions((int) grid.cellWidth, (int) grid.cellHeight);
            transform = gameObject.transform;
            transform.SetParent(item_list_transform, false);
            uiDynamicList.AddItemWidget(w);
          }
          else if (prefab_data != null || create_item_func != null)
          {
            transform = (Transform) null;
            if (create_item_func != null)
              transform = create_item_func(index, item_list_transform);
            if (Object.op_Equality((Object) transform, (Object) null) && prefab_data != null)
              transform = this._Realizes(prefab_data, item_list_transform, true);
            if (Object.op_Inequality((Object) transform, (Object) null) && Object.op_Inequality((Object) uiPanel, (Object) null))
            {
              UIPanel componentInChildren = ((Component) transform).GetComponentInChildren<UIPanel>();
              if (Object.op_Inequality((Object) componentInChildren, (Object) null))
                componentInChildren.depth = uiPanel.depth + 1;
            }
          }
          else
          {
            GameObject gameObject = new GameObject();
            gameObject.layer = 5;
            transform = gameObject.transform;
            transform.parent = item_list_transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            if (Object.op_Inequality((Object) component2, (Object) null))
              gameObject.AddComponent<UIDragScrollView>().scrollView = component2;
          }
          if (Object.op_Inequality((Object) component3, (Object) null))
            UIUtility.AddCenterOnClickChild(transform);
        }
        else
        {
          flag = true;
          transform = item_list_transform.GetChild(num);
          if (Object.op_Inequality((Object) component2, (Object) null) && Object.op_Inequality((Object) ((Component) component2).transform.GetChild(0), (Object) null))
          {
            UITweenAddToChildrenCtrl component4 = ((Component) ((Component) component2).transform.GetChild(0)).GetComponent<UITweenAddToChildrenCtrl>();
            if (Object.op_Inequality((Object) component4, (Object) null) && ((Behaviour) component4).enabled && Object.op_Inequality((Object) ((Component) transform).GetComponent<UITweenAddCtrlChild>(), (Object) null))
              transform = transform.GetChild(0);
          }
          ((Component) transform).gameObject.SetActive(true);
          if (Object.op_Inequality((Object) uiDynamicList, (Object) null))
            uiDynamicList.AddItemWidget(((Component) transform).GetComponent<UIWidget>());
          ++num;
        }
        if (Object.op_Equality((Object) transform, (Object) null))
          return;
        ((Object) transform).name = index.ToString();
        if (Object.op_Equality((Object) uiDynamicList, (Object) null))
        {
          init_item_func(index, transform, flag);
          UIUtility.UpdateAnchors(transform);
        }
        if (((Component) transform).gameObject.activeSelf)
          ++item_num1;
      }
    }
    for (int index = childCount - 1; index >= num; --index)
      Object.DestroyImmediate((Object) ((Component) item_list_transform.GetChild(index)).gameObject);
    UITweenAddToChildrenCtrl addToChildrenCtrl = (UITweenAddToChildrenCtrl) null;
    if (Object.op_Inequality((Object) uiTable, (Object) null))
    {
      if (reset && uiTable.keepWithinPanel && uiTable.pivot == UIWidget.Pivot.TopLeft)
        ((Component) uiTable).transform.localPosition = new Vector3(-9999f, 9999f, 0.0f);
      uiTable.Reposition();
      addToChildrenCtrl = ((Component) uiTable).GetComponent<UITweenAddToChildrenCtrl>();
    }
    else if (Object.op_Inequality((Object) grid, (Object) null))
    {
      if (reset && grid.keepWithinPanel && grid.pivot == UIWidget.Pivot.TopLeft)
        ((Component) grid).transform.localPosition = new Vector3(-9999f, 9999f, 0.0f);
      grid.Reposition();
      if (Object.op_Inequality((Object) component2, (Object) null))
        UIUtility.SetGridItemsDraggableWidget(component2, grid, item_num1);
      addToChildrenCtrl = ((Component) grid).GetComponent<UITweenAddToChildrenCtrl>();
    }
    if (Object.op_Inequality((Object) component2, (Object) null))
    {
      if (reset)
        component2.ResetPosition();
      this.ActivateScrollBarCollider(component2, true);
      if (component2.canMoveHorizontally && !component2.shouldMoveHorizontally || component2.canMoveVertically && !component2.shouldMoveVertically)
      {
        ((Behaviour) component2).enabled = false;
        if (component2.showScrollBars != UIScrollView.ShowCondition.Always)
        {
          if (Object.op_Inequality((Object) component2.horizontalScrollBar, (Object) null))
            component2.horizontalScrollBar.alpha = 0.0f;
          if (Object.op_Inequality((Object) component2.verticalScrollBar, (Object) null))
          {
            component2.verticalScrollBar.alpha = 0.0f;
            this.ActivateScrollBarCollider(component2, false);
          }
        }
      }
    }
    if (Object.op_Inequality((Object) uiDynamicList, (Object) null))
    {
      if (Object.op_Inequality((Object) addToChildrenCtrl, (Object) null))
        addToChildrenCtrl.SkipTween();
      uiDynamicList.UpdateItems();
      if (Object.op_Inequality((Object) component2, (Object) null) && reset)
      {
        component2.ResetPosition();
        component2.MoveAbsolute(Vector3.zero);
      }
    }
    if (Object.op_Inequality((Object) uiWrapContent, (Object) null))
    {
      if (Object.op_Inequality((Object) component2, (Object) null))
        component2.ResetPosition();
      uiWrapContent.SortAlphabetically();
    }
    else if (Object.op_Inequality((Object) wrapContentFilter, (Object) null))
    {
      if (Object.op_Inequality((Object) component2, (Object) null))
        component2.ResetPosition();
      wrapContentFilter.Initialize();
    }
    if (!Object.op_Inequality((Object) addToChildrenCtrl, (Object) null) || !reset)
      return;
    addToChildrenCtrl.TweenAdd();
  }

  private IEnumerator SetItemListAsync<T>(
    Transform item_list_transform,
    string item_prefab_name,
    int item_num,
    bool reset,
    Func<int, bool> check_item_func,
    Func<int, Transform, Transform> create_item_func,
    Action<int, Transform, bool> init_item_func,
    bool is_dynamic)
    where T : Component
  {
    if (!Object.op_Equality((Object) item_list_transform, (Object) null))
    {
      T component1 = ((Component) item_list_transform).gameObject.GetComponent<T>();
      if (!Object.op_Equality((Object) (object) component1, (Object) null))
      {
        UITable table = (object) component1 as UITable;
        UIGrid grid = (object) component1 as UIGrid;
        UIWrapContent wrap = (object) component1 as UIWrapContent;
        UIWrapContentFilter wrapFilter = (object) component1 as UIWrapContentFilter;
        UIBehaviour.PrefabData prefab_data = (UIBehaviour.PrefabData) null;
        if (!string.IsNullOrEmpty(item_prefab_name))
        {
          prefab_data = this.GetPrefabData(item_prefab_name);
          if (prefab_data == null)
          {
            Log.Error(LOG.UI, "{0} not found.", (object) item_prefab_name);
            yield break;
          }
        }
        UIScrollView scroll_view = ((Component) item_list_transform.parent).gameObject.GetComponent<UIScrollView>();
        UIPanel scroll_view_panel = (UIPanel) null;
        if (Object.op_Inequality((Object) scroll_view, (Object) null))
        {
          ((Behaviour) scroll_view).enabled = true;
          scroll_view_panel = ((Component) scroll_view).GetComponent<UIPanel>();
        }
        UICenterOnChild center_on_child = ((Component) item_list_transform).GetComponent<UICenterOnChild>();
        if (((Component) item_list_transform).gameObject.tag == "Dirty")
        {
          ((Component) item_list_transform).gameObject.tag = "Untagged";
          reset = true;
        }
        else if (this.uiFirstUpdate)
          reset = true;
        UIDynamicList dynamic_list = (UIDynamicList) null;
        if (is_dynamic)
          dynamic_list = UIDynamicList.Set(scroll_view, item_num, prefab_data != null ? prefab_data.prefab : (GameObject) null, Object.op_Inequality((Object) center_on_child, (Object) null), create_item_func, init_item_func);
        int table_child_count = item_list_transform.childCount;
        int recycle_index = 0;
        int active_item_num = 0;
        for (int i = 0; i < item_num; ++i)
        {
          if (check_item_func == null || check_item_func(i))
          {
            bool flag;
            Transform transform;
            if (recycle_index >= table_child_count)
            {
              flag = false;
              if (Object.op_Inequality((Object) dynamic_list, (Object) null))
              {
                GameObject gameObject = new GameObject();
                gameObject.layer = 5;
                UIWidget w = gameObject.AddComponent<UIWidget>();
                w.SetDimensions((int) grid.cellWidth, (int) grid.cellHeight);
                transform = gameObject.transform;
                transform.SetParent(item_list_transform, false);
                dynamic_list.AddItemWidget(w);
              }
              else if (prefab_data != null || create_item_func != null)
              {
                transform = (Transform) null;
                if (create_item_func != null)
                  transform = create_item_func(i, item_list_transform);
                if (Object.op_Equality((Object) transform, (Object) null) && prefab_data != null)
                  transform = this._Realizes(prefab_data, item_list_transform, true);
                if (Object.op_Inequality((Object) transform, (Object) null) && Object.op_Inequality((Object) scroll_view_panel, (Object) null))
                {
                  UIPanel componentInChildren = ((Component) transform).GetComponentInChildren<UIPanel>();
                  if (Object.op_Inequality((Object) componentInChildren, (Object) null))
                    componentInChildren.depth = scroll_view_panel.depth + 1;
                }
              }
              else
              {
                GameObject gameObject = new GameObject();
                gameObject.layer = 5;
                transform = gameObject.transform;
                transform.parent = item_list_transform;
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
                if (Object.op_Inequality((Object) scroll_view, (Object) null))
                  gameObject.AddComponent<UIDragScrollView>().scrollView = scroll_view;
              }
              if (Object.op_Inequality((Object) center_on_child, (Object) null))
                UIUtility.AddCenterOnClickChild(transform);
            }
            else
            {
              flag = true;
              transform = item_list_transform.GetChild(recycle_index);
              if (Object.op_Inequality((Object) scroll_view, (Object) null) && Object.op_Inequality((Object) ((Component) scroll_view).transform.GetChild(0), (Object) null))
              {
                UITweenAddToChildrenCtrl component2 = ((Component) ((Component) scroll_view).transform.GetChild(0)).GetComponent<UITweenAddToChildrenCtrl>();
                if (Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled && Object.op_Inequality((Object) ((Component) transform).GetComponent<UITweenAddCtrlChild>(), (Object) null))
                  transform = transform.GetChild(0);
              }
              ((Component) transform).gameObject.SetActive(true);
              if (Object.op_Inequality((Object) dynamic_list, (Object) null))
                dynamic_list.AddItemWidget(((Component) transform).GetComponent<UIWidget>());
              ++recycle_index;
            }
            if (Object.op_Equality((Object) transform, (Object) null))
              yield break;
            ((Object) transform).name = i.ToString();
            if (Object.op_Equality((Object) dynamic_list, (Object) null))
            {
              init_item_func(i, transform, flag);
              UIUtility.UpdateAnchors(transform);
            }
            if (((Component) transform).gameObject.activeSelf)
              ++active_item_num;
            if (i % 5 == 0)
              yield return (object) null;
          }
        }
        for (int index = table_child_count - 1; index >= recycle_index; --index)
          Object.DestroyImmediate((Object) ((Component) item_list_transform.GetChild(index)).gameObject);
        UITweenAddToChildrenCtrl addToChildrenCtrl = (UITweenAddToChildrenCtrl) null;
        if (Object.op_Inequality((Object) table, (Object) null))
        {
          if (reset && table.keepWithinPanel && table.pivot == UIWidget.Pivot.TopLeft)
            ((Component) table).transform.localPosition = new Vector3(-9999f, 9999f, 0.0f);
          table.Reposition();
          addToChildrenCtrl = ((Component) table).GetComponent<UITweenAddToChildrenCtrl>();
        }
        else if (Object.op_Inequality((Object) grid, (Object) null))
        {
          if (reset && grid.keepWithinPanel && grid.pivot == UIWidget.Pivot.TopLeft)
            ((Component) grid).transform.localPosition = new Vector3(-9999f, 9999f, 0.0f);
          grid.Reposition();
          if (Object.op_Inequality((Object) scroll_view, (Object) null))
            UIUtility.SetGridItemsDraggableWidget(scroll_view, grid, active_item_num);
          addToChildrenCtrl = ((Component) grid).GetComponent<UITweenAddToChildrenCtrl>();
        }
        if (Object.op_Inequality((Object) scroll_view, (Object) null))
        {
          if (reset)
            scroll_view.ResetPosition();
          this.ActivateScrollBarCollider(scroll_view, true);
          if (scroll_view.canMoveHorizontally && !scroll_view.shouldMoveHorizontally || scroll_view.canMoveVertically && !scroll_view.shouldMoveVertically)
          {
            ((Behaviour) scroll_view).enabled = false;
            if (scroll_view.showScrollBars != UIScrollView.ShowCondition.Always)
            {
              if (Object.op_Inequality((Object) scroll_view.horizontalScrollBar, (Object) null))
                scroll_view.horizontalScrollBar.alpha = 0.0f;
              if (Object.op_Inequality((Object) scroll_view.verticalScrollBar, (Object) null))
              {
                scroll_view.verticalScrollBar.alpha = 0.0f;
                this.ActivateScrollBarCollider(scroll_view, false);
              }
            }
          }
        }
        if (Object.op_Inequality((Object) dynamic_list, (Object) null))
        {
          if (Object.op_Inequality((Object) addToChildrenCtrl, (Object) null))
            addToChildrenCtrl.SkipTween();
          dynamic_list.UpdateItems();
          if (Object.op_Inequality((Object) scroll_view, (Object) null) && reset)
          {
            scroll_view.ResetPosition();
            scroll_view.MoveAbsolute(Vector3.zero);
          }
        }
        if (Object.op_Inequality((Object) wrap, (Object) null))
        {
          if (Object.op_Inequality((Object) scroll_view, (Object) null))
            scroll_view.ResetPosition();
          wrap.SortAlphabetically();
        }
        else if (Object.op_Inequality((Object) wrapFilter, (Object) null))
        {
          if (Object.op_Inequality((Object) scroll_view, (Object) null))
            scroll_view.ResetPosition();
          wrapFilter.Initialize();
        }
        if (Object.op_Inequality((Object) addToChildrenCtrl, (Object) null) && reset)
          addToChildrenCtrl.TweenAdd();
      }
    }
  }

  protected void InitTween(Enum ctrl_enum) => this.InitTween(this.GetCtrl(ctrl_enum));

  protected void InitTween(Transform root, Enum ctrl_enum)
  {
    this.InitTween(this.FindCtrl(root, ctrl_enum));
  }

  protected void InitTween(Transform t, bool reverse = false, EventDelegate.Callback callback = null)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    UITweenCtrl.Set(t);
  }

  protected void PlayTween(
    Enum ctrl_enum,
    bool forward = true,
    EventDelegate.Callback callback = null,
    bool is_input_block = true,
    int tween_ctrl_id = 0)
  {
    this.PlayTween(this.GetCtrl(ctrl_enum), forward, callback, is_input_block, tween_ctrl_id);
  }

  protected void PlayTween(
    Transform root,
    Enum ctrl_enum,
    bool forward = true,
    EventDelegate.Callback callback = null,
    bool is_input_block = true,
    int tween_ctrl_id = 0)
  {
    this.PlayTween(this.FindCtrl(root, ctrl_enum), forward, callback, is_input_block, tween_ctrl_id);
  }

  protected void PlayTween(
    Transform t,
    bool forward = true,
    EventDelegate.Callback callback = null,
    bool is_input_block = true,
    int tween_ctrl_id = 0)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    if (forward)
      ((Component) t).gameObject.tag = "Dirty";
    else
      ((Component) t).gameObject.tag = "Untagged";
    UITweenCtrl.Play(t, forward, callback, is_input_block, tween_ctrl_id);
  }

  protected void SkipTween(Enum ctrl_enum, bool forward = true, int tween_ctrl_id = 0)
  {
    this.SkipTween(this.GetCtrl(ctrl_enum), forward, tween_ctrl_id);
  }

  protected void SkipTween(Transform root, Enum ctrl_enum, bool forward = true, int tween_ctrl_id = 0)
  {
    this.SkipTween(this.FindCtrl(root, ctrl_enum), forward, tween_ctrl_id);
  }

  protected void SkipTween(Transform t, bool forward = true, int tween_ctrl_id = 0)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    if (forward)
      ((Component) t).gameObject.tag = "Dirty";
    else
      ((Component) t).gameObject.tag = "Untagged";
    UITweenCtrl.Skip(t, forward, tween_ctrl_id);
  }

  protected void ResetTween(Enum ctrl_enum, int tween_ctrl_id = 0)
  {
    this.ResetTween(this.GetCtrl(ctrl_enum), tween_ctrl_id);
  }

  protected void ResetTween(Transform root, Enum ctrl_enum, int tween_ctrl_id = 0)
  {
    this.ResetTween(this.FindCtrl(root, ctrl_enum), tween_ctrl_id);
  }

  protected void ResetTween(Transform t, int tween_ctrl_id = 0)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    ((Component) t).gameObject.tag = "Untagged";
    UITweenCtrl.Reset(t, tween_ctrl_id);
  }

  protected void InitUITweener<T>(
    Transform root,
    Enum ctrl_enum,
    bool is_enable,
    EventDelegate.Callback on_finish = null)
    where T : UITweener
  {
    this.InitUITweener<T>(this.FindCtrl(root, ctrl_enum), is_enable, on_finish);
  }

  protected void InitUITweener<T>(Enum ctrl_enum, bool is_enable, EventDelegate.Callback on_finish = null) where T : UITweener
  {
    this.InitUITweener<T>(this.GetCtrl(ctrl_enum), is_enable, on_finish);
  }

  protected void InitUITweener<T>(Transform t, bool is_enable, EventDelegate.Callback on_finish = null) where T : UITweener
  {
    T component = this.GetComponent<T>(t);
    if (Object.op_Equality((Object) (object) component, (Object) null))
      return;
    component.ResetToBeginning();
    if (on_finish != null)
      component.SetOnFinished(on_finish);
    ((Behaviour) (object) component).enabled = is_enable;
  }

  protected virtual GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags() => (GameSection.NOTIFY_FLAG) 0;

  public void Open(UITransition.TYPE type = UITransition.TYPE.OPEN)
  {
    if (Object.op_Inequality((Object) this.transferUI, (Object) null))
    {
      this.transferUI.Open(type);
    }
    else
    {
      if (this._state != UIBehaviour.STATE.CLOSE)
        return;
      this.uiVisible = true;
      this.uiUpdateInstant = true;
      this.OnOpen();
      if (this.ctrls != null)
        this.RefreshUI();
      this.uiFirstUpdate = false;
      this.uiUpdateInstant = false;
      if (this.transitions != null && this.transitions.Count > 0)
      {
        int index = 0;
        for (int count = this.transitions.Count; index < count; ++index)
          this.transitions[index].Play(type, new System.Action(this.OnOpened));
        this._state = UIBehaviour.STATE.TO_OPEN;
      }
      else
        this.OnOpened();
    }
  }

  private void OnOpened()
  {
    if (this.transitions != null)
    {
      int index = 0;
      for (int count = this.transitions.Count; index < count; ++index)
      {
        if (this.transitions[index].isBusy)
          return;
      }
    }
    this._state = UIBehaviour.STATE.OPEN;
  }

  protected virtual void OnOpen()
  {
  }

  public virtual void Close(UITransition.TYPE type = UITransition.TYPE.CLOSE)
  {
    if (Object.op_Inequality((Object) this.transferUI, (Object) null))
    {
      this.transferUI.Close(type);
    }
    else
    {
      if (this._state != UIBehaviour.STATE.OPEN)
        return;
      this.OnCloseStart();
      if (this.transitions != null && this.transitions.Count > 0)
      {
        int index = 0;
        for (int count = this.transitions.Count; index < count; ++index)
          this.transitions[index].Play(type, new System.Action(this.OnClosed));
        this._state = UIBehaviour.STATE.TO_CLOSE;
      }
      else
        this.OnClosed();
    }
  }

  private void OnClosed()
  {
    if (this.transitions != null)
    {
      int index = 0;
      for (int count = this.transitions.Count; index < count; ++index)
      {
        if (this.transitions[index].isBusy)
          return;
      }
    }
    this.uiVisible = false;
    this._state = UIBehaviour.STATE.CLOSE;
    this.OnClose();
  }

  protected virtual void OnCloseStart()
  {
  }

  protected virtual void OnClose()
  {
  }

  public void RefreshUI()
  {
    if (Object.op_Inequality((Object) this.transferUI, (Object) null))
    {
      this.transferUI.RefreshUI();
    }
    else
    {
      if (!Object.op_Equality((Object) this.collectUI, (Object) null) && !((Component) this.collectUI).gameObject.activeInHierarchy)
        return;
      this.UpdateUI();
    }
  }

  public virtual void UpdateUI()
  {
    if (((Component) this).gameObject.activeInHierarchy)
      return;
    Log.Error(LOG.UI, "UpdateUI : activeInHierarchy = false");
  }

  public virtual void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((this.GetUpdateUINotifyFlags() & flags) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.RefreshUI();
  }

  public virtual void OnModifyChat(MainChat.NOTIFY_FLAG flag)
  {
  }

  public virtual string GetCaptionText() => (string) null;

  public void PlayAudio(Enum audio_label, float volume = 1f, bool as_jingle = false)
  {
    if (Object.op_Equality((Object) this.resourceLink, (Object) null))
    {
      Log.Error("resourceLinkがありません");
    }
    else
    {
      int int32 = Convert.ToInt32((object) audio_label);
      this.PlayAudio(int32, volume, int32, as_jingle);
    }
  }

  public void PlayAudio(int se_id, float volume, int config_id, bool as_jingle)
  {
    string se = ResourceName.GetSE(se_id);
    int config_id1 = config_id != 0 ? config_id : se_id;
    AudioClip clip = this.resourceLink.Get<AudioClip>(se);
    if (Object.op_Equality((Object) clip, (Object) null))
      Log.Error("AudioClip{0}がありません", (object) se);
    else if (as_jingle)
      SoundManager.PlayUISE(clip, volume, false, (Transform) null, config_id1);
    else
      SoundManager.PlayOneshotJingle(clip, se_id);
  }

  public void CacheAudio(LoadingQueue load_queue)
  {
    System.Type type = System.Type.GetType(((Object) this).name + "+AUDIO");
    if (!(type != (System.Type) null))
      return;
    foreach (int se_id in (int[]) Enum.GetValues(type))
      load_queue.CacheSE(se_id);
  }

  public T[] GetPagingList<T>(T[] list, int numInPage, int nowPage) where T : class
  {
    int num = 1 + (list.Length - 1) / numInPage;
    int sourceIndex = numInPage * (nowPage - 1);
    int length = nowPage == num ? list.Length - sourceIndex : numInPage;
    T[] destinationArray = new T[length];
    Array.Copy((Array) list, sourceIndex, (Array) destinationArray, 0, length);
    return destinationArray;
  }

  private void ActivateScrollBarCollider(UIScrollView scroll_view, bool activate)
  {
    if (!Object.op_Inequality((Object) scroll_view.verticalScrollBar, (Object) null))
      return;
    GameObject gameObject = ((Component) scroll_view.verticalScrollBar).gameObject;
    if (!Object.op_Inequality((Object) gameObject, (Object) null))
      return;
    Collider component = gameObject.GetComponent<Collider>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.enabled = activate;
  }

  public enum STATE
  {
    CLOSE,
    TO_OPEN,
    OPEN,
    TO_CLOSE,
  }

  private class PrefabData
  {
    public string name;
    public GameObject prefab;
    public GameObject inactiveObject;

    public Transform Realizes(Transform parent)
    {
      return Object.op_Equality((Object) this.prefab, (Object) null) ? (Transform) null : (!Object.op_Inequality((Object) this.inactiveObject, (Object) null) ? ResourceUtility.Realizes((Object) this.prefab, parent, 5) : InstantiateManager.Realizes(ref this.inactiveObject, parent, 5));
    }
  }

  protected struct LabelWidthLimitter
  {
    private UILabel label;
    private int width;
    private bool fixAnchorForEffect;
    private int defaultHeight;

    public LabelWidthLimitter(UILabel label, int width, bool fixAnchorForEffect)
    {
      this.label = label;
      this.width = width;
      this.defaultHeight = label.height;
      this.fixAnchorForEffect = fixAnchorForEffect;
    }

    public void Update()
    {
      if (this.label.width > this.width)
      {
        this.label.overflowMethod = UILabel.Overflow.ShrinkContent;
        this.label.width = this.width;
        this.label.height = Mathf.RoundToInt(this.label.printedSize.y);
      }
      else if (this.label.width < this.width)
      {
        this.label.overflowMethod = UILabel.Overflow.ResizeFreely;
        this.label.height = this.defaultHeight;
      }
      if (!this.fixAnchorForEffect)
        return;
      this.label.bottomAnchor.absolute = 0;
      this.label.topAnchor.absolute = this.label.height;
    }
  }
}
