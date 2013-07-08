using System;
using System.Collections.Generic;
using System.Linq;

namespace bdl
{
    public class BdlParser
    {
        private readonly Queue<string> _tokens;

        public BdlParser(string query)
        {
            var tokenizer = new BdlLexicalScanner();
            _tokens = tokenizer.Tokenize(query);
            Query = ReadExpression();

            if (_tokens.Any())
            {
                throw new Exception("Unknown junk at end of query:"+string.Join(", ", _tokens));
            }

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
            return token == "+" || token == "&" || token == "a" || token == "and";
        }

        private bool IsOr(string token)
        {
            return token == "." || token == "|" || token == "o" || token == "or";
        }

        private bool IsNot(string token)
        {
            return token == "!" || token == "¬" || token == "n" || token == "not";
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
            var token = _tokens.Dequeue();
            if (expectedToken != null && token != expectedToken)
            {
                throw new Exception(string.Format("Expected '{0}' but got '{1}'", expectedToken, token));
            }
        }
    }
}
