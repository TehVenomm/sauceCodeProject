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

	private Vector3[] cameraPositions = new Vector3[8];

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
		locationTex = location_tex;
		effectTex = effect_tex;
		eventReceiver = event_receiver;
		for (int i = 0; i < cameraPositions.Length; i++)
		{
			cameraPositions[i] = new Vector3(0f, 0f, 0f);
		}
		StartCoroutine(DoScript(script_id));
	}

	private IEnumerator DoScript(int script_id)
	{
		yield return StartCoroutine(ParseScript(script_id));
		yield return StartCoroutine(LoadScriptResources());
		while (isLoading)
		{
			yield return null;
		}
		yield return StartCoroutine(RunScript());
	}

	private IEnumerator ParseScript(int script_id)
	{
		isLoading = true;
		string storyScript = ResourceName.GetStoryScript(script_id);
		bool loading = true;
		string text = null;
		MonoBehaviourSingleton<DataTableManager>.I.LoadStory(storyScript, delegate(string x)
		{
			text = x;
			loading = false;
		});
		while (loading)
		{
			yield return null;
		}
		isRunning = true;
		CSVReader cSVReader = new CSVReader(text, "cmd,p0,p1,p2,p3,jp");
		while (cSVReader.NextLine())
		{
			StoryScript storyScript2 = new StoryScript();
			cSVReader.Pop(ref storyScript2.cmd);
			if (!string.IsNullOrEmpty(storyScript2.cmd))
			{
				cSVReader.Pop(ref storyScript2.p0);
				cSVReader.Pop(ref storyScript2.p1);
				cSVReader.Pop(ref storyScript2.p2);
				cSVReader.Pop(ref storyScript2.p3);
				cSVReader.Pop(ref storyScript2.msg);
				scriptCommands.Add(storyScript2);
			}
		}
	}

	private IEnumerator LoadScriptResources()
	{
		Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		initCameraPos = mainCameraTransform.position;
		cameraPosAnim.Set(initCameraPos);
		LoadingQueue loadingQueue = new LoadingQueue(this);
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
					LoadObject value = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, p);
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
					loadingQueue.CacheSE(se_id);
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
						loadingQueue.CacheVoice(voice_id);
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
				i = 0;
				while ((float)i < 4f)
				{
					if (charas.Find((StoryCharacter o) => o.id == i) == null)
					{
						num = i;
						break;
					}
					int num2 = ++i;
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
		int cmd_row = 0;
		Vector3 vector2 = default(Vector3);
		Vector3 vector3 = default(Vector3);
		while (cmd_row < scriptCommands.Count)
		{
			string cmd = scriptCommands[cmd_row].cmd;
			string p4 = scriptCommands[cmd_row].p0;
			string p = scriptCommands[cmd_row].p1;
			string p2 = scriptCommands[cmd_row].p2;
			string p3 = scriptCommands[cmd_row].p3;
			string msg = scriptCommands[cmd_row].msg;
			Vector3 vector;
			int i;
			switch (cmd)
			{
			case "WAIT":
			{
				float time2 = string.IsNullOrEmpty(p4) ? 0f : float.Parse(p4);
				while (true)
				{
					yield return null;
					if (time2 > 0f)
					{
						time2 -= Time.deltaTime;
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
				Color fadeout_color2 = Color.black;
				switch (p4)
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
				eventReceiver.FadeOut(fadeout_color2);
				break;
			}
			case "CHR_EASE":
			{
				StoryCharacter.EaseDir type = (!(p == "L")) ? StoryCharacter.EaseDir.RIGHT : StoryCharacter.EaseDir.LEFT;
				bool forward2 = (!(p2 == "IN")) ? true : false;
				StoryCharacter chara2 = FindChara(p4);
				if (chara2 != null)
				{
					while (chara2.isLoading)
					{
						yield return null;
					}
					chara2.PlayTween(type, forward2);
				}
				break;
			}
			case "EFF_SHOW":
			{
				LoadObject load_obj = effectPrefabs.Get(p4);
				if (load_obj != null)
				{
					while (load_obj.isLoading)
					{
						yield return null;
					}
					Transform transform = ResourceUtility.Realizes(load_obj.loadedObject, effectRenderTex.modelTransform, 1);
					Vector3 position = Vector3.zero;
					StoryCharacter storyCharacter3 = FindChara(p);
					if (storyCharacter3 != null)
					{
						position = storyCharacter3.model.position;
					}
					position.y = camera_t.position.y;
					transform.position = position;
					if (float.TryParse(p2, out float result3))
					{
						transform.localScale = new Vector3(result3, result3, result3);
					}
				}
				break;
			}
			case "EFF_SHOW_POS":
			{
				LoadObject load_obj = effectPrefabs.Get(p4);
				if (load_obj != null)
				{
					while (load_obj.isLoading)
					{
						yield return null;
					}
					Transform transform4 = ResourceUtility.Realizes(load_obj.loadedObject, effectRenderTex.modelTransform, 1);
					float result9 = 0f;
					float result10 = 0f;
					float.TryParse(p, out result9);
					if (float.TryParse(p2, out result10))
					{
						result10 += camera_t.position.y;
					}
					transform4.position = new Vector3(result9, result10, 3.5f);
					if (float.TryParse(p3, out float result11))
					{
						transform4.localScale = new Vector3(result11, result11, result11);
					}
				}
				break;
			}
			case "EFF_STOP":
			{
				Transform transform2 = effectRenderTex.modelTransform.Find(p4);
				if (transform2 != null)
				{
					Object.Destroy(transform2.gameObject);
				}
				break;
			}
			case "SE_PLAY":
				SoundManager.PlayOneShotUISE(int.Parse(p4));
				break;
			case "BGM_CHANGE":
			{
				int requestBGMID = int.Parse(p4);
				MonoBehaviourSingleton<SoundManager>.I.requestBGMID = requestBGMID;
				break;
			}
			case "CHR_SHOW":
			{
				StoryCharacter chara2 = FindChara(p4);
				if (chara2 != null)
				{
					while (chara2.isLoading)
					{
						yield return null;
					}
					FadeCharacter(fadein: true, chara2);
					yield return new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
				}
				break;
			}
			case "CHR_HIDE":
			{
				StoryCharacter chara2 = FindChara(p4);
				if (chara2 != null)
				{
					while (chara2.isLoading)
					{
						yield return null;
					}
					FadeCharacter(fadein: false, chara2);
					yield return new WaitForSeconds(MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime);
				}
				break;
			}
			case "CHR_ROT":
			{
				StoryCharacter storyCharacter5 = FindChara(p4);
				if (storyCharacter5 != null)
				{
					float result4 = 0.5f;
					if (!string.IsNullOrEmpty(p2))
					{
						float.TryParse(p2, out result4);
					}
					float result5;
					if (string.IsNullOrEmpty(p))
					{
						storyCharacter5.RotateDefault(result4);
					}
					else if (p == "F")
					{
						storyCharacter5.RotateFront(result4);
					}
					else if (float.TryParse(p, out result5))
					{
						storyCharacter5.RotateAngle(result5, result4);
					}
				}
				break;
			}
			case "CHR_POSE":
			{
				StoryCharacter storyCharacter8 = FindChara(p4);
				if (storyCharacter8 != null)
				{
					storyCharacter8.RequestPose(p);
				}
				break;
			}
			case "CHR_STAND":
			{
				StoryCharacter storyCharacter = FindChara(p4);
				if (storyCharacter != null)
				{
					storyCharacter.SetStandPosition(p, doesSetImmidiate: true);
				}
				break;
			}
			case "CHR_STAND_POS":
			{
				StoryCharacter storyCharacter6 = FindChara(p4);
				if (storyCharacter6 != null)
				{
					float result6 = 0f;
					float result7 = 0f;
					float result8 = 0.3f;
					if (!string.IsNullOrEmpty(p))
					{
						float.TryParse(p, out result6);
					}
					if (!string.IsNullOrEmpty(p2))
					{
						float.TryParse(p2, out result7);
					}
					if (!string.IsNullOrEmpty(p3))
					{
						float.TryParse(p3, out result8);
					}
					storyCharacter6.SetPosition(result6, result7, result8);
				}
				break;
			}
			case "CHR_SCALE":
			{
				StoryCharacter storyCharacter4 = FindChara(p4);
				if (storyCharacter4 != null)
				{
					vector = default(Vector3);
					vector.x = float.Parse(p);
					vector.y = float.Parse(p);
					vector.z = float.Parse(p);
					Vector3 modelScale = vector;
					storyCharacter4.SetModelScale(modelScale);
				}
				break;
			}
			case "CHR_FACE":
			{
				StoryCharacter storyCharacter7 = FindChara(p4);
				if (storyCharacter7 != null)
				{
					storyCharacter7.RequestFace(p, p2);
				}
				break;
			}
			case "CAM_SET":
			{
				int num5 = int.Parse(p4);
				if (0 <= num5 && cameraPositions.Length > num5)
				{
					float x = float.Parse(p);
					float y = float.Parse(p2);
					float z = float.Parse(p3);
					cameraPositions[num5] = new Vector3(x, y, z);
				}
				break;
			}
			case "CAM_MOV":
			{
				float result = 0.3f;
				if (!string.IsNullOrEmpty(p))
				{
					float.TryParse(p, out result);
				}
				int num = int.Parse(p4);
				if (0 <= num && cameraPositions.Length > num)
				{
					Vector3Interpolator vector3Interpolator = cameraPosAnim;
					float time3 = result;
					Vector3 end_value = cameraPositions[num];
					vector = default(Vector3);
					vector3Interpolator.Set(time3, end_value, null, vector);
					cameraPosAnim.Play();
				}
				break;
			}
			case "CAM_PAN":
			{
				StoryCharacter storyCharacter10 = FindChara(p4);
				if (storyCharacter10 != null)
				{
					int charaShowCount = GetCharaShowCount();
					if (1 == charaShowCount)
					{
						vector2.x = storyCharacter10.model.position.x;
						Transform transform3 = (!(p == "F")) ? Utility.Find(storyCharacter10.model, "Neck") : Utility.Find(storyCharacter10.model, "Spine01");
						if (transform3 != null)
						{
							vector2.y = transform3.position.y;
						}
						else
						{
							vector2.y = initCameraPos.y;
						}
						if (p == "N")
						{
							vector2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNearZ;
						}
						else if (p == "F")
						{
							vector2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanFarZ;
						}
						else
						{
							vector2.z = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraPanNormalZ;
						}
						Vector3Interpolator vector3Interpolator2 = cameraPosAnim;
						Vector3 end_value2 = vector2;
						vector = default(Vector3);
						vector3Interpolator2.Set(0.3f, end_value2, null, vector);
						cameraPosAnim.Play();
					}
					else if (2 == charaShowCount)
					{
						Vector3Interpolator vector3Interpolator3 = cameraPosAnim;
						Vector3 duoCameraPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.duoCameraPos;
						vector = default(Vector3);
						vector3Interpolator3.Set(0.3f, duoCameraPos, null, vector);
						cameraPosAnim.Play();
					}
					else if (3 <= charaShowCount)
					{
						Vector3Interpolator vector3Interpolator4 = cameraPosAnim;
						Vector3 trioCameraPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.trioCameraPos;
						vector = default(Vector3);
						vector3Interpolator4.Set(0.3f, trioCameraPos, null, vector);
						cameraPosAnim.Play();
					}
				}
				else
				{
					vector3.x = 0f;
					vector3.y = initCameraPos.y;
					vector3.z = 0f;
					Vector3Interpolator vector3Interpolator5 = cameraPosAnim;
					Vector3 end_value3 = vector3;
					vector = default(Vector3);
					vector3Interpolator5.Set(0.3f, end_value3, null, vector);
					cameraPosAnim.Play();
				}
				break;
			}
			case "CHR_ALIAS":
			{
				StoryCharacter storyCharacter9 = FindChara(p4);
				if (storyCharacter9 != null)
				{
					storyCharacter9.SetAliasName(p);
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
				StoryCharacter storyCharacter2 = FindChara(p4);
				msg = GetReplacedText(msg);
				LabelOption labelOption = null;
				if (!string.IsNullOrEmpty(p3))
				{
					string[] array = p3.Split(',');
					if (array != null && array.Length != 0)
					{
						labelOption = new LabelOption();
						string[] array2 = array;
						for (i = 0; i < array2.Length; i++)
						{
							_ = array2[i];
							string text2 = p3.ToUpper();
							if (text2.IndexOf("BBCODE") >= 0)
							{
								labelOption.BBCode = true;
							}
							if (text2.IndexOf("CENTER") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Center;
							}
							if (text2.IndexOf("RIGHT") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Right;
							}
							if (text2.IndexOf("LEFT") >= 0)
							{
								labelOption.Alignment = NGUIText.Alignment.Left;
							}
							if (text2.IndexOf("FONTSIZE") < 0 || text2.IndexOf('=') < 0)
							{
								continue;
							}
							string[] array3 = text2.Split('=');
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
				if (storyCharacter2 != null)
				{
					pOS = storyCharacter2.dir;
				}
				if (pOS != 0 && tailPos != 0)
				{
					pOS = tailPos;
				}
				eventReceiver.AddMessage((storyCharacter2 != null) ? storyCharacter2.displayName : p4, msg, pOS, msg_type, labelOption);
				if (!string.IsNullOrEmpty(p2))
				{
					SoundManager.PlayVoice(int.Parse(p2));
				}
				break;
			}
			case "BG":
			{
				bool forward2 = false;
				LoadingQueue loadingQueue = new LoadingQueue(this);
				if (locationRednerTex != null)
				{
					locationRednerTex.Release();
				}
				else
				{
					forward2 = true;
				}
				int num3 = int.Parse(p4);
				int num4 = int.Parse(p);
				ResourceManager.enableCache = false;
				LoadObject load_obj = (num3 > 0) ? loadingQueue.Load(RESOURCE_CATEGORY.STORY_LOCATION_IMAGE, ResourceName.GetStoryLocationImage(num3)) : null;
				LoadObject lo_loc_sky = (num4 > 0) ? loadingQueue.Load(RESOURCE_CATEGORY.STORY_LOCATION_SKY, ResourceName.GetStoryLocationSky(num4)) : null;
				yield return loadingQueue.Wait();
				ResourceManager.enableCache = true;
				locationRednerTex = UIRenderTexture.Get(locationTex, -1f, link_main_camera: false, 0);
				locationRednerTex.Disable();
				locationRednerTex.orthographicSize = (float)locationTex.height * 0.5f * 0.01f;
				locationRednerTex.modelTransform.position = new Vector3(0f, 0f, 10f);
				locationRoot = Utility.CreateGameObject("LocationRoot", locationRednerTex.modelTransform, locationRednerTex.renderLayer);
				locationRoot.localPosition = new Vector3(0f, 0f, 3f);
				locationRoot.localScale = new Vector3(0.01f, 0.01f, 1f);
				if (load_obj != null)
				{
					locationImageRoot = Utility.CreateGameObject("LocationImageRoot", locationRoot, locationRednerTex.renderLayer);
					locationImage = ResourceUtility.Realizes(load_obj.loadedObject, locationImageRoot, locationRednerTex.renderLayer);
				}
				if (lo_loc_sky != null)
				{
					locationSky = ResourceUtility.Realizes(lo_loc_sky.loadedObject, locationRoot, locationRednerTex.renderLayer);
					locationSky.localPosition = new Vector3(0f, 0f, 1f);
				}
				locationRednerTex.Enable();
				if (forward2)
				{
					eventReceiver.EndLoadFirstBG();
				}
				break;
			}
			case "HIGE_POS":
			{
				string text = p4;
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
			case "FADE_IN_TIME":
			{
				float fade_time = 1f;
				if (!string.IsNullOrEmpty(p))
				{
					fade_time = float.Parse(p);
				}
				eventReceiver.FadeIn(fade_time);
				break;
			}
			case "FADE_OUT_TIME":
			{
				Color fadeout_color = Color.black;
				float fade_time2 = 1f;
				if (!string.IsNullOrEmpty(p))
				{
					fade_time2 = float.Parse(p);
				}
				if (!(p4 == "BLUE"))
				{
					if (!(p4 == "WHITE"))
					{
						if (!(p4 == "RED"))
						{
							if (p4 == "YELLOW")
							{
								fadeout_color = Color.yellow;
							}
						}
						else
						{
							fadeout_color = Color.red;
						}
					}
					else
					{
						fadeout_color = Color.white;
					}
				}
				else
				{
					fadeout_color = Color.blue;
				}
				eventReceiver.FadeOut(fadeout_color, fade_time2);
				break;
			}
			case "CAM_SHAKE":
			{
				string text3 = p4;
				float num2 = 0f;
				cameraShakeAnim.loopType = Interpolator.LOOP.NONE;
				if (!string.IsNullOrEmpty(p))
				{
					num2 = float.Parse(p);
					if (num2 == 0f)
					{
						cameraShakeAnim.loopType = Interpolator.LOOP.REPETE;
						num2 = 1f;
					}
				}
				else
				{
					Log.Error(LOG.EXCEPTION, "{0}コマンドの時間がセットされていません。", cmd);
				}
				Vector3 add_value = new Vector3(0.05f, 0.1f, 1f);
				if (text3.Equals("S"))
				{
					add_value = new Vector3(0.005f, 0.0125f, 1f);
				}
				else if (text3.Equals("M"))
				{
					add_value = new Vector3(0.01f, 0.025f, 1f);
				}
				else if (text3.Equals("L"))
				{
					add_value = new Vector3(0.02f, 0.05f, 1f);
				}
				cameraShakeAnim.Set(num2, Vector3.zero, Vector3.zero, null, add_value);
				cameraShakeAnim.Play();
				break;
			}
			case "CAM_SHAKE_STOP":
			{
				float time2 = string.IsNullOrEmpty(p4) ? 0f : float.Parse(p4);
				while (true)
				{
					yield return null;
					if (time2 > 0f)
					{
						time2 -= Time.deltaTime;
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
			while (waitMessage)
			{
				yield return null;
			}
			i = cmd_row + 1;
			cmd_row = i;
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
				TweenColor.Begin(o.uiTex.gameObject, 0.1f, (o == pickup_chara || pickup_chara == null) ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f, 1f));
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
				int num = ++i;
			}
		});
		return i;
	}

	private void LateUpdate()
	{
		if (!isRunning)
		{
			return;
		}
		Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		Vector3 a = mainCameraTransform.position = cameraPosAnim.Update();
		if (cameraShakeAnim.IsPlaying())
		{
			Vector3 b = cameraShakeAnim.Update();
			mainCameraTransform.position = a + b;
			if (locationRoot != null)
			{
				locationRoot.transform.localPosition = new Vector3(0f, 0f, 3f) + b;
			}
		}
		else if (locationRoot != null)
		{
			locationRoot.localPosition = new Vector3(0f, 0f, 3f);
		}
		if (locationImage != null)
		{
			Vector3 localPosition = locationImage.localPosition;
			localPosition.x = (0f - a.x) * 50f;
			localPosition.y = (initCameraPos.y - a.y) * 50f;
			locationImage.localPosition = localPosition;
			float num = a.z * 0.1f + 1f;
			locationImageRoot.localScale = new Vector3(num, num, 1f);
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
			locationRoot.gameObject.SetActive(value: false);
		}
	}
}
