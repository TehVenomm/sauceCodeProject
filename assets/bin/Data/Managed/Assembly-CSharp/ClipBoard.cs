public class ClipBoard
{
	public static iClipBoard clipBoard = new AndroidClipBoard();

	public static void SetClipBoard(string setText)
	{
		clipBoard.SetClipBoard(setText);
	}
}
