using System.Text.RegularExpressions;

namespace DotFinglish
{
    internal class Finglish
    {
        private readonly Dictionary<string, IEnumerable<string>> _beginnings;
        private readonly Dictionary<string, IEnumerable<string>> _middles;
        private readonly Dictionary<string, IEnumerable<string>> _endings;
        private readonly Dictionary<string, string> _wordDictionary;
        private readonly Dictionary<string, int> _wordFrequency;
        private readonly int _maxWordLength;
        private readonly int _cutOff;
        private readonly Regex _regex;

        public Finglish(
            Dictionary<string, IEnumerable<string>> beginnings,
            Dictionary<string, IEnumerable<string>> middles,
            Dictionary<string, IEnumerable<string>> endings,
            Dictionary<string, string> wordDictionary,
            Dictionary<string, int> wordFrequency,
            int maxWordLength, int cutOff
        )
        {
            _beginnings = beginnings;
            _middles = middles;
            _endings = endings;
            _wordDictionary = wordDictionary;
            _wordFrequency = wordFrequency;
            _maxWordLength = maxWordLength;
            _cutOff = cutOff;
            _regex = new Regex("[ \\-_~!@#%$^&*\\(\\)\\[\\]\\{\\}/\\:;\"|,./?`]");
        }

        public string Convert(string phrase)
        {
            var convertedPhrase = ConvertPhrase(phrase);

            return string.Join(" ", convertedPhrase);
        }

        private IEnumerable<InnerWord> ConvertWord(string word)
        {
            var lowerWord = word.ToLower();

            if (_wordDictionary.TryGetValue(lowerWord, out var translatedWord))
            {
                return new List<InnerWord>
                {
                    new InnerWord
                    {
                        Word= translatedWord,
                        Weight = 1
                    }
                };
            }

            if (string.IsNullOrWhiteSpace(word))
            {
                return new List<InnerWord>();
            }
            else if (lowerWord.Length > _maxWordLength)
            {
                return new List<InnerWord>()
                {
                    new InnerWord
                    {
                        Word= word,
                        Weight = 1
                    }
                };
            }

            var variations = VariationUtility.Variations(lowerWord);

            var result = new List<InnerWord>();

            foreach (var variation in variations)
            {
                var innerWords = ConvertInnerWord(variation, word);

                result.AddRange(innerWords);
            }

            return result.OrderByDescending(x => x.Weight).Take(_cutOff).ToList();
        }

        private IEnumerable<InnerWord> ConvertInnerWord(IEnumerable<string> words, string originalWord)
        {
            var persian = new List<IEnumerable<string>>();

            foreach (var (letter, index) in words.WithIndex())
            {
                Dictionary<string, IEnumerable<string>> converter;

                if (index == 0)
                    converter = _beginnings;
                else if (index == words.Count() - 1)
                    converter = _endings;
                else
                    converter = _middles;

                if (!converter.TryGetValue($"{letter}", out var converteds))
                    return new List<InnerWord>
                    {
                        new InnerWord
                        {
                            Word = originalWord,
                            Weight = 0
                        }
                    };

                persian.Add(converteds);
            }

            var products = persian.CartesianProduct();

            var joinedProducts = products.Select(x => string.Join("", x));

            var alternatives = new List<InnerWord>();

            foreach (var joinedProduct in joinedProducts)
            {
                if (_wordFrequency.TryGetValue(joinedProduct, out int foundFrequency)) ;

                alternatives.Add(new InnerWord
                {
                    Word = joinedProduct,
                    Weight = foundFrequency
                });
            }

            if (alternatives.Count > 0)
            {
                var maxFrequency = alternatives.Max(x => x.Weight);

                foreach (var alternative in alternatives)
                {
                    if (alternative.Weight != 0)
                    {
                        alternative.Weight = alternative.Weight / maxFrequency;
                    }
                }

                return alternatives;
            }

            return new List<InnerWord>
            {
                new InnerWord
                {
                    Word = string.Join("", words),
                    Weight = 1
                }
            };
        }

        private IEnumerable<string> ConvertPhrase(string phrase)
        {
            var words = _regex.Split(phrase);

            var persianWords = new List<string>();

            foreach (var word in words)
            {
                var convertedWord = ConvertWord(word);

                if (convertedWord != null && convertedWord.Any())
                    persianWords.Add(convertedWord.First().Word);
            }

            return persianWords;
        }

        private class InnerWord
        {
            public string Word { get; set; }
            public float Weight { get; set; }
        }
    }
}
