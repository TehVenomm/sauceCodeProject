using System;

[Serializable]
public class FixOffsetPosition
{
	public string Path;

	public float OffsetHeigh;

	public bool IsOnlyInphoneX;

	public FixOffsetPosition(string _path)
	{
		Path = _path;
		OffsetHeigh = 0f;
	}
}
