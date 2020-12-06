using UnityEngine;

namespace SupremeMelee
{
    // Token: 0x02000009 RID: 9
    public static class UIHelper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002A48 File Offset: 0x00000C48
		public static Rect GetRowRect(Rect inRect, float rowHeight, int row)
		{
			var y = inRect.y + (rowHeight * row);
			var result = new Rect(inRect.x, y, inRect.width, rowHeight);
			return result;
		}
	}
}
