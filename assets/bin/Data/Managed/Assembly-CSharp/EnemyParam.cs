using UnityEngine;

public class EnemyParam
{
	public StageObject.StampInfo[] stampInfos;

	public AttackHitInfo[] attackHitInfos;

	public AttackContinuationInfo[] attackContinuationInfos;

	public AttackHitInfo[] convertAttackHitInfos;

	public AttackContinuationInfo[] convertAttackContinuationInfos;

	public Enemy.RegionInfo[] regionInfos;

	public Enemy.RegionInfo[] convertRegionInfos;

	[Tooltip("最短移動回転時間（回転始動と終了のスム\u30fcズに関係")]
	public float moveRotateMinimumTime = 0.3f;

	[Tooltip("移動回転最大速度（角度/s")]
	public float moveRotateMaxSpeed = 60f;

	[Tooltip("移動停止範囲")]
	public float moveStopRange = 5f;

	[Tooltip("最短回転時間（回転始動と終了のスム\u30fcズに関係")]
	public float rotateMinimumTime = 0.1f;

	[Tooltip("回転最大速度（角度/s")]
	public float rotateMaxSpeed = 120f;

	[Tooltip("回転時のモ\u30fcション無効")]
	public bool rotateDisableMotion;

	[Tooltip("頭のオブジェクト名(頭からの距離測定起点)")]
	public string headObjectName = "c_head";

	[Tooltip("尻のオブジェクト名(尻からの距離測定起点)")]
	public string hipObjectName = "c_hip";

	[Tooltip("ダウン値（蓄積最大値")]
	public int downMax = 100;

	[Tooltip("ダウン値の秒間回復量")]
	public float downHeal = 10f;

	[Tooltip("体の大きさ半径（マップとの当たり、影の大きさ")]
	public float bodyRadius = 1f;

	[Tooltip("影の大きさ")]
	public float shadowSize;

	[Tooltip("UI高さ")]
	public float uiHeight = 2f;

	[Tooltip("麻痺耐性")]
	public float paralyzeMax = 100f;

	[Tooltip("毒耐性")]
	public float poisonMax = 100f;

	[Tooltip("凍結耐性")]
	public float freezeMax = 100f;

	[Tooltip("しばり拘束時間減少割合")]
	public float shadowSealingBindResist = 1f;

	[Tooltip("基本ヒット素材名(EnemyHitMaterialTable)")]
	public string baseHitMaterialName;

	[Tooltip("思考情報")]
	public BrainParam brainParam = new BrainParam();

	[Tooltip("逃げる情報の名前")]
	public string escapeParamName = string.Empty;

	[Tooltip("バリア最大HP")]
	public int barrierHpMax;

	[Tooltip("幽体化時の耐性値")]
	public AtkAttribute ghostFormParam = new AtkAttribute();

	[Tooltip("幽体化バフのシェ\u30fcダ調整値")]
	public GhostFormShaderParam ghostFormShaderParam = new GhostFormShaderParam();

	[Tooltip("生成時に自動起動するバフ情報")]
	public AutoBuffParam[] autoBuffParams;

	[Tooltip("ドレイン攻撃情報")]
	public DrainAttackInfo[] drainAtkInfos;

	[Tooltip("回復ダメ\u30fcジ倍率")]
	public float healDamageRate;

	[Tooltip("隠れた状態で登場するか")]
	public bool isHide;

	[Tooltip("隠れた状態から姿を表す距離")]
	public float turnUpDistance;

	[Tooltip("隠れた状態の時に偽装するGatherPointViewTableのID")]
	public uint gatherPointViewId;

	[Tooltip("シ\u30fcルド最大HP")]
	public int shieldHpMax;

	[Tooltip("シ\u30fcルド有効時の耐性値")]
	public AtkAttribute shieldTolerance = new AtkAttribute();

	[Tooltip("シ\u30fcルド破壊時の眩暈リアクションル\u30fcプ時間")]
	public float dizzyReactionLoopTime;

	[Tooltip("拡張アクションID")]
	public int exActionId;

	[Tooltip("拡張アクション切り替え条件")]
	public int exActionCondition;

	[Tooltip("拡張アクション切り替え条件値")]
	public int exActionConditionValue;

	[Tooltip("掴み最大HP")]
	public int grabHpMax;

	[Tooltip("掴み弱点への砲台ダメ\u30fcジ値")]
	public int grabCannonDamage;

	[Tooltip("ダウンル\u30fcプを使用する")]
	public bool useDownLoopTime;

	[Tooltip("ダウンル\u30fcプ開始時間")]
	public float downLoopStartTime = -1f;

