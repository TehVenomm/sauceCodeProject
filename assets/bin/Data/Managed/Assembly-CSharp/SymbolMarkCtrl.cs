using Network;
using UnityEngine;

public class SymbolMarkCtrl : MonoBehaviour
{
	[SerializeField]
	private SymbolTexture markTexture;

	[SerializeField]
	private SymbolTexture frameTexture;

	[SerializeField]
	private SymbolTexture patternTexture;

	[SerializeField]
	private SymbolTexture frameOutLineTexture;

	public void Initilize()
	{
		markTexture.Initilize(SymbolTable.SymbolType.MARK);
		frameTexture.Initilize(SymbolTable.SymbolType.FRAME);
		frameOutLineTexture.Initilize(SymbolTable.SymbolType.FRAME_OUTLINE);
		patternTexture.Initilize(SymbolTable.SymbolType.PATTERN);
	}

	public void SetSize(int size)
	{
		markTexture.GetTexture().width = size;
		markTexture.GetTexture().height = size;
		frameTexture.GetTexture().width = size;
		frameTexture.GetTexture().height = size;
		frameOutLineTexture.GetTexture().width = size;
		frameOutLineTexture.GetTexture().height = size;
		patternTexture.GetTexture().width = size;
		patternTexture.GetTexture().height = size;
	}

	public void LoadSymbol(ClanSymbolData data)
	{
		markTexture.LoadSymbol(data.m);
		markTexture.SetColor(data.mo);
		frameTexture.LoadSymbol(data.f);
		frameOutLineTexture.LoadSymbol(data.f);
		frameTexture.SetColor(data.fo);
		patternTexture.LoadSymbol(data.p);
		patternTexture.SetColor(data.po);
	}
}
