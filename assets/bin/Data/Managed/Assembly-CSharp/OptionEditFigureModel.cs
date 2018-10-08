public class OptionEditFigureModel : BaseModel
{
	public class RequestSendForm
	{
		public int sex;

		public int face;

		public int hair;

		public int color;

		public int skin;

		public int voice;

		public string name;

		public int crystalCL;
	}

	public static string URL = "ajax/option/editfigure";
}
