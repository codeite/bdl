using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace bdl
{
    public class BdlLexicalScanner
    {
        public Queue<string> Tokenize(string query)
        {
            var tokens = new Queue<string>();
            var chars = query.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                var c = chars[i];

                if (IsDigit(c))
                {
                    tokens.Enqueue(ReadNumber(chars, ref i));
                }
                else if (IsLetter(c))
                {
                    tokens.Enqueue(ReadWord(chars, ref i));
                }
                else
                {
                    tokens.Enqueue(new string(c, 1));
                }
            }

            return tokens;
        }

        private string ReadWord(char[] chars, ref int i)
        {
            var start = i;
            while (i+1 < chars.Length && IsLetter(chars[i+1]))
            {
                i++;
            }

            var length = i - start +1;
            return new string(chars, start, length).ToLowerInvariant();
        }

        private bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }

        private string ReadNumber(char[] chars, ref int i)
        {
            var start = i;
            while (i+1 < chars.Length && IsDigit(chars[i+1]))
            {
                i++;
            }

            var length = i - start + 1;
            return new string(chars, start, length);
        }

        private bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }
    }
}
