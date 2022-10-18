using UnityEngine;

namespace SupremeMelee;

public static class UIHelper
{
    public static Rect GetRowRect(Rect inRect, float rowHeight, int row)
    {
        var y = inRect.y + (rowHeight * row);
        var result = new Rect(inRect.x, y, inRect.width, rowHeight);
        return result;
    }
}