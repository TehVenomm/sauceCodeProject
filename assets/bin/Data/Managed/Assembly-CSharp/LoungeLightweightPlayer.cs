using Network;
using System.Collections;
using UnityEngine;

public class LoungeLightweightPlayer : LoungePlayer
{
	protected override bool IsValidMove()
	{
		if (isMoving)
		{
			return false;
		}
		if (isPlayingSitAnimation)
		{
			return false;
		}
		return true;
	}

	protected override void InitAnim()
	{
	}

	protected override IEnumerator Move()
	{
		isMoving = true;
		float t = 0f;
		Vector3 pos = base._transform.get_position();
		do
		{
			Vector3 val = base.moveTargetPos - pos;
			base._transform.set_position(Vector3.Lerp(pos, base.moveTargetPos, t));
			t += Time.get_deltaTime() / 1f;
			yield return null;
		}
		while (!(t >= 1f));
		base._transform.set_position(base.moveTargetPos);
		isMoving = false;
	}

	protected override IEnumerator DoSit()
	{
		yield break;
	}

	protected override PlayerLoader Load(LoungePlayer chara, GameObject go, CharaInfo chara_info, PlayerLoader.OnCompleteLoad callback)
	{
		PlayerLoader playerLoader = go.AddComponent<LightweightPlayerLoader>();
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		if (chara_info != null)
		{
			playerLoadInfo.Apply(chara_info, need_weapon: false, need_helm: true, need_leg: true, is_priority_visual_equip: true);
		}
		playerLoader.StartLoad(playerLoadInfo, 8, 99, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: true, need_dev_frame_instantiate: true, SHADER_TYPE.NORMAL, callback);
		return playerLoader;
	}

	public override void OnRecvSit()
	{
	}

	public override void OnRecvStandUp()
	{
	}

	public override bool DispatchEvent()
	{
		return false;
	}
}
