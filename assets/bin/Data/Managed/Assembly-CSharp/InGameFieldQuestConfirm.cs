using System;
using UnityEngine;

public class InGameFieldQuestConfirm : GameSection
{
	private enum UI
	{
		PORTRAIT_MAIN,
		PORTRAIT_SUB,
		PORTRAIT_DROP,
		PORTRAIT_BTN,
		PORTRAIT_TIME,
		PORTRAIT_BTN_0,
		PORTRAIT_BTN_2,
		PORTRAIT_MISSION,
		LANDSCAPE_MAIN,
		LANDSCAPE_SUB,
		LANDSCAPE_DROP,
		LANDSCAPE_BTN,
		LANDSCAPE_TIME,
		LANDSCAPE_BTN_0,
		LANDSCAPE_BTN_2,
		LANDSCAPE_MISSION,
		BOSS_INFO_MAIN,
		BOSS_INFO_SUB,
		BTN_FRAME,
		TIME,
		SPR_BTN_0,
		SPR_BTN_2,
		MISSION,
		LBL_NAME,
		NUM_LV,
		LBL_LV,
		STR_ELEM,
		STR_WEAK_NONE,
		NUM_TIMER,
		LBL_TIME,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		COUNT_ANIM_0,
		COUNT_ANIM_1,
		COUNT_ANIM_2,
		OBJ_FRAME,
		MISSION_LABEL_01,
		MISSION_LABEL_02,
		MISSION_LABEL_03,
		MISSION_CROWN_ON_01,
		MISSION_CROWN_ON_02,
		MISSION_CROWN_ON_03,
		MISSION_CROWN_OFF_01,
		MISSION_CROWN_OFF_02,
		MISSION_CROWN_OFF_03
	}

	public class Desc
	{
		public QuestTable.QuestTableData questData;

		public QuestInfoData.Quest.Reward[] reward;
	}

	private float timeLimit;

	private int prevTime;

	private bool isAnswer;

