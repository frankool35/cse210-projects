using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = new List<Word>();
            foreach (string word in text.Split(" "))
            {
                _words.Add(new Word(word));
            }
        }

        public void HideRandomWords(int numberToHide)
        {
            Random rand = new Random();
            List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

            for (int i = 0; i < numberToHide && visibleWords.Count > 0; i++)
            {
                int index = rand.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }

        public string GetDisplayText()
        {
            return $"{_reference.GetDisplayText()}\n" + string.Join(" ", _words.Select(w => w.GetDisplayText()));
        }

        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden());
        }
    }
}
