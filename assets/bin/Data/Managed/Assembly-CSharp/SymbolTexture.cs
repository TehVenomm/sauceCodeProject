using System.Collections;
using UnityEngine;

public class SymbolTexture : MonoBehaviour
{
	[SerializeField]
	private UITexture m_Texture;

	private int symbolId;

	private int oldSymbolId;

	public bool isReady;

	private SymbolTable.SymbolType symbolType;

	private IEnumerator m_CoroutineLoadSymbol;

	public SymbolTexture()
		: this()
	{
	}

	public void Initilize(SymbolTable.SymbolType type)
	{
		symbolType = type;
		isReady = false;
		oldSymbolId = 0;
		symbolId = 0;
		SetActiveComponents(isActive: false);
	}

	public void LoadSymbol(int symbolIndex)
	{
		oldSymbolId = symbolId;
		symbolId = Singleton<SymbolTable>.I.GetSymbolID(symbolType, symbolIndex);
		if (oldSymbolId != symbolId)
		{
			SetActiveComponents(isActive: false);
			RequestLoadSymbol();
		}
	}

	public void SetColor(int colorIndex)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		m_Texture.color = Singleton<SymbolTable>.I.GetColor(symbolType, colorIndex);
	}

	public UITexture GetTexture()
	{
		return m_Texture;
	}

	public void SetActiveComponents(bool isActive)
	{
		m_Texture.alpha = (isActive ? 1 : 0);
	}

	private void RequestLoadSymbol()
	{
		CancelLoadSymbol();
		m_CoroutineLoadSymbol = CoroutineLoadSymbol();
		if (this.get_gameObject().get_activeInHierarchy())
		{
			this.StartCoroutine(_Update());
		}
	}

	private IEnumerator CoroutineLoadSymbol()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_symbol = (symbolType != SymbolTable.SymbolType.FRAME_OUTLINE) ? load_queue.LoadSymbol(symbolId) : load_queue.LoadSymbolFrame(symbolId);
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		if (lo_symbol.loadedObject != null)
		{
			Texture2D mainTexture = lo_symbol.loadedObject as Texture2D;
			m_Texture.mainTexture = mainTexture;
			SetActiveComponents(isActive: true);
			isReady = true;
		}
		m_CoroutineLoadSymbol = null;
	}

	private void CancelLoadSymbol()
	{
		m_CoroutineLoadSymbol = null;
	}

	private IEnumerator _Update()
	{
		while (m_CoroutineLoadSymbol != null && m_CoroutineLoadSymbol.MoveNext())
		{
			yield return null;
		}
	}

	private void OnEnable()
	{
		if (m_CoroutineLoadSymbol != null)
		{
			RequestLoadSymbol();
		}
	}
}
