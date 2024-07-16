// namespace GameManager.UI.Helpers;
//
// public class NumberHelper
// {
//     private readonly Dictionary<char, int> _commonRomanNumerals = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 } };
//
//     public int TotalRomanNumeralValue { get; private set; }
//
//     /// <summary>
//     ///     Checks to see if a word is a roman numeral or not
//     /// </summary>
//     /// <param name="word">The string to check.</param>
//     /// <returns>True if it is a roman numeral, False otherwise.</returns>
//     public bool IsRomanNumeral(string word)
//     {
//         // The maximum length roman numeral is XVIII, also add another letter for edge cases
//         if (word.Length > 6)
//         {
//             return false;
//         }
//
//         if (word.Any(character => !_commonRomanNumerals.ContainsKey(character)))
//         {
//             return false;
//         }
//
//         int totalValue = 0;
//         int previousValue = 0;
//
//         foreach (int currentValue in word.Select(t => _commonRomanNumerals[char.ToUpper(t)]))
//         {
//             if (currentValue > previousValue)
//             {
//                 totalValue += currentValue - (2 * previousValue);
//             }
//             else
//             {
//                 totalValue += currentValue;
//             }
//
//             previousValue = currentValue;
//         }
//
//         TotalRomanNumeralValue = totalValue;
//         return totalValue > 0;
//     }
// }
