using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace bdl
{
    public class BdlParser
    {
        private readonly Stack<string> _tokens;

        public BdlParser(string query)
        {
            _tokens = Tokenize(query);
            Query = ReadExpression();
        }

        public BdlQueryComponent Query { get; private set; }


        private BdlQueryComponent ReadExpression()
        {
            var components = new List<BdlQueryComponent> { ReadTerm() };

            while (IsOr(Peek))
            {
                Read();
                components.Add(ReadTerm());
            }

            if (components.Skip(1).Any())
            {
                return new BdlOr(components);
            }

            return components.First();
        }

        private BdlQueryComponent ReadTerm()
        {
            var components = new List<BdlQueryComponent> { ReadNotFactor() };

            while (IsAnd(Peek))
            {
                Read();
                components.Add(ReadNotFactor());
            }

            if (components.Skip(1).Any())
            {
                return new BdlAnd(components);
            }

            return components.First();
        }

        private bool IsAnd(string token)
        {
            return token == "+" || token == "&";
        }

        private bool IsOr(string token)
        {
            return token == "." || token == "|";
        }

        private bool IsNot(string token)
        {
            return token == "!" || token == "¬";
        }

        private BdlQueryComponent ReadNotFactor()
        {
            var token = Peek;

            if (IsNot(token))
            {
                Read();
                return new BdlNot(ReadNotFactor());
            }

            return ReadFactor();
        }

        private BdlQueryComponent ReadFactor()
        {
            var token = Peek;
            int number;

            if (token == "(")
            {
                return ReadBlock();
            }
            
            if (int.TryParse(token, out number))
            {
                Read();
                return new BdlValue(number);
            }

            throw new Exception("In factor and expected block or number but got: "+token);
        }

        private BdlQueryComponent ReadBlock()
        {
            Read("(");

            var expression = new BdlBlock(ReadExpression());

            Read(")");

            return expression;
        }

        private string Peek
        {
            get { return _tokens.Any() ? _tokens.Peek() : null; }
        }

        private void Read(string expectedToken = null)
        {
            var token = _tokens.Pop();
            if (expectedToken != null && token != expectedToken)
            {
                throw new Exception(string.Format("Expected '{0}' but got '{1}'", expectedToken, token));
            }
        }

        public static Stack<string> Tokenize(string query)
        {
            //var collection = query.Split(new[] {'(', ')', '+', '|', '&', '.'});
            var collection = Regex.Split(query, @"(?=[()+|&.!¬])|(?<=[()+|&.!¬])").Where(x => x != "").Reverse();
            return new Stack<string>(collection);
        }
    }
}
