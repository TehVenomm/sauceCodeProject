using UnityEngine;

public class UIBurstBulletIconController : MonoBehaviour
{
	public class InitParam
	{
		public int IconIndex;

		public int DepthOffset;
	}

	private readonly Vector3 BULLET_ICON_POS_INTERVAL = new Vector3(22f, 0f, 0f);

	[SerializeField]
	private UISprite m_baseIcon;

	[SerializeField]
	private UISprite m_bulletIcon;

	public UIBurstBulletIconController()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)


	public bool Initialize(InitParam _param)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (_param == null)
		{
			return false;
		}
		if (m_baseIcon == null || m_bulletIcon == null)
		{
			return false;
		}
		m_baseIcon.depth = _param.DepthOffset + _param.IconIndex * 2;
		m_bulletIcon.depth = _param.DepthOffset + _param.IconIndex * 2 + 1;
		this.get_transform().set_localPosition(BULLET_ICON_POS_INTERVAL * (float)_param.IconIndex);
		this.get_transform().set_localRotation(Quaternion.Euler(Vector3.get_zero()));
		this.get_transform().set_localScale(Vector3.get_one());
		return true;
	}

	public bool SetVisibleAllIcon()
	{
		return SwitchVisibleIcon(m_baseIcon, _isVisible: true) && SwitchVisibleIcon(m_bulletIcon, _isVisible: true);
	}

	public bool SetInvisibleAllIcon()
	{
		return SwitchVisibleIcon(m_baseIcon, _isVisible: false) && SwitchVisibleIcon(m_bulletIcon, _isVisible: false);
	}

	public bool SetVisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, _isVisible: true);
	}

	public bool SetInvisibleBulletIcon()
	{
		return SwitchVisibleIcon(m_bulletIcon, _isVisible: false);
	}

	private bool SwitchVisibleIcon(UISprite _sprite, bool _isVisible)
	{
		if (_sprite == null || _sprite.get_enabled() == _isVisible)
		{
			return false;
		}
		_sprite.set_enabled(_isVisible);
		return true;
	}
}
