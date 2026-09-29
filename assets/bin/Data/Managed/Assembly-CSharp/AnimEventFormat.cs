// Decompiled with JetBrains decompiler
// Type: AnimEventFormat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AnimEventFormat
{
  private static Transform CreateCameraEffect(
    string effect_name,
    float effect_scale,
    AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return (Transform) null;
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    if (Object.op_Equality((Object) cameraTransform, (Object) null))
      return (Transform) null;
    Transform effect = EffectManager.GetEffect(effect_name, cameraTransform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return (Transform) null;
    Vector3 localScale = effect.localScale;
    effect.localScale = Vector3.op_Multiply(localScale, effect_scale);
    if (data.floatArgs.Length >= 7)
    {
      effect.localPosition = new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]);
      effect.localRotation = Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
    }
    return effect;
  }

  public static Transform EffectEventExec(
    AnimEventFormat.ID id,
    AnimEventData.EventData data,
    Transform transform,
    bool is_oneshot_priority,
    AnimEventFormat.EffectNameAnalyzer name_analyzer = null,
    AnimEventFormat.NodeFinder node_finder = null,
    Character chara = null)
  {
    if (id != AnimEventFormat.ID.EFFECT && id != AnimEventFormat.ID.EFFECT_ONESHOT && id != AnimEventFormat.ID.EFFECT_STATIC && id != AnimEventFormat.ID.EFFECT_LOOP_CUSTOM && id != AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE && id != AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT && id != AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE && id != AnimEventFormat.ID.CAMERA_EFFECT && id != AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION && id != AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS)
      return (Transform) null;
    Transform transform1 = (Transform) null;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string effect_name;
    string str;
    if (id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE)
    {
      if (chara == null)
        return (Transform) null;
      if (!(chara is Player player))
        return (Transform) null;
      int index = (int) (1 + player.spAttackType);
      if (index >= data.stringArgs.Length)
        return (Transform) null;
      effect_name = data.stringArgs[index];
      str = data.stringArgs[0];
    }
    else
    {
      effect_name = data.stringArgs[0];
      str = data.stringArgs.Length > 1 ? data.stringArgs[1] : string.Empty;
    }
    if (name_analyzer != null)
    {
      effect_name = name_analyzer(effect_name);
      if (effect_name == null)
        return (Transform) null;
    }
    int length = data.floatArgs.Length;
    float effect_scale = length > 0 ? data.floatArgs[0] : 1f;
    if (id == AnimEventFormat.ID.CAMERA_EFFECT)
      return AnimEventFormat.CreateCameraEffect(effect_name, effect_scale, data);
    Transform parent = node_finder == null ? (!string.IsNullOrEmpty(str) ? Utility.Find(transform, str) : transform) : node_finder(str);
    switch (id)
    {
      case AnimEventFormat.ID.EFFECT:
      case AnimEventFormat.ID.EFFECT_STATIC:
      case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
      case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
      case AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE:
        transform1 = EffectManager.GetEffect(effect_name, parent);
        if (Object.op_Equality((Object) transform1, (Object) null))
        {
          Log.Warning("Failed to create effect!! " + effect_name);
          break;
        }
        if (id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE && chara != null)
          effect_scale = chara.GetEffectScaleDependValue();
        Vector3 localScale1 = transform1.localScale;
        transform1.localScale = Vector3.op_Multiply(localScale1, effect_scale);
        if (length >= 7)
        {
          transform1.localPosition = new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]);
          transform1.localRotation = Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
          break;
        }
        break;
      case AnimEventFormat.ID.EFFECT_ONESHOT:
        Vector3 pos1;
        Quaternion rot1;
        if (length >= 7)
        {
          Matrix4x4 localToWorldMatrix = parent.localToWorldMatrix;
          Vector3 vector3 = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
          Quaternion quaternion = Quaternion.op_Multiply(parent.rotation, Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6])));
          pos1 = vector3;
          rot1 = quaternion;
        }
        else
        {
          pos1 = parent.position;
          rot1 = parent.rotation;
        }
        if (((Component) parent).gameObject.activeInHierarchy)
        {
          EffectManager.OneShot(effect_name, pos1, rot1, Vector3.op_Multiply(parent.lossyScale, effect_scale), is_oneshot_priority, (Action<Transform>) (effect =>
          {
            rymFX component = ((Component) effect).gameObject.GetComponent<rymFX>();
            if (!Object.op_Inequality((Object) component, (Object) null))
              return;
            component.LoopEnd = true;
            component.AutoDelete = true;
          }));
          break;
        }
        break;
      case AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT:
        Player player1 = chara as Player;
        if (!Object.op_Equality((Object) player1, (Object) null))
        {
          int currentWeaponElement = player1.GetCurrentWeaponElement();
          if (currentWeaponElement >= 0 && currentWeaponElement < 6)
          {
            transform1 = EffectManager.GetEffect(effect_name + currentWeaponElement.ToString(), parent);
            if (!Object.op_Equality((Object) transform1, (Object) null))
            {
              Vector3 localScale2 = transform1.localScale;
              transform1.localScale = Vector3.op_Multiply(localScale2, effect_scale);
              if (length >= 7)
              {
                transform1.localPosition = new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]);
                transform1.localRotation = Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6]));
                break;
              }
              break;
            }
            break;
          }
          break;
        }
        break;
      case AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
        transform1 = AnimEventFormat.GetSwichedEffect(new AnimEventFormat.GenerateEffectParam()
        {
          EffectName = effect_name,
          EffectParent = parent,
          Chara = chara,
          EffectScale = effect_scale,
          Data = data
        });
        break;
      case AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
        Player player2 = chara as Player;
        if (!Object.op_Equality((Object) player2, (Object) null))
        {
          Vector3 pos2 = player2.rainShotFallPosition;
          Quaternion rot2 = Quaternion.Euler(new Vector3(0.0f, player2.rainShotFallRotateY, 0.0f));
          if (length >= 7)
          {
            pos2 = Vector3.op_Addition(pos2, new Vector3(data.floatArgs[1], data.floatArgs[2], data.floatArgs[3]));
            rot2 = Quaternion.op_Multiply(rot2, Quaternion.Euler(new Vector3(data.floatArgs[4], data.floatArgs[5], data.floatArgs[6])));
          }
          EffectManager.OneShot(effect_name, pos2, rot2, Vector3.op_Multiply(Vector3.one, effect_scale), is_oneshot_priority, (Action<Transform>) (effect =>
          {
            rymFX component = ((Component) effect).gameObject.GetComponent<rymFX>();
            if (!Object.op_Inequality((Object) component, (Object) null))
              return;
            component.LoopEnd = true;
            component.AutoDelete = true;
          }));
          break;
        }
        break;
    }
    return transform1;
  }

  public static Transform[] EffectsEventExec(
    AnimEventFormat.ID id,
    AnimEventData.EventData data,
    Transform transform,
    bool is_oneshot_priority,
    AnimEventFormat.EffectNameAnalyzer name_analyzer = null,
    AnimEventFormat.NodeFinder node_finder = null,
    Character chara = null)
  {
    if (id != AnimEventFormat.ID.EFFECT_TILING)
      return (Transform[]) null;
    if (data == null || data.stringArgs == null)
      return (Transform[]) null;
    List<Transform> transformList = new List<Transform>();
    string effect_name = data.stringArgs.Length != 0 ? data.stringArgs[0] : string.Empty;
    string str = data.stringArgs.Length > 1 ? data.stringArgs[1] : string.Empty;
    if (name_analyzer != null)
    {
      effect_name = name_analyzer(effect_name);
      if (effect_name == null)
        return (Transform[]) null;
    }
    float num = data.floatArgs.Length > 0 ? data.floatArgs[0] : 1f;
    Transform transform1 = node_finder == null ? (!string.IsNullOrEmpty(str) ? Utility.Find(transform, str) : transform) : node_finder(str);
    if (id == AnimEventFormat.ID.EFFECT_TILING)
      transformList = AnimEventFormat.GetMultiEffect(new AnimEventFormat.GenerateEffectParam()
      {
        EffectName = effect_name,
        EffectParent = transform1,
        Chara = chara,
        EffectScale = num,
        Data = data
      });
    return transformList?.ToArray();
  }

  private static Transform GetSwichedEffect(AnimEventFormat.GenerateEffectParam _param)
  {
    Player chara = _param.Chara as Player;
    if (Object.op_Equality((Object) chara, (Object) null))
      return (Transform) null;
    Transform paramGeneratedEffect = AnimEventFormat.GetParamGeneratedEffect(_param);
    EffectCtrl component = ((Component) paramGeneratedEffect).GetComponent<EffectCtrl>();
    if (Object.op_Equality((Object) component, (Object) null) || _param.Data.intArgs == null || _param.Data.intArgs.Length <= 2 || _param.Data.intArgs[2] != 1 || chara.thsCtrl == null || !chara.thsCtrl.IsRequiredReloadAction())
      return paramGeneratedEffect;
    component.Play("LOOP2");
    return paramGeneratedEffect;
  }

  private static List<Transform> GetMultiEffect(AnimEventFormat.GenerateEffectParam _param)
  {
    if (_param == null || Object.op_Equality((Object) _param.Chara, (Object) null) || _param.Data.intArgs == null || _param.Data.intArgs.Length < 7 || _param.Data.floatArgs == null || _param.Data.floatArgs.Length < 13)
      return (List<Transform>) null;
    Player chara = _param.Chara as Player;
    if (Object.op_Equality((Object) chara, (Object) null))
      return (List<Transform>) null;
    int num1 = _param.Data.intArgs[4];
    int num2 = _param.Data.intArgs[5];
    int num3 = _param.Data.intArgs[6];
    Vector3 _intervalPos;
    // ISSUE: explicit constructor call
    ((Vector3) ref _intervalPos).\u002Ector(_param.Data.floatArgs[7], _param.Data.floatArgs[8], _param.Data.floatArgs[9]);
    Vector3 _intervalRot;
    // ISSUE: explicit constructor call
    ((Vector3) ref _intervalRot).\u002Ector(_param.Data.floatArgs[10], _param.Data.floatArgs[11], _param.Data.floatArgs[12]);
    AnimEventFormat.EFFECT_TILING_PATTERN intArg = (AnimEventFormat.EFFECT_TILING_PATTERN) _param.Data.intArgs[2];
    switch (intArg)
    {
      case AnimEventFormat.EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI:
        if (chara.thsCtrl != null)
        {
          num2 = chara.thsCtrl.CurrentMaxBulletCount;
          num1 = 1;
          num3 = 1;
          break;
        }
        break;
      case AnimEventFormat.EFFECT_TILING_PATTERN.AS_BURST_SHOTGUN:
        if (chara.thsCtrl != null)
        {
          num2 = chara.thsCtrl.CurrentRestBulletCount;
          num1 = 1;
          num3 = 1;
          break;
        }
        break;
    }
    int totalCount = num1 * num2 * num3;
    List<Transform> multiEffect = new List<Transform>();
    for (int k = 0; k < num3; ++k)
    {
      for (int j = 0; j < num2; ++j)
      {
        for (int i = 0; i < num1; ++i)
        {
          Transform paramGeneratedEffect = AnimEventFormat.GetParamGeneratedEffect(_param);
          if (!Object.op_Equality((Object) paramGeneratedEffect, (Object) null))
          {
            multiEffect.Add(paramGeneratedEffect);
            AnimEventFormat.SetEffectState(paramGeneratedEffect, i, j, k, totalCount, intArg, chara);
            AnimEventFormat.SetTransformInfo(paramGeneratedEffect, i, j, k, totalCount, intArg, _intervalPos, _intervalRot, chara);
          }
        }
      }
    }
    return multiEffect;
  }

  private static void SetEffectState(
    Transform _tr,
    int i,
    int j,
    int k,
    int totalCount,
    AnimEventFormat.EFFECT_TILING_PATTERN _pattern,
    Player _player)
  {
    EffectCtrl component = ((Component) _tr).GetComponent<EffectCtrl>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    int num = (i + 1) * (j + 1) * (k + 1);
    if (_pattern != AnimEventFormat.EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI)
      return;
    if (totalCount - num < _player.thsCtrl.CurrentRestBulletCount)
      component.Play("LOOP1");
    else
      component.Play("LOOP2");
  }

  private static void SetTransformInfo(
    Transform _tr,
    int i,
    int j,
    int k,
    int totalCount,
    AnimEventFormat.EFFECT_TILING_PATTERN _pattern,
    Vector3 _intervalPos,
    Vector3 _intervalRot,
    Player _player)
  {
    switch (_pattern)
    {
      case AnimEventFormat.EFFECT_TILING_PATTERN.AS_BURST_BULLET_UI:
        Transform transform1 = _tr;
        transform1.localPosition = Vector3.op_Addition(transform1.localPosition, new Vector3(_intervalPos.x * (float) i, _intervalPos.y * (float) j, _intervalPos.z * (float) k));
        float num1 = (totalCount % 2 == 1 ? (float) totalCount / 2f : (float) ((double) totalCount / 2.0 + 0.5)) - 1f;
        _tr.Rotate(_intervalRot.x * ((float) i - num1), _intervalRot.y * ((float) j - num1), _intervalRot.z * ((float) k - num1));
        break;
      case AnimEventFormat.EFFECT_TILING_PATTERN.AS_BURST_SHOTGUN:
        float num2 = Random.Range(_intervalPos.x, _intervalPos.x);
        float num3 = Random.Range(_intervalPos.y, _intervalPos.y);
        float num4 = Random.Range(0.0f, 360f) * ((float) Math.PI / 180f);
        Vector3 right = ((Component) _player).transform.right;
        Vector3 forward = ((Component) _player).transform.forward;
        Transform transform2 = _tr;
        transform2.localPosition = Vector3.op_Addition(transform2.localPosition, Vector3.op_Addition(Vector3.op_Multiply(num2 * Mathf.Cos(num4), right), Vector3.op_Multiply(num3 * Mathf.Sin(num4), forward)));
        break;
    }
  }

  private static Transform GetParamGeneratedEffect(AnimEventFormat.GenerateEffectParam _param)
  {
    Player chara = _param.Chara as Player;
    if (Object.op_Equality((Object) chara, (Object) null))
      return (Transform) null;
    int num = _param.Data.intArgs == null || _param.Data.intArgs.Length <= 3 ? 0 : (_param.Data.intArgs[3] == 1 ? 1 : 0);
    int currentWeaponElement = chara.GetCurrentWeaponElement();
    Transform paramGeneratedEffect = num == 0 || 0 > currentWeaponElement || currentWeaponElement >= 6 ? EffectManager.GetEffect(_param.EffectName, _param.EffectParent) : EffectManager.GetEffect($"{_param.EffectName}{currentWeaponElement:D2}", _param.EffectParent);
    if (Object.op_Equality((Object) paramGeneratedEffect, (Object) null))
      return paramGeneratedEffect;
    Vector3 localScale = paramGeneratedEffect.localScale;
    paramGeneratedEffect.localScale = Vector3.op_Multiply(localScale, _param.EffectScale);
    if (_param.Data.floatArgs.Length >= 7)
    {
      paramGeneratedEffect.localPosition = new Vector3(_param.Data.floatArgs[1], _param.Data.floatArgs[2], _param.Data.floatArgs[3]);
      paramGeneratedEffect.localRotation = Quaternion.Euler(new Vector3(_param.Data.floatArgs[4], _param.Data.floatArgs[5], _param.Data.floatArgs[6]));
    }
    return paramGeneratedEffect;
  }

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
    ACTION_RECEIVE_DAMAGE_RATE,
  }

  public enum EFFECT_EXEC_CONDITION
  {
    NONE,
    DISABLE_BUFF,
  }

  public enum EFFECT_SWITCH_CONDITION
  {
    NONE,
    BURST_REST_BULLET_COUNT,
  }

  public enum EFFECT_TILING_PATTERN
  {
    NONE,
    CIRCLE,
    MATRIX,
    AS_BURST_BULLET_UI,
    AS_BURST_SHOTGUN,
  }

  public enum MULTI_COL_GENERATE_CONDITION
  {
    NONE,
    IN_CORN,
  }

  public delegate string EffectNameAnalyzer(string effect_name);

  public delegate Transform NodeFinder(string node_name);

  public class GenerateEffectParam
  {
    public string EffectName = "";
    public Transform EffectParent;
    public bool IsDependOnWeaponElement;
    public Character Chara;
    public float EffectScale = 1f;
    public AnimEventData.EventData Data;
  }
}
