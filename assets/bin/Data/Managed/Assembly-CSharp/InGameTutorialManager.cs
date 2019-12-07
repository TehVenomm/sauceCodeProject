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
				if (_selfController == null)
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
				if (_character == null)
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

		public virtual void FixedUpdate()
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
			Player obj = base.character as Player;
			obj.SetDiableAction(Character.ACTION_ID.MOVE, disable: false);
			obj.SetDiableAction(Character.ACTION_ID.ATTACK, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)33, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)19, disable: true);
			puniconManager = MonoBehaviourSingleton<PuniConManager>.I;
			targetAreaObject = ResourceUtility.Realizes(tutorialManager.targetAreaPrefab);
			targetAreaObject.position = TARGET_AREA_POSITION;
			targetAreaObject.gameObject.SetActive(value: false);
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
					if (null != component)
					{
						component.gameObject.SetActive(value: true);
						component.Play();
					}
					tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2.5f, delegate
					{
						tutorialManager.helper.moveHelper.HideHelpText(delegate
						{
							tutorialManager.helper.fingerMove.gameObject.SetActive(value: false);
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
				tutorialManager.helper.moveHelper.HideHelpText();
				UITweenCtrl component = tutorialManager.helper.fingerMove.GetComponent<UITweenCtrl>();
				if (null != component)
				{
					component.gameObject.SetActive(value: false);
				}
				tutorialManager.helper.commonHelper.ShowGoodJob();
				currentPhase = Phase.GG_DONE_TUTORIAL_MOVE;
				timer = 0f;
				return;
			}
			timer += Time.deltaTime;
			if (timer >= 1f)
			{
				currentPhase = Phase.NONE;
				tutorialManager.helper.moveHelper.ShowHelpText();
				UITweenCtrl ctrl = tutorialManager.helper.fingerMove.GetComponent<UITweenCtrl>();
				if (null != ctrl)
				{
					ctrl.gameObject.SetActive(value: true);
					ctrl.Reset();
					ctrl.Play(forward: true, delegate
					{
						tutorialManager.StartCoroutine(tutorialManager.WaitForTime(1f, delegate
						{
							tutorialManager.helper.moveHelper.HideHelpText(delegate
							{
								ctrl.gameObject.SetActive(value: false);
								currentPhase = Phase.GG_TUTORIAL_MOVE;
								timer = 0f;
							});
						}));
					});
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
				touchInfo.beginPosition = new Vector2(Screen.width / 2, Screen.height / 4);
				touchInfo.position = touchInfo.beginPosition;
				puniconManager.OnTouchOn(touchInfo);
				finger = tutorialManager.helper.commonHelper.ShowFinger();
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
				base.character.ActIdle();
				puniconManager.OnTouchOff(touchInfo);
				tutorialManager.helper.commonHelper.HideFinger();
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.moveHelper.HideHelpText(delegate
				{
					targetAreaObject.gameObject.SetActive(value: true);
					tutorialManager.helper.moveHelper.ShowHelpPicture();
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
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
			if ((base.character._transform.position - TARGET_AREA_POSITION).sqrMagnitude < TARGET_AREA_RANGE * TARGET_AREA_RANGE)
			{
				if (targetAreaObject != null)
				{
					EffectManager.ReleaseEffect(targetAreaObject.gameObject);
					targetAreaObject = null;
				}
				currentPhase = Phase.NONE;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
				tutorialManager.helper.moveHelper.HideHelpPicture();
				tutorialManager.helper.fingerMove.gameObject.SetActive(value: false);
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
				if (null != component)
				{
					component.gameObject.SetActive(value: true);
					component.Play();
				}
			}
		}

		private void ShowDialogRolling()
		{
			timer += Time.deltaTime;
			if (MOVE_TRAINING_TIME < timer)
			{
				tutorialManager.helper.avoidHelper.HideHelpText();
				currentPhase = Phase.FINISH_AND_WAIT_DIALOG;
				tutorialManager.Change(new TutorialBattle(tutorialManager));
				timer = 0f;
				tutorialManager.helper.fingerRolling.gameObject.SetActive(value: false);
			}
		}

		public override void Final()
		{
			if (targetAreaObject != null)
			{
				EffectManager.ReleaseEffect(targetAreaObject.gameObject);
				targetAreaObject = null;
			}
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
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
			base.character.ActMoveVelocity(parameter.enableRootMotion ? Vector3.zero : vector, parameter.moveForwardSpeed, motion_id);
			base.character.SetLerpRotation(vector);
		}

		private void Rotate(ref Vector2 v, float degrees)
		{
			float num = Mathf.Sin(degrees * ((float)Math.PI / 180f));
			float num2 = Mathf.Cos(degrees * ((float)Math.PI / 180f));
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

		public void AddRollingCount()
		{
			playerRollingNum++;
		}

		public TutorialRolling(InGameTutorialManager owner)
			: base(owner)
		{
		}

		public override void Init()
		{
			Player obj = base.character as Player;
			obj.SetDiableAction(Character.ACTION_ID.MOVE, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.ATTACK, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.MAX, disable: false);
			obj.SetDiableAction((Character.ACTION_ID)33, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)19, disable: true);
			tutorialManager.dialog.Open(0, "Tutorial_Avoidance_Text_0101", 0, "Tutorial_Avoidance_Text_0102");
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
				tutorialManager.dialog.Close(1);
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
			if (finger != null)
			{
				Vector3 a = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.25f, 0f);
				Vector3 b = new Vector3((float)Screen.width * 0.75f, (float)Screen.height * 0.25f, 0f);
				finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(a, b, timer / AUTO_CONTROL_AVOID_TIME));
			}
			if (AUTO_CONTROL_AVOID_TIME < timer)
			{
				timer = 0f;
				tutorialManager.helper.commonHelper.HideFinger();
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
			if (finger != null)
			{
				Vector3 a = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.25f, 0f);
				Vector3 b = new Vector3((float)Screen.width * 0.25f, (float)Screen.height * 0.25f, 0f);
				finger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(Vector3.Lerp(a, b, timer / AUTO_CONTROL_AVOID_TIME));
			}
			if (AUTO_CONTROL_AVOID_TIME < timer)
			{
				timer = 0f;
				tutorialManager.helper.commonHelper.HideFinger();
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
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
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
			Player obj = base.character as Player;
			obj.SetDiableAction(Character.ACTION_ID.MOVE, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.ATTACK, disable: false);
			obj.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)33, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)19, disable: true);
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
				tutorialManager.dialog.Close(1);
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
			tutorialManager.helper.commonHelper.ShowTapIcon();
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
				tutorialManager.helper.commonHelper.HideTapFinger();
				tutorialManager.helper.attackHelper.HideHelpText(delegate
				{
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0203");
					timer = 0f;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
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
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
				tutorialManager.helper.commonHelper.HideTapFinger();
				tutorialManager.helper.attackHelper.HideHelpText();
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
				tutorialManager.helper.commonHelper.ShowTapIcon();
				tutorialManager.helper.commonHelper.ShowTapIcon(1);
				tutorialManager.helper.commonHelper.ShowTapIcon(2);
				tapFinger = tutorialManager.helper.commonHelper.ShowTapFinger();
				tapFinger.cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 4f, 0f));
				tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2f, delegate
				{
					tutorialManager.helper.attackHelper.HideHelpText(delegate
					{
						tutorialManager.helper.commonHelper.HideTapIcon();
						tutorialManager.helper.commonHelper.HideTapFinger();
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
				tutorialManager.dialog.Close(1);
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
				tutorialManager.helper.commonHelper.ShowTapIcon();
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
			if (base.character.IsPlayingMotion(17))
			{
				currentPhase = Phase.WAIT_AUTO_CONTOROL_COMBO;
			}
		}

		private void WaitAutoControlCombo()
		{
			if (base.character.actionID != Character.ACTION_ID.ATTACK)
			{
				tutorialManager.helper.commonHelper.HideTapFinger();
				tutorialManager.helper.commonHelper.HideTapIcon();
				currentPhase = Phase.NONE;
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				tutorialManager.helper.attackHelper.HideComboHelpText(delegate
				{
					tutorialManager.PopEnemy(base.character._transform.position + base.character._transform.forward * ENEMY_SPAWN_DISTANCE, setAI: false);
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 0, "Tutorial_Attack_Text_0208");
					timer = 0f;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_COMBO_TRAINING;
				});
			}
		}

		private void PlayerComboTraining()
		{
			if (base.character.IsPlayingMotion(17))
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
				tutorialManager.PopEnemy(base.character._transform.position + base.character._transform.forward * ENEMY_SPAWN_DISTANCE, setAI: false);
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
			Player obj = base.character as Player;
			obj.SetDiableAction(Character.ACTION_ID.MOVE, disable: false);
			obj.SetDiableAction(Character.ACTION_ID.ATTACK, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)33, disable: false);
			obj.SetDiableAction((Character.ACTION_ID)19, disable: false);
			tutorialManager.dialog.Open(1, "Tutorial_Special_Text_0401", 1, "Tutorial_Special_Text_0402");
			base.selfController.enabled = false;
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
				tutorialManager.helper.commonHelper.ShowLongTapFinger().cachedTransform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 4f, 0f));
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
				base.character.ActIdle();
				tutorialManager.helper.commonHelper.HideLongTapFinger();
				tutorialManager.helper.commonHelper.HideAutoControlMark();
				currentPhase = Phase.NONE;
				tutorialManager.helper.guardHelper.HideHelpText(delegate
				{
					tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0003", 1, "Tutorial_Special_Text_0405", 1, "Tutorial_Special_Text_0404");
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
					base.selfController.enabled = true;
					currentPhase = Phase.PLAYER_GUAD_TRAINING;
				});
			}
		}

		private void PlayerGuardTraining()
		{
			Player player = base.character as Player;
			if (player.isGuardWalk || base.character.actionID == (Character.ACTION_ID)19)
			{
				player.SetDiableAction(Character.ACTION_ID.MOVE, disable: false);
				timer += Time.deltaTime;
			}
			else
			{
				player.SetDiableAction(Character.ACTION_ID.MOVE, disable: true);
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
			Player obj = base.character as Player;
			obj.SetDiableAction(Character.ACTION_ID.MOVE, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.ATTACK, disable: true);
			obj.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)33, disable: true);
			obj.SetDiableAction((Character.ACTION_ID)19, disable: true);
			tutorialManager.CheckAndCallOnDeadAll(null);
			tutorialManager.helper.moveHelper.HideHelpText();
			currentPhase = Phase.WAIT_DISP_FIRST_DIALOG;
			timer = 0f;
		}

		public override void Update()
		{
			if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID == 10000101)
			{
				tutorialManager.dialog.Close(1);
				tutorialManager.Change(new TutorialBossBattle(tutorialManager));
				return;
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self != null && self.hp < self.hpMax / 2)
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
				if (null != component)
				{
					component.gameObject.SetActive(value: true);
					component.Play();
				}
				tutorialManager.StartCoroutine(tutorialManager.WaitForTime(2.5f, delegate
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(value: false);
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
				tutorialManager.helper.attackHelper.HideHelpPicture();
				if (tutorialManager.helper.fingerAttack.gameObject.activeSelf)
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(value: false);
				}
			}
			tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			if (tutorialManager.CheckAllEnemiesDead() && DISP_FINGER_ATTACK_TIME < timer)
			{
				tutorialManager.helper.attackHelper.HideHelpPicture();
				if (tutorialManager.helper.fingerAttack.gameObject.activeSelf)
				{
					tutorialManager.helper.fingerAttack.gameObject.SetActive(value: false);
				}
				currentPhase = Phase.WAIT_PORTAL_EFFECT;
				timer = 0f;
			}
		}

		private void GGPlayerBattleTraining()
		{
			tutorialManager.CheckAndCallOnDeadAll(CreatePortalPoint);
			if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count <= 0)
			{
				return;
			}
			PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0];
			if (!createdPNC)
			{
				createdPNC = true;
				Player player = base.character as Player;
				player.SetDiableAction(Character.ACTION_ID.MOVE, disable: false);
				player.SetDiableAction(Character.ACTION_ID.ATTACK, disable: false);
				player.SetDiableAction(Character.ACTION_ID.MAX, disable: false);
				player.SetDiableAction((Character.ACTION_ID)33, disable: false);
				player.SetDiableAction((Character.ACTION_ID)19, disable: false);
				Vector3 a = Vector3.zero;
				for (int i = 0; i < 1; i++)
				{
					float num = UnityEngine.Random.Range(0f, 360f);
					float num2 = 2.5f;
					tutorialManager.PopEnemy(new Vector3(player.positionXZ.x, 0f, player.positionXZ.y) + new Vector3(num2 * Mathf.Cos(num * ((float)Math.PI / 180f)), 0f, num2 * Mathf.Sin(num * ((float)Math.PI / 180f))), setAI: false);
					a = new Vector3(player.positionXZ.x, 0f, player.positionXZ.y) + new Vector3(num2 * Mathf.Cos(num * ((float)Math.PI / 180f)), 0f, num2 * Mathf.Sin(num * ((float)Math.PI / 180f)));
				}
				for (int j = 0; j < 1; j++)
				{
					float num3 = UnityEngine.Random.Range(0f, 360f);
					float num4 = 4f;
					tutorialManager.PopEnemy(new Vector3(player.positionXZ.x, 0f, player.positionXZ.y) + new Vector3(num4 * Mathf.Cos(num3 * ((float)Math.PI / 180f)), 0f, num4 * Mathf.Sin(num3 * ((float)Math.PI / 180f))), setAI: false);
				}
				for (int k = 0; k < 1; k++)
				{
					float num5 = UnityEngine.Random.Range(0f, 360f);
					float num6 = 5f;
					tutorialManager.PopEnemy(new Vector3(player.positionXZ.x, 0f, player.positionXZ.y) + new Vector3(num6 * Mathf.Cos(num5 * ((float)Math.PI / 180f)), 0f, num6 * Mathf.Sin(num5 * ((float)Math.PI / 180f))), setAI: false);
				}
				for (int l = 0; l < 10; l++)
				{
					float num7 = UnityEngine.Random.Range(0f, 360f);
					float num8 = UnityEngine.Random.Range(4f, 26f);
					tutorialManager.PopEnemy(new Vector3(num8 * Mathf.Cos(num7 * ((float)Math.PI / 180f)), 0f, num8 * Mathf.Sin(num7 * ((float)Math.PI / 180f))), setAI: false);
				}
				Vector3 b = new Vector3(0f, 1.6f, 0f);
				Vector3 localScale = new Vector3(1.5f, 1.5f, 1.5f);
				tutorialManager.mdlArrow.localScale = localScale;
				tutorialManager.mdlArrow.position = a + b;
				tutorialManager.mdlArrow.gameObject.SetActive(value: true);
				tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
			}
			if (currentPortalPoint != portalObject.nowPoint && !tutorialManager.dialog.isThreeLineLabel2Active())
			{
				currentPortalPoint = portalObject.nowPoint;
				tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
			}
			else if (currentPortalPoint != portalObject.nowPoint && portalObject.isFull && tutorialManager.dialog.isThreeLineLabel2Active())
			{
				currentPortalPoint = portalObject.nowPoint;
				tutorialManager.helper.commonHelper.ShowEnemyCount(portalObject.nowPoint);
				tutorialManager.dialog.HideThreeLineLabel2();
			}
			if (portalObject.isFull && !tutorialManager.helper.commonHelper.OnShowEnemyCount && tutorialManager.helper.commonHelper.CurrentEnemyShow > 0)
			{
				Debug.Log("Current Enemy Show: " + tutorialManager.helper.commonHelper.CurrentEnemyShow);
				tutorialManager.helper.commonHelper.HideEnemyCount();
				tutorialManager.helper.commonHelper.ShowSplendid();
				currentPhase = Phase.WAIT_PORTAL_EFFECT;
				tutorialManager.dialog.HideThreeLineLabel2();
				timer = 0f;
			}
			else if (portalObject.isFull && !tutorialManager.helper.commonHelper.OnShowEnemyCount && tutorialManager.helper.commonHelper.CurrentEnemyShow == 0)
			{
				timer += Time.deltaTime;
				if (timer > 2f)
				{
					Debug.Log("Current Enemy Show: " + tutorialManager.helper.commonHelper.CurrentEnemyShow);
					tutorialManager.helper.commonHelper.HideEnemyCount();
					tutorialManager.helper.commonHelper.ShowSplendid();
					currentPhase = Phase.WAIT_PORTAL_EFFECT;
					tutorialManager.dialog.HideThreeLineLabel2();
					timer = 0f;
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
					tutorialManager.PopEnemy(array[i], setAI: true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
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
					portalObjectList[0].gameObject.SetActive(value: true);
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
						tutorialManager.PopEnemy(array[i], setAI: true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
					}
					tutorialManager.SetActiveAllEnemiesController(active: false);
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
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
					tutorialManager.SetActiveAllEnemiesController(active: true);
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
						tutorialManager.PopEnemy(array[i], setAI: true, MAX_APEAR_POS_X, MAX_APEAR_POS_Z);
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
				tutorialManager.dialog.Close(1);
			}
			if (tutorialManager.CheckAllEnemiesDead())
			{
				tutorialManager.dialog.Close(1);
				MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
				cameraPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
				base.selfController.enabled = true;
				currentPhase = Phase.WAIT_DISP_LAST_DIALOG;
				Vector3 localScale = new Vector3(2.5f, 2.5f, 2.5f);
				Vector3 b = new Vector3(0f, 2.6f, 0f);
				tutorialManager.mdlArrow.localScale = localScale;
				tutorialManager.mdlArrow.position = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[0]._transform.position + b;
				tutorialManager.mdlArrow.gameObject.SetActive(value: true);
			}
		}

		private void WaitDispLastDialog()
		{
			tutorialManager.UpdateArrowModel();
			timer += Time.deltaTime;
			if (DURATION_DISP_DIALOG < timer)
			{
				tutorialManager.dialog.Close(1);
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
			coop_Model_EnemyDefeat.x = (int)enemy._transform.position.x;
			coop_Model_EnemyDefeat.z = (int)enemy._transform.position.z;
			if (tutorialManager.mdlArrow.gameObject.transform.position.x == enemy._transform.position.x && tutorialManager.mdlArrow.gameObject.transform.position.z == enemy._transform.position.z)
			{
				tutorialManager.mdlArrow.gameObject.SetActive(value: false);
			}
			FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
			MonoBehaviourSingleton<FieldManager>.I.AddPortalPointToPortalInfo(coop_Model_EnemyDefeat.ppt);
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

		private readonly int ENEMY_ID = 110010911;

		private readonly int ENEMY_LV = 1;

		private readonly float SOLO_BATTLE_TIME_LIMIT = 30f;

		private readonly float SKILL_WAIT_LIMIT_TIME = 25f;

		private readonly float BATTLE_WITH_FRIEND_TIME = 35f;

		private readonly float BOSS_MIN_HP_RATE = 0.2f;

		private readonly float BOSS_ESCAPE_HP_RATE = 0.5f;

		private readonly float BOSS_REFILL_HP_RATE = 0.3f;

		private Phase currentPhase = Phase.WAIT_SCENE_LOADING;

		private Enemy boss;

		private EnemyController bossController;

		private Player playerOwn;

		private float timer;

		private float newTipTimer;

		private bool isShowHelp;

		private float delayTimeNpcUseSkill;

		private int delayTimeNpcUseSkillOffset;

		private int bossMultiXHp;

		private Transform fakeCamera;

		private bool showPhase1BotCam;

		private bool initFakeCamera;

		private bool boolSkillNpc;

		private Transform playerStatusRoot;

		private Transform enemyStatusRoot;

		private readonly float DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR = 100f;

		private const int NPC_PLAYER_ID_0 = 991;

		private const int NPC_PLAYER_ID_1 = 990;

		private const int NPC_PLAYER_ID_2 = 992;

		private readonly int TUTORIAL_STAMP_ID = 1;

		private Transform cursorTop;

		private float showBotTimer;

		private bool lockCamBot;

		private float camFov;

		private float showBotCamFov = 104f;

		private Vector3 camPos = Vector3.zero;

		private Vector3 camRot = Vector3.zero;

		private Vector3 showBotCamPos = new Vector3(0f, 1.15f, -29f);

		private Vector3 showBotCamRot = new Vector3(-20.6f, 180f, 0f);

		private float countTimerForReSkill;

		private readonly int NPC_LOAD_COMPLETE_BIT = 7;

		private int m_npcLoadCompleteBitFlag;

		private NpcController npcUseSkill1;

		private NpcController npcUseSkill2;

		private NpcController npcUseSkill0;

		private Player[] listNpc = new Player[3];

		private StageObjectManager.CreatePlayerInfo.ExtentionInfo[] listNpcInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo[3];

		private Vector3[] initNPCPos = new Vector3[3];

		private static int ENTRY_VOICE_PATTERN_1 = 18;

		private static int ENTRY_VOICE_PATTERN_2 = 10014;

		private static int ENTRY_VOICE_PATTERN_3 = 14;

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
				delayTimeNpcUseSkill = tutorialParam.deplayTimeNpcUseSkill;
				delayTimeNpcUseSkillOffset = tutorialParam.deplayTimeNpcUseSkillOffset;
				bossMultiXHp = tutorialParam.bossMultiXHp;
			}
		}

		public override void Init()
		{
			fakeCamera = new GameObject().transform;
		}

		public override void Update()
		{
			if (boss != null)
			{
				boss.hpMax = 500000;
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self != null && self.hp < self.hpMax / 2)
				{
					self.hp = self.hpMax / 2;
				}
			}
			if (boss != null && boss.hpMax > 0)
			{
				int num = (int)((float)boss.hpMax * BOSS_REFILL_HP_RATE);
				if (boss.hp <= num)
				{
					boss.hp = boss.hpMax;
					if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnemyStatus>.I.SetHpMultiX(--bossMultiXHp);
						MonoBehaviourSingleton<UIEnemyStatus>.I.PlayShakeHpMultiX();
					}
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
			if (tutorialManager.dialog.isTwoLineGameObjectActive())
			{
				newTipTimer += Time.deltaTime;
			}
			if (newTipTimer >= SHOW_WELCOME_LOG_TIME && !isShowHelp)
			{
				tutorialManager.dialog.gameObject.SetActive(value: false);
				tutorialManager.helper.basicNewHelper.ShowHelpPicture(delegate
				{
					boss.setPauseWithAnim(pause: false);
					bossController.enabled = true;
					SetDisablePlayerControl(playerOwn, isDisable: false);
					timer = 0f;
				});
				newTipTimer = 0f;
				isShowHelp = true;
			}
		}

		private void DebugVector(string name, Vector3 v)
		{
		}

		public override void FixedUpdate()
		{
			if (lockCamBot)
			{
				if (!showPhase1BotCam)
				{
					showBotTimer += Time.deltaTime;
					if (!initFakeCamera)
					{
						camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
						camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
						fakeCamera.position = Vector3.LerpUnclamped(showBotCamPos, playerOwn.transform.position, SHOW_BOT_CAM_TIME_RATE + 1f);
						float y = Mathf.Lerp(camPos.y, SHOW_BOT_CAM_Y, SHOW_BOT_CAM_TIME_RATIO_PHASE1);
						fakeCamera.position = new Vector3(fakeCamera.position.x, y, fakeCamera.position.z);
						fakeCamera.LookAt(showBotCamPos);
						initFakeCamera = true;
					}
					Vector3 position = Vector3.Lerp(camPos, fakeCamera.position, showBotTimer / SHOW_BOT_CAM_TIME_RATIO_PHASE1);
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = position;
					float fieldOfView = Mathf.Lerp(camFov, SHOW_BOT_CAM_FOV_PHASE1, showBotTimer / SHOW_BOT_CAM_TIME_RATIO_PHASE1);
					MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = fieldOfView;
					Vector3 eulerAngles = camRot + learpAngle(camRot, fakeCamera.eulerAngles, showBotTimer / SHOW_BOT_CAM_TIME_RATIO_PHASE1);
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles = eulerAngles;
					if (showBotTimer > SHOW_BOT_CAM_TIME_RATIO_PHASE1)
					{
						showPhase1BotCam = true;
						showBotTimer = 0f;
						camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
						camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
						camFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
					}
				}
				if (showPhase1BotCam)
				{
					showBotTimer += Time.deltaTime;
					float num = showBotTimer / SHOW_BOT_CAM_TIME_RATIO_PHASE2;
					Vector3 position2 = Vector3.Lerp(camPos, showBotCamPos, num);
					Vector3 eulerAngles2 = camRot + learpAngle(camRot, showBotCamRot, num);
					float fieldOfView2 = Mathf.Lerp(camFov, showBotCamFov, num);
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = position2;
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles = eulerAngles2;
					MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = fieldOfView2;
					if (showBotTimer > SHOW_BOT_CAM_TIME)
					{
						MonoBehaviourSingleton<InGameCameraManager>.I.enabled = true;
						lockCamBot = false;
						timer = 0f;
					}
				}
			}
			if (boolSkillNpc)
			{
				if (showBotTimer <= SHOW_BOT_SKILL_TIME)
				{
					showBotTimer += Time.deltaTime;
					return;
				}
				boolSkillNpc = false;
				showBotTimer = 0f;
				MonoBehaviourSingleton<InGameCameraManager>.I.enabled = true;
				bossController.enabled = true;
			}
		}

		private void UpdateCamNpcUseSkill(Transform npcTransform)
		{
			if (!(npcTransform == null))
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
				Vector3 vector = npcTransform.position - boss.transform.position;
				Vector3 vector2 = npcTransform.position + vector.normalized * 3f;
				MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = new Vector3(vector2.x, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position.y, vector2.z);
				MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.LookAt(npcTransform);
				showBotTimer = 0f;
				boolSkillNpc = true;
				bossController.enabled = false;
			}
		}

		private Vector3 learpAngle(Vector3 source, Vector3 destination, float deltaTime)
		{
			float x = learpAngle(source.x, destination.x, deltaTime);
			float y = learpAngle(source.y, destination.y, deltaTime);
			float z = learpAngle(source.z, destination.z, deltaTime);
			DebugVector("learpAngle ", new Vector3(x, y, z));
			return new Vector3(x, y, z);
		}

		private float learpAngle(float source, float destination, float deltaTime)
		{
			float b = Mathf.DeltaAngle(source, destination);
			return Mathf.Lerp(0f, b, deltaTime);
		}

		private void WaitSceneLoading()
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				ResourceManager.autoRetry = false;
			}
			tutorialManager.UpdateArrowModel();
			if (MonoBehaviourSingleton<FieldManager>.I.currentPortalID != 10000101)
			{
				return;
			}
			if (null != tutorialManager.mdlArrow)
			{
				UnityEngine.Object.Destroy(tutorialManager.mdlArrow.gameObject);
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "InGameMain")
			{
				return;
			}
			if (!tutorialManager.isLoadingSE && !tutorialManager.isCompleteLoadSE)
			{
				tutorialManager.LoadSE();
			}
			if (!tutorialManager.isCompleteLoadSE || !MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().isInitialized)
			{
				return;
			}
			ResourceManager.autoRetry = true;
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
			InGameMain inGameMain = null;
			if (playerStatusRoot == null)
			{
				inGameMain = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain);
				if (inGameMain != null)
				{
					Transform transform = inGameMain._transform.Find("InGameMain/StaticSwitchPanel/NewMenuParent");
					if (transform != null)
					{
						transform.gameObject.SetActive(value: false);
					}
					playerStatusRoot = inGameMain._transform.Find("InGameMain/PlayerStatus");
					if (playerStatusRoot != null)
					{
						playerStatusRoot.gameObject.SetActive(value: false);
					}
					transform = inGameMain._transform.Find("InGameMain/StaticSwitchPanel/ChatButtonParent");
					if (transform != null)
					{
						transform.gameObject.SetActive(value: false);
					}
				}
			}
			if (base.character.actionID != (Character.ACTION_ID)23)
			{
				inGameMain = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain);
				if (inGameMain != null && enemyStatusRoot == null)
				{
					enemyStatusRoot = inGameMain._transform.Find("InGameMain/StaticPanel/EnemyStatus");
				}
				if (enemyStatusRoot != null)
				{
					enemyStatusRoot.gameObject.SetActive(value: false);
				}
				base.selfController.enabled = false;
				tutorialManager.legendDragon.SetActive(value: false);
				MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, new Vector3(0f, 0f, 0f), 0f, ENEMY_ID, ENEMY_LV, is_boss: true, is_big_monster: true, set_ai: true, willStock: false, delegate(Enemy e)
				{
					tutorialManager.boss = e;
					boss = e;
					e.SetImmortal();
					bossController = boss.GetComponent<EnemyController>();
					bossController.enabled = true;
					tutorialManager.director.StartBattleStartDirection(e, base.character, delegate
					{
						playerOwn = (base.character as Player);
						WaitForDispGreeting();
						playerOwn.skillInfo.GetSkillParam(0).useGaugeCounter = 0f;
						MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
						base.selfController.enabled = true;
						if (playerStatusRoot != null)
						{
							playerStatusRoot.gameObject.SetActive(value: true);
							playerStatusRoot.GetComponent<UIPanel>().alpha = 0f;
							TweenAlpha tweenAlpha = TweenAlpha.Begin(playerStatusRoot.gameObject, 0.3f, 1f);
							UISkillButton skillButton = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetUISkillButton(2);
							skillButton.ReleaseEffects();
							skillButton.gameObject.SetActive(value: false);
							tweenAlpha.AddOnFinished(delegate
							{
								skillButton.gameObject.SetActive(value: true);
							});
						}
						if (enemyStatusRoot != null)
						{
							enemyStatusRoot.gameObject.SetActive(value: true);
							enemyStatusRoot.GetComponent<UIWidget>().alpha = 0f;
							TweenAlpha.Begin(enemyStatusRoot.gameObject, 0.3f, 1f);
						}
						boss.setPauseWithAnim(pause: true);
						bossController.enabled = false;
						currentPhase = Phase.SOLO_BATTLE;
						MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start, "Tutorial");
						Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start.ToString());
						MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_3_battle_start, "Tutorial");
					});
					if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnemyStatus>.I.SetActiveTutorialObj(isActive: true);
						MonoBehaviourSingleton<UIEnemyStatus>.I.SetHpMultiX(bossMultiXHp);
					}
				}, isOverrideScale: true);
				LoadNpc();
				currentPhase = Phase.NONE;
			}
		}

		private static void SetDisablePlayerControl(Player player, bool isDisable)
		{
			if (player != null)
			{
				player.SetDiableAction(Character.ACTION_ID.MOVE, isDisable);
				player.SetDiableAction(Character.ACTION_ID.ATTACK, isDisable);
				player.SetDiableAction(Character.ACTION_ID.MAX, isDisable);
				player.SetDiableAction((Character.ACTION_ID)33, isDisable);
				player.SetDiableAction((Character.ACTION_ID)19, isDisable);
			}
		}

		private void WaitForDispGreeting()
		{
			tutorialManager.dialog.Open(0, "Tutorial_Move_Text_0001", 0, "Tutorial_Move_Text_0002");
			tutorialManager.dialog.OpenThreeLineLabel();
		}

		private void WalkingToPlayer()
		{
			if ((base.character._position - boss._position).sqrMagnitude < DISP_ENEMY_WEAK_DIALOG_RADIUS_SQR)
			{
				boss.setPause(pause: true);
				bossController.enabled = false;
				boss.regionWorks[1].weakState = Enemy.WEAK_STATE.WEAK;
				base.selfController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
					boss.setPause(pause: false);
					bossController.enabled = true;
					base.selfController.enabled = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
					currentPhase = Phase.SOLO_BATTLE;
				});
			}
		}

		private void SoloBattle()
		{
			timer += Time.deltaTime;
			boss._downMax = 1;
			if (cursorTop != null)
			{
				SkillInfo.SkillParam skillParam = (base.character as Player).skillInfo.GetSkillParam(0);
				Debug.Log("skillParam.useGaugeCounter" + skillParam.useGaugeCounter);
				if (skillParam.useGaugeCounter < (float)(int)skillParam.GetMaxGaugeValue())
				{
					Debug.Log("deattach");
					TutorialMessage.DetachCursor(cursorTop);
				}
			}
			if (timer > SKILL_WAIT_LIMIT_TIME && cursorTop == null)
			{
				SkillInfo.SkillParam skillParam2 = (base.character as Player).skillInfo.GetSkillParam(0);
				skillParam2.useGaugeCounter = (int)skillParam2.GetMaxGaugeValue();
				Transform transform = Utility.FindChild(MonoBehaviourSingleton<UIManager>.I.uiCamera.transform, "Skill01");
				if (null != transform)
				{
					UIButton[] componentsInChildren = transform.gameObject.GetComponentsInChildren<UIButton>();
					if (componentsInChildren.Length != 0)
					{
						cursorTop = TutorialMessage.AttachCursor(componentsInChildren[0].transform);
					}
				}
			}
			if (timer > SOLO_BATTLE_TIME_LIMIT)
			{
				timer = 0f;
				boss._downMax = 800;
				currentPhase = Phase.PLAYER_CONTROL_SKILL;
			}
		}

		private void WaitDown()
		{
			timer += Time.deltaTime;
			if (!(timer < 1f) && MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid() && !MonoBehaviourSingleton<UIInGamePopupDialog>.I.isOpenDialog)
			{
				timer = 0f;
				boss.setPause(pause: true);
				bossController.enabled = false;
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
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
		}

		private void PlayerControlSkill()
		{
			timer += Time.deltaTime;
			if (lockCamBot)
			{
				showBotTimer += Time.deltaTime;
			}
			if (cursorTop != null)
			{
				SkillInfo.SkillParam skillParam = (base.character as Player).skillInfo.GetSkillParam(0);
				if (skillParam.useGaugeCounter < (float)(int)skillParam.GetMaxGaugeValue())
				{
					TutorialMessage.DetachCursor(cursorTop);
				}
			}
			if (!(timer > SKILL_WAIT_LIMIT_TIME))
			{
				return;
			}
			boss.enemyLevel = 0;
			_ = boss._transform.position;
			_ = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
			ShowNpc();
			if (null != cursorTop)
			{
				TutorialMessage.DetachCursor(cursorTop);
				UISkillButton componentInChildren = cursorTop.gameObject.GetComponentInChildren<UISkillButton>();
				if (null != componentInChildren)
				{
					Transform transform = componentInChildren.GetCoolTimeGauge().transform;
					Vector3 localPosition = transform.localPosition;
					localPosition.z = 0f;
					transform.localPosition = localPosition;
				}
			}
			lockCamBot = true;
			MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
			camPos = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
			camRot = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.eulerAngles;
			camFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
			currentPhase = Phase.BATTLE_WITH_FRIENDS;
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear, "Tutorial");
			Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear.ToString());
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_4_battle_NPC_appear, "Tutorial");
			timer = 0f;
		}

		private void ShowNpc()
		{
			tutorialManager.StartCoroutine(IEShowNpc());
			tutorialManager.StartCoroutine(IEStopPose());
		}

		private IEnumerator IEShowNpc()
		{
			while (listNpc[0] == null || listNpc[1] == null || listNpc[2] == null)
			{
				yield return null;
			}
			tutorialManager.StartCoroutine(EnableNpc(3.5f, listNpc[0], listNpcInfo[0], initNPCPos[0]));
			tutorialManager.StartCoroutine(EnableNpc(2f, listNpc[1], listNpcInfo[1], initNPCPos[1]));
			tutorialManager.StartCoroutine(EnableNpc(2.5f, listNpc[2], listNpcInfo[2], initNPCPos[2]));
		}

		private void LoadNpc()
		{
			StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
			extentionInfo.npcDataID = 991;
			extentionInfo.npcLv = 0;
			extentionInfo.npcLvIndex = 1;
			CreatePlayer(991, extentionInfo, new Vector3(2f, 0f, -36f), 2f, null);
			StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo2 = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
			extentionInfo2.npcDataID = 990;
			extentionInfo2.npcLv = 0;
			extentionInfo2.npcLvIndex = 0;
			CreatePlayer(990, extentionInfo2, new Vector3(0f, 0f, -36f), 3.5f, null);
			StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo3 = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
			extentionInfo3.npcDataID = 992;
			extentionInfo3.npcLv = 0;
			extentionInfo3.npcLvIndex = 2;
			CreatePlayer(992, extentionInfo3, new Vector3(-2f, 0f, -36f), 2.5f, null);
		}

		private void BattleWithFriends()
		{
			if (IsCompleteLoadNPC)
			{
				_ = IsEscapeBossHp;
			}
			timer += Time.deltaTime;
			if (BATTLE_WITH_FRIEND_TIME < timer)
			{
				currentPhase = Phase.ENEMY_ESCAPE_BATTLE;
			}
			SkillInfo.SkillParam skillParam = (base.character as Player).skillInfo.GetSkillParam(0);
			if (skillParam.useGaugeCounter < (float)(int)skillParam.GetMaxGaugeValue())
			{
				countTimerForReSkill += Time.deltaTime;
				if (countTimerForReSkill > 6f)
				{
					countTimerForReSkill = 0f;
					skillParam.useGaugeCounter = (int)skillParam.GetMaxGaugeValue();
				}
			}
			if (timer > FIRST_SKILL_TIME_OFFSET && npcUseSkill1 != null)
			{
				npcUseSkill1.UseSkill();
				npcUseSkill1 = null;
			}
			if (timer > SECOND_SKILL_TIME && npcUseSkill0 != null)
			{
				npcUseSkill0.UseSkill();
				npcUseSkill0 = null;
			}
		}

		private void CreatePlayer(int npcId, StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos, float delay, string message, int stampId = 0)
		{
			CreatePlayerImpl(npcId, extention_info, pos, delay, message, stampId);
		}

		private void CreatePlayerImpl(int npcId, StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos, float delay, string message, int stampId)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.CreateNonPlayer(npcId, extention_info, pos, 0f);
			player.onTheGround = false;
			NpcController component = player.gameObject.GetComponent<NpcController>();
			player.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			player.SetDiableAction(Character.ACTION_ID.MOVE, disable: true);
			player.SetDiableAction(Character.ACTION_ID.ATTACK, disable: true);
			player.SetDiableAction(Character.ACTION_ID.MOVE_LOOKAT, disable: true);
			player.SetDiableAction(Character.ACTION_ID.MOVE_POINT, disable: true);
			player.SetDiableAction(Character.ACTION_ID.MAX, disable: true);
			player.transform.position = new Vector3(0f, 10000 + UnityEngine.Random.Range(0, 100), 0f);
			player.ActiveShadow(isActive: false);
			if (component != null)
			{
				component.SetPose(isActivePose: true);
			}
			if (player._collider != null)
			{
				player._collider.enabled = false;
			}
			if (player._rigidbody != null)
			{
				player._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			}
			player.ActIdle();
			initNPCPos[extention_info.npcLvIndex] = pos;
			listNpc[extention_info.npcLvIndex] = player;
			listNpcInfo[extention_info.npcLvIndex] = extention_info;
		}

		private IEnumerator EnableNpc(float delay, Player npc, StageObjectManager.CreatePlayerInfo.ExtentionInfo extention_info, Vector3 pos)
		{
			yield return new WaitForSeconds(delay);
			NpcController npcController = npc.gameObject.GetComponent<NpcController>();
			Debug.Log($"Set enable npc {pos.x} {pos.z}");
			npc.transform.position = pos;
			npc.onTheGround = true;
			npc.ActiveShadow(isActive: true);
			npc.transform.eulerAngles = Vector3.zero;
			npc.SetDiableAction(Character.ACTION_ID.MAX, disable: false);
			npc.SetDiableAction(Character.ACTION_ID.MOVE, disable: false);
			npc.SetDiableAction(Character.ACTION_ID.MOVE_LOOKAT, disable: false);
			npc.SetDiableAction(Character.ACTION_ID.MOVE_POINT, disable: false);
			npc.SetDiableAction(Character.ACTION_ID.ATTACK, disable: false);
			npc.SetDiableAction(Character.ACTION_ID.MAX, disable: false);
			if (npc._collider != null)
			{
				npc._collider.enabled = true;
			}
			if (npc._rigidbody != null)
			{
				npc._rigidbody.constraints = (RigidbodyConstraints)116;
			}
			tutorialManager.appearEffect.transform.position = npc.transform.position;
			LoadingQueue loadingQueue = new LoadingQueue(npc);
			int voice_id = 0;
			switch (extention_info.npcDataID)
			{
			case 991:
				voice_id = ENTRY_VOICE_PATTERN_1;
				npcUseSkill0 = npcController;
				break;
			case 990:
				voice_id = ENTRY_VOICE_PATTERN_2;
				npcUseSkill1 = npcController;
				break;
			case 992:
				voice_id = ENTRY_VOICE_PATTERN_3;
				npcUseSkill2 = npcController;
				break;
			}
			loadingQueue.CacheActionVoice(voice_id);
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			tutorialManager.StartCoroutine(IEStartPose(npcController, extention_info.npcDataID));
			yield return new WaitForSeconds(1f);
			m_npcLoadCompleteBitFlag |= 1 << extention_info.npcLvIndex;
		}

		private IEnumerator PlayVoice(int id)
		{
			switch (id)
			{
			case 991:
				SoundManager.PlayActionVoice(ENTRY_VOICE_PATTERN_1);
				break;
			case 990:
				yield return new WaitForSeconds(1.5f);
				SoundManager.PlayActionVoice(ENTRY_VOICE_PATTERN_2);
				break;
			case 992:
				yield return new WaitForSeconds(2f);
				SoundManager.PlayActionVoice(ENTRY_VOICE_PATTERN_3);
				break;
			}
		}

		private IEnumerator IEStartPose(NpcController controller, int npcId)
		{
			while (!controller.nonPlayer.isInitialized)
			{
				yield return null;
			}
			tutorialManager.StartCoroutine(PlayVoice(npcId));
			controller.nonPlayer.ActPose();
		}

		private IEnumerator IEStopPose()
		{
			while (npcUseSkill0 == null || npcUseSkill1 == null || npcUseSkill2 == null)
			{
				yield return null;
			}
			yield return new WaitForSeconds(BOT_POSE_TIME);
			npcUseSkill0.SetPose(isActivePose: false);
			npcUseSkill1.SetPose(isActivePose: false);
			npcUseSkill2.SetPose(isActivePose: false);
		}

		private int ChooseRandom(int[] items, float rejectRate = 0f)
		{
			if (items == null)
			{
				return 0;
			}
			if (UnityEngine.Random.Range(0f, 1f) < rejectRate)
			{
				return 0;
			}
			int num = UnityEngine.Random.Range(0, items.Length);
			return items[num];
		}

		private void EnemyEscapeBattle()
		{
			if (base.character.actionID != (Character.ACTION_ID)22)
			{
				if (!isTrackDragonFight)
				{
					isTrackDragonFight = true;
					MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end, "Tutorial");
					Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end.ToString());
					MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_5_battle_end, "Tutorial");
				}
				currentPhase = Phase.NONE;
				if (playerStatusRoot != null)
				{
					playerStatusRoot.gameObject.SetActive(value: false);
				}
				if (enemyStatusRoot != null)
				{
					enemyStatusRoot.gameObject.SetActive(value: false);
				}
				MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
				Transform transform = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain)._transform.Find("InGameMain/DynamicPanel");
				if (transform != null)
				{
					transform.gameObject.SetActive(value: false);
				}
				if (base.selfController != null)
				{
					base.selfController.enabled = false;
				}
				if (bossController != null)
				{
					bossController.enabled = false;
				}
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
				tutorialManager.director.StartBattleEndDirection(tutorialManager.legendDragon, tutorialManager.titleUIPrefab, delegate
				{
					currentPhase = Phase.DISP_TIELE;
					ResourceManager.autoRetry = true;
					MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
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
						MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting, "Tutorial");
						Debug.LogWarning("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting.ToString());
						MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_6_pamela_greeting, "Tutorial");
						MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameTutorialManager", tutorialManager.gameObject, "STORY", new object[4]
						{
							11000001,
							0,
							0,
							"MAIN_MENU_HOME"
						});
						MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
					}
				});
			});
		}

		public override void Final()
		{
		}
	}

	public float TutorialMoveTime;

	private static readonly float SHOW_BOT_CAM_TIME = 8f;

	private static readonly float SHOW_BOT_CAM_TIME_RATIO_PHASE1 = 1.5f;

	private static readonly float SHOW_BOT_CAM_FOV_PHASE1 = 50f;

	private static readonly float SHOW_BOT_CAM_TIME_RATE = 0.1f;

	private static readonly float SHOW_BOT_CAM_Y = 4f;

	private static readonly float SHOW_BOT_CAM_TIME_RATIO_PHASE2 = 2f;

	private static readonly float SHOW_WELCOME_LOG_TIME = 3f;

	private static readonly float BOT_POSE_TIME = 4f;

	private static readonly float SECOND_SKILL_TIME = 20f;

	private static readonly float FIRST_SKILL_TIME_OFFSET = 10f;

	private static readonly float SHOW_BOT_SKILL_CAM_COORDINATE_OFFSET = 2.5f;

	private static readonly float SHOW_BOT_SKILL_TIME = 2f;

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

	public const int BATTLE_END_STORY_ID = 11000001;

	public const int CHARAMAKE_END_STORY_ID = 11000002;

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
			if (dialogWindow == null)
			{
				dialogWindow = ResourceUtility.Realizes(dialogPrefab).GetComponent<UITutorialDialog>();
				dialogWindow.Close();
			}
			return dialogWindow;
		}
	}

	public UITutorialOperationHelper helper
	{
		get
		{
			if (helperUI == null)
			{
				helperUI = ResourceUtility.Realizes(helperPrefab).GetComponent<UITutorialOperationHelper>();
			}
			return helperUI;
		}
	}

	public TutorialBossDirector director
	{
		get
		{
			if (bossDirector == null)
			{
				bossDirector = ResourceUtility.Realizes(bossDirectorPrefab).GetComponent<TutorialBossDirector>();
			}
			return bossDirector;
		}
	}

	public GameObject legendDragon
	{
		get
		{
			if (_legendDragon == null)
			{
				_legendDragon = ResourceUtility.Realizes(legendDragonPrefab).gameObject;
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
			if (_appearEffect == null)
			{
				_appearEffect = ResourceUtility.Realizes(appearEffectPrefab).gameObject;
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
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, is_stop: true);
		while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			yield return null;
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedDialog = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialDialog");
		LoadObject loadedTargetArea = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_tutorial_area_01");
		LoadObject loadedHelper = loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialOperationHelper");
		LoadObject loadedDirector = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
		LoadObject loadedDragon = loadingQueue.Load(RESOURCE_CATEGORY.ENEMY_MODEL, "ENM01_Legend");
		loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[1]
		{
			"mdl_arrow_01"
		});
		LoadObject loadedAppear = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_battle_start_01");
		loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_01");
		MonoBehaviourSingleton<UIManager>.I.LoadUI(need_common: false, need_outgame: false, need_tutorial: true);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_COMPLETE);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		dialogPrefab = loadedDialog.loadedObject;
		targetAreaPrefab = loadedTargetArea.loadedObject;
		helperPrefab = loadedHelper.loadedObject;
		bossDirectorPrefab = loadedDirector.loadedObject;
		legendDragonPrefab = loadedDragon.loadedObject;
		appearEffectPrefab = loadedAppear.loadedObject;
		while (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.gameObject.SetActive(value: false);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.gameObject.SetActive(value: false);
		}
		InGameMain inGameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
		if (inGameMain != null)
		{
			Transform transform = inGameMain._transform.Find("InGameMain/PlayerStatus");
			if (transform != null)
			{
				transform.gameObject.SetActive(value: false);
			}
			transform = inGameMain._transform.Find("InGameMain/StaticSwitchPanel/NewMenuParent");
			if (transform != null)
			{
				transform.gameObject.SetActive(value: false);
			}
			transform = inGameMain._transform.Find("InGameMain/StaticSwitchPanel/ChatButtonParent");
			if (transform != null)
			{
				transform.gameObject.SetActive(value: false);
			}
		}
		List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
		for (int i = 0; i < portalObjectList.Count; i++)
		{
		}
		Change(new TutorialBattle(this));
		isCompleteLoadSE = false;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, is_stop: false);
		ResourceManager.autoRetry = true;
	}

	public void UpdateArrowModel()
	{
		if (!(null == mdlArrow))
		{
			float num = 2.5f;
			Vector3 position = mdlArrow.position;
			Vector3 position2 = MonoBehaviourSingleton<AppMain>.I.mainCamera.transform.position;
			float num2 = num;
			float num3 = Vector3.Distance(position, position2);
			num2 = ((!(10f > num3)) ? (num + 0.05f * (num3 - num)) : num);
			mdlArrow.localScale = new Vector3(num2, num2, num2);
		}
	}

	private void Update()
	{
		if (current != null)
		{
			current.Update();
		}
	}

	private void FixedUpdate()
	{
		if (current != null)
		{
			current.FixedUpdate();
		}
	}

	private void OnDestroy()
	{
		ResourceManager.autoRetry = false;
		if (helper != null)
		{
			UnityEngine.Object.Destroy(helper.gameObject);
		}
		if (dialog != null)
		{
			UnityEngine.Object.Destroy(dialog.gameObject);
		}
		if (null != mdlArrow)
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
		Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, pos, 0f, num, enemy_lv, is_boss: false, is_big_monster: false, setAI, willStock: false, delegate(Enemy target)
		{
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(target.id, target.hpMax);
			}
			if (!setAI)
			{
				EnemyController component = target.GetComponent<EnemyController>();
				if (component != null)
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
		yield return new WaitForSeconds(waitTime);
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
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_01);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_LANDING);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_01);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_THUNDERSTORM_02);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_02);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_03);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_DRAGON_CALL_04);
		loadingQueue.CacheSE(UITutorialOperationHelper.SE_ID_TITLELOGO);
		while (loadingQueue.IsLoading())
		{
			yield return null;
		}
		isLoadingSE = false;
		isCompleteLoadSE = true;
	}
}
