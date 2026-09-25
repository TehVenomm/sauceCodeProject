// Decompiled with JetBrains decompiler
// Type: SceneSettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SceneSettingsManager : MonoBehaviourSingleton<SceneSettingsManager>
{
  private const string OBJECTS_ROOT_NAME = "Objects";
  private const string BINGO_BOARD_OBJECT_NAME = "bingoboard";
  private const string BINGO_BLOCK_OBJECT_NAME = "block";
  private const string COLIDER_ROOT_NAME = "Collider";
  [Tooltip("フォグカラー")]
  public Color fogColor = Color.white;
  [Tooltip("フォグ開始距離。フォグモードLinearでのみ使用")]
  public float linearFogStart;
  [Tooltip("フォグ終了距離。フォグモードLinearでのみ使用")]
  public float linearFogEnd = 300f;
  [Tooltip("リムカラー")]
  public Color rimColor = new Color(1f, 1f, 1f, 1f);
  [Tooltip("アンビエントカラー")]
  public Color ambientColor = new Color(0.2f, 0.2f, 0.2f);
  [Tooltip("エフェクト用カラー")]
  public Color effectColor = new Color(0.2f, 0.2f, 0.2f);
  [Tooltip("エディタ用アンビエントカラー有効フラグ")]
  public bool enableEditorAmbientColor;
  [Tooltip("エディタ用アンビエントカラー")]
  public Color editorAmbientColor = new Color(0.2f, 0.2f, 0.2f);
  [Tooltip("属性ID（このIDによってエフェクトが変化する、など")]
  public int attributeID = 1;
  [Tooltip("フォグの開始距離での限界濃度")]
  public float limitFogStart;
  [Tooltip("フォグの終了距離での限界濃度")]
  public float limitFogEnd = 1f;
  [Tooltip("ライトプローブカラー乗算")]
  public Color lightProbeMul = new Color(1f, 1f, 1f, 1f);
  [Tooltip("ライトプローブカラー加算")]
  public Color lightProbeAdd = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  [Tooltip("ライトプローブカラークランプ値")]
  public float lightProbePeak = 2f;
  [Tooltip("NPC用アンビエントカラー")]
  public Color npcAmbientColor = new Color(1f, 1f, 1f, 0.0f);
  [Tooltip("NPC用ライト方向")]
  public Vector3 npcLightDir = new Vector3(0.0f, 0.0f, 0.0f);
  [Tooltip("フォグ強制ON")]
  public bool forceFogON;
  public Object[] linkResources;
  [Tooltip("マップの内外情報保存フラグ")]
  public bool saveInsideCollider = true;
  [Tooltip("マップの内外情報")]
  public SceneSettingsManager.InsideColliderData insideColliderData = new SceneSettingsManager.InsideColliderData();
  [Tooltip("天候変化の情報")]
  public WeatherController weatherController = new WeatherController();
  [Tooltip("防衛戦対象")]
  public GameObject[] waveTargets;
  private List<GameObject> addWaveTargetList = new List<GameObject>();
  [Tooltip("InsideCheckから外す")]
  public GameObject[] ignoreInsideCheckObjects;
  public SceneSettingsManager.HomeInfoCountData[] homeInfoCountData;
  [Tooltip("条件の進行度によってアクティブになるオブジェクト")]
  public GameObject[] objectsByProgress;
  private GameObject m_bingoBoardObject;
  private GameObject m_bingoBlockObject;
  public bool WeatherForceReturn;
  private Vector3 gObjContainCollidersScale;
  private GameObject gObjContainColliders;
  private string colliderNameToCompare = "Colliders";

  public GameObject GObjContainColliders
  {
    get => this.gObjContainColliders;
    set => this.gObjContainColliders = value;
  }

  protected override void Awake()
  {
    base.Awake();
    this.ApplyScene(false);
    this.ParseObjectName();
    this.addWaveTargetList.Clear();
    this.gObjContainCollidersScale = ((Component) this).transform.localScale;
    if (!Object.op_Equality((Object) this.GObjContainColliders, (Object) null))
      return;
    this.GObjContainColliders = this.OnGetGObjContainColliders();
  }

  private void ParseObjectName()
  {
    foreach (Transform transform1 in ((Component) this).transform)
    {
      if (((Object) transform1).name.Contains("Objects"))
      {
        foreach (Transform transform2 in transform1)
        {
          if (((Object) transform2).name.Contains("bingoboard"))
            this.m_bingoBoardObject = ((Component) transform2).gameObject;
          if (((Object) transform2).name.Contains("block"))
            this.m_bingoBlockObject = ((Component) transform2).gameObject;
        }
      }
    }
  }

  private void Start()
  {
    this.ApplyStageMaterial();
    this.ApplyRaiseCollider();
  }

  public void InitializeScene()
  {
    this.ApplyScene(false);
    this.ParseObjectName();
    this.addWaveTargetList.Clear();
    this.ApplyStageMaterial();
    if (Object.op_Equality((Object) this.GObjContainColliders, (Object) null))
      this.GObjContainColliders = this.OnGetGObjContainColliders();
    this.OnResizeGObjContainColliders(this.gObjContainCollidersScale);
  }

  public void ApplyScene(bool isEditorAmbientColor)
  {
    if (Application.isPlaying)
      isEditorAmbientColor = false;
    RenderSettings.fog = false;
    RenderSettings.ambientLight = isEditorAmbientColor ? this.editorAmbientColor : this.ambientColor;
    this.weatherController.Init();
  }

  public void ApplyStageMaterial()
  {
    ShaderGlobal.fogColor = this.fogColor;
    ShaderGlobal.fogNear = this.linearFogStart;
    ShaderGlobal.fogFar = this.linearFogEnd;
    ShaderGlobal.fogNearLimit = this.limitFogStart;
    ShaderGlobal.fogFarLimit = this.limitFogEnd;
    ShaderGlobal.globalRimColor = this.rimColor;
    ShaderGlobal.lightProbeMul = this.lightProbeMul;
    ShaderGlobal.lightProbeAdd = this.lightProbeAdd;
    ShaderGlobal.lightProbePeak = this.lightProbePeak;
    ShaderGlobal.npcAmbientColor = this.npcAmbientColor;
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      return;
    MonoBehaviourSingleton<GlobalSettingsManager>.I.npcLightDirection.localEulerAngles = this.npcLightDir;
  }

  public static void ApplyEffect(rymFX fx, bool force)
  {
    if (Object.op_Equality((Object) fx, (Object) null) || !force && (double) fx.BaseColor.a != 0.0)
      return;
    Color color = !MonoBehaviourSingleton<SceneSettingsManager>.IsValid() ? Color.white : MonoBehaviourSingleton<SceneSettingsManager>.I.effectColor;
    fx.BaseColor = color;
  }

  public void ApplyRaiseCollider()
  {
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      return;
    GlobalSettingsManager.InGameFieldSetting gameFieldSetting = MonoBehaviourSingleton<GlobalSettingsManager>.I.inGameFieldSetting;
    if (!gameFieldSetting.isRaiseWallCollider || !MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
      return;
    Transform transform1 = !QuestManager.IsValidInGameWaveMatch() ? Utility.FindChild(((Component) this).transform, "Colliders") : this._transform;
    if (Object.op_Inequality((Object) transform1, (Object) null))
    {
      BoxCollider[] componentsInChildren = ((Component) transform1).GetComponentsInChildren<BoxCollider>(true);
      if (!((IList<BoxCollider>) componentsInChildren).IsNullOrEmpty<BoxCollider>())
      {
        int index = 0;
        for (int length = componentsInChildren.Length; index < length; ++index)
        {
          BoxCollider boxCollider = componentsInChildren[index];
          if ((double) boxCollider.size.y <= (double) gameFieldSetting.raiseWallColliderSizeY)
          {
            boxCollider.size = new Vector3(boxCollider.size.x, gameFieldSetting.raiseWallColliderSizeY, boxCollider.size.z);
            boxCollider.center = new Vector3(boxCollider.center.x, gameFieldSetting.raiseWallColliderOffsetY, boxCollider.center.z);
          }
        }
      }
    }
    MeshCollider[] componentsInChildren1 = ((Component) this).GetComponentsInChildren<MeshCollider>(true);
    if (((IList<MeshCollider>) componentsInChildren1).IsNullOrEmpty<MeshCollider>())
      return;
    int index1 = 0;
    for (int length = componentsInChildren1.Length; index1 < length; ++index1)
    {
      Transform transform2 = ((Component) componentsInChildren1[index1]).transform;
      if ((double) transform2.localScale.y <= (double) gameFieldSetting.raiseWallColliderScaleY)
        transform2.localScale = new Vector3(transform2.localScale.x, gameFieldSetting.raiseWallColliderScaleY, transform2.localScale.z);
    }
  }

  public static T GetLinkResource<T>(string name) where T : Object
  {
    if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      return default (T);
    Object[] linkResources = MonoBehaviourSingleton<SceneSettingsManager>.I.linkResources;
    if (linkResources == null)
      return default (T);
    int index = 0;
    for (int length = linkResources.Length; index < length; ++index)
    {
      if (Object.op_Inequality(linkResources[index], (Object) null) && linkResources[index].name == name)
        return linkResources[index] as T;
    }
    return default (T);
  }

  public void ChangeWeather(float changingTime, float duration)
  {
    this.StartCoroutine(this.DoChangeWeather(changingTime, duration));
  }

  private IEnumerator DoChangeWeather(float changingTime, float duration)
  {
    float timer = 0.0f;
    this.WeatherForceReturn = false;
    this.weatherController.OnStartWeatherChange();
    while ((double) timer < (double) changingTime)
    {
      timer += Time.deltaTime;
      this.weatherController.Update(timer / changingTime);
      yield return (object) null;
    }
    this.weatherController.OnFinishedWeatherChange();
    timer = 0.0f;
    while ((double) timer < (double) duration && !this.WeatherForceReturn)
    {
      timer += Time.deltaTime;
      yield return (object) null;
    }
    timer = 0.0f;
    this.weatherController.OnStartReturnToOriginal();
    while ((double) timer < (double) changingTime)
    {
      timer += Time.deltaTime;
      this.weatherController.Update((float) (1.0 - (double) timer / (double) changingTime));
      yield return (object) null;
    }
    this.weatherController.OnFinishedReturnToOriginal();
  }

  public GameObject GetWaveTarget(string name)
  {
    if (this.waveTargets == null || this.waveTargets.Length == 0)
      return (GameObject) null;
    for (int index = 0; index < this.waveTargets.Length; ++index)
    {
      GameObject waveTarget = this.waveTargets[index];
      if (Object.op_Inequality((Object) waveTarget, (Object) null) && ((Object) waveTarget).name == name)
        return waveTarget;
    }
    return (GameObject) null;
  }

  public void AddWaveTarget(GameObject target)
  {
    if (Object.op_Equality((Object) target, (Object) null))
      return;
    this.addWaveTargetList.Add(target);
  }

  public void DisableWaveTarget()
  {
    if (!this.addWaveTargetList.IsNullOrEmpty<GameObject>())
    {
      int index = 0;
      for (int count = this.addWaveTargetList.Count; index < count; ++index)
      {
        GameObject addWaveTarget = this.addWaveTargetList[index];
        if (Object.op_Inequality((Object) addWaveTarget, (Object) null))
          addWaveTarget.SetActive(false);
      }
    }
    if (this.waveTargets == null || this.waveTargets.Length == 0)
      return;
    int index1 = 0;
    for (int length = this.waveTargets.Length; index1 < length; ++index1)
    {
      GameObject waveTarget = this.waveTargets[index1];
      if (Object.op_Inequality((Object) waveTarget, (Object) null))
        waveTarget.SetActive(false);
    }
  }

  public void SetEventItemCount(List<EventItemCounts> list)
  {
    if (list == null || list.Count <= 0 || this.homeInfoCountData == null || this.homeInfoCountData.Length == 0)
      return;
    int index1 = 0;
    for (int count = list.Count; index1 < count; ++index1)
    {
      EventItemCounts eic = list[index1];
      int index2 = 0;
      for (int length = this.homeInfoCountData.Length; index2 < length; ++index2)
      {
        SceneSettingsManager.HomeInfoCountData hicd = this.homeInfoCountData[index2];
        if (eic.eventId == hicd.eventId && eic.eventType == hicd.eventType)
        {
          this._ExecEventItemCount(eic, hicd);
          break;
        }
      }
    }
  }

  private void _ExecEventItemCount(EventItemCounts eic, SceneSettingsManager.HomeInfoCountData hicd)
  {
    int rewardGrade = eic.rewardGrade;
    if (hicd.gradeToValue != null)
    {
      for (int index = hicd.gradeToValue.Length - 1; index >= 0; --index)
      {
        if (eic.rewardGrade >= hicd.gradeToValue[index].threshold)
        {
          rewardGrade = hicd.gradeToValue[index].value;
          break;
        }
      }
    }
    if (!Object.op_Inequality((Object) hicd.animator, (Object) null))
      return;
    hicd.animator.SetInteger("Value", rewardGrade);
  }

  public void SwitchBingoObjectsActivation(bool _isActive)
  {
    if (Object.op_Inequality((Object) this.m_bingoBoardObject, (Object) null) && this.m_bingoBoardObject.activeSelf != _isActive)
      this.m_bingoBoardObject.SetActive(_isActive);
    if (!Object.op_Inequality((Object) this.m_bingoBlockObject, (Object) null) || this.m_bingoBlockObject.activeSelf == _isActive)
      return;
    this.m_bingoBlockObject.SetActive(_isActive);
  }

  public void ActivateObjectsByProgress(int progress)
  {
    if (((IList<GameObject>) this.objectsByProgress).IsNullOrEmpty<GameObject>() || this.objectsByProgress.Length <= progress)
      return;
    this.objectsByProgress[progress].SetActive(true);
  }

  public void Self() => this.SelfInstance();

  public void Remove() => this.RemoveInstance();

  private GameObject OnGetGObjContainColliders()
  {
    if (((Component) this).transform.childCount <= 0)
      return (GameObject) null;
    Transform[] componentsInChildren1 = ((Component) this).gameObject.GetComponentsInChildren<Transform>(true);
    if (componentsInChildren1 == null || componentsInChildren1.Length == 0)
      return (GameObject) null;
    foreach (Transform transform in componentsInChildren1)
    {
      if (!Object.op_Equality((Object) transform, (Object) null) && ((Object) ((Component) transform).gameObject).name.Equals(this.colliderNameToCompare) && transform.childCount > 0)
      {
        Collider[] componentsInChildren2 = ((Component) transform).GetComponentsInChildren<Collider>(true);
        if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
          return ((Component) transform).gameObject;
      }
    }
    return (GameObject) null;
  }

  public void OnResizeGObjContainColliders(Vector3 size)
  {
    if (Object.op_Equality((Object) this.GObjContainColliders, (Object) null))
      this.GObjContainColliders = this.OnGetGObjContainColliders();
    if (Object.op_Equality((Object) this.GObjContainColliders, (Object) null) || Vector3.op_Equality(size, Vector3.zero))
      return;
    if (MonoBehaviourSingleton<GoGameSettingsManager>.IsValid())
    {
      Vector3 colliderOfMapScale = MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale;
      if (Vector3.op_Equality(MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale, this.GObjContainColliders.transform.localScale))
        return;
    }
    Vector3 localScale = this.GObjContainColliders.transform.localScale;
    size.x = localScale.x;
    size.z = localScale.z;
    this.GObjContainColliders.transform.localScale = size;
  }

  [Serializable]
  public class InsideColliderData
  {
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;
    public float chipSize = 1f;
    public List<int> insideFlags = new List<int>();
  }

  [Serializable]
  public class GradeToValue
  {
    public int threshold;
    public int value;
  }

  [Serializable]
  public class HomeInfoCountData
  {
    public int eventId;
    public int eventType;
    public SceneSettingsManager.GradeToValue[] gradeToValue;
    public Animator animator;
  }
}