	[Tooltip("ダウンル\u30fcプ時間")]
	public float downLoopTime = -1f;

	[Tooltip("常駐エフェクト設定デ\u30fcタ")]
	public SystemEffectSetting residentEffectSetting;

	[Tooltip("麻痺時間")]
	public float paralyzeLoopTime = 9f;

	[Tooltip("属性耐性テ\u30fcブル情報上書き")]
	public ConverteElementToleranceTable[] converteElementToleranceTable = new ConverteElementToleranceTable[0];

	[Tooltip("魔狂化になるHP割合")]
	public int madModeHpThreshold;

	[Tooltip("魔狂化になるLv")]
	public int madModeLvThreshold;

	public EnemyParam()
		: this()
	{
	}

	public void SetParam(Enemy targetEnemy)
	{
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		targetEnemy.SetResidentEffectSetting(residentEffectSetting);
		targetEnemy.SetAttackInfos(Utility.CreateMergedArray((AttackInfo[])attackHitInfos, (AttackInfo[])attackContinuationInfos));
		targetEnemy.convertAttackInfos = Utility.CreateMergedArray((AttackInfo[])convertAttackHitInfos, (AttackInfo[])convertAttackContinuationInfos);
		targetEnemy.regionInfos = regionInfos;
		targetEnemy.convertRegionInfos = convertRegionInfos;
		targetEnemy.moveRotateMinimumTime = moveRotateMinimumTime;
		targetEnemy.moveRotateMaxSpeed = moveRotateMaxSpeed;
		targetEnemy.moveStopRange = moveStopRange;
		targetEnemy.rotateMinimumTime = rotateMinimumTime;
		targetEnemy.rotateMaxSpeed = rotateMaxSpeed;
		targetEnemy.rotateDisableMotion = rotateDisableMotion;
		targetEnemy.headObjectName = headObjectName;
		targetEnemy.hipObjectName = hipObjectName;
		targetEnemy._downMax = downMax;
		targetEnemy.downHeal = downHeal;
		targetEnemy.bodyRadius = bodyRadius;
		targetEnemy.uiHeight = uiHeight;
		targetEnemy.badStatusMax.paralyze = paralyzeMax;
		targetEnemy.badStatusMax.poison = poisonMax;
		targetEnemy.badStatusMax.freeze = freezeMax;
		targetEnemy.badStatusBase.Copy(targetEnemy.badStatusMax);
		targetEnemy.baseHitMaterialName = baseHitMaterialName;
		targetEnemy.brainParam = brainParam;
		targetEnemy.BarrierHpMax = barrierHpMax;
		targetEnemy.GhostFormParam = ghostFormParam;
		targetEnemy.GhostFormShaderParam = ghostFormShaderParam;
		targetEnemy.AutoBuffParamList = autoBuffParams;
		targetEnemy.drainAtkInfos = drainAtkInfos;
		targetEnemy.healDamageRate = healDamageRate;
		targetEnemy.isHideSpawn = isHide;
		targetEnemy.isHiding = isHide;
		targetEnemy.turnUpDistance = turnUpDistance;
		targetEnemy.gatherPointViewId = gatherPointViewId;
		targetEnemy.ShieldTolerance = shieldTolerance;
		targetEnemy.ShieldHpMax = shieldHpMax;
		targetEnemy.DizzyReactionLoopTime = dizzyReactionLoopTime;
		targetEnemy.ExActionID = exActionId;
		targetEnemy.ExActionCondition = exActionCondition;
		targetEnemy.ExActionConditionValue = exActionConditionValue;
		targetEnemy.GrabHpMax = grabHpMax;
		targetEnemy.GrabCannonDamage = grabCannonDamage;
		targetEnemy.useDownLoopTime = useDownLoopTime;
		targetEnemy.downLoopStartTime = downLoopStartTime;
		targetEnemy.downLoopTime = downLoopTime;
		targetEnemy.paralyzeLoopTime = paralyzeLoopTime;
		targetEnemy.shadowSealingBindResist = shadowSealingBindResist;
		targetEnemy.converteElementToleranceTable = converteElementToleranceTable;
		targetEnemy.madModeHpThreshold = madModeHpThreshold;
		targetEnemy.madModeLvThreshold = madModeLvThreshold;
		if (stampInfos != null && stampInfos.Length > 0)
		{
			CharacterStampCtrl characterStampCtrl = this.get_gameObject().GetComponent<CharacterStampCtrl>();
			if (characterStampCtrl == null)
			{
				characterStampCtrl = this.get_gameObject().AddComponent<CharacterStampCtrl>();
			}
			characterStampCtrl.Init(stampInfos, targetEnemy, false);
		}
	}
}
