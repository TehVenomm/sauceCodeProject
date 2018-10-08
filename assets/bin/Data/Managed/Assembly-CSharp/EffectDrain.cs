using UnityEngine;

public class EffectDrain
{
	private GameObject m_effect;

	private Transform m_cachedTrans;

	private Vector3 m_beginPos = Vector3.get_zero();

	private Vector3 m_endPos = Vector3.get_zero();

	private float m_timer;

	private float m_finishTime = 0.5f;

	private bool m_isDelete;

	public EffectDrain()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_000c: Unknown result type (might be due to invalid IL or missing references)
	//IL_0011: Unknown result type (might be due to invalid IL or missing references)


	public void Initialize(StageObject srcObj, StageObject dstObj, EffectPlayProcessor.EffectSetting setting)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		if (srcObj == null || dstObj == null || setting == null)
		{
			Object.Destroy(this.get_gameObject());
		}
		else
		{
			m_beginPos = srcObj.FindNode(setting.nodeName).get_transform().get_position();
			m_endPos = dstObj.FindNode("Hips").get_transform().get_position();
			m_timer = 0f;
			m_cachedTrans = this.get_transform();
			m_cachedTrans.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			m_cachedTrans.set_position(m_beginPos);
			Transform effect = EffectManager.GetEffect(setting.effectName, m_cachedTrans);
			if (effect == null)
			{
				Object.Destroy(this.get_gameObject());
			}
			else
			{
				effect.set_localPosition(Vector3.get_zero());
				effect.set_localRotation(Quaternion.get_identity());
				effect.set_localScale(Vector3.get_one());
				m_effect = effect.get_gameObject();
			}
		}
	}

	private void Update()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDelete)
		{
			m_timer += Time.get_deltaTime();
			float num = Mathf.Clamp(m_timer / m_finishTime, 0f, 1f);
			m_cachedTrans.set_position(Vector3.Slerp(m_beginPos, m_endPos, num));
			if (num >= 1f)
			{
				if (m_effect != null)
				{
					m_effect.get_transform().SetParent(null);
					EffectManager.ReleaseEffect(m_effect, true, false);
					m_effect = null;
				}
				Object.DestroyObject(this.get_gameObject());
				m_isDelete = true;
			}
		}
	}
}
