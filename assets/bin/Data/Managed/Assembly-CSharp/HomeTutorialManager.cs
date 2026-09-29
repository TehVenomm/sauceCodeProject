// Decompiled with JetBrains decompiler
// Type: HomeTutorialManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeTutorialManager : MonoBehaviour
{
  private bool is_loading;
  private HomeTop m_home_top;
  private Object prefab_dialog;
  private Object prefab_target_area;
  private Transform t_target_area;
  private UITutorialHomeDialog ui_tutorial_dialog;
  private UI_LastTutorial last_tutorial;
  private Transform pamelaArrow;
  private Transform questArrow;
  private Vector3 pamelaPosition = new Vector3(-4.28f, 1.66f, 4125f * (float) Math.PI / 887f);
  private Vector3 questPosition = new Vector3(3.2f, 2.8f, 14f);

  public UITutorialHomeDialog dialog
  {
    get
    {
      if (Object.op_Equality((Object) this.ui_tutorial_dialog, (Object) null) && Object.op_Inequality(this.prefab_dialog, (Object) null))
      {
        this.ui_tutorial_dialog = ((Component) ResourceUtility.Realizes(this.prefab_dialog)).GetComponent<UITutorialHomeDialog>();
        this.ui_tutorial_dialog.Close();
      }
      return this.ui_tutorial_dialog;
    }
  }

  public void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.dialog, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.dialog).gameObject);
  }

  public static bool DoesTutorial()
  {
    return TutorialStep.IsPlayingFirstAccept() || TutorialStep.IsPlayingFirstDelivery();
  }

  public static bool DoesTutorialAfterGacha2()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) && (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS));
  }

  public static bool ShouldRunGachaTutorial()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_START);
  }

  public static bool ShouldRunQuestShadowTutorial()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.DONE_CHANGE_WEAPON) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN);
  }

  public void Setup()
  {
    this.is_loading = true;
    if (HomeTutorialManager.DoesTutorial())
      this.StartCoroutine(this.DoSetupFirstHome());
    else if (HomeTutorialManager.DoesTutorialAfterGacha2())
      this.StartCoroutine(this.DoSetupTutorialAfterGacha2());
    else
      this.is_loading = false;
  }

  public void ExcuteDoSetupTutorialAfterGacha2()
  {
    this.StartCoroutine(this.DoSetupTutorialAfterGacha2());
  }

  private IEnumerator DoSetupFirstHome()
  {
    if (Object.op_Equality((Object) this.m_home_top, (Object) null))
      this.m_home_top = ((Component) this).GetComponent<HomeTop>();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject obj_target_area = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01");
    LoadObject obj_tutorial_dialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.prefab_target_area = obj_target_area.loadedObject;
    this.prefab_dialog = obj_tutorial_dialog.loadedObject;
    this.HidePassengers();
    this.SetTargetAreaNPC(0);
    this.SetDialog(HomeTutorialManager.DialogType.TALK_WITH_PAMERA);
    this.is_loading = false;
  }

  private IEnumerator DoSetupTutorialAfterGacha2()
  {
    if (Object.op_Equality((Object) this.m_home_top, (Object) null))
      this.m_home_top = ((Component) this).GetComponent<HomeTop>();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject obj_target_area = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01");
    LoadObject obj_tutorial_dialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.prefab_target_area = obj_target_area.loadedObject;
    this.prefab_dialog = obj_tutorial_dialog.loadedObject;
    this.HidePassengers();
    this.SetDialog(HomeTutorialManager.DialogType.AFTER_GACHA2);
    this.is_loading = false;
  }

  public void SetupGachaQuestTutorial()
  {
    this.is_loading = true;
    this.StartCoroutine(this.DoSetupGachaQuestTutorial());
  }

  private IEnumerator DoSetupGachaQuestTutorial()
  {
    if (Object.op_Equality((Object) this.m_home_top, (Object) null))
      this.m_home_top = ((Component) this).GetComponent<HomeTop>();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject obj_target_area = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01");
    LoadObject obj_tutorial_dialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialHomeDialog");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.prefab_target_area = obj_target_area.loadedObject;
    this.prefab_dialog = obj_tutorial_dialog.loadedObject;
    this.HidePassengers();
    this.SetTargetAreaNPC(2);
    this.is_loading = false;
  }

  private void HidePassengers()
  {
    if (!MonoBehaviourSingleton<HomeManager>.IsValid() || MonoBehaviourSingleton<HomeManager>.I.IHomePeople == null)
      return;
    MonoBehaviourSingleton<HomeManager>.I.IHomePeople.charas.ForEach((Action<HomeCharacterBase>) (o =>
    {
      if (!(o is HomePlayerCharacter))
        return;
      ((Component) o).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) null, (Object) o.GetNamePlate()))
        return;
      ((Component) o.GetNamePlate()).gameObject.SetActive(false);
    }));
  }

  private void SetTargetAreaNPC(int npc_id)
  {
    if (Object.op_Equality(this.prefab_target_area, (Object) null) || !MonoBehaviourSingleton<HomeManager>.IsValid() || MonoBehaviourSingleton<HomeManager>.I.IHomePeople == null)
      return;
    HomeNPCCharacter homeNpcCharacter = MonoBehaviourSingleton<HomeManager>.I.IHomePeople.GetHomeNPCCharacter(npc_id);
    if (Object.op_Equality((Object) homeNpcCharacter, (Object) null))
      return;
    this.t_target_area = ResourceUtility.Realizes(this.prefab_target_area, ((Component) homeNpcCharacter).transform);
    this.t_target_area.position = ((Component) homeNpcCharacter).transform.position;
  }

  private void SetDialog(HomeTutorialManager.DialogType type)
  {
    switch (type)
    {
      case HomeTutorialManager.DialogType.TALK_WITH_PAMERA:
        if (!Object.op_Inequality((Object) this.dialog, (Object) null))
          break;
        this.dialog.Open(0, "Tutorial_Request_Text_0701");
        break;
      case HomeTutorialManager.DialogType.LAST_TUTORIAL:
        if (!Object.op_Inequality((Object) this.last_tutorial, (Object) null))
          break;
        this.last_tutorial.OpenLastTutorial();
        break;
      case HomeTutorialManager.DialogType.AFTER_GACHA2:
        if (!Object.op_Inequality((Object) this.dialog, (Object) null))
          break;
        this.StartCoroutine(this.IEAfterGacha());
        break;
    }
  }

  private IEnumerator IEAfterGacha()
  {
    yield return (object) this.SetupAllArrow();
  }

  private IEnumerator SetupArrow(Vector3 position, bool isPamela = true)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
    {
      "mdl_arrow_01"
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Vector3 vector3_1 = position;
    Vector3 vector3_2;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_2).\u002Ector(4f, 4f, 4f);
    if (isPamela)
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
      {
        this.pamelaArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
        ResourceUtility.Realizes(loadedArrow.loadedObject, this.pamelaArrow);
        this.pamelaArrow.localScale = vector3_2;
        this.pamelaArrow.position = vector3_1;
        this.dialog.OpenAfterGacha2();
        this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 1U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
      }
    }
    else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST))
    {
      this.questArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
      ResourceUtility.Realizes(loadedArrow.loadedObject, this.questArrow);
      this.questArrow.localScale = vector3_2;
      this.questArrow.position = vector3_1;
      this.dialog.OpenAfterGacha2();
      this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 2U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
    }
  }

  private IEnumerator SetupAllArrow()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
    {
      "mdl_arrow_01"
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Vector3 ARROW_SCALE = new Vector3(4f, 4f, 4f);
    if (HomeBase.isFirstTimeDisplayTextTutorial)
    {
      bool showMessage = false;
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
      {
        yield return (object) new WaitForSeconds(2f);
        this.pamelaArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
        ResourceUtility.Realizes(loadedArrow.loadedObject, this.pamelaArrow);
        this.pamelaArrow.localScale = ARROW_SCALE;
        this.pamelaArrow.position = this.pamelaPosition;
        yield return (object) new WaitForSeconds(0.5f);
        this.dialog.OpenAfterGacha2();
        this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 1U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
        showMessage = true;
        yield return (object) new WaitForSeconds(3.5f);
      }
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST))
      {
        if (showMessage)
          this.dialog.Close();
        this.questArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
        ResourceUtility.Realizes(loadedArrow.loadedObject, this.questArrow);
        this.questArrow.localScale = ARROW_SCALE;
        this.questArrow.position = this.questPosition;
        yield return (object) new WaitForSeconds(0.5f);
        this.dialog.OpenAfterGacha2();
        this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 2U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
        showMessage = true;
        yield return (object) new WaitForSeconds(3.5f);
      }
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS))
      {
        if (showMessage)
          this.dialog.Close();
        MonoBehaviourSingleton<UIManager>.I.mainStatus.SetTutArrowActive(true);
        yield return (object) new WaitForSeconds(0.5f);
        this.dialog.OpenAfterGacha2();
        this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 3U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
        yield return (object) new WaitForSeconds(3.5f);
      }
      this.dialog.Close();
    }
    else
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_GACHA2))
      {
        this.pamelaArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
        ResourceUtility.Realizes(loadedArrow.loadedObject, this.pamelaArrow);
        this.pamelaArrow.localScale = ARROW_SCALE;
        this.pamelaArrow.position = this.pamelaPosition;
      }
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_QUEST))
      {
        this.questArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform);
        ResourceUtility.Realizes(loadedArrow.loadedObject, this.questArrow);
        this.questArrow.localScale = ARROW_SCALE;
        this.questArrow.position = this.questPosition;
      }
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS))
        MonoBehaviourSingleton<UIManager>.I.mainStatus.SetTutArrowActive(true);
    }
  }

  private void SetupUIArrow()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS))
    {
      if (!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainStatus, (Object) null))
        return;
      MonoBehaviourSingleton<UIManager>.I.mainStatus.SetTutArrowActive(false);
    }
    else
    {
      MonoBehaviourSingleton<UIManager>.I.mainStatus.SetTutArrowActive(true);
      this.dialog.OpenAfterGacha2();
      this.dialog.OpenMessage(StringTable.Get(STRING_CATEGORY.TUTORIAL_NEW_STR, 3U).Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
    }
  }

  public void DeleteArrow()
  {
    if (Object.op_Inequality((Object) this.pamelaArrow, (Object) null))
      Object.Destroy((Object) ((Component) this.pamelaArrow).gameObject);
    if (!Object.op_Inequality((Object) this.questArrow, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.questArrow).gameObject);
  }

  public void ForceDeleteArrow(bool isPamela)
  {
    if (isPamela && Object.op_Inequality((Object) this.pamelaArrow, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.pamelaArrow).gameObject);
    }
    else
    {
      if (isPamela || !Object.op_Inequality((Object) this.questArrow, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.questArrow).gameObject);
    }
  }

  private void OpenDialog()
  {
  }

  public void CloseDialog()
  {
    if (!Object.op_Inequality((Object) this.dialog, (Object) null))
      return;
    this.dialog.Close();
  }

  public bool IsLoading() => this.is_loading;

  public void DisableTargetArea()
  {
    if (!Object.op_Inequality((Object) this.t_target_area, (Object) null))
      return;
    ((Component) this.t_target_area).gameObject.SetActive(false);
  }

  private void OnDisable() => this.DeleteArrow();

  private enum DialogType
  {
    TALK_WITH_PAMERA,
    LAST_TUTORIAL,
    AFTER_GACHA2,
  }
}
