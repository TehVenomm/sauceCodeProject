using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventFormat
{
	public enum ID
	{
		SHOT_ARROW,
		ARROW_AIMABLE_START,
		ARROW_AIMABLE_END,
		COMBO_INPUT_ON,
		COMBO_INPUT_OFF,
		COMBO_TRANSITION_ON,
		CHARGE_INPUT_START,
		SKILL_CAST_LOOP_START,
		BLOW_CLEAR_INPUT_ON,
		BLOW_CLEAR_INPUT_OFF,
		BLOW_CLEAR_TRANSITION_ON,
		ROOT_MOTION_ON,
		ROOT_MOTION_OFF,
		ROOT_MOTION_MOVE_RATE,
		INVICIBLE_ON,
		INVICIBLE_OFF,
		SUPERARMOR_ON,
		SUPERARMOR_OFF,
		HIDE_RENDERER_ON,
		HIDE_RENDERER_OFF,
		HIDE_BASE_EFFECT_ON,
		HIDE_BASE_EFFECT_OFF,
		ACTION_RENDERER_ON,
		ACTION_RENDERER_OFF,
		SHAKE_CAMERA,
		EFFECT,
		EFFECT_ONESHOT,
		EFFECT_STATIC,
		EFFECT_DELETE,
		EFFECT_LOOP_CUSTOM,
		CAMERA_EFFECT,
		GROUP_EFFECT_ON,
		GROUP_EFFECT_OFF,
		UPDATE_ACTION_POSITION,
		UPDATE_DIRECTION,
		PERIODIC_SYNC_ACTION_POSITION_START,
		PERIODIC_SYNC_ACTION_POSITION_END,
		MOVE_FORWARD_START,
		MOVE_LEFT_START,
		MOVE_RIGHT_START,
		MOVE_FORWARD_TO_TARGET,
		MOVE_END,
		ROTATE_TO_TARGET_START,
		ROTATE_KEEP_TO_TARGET_START,
		ROTATE_TO_ANGLE_START,
		ROTATE_END,
		ROTATE_INPUT_ON,
		ROTATE_INPUT_OFF,
		STAMP,
		AUTO_STAMP_ON,
		AUTO_STAMP_OFF,
		REVIVE_REGION,
		DASH_START,
		WARP_VIEW_START,
		WARP_VIEW_END,
		WARP_TO_TARGET,
		WARP_TO_REVERSE_TARGET,
		WARP_TO_RANDOM,
		RADIAL_BLUR_START,
		RADIAL_BLUR_CHANGE,
		RADIAL_BLUR_END,
		SE_ONESHOT,
		SE_LOOP_PLAY,
		SE_LOOP_STOP,
		SE_SKILL_ONESHOT,
		VOICE,
		MOTION_CANCEL_ON,
		MOTION_CANCEL_OFF,
		ANIMATOR_BOOL_ON,
		ANIMATOR_BOOL_OFF,
		ATK_COLLIDER_CAPSULE,
		ATK_COLLIDER_CAPSULE_START,
		ATK_COLLIDER_CAPSULE_END,
		SHOT_GENERIC,
		SHOT_TARGET,
		SHOT_POINT,
		MOVE_SUPPRESS_ON,
		MOVE_SUPPRESS_OFF,
		CANCEL_TO_AVOID_ON,
		CANCEL_TO_AVOID_OFF,
		CANCEL_TO_MOVE_ON,
		CANCEL_TO_MOVE_OFF,
		CANCEL_TO_ATTACK_ON,
		CANCEL_TO_ATTACK_OFF,
		CANCEL_TO_SKILL_ON,
		CANCEL_TO_SKILL_OFF,
		CANCEL_TO_SPECIAL_ACTION_ON,
		CANCEL_TO_SPECIAL_ACTION_OFF,
		COUNTER_ATTACK_ON,
		COUNTER_ATTACK_OFF,
		REACTON_DELAY_ON,
		REACTON_DELAY_OFF,
		ENABLE_ANIM_RATE_ON,
		ENABLE_ANIM_RATE_OFF,
		WEAKPOINT_ON,
		WEAKPOINT_OFF,
		WEAKPOINT_ALL_ON,
		WEAKPOINT_ALL_OFF,
		FACE,
		DELETE_REMAIN_DMG_EFFECT,
		STUNNED_LOOP_START,
		BUFF_START,
		BUFF_END,
		CONTINUS_ATTACK,
		NWAY_LASER_ATTACK,
		FUNNEL_ATTACK,
		APPLY_SKILL_PARAM,
		APPLY_BLOW_FORCE,
		APPLY_CHANGE_WEAPON,
		APPLY_GATHER,
		TARGET_LOCK_ON,
		TARGET_LOCK_OFF,
		CHANGE_SHADER_PARAM,
		CANCEL_ACTION,
		RELEASE_GRAB,
		FLOATING_MINE_ATTACK,
		WEATHER_CHANGE,
		WEATHER_CHANGE_OFF,
		SHOT_RANDOM_AUTO,
		RECOVER_BARRIER_HP,
		RECOVER_BARRIER_HP_ALL,
		SHIELD_ON,
		PLAYER_DISABLE_MOVE,
		FIELD_QUEST_UI_OPEN,
		ESCAPE,
		DAMAGE_SHAKE_ON,
		DAMAGE_SHAKE_OFF,
		EXATK_COLLIDER_START,
		EXATK_COLLIDER_END,
		GENERATE_TRACKING,
		UNDEAD_ATTACK,
		ICE_FLOOR_CREATE,
		DIG_ATTACK,
		MOVE_SIDEWAYS_LOOK_TARGET,
		SHOT_LEAVE,
		PLAYER_FUNNEL_ATTACK,
		ACTION_MINE_ATTACK,
		SHOT_REFLECT_BULLET,
		SHOT_PRESENT,
		MOVE_POINT_DATA,
		RUSH_LOOP_START,
		SP_ATTACK_CONTINUE_ON,
		SP_ATTACK_CONTINUE_OFF,
		STATUS_UP_DEFENCE_ON,
		STATUS_UP_DEFENCE_OFF,
		TAIL_CONTROL_ON,
		TAIL_CONTROL_OFF,
		ACTION_MODE_ID_CHANGE,
		ANIMATION_LAYER_WEIGHT,
		ELEMENT_CHANGE,
		BLEND_COLOR_CHANGE,
		EFFECT_DEPEND_SP_ATTACK_TYPE,
		SHOT_ZONE,
		SHOT_NODE_LINK,
		TARGET_CHANGE_HATE_RANKING,
		TWO_HAND_SWORD_CHARGE_EXPAND_START,
		EFFECT_DEPEND_WEAPON_ELEMENT,
		SE_ONESHOT_DEPEND_WEAPON_ELEMENT,
		ELEMENT_ICON_CHANGE,
		WEAK_ELEMENT_ICON_CHANGE,
		SPEAR_HUNDRED_START,
		ATTACKHIT_CLEAR_ALL,
		ATTACKHIT_CLEAR_INFO,
		SPEAR_JUMP_CHARGE_START,
		SPEAR_JUMP_RIZE,
		SPEAR_JUMP_FALL_WAIT,
		RUSH_CAN_RELEASE,
		ATK_COLLIDER_CAPSULE_DEPEND_VALUE,
		EFFECT_SCALE_DEPEND_VALUE,
		ROOT_COLLIDER_ON,
		ROOT_COLLIDER_OFF,
		REGION_COLLIDER_ATK_HIT_ON,
		REGION_COLLIDER_ATK_HIT_OFF,
		COUNTER_ENABLED_ON,
		COUNTER_ENABLED_OFF,
		BOOSTCOMBO_TRANSITION_ON,
		TWO_HAND_SWORD_CHARGE_SOUL_START,
		SHOT_DECOY,
		BUFF_CANCELLATION,
		CAMERA_TARGET_OFFSET_ON,
		CAMERA_TARGET_OFFSET_OFF,
		DAMAGE_TO_ENDURANCE,
		MOVE_LOOKAT_DATA,
		SHOT_WORLD_POINT,
		MOVE_TO_WORLDPOS_START,
		EXECUTE_EVOLVE,
		SYNC_ACTION_TARGET,
		SKIP_TO_SKILL_ACTION_ON,
		SKIP_TO_SKILL_ACTION_OFF,
		CHANGE_SKILL_TO_SECOND_GRADE,
		CANCEL_TO_EVOLVE_SPECIAL_ACTION_ON,
		CANCEL_TO_EVOLVE_SPECIAL_ACTION_OFF,
		GENERATE_AEGIS,
		DBG_TIME_START,
		DBG_TIME_END,
		BLEND_COLOR_ON,
		BLEND_COLOR_OFF,
		CAMERA_STOP_ON,
		CAMERA_STOP_OFF,
		PAIR_SWORDS_SHOT_BULLET,
		PAIR_SWORDS_SHOT_LASER,
		PAIR_SWORDS_SOUL_EFFECT_START_SHOT_LASER,
		CANCEL_TO_ATTACK_NEXT_ON,
		CANCEL_TO_ATTACK_NEXT_OFF,
		ROTATE_TO_TARGET_POINT_START,
		REGION_NODE_ACTIVATE,
		REGION_NODE_DEACTIVATE,
		SHOT_HEALING_HOMING,
		SHOT_SOUL_ARROW,
		SPEAR_SOUL_HEAL_HP,
		CAMERA_CUT_ON,
		CAMERA_CUT_OFF,
		TWO_HAND_SWORD_BURST_BASE_ATK,
		TWO_HAND_SWORD_BURST_FULL_BURST,
		TWO_HAND_SWORD_BURST_READY_FOR_SHOT,
		TWO_HAND_SWORD_BURST_SINGLE_SHOT,
		TWO_HAND_SWORD_BURST_START_RELOAD,
		TWO_HAND_SWORD_BURST_RELOAD_NOW,
		TWO_HAND_SWORD_BURST_RELOAD_DONE,
		THS_BURST_RELOAD_VARIABLE_SPEED_ON,
		THS_BURST_RELOAD_VARIABLE_SPEED_OFF,
		EFFECT_SWITCH_OBJECT_BY_CONDITION,
		EFFECT_TILING,
		ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI,
		THS_BURST_TRANSITION_AVOID_ATK_ON,
		THS_BURST_TRANSITION_AVOID_ATK_OFF,
		GATHER_GIMMICK_GET,
		LOAD_BULLET,
		TRACKING_BULLET_OFF,
		ENEMY_RECOVER_HP,
		FLICK_ACTION_ON,
		FLICK_ACTION_OFF,
		NEXTTRIGGER_INPUT_ON,
		NEXTTRIGGER_INPUT_OFF,
		NEXTTRIGGER_TRANSITION_ON,
		COUNT_LONGTOUCH_ON,
		TRANSITION_LONGTOUCH,
		COMBINE_BURST_PAIRSWORD,
		AERIAL_ON,
		AERIAL_OFF,
		FISHING_SE_PLAY,
		FISHING_SE_STOP,
		SHOT_RESURRECTION_HOMING,
		WEAPON_ACTION_START,
		WEAPON_ACTION_END,
		SET_CONDITION_TRIGGER,
		SET_CONDITION_TRIGGER_2,
		FIX_POSITION_WEAPON_R_ON,
		FIX_POSITION_WEAPON_R_OFF,
		SPEAR_BURST_BARRIER_OFF,
		WEAPON_ACTION_ENABLED_ON,
		WEAPON_ACTION_ENABLED_OFF,
		SUMMON_ENEMY,
		SUMMON_ATTACK,
		ENEMY_DEAD_REVIVE,
		BUFF_START_SHIELD_REFLECT,
		RAIN_SHOT_CHARGE_START,
		SHOT_ARROW_RAIN,
		EFFECT_ONESHOT_ON_RAIN_SHOT_POS,
		THS_ORACLE_HORIZONTAL_START,
		THS_ORACLE_HORIZONTAL_NEXT_CHECK,
		THS_ORACLE_VERNIER_EFFECT_ON,
		THS_ORACLE_VERNIER_EFFECT_OFF,
		START_CARRY_GIMMICK,
		END_CARRY_GIMMICK,
		ENABLE_CARRY_PUT_ON,
		ENABLE_CARRY_PUT_OFF,
		PLAYER_BLEEDING_DAMAGE,
		PLAYER_TELEPORT_AVOID_ON,
		PLAYER_TELEPORT_AVOID_OFF,
		PLAYER_TELEPORT_TO_TARGET_OFFSET,
		CAMERA_RESET_POSITION,
		SET_EXTRA_SP_GAUGE_DECREASING_RATE,
		NEXT_IF_BOOST_MODE,
		NEXT_IF_NOT_BOOST_MODE,
		ROTATE_TO_TARGET_OFFSET,
		ENEMY_ASSIMILATION,
		ENEMY_DISSIMILATION,
		SKIP_BY_DAMAGE_ON,
		SKIP_BY_DAMAGE_OFF,
		CAMERA_TARGET_ROTATE_ON,
		CAMERA_TARGET_ROTATE_OFF,
		ORACLE_SPEAR_GUARD_ON,
		ORACLE_SPEAR_GUARD_OFF,
		SHOT_ORACLE_SPEAR_SP,
		DESTROY_ORACLE_SPEAR_SP,
		SHOT_ORACLE_PAIR_SWORDS_RUSH,
		PLAYER_RUSH_AVOID_ON,
		PLAYER_RUSH_AVOID_OFF,
		ORACLE_PAIR_SWORDS_SP_START,
		ORACLE_PAIR_SWORDS_SP_END,
		ACTION_RECEIVE_DAMAGE_RATE
	}

	public enum EFFECT_EXEC_CONDITION
	{
		NONE,
		DISABLE_BUFF
	}

	public enum EFFECT_SWITCH_CONDITION
	{
		NONE,
		BURST_REST_BULLET_COUNT
	}

	public enum EFFECT_TILING_PATTERN
	{
		NONE,
		CIRCLE,
		MATRIX,
		AS_BURST_BULLET_UI,
		AS_BURST_SHOTGUN
	}

	public enum MULTI_COL_GENERATE_CONDITION
	{
		NONE,
		IN_CORN
	}

	public delegate string EffectNameAnalyzer(string effect_name);

	public delegate Transform NodeFinder(string node_name);

	public class GenerateEffectParam
	{
		public string EffectName = string.Empty;

		public Transform EffectParent;

		public bool IsDependOnWeaponElement;

		public Character Chara;

		public float EffectScale = 1f;

		public AnimEventData.EventData Data;
	}

	private static Transform CreateCameraEffect(string effect_name, float effect_scale, AnimEventData.EventData data)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return null;
		}
		Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
		if (cameraTransform == null)
		{
			return null;
		}
		Transform effect = EffectManager.GetEffect(effect_name, cameraTransform);
		if (effect == null)
		{
			return null;
		}
		Vector3 localScale = effect.get_localScale();
		effect.set_localScale(localScale * effect_scale);
		if (data.floatArgs.Length >= 7)
		{
			effect.set_localPosition(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
			effect.set_localRotation(Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6])));
		}
		return effect;
	}

	public static Transform EffectEventExec(ID id, AnimEventData.EventData data, Transform transform, bool is_oneshot_priority, EffectNameAnalyzer name_analyzer = null, NodeFinder node_finder = null, Character chara = null)
	{
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		if (id != ID.EFFECT && id != ID.EFFECT_ONESHOT && id != ID.EFFECT_STATIC && id != ID.EFFECT_LOOP_CUSTOM && id != ID.EFFECT_DEPEND_SP_ATTACK_TYPE && id != ID.EFFECT_DEPEND_WEAPON_ELEMENT && id != ID.EFFECT_SCALE_DEPEND_VALUE && id != ID.CAMERA_EFFECT && id != ID.EFFECT_SWITCH_OBJECT_BY_CONDITION && id != ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS)
		{
			return null;
		}
		Transform val = null;
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (id == ID.EFFECT_DEPEND_SP_ATTACK_TYPE)
		{
			if (object.ReferenceEquals(chara, null))
			{
				return null;
			}
			Player player = chara as Player;
			if (object.ReferenceEquals(player, null))
			{
				return null;
			}
			int num = (int)(1 + player.spAttackType);
			if (num >= data.stringArgs.Length)
			{
				return null;
			}
			empty = data.stringArgs[num];
			empty2 = data.stringArgs[0];
		}
		else
		{
			empty = data.stringArgs[0];
			empty2 = ((data.stringArgs.Length <= 1) ? string.Empty : data.stringArgs[1]);
		}
		if (name_analyzer != null)
		{
			empty = name_analyzer(empty);
			if (empty == null)
			{
				return null;
			}
		}
		int num2 = data.floatArgs.Length;
		float num3 = (num2 <= 0) ? 1f : data.floatArgs[0];
		if (id == ID.CAMERA_EFFECT)
		{
			return CreateCameraEffect(empty, num3, data);
		}
		Transform val2 = (node_finder != null) ? node_finder(empty2) : ((!string.IsNullOrEmpty(empty2)) ? Utility.Find(transform, empty2) : transform);
		switch (id)
		{
		case ID.EFFECT:
		case ID.EFFECT_STATIC:
		case ID.EFFECT_LOOP_CUSTOM:
		case ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
		case ID.EFFECT_SCALE_DEPEND_VALUE:
		{
			val = EffectManager.GetEffect(empty, val2);
			if (val == null)
			{
				Log.Warning("Failed to create effect!! " + empty);
				break;
			}
			if (id == ID.EFFECT_SCALE_DEPEND_VALUE && !object.ReferenceEquals(chara, null))
			{
				num3 = chara.GetEffectScaleDependValue();
			}
			Vector3 localScale2 = val.get_localScale();
			val.set_localScale(localScale2 * num3);
			if (num2 >= 7)
			{
				val.set_localPosition(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
				val.set_localRotation(Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6])));
			}
			break;
		}
		case ID.EFFECT_DEPEND_WEAPON_ELEMENT:
		{
			Player player3 = chara as Player;
			if (player3 == null)
			{
				break;
			}
			int currentWeaponElement = player3.GetCurrentWeaponElement();
			if (currentWeaponElement < 0 || currentWeaponElement >= 6)
			{
				break;
			}
			val = EffectManager.GetEffect(empty + currentWeaponElement.ToString(), val2);
			if (!(val == null))
			{
				Vector3 localScale = val.get_localScale();
				val.set_localScale(localScale * num3);
				if (num2 >= 7)
				{
					val.set_localPosition(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
					val.set_localRotation(Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6])));
				}
			}
			break;
		}
		case ID.EFFECT_ONESHOT:
		{
			Vector3 pos;
			Quaternion rot;
			if (num2 >= 7)
			{
				Matrix4x4 localToWorldMatrix = val2.get_localToWorldMatrix();
				Vector3 val5 = localToWorldMatrix.MultiplyPoint3x4(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
				Quaternion val6 = val2.get_rotation() * Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
				pos = val5;
				rot = val6;
			}
			else
			{
				pos = val2.get_position();
				rot = val2.get_rotation();
			}
			if (val2.get_gameObject().get_activeInHierarchy())
			{
				EffectManager.OneShot(empty, pos, rot, val2.get_lossyScale() * num3, is_oneshot_priority, delegate(Transform effect)
				{
					rymFX component = effect.get_gameObject().GetComponent<rymFX>();
					if (component != null)
					{
						component.LoopEnd = true;
						component.AutoDelete = true;
					}
				});
			}
			break;
		}
		case ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
		{
			GenerateEffectParam generateEffectParam = new GenerateEffectParam();
			generateEffectParam.EffectName = empty;
			generateEffectParam.EffectParent = val2;
			generateEffectParam.Chara = chara;
			generateEffectParam.EffectScale = num3;
			generateEffectParam.Data = data;
			GenerateEffectParam param = generateEffectParam;
			val = GetSwichedEffect(param);
			break;
		}
		case ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
		{
			Player player2 = chara as Player;
			if (!(player2 == null))
			{
				Vector3 val3 = player2.rainShotFallPosition;
				Quaternion val4 = Quaternion.Euler(new Vector3(0f, player2.rainShotFallRotateY, 0f));
				if (num2 >= 7)
				{
					val3 += new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]);
					val4 *= Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
				}
				EffectManager.OneShot(empty, val3, val4, Vector3.get_one() * num3, is_oneshot_priority, delegate(Transform effect)
				{
					rymFX component2 = effect.get_gameObject().GetComponent<rymFX>();
					if (component2 != null)
					{
						component2.LoopEnd = true;
						component2.AutoDelete = true;
					}
				});
			}
			break;
		}
		}
		return val;
	}

	public static Transform[] EffectsEventExec(ID id, AnimEventData.EventData data, Transform transform, bool is_oneshot_priority, EffectNameAnalyzer name_analyzer = null, NodeFinder node_finder = null, Character chara = null)
	{
		if (id != ID.EFFECT_TILING)
		{
			return null;
		}
		if (data == null || data.stringArgs == null)
		{
			return null;
		}
		List<Transform> list = new List<Transform>();
		string text = (data.stringArgs.Length <= 0) ? string.Empty : data.stringArgs[0];
		string text2 = (data.stringArgs.Length <= 1) ? string.Empty : data.stringArgs[1];
		if (name_analyzer != null)
		{
			text = name_analyzer(text);
			if (text == null)
			{
				return null;
			}
		}
		int num = data.floatArgs.Length;
		float effectScale = (num <= 0) ? 1f : data.floatArgs[0];
		Transform effectParent = (node_finder != null) ? node_finder(text2) : ((!string.IsNullOrEmpty(text2)) ? Utility.Find(transform, text2) : transform);
		if (id == ID.EFFECT_TILING)
		{
			GenerateEffectParam generateEffectParam = new GenerateEffectParam();
			generateEffectParam.EffectName = text;
			generateEffectParam.EffectParent = effectParent;
			generateEffectParam.Chara = chara;
			generateEffectParam.EffectScale = effectScale;
			generateEffectParam.Data = data;
			GenerateEffectParam param = generateEffectParam;
			list = GetMultiEffect(param);
		}
		return list?.ToArray();
	}

	private static Transform GetSwichedEffect(GenerateEffectParam _param)
	{
		Player player = _param.Chara as Player;
		if (player == null)
		{
			return null;
		}
		Transform paramGeneratedEffect = GetParamGeneratedEffect(_param);
		EffectCtrl component = paramGeneratedEffect.GetComponent<EffectCtrl>();
		if (component == null)
		{
			return paramGeneratedEffect;
		}
		if (_param.Data.intArgs != null && _param.Data.intArgs.Length > 2)
		{
			EFFECT_SWITCH_CONDITION eFFECT_SWITCH_CONDITION = (EFFECT_SWITCH_CONDITION)_param.Data.intArgs[2];
			if (eFFECT_SWITCH_CONDITION == EFFECT_SWITCH_CONDITION.BURST_REST_BULLET_COUNT && player.thsCtrl != null && player.thsCtrl.IsRequiredReloadAction())
			{
				component.Play("LOOP2");
			}
		}
		return paramGeneratedEffect;
	}

	private static List<Transform> GetMultiEffect(GenerateEffectParam _param)
	{
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		if (_param == null || _param.Chara == null || _param.Data.intArgs == null || _param.Data.intArgs.Length < 7 || _param.Data.floatArgs == null || _param.Data.floatArgs.Length < 13)
		{
			return null;
		}
		Player player = _param.Chara as Player;
		if (player == null)
		{
			return null;
		}
		int num = _param.Data.intArgs[4];
		int num2 = _param.Data.intArgs[5];
		int num3 = _param.Data.intArgs[6];
		Vector3 intervalPos = default(Vector3);
		intervalPos._002Ector(_param.Data.floatArgs[7], _param.Data.floatArgs[8], _param.Data.floatArgs[9]);
		Vector3 intervalRot = default(Vector3);
		intervalRot._002Ector(_param.Data.floatArgs[10], _param.Data.floatArgs[11], _param.Data.floatArgs[12]);
		int num4 = num;
		EFFECT_TILING_PATTERN eFFECT_TILING_PATTERN = (EFFECT_TILING_PATTERN)_param.Data.intArgs[2];
		switch (eFFECT_TILING_PATTERN)
		{
		case EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI:
			if (player.thsCtrl != null)
			{
				num2 = player.thsCtrl.CurrentMaxBulletCount;
				num = 1;
				num3 = 1;
			}
			break;
		case EFFECT_TILING_PATTERN.AS_BURST_SHOTGUN:
			if (player.thsCtrl != null)
			{
				num2 = player.thsCtrl.CurrentRestBulletCount;
				num = 1;
				num3 = 1;
			}
			break;
		}
		int totalCount = num * num2 * num3;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < num3; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					Transform paramGeneratedEffect = GetParamGeneratedEffect(_param);
					if (!(paramGeneratedEffect == null))
					{
						list.Add(paramGeneratedEffect);
						SetEffectState(paramGeneratedEffect, k, j, i, totalCount, eFFECT_TILING_PATTERN, player);
						SetTransformInfo(paramGeneratedEffect, k, j, i, totalCount, eFFECT_TILING_PATTERN, intervalPos, intervalRot, player);
					}
				}
			}
		}
		return list;
	}

	private static void SetEffectState(Transform _tr, int i, int j, int k, int totalCount, EFFECT_TILING_PATTERN _pattern, Player _player)
	{
		EffectCtrl component = _tr.GetComponent<EffectCtrl>();
		if (component == null)
		{
			return;
		}
		int num = (i + 1) * (j + 1) * (k + 1);
		if (_pattern == EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI)
		{
			if (totalCount - num < _player.thsCtrl.CurrentRestBulletCount)
			{
				component.Play("LOOP1");
			}
			else
			{
				component.Play("LOOP2");
			}
		}
	}

	private static void SetTransformInfo(Transform _tr, int i, int j, int k, int totalCount, EFFECT_TILING_PATTERN _pattern, Vector3 _intervalPos, Vector3 _intervalRot, Player _player)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		switch (_pattern)
		{
		case EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI:
		{
			_tr.set_localPosition(_tr.get_localPosition() + new Vector3(_intervalPos.x * (float)i, _intervalPos.y * (float)j, _intervalPos.z * (float)k));
			float num4 = (totalCount % 2 != 1) ? ((float)totalCount / 2f + 0.5f) : ((float)totalCount / 2f);
			num4 -= 1f;
			_tr.Rotate(_intervalRot.x * ((float)i - num4), _intervalRot.y * ((float)j - num4), _intervalRot.z * ((float)k - num4));
			break;
		}
		case EFFECT_TILING_PATTERN.AS_BURST_SHOTGUN:
		{
			float num = Random.Range(_intervalPos.x, _intervalPos.x);
			float num2 = Random.Range(_intervalPos.y, _intervalPos.y);
			float num3 = Random.Range(0f, 360f) * ((float)Math.PI / 180f);
			Vector3 right = _player.get_transform().get_right();
			Vector3 forward = _player.get_transform().get_forward();
			_tr.set_localPosition(_tr.get_localPosition() + (num * Mathf.Cos(num3) * right + num2 * Mathf.Sin(num3) * forward));
			break;
		}
		}
	}

	private static Transform GetParamGeneratedEffect(GenerateEffectParam _param)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		Transform val = null;
		Player player = _param.Chara as Player;
		if (player == null)
		{
			return null;
		}
		bool flag = _param.Data.intArgs != null && _param.Data.intArgs.Length > 3 && _param.Data.intArgs[3] == 1;
		int currentWeaponElement = player.GetCurrentWeaponElement();
		val = ((!flag || 0 > currentWeaponElement || currentWeaponElement >= 6) ? EffectManager.GetEffect(_param.EffectName, _param.EffectParent) : EffectManager.GetEffect($"{_param.EffectName}{currentWeaponElement:D2}", _param.EffectParent));
		if (val == null)
		{
			return val;
		}
		Vector3 localScale = val.get_localScale();
		val.set_localScale(localScale * _param.EffectScale);
		if (_param.Data.floatArgs.Length >= 7)
		{
			val.set_localPosition(new Vector3(_param.Data.floatArgs[1], _param.Data.floatArgs[2], _param.Data.floatArgs[3]));
			val.set_localRotation(Quaternion.Euler(new Vector3(_param.Data.floatArgs[4], _param.Data.floatArgs[5], _param.Data.floatArgs[6])));
		}
		return val;
	}
}
