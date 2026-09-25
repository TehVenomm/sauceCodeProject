// Decompiled with JetBrains decompiler
// Type: StoryDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StoryDirector : MonoBehaviourSingleton<StoryDirector>
{
  private List<StoryDirector.StoryScript> scriptCommands = new List<StoryDirector.StoryScript>();
  private const float LOCATION_SCALE = 0.01f;
  private const float LOCATION_SCALE_INV = 100f;
  private const float LOCATION_MOVE_SCALE = 0.02f;
  private const float LOCATION_MOVE_SCALE_INV = 50f;
  private const float CHARA_MAX = 4f;
  private const float TRANSITION_TIME_CAM_PAN = 0.3f;
  private const float TRANSITION_TIME_FOCUS_CHARA = 0.1f;
  public static readonly int SPEED_TYPEWRITER = 40;
  private StoryDirector.IStoryEventReceiver eventReceiver;
  private UITexture locationTex;
  private UIRenderTexture locationRednerTex;
  private Transform locationRoot;
  private Transform locationImageRoot;
  private Transform locationImage;
  private Transform locationSky;
  private UITexture effectTex;
  private UIRenderTexture effectRenderTex;
  private StringKeyTable<LoadObject> effectPrefabs = new StringKeyTable<LoadObject>();
  private Vector3 initCameraPos;
  private Vector3Interpolator cameraPosAnim = new Vector3Interpolator();
  private Vector3[] cameraPositions = new Vector3[8];
  public ShakeInterpolator cameraShakeAnim = new ShakeInterpolator();
  private List<StoryCharacter> charas = new List<StoryCharacter>();
  private bool waitMessage;
  public StoryDirector.POS tailPos;

  public bool isLoading { get; private set; }

  public bool isRunning { get; private set; }

  public void StartScript(
    int script_id,
    UITexture location_tex,
    UITexture effect_tex,
    StoryDirector.IStoryEventReceiver event_receiver)
  {
    this.locationTex = location_tex;
    this.effectTex = effect_tex;
    this.eventReceiver = event_receiver;
    for (int index = 0; index < this.cameraPositions.Length; ++index)
      this.cameraPositions[index] = new Vector3(0.0f, 0.0f, 0.0f);
    this.StartCoroutine(this.DoScript(script_id));
  }

  private IEnumerator DoScript(int script_id)
  {
    yield return (object) this.StartCoroutine(this.ParseScript(script_id));
    yield return (object) this.StartCoroutine(this.LoadScriptResources());
    while (this.isLoading)
      yield return (object) null;
    yield return (object) this.StartCoroutine(this.RunScript());
  }

  private IEnumerator ParseScript(int script_id)
  {
    this.isLoading = true;
    string storyScript1 = ResourceName.GetStoryScript(script_id);
    bool loading = true;
    string text = (string) null;
    MonoBehaviourSingleton<DataTableManager>.I.LoadStory(storyScript1, (Action<string>) (x =>
    {
      text = x;
      loading = false;
    }));
    while (loading)
      yield return (object) null;
    this.isRunning = true;
    CSVReader csvReader = new CSVReader(text, "cmd,p0,p1,p2,p3,jp");
    while (csvReader.NextLine())
    {
      StoryDirector.StoryScript storyScript2 = new StoryDirector.StoryScript();
      csvReader.Pop(ref storyScript2.cmd);
      if (!string.IsNullOrEmpty(storyScript2.cmd))
      {
        csvReader.Pop(ref storyScript2.p0);
        csvReader.Pop(ref storyScript2.p1);
        csvReader.Pop(ref storyScript2.p2);
        csvReader.Pop(ref storyScript2.p3);
        csvReader.Pop(ref storyScript2.msg);
        this.scriptCommands.Add(storyScript2);
      }
    }
  }

  private IEnumerator LoadScriptResources()
  {
    this.initCameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
    this.cameraPosAnim.Set(this.initCameraPos);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    for (int index = 0; index < this.scriptCommands.Count; ++index)
    {
      string cmd = this.scriptCommands[index].cmd;
      string p0 = this.scriptCommands[index].p0;
      string p1 = this.scriptCommands[index].p1;
      string p2 = this.scriptCommands[index].p2;
      switch (cmd)
      {
        case "EFF_SHOW":
        case "EFF_SHOW_POS":
          if (this.effectPrefabs.Get(p0) == null)
          {
            LoadObject loadObject = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, p0);
            this.effectPrefabs.Add(p0, loadObject);
            if (Object.op_Equality((Object) this.effectRenderTex, (Object) null))
            {
              this.effectRenderTex = UIRenderTexture.Get(this.effectTex, link_main_camera: true, layer: 1);
              this.effectRenderTex.Enable();
              break;
            }
            break;
          }
          break;
        case "SE_PLAY":
          try
          {
            int se_id = int.Parse(p0);
            loadingQueue.CacheSE(se_id);
            break;
          }
          catch
          {
            Log.Error(LOG.EXCEPTION, "{0}コマンドのSEIDに整数ではない値が指定されています。", (object) cmd);
            break;
          }
        case "MSG":
          if (!string.IsNullOrEmpty(p2))
          {
            try
            {
              int voice_id = int.Parse(p2);
              loadingQueue.CacheVoice(voice_id);
              break;
            }
            catch
            {
              Log.Error(LOG.EXCEPTION, "{0}コマンドのボイスIDに整数ではない値が指定されています。", (object) cmd);
              break;
            }
          }
          else
            break;
        case "CHR_LOAD":
          int id = -1;
          for (int i = 0; (double) i < 4.0; ++i)
          {
            if (Object.op_Equality((Object) this.charas.Find((Predicate<StoryCharacter>) (o => o.id == i)), (Object) null))
            {
              id = i;
              break;
            }
          }
          if (id != -1)
          {
            UITexture modelUiTexture = this.eventReceiver.GetModelUITexture(id);
            if (Object.op_Inequality((Object) modelUiTexture, (Object) null))
            {
              StoryCharacter storyCharacter = StoryCharacter.Initialize(id, modelUiTexture, p0, p1, p2, 24 + id);
              if (Object.op_Inequality((Object) storyCharacter, (Object) null))
              {
                this.charas.Add(storyCharacter);
                break;
              }
              break;
            }
            break;
          }
          break;
      }
    }
    while (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
      yield return (object) null;
    this.isLoading = false;
  }

  private IEnumerator RunScript()
  {
    Transform camera_t = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    for (int cmd_row = 0; cmd_row < this.scriptCommands.Count; ++cmd_row)
    {
      string cmd = this.scriptCommands[cmd_row].cmd;
      string p0 = this.scriptCommands[cmd_row].p0;
      string p1 = this.scriptCommands[cmd_row].p1;
      string p2 = this.scriptCommands[cmd_row].p2;
      string p3 = this.scriptCommands[cmd_row].p3;
      string msg = this.scriptCommands[cmd_row].msg;
      float time;
      bool forward;
      StoryCharacter chara;
      LoadObject load_obj;
      Vector3 vector3_1;
      switch (cmd)
      {
        case "BG":
          forward = false;
          LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
          if (Object.op_Inequality((Object) this.locationRednerTex, (Object) null))
            this.locationRednerTex.Release();
          else
            forward = true;
          int image_id = int.Parse(p0);
          int sky_id = int.Parse(p1);
          ResourceManager.enableCache = false;
          load_obj = image_id > 0 ? loadingQueue.Load(RESOURCE_CATEGORY.STORY_LOCATION_IMAGE, ResourceName.GetStoryLocationImage(image_id)) : (LoadObject) null;
          LoadObject lo_loc_sky = sky_id > 0 ? loadingQueue.Load(RESOURCE_CATEGORY.STORY_LOCATION_SKY, ResourceName.GetStoryLocationSky(sky_id)) : (LoadObject) null;
          yield return (object) loadingQueue.Wait();
          ResourceManager.enableCache = true;
          this.locationRednerTex = UIRenderTexture.Get(this.locationTex, layer: 0);
          this.locationRednerTex.Disable();
          this.locationRednerTex.orthographicSize = (float) ((double) this.locationTex.height * 0.5 * 0.0099999997764825821);
          this.locationRednerTex.modelTransform.position = new Vector3(0.0f, 0.0f, 10f);
          this.locationRoot = Utility.CreateGameObject("LocationRoot", this.locationRednerTex.modelTransform, this.locationRednerTex.renderLayer);
          this.locationRoot.localPosition = new Vector3(0.0f, 0.0f, 3f);
          this.locationRoot.localScale = new Vector3(0.01f, 0.01f, 1f);
          if (load_obj != null)
          {
            this.locationImageRoot = Utility.CreateGameObject("LocationImageRoot", this.locationRoot, this.locationRednerTex.renderLayer);
            this.locationImage = ResourceUtility.Realizes(load_obj.loadedObject, this.locationImageRoot, this.locationRednerTex.renderLayer);
          }
          if (lo_loc_sky != null)
          {
            this.locationSky = ResourceUtility.Realizes(lo_loc_sky.loadedObject, this.locationRoot, this.locationRednerTex.renderLayer);
            this.locationSky.localPosition = new Vector3(0.0f, 0.0f, 1f);
          }
          this.locationRednerTex.Enable();
          if (forward)
            this.eventReceiver.EndLoadFirstBG();
          load_obj = (LoadObject) null;
          lo_loc_sky = (LoadObject) null;
          break;
        case "BGM_CHANGE":
          MonoBehaviourSingleton<SoundManager>.I.requestBGMID = int.Parse(p0);
          break;
        case "CAM_MOV":
          float result1 = 0.3f;
          if (!string.IsNullOrEmpty(p1))
            float.TryParse(p1, out result1);
          int index1 = int.Parse(p0);
          if (0 <= index1 && this.cameraPositions.Length > index1)
          {
            Vector3Interpolator cameraPosAnim = this.cameraPosAnim;
            double _time = (double) result1;
            Vector3 cameraPosition = this.cameraPositions[index1];
            vector3_1 = new Vector3();
            Vector3 add_value = vector3_1;
            cameraPosAnim.Set((float) _time, cameraPosition, (AnimationCurve) null, add_value, (AnimationCurve) null);
            this.cameraPosAnim.Play();
            break;
          }
          break;
        case "CAM_PAN":
          StoryCharacter chara1 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara1, (Object) null))
          {
            int charaShowCount = this.GetCharaShowCount();
            if (1 == charaShowCount)
            {
              Vector3 vector3_2;
              vector3_2.x = chara1.model.position.x;
              Transform transform = !(p1 == "F") ? Utility.Find(chara1.model, "Neck") : Utility.Find(chara1.model, "Spine01");
              vector3_2.y = !Object.op_Inequality((Object) transform, (Object) null) ? this.initCameraPos.y : transform.position.y;
              vector3_2.z = !(p1 == "N") ? (!(p1 == "F") ? MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNormalZ : MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanFarZ) : MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNearZ;
              Vector3Interpolator cameraPosAnim = this.cameraPosAnim;
              Vector3 end_value = vector3_2;
              vector3_1 = new Vector3();
              Vector3 add_value = vector3_1;
              cameraPosAnim.Set(0.3f, end_value, (AnimationCurve) null, add_value, (AnimationCurve) null);
              this.cameraPosAnim.Play();
              break;
            }
            if (2 == charaShowCount)
            {
              Vector3Interpolator cameraPosAnim = this.cameraPosAnim;
              Vector3 duoCameraPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.duoCameraPos;
              vector3_1 = new Vector3();
              Vector3 add_value = vector3_1;
              cameraPosAnim.Set(0.3f, duoCameraPos, (AnimationCurve) null, add_value, (AnimationCurve) null);
              this.cameraPosAnim.Play();
              break;
            }
            if (3 <= charaShowCount)
            {
              Vector3Interpolator cameraPosAnim = this.cameraPosAnim;
              Vector3 trioCameraPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.trioCameraPos;
              vector3_1 = new Vector3();
              Vector3 add_value = vector3_1;
              cameraPosAnim.Set(0.3f, trioCameraPos, (AnimationCurve) null, add_value, (AnimationCurve) null);
              this.cameraPosAnim.Play();
              break;
            }
            break;
          }
          Vector3 vector3_3;
          vector3_3.x = 0.0f;
          vector3_3.y = this.initCameraPos.y;
          vector3_3.z = 0.0f;
          Vector3Interpolator cameraPosAnim1 = this.cameraPosAnim;
          Vector3 end_value1 = vector3_3;
          vector3_1 = new Vector3();
          Vector3 add_value1 = vector3_1;
          cameraPosAnim1.Set(0.3f, end_value1, (AnimationCurve) null, add_value1, (AnimationCurve) null);
          this.cameraPosAnim.Play();
          break;
        case "CAM_SET":
          int index2 = int.Parse(p0);
          if (0 <= index2 && this.cameraPositions.Length > index2)
          {
            float num1 = float.Parse(p1);
            float num2 = float.Parse(p2);
            float num3 = float.Parse(p3);
            this.cameraPositions[index2] = new Vector3(num1, num2, num3);
            break;
          }
          break;
        case "CAM_SHAKE":
          string str1 = p0;
          float _time1 = 0.0f;
          this.cameraShakeAnim.loopType = Interpolator.LOOP.NONE;
          if (!string.IsNullOrEmpty(p1))
          {
            _time1 = float.Parse(p1);
            if ((double) _time1 == 0.0)
            {
              this.cameraShakeAnim.loopType = Interpolator.LOOP.REPETE;
              _time1 = 1f;
            }
          }
          else
            Log.Error(LOG.EXCEPTION, "{0}コマンドの時間がセットされていません。", (object) cmd);
          Vector3 add_value2;
          // ISSUE: explicit constructor call
          ((Vector3) ref add_value2).\u002Ector(0.05f, 0.1f, 1f);
          switch (str1)
          {
            case "S":
              // ISSUE: explicit constructor call
              ((Vector3) ref add_value2).\u002Ector(0.005f, 0.0125f, 1f);
              break;
            case "M":
              // ISSUE: explicit constructor call
              ((Vector3) ref add_value2).\u002Ector(0.01f, 0.025f, 1f);
              break;
            case "L":
              // ISSUE: explicit constructor call
              ((Vector3) ref add_value2).\u002Ector(0.02f, 0.05f, 1f);
              break;
          }
          this.cameraShakeAnim.Set(_time1, Vector3.zero, Vector3.zero, (AnimationCurve) null, add_value2, (AnimationCurve) null);
          this.cameraShakeAnim.Play();
          break;
        case "CAM_SHAKE_STOP":
          time = string.IsNullOrEmpty(p0) ? 0.0f : float.Parse(p0);
          do
          {
            yield return (object) null;
            if ((double) time > 0.0)
              time -= Time.deltaTime;
          }
          while (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy || MonoBehaviourSingleton<TransitionManager>.I.isTransing || Object.op_Inequality((Object) this.charas.Find((Predicate<StoryCharacter>) (o => o.isMoving)), (Object) null) || this.cameraPosAnim.IsPlaying());
          this.cameraShakeAnim.Stop();
          break;
        case "CHR_ALIAS":
          StoryCharacter chara2 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara2, (Object) null))
          {
            chara2.SetAliasName(p1);
            break;
          }
          break;
        case "CHR_EASE":
          StoryCharacter.EaseDir type = p1 == "L" ? StoryCharacter.EaseDir.LEFT : StoryCharacter.EaseDir.RIGHT;
          forward = !(p2 == "IN");
          chara = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara, (Object) null))
          {
            while (chara.isLoading)
              yield return (object) null;
            chara.PlayTween(type, forward);
          }
          chara = (StoryCharacter) null;
          break;
        case "CHR_FACE":
          StoryCharacter chara3 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara3, (Object) null))
          {
            chara3.RequestFace(p1, p2);
            break;
          }
          break;
        case "CHR_HIDE":
          chara = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara, (Object) null))
          {
            while (chara.isLoading)
              yield return (object) null;
            this.FadeCharacter(false, chara);
            yield return (object) new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
          }
          chara = (StoryCharacter) null;
          break;
        case "CHR_POSE":
          StoryCharacter chara4 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara4, (Object) null))
          {
            chara4.RequestPose(p1);
            break;
          }
          break;
        case "CHR_ROT":
          StoryCharacter chara5 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara5, (Object) null))
          {
            float result2 = 0.5f;
            if (!string.IsNullOrEmpty(p2))
              float.TryParse(p2, out result2);
            if (string.IsNullOrEmpty(p1))
            {
              chara5.RotateDefault(result2);
              break;
            }
            if (p1 == "F")
            {
              chara5.RotateFront(result2);
              break;
            }
            float result3;
            if (float.TryParse(p1, out result3))
            {
              chara5.RotateAngle(result3, result2);
              break;
            }
            break;
          }
          break;
        case "CHR_SCALE":
          StoryCharacter chara6 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara6, (Object) null))
          {
            vector3_1 = new Vector3();
            vector3_1.x = float.Parse(p1);
            vector3_1.y = float.Parse(p1);
            vector3_1.z = float.Parse(p1);
            Vector3 scale = vector3_1;
            chara6.SetModelScale(scale);
            break;
          }
          break;
        case "CHR_SHOW":
          chara = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara, (Object) null))
          {
            while (chara.isLoading)
              yield return (object) null;
            this.FadeCharacter(true, chara);
            yield return (object) new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
          }
          chara = (StoryCharacter) null;
          break;
        case "CHR_STAND":
          StoryCharacter chara7 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara7, (Object) null))
          {
            chara7.SetStandPosition(p1, true);
            break;
          }
          break;
        case "CHR_STAND_POS":
          StoryCharacter chara8 = this.FindChara(p0);
          if (Object.op_Inequality((Object) chara8, (Object) null))
          {
            float result4 = 0.0f;
            float result5 = 0.0f;
            float result6 = 0.3f;
            if (!string.IsNullOrEmpty(p1))
              float.TryParse(p1, out result4);
            if (!string.IsNullOrEmpty(p2))
              float.TryParse(p2, out result5);
            if (!string.IsNullOrEmpty(p3))
              float.TryParse(p3, out result6);
            chara8.SetPosition(result4, result5, result6);
            break;
          }
          break;
        case "EFF_SHOW":
          load_obj = this.effectPrefabs.Get(p0);
          if (load_obj != null)
          {
            while (load_obj.isLoading)
              yield return (object) null;
            Transform transform = ResourceUtility.Realizes(load_obj.loadedObject, this.effectRenderTex.modelTransform, 1);
            Vector3 vector3_4 = Vector3.zero;
            StoryCharacter chara9 = this.FindChara(p1);
            if (Object.op_Inequality((Object) chara9, (Object) null))
              vector3_4 = chara9.model.position;
            vector3_4.y = camera_t.position.y;
            transform.position = vector3_4;
            float result7;
            if (float.TryParse(p2, out result7))
              transform.localScale = new Vector3(result7, result7, result7);
          }
          load_obj = (LoadObject) null;
          break;
        case "EFF_SHOW_POS":
          load_obj = this.effectPrefabs.Get(p0);
          if (load_obj != null)
          {
            while (load_obj.isLoading)
              yield return (object) null;
            Transform transform = ResourceUtility.Realizes(load_obj.loadedObject, this.effectRenderTex.modelTransform, 1);
            float result8 = 0.0f;
            float result9 = 0.0f;
            float.TryParse(p1, out result8);
            if (float.TryParse(p2, out result9))
              result9 += camera_t.position.y;
            transform.position = new Vector3(result8, result9, 3.5f);
            float result10;
            if (float.TryParse(p3, out result10))
              transform.localScale = new Vector3(result10, result10, result10);
          }
          load_obj = (LoadObject) null;
          break;
        case "EFF_STOP":
          Transform transform1 = this.effectRenderTex.modelTransform.Find(p0);
          if (Object.op_Inequality((Object) transform1, (Object) null))
          {
            Object.Destroy((Object) ((Component) transform1).gameObject);
            break;
          }
          break;
        case "FADE_IN":
          this.eventReceiver.FadeIn();
          break;
        case "FADE_IN_TIME":
          float fade_time1 = 1f;
          if (!string.IsNullOrEmpty(p1))
            fade_time1 = float.Parse(p1);
          this.eventReceiver.FadeIn(fade_time1);
          break;
        case "FADE_OUT":
          Color fadeout_color1 = Color.black;
          switch (p0)
          {
            case "BLUE":
              fadeout_color1 = Color.blue;
              break;
            case "WHITE":
              fadeout_color1 = Color.white;
              break;
            case "RED":
              fadeout_color1 = Color.red;
              break;
            case "YELLOW":
              fadeout_color1 = Color.yellow;
              break;
          }
          this.eventReceiver.FadeOut(fadeout_color1);
          break;
        case "FADE_OUT_TIME":
          Color fadeout_color2 = Color.black;
          float fade_time2 = 1f;
          if (!string.IsNullOrEmpty(p1))
            fade_time2 = float.Parse(p1);
          switch (p0)
          {
            case "BLUE":
              fadeout_color2 = Color.blue;
              break;
            case "WHITE":
              fadeout_color2 = Color.white;
              break;
            case "RED":
              fadeout_color2 = Color.red;
              break;
            case "YELLOW":
              fadeout_color2 = Color.yellow;
              break;
          }
          this.eventReceiver.FadeOut(fadeout_color2, fade_time2);
          break;
        case "HIGE_POS":
          string str2 = p0;
          this.tailPos = StoryDirector.POS.NONE;
          if (!string.IsNullOrEmpty(str2))
          {
            switch (str2)
            {
              case "L":
                this.tailPos = StoryDirector.POS.LEFT;
                break;
              case "C":
                this.tailPos = StoryDirector.POS.CENTER;
                break;
              case "R":
                this.tailPos = StoryDirector.POS.RIGHT;
                break;
            }
          }
          else
          {
            Log.Error(LOG.EXCEPTION, "{0}コマンドのパラムに値がセットされていません。", (object) cmd);
            break;
          }
          break;
        case "MSG":
          StoryDirector.MSG_TYPE msg_type = StoryDirector.MSG_TYPE.NORMAL;
          if (p1 == "M" || p1 == "MONOLOGUE")
            msg_type = StoryDirector.MSG_TYPE.MONOLOGUE;
          this.waitMessage = true;
          StoryCharacter chara10 = this.FindChara(p0);
          string replacedText = this.GetReplacedText(msg);
          StoryDirector.LabelOption labelOption = (StoryDirector.LabelOption) null;
          if (!string.IsNullOrEmpty(p3))
          {
            string[] strArray1 = p3.Split(',');
            if (strArray1 != null && strArray1.Length != 0)
            {
              labelOption = new StoryDirector.LabelOption();
              foreach (string str3 in strArray1)
              {
                string upper = p3.ToUpper();
                if (upper.IndexOf("BBCODE") >= 0)
                  labelOption.BBCode = true;
                if (upper.IndexOf("CENTER") >= 0)
                  labelOption.Alignment = NGUIText.Alignment.Center;
                if (upper.IndexOf("RIGHT") >= 0)
                  labelOption.Alignment = NGUIText.Alignment.Right;
                if (upper.IndexOf("LEFT") >= 0)
                  labelOption.Alignment = NGUIText.Alignment.Left;
                if (upper.IndexOf("FONTSIZE") >= 0 && upper.IndexOf('=') >= 0)
                {
                  string[] strArray2 = upper.Split('=');
                  if (strArray2 != null && strArray2.Length >= 2)
                  {
                    string s = strArray2[1].Trim();
                    int num = 20;
                    ref int local = ref num;
                    if (int.TryParse(s, out local))
                      labelOption.FontSize = num;
                  }
                }
              }
            }
          }
          StoryDirector.POS tail_dir = StoryDirector.POS.NONE;
          if (Object.op_Inequality((Object) chara10, (Object) null))
            tail_dir = chara10.dir;
          if (tail_dir != StoryDirector.POS.NONE && this.tailPos != StoryDirector.POS.NONE)
            tail_dir = this.tailPos;
          this.eventReceiver.AddMessage(Object.op_Inequality((Object) chara10, (Object) null) ? chara10.displayName : p0, replacedText, tail_dir, msg_type, labelOption);
          if (!string.IsNullOrEmpty(p2))
          {
            SoundManager.PlayVoice(int.Parse(p2));
            break;
          }
          break;
        case "SE_PLAY":
          SoundManager.PlayOneShotUISE(int.Parse(p0));
          break;
        case "WAIT":
          time = string.IsNullOrEmpty(p0) ? 0.0f : float.Parse(p0);
          do
          {
            yield return (object) null;
            if ((double) time > 0.0)
              time -= Time.deltaTime;
          }
          while (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy || MonoBehaviourSingleton<TransitionManager>.I.isTransing || Object.op_Inequality((Object) this.charas.Find((Predicate<StoryCharacter>) (o => o.isMoving)), (Object) null) || this.cameraPosAnim.IsPlaying());
          break;
      }
      while (this.waitMessage)
        yield return (object) null;
      p1 = (string) null;
      p2 = (string) null;
      p3 = (string) null;
    }
    this.isRunning = false;
    this.FocusChara((StoryCharacter) null);
    this.eventReceiver.EndStory();
  }

  private StoryCharacter FindChara(string chara_name)
  {
    return this.charas.Find((Predicate<StoryCharacter>) (o => o.charaName == chara_name));
  }

  private void FocusChara(StoryCharacter pickup_chara)
  {
    if (Object.op_Equality((Object) pickup_chara, (Object) null))
      return;
    this.charas.ForEach((Action<StoryCharacter>) (o => TweenColor.Begin(((Component) o.uiTex).gameObject, 0.1f, Object.op_Equality((Object) o, (Object) pickup_chara) || Object.op_Equality((Object) pickup_chara, (Object) null) ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f, 1f))));
  }

  private string GetReplacedText(string str)
  {
    str = str.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
    if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
      str = str.Replace("{CLAN_NAME}", MonoBehaviourSingleton<GuildManager>.I.guildData.name);
    return str;
  }

  public void OnNextMessage() => this.waitMessage = false;

  private int GetCharaShowCount()
  {
    int n = 0;
    this.charas.ForEach((Action<StoryCharacter>) (o =>
    {
      if (!o.IsShow())
        return;
      ++n;
    }));
    return n;
  }

  private void LateUpdate()
  {
    if (!this.isRunning)
      return;
    Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    Vector3 vector3_1 = this.cameraPosAnim.Update();
    mainCameraTransform.position = vector3_1;
    if (this.cameraShakeAnim.IsPlaying())
    {
      Vector3 vector3_2 = this.cameraShakeAnim.Update();
      mainCameraTransform.position = Vector3.op_Addition(vector3_1, vector3_2);
      if (Object.op_Inequality((Object) this.locationRoot, (Object) null))
        ((Component) this.locationRoot).transform.localPosition = Vector3.op_Addition(new Vector3(0.0f, 0.0f, 3f), vector3_2);
    }
    else if (Object.op_Inequality((Object) this.locationRoot, (Object) null))
      this.locationRoot.localPosition = new Vector3(0.0f, 0.0f, 3f);
    if (!Object.op_Inequality((Object) this.locationImage, (Object) null))
      return;
    Vector3 localPosition = this.locationImage.localPosition;
    localPosition.x = (float) (-(double) vector3_1.x * 50.0);
    localPosition.y = (float) (((double) this.initCameraPos.y - (double) vector3_1.y) * 50.0);
    this.locationImage.localPosition = localPosition;
    float num = (float) ((double) vector3_1.z * 0.10000000149011612 + 1.0);
    this.locationImageRoot.localScale = new Vector3(num, num, 1f);
  }

  private void FadeCharacter(bool fadein, StoryCharacter chara)
  {
    if (fadein)
      chara.FadeIn();
    else
      chara.FadeOut();
  }

  public void HideBG()
  {
    if (!Object.op_Inequality((Object) null, (Object) this.locationRoot))
      return;
    ((Component) this.locationRoot).gameObject.SetActive(false);
  }

  public interface IStoryEventReceiver
  {
    void FadeIn();

    void FadeOut(Color fadeout_color);

    void AddMessage(
      string name,
      string msg,
      StoryDirector.POS tail_dir,
      StoryDirector.MSG_TYPE msg_type,
      StoryDirector.LabelOption labelOption = null);

    UITexture GetModelUITexture(int id);

    void EndLoadFirstBG();

    void EndStory();

    void FadeIn(float fade_time);

    void FadeOut(Color fadeout_color, float fade_time);
  }

  public enum POS
  {
    NONE,
    LEFT,
    RIGHT,
    CENTER,
  }

  public enum MSG_TYPE
  {
    NORMAL,
    SHOUT,
    WHISPER,
    MONOLOGUE,
  }

  public enum CMD
  {
    BG,
    EFF_SHOW,
    SE_PLAY,
    MSG,
    CHR_LOAD,
    WAIT,
    FADE_IN,
    FADE_OUT,
    BGM_CHANGE,
    CHR_SHOW,
    CHR_HIDE,
    CHR_ROT,
    CHR_OFFSET,
    CHR_SCALE,
    CHR_POSE,
    CHR_FACE,
    CHR_STAND,
    CHR_EASE,
    CHR_ALIAS,
    CAM_PAN,
    CAM_SET,
    CAM_MOV,
    EFF_STOP,
    EFF_SHOW_POS,
    CHR_STAND_POS,
    HIGE_POS,
    FADE_IN_TIME,
    FADE_OUT_TIME,
    CAM_SHAKE,
    CAM_SHAKE_STOP,
  }

  public class LabelOption
  {
    public bool BBCode;
    public NGUIText.Alignment Alignment = NGUIText.Alignment.Left;
    public int FontSize = 20;
  }

  public class StoryScript
  {
    public string cmd = string.Empty;
    public string p0 = string.Empty;
    public string p1 = string.Empty;
    public string p2 = string.Empty;
    public string p3 = string.Empty;
    public string msg = string.Empty;
  }
}
