using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment8
{
    /// <summary>
    /// Main entry point for the Spell Checker application.
    /// This application demonstrates HashSet usage through interactive spell checking.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simple Spell Checker & Vocabulary Explorer ===");
            Console.WriteLine("This application uses HashSet<string> for fast word lookups and analysis.\n");

            try
            {
                // Initialize the spell checker system
                var spellChecker = new SpellChecker();
                // If invoked with "autotest", run a scripted test sequence (non-interactive)
                if (args != null && args.Length > 0 && args[0].Equals("autotest", StringComparison.OrdinalIgnoreCase))
                {
                    RunAutoTest(spellChecker);
                    return;
                }

                // Start the interactive navigator immediately
                var navigator = new SpellCheckerNavigator(spellChecker);
                navigator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static void RunAutoTest(SpellChecker spellChecker)
        {
            Console.WriteLine("Running non-interactive autotest...");
            string dict = "dictionary.txt";
            string sample = "sample.txt";

            Console.WriteLine($"Loading dictionary: {dict}");
            bool dloaded = spellChecker.LoadDictionary(dict);
            Console.WriteLine($"  Loaded: {dloaded} (size={spellChecker.DictionarySize})");

            Console.WriteLine($"Analyzing sample: {sample}");
            bool anal = spellChecker.AnalyzeTextFile(sample);
            Console.WriteLine($"  Analyzed: {anal}");

            Console.WriteLine("Categorizing words...");
            try { spellChecker.CategorizeWords(); Console.WriteLine("  Categorized"); } catch (Exception ex) { Console.WriteLine($"  Error during categorize: {ex.Message}"); }

            var stats = spellChecker.GetTextStats();
            Console.WriteLine($"Stats: total={stats.totalWords}, unique={stats.uniqueWords}, correct={stats.correctWords}, misspelled={stats.misspelledWords}");

            var miss = spellChecker.GetMisspelledWords(20);
            Console.WriteLine("Misspelled sample:");
            foreach (var m in miss) Console.WriteLine("  " + m);

            var check = "example";
            var result = spellChecker.CheckWord(check);
            Console.WriteLine($"Check '{check}': inDict={result.inDictionary}, inText={result.inText}, occ={result.occurrences}");
        }
    }
}