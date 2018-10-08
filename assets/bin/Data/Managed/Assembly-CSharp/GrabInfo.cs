using System;
using UnityEngine;

[Serializable]
public class GrabInfo
{
	[Tooltip("有効かどうか")]
	public bool enable;

	[Tooltip("掴んでいる秒数")]
	public float duration;

	[Tooltip("離し攻撃のId")]
	public int releaseAttackId;

	[Tooltip("掴み中につける骨")]
	public string parentNode;

	[Tooltip("掴み中のカメラの注視点")]
	public string cameraLookAt;

	[Tooltip("掴み中のカメラの方向")]
	public Vector3 toCameraDir;

	[Tooltip("掴み中のカメラまでの距離")]
	public float cameraDistance;

	[Tooltip("弱点攻撃で解放するかどうか")]
	public bool releaseByWeakHit;

	[Tooltip("武器弱点攻撃で解放するかどうか")]
	public bool releaseByWeaponWeakHit;

	[Tooltip("掴み中のドレイン情報ID")]
	public int drainAttackId;

	public void Copy(GrabInfo src)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		enable = src.enable;
		duration = src.duration;
		releaseAttackId = src.releaseAttackId;
		parentNode = src.parentNode;
		cameraLookAt = src.cameraLookAt;
		toCameraDir = src.toCameraDir;
		cameraDistance = src.cameraDistance;
		releaseByWeakHit = src.releaseByWeakHit;
		releaseByWeaponWeakHit = src.releaseByWeaponWeakHit;
		drainAttackId = src.drainAttackId;
	}
}
