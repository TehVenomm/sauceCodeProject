public class FieldCarriableEvolveItemGimmickObject : FieldCarriableGimmickObject
{
	private static readonly int kShiftIndex = 1000;

	protected override bool IsDefenseTool()
	{
		return false;
	}

	public void Use2Evolve(FieldCarriableGimmickObject gimmick)
	{
		gimmick.Evolve();
		base.gameObject.SetActive(value: false);
	}

	public override bool HasDeploied()
	{
		return false;
	}
}
