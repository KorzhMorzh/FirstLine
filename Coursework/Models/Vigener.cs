using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Coursework.Models
{
    public class Vigener
    {
        public string NewText { get; set; }
        public Vigener(string text, string key, string isEncode)
        {
            if (isEncode == "true")
            {
                NewText = Decode(text, key);
            }
            else
            {
                NewText = Encode(text, key);
            }
        }

        static string Encode(string text, string key)
        {
            var alphabet = new ArrayList
            {
                'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у',
                'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
            };

            StringBuilder newText = new StringBuilder();
            var j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    var indexofText = alphabet.IndexOf(text[i]);
                    var indexofKey = alphabet.IndexOf(key[(i - j) % key.Length]);
                    newText.Append(alphabet[(indexofText + indexofKey) % 33]);
                }
                else
                {
                    newText.Append(text[i]);
                    j += 1;
                }
            }

            return newText.ToString();
        }

        static string Decode(string text, string key)
        {
            var alphabet = new ArrayList
            {
                'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у',
                'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
            };

            StringBuilder newText = new StringBuilder();
            var j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i]))
                {
                    var indexofText = alphabet.IndexOf(text[i]);
                    var indexofKey = alphabet.IndexOf(key[(i - j) % key.Length]);
                    newText.Append(alphabet[(33 + indexofText - indexofKey) % 33]);
                }

                else
                {
                    newText.Append(text[i]);
                    j += 1;
                }
            }

            return newText.ToString();
        }
    }
}