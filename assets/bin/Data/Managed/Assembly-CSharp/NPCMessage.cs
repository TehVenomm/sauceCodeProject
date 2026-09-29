// Decompiled with JetBrains decompiler
// Type: NPCMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class NPCMessage : UIBehaviour
{
  private NPCMessageTable.Section section;
  private NPCMessageTable.Message message;
  private NPCTable.NPCData npcData;
  private Transform model;
  private bool isLoading;
  private NPCMessage.UI targetTex;
  private Transform voice;
  private bool isShowMessage = true;
  private bool needUpdateAnchors;

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

  private void OnScreenRotate(bool is_portrait) => this.needUpdateAnchors = true;

  public void UpdateMessage(GameSceneTables.SectionData section_data, bool is_open)
  {
    if (section_data == (GameSceneTables.SectionData) null || section_data != (GameSceneTables.SectionData) null && this.section != null && this.section.name == section_data.sectionName)
      return;
    if (!is_open)
    {
      this.Close();
    }
    else
    {
      int num = 100;
      this.section = Singleton<NPCMessageTable>.I.GetSection(section_data.sectionName);
      if (this.section == null)
      {
        HomeNPCTalk currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as HomeNPCTalk;
        if (Object.op_Inequality((Object) currentSection, (Object) null))
        {
          this.section = Singleton<NPCMessageTable>.I.GetSection($"{section_data.sectionName}_{currentSection.npcID:D3}");
          num = currentSection.baseDepth + 1;
        }
        if (this.section == null)
          return;
      }
      this.baseDepth = num;
      this.message = this.section.GetNPCMessage();
      this.LoadModel();
    }
  }

  protected override void OnClose()
  {
    this.DeleteModel();
    this.section = (NPCMessageTable.Section) null;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0 || !this.isLoading)
      return;
    this.DeleteModel();
  }

  private void LoadModel()
  {
    this.DeleteModel();
    this.targetTex = NPCMessage.UI.TEX_NPC;
    this.InitRenderTexture((Enum) this.targetTex, 45f);
    this.model = Utility.CreateGameObject("NPC", this.GetRenderTextureModelTransform((Enum) this.targetTex), this.GetRenderTextureLayer((Enum) this.targetTex));
    this.npcData = Singleton<NPCTable>.I.GetNPCData(this.message.npc);
    this.isLoading = true;
    this.npcData.LoadModel(((Component) this.model).gameObject, false, false, new Action<Animator>(this.OnModelLoadComplete), false);
  }

  private void OnModelLoadComplete(Animator animator)
  {
    if (this.message.has_voice)
      this.StartCoroutine(this.DoCacheVoice((System.Action) (() => this.OpenMessage(animator))));
    else
      this.OpenMessage(animator);
  }

  private IEnumerator DoCacheVoice(System.Action on_complete)
  {
    if (this.message != null && this.message.has_voice && Object.op_Inequality((Object) this.model, (Object) null))
    {
      NPCLoader component = ((Component) this.model).GetComponent<NPCLoader>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) component);
        loadingQueue.CacheVoice(this.message.voice_id);
        yield return (object) loadingQueue.Wait();
      }
    }
    on_complete();
  }

  private void OpenMessage(Animator animator)
  {
    this.isLoading = false;
    this.Open();
    if (this.needUpdateAnchors)
    {
      this.needUpdateAnchors = false;
      this.UpdateAnchors();
    }
    this.model.localPosition = this.message.pos;
    this.model.localEulerAngles = this.message.rot;
    if (Object.op_Inequality((Object) animator, (Object) null))
      PlayerAnimCtrl.Get(animator, PlayerAnimCtrl.StringToEnum(this.npcData.anim));
    this.EnableRenderTexture((Enum) this.targetTex);
    string replaceText = this.message.GetReplaceText();
    this.SetColor((Enum) NPCMessage.UI.SPR_MESSAGE, !this.isShowMessage || string.IsNullOrEmpty(replaceText) ? Color.clear : Color.white);
    this.isShowMessage = true;
    this.SetLabelText((Enum) NPCMessage.UI.LBL_MESSAGE, replaceText);
    this.SetLabelText((Enum) NPCMessage.UI.LBL_NAME, this.npcData.displayName);
    if (this.message.has_voice)
      SoundManager.PlayVoice(this.message.voice_id);
    if (this.targetTex != NPCMessage.UI.TEX_QUEST_NPC)
      return;
    this.InitUITweener<TweenColor>((Enum) NPCMessage.UI.TEX_QUEST_NPC, true, new EventDelegate.Callback(this.DeleteModel));
    this.InitUITweener<TweenColor>((Enum) NPCMessage.UI.SPR_MESSAGE, true);
  }

  private void DeleteModel()
  {
    this.DeleteRenderTexture((Enum) this.targetTex);
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.model).gameObject);
      this.model = (Transform) null;
    }
    this.isLoading = false;
  }

  public void HideMessage() => this.isShowMessage = false;

  private enum UI
  {
    TEX_NPC,
    TEX_QUEST_NPC,
    LBL_MESSAGE,
    LBL_NAME,
    SPR_MESSAGE,
  }
}
