using PigLatinSimple.Models;

namespace PigLatinSimple.Helpers
{
    public class UserInput
    {
         public void GetWord(User user)
        {
            string? validInput;

            bool redoLoop = true;

            while (redoLoop)
            {
                Console.WriteLine("Enter a word/phrase:");

                validInput = Console.ReadLine();

                if (string.IsNullOrEmpty(validInput))
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a valid word/phrase.");
                    redoLoop = true;

                }
                else
                { 
                    user.inputtedPhrase = validInput;
                    redoLoop = false;
                }

            }

        }

        public void SetCasePreference(User user)
        {
            string? validAnswer = "";

            bool redoLoop = true;

            while (redoLoop)
            {
                Console.WriteLine("Would you like your word to be case sensitive? yes/no");

                validAnswer = Console.ReadLine().ToLower();

                if (!string.IsNullOrEmpty(validAnswer))
                {

                    if (validAnswer != "yes" &&
                        validAnswer != "no")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid answer. yes or no");

                    }
                    else
                    {
                        user.caseMatters = validAnswer == "yes" ? true : false;
                        redoLoop = false;
                    }

                }
           
            }

            if (!user.caseMatters)
            {
                user.inputtedPhrase.ToLower();
            }

        }

        public bool ContinuePigLatin(User user)
        {
            char validAnswer;

            bool redoLoop = true;
            bool continuePigLatin = false;

            while (redoLoop)
            {
                Console.WriteLine("Do you want to translate another word? y/n");

                if (!char.TryParse(Console.ReadLine().ToLower(), out validAnswer) ||
                   validAnswer.CompareTo('y') != 0 &&
                   validAnswer.CompareTo('n') != 0)
                {
                    Console.WriteLine("Enter y or n only.");
                    redoLoop = true;
                }

                else
                {
                    if (validAnswer.CompareTo('y') == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You chose to continue with another phrase.");
                        Console.WriteLine();
                        continuePigLatin = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Have a nice day. Goodbye.");
                        continuePigLatin = false;
                    }

                    redoLoop = false;
                }

            }

            return continuePigLatin;

        }

    }

}
