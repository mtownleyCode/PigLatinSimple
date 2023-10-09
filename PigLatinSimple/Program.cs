
using PigLatinSimple.Helpers;
using PigLatinSimple.Models;

bool continuePigLatin = true;

User user = new User();
UserInput userInput = new UserInput();
PigLatinTranslator pigLatinTranslator = new PigLatinTranslator();


Console.WriteLine("Welcome to the Pig Latin Translator App!");
Console.WriteLine();

while (continuePigLatin)
{
    userInput.GetWord(user);
    Console.WriteLine();

    userInput.SetCasePreference(user);
    Console.WriteLine();

    Console.WriteLine(pigLatinTranslator.ProcessPhrase(user));
    Console.WriteLine();

    continuePigLatin = userInput.ContinuePigLatin(user);
}