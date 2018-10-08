public class UIStaticPanelChanger : UIStaticPanelRotateCheck
{
	private int unlockCount;

	public void Lock()
	{
		unlockCount--;
		if (unlockCount <= 0)
		{
			unlockCount = 0;
		}
	}

	public void UnLock()
	{
		unlockCount++;
		if (unlockCount > 0)
		{
			panel.widgetsAreStatic = false;
		}
	}

	protected override void Update()
	{
		if (unlockCount <= 0)
		{
			base.Update();
		}
	}
}
