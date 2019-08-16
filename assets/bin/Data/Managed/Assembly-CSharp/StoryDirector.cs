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

		void FadeIn(float fade_time);

		void FadeOut(Color fadeout_color, float fade_time);
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
		CHR_STAND_POS,
		HIGE_POS,
		FADE_IN_TIME,
		FADE_OUT_TIME,
		CAM_SHAKE,
		CAM_SHAKE_STOP
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

	private List<StoryScript> scriptCommands = new List<StoryScript>();

	private const float LOCATION_SCALE = 0.01f;

	private const float LOCATION_SCALE_INV = 100f;

	private const float LOCATION_MOVE_SCALE = 0.02f;

	private const float LOCATION_MOVE_SCALE_INV = 50f;

	private const float CHARA_MAX = 4f;

	private const float TRANSITION_TIME_CAM_PAN = 0.3f;

	private const float TRANSITION_TIME_FOCUS_CHARA = 0.1f;

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

	public ShakeInterpolator cameraShakeAnim = new ShakeInterpolator();

	private List<StoryCharacter> charas = new List<StoryCharacter>();

	private bool waitMessage;

	public POS tailPos;

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
		yield return this.StartCoroutine(ParseScript(script_id));
		yield return this.StartCoroutine(LoadScriptResources());
		while (isLoading)
		{
			yield return null;
		}
		yield return this.StartCoroutine(RunScript());
	}

	private IEnumerator ParseScript(int script_id)
	{
		isLoading = true;
		string scriptName = ResourceName.GetStoryScript(script_id);
		bool loading = true;
		string text = null;
		MonoBehaviourSingleton<DataTableManager>.I.LoadStory(scriptName, delegate(string x)
		{
			text = x;
			loading = false;
		});
		while (loading)
		{
			yield return null;
		}
		isRunning = true;
		CSVReader csv = new CSVReader(text, "cmd,p0,p1,p2,p3,jp");
		while (csv.NextLine())
		{
			StoryScript storyScript = new StoryScript();
			csv.Pop(ref storyScript.cmd);
			if (!string.IsNullOrEmpty(storyScript.cmd))
			{
				csv.Pop(ref storyScript.p0);
				csv.Pop(ref storyScript.p1);
				csv.Pop(ref storyScript.p2);
				csv.Pop(ref storyScript.p3);
				csv.Pop(ref storyScript.msg);
				scriptCommands.Add(storyScript);
			}
		}
	}

	private IEnumerator LoadScriptResources()
	{
		Transform camera_t = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		initCameraPos = camera_t.get_position();
		cameraPosAnim.Set(initCameraPos);
		LoadingQueue load_queue = new LoadingQueue(this);
		int i;
		for (int j = 0; j < scriptCommands.Count; j++)
		{
			string cmd = scriptCommands[j].cmd;
			string p = scriptCommands[j].p0;
			string p2 = scriptCommands[j].p1;
			string p3 = scriptCommands[j].p2;
			switch (cmd)
			{
			case "EFF_SHOW":
			case "EFF_SHOW_POS":
				if (effectPrefabs.Get(p) == null)
				{
					LoadObject value = load_queue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, p);
					effectPrefabs.Add(p, value);
					if (effectRenderTex == null)
					{
						effectRenderTex = UIRenderTexture.Get(effectTex, -1f, link_main_camera: true, 1);
						effectRenderTex.Enable();
					}
				}
				break;
			case "SE_PLAY":
				try
				{
					int se_id = int.Parse(p);
					load_queue.CacheSE(se_id);
				}
				catch
				{
					Log.Error(LOG.EXCEPTION, "{0}コマンドのSEIDに整数ではない値が指定されています。", cmd);
				}
				break;
			case "MSG":
				if (!string.IsNullOrEmpty(p3))
				{
					try
					{
						int voice_id = int.Parse(p3);
						load_queue.CacheVoice(voice_id);
					}
					catch
					{
						Log.Error(LOG.EXCEPTION, "{0}コマンドのボイスIDに整数ではない値が指定されています。", cmd);
					}
				}
				break;
			case "CHR_LOAD":
			{
				int num = -1;
				for (i = 0; (float)i < 4f; i++)
				{
					if (charas.Find((StoryCharacter o) => o.id == i) == null)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					break;
				}
				UITexture modelUITexture = eventReceiver.GetModelUITexture(num);
				if (modelUITexture != null)
				{
					StoryCharacter storyCharacter = StoryCharacter.Initialize(num, modelUITexture, p, p2, p3, 24 + num);
					if (storyCharacter != null)
					{
						charas.Add(storyCharacter);
					}
				}
				break;
			}
			}
		}
		while (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
		{
			yield return null;
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
			if (cmd != null)
			{
				if (_003C_003Ef__switch_0024map0 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(28);
					dictionary.Add("WAIT", 0);
					dictionary.Add("FADE_IN", 1);
					dictionary.Add("FADE_OUT", 2);
					dictionary.Add("CHR_EASE", 3);
					dictionary.Add("EFF_SHOW", 4);
					dictionary.Add("EFF_SHOW_POS", 5);
					dictionary.Add("EFF_STOP", 6);
					dictionary.Add("SE_PLAY", 7);
					dictionary.Add("BGM_CHANGE", 8);
					dictionary.Add("CHR_SHOW", 9);
					dictionary.Add("CHR_HIDE", 10);
					dictionary.Add("CHR_ROT", 11);
					dictionary.Add("CHR_POSE", 12);
					dictionary.Add("CHR_STAND", 13);
					dictionary.Add("CHR_STAND_POS", 14);
					dictionary.Add("CHR_SCALE", 15);
					dictionary.Add("CHR_FACE", 16);
					dictionary.Add("CAM_SET", 17);
					dictionary.Add("CAM_MOV", 18);
					dictionary.Add("CAM_PAN", 19);
					dictionary.Add("CHR_ALIAS", 20);
					dictionary.Add("MSG", 21);
					dictionary.Add("BG", 22);
					dictionary.Add("HIGE_POS", 23);
					dictionary.Add("FADE_IN_TIME", 24);
					dictionary.Add("FADE_OUT_TIME", 25);
					dictionary.Add("CAM_SHAKE", 26);
					dictionary.Add("CAM_SHAKE_STOP", 27);
					_003C_003Ef__switch_0024map0 = dictionary;
				}
				if (_003C_003Ef__switch_0024map0.TryGetValue(cmd, out int value))
				{
					Vector3 val2;
					switch (value)
					{
					case 0:
					{
						float time = (!string.IsNullOrEmpty(p0)) ? float.Parse(p0) : 0f;
						while (true)
						{
							yield return null;
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
					case 1:
						eventReceiver.FadeIn();
						break;
					case 2:
					{
						Color fadeout_color = Color.get_black();
						switch (p0)
						{
						case "BLUE":
							fadeout_color = Color.get_blue();
							break;
						case "WHITE":
							fadeout_color = Color.get_white();
							break;
						case "RED":
							fadeout_color = Color.get_red();
							break;
						case "YELLOW":
							fadeout_color = Color.get_yellow();
							break;
						}
						eventReceiver.FadeOut(fadeout_color);
						break;
					}
					case 3:
					{
						StoryCharacter.EaseDir type = (!(p == "L")) ? StoryCharacter.EaseDir.RIGHT : StoryCharacter.EaseDir.LEFT;
						bool forward = (!(p2 == "IN")) ? true : false;
						StoryCharacter chara = FindChara(p0);
						if (chara != null)
						{
							while (chara.isLoading)
							{
								yield return null;
							}
							chara.PlayTween(type, forward);
						}
						break;
					}
					case 4:
					{
						LoadObject load_obj = effectPrefabs.Get(p0);
						if (load_obj != null)
						{
							while (load_obj.isLoading)
							{
								yield return null;
							}
							Transform effect = ResourceUtility.Realizes(load_obj.loadedObject, effectRenderTex.modelTransform, 1);
							Vector3 pos = Vector3.get_zero();
							StoryCharacter chara2 = FindChara(p);
							if (chara2 != null)
							{
								pos = chara2.model.get_position();
							}
							Vector3 position = camera_t.get_position();
							pos.y = position.y;
							effect.set_position(pos);
							if (float.TryParse(p2, out float scale))
							{
								effect.set_localScale(new Vector3(scale, scale, scale));
							}
						}
						break;
					}
					case 5:
					{
						LoadObject load_obj2 = effectPrefabs.Get(p0);
						if (load_obj2 != null)
						{
							while (load_obj2.isLoading)
							{
								yield return null;
							}
							Transform effect2 = ResourceUtility.Realizes(load_obj2.loadedObject, effectRenderTex.modelTransform, 1);
							float x = 0f;
							float y = 0f;
							float.TryParse(p, out x);
							if (float.TryParse(p2, out y))
							{
								float num7 = y;
								Vector3 position2 = camera_t.get_position();
								y = num7 + position2.y;
							}
							effect2.set_position(new Vector3(x, y, 3.5f));
							if (float.TryParse(p3, out float scale2))
							{
								effect2.set_localScale(new Vector3(scale2, scale2, scale2));
							}
						}
						break;
					}
					case 6:
					{
						Transform val = effectRenderTex.modelTransform.Find(p0);
						if (val != null)
						{
							Object.Destroy(val.get_gameObject());
						}
						break;
					}
					case 7:
					{
						int se_id = int.Parse(p0);
						SoundManager.PlayOneShotUISE(se_id);
						break;
					}
					case 8:
					{
						int requestBGMID = int.Parse(p0);
						MonoBehaviourSingleton<SoundManager>.I.requestBGMID = requestBGMID;
						break;
					}
					case 9:
					{
						StoryCharacter chara3 = FindChara(p0);
						if (chara3 != null)
						{
							while (chara3.isLoading)
							{
								yield return null;
							}
							FadeCharacter(fadein: true, chara3);
							yield return (object)new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
						}
						break;
					}
					case 10:
					{
						StoryCharacter chara4 = FindChara(p0);
						if (chara4 != null)
						{
							while (chara4.isLoading)
							{
								yield return null;
							}
							FadeCharacter(fadein: false, chara4);
							yield return (object)new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
						}
						break;
					}
					case 11:
					{
						StoryCharacter storyCharacter6 = FindChara(p0);
						if (storyCharacter6 != null)
						{
							float result3 = 0.5f;
							if (!string.IsNullOrEmpty(p2))
							{
								float.TryParse(p2, out result3);
							}
							float result4;
							if (string.IsNullOrEmpty(p))
							{
								storyCharacter6.RotateDefault(result3);
							}
							else if (p == "F")
							{
								storyCharacter6.RotateFront(result3);
							}
							else if (float.TryParse(p, out result4))
							{
								storyCharacter6.RotateAngle(result4, result3);
							}
						}
						break;
					}
					case 12:
					{
						StoryCharacter storyCharacter4 = FindChara(p0);
						if (storyCharacter4 != null)
						{
							storyCharacter4.RequestPose(p);
						}
						break;
					}
					case 13:
					{
						StoryCharacter storyCharacter2 = FindChara(p0);
						if (storyCharacter2 != null)
						{
							storyCharacter2.SetStandPosition(p, doesSetImmidiate: true);
						}
						break;
					}
					case 14:
					{
						StoryCharacter storyCharacter9 = FindChara(p0);
						if (storyCharacter9 != null)
						{
							float result5 = 0f;
							float result6 = 0f;
							float result7 = 0.3f;
							if (!string.IsNullOrEmpty(p))
							{
								float.TryParse(p, out result5);
							}
							if (!string.IsNullOrEmpty(p2))
							{
								float.TryParse(p2, out result6);
							}
							if (!string.IsNullOrEmpty(p3))
							{
								float.TryParse(p3, out result7);
							}
							storyCharacter9.SetPosition(result5, result6, result7);
						}
						break;
					}
					case 15:
					{
						StoryCharacter storyCharacter5 = FindChara(p0);
						if (storyCharacter5 != null)
						{
							val2 = default(Vector3);
							val2.x = float.Parse(p);
							val2.y = float.Parse(p);
							val2.z = float.Parse(p);
							Vector3 modelScale = val2;
							storyCharacter5.SetModelScale(modelScale);
						}
						break;
					}
					case 16:
					{
						StoryCharacter storyCharacter3 = FindChara(p0);
						if (storyCharacter3 != null)
						{
							storyCharacter3.RequestFace(p, p2);
						}
						break;
					}
					case 17:
					{
						int num3 = int.Parse(p0);
						if (0 <= num3 && cameraPositions.Length > num3)
						{
							float num4 = float.Parse(p);
							float num5 = float.Parse(p2);
							float num6 = float.Parse(p3);
							cameraPositions[num3] = new Vector3(num4, num5, num6);
						}
						break;
					}
					case 18:
					{
						float result = 0.3f;
						if (!string.IsNullOrEmpty(p))
						{
							float.TryParse(p, out result);
						}
						int num2 = int.Parse(p0);
						if (0 <= num2 && cameraPositions.Length > num2)
						{
							Vector3Interpolator vector3Interpolator = cameraPosAnim;
							float time3 = result;
							Vector3 end_value = cameraPositions[num2];
							val2 = default(Vector3);
							vector3Interpolator.Set(time3, end_value, null, val2);
							cameraPosAnim.Play();
						}
						break;
					}
					case 19:
					{
						StoryCharacter storyCharacter8 = FindChara(p0);
						if (storyCharacter8 != null)
						{
							int charaShowCount = GetCharaShowCount();
							switch (charaShowCount)
							{
							case 1:
							{
								val2 = storyCharacter8.model.get_position();
								Vector3 end_value2 = default(Vector3);
								end_value2.x = val2.x;
								Transform val3 = (!(p == "F")) ? Utility.Find(storyCharacter8.model, "Neck") : Utility.Find(storyCharacter8.model, "Spine01");
								if (val3 != null)
								{
									Vector3 position3 = val3.get_position();
									end_value2.y = position3.y;
								}
								else
								{
									end_value2.y = initCameraPos.y;
								}
								if (p == "N")
								{
									end_value2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNearZ;
								}
								else if (p == "F")
								{
									end_value2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanFarZ;
								}
								else
								{
									end_value2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNormalZ;
								}
								cameraPosAnim.Set(0.3f, end_value2);
								cameraPosAnim.Play();
								break;
							}
							case 2:
								cameraPosAnim.Set(0.3f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.duoCameraPos);
								cameraPosAnim.Play();
								break;
							default:
								if (3 <= charaShowCount)
								{
									cameraPosAnim.Set(0.3f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.trioCameraPos);
									cameraPosAnim.Play();
								}
								break;
							}
						}
						else
						{
							Vector3 end_value3 = default(Vector3);
							end_value3.x = 0f;
							end_value3.y = initCameraPos.y;
							end_value3.z = 0f;
							cameraPosAnim.Set(0.3f, end_value3);
							cameraPosAnim.Play();
						}
						break;
					}
					case 20:
					{
						StoryCharacter storyCharacter7 = FindChara(p0);
						if (storyCharacter7 != null)
						{
							storyCharacter7.SetAliasName(p);
						}
						break;
					}
					case 21:
					{
						MSG_TYPE msg_type = MSG_TYPE.NORMAL;
						if (p == "M" || p == "MONOLOGUE")
						{
							msg_type = MSG_TYPE.MONOLOGUE;
						}
						waitMessage = true;
						StoryCharacter storyCharacter = FindChara(p0);
						msg = GetReplacedText(msg);
						LabelOption labelOption = null;
						if (!string.IsNullOrEmpty(p3))
						{
							string[] array = p3.Split(',');
							if (array != null && array.Length > 0)
							{
								labelOption = new LabelOption();
								string[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									string text4 = array2[i];
									string text3 = p3.ToUpper();
									if (text3.IndexOf("BBCODE") >= 0)
									{
										labelOption.BBCode = true;
									}
									if (text3.IndexOf("CENTER") >= 0)
									{
										labelOption.Alignment = NGUIText.Alignment.Center;
									}
									if (text3.IndexOf("RIGHT") >= 0)
									{
										labelOption.Alignment = NGUIText.Alignment.Right;
									}
									if (text3.IndexOf("LEFT") >= 0)
									{
										labelOption.Alignment = NGUIText.Alignment.Left;
									}
									if (text3.IndexOf("FONTSIZE") < 0 || text3.IndexOf('=') < 0)
									{
										continue;
									}
									string[] array3 = text3.Split('=');
									if (array3 != null && array3.Length >= 2)
									{
										string s = array3[1].Trim();
										int result2 = 20;
										if (int.TryParse(s, out result2))
										{
											labelOption.FontSize = result2;
										}
									}
								}
							}
						}
						POS pOS = POS.NONE;
						if (storyCharacter != null)
						{
							pOS = storyCharacter.dir;
						}
						if (pOS != 0 && tailPos != 0)
						{
							pOS = tailPos;
						}
						eventReceiver.AddMessage((!(storyCharacter != null)) ? p0 : storyCharacter.displayName, msg, pOS, msg_type, labelOption);
						if (!string.IsNullOrEmpty(p2))
						{
							int voice_id = int.Parse(p2);
							SoundManager.PlayVoice(voice_id);
						}
						break;
					}
					case 22:
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
						LoadObject lo_loc_image = (loc_image_id <= 0) ? null : load_queue.Load(RESOURCE_CATEGORY.STORY_LOCATION_IMAGE, ResourceName.GetStoryLocationImage(loc_image_id));
						LoadObject lo_loc_sky = (loc_sky_id <= 0) ? null : load_queue.Load(RESOURCE_CATEGORY.STORY_LOCATION_SKY, ResourceName.GetStoryLocationSky(loc_sky_id));
						yield return load_queue.Wait();
						ResourceManager.enableCache = true;
						locationRednerTex = UIRenderTexture.Get(locationTex, -1f, link_main_camera: false, 0);
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
						locationRednerTex.Enable();
						if (isFirstLoad)
						{
							eventReceiver.EndLoadFirstBG();
						}
						break;
					}
					case 23:
					{
						string text = p0;
						tailPos = POS.NONE;
						if (!string.IsNullOrEmpty(text))
						{
							if (text.Equals("L"))
							{
								tailPos = POS.LEFT;
							}
							else if (text.Equals("C"))
							{
								tailPos = POS.CENTER;
							}
							else if (text.Equals("R"))
							{
								tailPos = POS.RIGHT;
							}
						}
						else
						{
							Log.Error(LOG.EXCEPTION, "{0}コマンドのパラムに値がセットされていません。", cmd);
						}
						break;
					}
					case 24:
					{
						float fade_time = 1f;
						if (!string.IsNullOrEmpty(p))
						{
							fade_time = float.Parse(p);
						}
						eventReceiver.FadeIn(fade_time);
						break;
					}
					case 25:
					{
						Color fadeout_color2 = Color.get_black();
						float fade_time2 = 1f;
						if (!string.IsNullOrEmpty(p))
						{
							fade_time2 = float.Parse(p);
						}
						if (p0 != null)
						{
							if (!(p0 == "BLUE"))
							{
								if (!(p0 == "WHITE"))
								{
									if (!(p0 == "RED"))
									{
										if (p0 == "YELLOW")
										{
											fadeout_color2 = Color.get_yellow();
										}
									}
									else
									{
										fadeout_color2 = Color.get_red();
									}
								}
								else
								{
									fadeout_color2 = Color.get_white();
								}
							}
							else
							{
								fadeout_color2 = Color.get_blue();
							}
						}
						eventReceiver.FadeOut(fadeout_color2, fade_time2);
						break;
					}
					case 26:
					{
						string text2 = p0;
						float num = 0f;
						cameraShakeAnim.loopType = Interpolator.LOOP.NONE;
						if (!string.IsNullOrEmpty(p))
						{
							num = float.Parse(p);
							if (num == 0f)
							{
								cameraShakeAnim.loopType = Interpolator.LOOP.REPETE;
								num = 1f;
							}
						}
						else
						{
							Log.Error(LOG.EXCEPTION, "{0}コマンドの時間がセットされていません。", cmd);
						}
						Vector3 add_value = default(Vector3);
						add_value._002Ector(0.05f, 0.1f, 1f);
						if (text2.Equals("S"))
						{
							add_value._002Ector(0.005f, 0.0125f, 1f);
						}
						else if (text2.Equals("M"))
						{
							add_value._002Ector(0.01f, 0.025f, 1f);
						}
						else if (text2.Equals("L"))
						{
							add_value._002Ector(0.02f, 0.05f, 1f);
						}
						cameraShakeAnim.Set(num, Vector3.get_zero(), Vector3.get_zero(), null, add_value);
						cameraShakeAnim.Play();
						break;
					}
					case 27:
					{
						float time2 = (!string.IsNullOrEmpty(p0)) ? float.Parse(p0) : 0f;
						while (true)
						{
							yield return null;
							if (time2 > 0f)
							{
								time2 -= Time.get_deltaTime();
							}
							else if (!MonoBehaviourSingleton<ResourceManager>.I.isLoading && !InstantiateManager.isBusy && !MonoBehaviourSingleton<TransitionManager>.I.isTransing && !(charas.Find((StoryCharacter o) => o.isMoving) != null) && !cameraPosAnim.IsPlaying())
							{
								break;
							}
						}
						cameraShakeAnim.Stop();
						break;
					}
					}
				}
			}
			while (waitMessage)
			{
				yield return null;
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
				//IL_0046: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		if (!isRunning)
		{
			return;
		}
		Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		Vector3 val = cameraPosAnim.Update();
		mainCameraTransform.set_position(val);
		if (cameraShakeAnim.IsPlaying())
		{
			Vector3 val2 = cameraShakeAnim.Update();
			mainCameraTransform.set_position(val + val2);
			if (locationRoot != null)
			{
				locationRoot.get_transform().set_localPosition(new Vector3(0f, 0f, 3f) + val2);
			}
		}
		else if (locationRoot != null)
		{
			locationRoot.set_localPosition(new Vector3(0f, 0f, 3f));
		}
		if (locationImage != null)
		{
			Vector3 localPosition = locationImage.get_localPosition();
			localPosition.x = (0f - val.x) * 50f;
			localPosition.y = (initCameraPos.y - val.y) * 50f;
			locationImage.set_localPosition(localPosition);
			float num = val.z * 0.1f + 1f;
			locationImageRoot.set_localScale(new Vector3(num, num, 1f));
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
		if (null != locationRoot)
		{
			locationRoot.get_gameObject().SetActive(false);
		}
	}
}
