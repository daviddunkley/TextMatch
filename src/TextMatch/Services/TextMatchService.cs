namespace TextMatch.Services
{
    using System;

    public class TextMatchService : ITextMatchService
    {
        public string FindMatches(string inputText, string subText)
        {
            // Check null occurences, result is an exception
            if (inputText == null) throw new ArgumentNullException("inputText");
            if (subText == null) throw new ArgumentNullException("subText");

            // Check empty string occurences, result is an empty string
            if (inputText == string.Empty) return string.Empty;
            if (subText == string.Empty) return string.Empty;

            var result = Knuth_Morris_Pratt(inputText, subText);

            return result;
        }

        public bool CharMatchCaseInsenitive(char x, char y)
        {
            return char.ToLower(x) == char.ToLower(y);
        }

        private string Knuth_Morris_Pratt(string inputText, string subText)
        {
            var result = "";

            // Note the lengths of the two strings, so we know when we are done
            // NB. C# strings are zero based arrays so the Length will be one more that the last array index
            var inputTextSize = inputText.Length;
            var subTextSize = subText.Length;

            // Mark the current Index we are on for both of the strings
            var subTextMarker = 0;
            var inputTextMarker = 0;

            // loop until we are told to stop
            for (;;)
            {
                // we reached the end of the text, we are done
                if (inputTextMarker == inputTextSize) break; 

                // if the current character of the text "expands" the current match 
                if (CharMatchCaseInsenitive(inputText[inputTextMarker], subText[subTextMarker]))
                {
                    subTextMarker++; // change the state of the automaton
                    inputTextMarker++; // get the next character from the text

                    // if we have not reached the end of the subText
                    if (subTextMarker == subTextSize)
                    {
                        // match found, append the result string
                        // NB. need to take the inputTextMarker back to the beginning index
                        // Add 1 because the requirements ask for 1 based positions not 0 based
                        // i.e. the length of the subText
                        result += ((inputTextMarker - subTextSize) + 1) + ",";
                        // reset subText marker, so we can keep looking
                        subTextMarker = 0;
                    }
                }

                // if the current state is not zero (we have not reached the empty string yet) we try to
                // "expand" the next best (largest) match
                else if (subTextMarker > 0)
                    subTextMarker = 0; //FailureFunction[j];

                // if we reached the empty string and failed to
                // "expand" even it; we go to the next 
                // character from the text, the state of the
                // automation remains zero
                else
                    inputTextMarker++;
            }

            // remove the trailing, if there is one
            return result.TrimEnd(',');
        }
    }
}