// Decompiled with JetBrains decompiler
// Type: FixedPanelNGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FixedPanelNGUI : MonoBehaviour
{
  public const float OffsetTopIphoneX = 75f;
  public static float BASERATIO = 0.5590062f;
  public Transform m_parent;
  public int m_depthParent = 1;
  public FixedPanelAction FixedPanelAction;
  public Transform m_mainChild;
  public Transform[] m_lockPosition;
  public Transform[] m_lockScaleUI;
  public Transform[] m_scaleUIChilds;
  [SerializeField]
  public Transform[] m_updateNewAnchor;
  [SerializeField]
  public Transform[] m_unlockStatic;
  [SerializeField]
  public LockAnchorRoot[] m_lockAnchor;
  [SerializeField]
  public global::FixOffsetHeigh[] m_fixOffsetPosition;
  private float ratio;
  private float ratioHeight;
  public static Transform Root;
  private float aspect;
  private Vector2 resulotion;
  public bool isLockAnchor;
  private bool detectCameraParent;
  public UIRect anchorCheat;
  private List<Vector3> m_listOldLockPosition;
  private FixedPanelNGUI.BeginStaticUI[] m_beginStaticUI;

  private void Awake()
  {
    this.aspect = (float) Screen.width / (float) Screen.height;
    FixedNGUIThrowIphoneX component = ((Component) this).GetComponent<FixedNGUIThrowIphoneX>();
    this.resulotion = new Vector2();
    this.resulotion = !FixedPanelNGUI.IsIphoneX() ? FixedPanelNGUI.GetResolutionFixed() : (!Object.op_Inequality((Object) component, (Object) null) ? FixedPanelNGUI.GetResolutionFixed() : FixedPanelNGUI.GetResolutionFixed(component.LockRatioPosition));
    this.ratioHeight = this.resulotion.x / this.resulotion.y / FixedPanelNGUI.BASERATIO;
    this.ratio = this.aspect / FixedPanelNGUI.BASERATIO;
    if ((double) this.aspect >= (double) FixedPanelNGUI.BASERATIO)
      return;
    if (Object.op_Equality((Object) FixedPanelNGUI.Root, (Object) null))
      FixedPanelNGUI.Root = GameObject.Find("UI_Root").transform;
    if (Object.op_Inequality((Object) FixedPanelNGUI.Root, (Object) null))
      this.ChangeTarget(FixedPanelNGUI.Root);
    this.saveBeginStaticUI();
  }

  private void Start()
  {
    if ((double) this.aspect >= (double) FixedPanelNGUI.BASERATIO)
      return;
    if (Object.op_Equality((Object) this.m_mainChild, (Object) null))
      this.m_mainChild = ((Component) this).transform;
    this.findParent();
    if (FixedPanelNGUI.IsIphoneX())
    {
      this.detectCameraParent = FixedPanelNGUI.checkCameraParent(((Component) this).gameObject);
      if (this.detectCameraParent)
      {
        Transform parent = this.m_parent;
        parent.localPosition = Vector3.op_Addition(parent.localPosition, Vector3.op_Division(Vector3.op_Multiply(Vector3.down, FixedPanelNGUI.GetOffseMoveHeightIfIphoneX(this.resulotion.y)), 2f));
      }
    }
    if (!this.checkExistComponent() && this.FixedPanelAction != FixedPanelAction.NONE)
      return;
    this.StartCoroutine(this.fixedNGUI());
  }

  public static bool checkCameraParent(GameObject obj)
  {
    while (Object.op_Inequality((Object) obj.transform.parent, (Object) null))
    {
      obj = ((Component) obj.transform.parent).gameObject;
      if (Object.op_Inequality((Object) obj.GetComponent<Camera>(), (Object) null))
        return true;
    }
    return false;
  }

  private bool checkExistComponent()
  {
    Transform parent = this.m_parent;
    while (Object.op_Inequality((Object) ((Component) parent).transform.parent, (Object) null) && Object.op_Inequality((Object) ((Component) parent).transform.parent, (Object) FixedPanelNGUI.Root) && ((Object) ((Component) parent).transform.parent).name != "AppMain")
    {
      parent = ((Component) parent).transform.parent;
      foreach (Transform transform in parent)
      {
        if (Object.op_Inequality((Object) transform, (Object) ((Component) this).transform) && Object.op_Inequality((Object) ((Component) transform).GetComponent<FixedPanelNGUI>(), (Object) null))
          return false;
      }
    }
    return true;
  }

  private void findParent()
  {
    this.m_parent = ((Component) this).transform;
    if (this.m_depthParent <= 0)
      return;
    for (int index = 0; index < this.m_depthParent; ++index)
      this.m_parent = this.m_parent.parent;
  }

  private IEnumerator fixedNGUI()
  {
    Vector3 vector3 = Vector3.op_Multiply(Vector3.one, this.ratio);
    if ((double) this.aspect < (double) FixedPanelNGUI.BASERATIO)
    {
      this.lockUI(false);
      List<Vector3> vector3List = new List<Vector3>();
      foreach (Transform transform in this.m_mainChild)
        vector3List.Add(transform.position);
      this.findListPositionLockTransform();
      bool flag = true;
      switch (this.FixedPanelAction)
      {
        case FixedPanelAction.NONE:
          flag = false;
          break;
        case FixedPanelAction.FIX_SIZE:
          this.m_parent.localScale = vector3;
          break;
      }
      if (flag)
      {
        for (int index = 0; index < vector3List.Count; ++index)
        {
          Transform child = this.m_mainChild.GetChild(index);
          UIRect component = ((Component) child).GetComponent<UIRect>();
          if (Object.op_Inequality((Object) component, (Object) null) && component.isAnchoredHorizontally)
          {
            child.position = vector3List[index];
          }
          else
          {
            Vector3 localPosition = this.m_mainChild.GetChild(index).localPosition;
            this.m_mainChild.GetChild(index).localPosition = new Vector3(localPosition.x, localPosition.y / this.ratioHeight, localPosition.z);
          }
        }
      }
      this.fixLockScale(this.ratio);
      this.fixScaleChilds(this.ratio);
      yield return (object) this.StartCoroutine(this.fixedLockTransform(this.ratio));
      this.UpdateAnchor();
      this.FixOffsetHeigh();
      if (Object.op_Inequality((Object) this.anchorCheat, (Object) null))
        this.anchorCheat.SetAnchor((Transform) null);
    }
  }

  private void findListPositionLockTransform()
  {
    if (this.m_lockPosition == null)
      return;
    this.m_listOldLockPosition = new List<Vector3>();
    for (int index = 0; index < this.m_lockPosition.Length; ++index)
      this.m_listOldLockPosition.Add(this.m_lockPosition[index].position);
  }

  private IEnumerator fixedLockTransform(float ratioScreen)
  {
    yield return (object) null;
    if (this.m_lockPosition != null)
    {
      for (int index = 0; index < this.m_lockPosition.Length; ++index)
        this.m_lockPosition[index].position = this.m_listOldLockPosition[index];
    }
  }

  private void fixLockScale(float ratio)
  {
    foreach (Transform transform in this.m_lockScaleUI)
      transform.localScale = Vector3.op_Division(transform.localScale, ratio);
  }

  private void fixScaleChilds(float ratio)
  {
    foreach (Transform scaleUiChild in this.m_scaleUIChilds)
      scaleUiChild.localScale = Vector3.op_Multiply(scaleUiChild.localScale, ratio);
  }

  private void ChangeTarget(Transform root)
  {
    if (this.m_lockAnchor == null)
      return;
    foreach (LockAnchorRoot lockAnchorRoot in this.m_lockAnchor)
    {
      UIRect component = ((Component) lockAnchorRoot.Panel).GetComponent<UIRect>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.leftAnchor.target = !lockAnchorRoot.FullAnchor ? (component.rightAnchor.target = root) : (component.rightAnchor.target = component.topAnchor.target = component.bottomAnchor.target = root);
    }
  }

  private void saveBeginStaticUI()
  {
    if (this.m_unlockStatic == null || this.m_unlockStatic.Length == 0)
      return;
    this.m_beginStaticUI = new FixedPanelNGUI.BeginStaticUI[this.m_unlockStatic.Length];
    for (int index = 0; index < this.m_unlockStatic.Length; ++index)
    {
      this.m_beginStaticUI[index] = new FixedPanelNGUI.BeginStaticUI();
      this.m_beginStaticUI[index].UiStatic = ((Component) this.m_unlockStatic[index]).GetComponent<UIStaticPanelChanger>();
      if (Object.op_Equality((Object) this.m_beginStaticUI[index].UiStatic, (Object) null))
        this.m_beginStaticUI[index].UIRotationStatic = ((Component) this.m_unlockStatic[index]).GetComponent<UIStaticPanelRotateCheck>();
      this.m_beginStaticUI[index].UIPanel = ((Component) this.m_unlockStatic[index]).GetComponent<UIPanel>();
      this.m_beginStaticUI[index].BeginStatic = this.m_beginStaticUI[index].UIPanel.widgetsAreStatic;
    }
  }

  private void lockUI(bool isLock)
  {
    if (this.m_beginStaticUI == null)
      return;
    for (int index = 0; index < this.m_beginStaticUI.Length; ++index)
    {
      FixedPanelNGUI.BeginStaticUI beginStaticUi = this.m_beginStaticUI[index];
      if (Object.op_Inequality((Object) beginStaticUi.UIPanel, (Object) null))
      {
        if (Object.op_Inequality((Object) beginStaticUi.UiStatic, (Object) null))
        {
          if (isLock)
          {
            ((Behaviour) beginStaticUi.UiStatic).enabled = true;
            beginStaticUi.UIPanel.widgetsAreStatic = beginStaticUi.BeginStatic;
          }
          else
          {
            ((Behaviour) beginStaticUi.UiStatic).enabled = false;
            beginStaticUi.UIPanel.widgetsAreStatic = false;
          }
        }
        else if (Object.op_Inequality((Object) beginStaticUi.UIRotationStatic, (Object) null))
        {
          if (isLock)
          {
            ((Behaviour) beginStaticUi.UIRotationStatic).enabled = true;
            beginStaticUi.UIPanel.widgetsAreStatic = beginStaticUi.BeginStatic;
          }
          else
          {
            ((Behaviour) beginStaticUi.UIRotationStatic).enabled = false;
            beginStaticUi.UIPanel.widgetsAreStatic = false;
          }
        }
      }
    }
  }

  public void FixOffsetHeigh()
  {
    if (this.m_fixOffsetPosition == null)
      return;
    foreach (global::FixOffsetHeigh fixOffsetHeigh in this.m_fixOffsetPosition)
    {
      if (!fixOffsetHeigh.IsOnlyInphoneX || fixOffsetHeigh.IsOnlyInphoneX && FixedPanelNGUI.IsIphoneX())
      {
        Transform objectMove = fixOffsetHeigh.ObjectMove;
        objectMove.localPosition = Vector3.op_Addition(objectMove.localPosition, Vector3.op_Multiply(fixOffsetHeigh.OffsetHeigh, Vector3.up));
      }
      Transform objectMove1 = fixOffsetHeigh.ObjectMove;
      objectMove1.localPosition = Vector3.op_Addition(objectMove1.localPosition, Vector3.op_Multiply(fixOffsetHeigh.OffsetWidt, Vector3.right));
    }
  }

  public void UpdateAnchor()
  {
    if (this.m_updateNewAnchor == null)
      return;
    foreach (Component component1 in this.m_updateNewAnchor)
    {
      UIRect component2 = component1.GetComponent<UIRect>();
      if (Object.op_Inequality((Object) component2, (Object) null))
        component2.UpdateAnchors();
    }
  }

  public static bool CheckResolutionCanFix()
  {
    double num1 = (double) Screen.width / (double) Screen.height;
    Vector2 resolutionFixed = FixedPanelNGUI.GetResolutionFixed();
    double num2 = (double) resolutionFixed.x / (double) resolutionFixed.y / (double) FixedPanelNGUI.BASERATIO;
    double baseratio = (double) FixedPanelNGUI.BASERATIO;
    return num1 < baseratio;
  }

  public static float GetScaleResolution()
  {
    double num1 = (double) Screen.width / (double) Screen.height;
    Vector2 resolutionFixed = FixedPanelNGUI.GetResolutionFixed();
    double num2 = (double) resolutionFixed.x / (double) resolutionFixed.y / (double) FixedPanelNGUI.BASERATIO;
    double baseratio = (double) FixedPanelNGUI.BASERATIO;
    return (float) (num1 / baseratio);
  }

  public static Vector2 GetResolutionFixed(bool isThrowIphoneX = false)
  {
    float width = (float) Screen.width;
    float height = (float) Screen.height;
    float num;
    float screenHeight;
    if ((double) width > (double) height)
    {
      num = 854f;
      screenHeight = (float) ((double) height / (double) width * 854.0);
    }
    else
    {
      screenHeight = 854f;
      num = (float) ((double) width / (double) height * 854.0);
    }
    if (!isThrowIphoneX && FixedPanelNGUI.IsIphoneX())
    {
      float moveHeightIfIphoneX = FixedPanelNGUI.GetOffseMoveHeightIfIphoneX(screenHeight);
      screenHeight -= moveHeightIfIphoneX;
    }
    return new Vector2(num, screenHeight);
  }

  public static float GetOffseMoveHeightIfIphoneX(float screenHeight)
  {
    return (float) (75.0 * ((double) screenHeight / 2436.0));
  }

  public static bool IsIphoneX()
  {
    int width = Screen.width;
    int height = Screen.height;
    return false;
  }

  private class BeginStaticUI
  {
    public UIStaticPanelChanger UiStatic;
    public UIStaticPanelRotateCheck UIRotationStatic;
    public UIPanel UIPanel;
    public bool BeginStatic;
  }
}
