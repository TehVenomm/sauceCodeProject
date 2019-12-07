using System;
using UnityEngine;

public class SymbolMakeListItem : MonoBehaviour
{
	[SerializeField]
	private BoxCollider m_Colider;

	[SerializeField]
	private SymbolTexture m_Texture;

	[SerializeField]
	private UIButton m_Button;

	public Action<int> onButton;

	public int symbolId
	{
		get;
		protected set;
	}

	public bool IsReady
	{
		get;
		protected set;
	}

	private void Awake()
	{
		m_Button.CacheDefaultColor();
		m_Button.tweenTarget = null;
	}

	public void Init(int id, SymbolTable.SymbolType type)
	{
		symbolId = Singleton<SymbolTable>.I.GetSymbolIndex(type, id);
		m_Texture.Initilize(type);
		m_Texture.LoadSymbol(symbolId);
		if (type == SymbolTable.SymbolType.FRAME_OUTLINE)
		{
			m_Texture.GetTexture().depth++;
		}
	}

	public void OnButton()
	{
		if (m_Texture.isReady && onButton != null)
		{
			onButton(symbolId);
		}
	}

	public void SetButtonActive(bool isActive)
	{
		m_Colider.enabled = isActive;
		m_Button.enabled = isActive;
	}
}
