using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTutorialManager : MonoBehaviour
{
	public enum CHAT_VOICE
	{
		CH00 = 17,
		CH01 = 200016,
		CH02 = 10018
	}

	public class EnemyHolder
	{
		public bool isCalledOndeadHandler;

		public Enemy enemy;

		public void CheckAndCallOndead(Action<Enemy> onDead)
		{
			if (enemy.actionID == Character.ACTION_ID.DEAD && !isCalledOndeadHandler)
			{
				isCalledOndeadHandler = true;
				onDead?.Invoke(enemy);
			}
		}
	}

	public class State
	{
		protected InGameTutorialManager tutorialManager;

		private SelfController _selfController;

		private Character _character;

		protected SelfController selfController
		{
			get
			{
				if ((UnityEngine.Object)_selfController == (UnityEngine.Object)null)
				{
					_selfController = (MonoBehaviourSingleton<StageObjectManager>.I.self.controller as SelfController);
				}
				return _selfController;
			}
		}

		protected Character character
		{
			get
			{
				if ((UnityEngine.Object)_character == (UnityEngine.Object)null)
				{
					_character = selfController.GetComponent<Character>();
				}
				return _character;
			}
		}

		public State(InGameTutorialManager owner)
		{
			tutorialManager = owner;
		}

		public virtual void Init()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Final()
		{
		}
	}

	public class TutorialMove : State
	{
		private enum Phase
		{
			NONE,
			WAIT_FOR_DISP_GREETING,
			GREETING_TO_THE_WORLD,
			WAIT_AUCO_CONTROL_START,
			AUTO_DRAGGING,
			DISP_PLAYER_CONTROL_DIALOG,
			PLAYER_MOVE_TRAINING,
			FINISH_AND_WAIT_DIALOG,
			SHOW_DIALOG_MOVE,
			SHOW_DIALOG_ROLLING_WAIT,
			SHOW_DIALOG_ROLLING,
			GG_TUTORIAL_MOVE,
			GG_DONE_TUTORIAL_MOVE
		}

		private PuniConManager puniconManager;

		private InputManager.TouchInfo touchInfo = new InputManager.TouchInfo();

		private Transform targetAreaObject;

		private UISprite finger;

		private float timer;

		private readonly float TO_END_POINT_DURATION = 0.5f;

		private readonly float WAIT_AUTO_ROTATIN_START = 2f;

		private readonly float TOTAL_AUTO_CONTROL_TIME = 4f;

		private Phase currentPhase;

		private readonly Vector3 TARGET_AREA_POSITION = new Vector3(0f, 0f, -3f);

		private readonly float WAIT_DRAG_DURATION = 1f;

		private readonly float HELP_TEXTURE_DISPLAY_TIME = 3.5f;

		private readonly float TARGET_AREA_RANGE = 1.2f;

		private readonly float MOVE_TRAINING_TIME = 1.5f;

		private readonly float ROLLING_TRAINING_WAIT_TIME = 1f;

		public TutorialMove(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public override void Init()
		{
			Player player = base.character as Player;
			player.SetDiableAction(Character.ACTION_ID.MOVE, false);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, true);
			player.SetDiableAction(Character.ACTION_ID.MAX, true);
			player.SetDiableAction((Character.ACTION_ID)32, true);
			player.SetDiableAction((Character.ACTION_ID)18, true);
			puniconManager = MonoBehaviourSingleton<PuniConManager>.I;
			targetAreaObject = ResourceUtility.Realizes(tutorialManager.targetAreaPrefab, -1);
			targetAreaObject.position = TARGET_AREA_POSITION;
			targetAreaObject.gameObject.SetActive(false);
			currentPhase = Phase.WAIT_FOR_DISP_GREETING;
			tutorialManager.Update();
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_end, "Tutorial");
		}

		public override void Update()
		{
			switch (currentPhase)
			{
			case Phase.WAIT_FOR_DISP_GREETING:
				WaitForDispGreeting();
				break;
			case Phase.GREETING_TO_THE_WORLD:
				GGGreetingToTheWorld();
				break;
			case Phase.WAIT_AUCO_CONTROL_START:
				WaitAutoControlStart();
				break;
			case Phase.AUTO_DRAGGING:
				AutoDragging();
				break;
			case Phase.DISP_PLAYER_CONTROL_DIALOG:
				DispPlayerControlDialog();
				break;
			case Phase.PLAYER_MOVE_TRAINING:
				PlayerMoveTraining();
				break;
			case Phase.FINISH_AND_WAIT_DIALOG:
				FinishAndWaitDialog();
				break;
			case Phase.SHOW_DIALOG_MOVE:
				ShowDialogMove();
				break;
			case Phase.SHOW_DIALOG_ROLLING_WAIT:
				ShowDialogRollingWait();
				break;
			case Phase.SHOW_DIALOG_ROLLING:
				ShowDialogRolling();
				break;
			case Phase.GG_TUTORIAL_MOVE:
				GGTutorialMove();
				break;
			case Phase.GG_DONE_TUTORIAL_MOVE:
				GGTutorialMoveDone();
				break;
			}
		}

		private void WaitForDispGreeting()
		{
			timer += Time.deltaTime;
			if (timer >= 1f)
			{
				timer = 0f;
				tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0001", 0, "Tutorial_Move_Text_0002");
				tutorialManager.dialog.OpenThreeLineLabel();
				currentPhase = Phase.GREETING_TO_THE_WORLD;
			}
		}

		private void GreetingToTheWorld()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG <= timer)
			{
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					timer = 0f;
					tutorialManager.helper.moveHelper.ShowHelpText();
					UITweenCtrl component = tutorialManager.helper.fingerMove.GetComponent<UITweenCtrl>();
					if ((UnityEngine.Object)null != (UnityEngine.Object)component)
					{
						component.gameObject.SetActive(true);
						component.Play(true, null);
					}
					tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2.5f, delegate
					{
						tutorialManager.helper.moveHelper.HideHelpText(delegate
						{
							tutorialManager.helper.fingerMove.gameObject.SetActive(false);
							tutorialManager.StartCoroutine(tutorialManager.WaitForTime(0.5f, delegate
							{
								tutorialManager.helper.moveHelper.ShowHelpPicture();
								tutorialManager.StartCoroutine(tutorialManager.WaitForTime(DURATION_DISP_DIALOG, delegate
								{
									tutorialManager.helper.moveHelper.HideHelpPicture(delegate
									{
										currentPhase = Phase.SHOW_DIALOG_MOVE;
									});
								}));
							}));
						});
					}));
				});
			}
		}

		private void GGGreetingToTheWorld()
		{
			timer += Time.deltaTime;
			if (3.5f <= timer)
			{
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					timer = 0f;
					currentPhase = Phase.GG_TUTORIAL_MOVE;
					tutorialManager.dialog.HideThreeLineLabel();
				});
			}
		}

		private void GGTutorialMove()
		{
			if (tutorialManager.TutorialMoveTime >= 2.5f)
			{
				tutorialManager.helper.moveHelper.HideHelpText(null);
				UITweenCtrl component = tutorialManager.helper.fingerMove.GetComponent<UITweenCtrl>();
				if ((UnityEngine.Object)null != (UnityEngine.Object)component)
				{
					component.gameObject.SetActive(false);
				}
				tutorialManager.helper.commonHelper.ShowGoodJob();
				currentPhase = Phase.GG_DONE_TUTORIAL_MOVE;
				timer = 0f;
			}
			else
			{
				timer += Time.deltaTime;
				if (timer >= 1f)
				{
					currentPhase = Phase.NONE;
					tutorialManager.helper.moveHelper.ShowHelpText();
					UITweenCtrl ctrl = tutorialManager.helper.fingerMove.GetComponent<UITweenCtrl>();
					if ((UnityEngine.Object)null != (UnityEngine.Object)ctrl)
					{
						ctrl.gameObject.SetActive(true);
						ctrl.Reset();
						ctrl.Play(true, delegate
						{
							tutorialManager.StartCoroutine(tutorialManager.WaitForTime(1f, delegate
							{
								tutorialManager.helper.moveHelper.HideHelpText(delegate
								{
									ctrl.gameObject.SetActive(false);
									currentPhase = Phase.GG_TUTORIAL_MOVE;
									timer = 0f;
								});
							}));
						});
					}
				}
			}
		}

		private void GGTutorialMoveDone()
		{
			timer += Time.deltaTime;
			if (1f < timer)
			{
				tutorialManager.helper.commonHelper.HideGoodJob();
				tutorialManager.Change(new TutorialAttack(tutorialManager));
			}
		}

		private void WaitAutoControlStart()
		{
			timer += Time.deltaTime;
			if (WAIT_DRAG_DURATION < timer)
			{
				timer = 0f;
				touchInfo.id = 1;
				touchInfo.beginPosition = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 4));
				touchInfo.position = touchInfo.beginPosition;
				puniconManager.OnTouchOn(touchInfo);
				finger = tutorialManager.helper.commonHelper.ShowFinger(0.3f);
				currentPhase = Phase.AUTO_DRAGGING;
			}
			puniconManager.OnDrag(touchInfo);
		}

		private void AutoDragging()
		{
			timer += Time.deltaTime;
			Vector2 v = Vector2.Lerp(b: new Vector2(-100f * ((float)Screen.width / 480f), 100f * ((float)Screen.height / 640f)), a: Vector2.zero, t: timer / TO_END_POINT_DURATION);
			if (timer >= WAIT_AUTO_ROTATIN_START)
			{
				float degrees = Mathf.Lerp(0f, -90f, timer - WAIT_AUTO_ROTATIN_START);
				Rotate(ref v, degrees);
			}
			touchInfo.position = touchInfo.beginPosition + v;
			finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3(touchInfo.position.x, touchInfo.position.y, MonoBehaviourSingleton<AppMain>.I.mainCamera.nearClipPlane));
			Vector2 vector = touchInfo.position - touchInfo.beginPosition;
			touchInfo.axis = vector.normalized;
			puniconManager.OnDrag(touchInfo);
			PlayerMove();
			if (TOTAL_AUTO_CONTROL_TIME <= timer)
			{
				timer = 0f;
				base.character.ActIdle(false, -1f);
				puniconManager.OnTouchOff(touchInfo);
				tutorialManager.helper.commonHelper.HideFinger(null);
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.moveHelper.HideHelpText(delegate
				{
					targetAreaObject.gameObject.SetActive(true);
					tutorialManager.helper.moveHelper.ShowHelpPicture();
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
				});
				currentPhase = Phase.DISP_PLAYER_CONTROL_DIALOG;
			}
		}

		private void DispPlayerControlDialog()
		{
			timer += Time.deltaTime;
			if (HELP_TEXTURE_DISPLAY_TIME < timer)
			{
				tutorialManager.helper.moveHelper.HideHelpPicture(delegate
				{
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0004", 0, "Tutorial_Move_Text_0005");
					base.selfController.enabled = true;
				});
				currentPhase = Phase.PLAYER_MOVE_TRAINING;
			}
		}

		private void PlayerMoveTraining()
		{
			float sqrMagnitude = (base.character._transform.position - TARGET_AREA_POSITION).sqrMagnitude;
			if (sqrMagnitude < TARGET_AREA_RANGE * TARGET_AREA_RANGE)
			{
				if ((UnityEngine.Object)targetAreaObject != (UnityEngine.Object)null)
				{
					EffectManager.ReleaseEffect(targetAreaObject.gameObject, true, false);
					targetAreaObject = null;
				}
				currentPhase = Phase.NONE;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				tutorialManager.dialog.Close(1, delegate
				{
					tutorialManager.helper.commonHelper.ShowComplete();
					currentPhase = Phase.FINISH_AND_WAIT_DIALOG;
					timer = 0f;
				});
			}
		}

		private void FinishAndWaitDialog()
		{
			timer += Time.deltaTime;
			if (1f < timer)
			{
				tutorialManager.helper.commonHelper.HideComplete();
				tutorialManager.Change(new TutorialAttack(tutorialManager));
			}
		}

		private void ShowDialogMove()
		{
			timer += Time.deltaTime;
			if (MOVE_TRAINING_TIME < timer)
			{
				timer = 0f;
				tutorialManager.helper.moveHelper.HideHelpPicture(null);
				tutorialManager.helper.fingerMove.gameObject.SetActive(false);
				tutorialManager.Change(new TutorialBattle(tutorialManager));
			}
		}

		private void ShowDialogRollingWait()
		{
			timer += Time.deltaTime;
			if (ROLLING_TRAINING_WAIT_TIME < timer)
			{
				tutorialManager.helper.avoidHelper.ShowHelpText();
				currentPhase = Phase.SHOW_DIALOG_ROLLING;
				timer = 0f;
				UITweenCtrl component = tutorialManager.helper.fingerRolling.GetComponent<UITweenCtrl>();
				if ((UnityEngine.Object)null != (UnityEngine.Object)component)
				{
					component.gameObject.SetActive(true);
					component.Play(true, null);
				}
			}
		}

		private void ShowDialogRolling()
		{
			timer += Time.deltaTime;
			if (MOVE_TRAINING_TIME < timer)
			{
				tutorialManager.helper.avoidHelper.HideHelpText(null);
				currentPhase = Phase.FINISH_AND_WAIT_DIALOG;
				tutorialManager.Change(new TutorialBattle(tutorialManager));
				timer = 0f;
				tutorialManager.helper.fingerRolling.gameObject.SetActive(false);
			}
		}

		public override void Final()
		{
			if ((UnityEngine.Object)targetAreaObject != (UnityEngine.Object)null)
			{
				EffectManager.ReleaseEffect(targetAreaObject.gameObject, true, false);
				targetAreaObject = null;
			}
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_guide_movement1, "Tutorial");
		}

		private void PlayerMove()
		{
			InGameSettingsManager.SelfController parameter = base.selfController.parameter;
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right = cameraTransform.right;
			Vector3 forward = cameraTransform.forward;
			forward.y = 0f;
			forward.Normalize();
			Vector3 vector = right * touchInfo.axis.x * parameter.moveForwardSpeed + forward * touchInfo.axis.y * parameter.moveForwardSpeed;
			Character.MOTION_ID motion_id = Character.MOTION_ID.WALK;
			base.character.ActMoveVelocity((!parameter.enableRootMotion) ? vector : Vector3.zero, parameter.moveForwardSpeed, motion_id);
			base.character.SetLerpRotation(vector);
		}

		private void Rotate(ref Vector2 v, float degrees)
		{
			float num = Mathf.Sin(degrees * 0.0174532924f);
			float num2 = Mathf.Cos(degrees * 0.0174532924f);
			float x = v.x;
			float y = v.y;
			v.x = num2 * x - num * y;
			v.y = num * x + num2 * y;
		}
	}

	public class TutorialRolling : State
	{
		private enum Phase
		{
			NONE,
			WAIT_EXPLAIN_WINDOW,
			WAIT_DISP_HELP_TEXT,
			AUTO_CONTROL_ROLLING_RIGHT,
			WAIT_AUTO_ROLLING_RIGHT,
			AUTO_CONTROL_ROLLING_LEFT,
			WAIT_AUTO_ROLLING_LEFT,
			PLAYER_ROLLING_TRAINING,
			FISNIH_AND_WAIT_DIALOG
		}

		private Phase currentPhase = Phase.WAIT_EXPLAIN_WINDOW;

		private float timer;

		private UISprite finger;

		private readonly float DURATION_DISP_HELPER_TEXT = 1.5f;

		private readonly float AUTO_CONTROL_AVOID_TIME = 0.2f;

		private int playerRollingNum;

		private readonly int PLAYER_ROLLING_SUCCESS_NUM = 3;

		public TutorialRolling(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public void AddRollingCount()
		{
			playerRollingNum++;
		}

		public override void Init()
		{
			Player player = base.character as Player;
			player.SetDiableAction(Character.ACTION_ID.MOVE, true);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, true);
			player.SetDiableAction(Character.ACTION_ID.MAX, false);
			player.SetDiableAction((Character.ACTION_ID)32, true);
			player.SetDiableAction((Character.ACTION_ID)18, true);
			tutorialManager.dialog.Open(0, "Tutorial_Avoidance_Text_0101", 0, "Tutorial_Avoidance_Text_0102");
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
			base.selfController.enabled = false;
		}

		public override void Update()
		{
			switch (currentPhase)
			{
			case Phase.WAIT_EXPLAIN_WINDOW:
				WaitExplainWindow();
				break;
			case Phase.WAIT_DISP_HELP_TEXT:
				WaitDispHelpText();
				break;
			case Phase.AUTO_CONTROL_ROLLING_RIGHT:
				AutoControllRollingRight();
				break;
			case Phase.WAIT_AUTO_ROLLING_RIGHT:
				WaitAutoRollingRight();
				break;
			case Phase.AUTO_CONTROL_ROLLING_LEFT:
				AutoControllRollingLeft();
				break;
			case Phase.WAIT_AUTO_ROLLING_LEFT:
				WaitAutoRollingLeft();
				break;
			case Phase.PLAYER_ROLLING_TRAINING:
				PlayerRollingTraining();
				break;
			case Phase.FISNIH_AND_WAIT_DIALOG:
				FinishAndWaitDialog();
				break;
			}
		}

		private void WaitExplainWindow()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				tutorialManager.dialog.Close(1, null);
				timer = 0f;
				tutorialManager.helper.avoidHelper.ShowHelpText();
				tutorialManager.helper.commonHelper.ShowAutoControlMark();
				currentPhase = Phase.WAIT_DISP_HELP_TEXT;
			}
		}

		private void WaitDispHelpText()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_HELPER_TEXT < timer)
			{
				timer = 0f;
				currentPhase = Phase.AUTO_CONTROL_ROLLING_RIGHT;
				finger = tutorialManager.helper.commonHelper.ShowFinger(0.1f);
			}
		}

		private void AutoControllRollingRight()
		{
			timer += Time.deltaTime;
			if ((UnityEngine.Object)finger != (UnityEngine.Object)null)
			{
				Vector3 a = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.25f, 0f);
				Vector3 b = new Vector3((float)Screen.width * 0.75f, (float)Screen.height * 0.25f, 0f);
				finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(a, b, timer / AUTO_CONTROL_AVOID_TIME));
			}
			if (AUTO_CONTROL_AVOID_TIME < timer)
			{
				timer = 0f;
				tutorialManager.helper.commonHelper.HideFinger(null);
				Rolling(new Vector2(1f, 0f));
				currentPhase = Phase.WAIT_AUTO_ROLLING_RIGHT;
			}
		}

		private void WaitAutoRollingRight()
		{
			if (base.character.actionID != Character.ACTION_ID.MAX)
			{
				finger = tutorialManager.helper.commonHelper.ShowFinger(0.1f);
				currentPhase = Phase.AUTO_CONTROL_ROLLING_LEFT;
			}
		}

		private void AutoControllRollingLeft()
		{
			timer += Time.deltaTime;
			if ((UnityEngine.Object)finger != (UnityEngine.Object)null)
			{
				Vector3 a = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.25f, 0f);
				Vector3 b = new Vector3((float)Screen.width * 0.25f, (float)Screen.height * 0.25f, 0f);
				finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(a, b, timer / AUTO_CONTROL_AVOID_TIME));
			}
			if (AUTO_CONTROL_AVOID_TIME < timer)
			{
				timer = 0f;
				tutorialManager.helper.commonHelper.HideFinger(null);
				Rolling(new Vector2(-1f, 0f));
				currentPhase = Phase.WAIT_AUTO_ROLLING_LEFT;
			}
		}

		private void WaitAutoRollingLeft()
		{
			if (base.character.actionID != Character.ACTION_ID.MAX)
			{
				currentPhase = Phase.NONE;
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.avoidHelper.HideHelpText(delegate
				{
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					base.selfController.enabled = true;
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Avoidance_Text_0104");
					playerRollingNum = 0;
					currentPhase = Phase.PLAYER_ROLLING_TRAINING;
				});
			}
		}

		private void PlayerRollingTraining()
		{
			if (PLAYER_ROLLING_SUCCESS_NUM <= playerRollingNum)
			{
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					timer = 0f;
					tutorialManager.helper.commonHelper.ShowComplete();
					currentPhase = Phase.FISNIH_AND_WAIT_DIALOG;
				});
			}
		}

		private void FinishAndWaitDialog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_COMPLETE < timer)
			{
				tutorialManager.helper.commonHelper.HideComplete();
				tutorialManager.Change(new TutorialAttack(tutorialManager));
			}
		}

		private void Rolling(Vector2 direction)
		{
			SelfController.Command command = new SelfController.Command();
			command.type = SelfController.COMMAND_TYPE.AVOID;
			command.inputVec = direction;
			base.selfController.SetCommand(command);
		}
	}

	public class TutorialAttack : State
	{
		private enum Phase
		{
			NONE,
			WAIT_DISP_FIRST_DIALOG,
			WAIT_DISP_HELP_TEXT,
			AUTO_CONTROL_ATTACK,
			WAIT_AUTO_CONTROL_ATTACK,
			PLAYER_ATTACK_TRAINING_WAIT,
			PLAYER_ATTACK_TRAINING,
			WAIT_PLAYER_ATTACK,
			WAIT_DISP_COMPLETE_FOR_ATTACK,
			WAIT_DISP_COMBO_DIALOG,
			WAIT_DISP_COMBO_HELP_TEXT,
			AUTO_CONTROL_COMBO,
			WAIT_AUTO_CONTOROL_COMBO,
			PLAYER_COMBO_TRAINING,
			WAIT_DISP_COMPLETE_FOR_COMBO
		}

		private Phase currentPhase = Phase.WAIT_DISP_FIRST_DIALOG;

		private float timer;

		private UISprite tapFinger;

		private readonly float ENEMY_SPAWN_DISTANCE = 26f;

		private readonly int NUM_ENEMIES = 20;

		private int AttackCounter;

		private readonly float DURATION_DISP_HELP_TEXT = 1.5f;

		private bool isTutorialTextShowed;

		private readonly float WAIT_FOR_DISP_TAP_FINGER = 0.5f;

		private int attackCount;

		public TutorialAttack(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public override void Init()
		{
			Player player = base.character as Player;
			player.SetDiableAction(Character.ACTION_ID.MOVE, true);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, false);
			player.SetDiableAction(Character.ACTION_ID.MAX, true);
			player.SetDiableAction((Character.ACTION_ID)32, true);
			player.SetDiableAction((Character.ACTION_ID)18, true);
			InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTap, new InputManager.OnTouchDelegate(TutorialAttackOnTap));
			currentPhase = Phase.WAIT_PLAYER_ATTACK;
		}

		private void TutorialAttackOnTap(InputManager.TouchInfo touch_info)
		{
			AttackCounter++;
		}

		public override void Update()
		{
			switch (currentPhase)
			{
			case Phase.WAIT_DISP_FIRST_DIALOG:
				WaitDispFirstDialog();
				break;
			case Phase.WAIT_DISP_HELP_TEXT:
				WaitDispHelpText();
				break;
			case Phase.AUTO_CONTROL_ATTACK:
				AutoControlAttack();
				break;
			case Phase.WAIT_AUTO_CONTROL_ATTACK:
				WaitAutoControlAttack();
				break;
			case Phase.PLAYER_ATTACK_TRAINING_WAIT:
				PlayerAttackTrainingWait();
				break;
			case Phase.PLAYER_ATTACK_TRAINING:
				PlayerAttackTraining();
				break;
			case Phase.WAIT_PLAYER_ATTACK:
				ShowAttackTutorial();
				WaitPlayerAttack();
				break;
			case Phase.WAIT_DISP_COMPLETE_FOR_ATTACK:
				WaitDispCompleteForAttack();
				break;
			case Phase.WAIT_DISP_COMBO_DIALOG:
				WaitDispComboDalog();
				break;
			case Phase.WAIT_DISP_COMBO_HELP_TEXT:
				WaitDispComboHelpText();
				break;
			case Phase.AUTO_CONTROL_COMBO:
				AutoControlCombo();
				break;
			case Phase.WAIT_AUTO_CONTOROL_COMBO:
				WaitAutoControlCombo();
				break;
			case Phase.PLAYER_COMBO_TRAINING:
				PlayerComboTraining();
				break;
			case Phase.WAIT_DISP_COMPLETE_FOR_COMBO:
				WaitDispCompleteForCombo();
				break;
			}
		}

		private void WaitDispFirstDialog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				tutorialManager.dialog.Close(1, null);
				tutorialManager.helper.attackHelper.ShowHelpText();
				tutorialManager.helper.commonHelper.ShowAutoControlMark();
				tapFinger = tutorialManager.helper.commonHelper.ShowTapFinger();
				tapFinger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 4f, 0f));
				currentPhase = Phase.WAIT_DISP_HELP_TEXT;
			}
		}

		private void WaitDispHelpText()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_HELP_TEXT < timer)
			{
				timer = 0f;
				currentPhase = Phase.AUTO_CONTROL_ATTACK;
			}
		}

		private void AutoControlAttack()
		{
			tutorialManager.helper.commonHelper.ShowTapIcon(0);
			SelfController.Command command = new SelfController.Command();
			command.type = SelfController.COMMAND_TYPE.ATTACK;
			command.isTouchOn = true;
			base.selfController.SetCommand(command);
			currentPhase = Phase.WAIT_AUTO_CONTROL_ATTACK;
		}

		private void WaitAutoControlAttack()
		{
			if (base.character.actionID != Character.ACTION_ID.ATTACK)
			{
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.commonHelper.HideTapIcon();
				currentPhase = Phase.NONE;
				tutorialManager.helper.commonHelper.HideTapFinger(null);
				tutorialManager.helper.attackHelper.HideHelpText(delegate
				{
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0203");
					timer = 0f;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_ATTACK_TRAINING;
				});
			}
		}

		private void PlayerAttackTrainingWait()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_HELP_TEXT < timer)
			{
				timer = 0f;
				currentPhase = Phase.PLAYER_ATTACK_TRAINING;
				tutorialManager.helper.attackHelper.ShowHelpText();
			}
		}

		private void PlayerAttackTraining()
		{
			if (base.character.actionID == Character.ACTION_ID.ATTACK)
			{
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				base.selfController.enabled = false;
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					tutorialManager.helper.commonHelper.ShowComplete();
					currentPhase = Phase.WAIT_PLAYER_ATTACK;
				});
			}
		}

		private void WaitPlayerAttack()
		{
			if (AttackCounter >= 3)
			{
				InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTap, new InputManager.OnTouchDelegate(TutorialAttackOnTap));
				tutorialManager.helper.commonHelper.HideTapIcon();
				tutorialManager.helper.commonHelper.HideTapFinger(null);
				tutorialManager.helper.attackHelper.HideHelpText(null);
				tutorialManager.helper.commonHelper.ShowExcellent();
				timer = 0f;
				currentPhase = Phase.WAIT_DISP_COMPLETE_FOR_ATTACK;
			}
		}

		private void ShowAttackTutorial()
		{
			timer += Time.deltaTime;
			if (timer >= 1f && !isTutorialTextShowed)
			{
				isTutorialTextShowed = true;
				tutorialManager.helper.attackHelper.ShowHelpText();
				tutorialManager.helper.commonHelper.ShowTapIcon(0);
				tutorialManager.helper.commonHelper.ShowTapIcon(1);
				tutorialManager.helper.commonHelper.ShowTapIcon(2);
				tapFinger = tutorialManager.helper.commonHelper.ShowTapFinger();
				tapFinger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 4f, 0f));
				tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2f, delegate
				{
					tutorialManager.helper.attackHelper.HideHelpText(delegate
					{
						tutorialManager.helper.commonHelper.HideTapIcon();
						tutorialManager.helper.commonHelper.HideTapFinger(null);
						timer = 0f;
						isTutorialTextShowed = false;
					});
				}));
			}
		}

		private void WaitDispCompleteForAttack()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_COMPLETE < timer)
			{
				tutorialManager.helper.commonHelper.HideExcellent();
				timer = 0f;
				tutorialManager.Change(new TutorialBattle(tutorialManager));
			}
		}

		private void WaitDispComboDalog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				tutorialManager.dialog.Close(1, null);
				tutorialManager.helper.attackHelper.ShowComboHelpText();
				tutorialManager.helper.commonHelper.ShowAutoControlMark();
				currentPhase = Phase.WAIT_DISP_COMBO_HELP_TEXT;
			}
		}

		private void WaitDispComboHelpText()
		{
			timer += Time.deltaTime;
			if (WAIT_FOR_DISP_TAP_FINGER < timer)
			{
				tapFinger = tutorialManager.helper.commonHelper.ShowTapFinger();
				currentPhase = Phase.AUTO_CONTROL_COMBO;
			}
		}

		private void AutoControlCombo()
		{
			Player player = base.character as Player;
			if (base.character.actionID != Character.ACTION_ID.ATTACK)
			{
				tutorialManager.helper.commonHelper.ShowTapIcon(0);
				SelfController.Command command = new SelfController.Command();
				command.type = SelfController.COMMAND_TYPE.ATTACK;
				command.isTouchOn = true;
				base.selfController.SetCommand(command);
				attackCount = 1;
			}
			else if (player.enableInputCombo)
			{
				tutorialManager.helper.commonHelper.ShowTapIcon(attackCount);
				attackCount++;
				player.ActAttackCombo();
			}
			if (base.character.IsPlayingMotion(17, true))
			{
				currentPhase = Phase.WAIT_AUTO_CONTOROL_COMBO;
			}
		}

		private void WaitAutoControlCombo()
		{
			if (base.character.actionID != Character.ACTION_ID.ATTACK)
			{
				tutorialManager.helper.commonHelper.HideTapFinger(null);
				tutorialManager.helper.commonHelper.HideTapIcon();
				currentPhase = Phase.NONE;
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.attackHelper.HideComboHelpText(delegate
				{
					tutorialManager.PopEnemy(base.character._transform.position + base.character._transform.forward * ENEMY_SPAWN_DISTANCE, false, -1f, -1f);
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0208");
					timer = 0f;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_COMBO_TRAINING;
				});
			}
		}

		private void PlayerComboTraining()
		{
			if (base.character.IsPlayingMotion(17, true))
			{
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					tutorialManager.helper.commonHelper.ShowComplete();
					timer = 0f;
					currentPhase = Phase.WAIT_DISP_COMPLETE_FOR_COMBO;
				});
			}
			else if (tutorialManager.CheckAllEnemiesDead())
			{
				timer = 0f;
				tutorialManager.PopEnemy(base.character._transform.position + base.character._transform.forward * ENEMY_SPAWN_DISTANCE, false, -1f, -1f);
			}
		}

		private void WaitDispCompleteForCombo()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_COMPLETE < timer)
			{
				tutorialManager.helper.commonHelper.HideComplete();
				tutorialManager.Change(new TutorialSpecialMove(tutorialManager));
			}
		}

		public override void Final()
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_guide_movement2, "Tutorial");
		}
	}

	public class TutorialSpecialMove : State
	{
		private enum Phase
		{
			NONE,
			WAIT_DISP_FIRST_DIALOG,
			WAIT_DISP_HELP_TEXT,
			AUTO_CONTROL_GUARD_START,
			AUTO_CONTROL_GUARD,
			PLAYER_GUAD_TRAINING,
			WAIT_DISP_COMPLETE
		}

		private Phase currentPhase = Phase.WAIT_DISP_FIRST_DIALOG;

		private float timer;

		private readonly float AUTO_GUARD_DURATION = 2f;

		private readonly float DURATION_DISP_DIALOG = 4f;

		private readonly float DURATION_WAIT_DISP_HELP_TEXT = 2.5f;

		private readonly float GUARD_SUCCESS_TIME = 2f;

		public TutorialSpecialMove(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public override void Init()
		{
			Player player = base.character as Player;
			player.SetDiableAction(Character.ACTION_ID.MOVE, false);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, true);
			player.SetDiableAction(Character.ACTION_ID.MAX, true);
			player.SetDiableAction((Character.ACTION_ID)32, false);
			player.SetDiableAction((Character.ACTION_ID)18, false);
			tutorialManager.dialog.Open(1, "Tutorial_Special_Text_0401", 1, "Tutorial_Special_Text_0402");
			base.selfController.enabled = false;
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
		}

		public override void Update()
		{
			switch (currentPhase)
			{
			case Phase.WAIT_DISP_FIRST_DIALOG:
				WaitDispFirstDialog();
				break;
			case Phase.WAIT_DISP_HELP_TEXT:
				WaitDispHelpText();
				break;
			case Phase.AUTO_CONTROL_GUARD_START:
				AutoControlGuardStart();
				break;
			case Phase.AUTO_CONTROL_GUARD:
				AutoControlGuard();
				break;
			case Phase.PLAYER_GUAD_TRAINING:
				PlayerGuardTraining();
				break;
			case Phase.WAIT_DISP_COMPLETE:
				WaitDispComplete();
				break;
			}
		}

		private void WaitDispFirstDialog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				tutorialManager.dialog.Close(1, delegate
				{
					tutorialManager.helper.commonHelper.ShowAutoControlMark();
					tutorialManager.helper.guardHelper.ShowHelpText();
					currentPhase = Phase.WAIT_DISP_HELP_TEXT;
				});
				currentPhase = Phase.NONE;
			}
		}

		private void WaitDispHelpText()
		{
			timer += Time.deltaTime;
			if (DURATION_WAIT_DISP_HELP_TEXT < timer)
			{
				timer = 0f;
				UISprite uISprite = tutorialManager.helper.commonHelper.ShowLongTapFinger();
				uISprite.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 4f, 0f));
				currentPhase = Phase.AUTO_CONTROL_GUARD_START;
			}
		}

		private void AutoControlGuardStart()
		{
			SelfController.Command command = new SelfController.Command();
			command.type = SelfController.COMMAND_TYPE.SPECIAL_ACTION;
			command.isTouchOn = true;
			base.selfController.SetCommand(command);
			currentPhase = Phase.AUTO_CONTROL_GUARD;
		}

		private void AutoControlGuard()
		{
			timer += Time.deltaTime;
			if (AUTO_GUARD_DURATION <= timer)
			{
				timer = 0f;
				base.character.ActIdle(false, -1f);
				tutorialManager.helper.commonHelper.HideLongTapFinger(null);
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				currentPhase = Phase.NONE;
				tutorialManager.helper.guardHelper.HideHelpText(delegate
				{
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 1, "Tutorial_Special_Text_0405", 1, "Tutorial_Special_Text_0404");
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_GUAD_TRAINING;
				});
			}
		}

		private void PlayerGuardTraining()
		{
			Player player = base.character as Player;
			if (player.isGuardWalk || base.character.actionID == (Character.ACTION_ID)18)
			{
				player.SetDiableAction(Character.ACTION_ID.MOVE, false);
				timer += Time.deltaTime;
			}
			else
			{
				player.SetDiableAction(Character.ACTION_ID.MOVE, true);
				timer = 0f;
			}
			if (GUARD_SUCCESS_TIME <= timer)
			{
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(2, delegate
				{
					timer = 0f;
					tutorialManager.helper.commonHelper.ShowComplete();
					currentPhase = Phase.WAIT_DISP_COMPLETE;
				});
			}
		}

		private void WaitDispComplete()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_COMPLETE < timer)
			{
				tutorialManager.helper.commonHelper.HideComplete();
				tutorialManager.Change(new TutorialBattle(tutorialManager));
			}
		}
	}

	public class TutorialBattle : State
	{
		private enum Phase
		{
			NONE,
			PLAYER_ATTACK_TRAINING_WAIT,
			PLAYER_ATTACK_TRAINING,
			WAIT_DISP_FIRST_DIALOG,
			WAIT_DISP_PORTAL_EXPLAIN_DIALOG,
			WAIT_PORTAL_EFFECT,
			WAIT_DISP_HELP_PICTURE_0,
			WAIT_DISP_HELP_PICTURE_1,
			WAIT_DISP_PLAYER_TRAINING_DIALOG,
			PLAYER_BATTLE_TRAINING,
			WAIT_HIDE_PORTAL_OPEN_INFO,
			WAIT_DISP_LAST_DIALOG
		}

		private Phase currentPhase = Phase.WAIT_DISP_FIRST_DIALOG;

		private float timer;

		private readonly float ENEMY_SPAWN_DISTANCE = 3f;

		private readonly float PLAYER_ATTACK_TRAINING_WAIT_TIME = 1.5f;

		private readonly float DISP_FINGER_ATTACK_TIME = 5f;

		private int currentPortalPoint;

		private bool createdPNC;

		private readonly float WAIT_PORTAL_EFFECT_TIME = 2f;

		private readonly float BATTLE_TRAINING_DIALOG_TIME = 2.5f;

		private bool battleTrainingDialog;

		private Vector3 portalPosition;

		private Vector3 targetCameraPos;

		private Vector3 cameraPos;

		public TutorialBattle(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public override void Init()
		{
			Player player = base.character as Player;
			player.SetDiableAction(Character.ACTION_ID.MOVE, true);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, true);
			player.SetDiableAction(Character.ACTION_ID.MAX, true);
			player.SetDiableAction((Character.ACTION_ID)32, true);
			player.SetDiableAction((Character.ACTION_ID)18, true);
			tutorialManager.CheckAndCallOnDeadAll(null);
			tutorialManager.helper.moveHelper.HideHelpText(null);
			currentPhase = Phase.WAIT_DISP_FIRST_DIALOG;
			timer = 0f;
		}

		public override void Update()
		{
			if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID == 10000101)
			{
				tutorialManager.dialog.Close(1, null);
				tutorialManager.Change(new TutorialBossBattle(tutorialManager));
			}
			else
			{
				if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
				{
					Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
					if ((UnityEngine.Object)self != (UnityEngine.Object)null && self.hp < self.hpMax / 2)
					{
						self.hp = self.hpMax / 2;
					}
				}
				switch (currentPhase)
				{
				case Phase.PLAYER_ATTACK_TRAINING_WAIT:
					PlayerAttackTrainingWait();
					break;
				case Phase.PLAYER_ATTACK_TRAINING:
					PlayerAttackTraining();
					break;
				case Phase.WAIT_DISP_FIRST_DIALOG:
					WaitDispFirstDialog();
					break;
				case Phase.WAIT_DISP_PORTAL_EXPLAIN_DIALOG:
					GGPlayerBattleTraining();
					break;
				case Phase.WAIT_PORTAL_EFFECT:
					WaitPortalEffect();
					break;
				case Phase.WAIT_DISP_HELP_PICTURE_0:
					WaitDispHelpPicture0();
					break;
				case Phase.WAIT_DISP_HELP_PICTURE_1:
					WaitDispHelpPicture1();
					break;
				case Phase.WAIT_DISP_PLAYER_TRAINING_DIALOG:
					WaitDispPlayerTrainingDialog();
					break;
				case Phase.PLAYER_BATTLE_TRAINING:
					PlayerBattleTraining();
					break;
				case Phase.WAIT_HIDE_PORTAL_OPEN_INFO:
					WaitHidePortalOpenInfo();
					break;
				case Phase.WAIT_DISP_LAST_DIALOG:
					WaitDispLastDialog();
					break;
				}
			}
		}

		private void PlayerAttackTrainingWait()
		{
			tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			timer += Time.deltaTime;
			if (PLAYER_ATTACK_TRAINING_WAIT_TIME < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.helper.attackHelper.ShowHelpText();
				UITweenCtrl component = tutorialManager.helper.fingerAttack.GetComponent<UITweenCtrl>();
				if ((UnityEngine.Object)null != (UnityEngine.Object)component)
				{
					component.gameObject.SetActive(true);
					component.Play(true, null);
				}
				tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2.5f, delegate
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(false);
					tutorialManager.helper.attackHelper.HideHelpText(delegate
					{
						tutorialManager.StartCoroutine(tutorialManager.WaitForTime(0.5f, delegate
						{
							currentPhase = Phase.PLAYER_ATTACK_TRAINING;
							tutorialManager.helper.attackHelper.ShowHelpPicture();
						}));
					});
				}));
			}
		}

		private void PlayerAttackTraining()
		{
			timer += Time.deltaTime;
			if (DISP_FINGER_ATTACK_TIME < timer)
			{
				tutorialManager.helper.attackHelper.HideHelpPicture(null);
				if (tutorialManager.helper.fingerAttack.gameObject.activeSelf)
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(false);
				}
			}
			tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			if (tutorialManager.CheckAllEnemiesDead() && DISP_FINGER_ATTACK_TIME < timer)
			{
				tutorialManager.helper.attackHelper.HideHelpPicture(null);
				if (tutorialManager.helper.fingerAttack.gameObject.activeSelf)
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(false);
				}
				currentPhase = Phase.WAIT_PORTAL_EFFECT;
				timer = 0f;
			}
		}

		private void GGPlayerBattleTraining()
		{
			base.tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList != null && MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count > 0)
			{
				PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0];
				if (!createdPNC)
				{
					createdPNC = true;
					Player player = base.character as Player;
					player.SetDiableAction(Character.ACTION_ID.MOVE, false);
					player.SetDiableAction(Character.ACTION_ID.ATTACK, false);
					player.SetDiableAction(Character.ACTION_ID.MAX, false);
					player.SetDiableAction((Character.ACTION_ID)32, false);
					player.SetDiableAction((Character.ACTION_ID)18, false);
					Vector3 a = Vector3.zero;
					for (int i = 0; i < 1; i++)
					{
						float num = UnityEngine.Random.Range(0f, 360f);
						float num2 = 2.5f;
						InGameTutorialManager tutorialManager = base.tutorialManager;
						Vector2 positionXZ = player.positionXZ;
						float x = positionXZ.x;
						Vector2 positionXZ2 = player.positionXZ;
						Enemy enemy = tutorialManager.PopEnemy(new Vector3(x, 0f, positionXZ2.y) + new Vector3(num2 * Mathf.Cos(num * 0.0174532924f), 0f, num2 * Mathf.Sin(num * 0.0174532924f)), false, -1f, -1f);
						Vector2 positionXZ3 = player.positionXZ;
						float x2 = positionXZ3.x;
						Vector2 positionXZ4 = player.positionXZ;
						a = new Vector3(x2, 0f, positionXZ4.y) + new Vector3(num2 * Mathf.Cos(num * 0.0174532924f), 0f, num2 * Mathf.Sin(num * 0.0174532924f));
					}
					for (int j = 0; j < 1; j++)
					{
						float num3 = UnityEngine.Random.Range(0f, 360f);
						float num4 = 4f;
						InGameTutorialManager tutorialManager2 = base.tutorialManager;
						Vector2 positionXZ5 = player.positionXZ;
						float x3 = positionXZ5.x;
						Vector2 positionXZ6 = player.positionXZ;
						Enemy enemy2 = tutorialManager2.PopEnemy(new Vector3(x3, 0f, positionXZ6.y) + new Vector3(num4 * Mathf.Cos(num3 * 0.0174532924f), 0f, num4 * Mathf.Sin(num3 * 0.0174532924f)), false, -1f, -1f);
					}
					for (int k = 0; k < 1; k++)
					{
						float num5 = UnityEngine.Random.Range(0f, 360f);
						float num6 = 5f;
						InGameTutorialManager tutorialManager3 = base.tutorialManager;
						Vector2 positionXZ7 = player.positionXZ;
						float x4 = positionXZ7.x;
						Vector2 positionXZ8 = player.positionXZ;
						Enemy enemy3 = tutorialManager3.PopEnemy(new Vector3(x4, 0f, positionXZ8.y) + new Vector3(num6 * Mathf.Cos(num5 * 0.0174532924f), 0f, num6 * Mathf.Sin(num5 * 0.0174532924f)), false, -1f, -1f);
					}
					for (int l = 0; l < 10; l++)
					{
						float num7 = UnityEngine.Random.Range(0f, 360f);
						float num8 = UnityEngine.Random.Range(4f, 26f);
						Enemy enemy4 = base.tutorialManager.PopEnemy(new Vector3(num8 * Mathf.Cos(num7 * 0.0174532924f), 0f, num8 * Mathf.Sin(num7 * 0.0174532924f)), false, -1f, -1f);
					}
					Vector3 b = new Vector3(0f, 1.6f, 0f);
					Vector3 localScale = new Vector3(1.5f, 1.5f, 1.5f);
					base.tutorialManager.mdlArrow.localScale = localScale;
					base.tutorialManager.mdlArrow.position = a + b;
					base.tutorialManager.mdlArrow.gameObject.SetActive(true);
					base.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
				}
				if (currentPortalPoint != portalObject.nowPoint && !base.tutorialManager.dialog.isThreeLineLabel2Active())
				{
					currentPortalPoint = portalObject.nowPoint;
					base.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
				}
				else if (currentPortalPoint != portalObject.nowPoint && portalObject.isFull && base.tutorialManager.dialog.isThreeLineLabel2Active())
				{
					currentPortalPoint = portalObject.nowPoint;
					base.tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
					base.tutorialManager.dialog.HideThreeLineLabel2();
				}
				if (portalObject.isFull && !base.tutorialManager.helper.commonHelper.OnShowEnemyCount && base.tutorialManager.helper.commonHelper.CurrentEnemyShow > 0)
				{
					Debug.Log("Current Enemy Show: " + base.tutorialManager.helper.commonHelper.CurrentEnemyShow);
					base.tutorialManager.helper.commonHelper.HideEnemyCount();
					base.tutorialManager.helper.commonHelper.ShowSplendid();
					currentPhase = Phase.WAIT_PORTAL_EFFECT;
					base.tutorialManager.dialog.HideThreeLineLabel2();
					timer = 0f;
				}
				else if (portalObject.isFull && !base.tutorialManager.helper.commonHelper.OnShowEnemyCount && base.tutorialManager.helper.commonHelper.CurrentEnemyShow == 0)
				{
					timer += Time.deltaTime;
					if (timer > 2f)
					{
						Debug.Log("Current Enemy Show: " + base.tutorialManager.helper.commonHelper.CurrentEnemyShow);
						base.tutorialManager.helper.commonHelper.HideEnemyCount();
						base.tutorialManager.helper.commonHelper.ShowSplendid();
						currentPhase = Phase.WAIT_PORTAL_EFFECT;
						base.tutorialManager.dialog.HideThreeLineLabel2();
						timer = 0f;
					}
				}
			}
		}

		private void WaitPortalEffect()
		{
			timer += Time.deltaTime;
			if (WAIT_PORTAL_EFFECT_TIME < timer)
			{
				timer = 0f;
				currentPhase = Phase.PLAYER_BATTLE_TRAINING;
				List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
				portalPosition = portalObjectList[0]._transform.position;
				tutorialManager.ClearedPoppedEnemiesInfo();
				Vector3 position = base.character._transform.position;
				Vector3[] array = new Vector3[2]
				{
					position + new Vector3(3.5f, 0f, -5f),
					position + new Vector3(-3.5f, 0f, -5f)
				};
				for (int i = 0; i < array.Length; i++)
				{
					tutorialManager.PopEnemy(array[i], true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
				}
				tutorialManager.dialog.Open(1, "Tutorial_Portal_Text_0507", 1, "Tutorial_Portal_Text_0505");
				battleTrainingDialog = true;
			}
		}

		private void WaitDispFirstDialog()
		{
			currentPhase = Phase.WAIT_DISP_PORTAL_EXPLAIN_DIALOG;
		}

		private void WaitDispPortalExplainDialog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					tutorialManager.helper.battleHelper.ShowHelpPicture0();
					currentPhase = Phase.WAIT_DISP_HELP_PICTURE_0;
					List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
					portalObjectList[0].gameObject.SetActive(true);
					portalPosition = portalObjectList[0]._transform.position;
					tutorialManager.ClearedPoppedEnemiesInfo();
					Vector3 position = base.character._transform.position;
					Vector3[] array = new Vector3[3]
					{
						position + new Vector3(3.5f, 0f, -5f),
						position + new Vector3(-3.5f, 0f, -5f),
						position + new Vector3(0f, 0f, -5f)
					};
					for (int i = 0; i < array.Length; i++)
					{
						tutorialManager.PopEnemy(array[i], true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
					}
					tutorialManager.SetActiveAllEnemiesController(false);
				});
			}
		}

		private void WaitDispHelpPicture0()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.helper.battleHelper.HideHelpPicture0(delegate
				{
					tutorialManager.helper.battleHelper.ShowHelpPicture1();
					currentPhase = Phase.WAIT_DISP_HELP_PICTURE_1;
				});
			}
		}

		private void WaitDispHelpPicture1()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.helper.battleHelper.HideHelpPicture1(delegate
				{
					base.selfController.enabled = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					tutorialManager.SetActiveAllEnemiesController(true);
					currentPhase = Phase.PLAYER_BATTLE_TRAINING;
					List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
					portalPosition = portalObjectList[0]._transform.position;
					tutorialManager.ClearedPoppedEnemiesInfo();
					Vector3 position = base.character._transform.position;
					Vector3[] array = new Vector3[2]
					{
						position + new Vector3(3.5f, 0f, -5f),
						position + new Vector3(-3.5f, 0f, -5f)
					};
					for (int i = 0; i < array.Length; i++)
					{
						tutorialManager.PopEnemy(array[i], true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
					}
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 1, "Tutorial_Portal_Text_0505");
					battleTrainingDialog = true;
				});
			}
		}

		private void WaitDispPlayerTrainingDialog()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.dialog.Close(1, delegate
				{
					currentPhase = Phase.PLAYER_BATTLE_TRAINING;
				});
			}
		}

		private void PlayerBattleTraining()
		{
			tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			timer += Time.deltaTime;
			if (BATTLE_TRAINING_DIALOG_TIME < timer && battleTrainingDialog)
			{
				battleTrainingDialog = false;
				tutorialManager.dialog.Close(1, null);
			}
			if (tutorialManager.CheckAllEnemiesDead())
			{
				tutorialManager.dialog.Close(1, null);
				MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
				cameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				timer = 0f;
				targetCameraPos = portalPosition + (cameraPos - base.character._position);
				currentPhase = Phase.WAIT_HIDE_PORTAL_OPEN_INFO;
			}
		}

		private void WaitHidePortalOpenInfo()
		{
			timer += Time.deltaTime;
			Vector3 position = Vector3.Lerp(cameraPos, targetCameraPos, timer / 1.2f);
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = position;
			if (!(timer < 2.7f))
			{
				MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = cameraPos;
				MonoBehaviourSingleton<InGameCameraManager>.I.enabled = true;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
				base.selfController.enabled = true;
				currentPhase = Phase.WAIT_DISP_LAST_DIALOG;
				Vector3 localScale = new Vector3(2.5f, 2.5f, 2.5f);
				Vector3 b = new Vector3(0f, 2.6f, 0f);
				tutorialManager.mdlArrow.localScale = localScale;
				tutorialManager.mdlArrow.position = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0]._transform.position + b;
				tutorialManager.mdlArrow.gameObject.SetActive(true);
			}
		}

		private void WaitDispLastDialog()
		{
			tutorialManager.UpdateArrowModel();
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				tutorialManager.dialog.Close(1, null);
				tutorialManager.Change(new TutorialBossBattle(tutorialManager));
			}
		}

		private void CreatePortalPoint(Enemy enemy)
		{
			if (!MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				return;
			}
			Coop_Model_EnemyDefeat coop_Model_EnemyDefeat = new Coop_Model_EnemyDefeat();
			coop_Model_EnemyDefeat.ppt = 1;
			Coop_Model_EnemyDefeat coop_Model_EnemyDefeat2 = coop_Model_EnemyDefeat;
			Vector3 position = enemy._transform.position;
			coop_Model_EnemyDefeat2.x = (int)position.x;
			Coop_Model_EnemyDefeat coop_Model_EnemyDefeat3 = coop_Model_EnemyDefeat;
			Vector3 position2 = enemy._transform.position;
			coop_Model_EnemyDefeat3.z = (int)position2.z;
			Vector3 position3 = tutorialManager.mdlArrow.gameObject.transform.position;
			float x = position3.x;
			Vector3 position4 = enemy._transform.position;
			if (x == position4.x)
			{
				Vector3 position5 = tutorialManager.mdlArrow.gameObject.transform.position;
				float z = position5.z;
				Vector3 position6 = enemy._transform.position;
				if (z == position6.z)
				{
					tutorialManager.mdlArrow.gameObject.SetActive(false);
				}
			}
			FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
			if (!MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(coop_Model_EnemyDefeat.ppt))
			{
				goto IL_00f9;
			}
			goto IL_00f9;
			IL_00f9:
			if (portalPointToPortalInfo != null && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0].nowPoint == 0)
				{
					tutorialManager.helper.commonHelper.HideEnemyCount();
					tutorialManager.dialog.OpenThreeLineLabel2();
				}
				MonoBehaviourSingleton<InGameProgress>.I.CreatePortalPoint(portalPointToPortalInfo, coop_Model_EnemyDefeat);
			}
		}

		public override void Final()
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_guide_movement3, "Tutorial");
		}
	}

	public class TutorialBossBattle : State
	{
		public enum Phase
		{
			NONE,
			WAIT_SCENE_LOADING,
			WALKING_TO_PLAYER,
			WAIT_DISP_WEAK_MARK,
			WAIT_HELP_PICTURE_0,
			WAIT_HELP_PICTURE_1,
			SOLO_BATTLE,
			WAIT_DOWN,
			BATTLE_WITH_FRIENDS,
			WAIT_SKILL_HELP_PICTURE_0,
			WAIT_SKILL_HELP_PICTURE_1,
			PLAYER_CONTROL_SKILL,
			ENEMY_ESCAPE_BATTLE,
			DISP_TIELE
		}

		private const int NPC_PLAYER_ID_0 = 990;

		private const int NPC_PLAYER_ID_1 = 991;

		private const int NPC_PLAYER_ID_2 = 992;

		private readonly int ENEMY_ID = 110010911;

		private readonly int ENEMY_LV = 1;

		private readonly float SOLO_BATTLE_TIME_LIMIT = 20f;

		private readonly float SKILL_WAIT_LIMIT_TIME = 20f;

		private readonly float BATTLE_WITH_FRIEND_TIME = 35f;

		private readonly float BOSS_MIN_HP_RATE = 0.2f;

		private readonly float BOSS_ESCAPE_HP_RATE = 0.5f;

		private Phase currentPhase = Phase.WAIT_SCENE_LOADING;

		private Enemy boss;

		private EnemyController bossController;

		private float timer;

		private Transform playerStatusRoot;

		private Transform enemyStatusRoot;

		private readonly float DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR = 100f;

		private readonly int TUTORIAL_STAMP_ID = 1;

		private Transform cursorTop;

		private readonly int NPC_LOAD_COMPLETE_BIT = 7;

		private int m_npcLoadCompleteBitFlag;

		private static readonly int[] ENTRY_VOICE_PATTERN_1 = new int[1]
		{
			17
		};

		private static readonly int[] ENTRY_VOICE_PATTERN_2 = new int[1]
		{
			200016
		};

		private static readonly int[] ENTRY_VOICE_PATTERN_3 = new int[1]
		{
			10018
		};

		private bool isTrackDragonFight;

		private bool IsEscapeBossHp => boss.hp <= (int)((float)boss.hpMax * BOSS_ESCAPE_HP_RATE);

		private bool IsCompleteLoadNPC => m_npcLoadCompleteBitFlag == NPC_LOAD_COMPLETE_BIT;

		public TutorialBossBattle(InGameTutorialManager owner)
			: base(owner)
		{
			InGameSettingsManager.TutorialParam tutorialParam = MonoBehaviourSingleton<InGameSettingsManager>.I.tutorialParam;
			if (tutorialParam != null)
			{
				ENEMY_ID = tutorialParam.enemyID;
				ENEMY_LV = tutorialParam.enemyLv;
				SOLO_BATTLE_TIME_LIMIT = tutorialParam.soloBattleTimeLimit;
				SKILL_WAIT_LIMIT_TIME = tutorialParam.skillWaitLimitTime;
				BATTLE_WITH_FRIEND_TIME = tutorialParam.battleWithFriendTime;
				BOSS_MIN_HP_RATE = tutorialParam.bossMinHpRate;
				BOSS_ESCAPE_HP_RATE = tutorialParam.bossEscapeHpRate;
				if (BATTLE_WITH_FRIEND_TIME < 10f)
				{
					BATTLE_WITH_FRIEND_TIME = 10f;
				}
			}
		}

		public override void Init()
		{
		}

		public override void Update()
		{
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if ((UnityEngine.Object)self != (UnityEngine.Object)null && self.hp < self.hpMax / 2)
				{
					self.hp = self.hpMax / 2;
				}
			}
			if ((UnityEngine.Object)boss != (UnityEngine.Object)null && boss.hpMax > 0)
			{
				int num = (int)((float)boss.hpMax * BOSS_MIN_HP_RATE);
				if (boss.hp < num)
				{
					boss.hp = num;
				}
			}
			switch (currentPhase)
			{
			case Phase.WAIT_SCENE_LOADING:
				WaitSceneLoading();
				break;
			case Phase.WALKING_TO_PLAYER:
				WalkingToPlayer();
				break;
			case Phase.WAIT_DISP_WEAK_MARK:
				WaitDispWeakMark();
				break;
			case Phase.WAIT_HELP_PICTURE_0:
				WaitHelpPicture0();
				break;
			case Phase.WAIT_HELP_PICTURE_1:
				WaitHelpPicture1();
				break;
			case Phase.SOLO_BATTLE:
				SoloBattle();
				break;
			case Phase.WAIT_DOWN:
				WaitDown();
				break;
			case Phase.WAIT_SKILL_HELP_PICTURE_0:
				WaitSkillHelpPicture0();
				break;
			case Phase.WAIT_SKILL_HELP_PICTURE_1:
				WaitSkillHelpPicture1();
				break;
			case Phase.PLAYER_CONTROL_SKILL:
				PlayerControlSkill();
				break;
			case Phase.BATTLE_WITH_FRIENDS:
				BattleWithFriends();
				break;
			case Phase.ENEMY_ESCAPE_BATTLE:
				EnemyEscapeBattle();
				break;
			case Phase.DISP_TIELE:
				DispTitle();
				break;
			}
		}

		private void WaitSceneLoading()
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				ResourceManager.autoRetry = false;
			}
			tutorialManager.UpdateArrowModel();
			if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID == 10000101)
			{
				if ((UnityEngine.Object)null != (UnityEngine.Object)tutorialManager.mdlArrow)
				{
					UnityEngine.Object.Destroy(tutorialManager.mdlArrow.gameObject);
				}
				if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "InGameMain"))
				{
					if (!tutorialManager.isLoadingSE && !tutorialManager.isCompleteLoadSE)
					{
						tutorialManager.LoadSE();
					}
					if (tutorialManager.isCompleteLoadSE && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().isInitialized)
					{
						ResourceManager.autoRetry = true;
						MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
						InGameMain inGameMain = null;
						if ((UnityEngine.Object)playerStatusRoot == (UnityEngine.Object)null)
						{
							inGameMain = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain);
							if ((UnityEngine.Object)inGameMain != (UnityEngine.Object)null)
							{
								Transform transform = inGameMain._transform.FindChild("InGameMain/StaticSwitchPanel/NewMenuParent");
								if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
								{
									transform.gameObject.SetActive(false);
								}
								playerStatusRoot = inGameMain._transform.FindChild("InGameMain/PlayerStatus");
								if ((UnityEngine.Object)playerStatusRoot != (UnityEngine.Object)null)
								{
									playerStatusRoot.gameObject.SetActive(false);
								}
								transform = inGameMain._transform.FindChild("InGameMain/StaticSwitchPanel/ChatButtonParent");
								if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
								{
									transform.gameObject.SetActive(false);
								}
							}
						}
						if (base.character.actionID != (Character.ACTION_ID)22)
						{
							inGameMain = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain);
							if ((UnityEngine.Object)inGameMain != (UnityEngine.Object)null && (UnityEngine.Object)enemyStatusRoot == (UnityEngine.Object)null)
							{
								enemyStatusRoot = inGameMain._transform.FindChild("InGameMain/StaticPanel/EnemyStatus");
							}
							if ((UnityEngine.Object)enemyStatusRoot != (UnityEngine.Object)null)
							{
								enemyStatusRoot.gameObject.SetActive(false);
							}
							base.selfController.enabled = false;
							tutorialManager.legendDragon.SetActive(false);
							MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, new Vector3(0f, 0f, 0f), 0f, ENEMY_ID, ENEMY_LV, true, true, true, false, delegate(Enemy e)
							{
								tutorialManager.boss = e;
								boss = e;
								bossController = boss.GetComponent<EnemyController>();
								bossController.enabled = true;
								tutorialManager.director.StartBattleStartDirection(e, base.character, delegate
								{
									Player player = base.character as Player;
									SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(0);
									skillParam.useGaugeCounter = 0f;
									MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
									base.selfController.enabled = true;
									if ((UnityEngine.Object)playerStatusRoot != (UnityEngine.Object)null)
									{
										playerStatusRoot.gameObject.SetActive(true);
										UIPanel component = playerStatusRoot.GetComponent<UIPanel>();
										component.alpha = 0f;
										TweenAlpha tweenAlpha = TweenAlpha.Begin(playerStatusRoot.gameObject, 0.3f, 1f);
										UISkillButton skillButton = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetUISkillButton(2);
										skillButton.ReleaseEffects();
										skillButton.gameObject.SetActive(false);
										tweenAlpha.AddOnFinished(delegate
										{
											skillButton.gameObject.SetActive(true);
										});
									}
									if ((UnityEngine.Object)enemyStatusRoot != (UnityEngine.Object)null)
									{
										enemyStatusRoot.gameObject.SetActive(true);
										UIWidget component2 = enemyStatusRoot.GetComponent<UIWidget>();
										component2.alpha = 0f;
										TweenAlpha.Begin(enemyStatusRoot.gameObject, 0.3f, 1f);
									}
									boss.setPause(false);
									bossController.enabled = true;
									currentPhase = Phase.SOLO_BATTLE;
								});
							});
							currentPhase = Phase.NONE;
						}
					}
				}
			}
		}

		private void WalkingToPlayer()
		{
			if ((base.character._position - boss._position).sqrMagnitude < DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR)
			{
				boss.setPause(true);
				bossController.enabled = false;
				boss.regionWorks[1].weakState = Enemy.WEAK_STATE.WEAK;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				timer = 0f;
				currentPhase = Phase.WAIT_DISP_WEAK_MARK;
			}
		}

		private void WaitDispWeakMark()
		{
			timer += Time.deltaTime;
			if (1.4f < timer)
			{
				timer = 0f;
				tutorialManager.helper.bossHelper.ShowHelpPicture0();
				currentPhase = Phase.WAIT_HELP_PICTURE_0;
			}
		}

		private void WaitHelpPicture0()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				currentPhase = Phase.NONE;
				tutorialManager.helper.bossHelper.HideHelpPicture0(delegate
				{
					timer = 0f;
					tutorialManager.helper.bossHelper.ShowHelpPicture1();
					currentPhase = Phase.WAIT_HELP_PICTURE_1;
				});
			}
		}

		private void WaitHelpPicture1()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				currentPhase = Phase.NONE;
				tutorialManager.helper.bossHelper.HideHelpPicture1(delegate
				{
					timer = 0f;
					boss.setPause(false);
					bossController.enabled = true;
					base.selfController.enabled = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					currentPhase = Phase.SOLO_BATTLE;
				});
			}
		}

		private void SoloBattle()
		{
			timer += Time.deltaTime;
			boss._downMax = 1;
			if (boss.actionID == (Character.ACTION_ID)13 || SOLO_BATTLE_TIME_LIMIT < timer)
			{
				timer = 0f;
				boss._downMax = 800;
				Player player = base.character as Player;
				SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(0);
				skillParam.useGaugeCounter = (float)(int)skillParam.GetMaxGaugeValue();
				Transform transform = Utility.FindChild(MonoBehaviourSingleton<UIManager>.I.uiCamera.transform, "Skill01");
				if ((UnityEngine.Object)null != (UnityEngine.Object)transform)
				{
					UIButton[] componentsInChildren = transform.gameObject.GetComponentsInChildren<UIButton>();
					if (0 < componentsInChildren.Length)
					{
						cursorTop = TutorialMessage.AttachCursor(componentsInChildren[0].transform, null);
					}
				}
				currentPhase = Phase.PLAYER_CONTROL_SKILL;
			}
		}

		private void WaitDown()
		{
			timer += Time.deltaTime;
			if (!(timer < 1f) && MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid() && !MonoBehaviourSingleton<UIInGamePopupDialog>.I.isOpenDialog)
			{
				timer = 0f;
				boss.setPause(true);
				bossController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				base.selfController.enabled = false;
				tutorialManager.helper.bossHelper.ShowHelpPicture2();
				currentPhase = Phase.WAIT_SKILL_HELP_PICTURE_0;
			}
		}

		private void WaitSkillHelpPicture0()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.helper.bossHelper.HideHelpPicture2(delegate
				{
					tutorialManager.helper.bossHelper.ShowHelpPicture3();
					currentPhase = Phase.WAIT_SKILL_HELP_PICTURE_1;
				});
			}
		}

		private void WaitSkillHelpPicture1()
		{
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				timer = 0f;
				currentPhase = Phase.NONE;
				tutorialManager.helper.bossHelper.HideHelpPicture3(delegate
				{
					Player player = base.character as Player;
					SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(0);
					skillParam.useGaugeCounter = (float)(int)skillParam.GetMaxGaugeValue();
					boss.setPause(false);
					bossController.enabled = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_CONTROL_SKILL;
				});
			}
		}

		private void PlayerControlSkill()
		{
			timer += Time.deltaTime;
			if (base.character.actionID == (Character.ACTION_ID)21 || SKILL_WAIT_LIMIT_TIME < timer)
			{
				boss.enemyLevel = 0;
				Vector3 position = boss._transform.position;
				if (IsEscapeBossHp)
				{
					currentPhase = Phase.ENEMY_ESCAPE_BATTLE;
				}
				else
				{
					GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
					StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
					extentionInfo.npcDataID = 990;
					extentionInfo.npcLv = 0;
					extentionInfo.npcLvIndex = 0;
					CreatePlayer(990, extentionInfo, position + new Vector3(5f, 0f, 0f), 1f, sectionData.GetText("STR_TUTORIAL_FIREND_0"), 0);
					StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo2 = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
					extentionInfo2.npcDataID = 991;
					extentionInfo2.npcLv = 0;
					extentionInfo2.npcLvIndex = 1;
					CreatePlayer(991, extentionInfo2, position + new Vector3(0f, 0f, 5f), 3.5f, sectionData.GetText("STR_TUTORIAL_FIREND_1"), 0);
					StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo3 = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
					extentionInfo3.npcDataID = 992;
					extentionInfo3.npcLv = 0;
					extentionInfo3.npcLvIndex = 2;
					CreatePlayer(992, extentionInfo3, position + new Vector3(-5f, 0f, 0f), 7f, null, TUTORIAL_STAMP_ID);
					if ((UnityEngine.Object)null != (UnityEngine.Object)cursorTop)
					{
						TutorialMessage.DetachCursor(cursorTop, true);
						UISkillButton componentInChildren = cursorTop.gameObject.GetComponentInChildren<UISkillButton>();
						if ((UnityEngine.Object)null != (UnityEngine.Object)componentInChildren)
						{
							Transform transform = componentInChildren.GetCoolTimeGauge().transform;
							Vector3 localPosition = transform.localPosition;
							localPosition.z = 0f;
							transform.localPosition = localPosition;
						}
					}
					currentPhase = Phase.BATTLE_WITH_FRIENDS;
				}
				timer = 0f;
			}
		}

		private void BattleWithFriends()
		{
			bool flag = false;
			if (IsCompleteLoadNPC)
			{
				flag = IsEscapeBossHp;
			}
			timer += Time.deltaTime;
			if (BATTLE_WITH_FRIEND_TIME < timer || flag)
			{
				currentPhase = Phase.ENEMY_ESCAPE_BATTLE;
			}
		}

		private void CreatePlayer(int npcId, StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos, float delay, string message, int stampId = 0)
		{
			tutorialManager.StartCoroutine(CreatePlayerImpl(npcId, extention_info, pos, delay, message, stampId));
		}

		private IEnumerator CreatePlayerImpl(int npcId, StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos, float delay, string message, int stampId)
		{
			yield return (object)new WaitForSeconds(delay);
			Player npc = MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(npcId, extention_info, pos, 0f, null, null);
			npc.transform.position = pos;
			GameObject appearEffect = tutorialManager.appearEffect;
			appearEffect.transform.position = pos;
			LoadingQueue loadingQueue = new LoadingQueue(npc);
			int voice_id = 0;
			switch (npcId)
			{
			case 990:
				voice_id = ChooseRandom(ENTRY_VOICE_PATTERN_1, 0f);
				break;
			case 991:
				voice_id = ChooseRandom(ENTRY_VOICE_PATTERN_2, 0f);
				break;
			case 992:
				voice_id = ChooseRandom(ENTRY_VOICE_PATTERN_3, 0f);
				break;
			}
			loadingQueue.CacheActionVoice(voice_id, null);
			if (loadingQueue.IsLoading())
			{
				yield return (object)loadingQueue.Wait();
			}
			yield return (object)new WaitForSeconds(1f);
			if ((UnityEngine.Object)npc.uiPlayerStatusGizmo != (UnityEngine.Object)null)
			{
				if (!string.IsNullOrEmpty(message))
				{
					npc.uiPlayerStatusGizmo.SayChat(message);
				}
				else if (stampId != 0)
				{
					npc.uiPlayerStatusGizmo.SayChatStamp(stampId);
				}
				SoundManager.PlayActionVoice(voice_id, 1f, 0u, null, null);
			}
			m_npcLoadCompleteBitFlag |= 1 << extention_info.npcLvIndex;
		}

		private int ChooseRandom(int[] items, float rejectRate = 0f)
		{
			if (items == null)
			{
				return 0;
			}
			float num = UnityEngine.Random.Range(0f, 1f);
			if (num < rejectRate)
			{
				return 0;
			}
			int num2 = UnityEngine.Random.Range(0, items.Length);
			return items[num2];
		}

		private void EnemyEscapeBattle()
		{
			if (base.character.actionID != (Character.ACTION_ID)21)
			{
				if (!isTrackDragonFight)
				{
					isTrackDragonFight = true;
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_dragon_fight, "Tutorial");
				}
				currentPhase = Phase.NONE;
				if ((UnityEngine.Object)playerStatusRoot != (UnityEngine.Object)null)
				{
					playerStatusRoot.gameObject.SetActive(false);
				}
				if ((UnityEngine.Object)enemyStatusRoot != (UnityEngine.Object)null)
				{
					enemyStatusRoot.gameObject.SetActive(false);
				}
				MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
				InGameMain inGameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
				Transform transform = inGameMain._transform.FindChild("InGameMain/DynamicPanel");
				if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
				{
					transform.gameObject.SetActive(false);
				}
				base.selfController.enabled = false;
				bossController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
				tutorialManager.director.StartBattleEndDirection(tutorialManager.legendDragon, tutorialManager.titleUIPrefab, delegate
				{
					currentPhase = Phase.DISP_TIELE;
					ResourceManager.autoRetry = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
				});
			}
		}

		private void DispTitle()
		{
			tutorialManager.Change(null);
			Protocol.Force(delegate
			{
				MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep(delegate(bool is_success)
				{
					if (is_success)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "CharaMake", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
						MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
					}
				});
			});
		}

		public override void Final()
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_dragon_fight, "Tutorial");
		}
	}

	public const int BATTLE_END_STORY_ID = 11000001;

	public const int CHARAMAKE_END_STORY_ID = 11000002;

	public float TutorialMoveTime;

	public static readonly float DURATION_DISP_DIALOG = 2f;

	public static readonly float DURATION_DISP_COMPLETE = 2f;

	private static readonly float MAX_APEAR_POS_X = 23f;

	private static readonly float MAX_APEAR_POS_Z = 23f;

	private List<EnemyHolder> poppedEnemies = new List<EnemyHolder>(10);

	private State current;

	private UnityEngine.Object dialogPrefab;

	private UnityEngine.Object helperPrefab;

	private UnityEngine.Object bossDirectorPrefab;

	private UITutorialDialog dialogWindow;

	private UITutorialOperationHelper helperUI;

	private TutorialBossDirector bossDirector;

	private Transform mdlArrow;

	private UnityEngine.Object legendDragonPrefab;

	private GameObject _legendDragon;

	private UnityEngine.Object appearEffectPrefab;

	private GameObject _appearEffect;

	public Enemy boss
	{
		get;
		set;
	}

	public UITutorialDialog dialog
	{
		get
		{
			if ((UnityEngine.Object)dialogWindow == (UnityEngine.Object)null)
			{
				dialogWindow = ResourceUtility.Realizes(dialogPrefab, -1).GetComponent<UITutorialDialog>();
				dialogWindow.Close(0, null);
			}
			return dialogWindow;
		}
	}

	public UITutorialOperationHelper helper
	{
		get
		{
			if ((UnityEngine.Object)helperUI == (UnityEngine.Object)null)
			{
				helperUI = ResourceUtility.Realizes(helperPrefab, -1).GetComponent<UITutorialOperationHelper>();
			}
			return helperUI;
		}
	}

	public TutorialBossDirector director
	{
		get
		{
			if ((UnityEngine.Object)bossDirector == (UnityEngine.Object)null)
			{
				bossDirector = ResourceUtility.Realizes(bossDirectorPrefab, -1).GetComponent<TutorialBossDirector>();
			}
			return bossDirector;
		}
	}

	public GameObject legendDragon
	{
		get
		{
			if ((UnityEngine.Object)_legendDragon == (UnityEngine.Object)null)
			{
				_legendDragon = ResourceUtility.Realizes(legendDragonPrefab, -1).gameObject;
			}
			return _legendDragon;
		}
	}

	public GameObject titleUIPrefab
	{
		get;
		set;
	}

	public UnityEngine.Object targetAreaPrefab
	{
		get;
		set;
	}

	public GameObject appearEffect
	{
		get
		{
			if ((UnityEngine.Object)_appearEffect == (UnityEngine.Object)null)
			{
				_appearEffect = ResourceUtility.Realizes(appearEffectPrefab, -1).gameObject;
			}
			return _appearEffect;
		}
	}

	public bool isLoadingSE
	{
		get;
		private set;
	}

	public bool isCompleteLoadSE
	{
		get;
		private set;
	}

	private IEnumerator Start()
	{
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, true);
		while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			yield return (object)null;
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedDialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialDialog", false);
		LoadObject loadedTargetArea = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01", false);
		LoadObject loadedHelper = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialOperationHelper", false);
		LoadObject loadedDirector = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector", false);
		LoadObject loadedDragon = loadingQueue.Load(RESOURCE_CATEGORY.ENEMY_MODEL, "ENM01_Legend", false);
		LoadObject loadedArrow = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
		{
			"mdl_arrow_01"
		}, false);
		LoadObject loadedAppear = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_battle_start_01", false);
		MonoBehaviourSingleton<UIManager>.I.LoadUI(false, false, true);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_COMPLETE, null);
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		dialogPrefab = loadedDialog.loadedObject;
		targetAreaPrefab = loadedTargetArea.loadedObject;
		helperPrefab = loadedHelper.loadedObject;
		bossDirectorPrefab = loadedDirector.loadedObject;
		legendDragonPrefab = loadedDragon.loadedObject;
		appearEffectPrefab = loadedAppear.loadedObject;
		while (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.gameObject.SetActive(false);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.gameObject.SetActive(false);
		}
		InGameMain inGameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
		if ((UnityEngine.Object)inGameMain != (UnityEngine.Object)null)
		{
			Transform t3 = inGameMain._transform.FindChild("InGameMain/PlayerStatus");
			if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
			{
				t3.gameObject.SetActive(false);
			}
			t3 = inGameMain._transform.FindChild("InGameMain/StaticSwitchPanel/NewMenuParent");
			if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
			{
				t3.gameObject.SetActive(false);
			}
			t3 = inGameMain._transform.FindChild("InGameMain/StaticSwitchPanel/ChatButtonParent");
			if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
			{
				t3.gameObject.SetActive(false);
			}
		}
		List<PortalObject> list = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
		for (int i = 0; i < list.Count; i++)
		{
		}
		Vector3 ARROW_OFFSET = new Vector3(0f, 2.6f, 0f);
		Vector3 ARROW_SCALE = new Vector3(2.5f, 2.5f, 2.5f);
		mdlArrow = Utility.CreateGameObject("MdlArrow", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		ResourceUtility.Realizes(loadedArrow.loadedObject, mdlArrow, -1);
		mdlArrow.localScale = ARROW_SCALE;
		mdlArrow.position = list[0]._transform.position + ARROW_OFFSET;
		mdlArrow.gameObject.SetActive(false);
		Change(new TutorialMove(this));
		isCompleteLoadSE = false;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, false);
		ResourceManager.autoRetry = true;
	}

	public void UpdateArrowModel()
	{
		if (!((UnityEngine.Object)null == (UnityEngine.Object)mdlArrow))
		{
			float num = 2.5f;
			float num2 = 10f;
			Vector3 position = mdlArrow.position;
			Vector3 position2 = MonoBehaviourSingleton<AppMain>.I.mainCamera.transform.position;
			float num3 = num;
			float num4 = Vector3.Distance(position, position2);
			num3 = ((!(num2 > num4)) ? (num + 0.05f * (num4 - num)) : num);
			mdlArrow.localScale = new Vector3(num3, num3, num3);
		}
	}

	private void Update()
	{
		if (current != null)
		{
			current.Update();
		}
	}

	private void OnDestroy()
	{
		ResourceManager.autoRetry = false;
		if ((UnityEngine.Object)helper != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(helper.gameObject);
		}
		if ((UnityEngine.Object)dialog != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(dialog.gameObject);
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)mdlArrow)
		{
			UnityEngine.Object.Destroy(mdlArrow.gameObject);
		}
	}

	public void ClearedPoppedEnemiesInfo()
	{
		poppedEnemies.Clear();
	}

	public Enemy PopEnemy(Vector3 pos, bool setAI = true, float xMax = -1f, float zMax = -1f)
	{
		FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, 0);
		if (enemyPopData == null)
		{
			return null;
		}
		int num = (int)enemyPopData.enemyID;
		int enemy_lv = (int)enemyPopData.enemyLv;
		if (num == 0 && QuestManager.IsValidInGame())
		{
			num = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID();
			enemy_lv = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
		}
		if (0f < xMax)
		{
			pos.x = Mathf.Clamp(pos.x, 0f - xMax, xMax);
		}
		if (0f < zMax)
		{
			pos.z = Mathf.Clamp(pos.z, 0f - zMax, zMax);
		}
		if (pos.z < -29.5f)
		{
			pos.z = -29.5f;
		}
		Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, pos, 0f, num, enemy_lv, false, false, setAI, false, delegate(Enemy target)
		{
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
			}
			if (!setAI)
			{
				EnemyController component = target.GetComponent<EnemyController>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(component);
				}
			}
		});
		EnemyHolder enemyHolder = new EnemyHolder();
		enemyHolder.enemy = enemy;
		poppedEnemies.Add(enemyHolder);
		return enemy;
	}

	public void SetActiveAllEnemiesController(bool active)
	{
		for (int i = 0; i < poppedEnemies.Count; i++)
		{
			if (poppedEnemies[i].enemy.gameObject.activeInHierarchy)
			{
				poppedEnemies[i].enemy.enabled = active;
			}
		}
	}

	public bool CheckAllEnemiesDead()
	{
		for (int i = 0; i < poppedEnemies.Count; i++)
		{
			if (poppedEnemies[i].enemy.gameObject.activeInHierarchy)
			{
				return false;
			}
		}
		return true;
	}

	public void CheckAndCallOnDeadAll(Action<Enemy> onDead)
	{
		for (int i = 0; i < poppedEnemies.Count; i++)
		{
			poppedEnemies[i].CheckAndCallOndead(onDead);
		}
	}

	public T GetState<T>() where T : class
	{
		return current as T;
	}

	public void Change(State next)
	{
		if (current != null)
		{
			current.Final();
		}
		next?.Init();
		current = next;
	}

	private IEnumerator WaitForTime(float waitTime, Action action)
	{
		yield return (object)new WaitForSeconds(waitTime);
		action();
	}

	public void LoadSE()
	{
		isLoadingSE = true;
		StartCoroutine("DoLoadSE");
	}

	private IEnumerator DoLoadSE()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_01, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_LANDING, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_01, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_02, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_02, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_03, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_04, null);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_TITLELOGO, null);
		while (loadingQueue.IsLoading())
		{
			yield return (object)null;
		}
		isLoadingSE = false;
		isCompleteLoadSE = true;
	}
}
