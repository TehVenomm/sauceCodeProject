using rhyme;
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
		SHOT_SOUL_ARROW
	}

	public enum EFFECT_EXEC_CONDITION
	{
		NONE,
		DISABLE_BUFF
	}

	public delegate string EffectNameAnalyzer(string effect_name);

	public delegate Transform NodeFinder(string node_name);

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
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		if (id != ID.EFFECT && id != ID.EFFECT_ONESHOT && id != ID.EFFECT_STATIC && id != ID.EFFECT_LOOP_CUSTOM && id != ID.EFFECT_DEPEND_SP_ATTACK_TYPE && id != ID.EFFECT_DEPEND_WEAPON_ELEMENT && id != ID.EFFECT_SCALE_DEPEND_VALUE && id != ID.CAMERA_EFFECT)
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
			val = EffectManager.GetEffect(empty, val2);
			if (val == null)
			{
				Log.Warning("Failed to create effect!! " + empty);
			}
			else
			{
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
			}
			break;
		case ID.EFFECT_DEPEND_WEAPON_ELEMENT:
		{
			Player player2 = chara as Player;
			if (!(player2 == null))
			{
				int currentWeaponElement = player2.GetCurrentWeaponElement();
				if (currentWeaponElement >= 0 && currentWeaponElement < 6)
				{
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
				Vector3 val3 = localToWorldMatrix.MultiplyPoint3x4(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
				Quaternion val4 = val2.get_rotation() * Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
				pos = val3;
				rot = val4;
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
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
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
		}
		return val;
	}
}
