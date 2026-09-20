class VocabMenu
{
  Dictionary<string, List<Word>> fromSwedishToEnglish;
  Dictionary<string, List<Word>> fromEnglishToSwedish;
  Dictionary<string, List<Word>>? selectedDictionary;

  string selectedFromLanguage = "";
  string selectedToLanguage = "";

  bool reverseTranslation = false;

  public VocabMenu(List<Word> words)
  {
    // Ingen datatyp här – vi tilldelar klassens fält.
    fromSwedishToEnglish = words
    .Where(word => word.FromLanguage == "swedish")
    .Where(word => word.ToLanguage == "english")
    .GroupBy(word => word.WordFromLanguage)
    .ToDictionary(
        group => group.Key,
        group => group.ToList(),
        StringComparer.OrdinalIgnoreCase
    );

    fromEnglishToSwedish = words
        .Where(word => word.FromLanguage == "swedish")
        .Where(word => word.ToLanguage == "english")
        .GroupBy(word => word.WordToLanguage)
        .ToDictionary(
            group => group.Key,
            group => group.ToList(),
            StringComparer.OrdinalIgnoreCase
        );

    RunMenu();
  }

  void RunMenu()
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
        selectedDictionary = fromSwedishToEnglish;
        reverseTranslation = false;
        selectedFromLanguage = "svenska";
        selectedToLanguage = "engelska";
        break;

      case "2":
        selectedDictionary = fromEnglishToSwedish;
        reverseTranslation = true;
        selectedFromLanguage = "engelska";
        selectedToLanguage = "svenska";
        break;

      default:
        Console.WriteLine("Ogiltigt språkval.");
        return;
    }

    Console.WriteLine(
        $"Valt: {selectedFromLanguage} → {selectedToLanguage}"
    );
  }

  void TranslateWord()
  {
    if (selectedDictionary == null)
    {
      Console.WriteLine("Du måste först välja språk.");
      return;
    }

    Console.Write($"\nAnge ett ord på {selectedFromLanguage}: ");
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
      Console.WriteLine($"Översättning till {selectedToLanguage}:");

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
}