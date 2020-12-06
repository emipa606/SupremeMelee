namespace SupremeMelee
{
    // Token: 0x02000008 RID: 8
    public static class MathUtils
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002A20 File Offset: 0x00000C20
		public static float TwoWayScale(float first, float second)
		{
			var flag = second > first;
			float result;
			if (flag)
			{
				result = first / second;
			}
			else
			{
				result = second / first;
			}
			return result;
		}
	}
}
