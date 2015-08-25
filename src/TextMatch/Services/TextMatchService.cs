namespace TextMatch.Services
{
    using System;
    using System.Text;

    /// <summary>
    /// Service class to implement the Text match Services.
    /// </summary>
    public class TextMatchService : ITextMatchService
    {
        /// <summary>
        /// Method to perform the pattern match. Looks for the occurences of the <paramref name="subText"/>
        /// in the <paramref name="inputText"/> and returns a comma separated string with the <paramref name="inputText"/>
        /// string 1-based indexes.
        /// </summary>
        /// <param name="inputText">The Input string which will be used to search for the <paramref name="subText"/></param>
        /// <param name="subText">The pattern string whose occurances will be search for in the <paramref name="inputText"/>.</param>
        /// <returns>A comma separated string with the occurences of the <paramref name="subText"/> in the <paramref name="inputText"/></returns>
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

        /// <summary>
        /// Method to perform a case-insentive character comparison
        /// </summary>
        /// <param name="x">First character which will be compared with <paramref name="y"/>.</param>
        /// <param name="y">Second character which will be compared with <paramref name="x"/>.</param>
        /// <returns>True if <paramref name="x"/> matches <paramref name="y"/>, otherwise False.</returns>
        public bool CharMatchCaseInsenitive(char x, char y)
        {
            return char.ToLower(x) == char.ToLower(y);
        }

        /// <summary>
        /// Method to perform the Knuth Morris Pratt string search algorithm.
        /// I took the psuedo code from the following link https://www.topcoder.com/community/data-science/data-science-tutorials/introduction-to-string-searching-algorithms/
        /// </summary>
        /// <param name="inputText">The Input string which will be used to search for the <paramref name="subText"/></param>
        /// <param name="subText">The pattern string whose occurances will be search for in the <paramref name="inputText"/>.</param>
        /// <returns></returns>
        private string Knuth_Morris_Pratt(string inputText, string subText)
        {
            var result = new StringBuilder();

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
                    subTextMarker++; // move to the next subText char
                    inputTextMarker++; // move to the next inputText char

                    // if we have reached the end of the subText
                    if (subTextMarker == subTextSize)
                    {
                        // match found, append to the result string
                        // NB. need to take the inputTextMarker back to the beginning index
                        // i.e. the length of the subText
                        // then we need to add 1 because the requirements ask for 1 based positions not 0 based
                        result.AppendFormat("{0},",(inputTextMarker - subTextSize) + 1);
                        // reset subText marker, so we can keep looking
                        subTextMarker = 0;
                    }
                }

                // if the current state is not zero (we have not reached the empty string yet) we try to
                // "expand" the next best (largest) match
                else if (subTextMarker > 0)
                    subTextMarker = 0;

                // if we reached the empty string and failed to "expand" even it; we go to the next 
                // character from the text, the state of the automation remains zero
                else
                    inputTextMarker++;
            }

            // remove the trailing, if there is one
            return result.ToString().TrimEnd(',');
        }
    }
}