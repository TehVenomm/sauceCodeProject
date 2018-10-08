using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDirector : MonoBehaviourSingleton<StoryDirector>
{
	public interface IStoryEventReceiver
	{
		void FadeIn();

		void FadeOut(Color fadeout_color);

		void AddMessage(string name, string msg, POS tail_dir, MSG_TYPE msg_type, LabelOption labelOption = null);

		UITexture GetModelUITexture(int id);

		void EndLoadFirstBG();

		void EndStory();
	}

	public enum POS
	{
		NONE,
		LEFT,
		RIGHT,
		CENTER
	}

	public enum MSG_TYPE
	{
		NORMAL,
		SHOUT,
		WHISPER,
		MONOLOGUE
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
		CHR_STAND_POS
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

	private const float LOCATION_SCALE = 0.01f;

	private const float LOCATION_SCALE_INV = 100f;

	private const float LOCATION_MOVE_SCALE = 0.02f;

	private const float LOCATION_MOVE_SCALE_INV = 50f;

	private const float CHARA_MAX = 4f;

	private const float TRANSITION_TIME_CAM_PAN = 0.3f;

	private const float TRANSITION_TIME_FOCUS_CHARA = 0.1f;

	private List<StoryScript> scriptCommands = new List<StoryScript>();

	public static readonly int SPEED_TYPEWRITER = 40;

	private IStoryEventReceiver eventReceiver;

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

	private Vector3[] cameraPositions = (Vector3[])new Vector3[8];

	private List<StoryCharacter> charas = new List<StoryCharacter>();

	private bool waitMessage;

	public bool isLoading
	{
		get;
		private set;
	}

	public bool isRunning
	{
		get;
		private set;
	}

	public void StartScript(int script_id, UITexture location_tex, UITexture effect_tex, IStoryEventReceiver event_receiver)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		locationTex = location_tex;
		effectTex = effect_tex;
		eventReceiver = event_receiver;
		for (int i = 0; i < cameraPositions.Length; i++)
		{
			cameraPositions[i] = new Vector3(0f, 0f, 0f);
		}
		this.StartCoroutine(DoScript(script_id));
	}

	private IEnumerator DoScript(int script_id)
	{
		yield return (object)this.StartCoroutine(ParseScript(script_id));
		yield return (object)this.StartCoroutine(LoadScriptResources());
		while (isLoading)
		{
			yield return (object)null;
		}
		yield return (object)this.StartCoroutine(RunScript());
	}

	private IEnumerator ParseScript(int script_id)
	{
		isLoading = true;
		string scriptName = ResourceName.GetStoryScript(script_id);
		bool loading = true;
		string text = null;
		MonoBehaviourSingleton<DataTableManager>.I.LoadStory(scriptName, delegate(string x)
		{
			((_003CParseScript_003Ec__IteratorDF)/*Error near IL_0057: stateMachine*/)._003Ctext_003E__2 = x;
			((_003CParseScript_003Ec__IteratorDF)/*Error near IL_0057: stateMachine*/)._003Cloading_003E__1 = false;
		});
		while (loading)
		{
			yield return (object)null;
		}
		isRunning = true;
		CSVReader csv = new CSVReader(text, "cmd,p0,p1,p2,p3,jp", false);
		while (csv.NextLine())
		{
			StoryScript scr = new StoryScript();
			csv.Pop(ref scr.cmd);
			if (!string.IsNullOrEmpty(scr.cmd))
			{
				csv.Pop(ref scr.p0);
				csv.Pop(ref scr.p1);
				csv.Pop(ref scr.p2);
				csv.Pop(ref scr.p3);
				csv.Pop(ref scr.msg);
				scriptCommands.Add(scr);
			}
		}
	}

	private IEnumerator LoadScriptResources()
	{
		Transform camera_t = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		initCameraPos = camera_t.get_position();
		cameraPosAnim.Set(initCameraPos);
		LoadingQueue load_queue = new LoadingQueue(this);
		for (int cmd_row = 0; cmd_row < scriptCommands.Count; cmd_row++)
		{
			string cmd = scriptCommands[cmd_row].cmd;
			string p4 = scriptCommands[cmd_row].p0;
			string p3 = scriptCommands[cmd_row].p1;
			string p2 = scriptCommands[cmd_row].p2;
			switch (cmd)
			{
			case "EFF_SHOW":
			case "EFF_SHOW_POS":
				if (effectPrefabs.Get(p4) == null)
				{
					LoadObject load_obj = load_queue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, p4, false);
					effectPrefabs.Add(p4, load_obj);
					if (effectRenderTex == null)
					{
						effectRenderTex = UIRenderTexture.Get(effectTex, -1f, true, 1);
						effectRenderTex.Enable(0.25f);
					}
				}
				break;
			case "SE_PLAY":
				try
				{
					int se_id = int.Parse(p4);
					load_queue.CacheSE(se_id, null);
				}
				catch
				{
					Log.Error(LOG.EXCEPTION, "{0}コマンドのSEIDに整数ではない値が指定されています。", cmd);
				}
				break;
			case "MSG":
				if (!string.IsNullOrEmpty(p2))
				{
					try
					{
						int voice_id = int.Parse(p2);
						load_queue.CacheVoice(voice_id, null);
					}
					catch
					{
						Log.Error(LOG.EXCEPTION, "{0}コマンドのボイスIDに整数ではない値が指定されています。", cmd);
					}
				}
				break;
			case "CHR_LOAD":
			{
				int id = -1;
				for (int i = 0; (float)i < 4f; i++)
				{
					if (charas.Find((StoryCharacter o) => o.id == ((_003CLoadScriptResources_003Ec__IteratorE0)/*Error near IL_02fd: stateMachine*/)._003Ci_003E__11) == null)
					{
						id = i;
						break;
					}
				}
				if (id != -1)
				{
					UITexture ui_tex = eventReceiver.GetModelUITexture(id);
					if (ui_tex != null)
					{
						StoryCharacter chara = StoryCharacter.Initialize(id, ui_tex, p4, p3, p2, 24 + id);
						if (chara != null)
						{
							charas.Add(chara);
						}
					}
				}
				break;
			}
			}
		}
		while (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
		{
			yield return (object)null;
		}
		isLoading = false;
	}

	private IEnumerator RunScript()
	{
		Transform camera_t = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		for (int cmd_row = 0; cmd_row < scriptCommands.Count; cmd_row++)
		{
			string cmd = scriptCommands[cmd_row].cmd;
			string p0 = scriptCommands[cmd_row].p0;
			string p = scriptCommands[cmd_row].p1;
			string p2 = scriptCommands[cmd_row].p2;
			string p3 = scriptCommands[cmd_row].p3;
			string msg = scriptCommands[cmd_row].msg;
			switch (cmd)
			{
			case "WAIT":
			{
				float time = (!string.IsNullOrEmpty(p0)) ? float.Parse(p0) : 0f;
				while (true)
				{
					yield return (object)null;
					if (time > 0f)
					{
						time -= Time.get_deltaTime();
					}
					else if (!MonoBehaviourSingleton<ResourceManager>.I.isLoading && !InstantiateManager.isBusy && !MonoBehaviourSingleton<TransitionManager>.I.isTransing && !(charas.Find((StoryCharacter o) => o.isMoving) != null) && !cameraPosAnim.IsPlaying())
					{
						break;
					}
				}
				break;
			}
			case "FADE_IN":
				eventReceiver.FadeIn();
				break;
			case "FADE_OUT":
			{
				Color c = Color.get_black();
				switch (p0)
				{
				case "BLUE":
					c = Color.get_blue();
					break;
				case "WHITE":
					c = Color.get_white();
					break;
				case "RED":
					c = Color.get_red();
					break;
				case "YELLOW":
					c = Color.get_yellow();
					break;
				}
				eventReceiver.FadeOut(c);
				break;
			}
			case "CHR_EASE":
			{
				StoryCharacter.EaseDir type = (!(p == "L")) ? StoryCharacter.EaseDir.RIGHT : StoryCharacter.EaseDir.LEFT;
				bool forward = (!(p2 == "IN")) ? true : false;
				StoryCharacter chara = FindChara(p0);
				if (chara != null)
				{
					while (chara.isLoading)
					{
						yield return (object)null;
					}
					chara.PlayTween(type, forward, null);
				}
				break;
			}
			case "EFF_SHOW":
			{
				LoadObject load_obj = effectPrefabs.Get(p0);
				if (load_obj != null)
				{
					while (load_obj.isLoading)
					{
						yield return (object)null;
					}
					Transform effect = ResourceUtility.Realizes(load_obj.loadedObject, effectRenderTex.modelTransform, 1);
					Vector3 pos = Vector3.get_zero();
					StoryCharacter chara2 = FindChara(p);
					if (chara2 != null)
					{
						pos = chara2.model.get_position();
					}
					Vector3 position3 = camera_t.get_position();
					pos.y = position3.y;
					effect.set_position(pos);
					if (float.TryParse(p2, out float scale))
					{
						effect.set_localScale(new Vector3(scale, scale, scale));
					}
				}
				break;
			}
			case "EFF_SHOW_POS":
			{
				LoadObject load_obj2 = effectPrefabs.Get(p0);
				if (load_obj2 != null)
				{
					while (load_obj2.isLoading)
					{
						yield return (object)null;
					}
					Transform effect2 = ResourceUtility.Realizes(load_obj2.loadedObject, effectRenderTex.modelTransform, 1);
					float x = 0f;
					float y = 0f;
					float.TryParse(p, out x);
					if (float.TryParse(p2, out y))
					{
						float num = y;
						Vector3 position4 = camera_t.get_position();
						y = num + position4.y;
					}
					effect2.set_position(new Vector3(x, y, 3.5f));
					if (float.TryParse(p3, out float scale2))
					{
						effect2.set_localScale(new Vector3(scale2, scale2, scale2));
					}
				}
				break;
			}
			case "EFF_STOP":
			{
				Transform target = effectRenderTex.modelTransform.FindChild(p0);
				if (target != null)
				{
					Object.Destroy(target.get_gameObject());
				}
				break;
			}
			case "SE_PLAY":
			{
				int se_id = int.Parse(p0);
				SoundManager.PlayOneShotUISE(se_id);
				break;
			}
			case "BGM_CHANGE":
			{
				int bgm_id = int.Parse(p0);
				MonoBehaviourSingleton<SoundManager>.I.requestBGMID = bgm_id;
				break;
			}
			case "CHR_SHOW":
			{
				StoryCharacter chara4 = FindChara(p0);
				if (chara4 != null)
				{
					while (chara4.isLoading)
					{
						yield return (object)null;
					}
					FadeCharacter(true, chara4);
					yield return (object)new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
				}
				break;
			}
			case "CHR_HIDE":
			{
				StoryCharacter chara5 = FindChara(p0);
				if (chara5 != null)
				{
					while (chara5.isLoading)
					{
						yield return (object)null;
					}
					FadeCharacter(false, chara5);
					yield return (object)new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
				}
				break;
			}
			case "CHR_ROT":
			{
				StoryCharacter chara6 = FindChara(p0);
				if (chara6 != null)
				{
					float rotationtime = 0.5f;
					if (!string.IsNullOrEmpty(p2))
					{
						float.TryParse(p2, out rotationtime);
					}
					float angle;
					if (string.IsNullOrEmpty(p))
					{
						chara6.RotateDefault(rotationtime);
					}
					else if (p == "F")
					{
						chara6.RotateFront(rotationtime);
					}
					else if (float.TryParse(p, out angle))
					{
						chara6.RotateAngle(angle, rotationtime);
					}
				}
				break;
			}
			case "CHR_POSE":
			{
				StoryCharacter chara7 = FindChara(p0);
				if (chara7 != null)
				{
					chara7.RequestPose(p);
				}
				break;
			}
			case "CHR_STAND":
			{
				StoryCharacter chara8 = FindChara(p0);
				if (chara8 != null)
				{
					chara8.SetStandPosition(p, true);
				}
				break;
			}
			case "CHR_STAND_POS":
			{
				StoryCharacter chara9 = FindChara(p0);
				if (chara9 != null)
				{
					float x2 = 0f;
					float y2 = 0f;
					float time2 = 0.3f;
					if (!string.IsNullOrEmpty(p))
					{
						float.TryParse(p, out x2);
					}
					if (!string.IsNullOrEmpty(p2))
					{
						float.TryParse(p2, out y2);
					}
					if (!string.IsNullOrEmpty(p3))
					{
						float.TryParse(p3, out time2);
					}
					chara9.SetPosition(x2, y2, time2);
				}
				break;
			}
			case "CHR_SCALE":
			{
				StoryCharacter chara10 = FindChara(p0);
				if (chara10 != null)
				{
					Vector3 scale3 = new Vector3
					{
						x = float.Parse(p),
						y = float.Parse(p),
						z = float.Parse(p)
					};
					chara10.SetModelScale(scale3);
				}
				break;
			}
			case "CHR_FACE":
			{
				StoryCharacter chara11 = FindChara(p0);
				if (chara11 != null)
				{
					chara11.RequestFace(p, p2);
				}
				break;
			}
			case "CAM_SET":
			{
				int camIndex = int.Parse(p0);
				if (0 <= camIndex && cameraPositions.Length > camIndex)
				{
					float x3 = float.Parse(p);
					float y3 = float.Parse(p2);
					float z = float.Parse(p3);
					cameraPositions[camIndex] = new Vector3(x3, y3, z);
				}
				break;
			}
			case "CAM_MOV":
			{
				float moveTime = 0.3f;
				if (!string.IsNullOrEmpty(p))
				{
					float.TryParse(p, out moveTime);
				}
				int camIndex2 = int.Parse(p0);
				if (0 <= camIndex2 && cameraPositions.Length > camIndex2)
				{
					((InterpolatorBase<Vector3>)cameraPosAnim).Set(moveTime, cameraPositions[camIndex2], null, default(Vector3), null);
					cameraPosAnim.Play();
				}
				break;
			}
			case "CAM_PAN":
			{
				StoryCharacter chara13 = FindChara(p0);
				if (chara13 != null)
				{
					int charaShowCount = GetCharaShowCount();
					switch (charaShowCount)
					{
					case 1:
					{
						Vector3 position = chara13.model.get_position();
						Vector3 pos3;
						pos3.x = position.x;
						Transform chara_neck = (!(p == "F")) ? Utility.Find(chara13.model, "Neck") : Utility.Find(chara13.model, "Spine01");
						if (chara_neck != null)
						{
							Vector3 position2 = chara_neck.get_position();
							pos3.y = position2.y;
						}
						else
						{
							pos3.y = initCameraPos.y;
						}
						if (p == "N")
						{
							pos3.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNearZ;
						}
						else if (p == "F")
						{
							pos3.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanFarZ;
						}
						else
						{
							pos3.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNormalZ;
						}
						((InterpolatorBase<Vector3>)cameraPosAnim).Set(0.3f, pos3, null, default(Vector3), null);
						cameraPosAnim.Play();
						break;
					}
					case 2:
						((InterpolatorBase<Vector3>)cameraPosAnim).Set(0.3f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.duoCameraPos, null, default(Vector3), null);
						cameraPosAnim.Play();
						break;
					default:
						if (3 <= charaShowCount)
						{
							((InterpolatorBase<Vector3>)cameraPosAnim).Set(0.3f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.trioCameraPos, null, default(Vector3), null);
							cameraPosAnim.Play();
						}
						break;
					}
				}
				else
				{
					Vector3 pos2;
					pos2.x = 0f;
					pos2.y = initCameraPos.y;
					pos2.z = 0f;
					((InterpolatorBase<Vector3>)cameraPosAnim).Set(0.3f, pos2, null, default(Vector3), null);
					cameraPosAnim.Play();
				}
				break;
			}
			case "CHR_ALIAS":
			{
				StoryCharacter chara12 = FindChara(p0);
				if (chara12 != null)
				{
					chara12.SetAliasName(p);
				}
				break;
			}
			case "MSG":
			{
				MSG_TYPE msg_type = MSG_TYPE.NORMAL;
				if (p == "M" || p == "MONOLOGUE")
				{
					msg_type = MSG_TYPE.MONOLOGUE;
				}
				waitMessage = true;
				StoryCharacter chara3 = FindChara(p0);
				msg = GetReplacedText(msg);
				LabelOption labelOption = null;
				if (!string.IsNullOrEmpty(p3))
				{
					string[] opts = p3.Split(',');
					if (opts != null && opts.Length > 0)
					{
						labelOption = new LabelOption();
						string[] array = opts;
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							string optString = p3.ToUpper();
							if (optString.IndexOf("BBCODE") >= 0)
							{
								labelOption.BBCode = true;
							}
							if (optString.IndexOf("CENTER") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Center;
							}
							if (optString.IndexOf("RIGHT") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Right;
							}
							if (optString.IndexOf("LEFT") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Left;
							}
							if (optString.IndexOf("FONTSIZE") >= 0 && optString.IndexOf('=') >= 0)
							{
								string[] keyvalue = optString.Split('=');
								if (keyvalue != null && keyvalue.Length >= 2)
								{
									string valueString = keyvalue[1].Trim();
									int fontSize = 20;
									if (int.TryParse(valueString, out fontSize))
									{
										labelOption.FontSize = fontSize;
									}
								}
							}
						}
					}
				}
				eventReceiver.AddMessage((!(chara3 != null)) ? p0 : chara3.displayName, msg, (chara3 != null) ? chara3.dir : POS.NONE, msg_type, labelOption);
				if (!string.IsNullOrEmpty(p2))
				{
					int voice_id = int.Parse(p2);
					SoundManager.PlayVoice(voice_id, 1f, 0u, null, null);
				}
				break;
			}
			case "BG":
			{
				bool isFirstLoad = false;
				LoadingQueue load_queue = new LoadingQueue(this);
				if (locationRednerTex != null)
				{
					locationRednerTex.Release();
				}
				else
				{
					isFirstLoad = true;
				}
				int loc_image_id = int.Parse(p0);
				int loc_sky_id = int.Parse(p);
				ResourceManager.enableCache = false;
				LoadObject lo_loc_image = (loc_image_id <= 0) ? null : load_queue.Load(RESOURCE_CATEGORY.STORY_LOCATION_IMAGE, ResourceName.GetStoryLocationImage(loc_image_id), false);
				LoadObject lo_loc_sky = (loc_sky_id <= 0) ? null : load_queue.Load(RESOURCE_CATEGORY.STORY_LOCATION_SKY, ResourceName.GetStoryLocationSky(loc_sky_id), false);
				yield return (object)load_queue.Wait();
				ResourceManager.enableCache = true;
				locationRednerTex = UIRenderTexture.Get(locationTex, -1f, false, 0);
				locationRednerTex.Disable();
				locationRednerTex.orthographicSize = (float)locationTex.height * 0.5f * 0.01f;
				locationRednerTex.modelTransform.set_position(new Vector3(0f, 0f, 10f));
				locationRoot = Utility.CreateGameObject("LocationRoot", locationRednerTex.modelTransform, locationRednerTex.renderLayer);
				locationRoot.set_localPosition(new Vector3(0f, 0f, 3f));
				locationRoot.set_localScale(new Vector3(0.01f, 0.01f, 1f));
				if (lo_loc_image != null)
				{
					locationImageRoot = Utility.CreateGameObject("LocationImageRoot", locationRoot, locationRednerTex.renderLayer);
					locationImage = ResourceUtility.Realizes(lo_loc_image.loadedObject, locationImageRoot, locationRednerTex.renderLayer);
				}
				if (lo_loc_sky != null)
				{
					locationSky = ResourceUtility.Realizes(lo_loc_sky.loadedObject, locationRoot, locationRednerTex.renderLayer);
					locationSky.set_localPosition(new Vector3(0f, 0f, 1f));
				}
				locationRednerTex.Enable(0.25f);
				if (isFirstLoad)
				{
					eventReceiver.EndLoadFirstBG();
				}
				break;
			}
			}
			while (waitMessage)
			{
				yield return (object)null;
			}
		}
		isRunning = false;
		FocusChara(null);
		eventReceiver.EndStory();
	}

	private StoryCharacter FindChara(string chara_name)
	{
		return charas.Find((StoryCharacter o) => o.charaName == chara_name);
	}

	private void FocusChara(StoryCharacter pickup_chara)
	{
		if (!(pickup_chara == null))
		{
			charas.ForEach(delegate(StoryCharacter o)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0046: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Unknown result type (might be due to invalid IL or missing references)
				//IL_0069: Expected O, but got Unknown
				TweenColor.Begin(o.uiTex.get_gameObject(), 0.1f, (!(o == pickup_chara) && !(pickup_chara == null)) ? new Color(0.5f, 0.5f, 0.5f, 1f) : new Color(1f, 1f, 1f, 1f));
			});
		}
	}

	private string GetReplacedText(string str)
	{
		str = str.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
		{
			str = str.Replace("{CLAN_NAME}", MonoBehaviourSingleton<GuildManager>.I.guildData.name);
		}
		return str;
	}

	public void OnNextMessage()
	{
		waitMessage = false;
	}

	private int GetCharaShowCount()
	{
		int i = 0;
		charas.ForEach(delegate(StoryCharacter o)
		{
			if (o.IsShow())
			{
				i++;
			}
		});
		return i;
	}

	private void LateUpdate()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (isRunning)
		{
			Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			Vector3 position = cameraPosAnim.Update();
			mainCameraTransform.set_position(position);
			if (locationImage != null)
			{
				Vector3 localPosition = locationImage.get_localPosition();
				localPosition.x = (0f - position.x) * 50f;
				localPosition.y = (initCameraPos.y - position.y) * 50f;
				locationImage.set_localPosition(localPosition);
				float num = position.z * 0.1f + 1f;
				locationImageRoot.set_localScale(new Vector3(num, num, 1f));
			}
		}
	}

	private void FadeCharacter(bool fadein, StoryCharacter chara)
	{
		if (fadein)
		{
			chara.FadeIn();
		}
		else
		{
			chara.FadeOut();
		}
	}

	public void HideBG()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (null != locationRoot)
		{
			locationRoot.get_gameObject().SetActive(false);
		}
	}
}
