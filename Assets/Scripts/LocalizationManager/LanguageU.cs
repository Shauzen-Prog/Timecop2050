using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageU
{
    public static Dictionary<Language, Dictionary<string, string>> LoadCodex(string source)
    {
        var codex = new Dictionary<Language, Dictionary<string, string>>();

        var rows = source.Split(new[] { '\r', '\n' });

        var columToIndex = new Dictionary<string, int>();

        var first = true;

        var lineNum = 0;

        foreach (var row in rows)
        {
            lineNum++;

            var cells = row.Split(',');

            if (first)
            {
                first = false;
                for (var i = 0; i < cells.Length; i++)
                {
                    columToIndex[cells[i]] = i;
                }

                continue;
            }

            var langName = cells[columToIndex["Idioma"]];

            Language lang = default;

            try
            {
                lang = (Language)Enum.Parse(typeof(Language), langName);
            }
            catch (Exception e)
            {
                
                continue;
            }

            var idName = cells[columToIndex["ID"]];
            var text = cells[columToIndex["Texto"]];

            if (!codex.ContainsKey(lang))
            {
                codex[lang] = new Dictionary<string, string>();
            }

            codex[lang][idName] = text;
        }

        return codex;
    }
}