	private int countAnimStep;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
	}

	public override void UpdateUI()
	{
		Desc desc = GameSection.GetEventData() as Desc;
		if (desc == null)
		{
			return;
		}
		QuestTable.QuestTableData questData = desc.questData;
		if (questData == null)
		{
			return;
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData == null)
		{
			return;
		}
		int mainEnemyLv = questData.GetMainEnemyLv();
		SetLabelText(UI.LBL_NAME, enemyData.name);
		SetLabelText(UI.NUM_LV, mainEnemyLv.ToString());
		SetElementSprite(UI.STR_ELEM, (int)enemyData.weakElement);
		if (enemyData.weakElement != ELEMENT_TYPE.MAX)
		{
			SetActive(UI.STR_WEAK_NONE, is_visible: false);
		}
		int num = (int)(questData.limitTime / 60f);
		int num2 = (int)(questData.limitTime % 60f);
		SetLabelText(UI.NUM_TIMER, $"{num:D2}:{num2:D2}");
		UI[] array = new UI[10]
		{
			UI.OBJ_DIFFICULT_STAR_1,
			UI.OBJ_DIFFICULT_STAR_2,
			UI.OBJ_DIFFICULT_STAR_3,
			UI.OBJ_DIFFICULT_STAR_4,
			UI.OBJ_DIFFICULT_STAR_5,
			UI.OBJ_DIFFICULT_STAR_6,
			UI.OBJ_DIFFICULT_STAR_7,
			UI.OBJ_DIFFICULT_STAR_8,
			UI.OBJ_DIFFICULT_STAR_9,
			UI.OBJ_DIFFICULT_STAR_10
		};
		int num3 = (int)(questData.difficulty + 1);
		int i = 0;
		for (int num4 = array.Length; i < num4; i++)
		{
			SetActive(array[i], i < num3);
		}
		PlayTween(UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
		QuestInfoData.Mission[] array2 = QuestInfoData.CreateMissionData(questData);
		if (array2 != null)
		{
			GetCtrl(UI.MISSION).gameObject.SetActive(value: true);
			UI[] array3 = new UI[3]
			{
				UI.MISSION_LABEL_01,
				UI.MISSION_LABEL_02,
				UI.MISSION_LABEL_03
			};
			UI[] array4 = new UI[3]
			{
				UI.MISSION_CROWN_ON_01,
				UI.MISSION_CROWN_ON_02,
				UI.MISSION_CROWN_ON_03
			};
			UI[] array5 = new UI[3]
			{
				UI.MISSION_CROWN_OFF_01,
				UI.MISSION_CROWN_OFF_02,
				UI.MISSION_CROWN_OFF_03
			};
			int num5 = Mathf.Min(array2.Length, 3);
			for (int j = 0; j < num5; j++)
			{
				QuestInfoData.Mission mission = array2[j];
				SetActive(array4[j], CLEAR_STATUS.CLEAR == mission.state);
				SetActive(array5[j], CLEAR_STATUS.CLEAR != mission.state);
				SetLabelText(array3[j], mission.tableData.missionText);
			}
		}
		if (desc.reward != null)
		{
			Array.Sort(desc.reward, (QuestInfoData.Quest.Reward l, QuestInfoData.Quest.Reward r) => l.priority - r.priority);
		}
		SetFontStyle(UI.LBL_NAME, FontStyle.Italic);
		SetFontStyle(UI.NUM_LV, FontStyle.Italic);
		SetFontStyle(UI.LBL_LV, FontStyle.Italic);
		countAnimStep = 0;
		timeLimit = MonoBehaviourSingleton<InGameSettingsManager>.I.happenQuestDirection.confirmUITime;
		prevTime = -1;
		isAnswer = false;
		Update();
		UpdateAnchors();
		PlayTween(UI.OBJ_FRAME);
		PlayTween(UI.COUNT_ANIM_0, forward: true, null, is_input_block: false);
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void Update()
	{
		if (isAnswer)
		{
			return;
		}
		timeLimit -= Time.deltaTime;
		if (timeLimit <= 0f)
		{
			timeLimit = 0f;
		}
		int num = (int)timeLimit;
		if (timeLimit > 0f && timeLimit - (float)num > 0f)
		{
			num++;
		}
		switch (countAnimStep)
		{
		case 0:
			if (num < 10)
			{
				ResetTween(UI.COUNT_ANIM_0);
				PlayTween(UI.COUNT_ANIM_1, forward: true, null, is_input_block: false);
				countAnimStep++;
			}
			break;
		case 1:
			if (num < 6)
			{
				ResetTween(UI.COUNT_ANIM_1);
				PlayTween(UI.COUNT_ANIM_2, forward: true, null, is_input_block: false);
				SetColor(UI.LBL_TIME, Color.red);
				countAnimStep++;
			}
			break;
		case 2:
			if (prevTime != num)
			{
				ResetTween(UI.COUNT_ANIM_2);
				PlayTween(UI.COUNT_ANIM_2, forward: true, null, is_input_block: false);
			}
			break;
		}
		if (prevTime != num)
		{
			SetLabelText(UI.LBL_TIME, num.ToString());
			prevTime = num;
		}
		if (!(timeLimit > 0f) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<InGameProgress>.I.endHappenQuestDirection)
		{
			ResetTween(UI.COUNT_ANIM_0);
			ResetTween(UI.COUNT_ANIM_1);
			ResetTween(UI.COUNT_ANIM_2);
			OnQuery_YES();
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		Transform ctrl = GetCtrl(UI.BOSS_INFO_MAIN);
		Transform ctrl2 = GetCtrl(UI.BOSS_INFO_SUB);
		Transform ctrl3 = GetCtrl(UI.BTN_FRAME);
		Transform ctrl4 = GetCtrl(UI.MISSION);
		Transform ctrl5 = GetCtrl(UI.TIME);
		Transform ctrl6 = GetCtrl(UI.SPR_BTN_0);
		Transform ctrl7 = GetCtrl(UI.SPR_BTN_2);
		Vector3 localPosition = ctrl.localPosition;
		Vector3 localPosition2 = ctrl2.localPosition;
		Vector3 localPosition3 = ctrl3.localPosition;
		Vector3 localPosition4 = ctrl4.localPosition;
		if (is_portrait)
		{
			ctrl.parent = GetCtrl(UI.PORTRAIT_MAIN);
			ctrl2.parent = GetCtrl(UI.PORTRAIT_SUB);
			ctrl3.parent = GetCtrl(UI.PORTRAIT_BTN);
			ctrl4.parent = GetCtrl(UI.PORTRAIT_MISSION);
			ctrl5.localPosition = GetCtrl(UI.PORTRAIT_TIME).localPosition;
			ctrl6.localPosition = GetCtrl(UI.PORTRAIT_BTN_0).localPosition;
			ctrl7.localPosition = GetCtrl(UI.PORTRAIT_BTN_2).localPosition;
		}
		else
		{
			ctrl.parent = GetCtrl(UI.LANDSCAPE_MAIN);
			ctrl2.parent = GetCtrl(UI.LANDSCAPE_SUB);
			ctrl3.parent = GetCtrl(UI.LANDSCAPE_BTN);
			ctrl4.parent = GetCtrl(UI.LANDSCAPE_MISSION);
			ctrl5.localPosition = GetCtrl(UI.LANDSCAPE_TIME).localPosition;
			ctrl6.localPosition = GetCtrl(UI.LANDSCAPE_BTN_0).localPosition;
			ctrl7.localPosition = GetCtrl(UI.LANDSCAPE_BTN_2).localPosition;
		}
		ctrl.localPosition = localPosition;
		ctrl2.localPosition = localPosition2;
		ctrl3.localPosition = localPosition3;
		ctrl4.localPosition = localPosition4;
	}

	private void OnQuery_YES()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.OnFieldQuestConfirm(is_yes: true);
		}
		isAnswer = true;
	}

	private void OnQuery_NO()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.OnFieldQuestConfirm(is_yes: false);
		}
		isAnswer = true;
	}
}
