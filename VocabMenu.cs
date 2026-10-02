class VocabMenu
{
  List<Word>? selectedWordList;
  Dictionary<string, List<Word>>? selectedDictionary;
  string filePath = "";
  bool reverseTranslation = false;

  public VocabMenu()
  {
    bool isRunning = true;

    while (isRunning)
    {
      Console.WriteLine("\nVOCABS");
      Console.WriteLine("1. Välj språk");
      Console.WriteLine("2. Ange ord och få översättning");
      Console.WriteLine("3. Avsluta");
      Console.Write("Ditt val: ");

      string? choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
          SelectLanguages();
          break;

        case "2":
          TranslateWord();
          break;

        case "3":
          isRunning = false;
          Console.WriteLine("Programmet avslutas.");
          break;

        default:
          Console.WriteLine("Ogiltigt menyval.");
          break;
      }
    }
  }

  void SelectLanguages()
  {
    Console.WriteLine("\nVälj språkriktning:");
    Console.WriteLine("1. Svenska → engelska");
    Console.WriteLine("2. Engelska → svenska");
    Console.Write("Ditt val: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
      case "1":
        reverseTranslation = false;
        filePath = "wordlists/swedish-english.txt";
        break;

      case "2":
        reverseTranslation = true;
        filePath = "wordlists/swedish-english.txt";
        break;

      default:
        Console.WriteLine("Ogiltigt språkval.");
        return;
    }

    // The file is always stored as "swedish;english", regardless of direction.
    // Reversing is done by choosing which side of the word pair to use as key.
    selectedWordList = LoadWords(filePath, "swedish", "english");
    selectedDictionary = selectedWordList
    .GroupBy(word => reverseTranslation ? word.WordToLanguage : word.WordFromLanguage)
    .ToDictionary(
        group => group.Key,
        group => group.ToList(),
        StringComparer.OrdinalIgnoreCase
    );

    // foreach (var word in selectedWordList)
    // {
    //   Console.WriteLine($"{word.FromLanguage}: {word.WordFromLanguage} => {word.ToLanguage}: {word.WordToLanguage}");
    // }

  }

  void TranslateWord()
  {
    if (selectedDictionary == null)
    {
      Console.WriteLine("Du måste först välja språk.");
      return;
    }

    Console.Write($"\nAnge ett ord att översätta: ");
    string? searchWord = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(searchWord))
    {
      Console.WriteLine("Du måste ange ett ord.");
      return;
    }

    if (selectedDictionary.TryGetValue(
        searchWord.Trim(),
        out List<Word>? matches))
    {
      Console.WriteLine($"Översättning:");

      foreach (Word match in matches)
      {
        if (reverseTranslation)
        {
          Console.WriteLine($"- {match.WordFromLanguage}");
        }
        else
        {
          Console.WriteLine($"- {match.WordToLanguage}");
        }
      }
    }
    else
    {
      Console.WriteLine($"Ordet \"{searchWord}\" hittades inte.");
    }
  }

  public List<Word> LoadWords(string filePath, string fromLanguage, string toLanguage)
  {
    List<Word> words = [];

    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
      string[] parts = line.Split(';');

      Word word = new(
          fromLanguage,
          parts[0],
          toLanguage,
          parts[1]
      );

      words.Add(word);
    }

    return words;
  }

}