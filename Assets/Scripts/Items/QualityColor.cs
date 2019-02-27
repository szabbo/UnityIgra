using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//enumeracije - definiranje svog tipa podatka
public enum Quality { Common, Uncommon, Rare, Epic }

public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common, "#ffffffff" },
        {Quality.Uncommon, "#00ff00ff" },
        {Quality.Rare, "#ff8a00" },
        {Quality.Epic, "#800080ff" }
    };

    public static Dictionary<Quality, string> MyColors
    {
        get
        {
            return colors;
        }
    }
}
