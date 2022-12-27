namespace DotFinglish
{
    internal static class VariationUtility
    {
        public static IEnumerable<IEnumerable<string>> Variations(string word)
        {
            if (word == "a")
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "A"
                    }
                };
            }
            else if (word.Length == 1)
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        $"{word[0]}"
                    }
                };
            }
            else if (word == "aa")
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "A"
                    }
                };
            }
            else if (word == "ee")
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "i"
                    }
                };
            }
            else if (word == "ei")
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "ei"
                    }
                };
            }
            else if (new List<string> { "oo", "ou" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "u"
                    }
                };
            }
            else if (word == "kha")
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "kha"
                    },
                    new List<string>
                    {
                        "kh",
                        "a"
                    }
                };
            }
            else if (new List<string> { "kh", "gh", "ch", "sh", "zh", "ck" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        word
                    }
                };
            }
            else if (new List<string> { "'ee", "'ei" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "'i"
                    }
                };
            }
            else if (new List<string> { "'oo", "'ou" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "'u"
                    }
                };
            }
            else if (new List<string> { "a'", "e'", "o'", "i'", "u'", "A'" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        word[0] + "'"
                    }
                };
            }
            else if (new List<string> { "'a", "'e", "'o", "'i", "'u", "'A" }.Contains(word))
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        "'" + word[1]
                    }
                };
            }
            else if (word.Length == 2 && word[0] == word[1])
            {
                return new List<List<string>>
                {
                    new List<string>
                    {
                        $"{word[0]}"
                    }
                };
            }
            if (word.StartsWith("aa"))
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "A"
                    }
                };

                res.AddRange(Variations(word.Substring(2)));

                return res;
            }
            else if (word.StartsWith("ee"))
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "i"
                    }
                };

                res.AddRange(Variations(word.Substring(2)));

                return res;
            }
            else if (word.StartsWith("oo") || word.StartsWith("ou"))
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "u"
                    }
                };

                res.AddRange(Variations(word.Substring(2)));

                return res;
            }
            else if (word.StartsWith("kha"))
            {
                var res1 = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "kha"
                    }
                };

                var khaVariations = Variations(word.Substring(3));

                res1.AddRange(khaVariations);

                var res2 = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "kh",
                        "a"
                    }
                };

                res2.AddRange(khaVariations);

                var res3 = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "k",
                        "h",
                        "a"
                    }
                };

                res3.AddRange(khaVariations);

                return res1.Concat(res2).Concat(res3);
            }
            else if (
                word.StartsWith("kh") ||
                word.StartsWith("gh") ||
                word.StartsWith("ch") ||
                word.StartsWith("sh") ||
                word.StartsWith("zh") ||
                word.StartsWith("ck")
                )
            {
                var res1 = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word.Take(2)}"
                    }
                };

                var variations = Variations(word.Substring(2));

                res1.AddRange(variations);

                var res2 = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word[0]}"
                    }
                };

                res2.AddRange(variations);

                return res1.Concat(res2);
            }
            else if (
                word.StartsWith("a'") ||
                word.StartsWith("e'") ||
                word.StartsWith("o'") ||
                word.StartsWith("i'") ||
                word.StartsWith("u'") ||
                word.StartsWith("A'")
                )
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word.Take(2)}"
                    }
                };

                var variations = Variations(word.Substring(2));

                res.AddRange(variations);

                return res;
            }
            else if (word.StartsWith("'ee") || word.StartsWith("'ei"))
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "'i"
                    }
                };

                res.AddRange(Variations(word.Substring(3)));

                return res;
            }
            else if (word.StartsWith("'oo") || word.StartsWith("'ou"))
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        "'i"
                    }
                };

                res.AddRange(Variations(word.Substring(3)));

                return res;
            }
            else if (word.StartsWith("'a") ||
                word.StartsWith("'e") ||
                word.StartsWith("'o") ||
                word.StartsWith("'i") ||
                word.StartsWith("'u") ||
                word.StartsWith("'A")
                )
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word.Take(2)}"
                    }
                };

                var variations = Variations(word.Substring(2));

                res.AddRange(variations);

                return res;
            }
            else if (word.Length >= 2 && word[0] == word[1])
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word[0]}"
                    }
                };

                var variations = Variations(word.Substring(2));

                res.AddRange(variations);

                return res;
            }
            else
            {
                var res = new List<IEnumerable<string>>
                {
                    new List<string>
                    {
                        $"{word[0]}"
                    }
                };

                var variations = Variations(word.Substring(1));

                res.AddRange(variations);

                return res;
            }
        }

    }
}
