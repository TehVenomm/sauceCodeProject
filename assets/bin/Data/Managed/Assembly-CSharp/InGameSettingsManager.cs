using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameSettingsManager : MonoBehaviourSingleton<InGameSettingsManager>
{
	[Serializable]
	public class SelfController
	{
		[Serializable]
		public class ArrowAimBossSettings
		{
			[Tooltip("狙いモ\u30fcド継続有効")]
			public bool enableAimKeep = true;

			[Tooltip("上方向への限界角度")]
			public float angleLimitUp = 30f;

			[Tooltip("下方向への限界角度")]
			public float angleLimitDown = 10f;

			[Tooltip("横方向への限界角度")]
			public float angleLimitSide = 25f;

			[Tooltip("ドラッグ変化率X軸")]
			public float dragRateX = 15f;

			[Tooltip("ドラッグ変化率Y軸")]
			public float dragRateY = 15f;

			[Tooltip("余剰回転速度")]
			public float overSpeedRate = 1f;

			[Tooltip("余剰回転速度制限")]
			public float overSpeedLimit = 2f;

			[Tooltip("余剰回転スクリ\u30fcン割合")]
			public float overScreenRate = 0.15f;

			[Tooltip("余剰回転スクリ\u30fcン猶予")]
			public float overScreenMargin = 0.05f;

			[Tooltip("余剰回転速度、最小角度範囲")]
			public float overSpeedAngleMinRange = 30f;

			[Tooltip("余剰回転速度、変化角度範囲")]
			public float overSpeedAngleChangeRange = 60f;

			[Tooltip("余剰回転速度、最大倍率")]
			public float overSpeedMaxRate = 3f;
		}

		[Serializable]
		public class ArrowAimLesserSettings
		{
			[Tooltip("ドラッグ最小距離")]
			public float dragMinLength = 0.2f;

			[Tooltip("ドラッグ最大距離")]
			public float dragMaxLength = 1.7f;

			[Tooltip("カ\u30fcソル移動速度")]
			public float cursorSpeed = 10f;

			[Tooltip("カ\u30fcソル初速度倍率")]
			public float cursorStartSpeedRate = 2f;

			[Tooltip("カ\u30fcソル初速度継続時間")]
			public float cursorStartSpeedTime = 0.5f;

			[Tooltip("カ\u30fcソルタ\u30fcゲット中速度倍率")]
			public float cursorTargetingSpeedRate = 0.5f;

			[Tooltip("カ\u30fcソル移動限界距離")]
			public float cursorMaxDistance = 15f;

			[Tooltip("カ\u30fcソルサイズスケ\u30fcル")]
			public float cursorScale = 2f;
		}

		public float moveForwardSpeed = 5.5f;

		public float moveSideSpeed = 5.5f;

		public bool enableRootMotion;

		public bool alwaysTopSpeed = true;

		public float targetingDistance = 20f;

		public float targetingDistanceFar = 30f;

		[Tooltip("移動入力受付開始時間")]
		public float inputMoveStartTime = 0.15f;

		[Tooltip("先行入力有効時間（0:攻撃 1:回避 2:スキル 3:武器固有アクション 4:武器切り替え 5:採取 6:大砲搭乗 7:大砲発射 8:ソナ\u30fc 9:ワ\u30fcプ")]
		public float[] inputCommandValidTime;

		[Tooltip("同一コマンド入力間隔時間（0:攻撃 1:回避 2:スキル 3:武器固有アクション 4:武器切り替え 5:採取 6:大砲搭乗 7:大砲発射 8:ソナ\u30fc 9:ワ\u30fcプ")]
		public float[] inputCommandIntervalTime;

		[Tooltip("タッチ入力の攻撃判定開始時間、感度高")]
		public float inputLongTouchTimeHigh = 0.15f;

		[Tooltip("タッチ入力の攻撃判定開始時間、感度低")]
		public float inputLongTouchTimeLow = 0.35f;

		[Tooltip("ガ\u30fcド移動のドラッグ距離閾値")]
		public float guardMoveThreshold = 0.6f;

		[Tooltip("ガ\u30fcド移動の同期速度")]
		public float guardMoveSyncSpeed = 2f;

		[Tooltip("狙い開始のドラッグ量")]
		public float aimDragLength = 0.35f;

		[Tooltip("狙い開始の猶予時間")]
		public float aimDelayTime = 0.15f;

		[Tooltip("SpAttackType指定のあるアビリティを無視")]
		public bool ignoreSpAttackTypeAbility = true;

		[Tooltip("InGameでのDynamicBoneを削除するか")]
		public DYNAMICBONE_TYPE dynamicBoneType = DYNAMICBONE_TYPE.DISABLE_LOW;

		public ArrowAimBossSettings arrowAimBossSettings;

		public ArrowAimLesserSettings arrowAimLesserSettings;

		public ArrowAimLesserSettings spearAimLesserSettings;

		[Tooltip("氷の上で最高速度に到達するまでの時間")]
		public float needTimeToMaxTimeOnIce = 2f;

		[Tooltip("氷の上ですべる時間")]
		public float slideTime = 1.2f;

		[Tooltip("氷の上で回避した際のすべるスピ\u30fcド")]
		public float avoidSpeedOnIce = 5f;

		[Tooltip("大砲の横方向操作距離に掛ける係数")]
		public float cannonDragRateX = 2f;

		[Tooltip("画面内におさめるオフセット")]
		public float screenSaftyOffset = 1.7f;
	}

	[Serializable]
	public class EnemyController
	{
		[Tooltip("AI開始時待機時間")]
		public float startWaitTime = 3f;

		[Tooltip("リアクション後待機時間")]
		public float afterReactionWaitTime = 0.5f;
	}

	[Serializable]
	public class NpcController
	{
		[Tooltip("AI開始時待機時間")]
		public float startWaitTime = 1f;

		[Tooltip("リアクション後待機時間")]
		public float afterReactionWaitTime = 0.5f;
	}

	[Serializable]
	public class TargetMarkerSettings
	{
		[Tooltip("タ\u30fcゲットマ\u30fcカ\u30fc用エフェクト")]
		public string[] effectNames;
	}

	[Serializable]
	public class TargetMarker
	{
		public float showAngle = 180f;

		public float targetAngle = 90f;

		[Tooltip("タ\u30fcゲット可能な距離")]
		public float targetDistance = 3f;

		[Tooltip("タ\u30fcゲットマ\u30fcクを表示できる距離")]
		public float showTargetDistance = 10f;

		[Tooltip("弓でタ\u30fcゲット可能な距離")]
		public float targetDistanceArrow = 30f;

		[Tooltip("弓を装備している際のタ\u30fcゲットマ\u30fcクを表示できる距離")]
		public float showTargetDistanceArrow = 30f;

		[Tooltip("新タ\u30fcゲットが選択出来るまでの時間")]
		public float selectAbleTime = 0.3f;

		[Tooltip("今のタ\u30fcゲットが変更出来るまでの時間")]
		public float changeAbleTime = 0.5f;

		[Tooltip("ロック解除までのデフォルト時間")]
		public float defaultUnlockTime = 0.1f;

		[Tooltip("weakタ\u30fcゲットが変更出来るまでの時間")]
		public float changeWeakTime = 2f;

		[Tooltip("カメラ外のタ\u30fcゲット無効化")]
		public bool enableCameraCulling;

		[Tooltip("カメラ外のタ\u30fcゲット無効化割合")]
		public float cameraCullingMargin = 0.1f;

		[Tooltip("weakマ\u30fcカ\u30fc狙い補正距離")]
		public float weakMarginDistance = 1f;

		[Tooltip("通常マ\u30fcカ\u30fcの表示")]
		public bool enableNormalMarker;
	}

	[Serializable]
	public class StageObjectParam
	{
		[Tooltip("パケット実行許容時間（秒）")]
		public float packetHandleMarginTime = 10f;

		[Tooltip("連続マップヒット判定までの時間（秒）")]
		public float wallStayCheckTime = 0.5f;

		[Tooltip("パケット待ちの更新周期時間（秒）")]
		public float waitingPacketIntervalTime = 3f;

		[Tooltip("パケット待ちの更新猶予時間（秒）")]
		public float waitingPacketMarginTime = 3f;

		[Tooltip("座標計算用、マップ最大直径距離")]
		public float mapMaxDiameter = 300f;

		[Tooltip("同期待機の最大時間（秒）")]
		public float maxWaitSyncTime = 2f;
	}

	[Serializable]
	public class Character
	{
		[Tooltip("移動同期時回転時間")]
		public float moveSyncRotateTime = 0.2f;

		[Tooltip("移動抑制距離")]
		public float moveSuppressLength = 1.5f;

		[Tooltip("移動抑制レ\u30fcト")]
		public float moveSuppressRate = 0.1f;

		[Tooltip("移動の定期同期時間（秒")]
		public float moveSendInterval = 1f;

		[Tooltip("タ\u30fcゲットへの最大回転回数（90°ずつ回転")]
		public int rotateTargetMaxNum = 3;

		[Tooltip("モ\u30fcション変更補間時間（秒")]
		public float motionTransitionTime = 0.1f;

		[Tooltip("バフの定期同期時間（秒")]
		public float buffSyncUpdateInterval = 15f;

		[Tooltip("PERIODIC_SYNC_ACTION_POSITIONの座標決定時間（秒")]
		public float periodicSyncActionPositionCheckTime = 0.166666657f;

		[Tooltip("PERIODIC_SYNC_ACTION_POSITIONの座標決定からの適用時間（秒")]
		public float periodicSyncActionPositionApplyTime = 0.166666657f;
	}

	[Serializable]
	public class Player
	{
		[Serializable]
		public class WeaponInfo
		{
			public string name;

			[Tooltip("weakマ\u30fcカ\u30fcの攻撃力に対する倍率")]
			public float attackWeakRate = 1.5f;

			[Tooltip("ダウンマ\u30fcカ\u30fcの攻撃力に対する倍率")]
			public float attackDownRate = 1.5f;

			[Tooltip("武器ごとの防御力倍率(1で通常")]
			public float defenceRate = 1f;

			[Tooltip("weak時のダメ\u30fcジに対するダウン値の倍率")]
			public float downPowerWeak = 10f;

			[Tooltip("赤weak時のダメ\u30fcジに対するダウン値の倍率")]
			public float downPowerSimpleWeak = 10f;

			[Tooltip("(1.2.3で廃止)weakへのHIT時に怯ませるか？")]
			public bool weakFalter;

			[Tooltip("攻撃ヒット時のスキルゲ\u30fcジ回復量、武器種倍率")]
			public float healSkillGaugeHitRate = 1f;

			[Tooltip("スペシャルアタックヒット時のスキルゲ\u30fcジ回復量、武器種倍率")]
			public float healSkillGaugeHitRateSpecialAttack = 1f;

			[Tooltip("weak:属性攻撃に対する倍率")]
			public float weakRateElementAttack = 1.5f;

			[Tooltip("weak:マギ攻撃に対する倍率")]
			public float weakRateSkillAttack = 1.5f;

			[Tooltip("weak:マギ+属性攻撃に対する倍率")]
			public float weakRateElementSkillAttack = 1.5f;

			[Tooltip("weak:ヒ\u30fcル攻撃に対する倍率")]
			public float weakRateHealAttack = 1.5f;

			[Tooltip("weak:属性SPに対する倍率")]
			public float weakRateElementSpAttack = 1.5f;

			[Tooltip("タ\u30fcゲット箇所として無視する高さ")]
			public float ignoreTargetHeight = 5f;

			[Tooltip("タ\u30fcゲット箇所として無視する高さ（タイプ別）")]
			public float[] ignoreTargetHeightsByType;
		}

		[Serializable]
		public class SpecialActionInfo
		{
			[Serializable]
			public class ArrowBleedOther
			{
				[Tooltip("弓継続エフェクトの他人表示有効")]
				public bool enable = true;

				[Tooltip("弓継続エフェクトの他人表示、軸回転ランダム範囲角度")]
				public float axisRandomAngle = 90f;

				[Tooltip("弓継続エフェクトの他人表示、開き回転固定角度")]
				public float openFixAngle = 15f;

				[Tooltip("弓継続エフェクトの他人表示、開き回転ランダム範囲角度")]
				public float openRandomAngle = 20f;
			}

			[Tooltip("有効フラグ")]
			public bool enable = true;

			[Tooltip("発動エフェクト")]
			public string startEffectName;

			[Tooltip("特殊攻撃用の攻撃ID")]
			public int spAttackID;

			[Tooltip("ガ\u30fcドによるゲ\u30fcジ回復量")]
			public float guardHealGauge;

			[Tooltip("両手剣固有アクションによる弱点への攻撃力に対する倍率")]
			public float twoHandSwordWeakRate = 3f;

			[Tooltip("弓の出血秒間ダメ\u30fcジ割合")]
			public float arrowBleedDamageRate = 1f;

			[Tooltip("弓の出血ダメ\u30fcジ時間間隔")]
			public float arrowBleedTimeInterval = 5f;

			[Tooltip("弓の出血ダメ\u30fcジ初回スキップ残り時間レ\u30fcト(0〜1)")]
			public float arrowBleedSkipTimeRate = 0.2f;

			[Tooltip("弓の出血ダメ\u30fcジ回数")]
			public int arrowBleedCount = 1;

			[Tooltip("弓の出血継続エフェクト名")]
			public string arrowBleedEffectName;

			[Tooltip("弓の出血継続他人用エフェクト名")]
			public string arrowBleedOtherEffectName;

			[Tooltip("弓の出血ダメ\u30fcジエフェクト名")]
			public string arrowBleedDamageEffectName;

			[Tooltip("弓の狙いエフェクト名")]
			public string arrowChargeAimEffectName;

			[Tooltip("弓の出血ダメ\u30fcジ表示")]
			public bool arrowBleedShowDamage;

			[Tooltip("弓のザコ狙いカ\u30fcソルエフェクト名")]
			public string arrowAimLesserCursorEffectName;

			[Tooltip("弓のザコ狙い後アンロック時間")]
			public float arrowAimLesserUnlockTime = 2f;

			public ArrowBleedOther arrowBleedOther;

			[Tooltip("無属性バ\u30fcストショット")]
			public string arrowBurstEffectName;

			[Tooltip("火属性バ\u30fcストショット")]
			public string arrowFireBurstEffectName;

			[Tooltip("水属性バ\u30fcストショット")]
			public string arrowWaterBurstEffectName;

			[Tooltip("雷属性バ\u30fcストショット")]
			public string arrowThunderBurstEffectName;

			[Tooltip("地属性バ\u30fcストショット")]
			public string arrowSoilBurstEffectName;

			[Tooltip("光属性バ\u30fcストショット")]
			public string arrowLightrBurstEffectName;

			[Tooltip("闇属性バ\u30fcストショット")]
			public string arrowDarkBurstEffectName;

			[Tooltip("バ\u30fcストショットのダメ\u30fcジ倍率")]
			public float arrowBurstDamageRate = 4f;

			public string GetBurstEffectName(ELEMENT_TYPE type)
			{
				switch (type)
				{
				case ELEMENT_TYPE.MAX:
					return arrowBurstEffectName;
				case ELEMENT_TYPE.FIRE:
					return arrowFireBurstEffectName;
				case ELEMENT_TYPE.WATER:
					return arrowWaterBurstEffectName;
				case ELEMENT_TYPE.THUNDER:
					return arrowThunderBurstEffectName;
				case ELEMENT_TYPE.SOIL:
					return arrowSoilBurstEffectName;
				case ELEMENT_TYPE.LIGHT:
					return arrowLightrBurstEffectName;
				case ELEMENT_TYPE.DARK:
					return arrowDarkBurstEffectName;
				default:
					return arrowBurstEffectName;
				}
			}
		}

		[Serializable]
		public class TwoHandSwordActionInfo
		{
			[Tooltip("回避攻撃：できていいか")]
			public bool avoidAttackEnable = true;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜め用エフェクト名")]
			public string nameChargeExpandEffect;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めを溜めMAXで自動解放するか")]
			public bool isChargeExpandAutoRelease = true;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めの溜め時間")]
			public float timeChargeExpandMax = 3f;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めの溜め時間最短の秒数")]
			public float minTimeChargeExpandMax = 0.5f;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めによる属性ダメ\u30fcジ倍率最低値")]
			public float elementDamageRateMin = 1.1f;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めによる属性ダメ\u30fcジ倍率")]
			public float elementDamageRate = 2f;

			[Tooltip("両手剣[ノ\u30fcマル]の闘気溜めによる属性ダメ\u30fcジアップ倍率(MAXチャ\u30fcジ時)")]
			public float elementDamageRateFullCharge = 4f;

			[Tooltip("[ヒ\u30fcト]最大溜めHit後入力受付時間")]
			public float timeSpAttackContinueInput = 0.4f;

			[Tooltip("[ヒ\u30fcト]ヒ\u30fcトゲ\u30fcジ増加値：基礎")]
			public float heatGaugeIncreaseBase = 30f;

			[Tooltip("[ヒ\u30fcト]ブ\u30fcスト中の属性防御係数")]
			public float heatComboElementDefRate = 1f;

			[Tooltip("[Soul] 長い場合の通常攻撃ID")]
			public int Soul_LongAttackId = 15;

			[Tooltip("[Soul] 長い場合の通常攻撃最終ID")]
			public int Soul_LongAttackFinishId = 18;

			[Tooltip("[Soul] 長い場合の特殊アクションID")]
			public int Soul_LongSpAttackId = 95;

			[Tooltip("[ソウル]歩く速度")]
			public float soulWalkSpeed = 0.113f;

			[Tooltip("[ソウル]部位へのダメ\u30fcジ倍率")]
			public float soulRegionDamageRate = 2f;

			[Tooltip("[ソウル]居合 溜め時間")]
			public float soulIaiChargeTime;

			[Tooltip("[ソウル]居合 溜め時間(最小)")]
			public float soulIaiChargeTimeMin;

			[Tooltip("[ソウル]居合 溜め最大時エフェクト")]
			public string soulIaiChargeMaxEffect = "ef_btl_wsk_longsword_02_03";

			[Tooltip("[ソウル]居合 溜め最大時SEID")]
			public int soulIaiChargeMaxSeId = 40000359;

			[Tooltip("[ソウル]居合 抜刀ポ\u30fcズ時間")]
			public float soulIaiInSec = 0.15f;

			[Tooltip("[ソウル]居合 抜刀移動時間")]
			public float soulIaiMoveSec = 0.2f;

			[Tooltip("[ソウル]居合 抜刀消えてる時間")]
			public float soulIaiHideSec = 0.18f;

			[Tooltip("[ソウル]居合 移動速度[0]Min[1]Max")]
			public float[] soulIaiMoveSpeed;

			[Tooltip("[ソウル]居合 無属性ダメ\u30fcジアップ[0]Min[1]Max")]
			public float[] soulIaiNormalDamageUp;

			[Tooltip("[ソウル]居合 ゲ\u30fcジ上昇量[0]Min[1]Max")]
			public float[] soulIaiGaugeIncreaseValue;

			[Tooltip("[ソウル]コンボ最終 ゲ\u30fcジ上昇量")]
			public float soulComboGaugeIncreaseValue = 50f;

			[Tooltip("[ソウル]ジャストタップ 許容時間")]
			public float soulJustTapEnableSec = 1f;

			[Tooltip("[ソウル]ジャストタップ ゲ\u30fcジ上昇割合")]
			public float soulJustTapGaugeRate = 1.5f;

			[Tooltip("[ソウル]ソウル玉 通常スケ\u30fcル")]
			public float soulSoulEnergyNormalScale = 100f;

			[Tooltip("[ソウル]ソウル玉 ジャストタップスケ\u30fcル")]
			public float soulSoulEnergyJustTapScale = 150f;

			[Tooltip("[ソウル]刀舞モ\u30fcド ゲ\u30fcジ上昇割合")]
			public float soulBoostModeGaugeRate = 0.3f;

			[Tooltip("[ソウル]刀舞モ\u30fcド 突入時のSE")]
			public int soulBoostSeId = 20000106;

			[Tooltip("[ソウル]刀舞モ\u30fcド 攻撃速度-最低")]
			public float soulBoostMinAttackSpeed = 0.1f;

			[Tooltip("[ソウル]刀舞モ\u30fcド 攻撃速度-加算")]
			public float soulBoostAddAttackSpeed = 0.09f;

			[Tooltip("[ソウル]刀舞モ\u30fcド 攻撃速度-最大")]
			public float soulBoostMaxAttackSpeed = 1f;

			[Tooltip("[ソウル]刀舞モ\u30fcド 属性ダメ\u30fcジ倍率")]
			public float soulBoostElementDamageRate = 4f;

			[Tooltip("[ソウル]刀舞モ\u30fcド ゲ\u30fcジ減少値(毎秒)")]
			public float soulBoostGaugeDecreasePerSecond = 65f;

			[Tooltip("[ソウル]刀舞モ\u30fcド 溜め時ゲ\u30fcジ減少値(毎秒)")]
			public float soulBoostChargeGaugeDecreasePerSecond = 30f;

			[Tooltip("[ソウル]刀舞モ\u30fcド ダメ\u30fcジでリセットするか")]
			public bool isSoulBoostResetTriggerDamage = true;

			[Tooltip("[ソウル]刀舞モ\u30fcド ダメ\u30fcジでリセットするか")]
			public float soulBoostWaitPacketSec = 60f;

			[Tooltip("[Burst] バ\u30fcスト両手剣設定")]
			public BurstTwoHandSwordActionInfo burstTHSInfo = new BurstTwoHandSwordActionInfo();
		}

		[Serializable]
		public class BurstTwoHandSwordActionInfo
		{
			[Tooltip("[Burst] 基本攻撃のID")]
			public int BaseAtkId = 70;

			[Tooltip("[Burst] 基本攻撃02 のID")]
			public int BaseAtkCombo02 = 71;

			[Tooltip("[Burst] 基本攻撃03 のID")]
			public int BaseAtkCombo03 = 72;

			[Tooltip("[Burst] フルバ\u30fcスト動作のAttackID")]
			public int FullBurstAttackID = 73;

			[Tooltip("[Burst] 射撃構えのAttackID")]
			public int ReadyForShotID = 74;

			[Tooltip("[Burst] 初回射撃のAttackID")]
			public int FirstShotAttackID = 75;

			[Tooltip("[Burst] 連続射撃のAttackID")]
			public int NextShotAttackID = 76;

			[Tooltip("[Burst] 初回ReloadアクションのID")]
			public int FirstReloadActionAttackID = 77;

			[Tooltip("[Burst] 連続ReloadアクションのID")]
			public int NextReloadActionAttackID = 78;

			[Tooltip("[Burst] 兜割り(単発射撃へ接続可能)")]
			public int BurstAvoidAttackID = 79;

			[SerializeField]
			[Tooltip("[Burst] 距離減衰の最小減衰距離")]
			private float MinAttenuationDistance = 10f;

			[Tooltip("[Burst] 距離減衰の最大減衰距離")]
			[SerializeField]
			private float MaxAttenuationDistance = 1000f;

			[SerializeField]
			[Tooltip("[Burst] 距離減衰の最小ダメ\u30fcジレ\u30fcト")]
			private float MinAttenuationDmgRate = 0.01f;

			[Tooltip("[Burst] 距離減衰の最大ダメ\u30fcジレ\u30fcト")]
			[SerializeField]
			private float MaxAttenuationDmgRate = 1f;

			[Tooltip("[Burst] 距離減衰定義(X:正規化距離[0.0 1.0] Y:正規化ダメ\u30fcジ補正値[0.0 1.0]")]
			[SerializeField]
			private AnimationCurve AnimCurve = Curves.CreateEaseInCurve();

			[SerializeField]
			[Tooltip("[Burst] 射撃系の属性ダメ\u30fcジ倍率")]
			public float SingleShotBaseDmgRate = 1f;

			[Tooltip("[Burst] 射撃系の属性ダメ\u30fcジ倍率")]
			[SerializeField]
			public float SingleShotElementDmgRate = 2f;

			[SerializeField]
			[Tooltip("[Burst] 射撃系の属性ダメ\u30fcジ倍率")]
			public float FullBurstBaseDmgRate = 2f;

			[SerializeField]
			[Tooltip("[Burst] 射撃系の属性ダメ\u30fcジ倍率")]
			public float FullBurstElementDmgRate = 4f;

			[Tooltip("単発ショットのヒットエフェクト(属性差分あり")]
			public string[] HitEffect_SingleShot;

			[Tooltip("フルバ\u30fcストのヒットエフェクト(属性差分あり")]
			public string[] HitEffect_FullBurst;

			public float GetDistanceAttenuationRatio(float _distance)
			{
				if (_distance < 0f || AnimCurve == null)
				{
					return 0f;
				}
				if (_distance < MinAttenuationDistance)
				{
					return MaxAttenuationDmgRate;
				}
				if (_distance > MaxAttenuationDistance)
				{
					return MinAttenuationDmgRate;
				}
				float time = (_distance - MinAttenuationDistance) / (MaxAttenuationDistance - MinAttenuationDistance);
				float num = Mathf.Round(AnimCurve.Evaluate(time) * 100f) / 100f;
				return (MaxAttenuationDmgRate - MinAttenuationDmgRate) * num + MinAttenuationDmgRate;
			}
		}

		[Serializable]
		public class PairSwordsActionInfo
		{
			[Tooltip("ノ\u30fcマル：乱舞のActionId")]
			public int wildDanceAttackID = 89;

			[Tooltip("ノ\u30fcマル：溜めなし乱舞のActionId")]
			public int wildDanceNoneChargeAttackID = 88;

			[Tooltip("ノ\u30fcマル：乱舞の溜め完了SeId")]
			public int wildDanceChargeMaxSeId = 10000104;

			[Tooltip("双剣[ヒ\u30fcト]の連刃モ\u30fcド中攻撃&移動速度アップ倍率")]
			public float boostAttackAndMoveSpeedUpRate = 0.5f;

			[Tooltip("双剣[ヒ\u30fcト]の連刃モ\u30fcド中回避距離アップ倍率")]
			public float boostAvoidUpRate = 0.5f;

			[Tooltip("双剣[ヒ\u30fcト]のヒ\u30fcトゲ\u30fcジ減少値(毎秒)")]
			public float boostGaugeDecreasePerSecond = 65f;

			[Tooltip("双剣[ヒ\u30fcト]のヒ\u30fcトゲ\u30fcジ増加基礎値")]
			public float boostGaugeIncreaseBase = 20f;

			[Tooltip("双剣[ヒ\u30fcト]のダメ\u30fcジアップレベルを1つ上げるのに必要な攻撃回数")]
			public int boostDamageUpLevelUpHitCount = 7;

			[Tooltip("双剣[ヒ\u30fcト]のダメ\u30fcジアップレベル最大値")]
			public int boostDamageUpLevelMax = 4;

			[Tooltip("双剣[ヒ\u30fcト]のダメ\u30fcジアップレベル1ごとに上がる倍率")]
			public float boostDamageUpRatePerLevel = 1f;

			[Tooltip("[Heat] 通常攻撃のAttackId")]
			public int Heat_AttackId = 94;

			[Tooltip("[Soul] 通常攻撃のAttackId")]
			public int Soul_AttackId = 10;

			[Tooltip("[Soul] コンボ外連続攻撃のAttackId")]
			public int Soul_AttackNextId = 12;

			[Tooltip("[Soul] SP攻撃（レ\u30fcザ\u30fc待機）のAttackId")]
			public int Soul_SpLaserWaitAttackId = 92;

			[Tooltip("[Soul] SP攻撃（レ\u30fcザ\u30fc発射）のAttackId")]
			public int Soul_SpLaserShotAttackId = 93;

			[Tooltip("[Soul] 武器エフェクト名（無属性）")]
			public string Soul_EffectForWeapon = "ef_btl_wep04_s2";

			[Tooltip("[Soul] 武器エフェクト名（属性）")]
			public string[] Soul_EffectsForWeapon;

			[Tooltip("[Soul] 魔弾エフェクト名（無属性）")]
			public string Soul_EffectForBullet = "ef_btl_wsk2_twinsword_01_02";

			[Tooltip("[Soul] 魔弾エフェクト名（属性）")]
			public string[] Soul_EffectsForBullet;

			[Tooltip("[Soul] レ\u30fcザ\u30fc待機エフェクト名")]
			public string Soul_EffectForWaitingLaser = "ef_btl_wsk2_twinsword_02_01";

			[Tooltip("[Soul] コンボレベルごとのレ\u30fcザ\u30fcのattackInfo名")]
			public string[] Soul_AttackInfoNamesForLaserByComboLv;

			[Tooltip("[Soul] コンボレベルごとのレ\u30fcザ\u30fcの本数")]
			public int[] Soul_NumOfLaserByComboLv;

			[Tooltip("[Soul] レ\u30fcザ\u30fc複数時の半径")]
			public float Soul_RadiusForLaser = 0.6f;

			[Tooltip("[Soul] 魔弾ヒット時のソウルゲ\u30fcジ上昇値")]
			public float Soul_SoulGaugeIncreaseValueBySoulBullet;

			[Tooltip("[Soul] 魔弾ヒット後からソウルゲ\u30fcジ減少が始まるまでの時間（sec）")]
			public float Soul_TimeForGaugeDecreaseAfterHit = 2f;

			[Tooltip("[Soul] 魔弾ヒット後からソウルゲ\u30fcジ減少が始まるまでの時間（ゲ\u30fcジ100%時）（sec）")]
			public float Soul_TimeForGaugeDecreaseAfterHitOnComboLvMax = 5f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（/sec）")]
			public float Soul_GaugeDecreasePerSecond = 30f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（レ\u30fcザ\u30fc待機中）（/sec）")]
			public float Soul_GaugeDecreaseWaitingLaserPerSecond = 10f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（レ\u30fcザ\u30fc発射中）（/sec）")]
			public float Soul_GaugeDecreaseShootingLaserPerSecond = 200f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（被ダメ\u30fcジ時）")]
			public float Soul_GaugeDecreaseByDamage = 100f;

			[Tooltip("[Soul] コンボレベル数（1からカウント）")]
			public int Soul_NumOfComboLv = 4;

			[Tooltip("[Soul] それぞれのコンボレベルに必要なソウルゲ\u30fcジ量（%）")]
			public float[] Soul_GaugePercentForComboLv;

			[Tooltip("[Soul] コンボレベルごとの攻撃速度アップ倍率")]
			public float[] Soul_AttackSpeedUpRatesByComboLv;

			[Tooltip("[Soul] コンボレベルごとの魔弾AtkRate補正倍率")]
			public float[] Soul_AtkRatesForBulletByComboLv;

			[Tooltip("[Soul] コンボレベルごとのレ\u30fcザ\u30fcAtkRate補正倍率")]
			public float[] Soul_AtkRatesForLaserByComboLv;

			[Tooltip("[Soul] SEのid(0:開始,1:ル\u30fcプ,2:終了)")]
			public int[] Soul_SeIds;

			[Tooltip("[Soul] 開始のSEを再生してから何秒後にル\u30fcプのSEを再生するか")]
			public float Soul_TimeForPlayLoopSE;
		}

		[Serializable]
		public class ArrowActionInfo
		{
			[Tooltip("弓のライン色：通常")]
			public Color bulletLineColor = Color.yellow;

			[Tooltip("弓のライン色：ソウル")]
			public Color bulletLineColorSoul = Color.magenta;

			[Tooltip("弓のライン色：ソウル(Max)")]
			public Color bulletLineColorSoulFull = Color.red;

			[Tooltip("弓のライン参照のattackInfoの名前")]
			public string[] attackInfoNames;

			[Tooltip("弓のしゃがみ撃ち用attackInfo名")]
			public string[] attackInfoForSitShotNames;

			[Tooltip("影縫矢が刺さったエフェクト名")]
			public string shadowSealingEffectName;

			[Tooltip("影縫矢が消えるまでの秒数")]
			public float shadowSealingExistSec = 30f;

			[Tooltip("影縫矢が消えるまでの最低秒数")]
			public float shadowSealingExistMinSec = 2.5f;

			[Tooltip("影縫バフ時の弓短縮レ\u30fcト(1.0f以上はだめ)")]
			public float shadowSealingBuffChargeRate = 0.9f;

			[Tooltip("影縫バフ時の距離減衰上書き(0.0fで未使用)")]
			public float shadowSealingBuffDistanceRate = 1f;

			[Tooltip("しゃがみ撃ちの溜め短縮レ\u30fcト")]
			public float sitShotChargeSpeedUpRate;

			[Tooltip("しゃがみ撃ちの弾速アップレ\u30fcト")]
			public float sitShotBulletSpeedUpRate = 1f;

			[Tooltip("ヒ\u30fcトのしゃがみ撃ちの2本目以降射出角度")]
			public float sitShotSideAngle = 5f;

			[Tooltip("貫通のインタ\u30fcバル")]
			public float pierceInterval = 0.033f;

			[Tooltip("マ\u30fcカ\u30fcに当たった際、貫通継続するか")]
			public bool isPierceAfterTarget = true;

			[Tooltip("[SOUL]発射インタ\u30fcバル")]
			public float soulShotInterval = 0.05f;

			[Tooltip("[SOUL]射出Dir")]
			public Vector3[] soulShotDirs;

			[Tooltip("[SOUL]射出逆方向係数")]
			public float soulShotPosVec = 0.7f;

			[Tooltip("[SOUL]射出逆方向係数")]
			public float soulShotDirVec = 0.9f;

			[Tooltip("[SOUL]1部位ロック回数によるAtkRate")]
			public float[] soulLockNumAtkRate;

			[Tooltip("[SOUL]ロック順によるAtkRate基本値")]
			public float soulLockOrderAtkRateBase = 0.5f;

			[Tooltip("[SOUL]ロック順によるAtkRate係数")]
			public float soulLockOrderAtkRateCoefficient = 0.1f;

			[Tooltip("[SOUL]ロック順によるAtkRate最終ロック")]
			public float soulLockOrderAtkRateMax = 2f;

			[Tooltip("[SOUL]ロック順によるAtkRate最終ロック(boost)")]
			public float soulLockOrderAtkRateMaxBoost = 4f;

			[Tooltip("[SOUL]Raycastの長さ")]
			public float soulRaycastDistance = 60f;

			[Tooltip("[SOUL]最大ロックオン数")]
			public int soulLockMax = 6;

			[Tooltip("[SOUL]ブ\u30fcスト時の最大ロックオン数")]
			public int soulBoostLockMax = 12;

			[Tooltip("[SOUL]1部位辺りの最大ロックオン数")]
			public int soulLockRegionMax = 3;

			[Tooltip("[SOUL]同じ部位へのロックオン間隔")]
			public float soulLockRegionInterval = 0.5f;

			[Tooltip("[SOUL]同じ部位へのロックオン間隔(boost中)")]
			public float soulBoostLockRegionInterval = 0.4f;

			[Tooltip("[SOUL]ロック限界SE")]
			public int soulLockMaxSeId = 40000358;

			[Tooltip("[SOUL]ロックオンSE")]
			public int soulLockSeId = 20000018;

			[Tooltip("[SOUL]ソウルゲ\u30fcジ上昇値")]
			public float soulGaugeIncreaseValue = 50f;

			[Tooltip("[SOUL]ブ\u30fcスト中ゲ\u30fcジ減少値(毎秒)")]
			public float soulBoostGaugeDecreasePerSecond = 65f;

			[Tooltip("[SOUL]ブ\u30fcスト中の属性ダメ\u30fcジアップ")]
			public float soulBoostElementDamageRate = 10f;
		}

		[Serializable]
		public class OneHandSwordActionInfo
		{
			[Tooltip("[共通]ガ\u30fcド時のダメ\u30fcジ減少率")]
			public float Common_GuardDamageCutRate = 0.3f;

			[Tooltip("[共通]カウンタ\u30fc攻撃のマギチャ\u30fcジ割合")]
			public float Common_CounterHealSkillRate = 5f;

			[Tooltip("[共通]ジャストガ\u30fcドになる秒数")]
			public float Common_JustGuardValidSec = 0.66f;

			[Tooltip("[Normal]ガ\u30fcド時、未確定HP回復速度上昇率")]
			public float Normal_GuardingHealSpeedRate = 4f;

			[Tooltip("[Normal]ジャストガ\u30fcド時のダメ\u30fcジ減少率")]
			public float Normal_JustGuardDamageCutRate = 0.1f;

			[Tooltip("[Normal]蘇生時間倍率")]
			public float Normal_PrayBoostRate = 1.5f;

			[Tooltip("[Heat]ジャストガ\u30fcド時のマギ回復量")]
			public float Heat_JustGuardSkillHealValue = 100f;

			[Tooltip("[Heat]カウンタ\u30fc２段目のゲ\u30fcジ溜まる固定値")]
			public float Heat_RevengeCounterValue = 100f;

			[Tooltip("[Heat]ジャスガからのカウンタ\u30fc２段目のゲ\u30fcジ溜まる固定値")]
			public float Heat_RevengeJustCounterValue = 120f;

			[Tooltip("[Heat]ゲ\u30fcジ溜まる固定値")]
			public float Heat_RevengeValue = 400f;

			[Tooltip("[Heat]通常ガ\u30fcドのゲ\u30fcジ溜まる割合")]
			public float Heat_RevengeGuardRate = 6f;

			[Tooltip("[Heat]ジャストガ\u30fcドのゲ\u30fcジ溜まる割合")]
			public float Heat_RevengeJustGuardRate = 9f;

			[Tooltip("[Heat]リベンジアタックするために溜める時間")]
			public float Heat_RevengeAttackChargeSec = 1f;

			[Tooltip("[Soul] 魔爪攻撃の修正後AttackID")]
			public int Soul_AlteredSpAttackId = 94;

			[Tooltip("[Soul] 魔爪モ\u30fcド 突入時のSE")]
			public int Soul_BoostSeId = 20000106;

			[Tooltip("[Soul] 魔爪ヒット時のSE")]
			public int Soul_SnatchHitSeId = 20000106;

			[Tooltip("[Soul] スナッチヒットエフェクト")]
			public string Soul_SnatchHitEffect = "ef_btl_wsk2_sword_02_01";

			[Tooltip("[Soul] スナッチヒットエフェクト（ブ\u30fcストモ\u30fcド中）")]
			public string Soul_SnatchHitEffectOnBoostMode = "ef_btl_wsk2_sword_02_02";

			[Tooltip("[Soul] 魔爪の敵に残るエフェクト")]
			public string Soul_SnatchHitRemainEffect = "ef_btl_wsk2_sword_01";

			[Tooltip("[Soul] 魔爪モ\u30fcド 属性ヒットエフェクト")]
			public string[] Soul_BoostElementHitEffect;

			[Tooltip("[Soul] 引き寄せ時の到達点マ\u30fcジン")]
			public float Soul_MoveStopRange = 2f;

			[Tooltip("[Soul] 引き寄せ時の速度")]
			public float Soul_SnatchMoveVelocity = 60f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ上昇値")]
			public float Soul_ComboGaugeIncreaseValue = 100f;

			[Tooltip("[Soul] ジャストタップ ゲ\u30fcジ上昇割合")]
			public float Soul_JustTapGaugeRate = 1.5f;

			[Tooltip("[Soul] 魔爪モ\u30fcド ゲ\u30fcジ上昇割合")]
			public float Soul_BoostModeGaugeRate = 0.3f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（毎秒）")]
			public float Soul_BoostGaugeDecreasePerSecond = 65f;

			[Tooltip("[Soul] ソウルゲ\u30fcジ減少量（毎秒）（スナッチ中）")]
			public float Soul_BoostSnatchGaugeDecreasePerSecond = 10f;

			[Tooltip("[Soul] 魔爪モ\u30fcド 属性ダメ\u30fcジ倍率")]
			public float Soul_BoostElementDamageRate = 4f;

			[Tooltip("[Soul] 魔爪モ\u30fcド 突き攻撃のAttackInfo名")]
			public string Soul_BoostSpAttackName = "PLC00_attack_95_02";

			[Tooltip("[Soul] 魔爪モ\u30fcド 突き攻撃のダウン値")]
			public int Soul_BoostSpAttackDownValue = 300;

			[Tooltip("[Soul] 魔爪によるダウンゲ\u30fcジ減少速度ダウン倍率")]
			public float[] Soul_DownGaugeDecreaseRates;

			[Tooltip("[Soul] AnimCtrlステ\u30fcト移行タイムリミット")]
			public float Soul_AnimStateTimeLimit = 3f;

			[Tooltip("[Soul] AnimCtrl突進ル\u30fcプタイムリミット")]
			public float Soul_AnimStateTimeLimitForMoveLoop = 1f;

			[Tooltip("[Burst] バ\u30fcスト片手剣設定")]
			public BurstOneHandSwordActionInfo burstOHSInfo = new BurstOneHandSwordActionInfo();
		}

		[Serializable]
		public class BurstOneHandSwordActionInfo
		{
			[Tooltip("[Burst]ジャストガ\u30fcドになる秒数")]
			public float JustGuardValidSec = 0.66f;

			[Tooltip("[Burst] 破迅突き 属性ヒットエフェクト")]
			public string[] BoostElementHitEffect;

			[Tooltip("[Burst]破迅突きによる属性ダメ\u30fcジ倍率")]
			public float elementDamageRate = 2f;

			[Tooltip("[Burst]ブ\u30fcストゲ\u30fcジの秒数")]
			public float GaugeTime = 10f;

			[Tooltip("[Burst]ブ\u30fcスト中の攻撃速度上昇倍率")]
			public float boostAttackSpeedUpRate = 0.5f;

			[Tooltip("[Burst]ブ\u30fcスト中の属性ダメ\u30fcジアップ")]
			public float BoostElementDamageRate = 2f;

			[Tooltip("[Burst] 基本攻撃のID")]
			public int BaseAtkId = 18;

			[Tooltip("[Burst] 回避攻撃（シ\u30fcルドタックル）のID")]
			public int AvoidAttackID = 22;

			[Tooltip("[Burst] 破迅突きのID")]
			public int CounterAttackId = 93;
		}

		[Serializable]
		public class SpearActionInfo
		{
			[Tooltip("ヒ\u30fcト：歩く速度")]
			public float heatWalkSpeed = 0.1f;

			[Tooltip("百烈：ル\u30fcプの最大秒")]
			public float hundredLoopLimitSec = 3f;

			[Tooltip("百烈：連打とみなす間隔")]
			public float hundredTapIntervalSec = 0.4f;

			[Tooltip("突進：キャンセル可能時間")]
			public float rushCancellableTime = 0.3f;

			[Tooltip("突進：ル\u30fcプ時間")]
			public float rushLoopTime = 0.26f;

			[Tooltip("突進：回避が可能になるフレ\u30fcム")]
			public float rushCanAvoidTime = 0.1f;

			[Tooltip("突進：距離倍率")]
			public float rushDistanceRate = 1f;

			[Tooltip("突進：ル\u30fcプ版の特殊攻撃ID")]
			public int rushLoopAttackID = 96;

			[Tooltip("極め突き：できていいか")]
			public bool exRushEnable;

			[Tooltip("極め突き：開始有効時間")]
			public float exRushValidSec = 2f;

			[Tooltip("極め突き：溜め時間")]
			public float exRushChargeSec = 3f;

			[Tooltip("極め突き：属性ダメ\u30fcジ倍率最低値")]
			public float exRushElementDamageRateMin = 1.5f;

			[Tooltip("極め突き：属性ダメ\u30fcジ倍率最大値")]
			public float exRushElementDamageRateMax = 3f;

			[Tooltip("極め突き：属性ダメ\u30fcジ倍率フルチャ\u30fcジ")]
			public float exRushElementDamageRateFull = 6f;

			[Tooltip("極め突き：フルチャ\u30fcジのSE")]
			public int exRushChargeMaxSeId = 40000359;

			[Tooltip("ジャンプ：ゲ\u30fcジ増加基礎値")]
			public float jumpGaugeIncreaseBase = 10f;

			[Tooltip("ジャンプ：基礎溜め時間")]
			public float jumpChargeBaseSec = 2f;

			[Tooltip("ジャンプ：最小溜め時間")]
			public float jumpChargeMinSec = 0.5f;

			[Tooltip("ジャンプ：基礎溜め時間")]
			public int jumpChargeMaxSeId = 40000359;

			[Tooltip("ジャンプ：降下開始高さ")]
			public float jumpStartHeight = 7f;

			[Tooltip("ジャンプ：降下前ウェイト")]
			public float jumpFallWaitSec = 0.5f;

			[Tooltip("ジャンプ：降下スピ\u30fcド")]
			public float jumpFallSpeed = 80f;

			[Tooltip("ジャンプ：成功時の着地位置")]
			public float jumpRandingLength = 4f;

			[Tooltip("ジャンプ：属性ダメ\u30fcジ倍率")]
			public float[] jumpElementDamageRate;

			[Tooltip("ジャンプ：Lv2無属性ヒットエフェクト")]
			public string jumpHugeHitEffectName;

			[Tooltip("ジャンプ：Lv2属性ヒットエフェクト")]
			public string[] jumpHugeElementHitEffectNames;

			[Tooltip("ジャンプ：衝撃波のAttackInfoのPrefix")]
			public string jumpWaveAttackInfoPrefix;

			[Tooltip("ジャンプ：衝撃波のレベル別半径 lv0〜")]
			public float[] jumpWaveColliderRadius;

			[Tooltip("ジャンプ：衝撃波のレベル別半径 lv0〜")]
			public float[] jumpWaveScales;

			[Tooltip("ジャンプ：成功時のHitStop")]
			public float jumpHitStop = 0.05f;

			[Tooltip("ジャンプ：くるりんY制御開始時間")]
			public float jumpRandingHeightStartTime = 0.83f;

			[Tooltip("ジャンプ：くるりんY制御終了時間")]
			public float jumpRandingHeightEndTime = 1.2f;

			[Tooltip("ジャンプ：くるりんXZ制御開始時間")]
			public float jumpRandingMoveStartTime = 0.1f;

			[Tooltip("ジャンプ：くるりんXZ制御終了時間")]
			public float jumpRandingMoveEndTime = 1.3f;

			[Tooltip("[Soul] 移動速度")]
			public float Soul_WalkSpeedUpRate = 0.1f;

			[Tooltip("[Soul] 通常攻撃ID")]
			public int Soul_AttackId = 14;

			[Tooltip("[Soul] 特殊アクション攻撃ID")]
			public int Soul_SpAttackId = 95;

			[Tooltip("[Soul] 特殊アクション2撃目ID")]
			public int Soul_SpAttackContinueId = 94;

			[Tooltip("[Soul] 回避攻撃アクションID")]
			public int Soul_AvoidAttackId = 23;

			[Tooltip("[Soul] ゲ\u30fcジ上昇値")]
			public float Soul_GaugeIncreaseValue = 100f;

			[Tooltip("[Soul] ジャストタップ ゲ\u30fcジ上昇割合")]
			public float Soul_JustTapGaugeRate = 1.5f;

			[Tooltip("[Soul] 魔槍モ\u30fcド中 ゲ\u30fcジ上昇割合")]
			public float Soul_BoostModeGaugeRate = 0.3f;

			[Tooltip("[Soul] 魔槍モ\u30fcド中 ゲ\u30fcジ減少値 特殊アクション溜め中（毎秒）")]
			public float Soul_BoostModeGaugeDecreasePerSecond = 100f;

			[Tooltip("[Soul] 魔槍モ\u30fcド中 ゲ\u30fcジ減少値（毎秒）")]
			public float Soul_BoostModeGaugeDecreasePerSecondOnSpActionCharging = 50f;

			[Tooltip("[Soul] 魔槍モ\u30fcド中 ダメ\u30fcジアップ倍率")]
			public float Soul_BoostElementDamageRate = 4f;

			[Tooltip("[Soul] 特殊アクション最大溜め時エフェクトオフセット")]
			public Vector3 Soul_SpAttackMaxChargeEffectOffsetPos = new Vector3(0f, 0f, -0.35f);

			[Tooltip("[Soul] HP使用or回復に対応する攻撃ID")]
			public int[] Soul_AttackIdsForSacrifice;

			[Tooltip("[Soul] HP使用割合（%）")]
			public int[] Soul_SacrificeHPPercents;

			[Tooltip("[Soul] HP回復割合（%）")]
			public int[] Soul_HealHPPercents;
		}

		[Serializable]
		public class BarrierBrokenReaction
		{
			[Tooltip("リアクションタイプ")]
			public AttackHitInfo.ToPlayer.REACTION_TYPE reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;

			[Tooltip("リアクション時間")]
			public float loopTime = 0.3f;

			[Tooltip("吹き飛ばし力")]
			public float blowForce = 100f;

			[Tooltip("吹き飛ばし角度")]
			public float blowAngle = 30f;

			[Tooltip("無敵になる秒数")]
			public float invincibleDuration = 0.3f;
		}

		[Tooltip("武器情報")]
		public WeaponInfo[] weaponInfo;

		[Tooltip("最大HP")]
		public int hpMax = 30;

		[Tooltip("秒間HP回復量")]
		public int hpHealSpeed = 1;

		[Tooltip("ダメ\u30fcジに対する自動回復分割合")]
		public float damegeHealRate = 0.3f;

		[Tooltip("味方からのヒットリアクションを行うか")]
		public bool playerHitReactionValid = true;

		[Tooltip("救助可能秒数")]
		public float[] rescueTimes;

		[Tooltip("バリア内での救助速度倍率")]
		public float rescueSpeedRateInBarrier = 2.5f;

		[Tooltip("魔石復活可能秒数")]
		public float continueTime = 15f;

		[Tooltip("救助にかかる秒数")]
		public float revivalTime = 3f;

		[Tooltip("救助エリア範囲半径")]
		public float revivalRange = 2f;

		[Tooltip("魔石復活時の回復割合(0〜1)")]
		public float continueHealRate = 1f;

		[Tooltip("復活後の無敵時間（s")]
		public float deadStandupHitOffTime = 2f;

		[Tooltip("遠距離攻撃ライン表示")]
		public bool enableBulletLine;

		[Tooltip("UI高さ")]
		public float uiHeight;

		[Tooltip("テストスキルID")]
		public List<int> testSkillIDs;

		[Tooltip("テストスキル有効フラグ")]
		public bool enableTestSkill;

		[Tooltip("スキルゲ\u30fcジ毎秒回復量")]
		public float healSkillGaugePerSecond;

		[Tooltip("攻撃ヒット時のスキルゲ\u30fcジ回復量")]
		public float healSkillGaugeHit;

		[Tooltip("スキル効果範囲用のエフェクト名")]
		public string skillRangeEffectName;

		[Tooltip("振動のル\u30fcプ時間（秒")]
		public float shakeLoopTime = 2f;

		[Tooltip("タップによるスタン軽減時間（秒")]
		public float stunnedReduceTimeValue = 0.1f;

		[Tooltip("タップによるスタン軽減最大割合(0〜1)")]
		public float stunnedReduceTimeMaxRate = 1f;

		[Tooltip("スタン用エフェクト")]
		public string[] stunnedEffectList;

		[Tooltip("移動回転最大速度（角度/s")]
		public float moveRotateMaxSpeed = 720f;

		[Tooltip("バトル開始モ\u30fcション後の無敵時間（s")]
		public float battleStartHitOffTime = 2f;

		[Tooltip("プレイヤ\u30fc出現のモンスタ\u30fcとの距離")]
		public float appearPosDistance = 10f;

		[Tooltip("プレイヤ\u30fc出現位置のランダム試行回数")]
		public int appearPosTryCount = 8;

		[Tooltip("連戦時の最低HP割合（0〜1")]
		public float seriesMinimumHpRate = 0.3f;

		[Tooltip("キャラ出現時のエフェクト名")]
		public string battleStartEffectName;

		[Tooltip("武器切り替えの最低時間（秒")]
		public float changeWeaponMinTime = 1f;

		[Tooltip("武器切り替え時のエフェクト名")]
		public string changeWeaponEffectName;

		[Tooltip("足踏みエフェクト距離制限")]
		public float stampDistance = 15f;

		[Tooltip("敵や壁との摩擦")]
		public float friction = 0.2f;

		[Tooltip("GetAnimatorSpeedの最大TimeRate")]
		public float animatorSpeedMaxTimeRate = 0.9f;

		[Range(0f, 1f)]
		[Tooltip("MaxDamageDownRate")]
		public float maxDamageDownRate = 0.6f;

		[Tooltip("武器固有アクション情報")]
		public SpecialActionInfo specialActionInfo;

		[Tooltip("ベスト距離のヒットエフェクト名")]
		public string bestDistanceEffect = string.Empty;

		[Tooltip("両手剣固有アクション情報")]
		public TwoHandSwordActionInfo twoHandSwordActionInfo;

		[Tooltip("双剣固有アクション情報")]
		public PairSwordsActionInfo pairSwordsActionInfo;

		[Tooltip("弓固有アクション情報")]
		public ArrowActionInfo arrowActionInfo;

		[Tooltip("片手剣固有アクション情報")]
		public OneHandSwordActionInfo ohsActionInfo;

		[Tooltip("槍固有アクション情報")]
		public SpearActionInfo spearActionInfo;

		[Tooltip("バリアが壊れたときにとるリアクション")]
		public BarrierBrokenReaction barrierBrokenReaction;

		[NonSerialized]
		public List<AttackInfos> weaponAttackInfoList = new List<AttackInfos>();

		[NonSerialized]
		public AttackInfo[] attackInfosAll;
	}

	[Serializable]
	public class Evolve
	{
		[Serializable]
		public class GaugeInfo
		{
			public EQUIPMENT_TYPE type;

			public float value = 15f;
		}

		[Serializable]
		public class TypeAbstract
		{
			[Serializable]
			public class EvolveBuff
			{
				public BuffParam.BUFFTYPE type;

				public int value;
			}

			public float execSec = 15f;

			public int healValue;

			public HEAL_TYPE[] healTypes;

			public EvolveBuff[] buffs;
		}

		[Serializable]
		public class Type10000 : TypeAbstract
		{
			public float execTime = 15f;

			public float execEffectDelay = 0.86f;

			public int rushSeId = 20000106;

			public float rushDistanceRate = 0.5f;

			public float damageRateMin = 3f;

			public float damageRateMax = 6f;

			public float damageRateFull = 12f;
		}

		[Serializable]
		public class Type10001 : TypeAbstract
		{
			public float rangeUp = 2f;

			public float elementDamageRate = 4f;

			public float specialLoopSec = 1.5f;
		}

		public GaugeInfo[] gaugeInfo;

		public Type10000 type10000;

		public Type10001 type10001;
	}

	[Serializable]
	public class Enemy
	{
		[Tooltip("ヒットライト時間")]
		public float hitShockLightTime = 0.2f;

		[Tooltip("ヒットライト最大RimPower")]
		public float hitShockLightRimPower = 3f;

		[Tooltip("ヒットライト最大RimWidth")]
		public float hitShockLightRimWidth = 0.2f;

		[Tooltip("ヒットオフセット時間")]
		public float hitShockOffsetTime = 0.2f;

		[Tooltip("ヒットオフセット距離")]
		public float hitShockOffsetLength = 0.3f;

		[Tooltip("ダメ\u30fcジ数値表示")]
		public bool showDamageNum = true;

		[Tooltip("モンスタ\u30fcへの属性別ヒットSE")]
		public int[] elementHitSEIDs = new int[6];

		[Tooltip("麻痺ヒットエフェクト名")]
		public string paralyzeHitEffectName;

		[Tooltip("毒ヒットエフェクト名")]
		public string poisonHitEffectName;

		[Tooltip("他プレイヤ\u30fc用簡易ヒットエフェクト名")]
		public string otherSimpleHitEffectName;

		[Tooltip("ダウン値の上昇レ\u30fcト")]
		public float[] downMaxRate;

		[Tooltip("狙いマ\u30fcカ\u30fcの標準サイズ")]
		public float aimMarkerBaseRate = 1f;

		[Tooltip("狙いマ\u30fcカ\u30fcの当たり半径")]
		public float aimMarkerHitRadius = 1f;

		[Tooltip("AI基本待機時間")]
		public float baseAfterWaitTime = 0.5f;

		[Tooltip("Lv別AI待機時間の閾値配列")]
		public float[] afterWaitTimeThresholdsByLv;

		[Tooltip("Lv別AI待機時間の時間配列")]
		public float[] afterWaitTimesByLv;

		[Tooltip("押し合い速度（距離/s")]
		public float jostleSpeed = 2f;

		[Tooltip("ザコ同期時の位置ズレ許容距離")]
		public float lesserEnemiesPositionMargin = 5f;

		[Tooltip("ステ\u30fcタスUI表示距離")]
		public float showStatusUIRange = 5f;

		[Tooltip("常時Aim有効テスト")]
		public bool testAimTarget;

		[Tooltip("足踏みエフェクト距離制限")]
		public float stampDistance = 30f;

		[Tooltip("狙い当たり判定ヒット比較許容深さ")]
		public float hitCompareAimDepthLimit = 0.8f;

		[Tooltip("当たり判定ヒット距離チェック速度")]
		public float hitCompareSpeed = 10f;

		[Tooltip("当たり判定ヒット比較許容距離")]
		public float hitCompareLengthLimit = 2f;

		[Tooltip("突進最大距離レ\u30fcト")]
		public float dashMaxDistanceRate = 2f;

		[Tooltip("ゲストのザコのEnemyOut呼び出し遅延時間（秒")]
		public float guestEnemyOutTime = 1f;
	}

	[Serializable]
	public class DropItem
	{
		public AnimationCurve popAnim = Curves.CreateArcHalfCurve();

		public float popAnimTime = 1f;

		public float popHeight = 30f;

		public float popSpeed = 10f;

		public float rotationSpeed = 90f;

		public float defHeight = 1f;
	}

	[Serializable]
	public class FieldDropItem
	{
		public float getDistance = 1f;

		public float popAnimTime = 1f;

		public float popHight = 4f;

		public AnimationCurve popAnim = Curves.CreateArcHalfCurve();

		public float getAnimTime = 1f;

		public AnimationCurve distanceAnim = Curves.CreateEaseInCurve();

		public float rotateSpeed = 20f;

		public AnimationCurve rotateSpeedAnim = Curves.CreateEaseInCurve();

		public AnimationCurve scaleAnim = Curves.CreateEaseInCurve();

		public AnimEventData animEventData;

		[Tooltip("アイテムを落とす際の座標のオフセットの最大値")]
		public Vector3 offsetMin;

		[Tooltip("アイテムを落とす際の座標のオフセット最小値")]
		public Vector3 offsetMax;

		public string tresureBoxOpenEffect;
	}

	[Serializable]
	public class DropMaker
	{
		public Mesh mesh;

		public Material material;

		public float animSpeed = 30f;

		public Vector3 offset;

		public Vector3 rotOffset;

		public float portalHeight;
	}

	[Serializable]
	public class Room
	{
		[Tooltip("受付終了する敵のHP割合")]
		public float entryCloseEnemyHpRate = 0.5f;

		[Tooltip("受付終了する経過時間割合")]
		public float entryCloseTimeRate = 0.5f;

		[Tooltip("受付終了する経過時間割合")]
		public float checkCharacterSyncInterval = 10f;
	}

	[Serializable]
	public class InGameProgress
	{
		[Tooltip("パケット送信チェック時間")]
		public float checkCompleteSendTimeout = 10f;

		[Tooltip("終了時の通信待ち表示までの間")]
		public float waitNetworkMarginTime = 1f;

		[Tooltip("勝利演出後のリザルトまでの時間")]
		public float victoryIntervalTime = 10f;

		[Tooltip("オ\u30fcナ\u30fcの通信待ちタイムアウト時間")]
		public float waitCompleteOwnerTimeout = 10f;

		[Tooltip("NPC再チェック時間")]
		public float npcCheckIntervalTime = 5f;
	}

	[Serializable]
	public class Portal
	{
		[Serializable]
		public class PointEffect
		{
			[Tooltip("ポイント（普通）エフェクト名")]
			public string normalEffectName;

			[Tooltip("ポイント（大）エフェクト名")]
			public string largeEffectName;

			public float popHeightAnimTime = 1f;

			public float popHeight = 4f;

			public AnimationCurve popHeightAnim = Curves.CreateEaseInCurve();

			public float getSpeedAnimTime = 1f;

			public float getSpeed = 20f;

			public AnimationCurve getSpeedAnim = Curves.CreateEaseInCurve();

			public float targetHeight = 1.5f;
		}

		[Tooltip("ポ\u30fcタルエフェクト名")]
		public string[] effectNames;

		[Tooltip("ポイント取得エフェクト名")]
		public string pointGetEffectName;

		[Tooltip("アイコンエネルギ\u30fc段階数")]
		public int pointRankNum = 10;

		[Tooltip("解放時取得魔石数")]
		public int clearCrystalNum = 1;

		[Tooltip("ハ\u30fcド解放時取得魔石数")]
		public int clearHardCrystalNum = 5;

		public PointEffect pointEffect;
	}

	[Serializable]
	public class HappenQuestDirection
	{
		[Serializable]
		public class EnemyDisplayInfo
		{
			public int modelID;

			public string typeName;

			public string cameraNamePortrait;

			public Vector3 cameraOffsetPortrait;

			public string cameraNameLandscape;

			public Vector3 cameraOffsetLandscape;

			public Vector3 modelOffset;
		}

		[Tooltip("警告時間（秒")]
		public float warningTime = 3f;

		[Tooltip("確認UI表示時間（秒")]
		public float confirmUITime = 30f;

		[Tooltip("モンスタ\u30fc初期位置")]
		public Vector3 enemyInitPos = Vector3.zero;

		[Tooltip("モンスタ\u30fc初期向き（角度")]
		public float enemyInitDir;

		[CustomArray("typeName")]
		public EnemyDisplayInfo[] enemyDisplayInfos;

		[CustomArray("typeName")]
		public EnemyDisplayInfo[] enemyDisplayInfoForDefense;

		public EnemyDisplayInfo GetEnemyDisplayInfo(EnemyTable.EnemyData enemyData)
		{
			EnemyDisplayInfo enemyDisplayInfo = Array.Find(enemyDisplayInfos, (EnemyDisplayInfo o) => o.modelID == enemyData.modelId);
			if (enemyDisplayInfo == null)
			{
				string typeName = enemyData.type.ToString();
				enemyDisplayInfo = Array.Find(enemyDisplayInfos, (EnemyDisplayInfo o) => o.typeName == typeName);
			}
			return enemyDisplayInfo;
		}

		public EnemyDisplayInfo GetEnemyDisplayInfoByQuestType(EnemyTable.EnemyData enemyData, QUEST_STYLE questStyle)
		{
			EnemyDisplayInfo[] array = null;
			array = ((questStyle != QUEST_STYLE.DEFENSE) ? enemyDisplayInfos : enemyDisplayInfoForDefense);
			if (array.IsNullOrEmpty())
			{
				return null;
			}
			EnemyDisplayInfo enemyDisplayInfo = Array.Find(array, (EnemyDisplayInfo o) => o.modelID == enemyData.modelId);
			if (enemyDisplayInfo == null)
			{
				string typeName = enemyData.type.ToString();
				enemyDisplayInfo = Array.Find(array, (EnemyDisplayInfo o) => o.typeName == typeName);
			}
			return enemyDisplayInfo;
		}
	}

	[Serializable]
	public class UseResources
	{
		public string[] effects;

		public string[] uiEffects;
	}

	[Serializable]
	public class UIParam
	{
		[Tooltip("武器切り替え時に武器名が残る秒数")]
		public float weaponDecideRemainTime = 0.3f;
	}

	[Serializable]
	public class BuffParamInfo
	{
		[Tooltip("ダメ\u30fcジ吸収のパラメ\u30fcタ")]
		public AbsorbDamageParam absorbDamageParam;

		[Tooltip("自動復活可能回数")]
		public int autoReviveMaxCount = 1;

		[Tooltip("無効系のインタ\u30fcバル")]
		public float invincibleInterval = 2f;

		[Tooltip("デコイのヒットインタ\u30fcバル")]
		public float decoyHitInterval = 2f;
	}

	[Serializable]
	public class DebuffParam
	{
		[Tooltip("猛毒のパラメ\u30fcタ\u30fc")]
		public DeadlyPoisonParam deadlyPosion;

		[Tooltip("凍結のパラメ\u30fcタ\u30fc")]
		public FreezeParam freezeParam;

		[Tooltip("滑りデバフのパラメ\u30fcタ")]
		public SlideParam slideParam;

		[Tooltip("滑りデバフ（氷）のパラメ\u30fcタ")]
		public SlideParam slideIceParam;

		[Tooltip("沈黙デバフのパラメ\u30fcタ")]
		public SilenceParam silenceParam;

		[Tooltip("影縫デバフのパラメ\u30fcタ")]
		public ShadowSealingParam shadowSealingParam;

		[Tooltip("攻撃速度減少デバフのパラメ\u30fcタ")]
		public AttackSpeedDownParam attackSpeedDownParam;

		[Tooltip("沈黙デバフのパラメ\u30fcタ")]
		public CantHealHpParam cantHealHpParam;

		[Tooltip("暗闇デバフのパラメ\u30fcタ")]
		public BlindParam blindParam;

		[Tooltip("バフ解除で無視するBUFFTYPE")]
		public List<BuffParam.BUFFTYPE> ignoreBuffCancellation;

		[Tooltip("光輪のパラメ\u30fcタ\u30fc")]
		public LightRingParam lightRingParam;
	}

	[Serializable]
	public class MadModeParam
	{
		[Tooltip("マギダメ\u30fcジにかかる係数")]
		public float skillDamagedRate = 0.25f;

		[Tooltip("状態異常蓄積値にかかる係数")]
		public float badStatusRate = 0.25f;

		[Tooltip("状態異常耐性があがるだけのBUFFTYPE")]
		public List<BuffParam.BUFFTYPE> onlyResistDebuff;

		[Tooltip("魔狂化時に矢が抜けるか")]
		public bool isClearStuckArrow;
	}

	[Serializable]
	public class PassiveParam
	{
		[Tooltip("防御力閾値(この閾値を超えた分はダメ\u30fcジへの影響が弱くなる)")]
		public int playerDefenseThreshold = 1250;

		[Tooltip("変化係数(閾値を超えた防御力の影響を弱めるための係数)")]
		public int playerDefenseCoefficient = 700;
	}

	[Serializable]
	public class DeadlyPoisonParam
	{
		public float duration = 20f;

		public float interval = 2f;

		public float percent = 0.1f;
	}

	[Serializable]
	public class FreezeParam
	{
		public float duration = 7f;

		public float damageRate = 1.3f;
	}

	[Serializable]
	public class SlideParam
	{
		[Tooltip("継続時間")]
		public float duration = 20f;

		[Tooltip("最高速度に到達するまでの時間")]
		public float needTimeToMaxTime = 2.5f;

		[Tooltip("滑る時間")]
		public float slideTime = 1f;

		[Tooltip("回避した際の滑るスピ\u30fcド")]
		public float avoidSpeed = 4f;
	}

	[Serializable]
	public class SilenceParam
	{
		[Tooltip("継続時間")]
		public float duration = 20f;
	}

	[Serializable]
	public class CantHealHpParam
	{
		[Tooltip("継続時間")]
		public float duration = 20f;
	}

	[Serializable]
	public class BlindParam
	{
		[Tooltip("継続時間")]
		public float duration = 20f;
	}

	[Serializable]
	public class ShadowSealingParam
	{
		public float duration = 7f;

		public float minDuration = 2f;

		public float resistRate = 0.5f;

		public int startSeId = 10000082;

		public int loopSeId = 30000007;

		public int endSeId = 30000028;

		public bool isReactionDamage;
	}

	[Serializable]
	public class AttackSpeedDownParam
	{
		[Tooltip("継続時間")]
		public float duration = 10f;

		[Tooltip("減少値")]
		public int value = 50;

		[Tooltip("鈍敵エフェクトスケ\u30fcル")]
		public float enemyEffectSize = 1.5f;
	}

	[Serializable]
	public class LightRingParam
	{
		[Tooltip("継続時間")]
		public float duration = 9f;

		[Tooltip("光輪開始時のSE")]
		public int startSeId;

		[Tooltip("光輪中のSE")]
		public int loopSeId;

		[Tooltip("光輪終了時のSE")]
		public int endSeId;
	}

	[Serializable]
	public class AbsorbDamageParam
	{
		public int limitPlayerAbsorbDamage = 200;

		public int limitPlayerHitAbsorb = 200;

		public float limitRateEnemyAbsorbDamage = 0.1f;
	}

	[Serializable]
	public class AbilityParam
	{
		public float oneHandSwordRadiusCustomRate = 1f;
	}

	[Serializable]
	public class TutorialParam
	{
		[Tooltip("チュ\u30fcトリアルボスID")]
		public int enemyID = 110010911;

		[Tooltip("チュ\u30fcトリアルボスレベル")]
		public int enemyLv = 1;

		[Tooltip("一人で戦っている間の秒数、この秒数をすぎたら強制的にスキル説明")]
		public float soloBattleTimeLimit = 20f;

		[Tooltip("スキル打たずにこの時間が経過したら次のチュ\u30fcトリアルへ進む")]
		public float skillWaitLimitTime = 20f;

		[Tooltip("仲間と一緒い戦っている間の秒数")]
		public float battleWithFriendTime = 35f;

		[Tooltip("ボスの最小HPの割合(これ以下は減らない)")]
		public float bossMinHpRate = 0.2f;

		[Tooltip("ボス逃走開始HPの割合(bossMinHpRateより大きい値を設定すること)")]
		public float bossEscapeHpRate = 0.5f;
	}

	[Serializable]
	public class ArenaParam
	{
		[Tooltip("マギ蓄積速度ダウン基礎倍率")]
		public float magiSpeedDownRateBase = 0.5f;

		[Tooltip("マギ蓄積速度ダウン係数")]
		public float magiSpeedDownRate = 0.1f;

		[Tooltip("マギ蓄積速度ダウンの効果が効かなくなる限界突破数")]
		public float magiSpeedDownRegistSkillExceedLv = 5f;

		[Tooltip("マギ蓄積速度アップ基礎倍率")]
		public float magiSpeedUpBaseRate = 0.5f;
	}

	[Serializable]
	public class DefenseBattleParam
	{
		[Tooltip("耐久力")]
		public float defenseEndurance = 1000f;

		[Tooltip("戦闘開始時大型モンスタ\u30fc位置オフセット")]
		public Vector3 bossAppearOffsetPos = new Vector3(57f, 0f, 0f);

		[Tooltip("戦闘開始時大型モンスタ\u30fcY軸回転")]
		public float bossAppearAngleY;

		[Tooltip("耐久物の名称")]
		public string enduranceObjectName;
	}

	[Serializable]
	public class CannonParam
	{
		[Tooltip("速射砲のク\u30fcルタイム")]
		public float coolTimeForRapid = 0.1f;

		[Tooltip("速射砲の発射SEID")]
		public int seIdForRapid = 10000080;

		[Tooltip("迫撃砲のク\u30fcルタイム")]
		public float coolTimeForHeavy = 0.5f;

		[Tooltip("波動砲のチャ\u30fcジタイム")]
		public float chargeTimeMaxForSpecial = 2f;

		[Tooltip("波動砲のチャ\u30fcジSEID")]
		public int seIdForSpecialCharge = 10000099;

		[Tooltip("波動砲の発射SEID")]
		public int seIdForSpecial = 10000101;

		[Tooltip("波動砲の始動SEID")]
		public int seIdForSpecialOnBoard = 10000098;

		[Tooltip("波動砲のゲ\u30fcジ上昇SEID")]
		public int seIdForSpecialChargeMax = 10000100;

		[Tooltip("波動砲のカメラ切替ディレイ")]
		public float delayChangeCameraForSpecial = 0.5f;

		[Tooltip("フィ\u30fcルド砲のク\u30fcルタイム")]
		public float coolTimeForField = 0.1f;

		[Tooltip("フィ\u30fcルド砲の発射SEID")]
		public int seIdForField = 10000094;
	}

	[Serializable]
	public class WaveMatchParam
	{
		public enum eGaugeType
		{
			Normal,
			Zero,
			Rate,
			Constant
		}

		[Tooltip("WAVE_EVENTか")]
		public bool isEvent;

		[Tooltip("マギゲ\u30fcジ上昇タイプ")]
		public eGaugeType skillGaugeType = eGaugeType.Rate;

		[Tooltip("マギゲ\u30fcジ上昇量")]
		public float skillGaugeValue = 0.3f;

		[Tooltip("SP/アストラルゲ\u30fcジ上昇タイプ")]
		public eGaugeType spGaugeType = eGaugeType.Rate;

		[Tooltip("SP/アストラルゲ\u30fcジ上昇量")]
		public float spGaugeValue = 0.3f;

		[Tooltip("雑魚敵のダメ\u30fcジ")]
		public int enemyNormalDamage = 100;

		[Tooltip("ボス敵のダメ\u30fcジ")]
		public int enemyBossDamage = 500;

		[Tooltip("アニメ\u30fcション切り替わり")]
		public string targetChangeAnimEffect = "ef_btl_wyvern_downsmoke_01";

		[Tooltip("タ\u30fcゲットHitEffect")]
		public string targetHitEffect = "ef_btl_wyvern_downsmoke_01";

		[Tooltip("タ\u30fcゲットHitEffectSclae")]
		public Vector3 targetHitEffectScale;

		[Tooltip("タ\u30fcゲットHitSe")]
		public int targetHitSeId = 10000041;

		[Tooltip("タ\u30fcゲットBreakSe")]
		public int targetBreakSeId = 10000029;

		[Tooltip("Wave開始Se")]
		public int waveJingleId = 40000069;

		[Tooltip("ホストリタイヤのディレイ")]
		public float hostRetireDelay = 1f;

		[Tooltip("コンテンツ防衛戦の場合カメラをあげる")]
		public float cameraFieldOffsetY;
	}

	[Serializable]
	public class FishingParam
	{
		[Tooltip("予兆開始時間（0:min, 1:max)")]
		public float[] waitSec;

		[Tooltip("予兆インタ\u30fcバル")]
		public float omenInterval = 1f;

		[Tooltip("最大予兆回数")]
		public int maxOmenNum = 3;

		[Tooltip("！がでてる時間")]
		public float hookSec = 2f;

		[Tooltip("SE:予兆")]
		public int omenSeId = 40000019;

		[Tooltip("SE:！のSE")]
		public int hookSeId = 40000013;

		[Tooltip("SE:釣った(0:スカ、1:アイテム、2:魚、3:モンスタ\u30fc)")]
		public int[] hitSeIds;

		[Tooltip("通信モ\u30fcション：最低保証")]
		public float sendMinSec = 0.5f;

		[Tooltip("通信モ\u30fcション:釣った(0:スカ、1:アイテム、2:魚、3:モンスタ\u30fc)")]
		public float[] sendSec;

		[Tooltip("通信モ\u30fcション：冠タイプ(0:通常、1:銀、2:金")]
		public float[] sendCrownTypeSec;

		[Tooltip("通信モ\u30fcション：レアの時")]
		public float sendRareSec = 1f;

		[Tooltip("敵釣り時インフォメ\u30fcションディレイ")]
		public float delayEnemyFishing = 5f;

		[Tooltip("敵釣り時のアニメ\u30fcション時間")]
		public float hitEnemyMoveSec = 0.5f;
	}

	public const float BASE_FIXED_DELTA_TIME = 0.02f;

	public SelfController selfController;

	public EnemyController enemyController;

	public NpcController npcController;

	public StageObjectParam stageObject;

	public Character character;

	public Player player;

	public Evolve evolve;

	public Enemy enemy;

	public TargetMarkerSettings targetMarkerSettings;

	public TargetMarker targetMarker;

	public TargetMarker targetMarkerLesserEnemies;

	public TargetMarker targetMarkerArrowAimLesser;

	public DropItem dropItem;

	public FieldDropItem fieldDrop;

	public DropMaker dropMaker;

	public Room room;

	public InGameProgress inGameProgress;

	public Portal portal;

	public HappenQuestDirection happenQuestDirection;

	public UseResources useResourcesCommon;

	public UseResources useResourcesField;

	public UseResources useResourcesQuest;

	public UIParam uiparam;

	public BuffParamInfo buff;

	public DebuffParam debuff;

	public MadModeParam madModeParam;

	public PassiveParam passive;

	public AbilityParam abilityParam;

	public TutorialParam tutorialParam;

	public ArenaParam arenaParam;

	public DefenseBattleParam defenseBattleParam;

	public CannonParam cannonParam;

	public WaveMatchParam waveMatchParam;

	public WaveMatchParam waveMatchEventParam;

	public FishingParam fishingParam;

	public WaveMatchParam GetWaveMatchParam()
	{
		return (!QuestManager.IsValidInGameWaveMatch(true)) ? waveMatchParam : waveMatchEventParam;
	}
}
