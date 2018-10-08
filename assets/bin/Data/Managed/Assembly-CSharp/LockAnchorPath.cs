using System;

[Serializable]
public class LockAnchorPath
{
	public string Path;

	public bool IsFullAnchor;

	public LockAnchorPath(string _path, bool _isFullAnchor)
	{
		Path = _path;
		IsFullAnchor = _isFullAnchor;
	}
}
