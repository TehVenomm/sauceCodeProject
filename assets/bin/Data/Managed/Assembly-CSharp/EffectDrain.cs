using UnityEngine;

public class EffectDrain : MonoBehaviour
{
	private GameObject m_effect;

	private Transform m_cachedTrans;

	private Vector3 m_beginPos = Vector3.get_zero();

	private Transform m_endTrans;

	private float m_timer;

	private float m_finishTime = 0.5f;

	private bool m_isDelete;

	public EffectDrain()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	public void Initialize(StageObject srcObj, StageObject dstObj, EffectPlayProcessor.EffectSetting setting)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (srcObj == null || dstObj == null || setting == null)
		{
			Object.Destroy(this.get_gameObject());
			return;
		}
		m_beginPos = srcObj.FindNode(setting.nodeName).get_transform().get_position();
		m_endTrans = dstObj.FindNode("Hip");
		m_timer = 0f;
		m_cachedTrans = this.get_transform();
		m_cachedTrans.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
		m_cachedTrans.set_position(m_beginPos);
		Transform effect = EffectManager.GetEffect(setting.effectName, m_cachedTrans);
		if (effect == null)
		{
			Object.Destroy(this.get_gameObject());
			return;
		}
		effect.set_localPosition(Vector3.get_zero());
		effect.set_localRotation(Quaternion.get_identity());
		effect.set_localScale(Vector3.get_one());
		m_effect = effect.get_gameObject();
	}

	private void Update()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (m_isDelete)
		{
			return;
		}
		m_timer += Time.get_deltaTime();
		float num = Mathf.Clamp(m_timer / m_finishTime, 0f, 1f);
		m_cachedTrans.set_position(Vector3.Slerp(m_beginPos, m_endTrans.get_position(), num));
		if (num >= 1f)
		{
			if (m_effect != null)
			{
				m_effect.get_transform().SetParent(null);
				EffectManager.ReleaseEffect(m_effect, isPlayEndAnimation: true, immediate: true);
				m_effect = null;
			}
			Object.DestroyObject(this.get_gameObject());
			m_isDelete = true;
		}
	}
}
