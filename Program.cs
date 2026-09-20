Console.WriteLine("Vocab app!");

List<Word> words = [
  // Swedish - english
  new("swedish", "snabb", "english", "fast"),
  new("swedish", "snabb", "english", "quick"),
  new("swedish", "snabb", "english", "rapid"),
  new("swedish", "glad", "english", "happy"),
  new("swedish", "stor", "english", "big"),
  new("swedish", "stor", "english", "large"),

  // Swedish - italian
  new("swedish", "snabb", "italian", "veloce"),
  new("swedish", "snabb", "italian", "rapido"),
  new("swedish", "glad", "italian", "felice"),
  new("swedish", "stor", "italian", "grande"),

  // English – italian
  new("english", "quick", "italian", "veloce"),
  new("english", "rapid", "italian", "rapido"),
  new("english", "happy", "italian", "felice"),
  new("english", "big", "italian", "grande"),
  new("english", "large", "italian", "grande"),

  // English – spanish
  new("english", "quick", "spanish", "rápido"),
  new("english", "rapid", "spanish", "rápido"),
  new("english", "happy", "spanish", "feliz"),
  new("english", "big", "spanish", "grande"),
  new("english", "large", "spanish", "grande")
];

// create dictionaries and a menu to lookup translations in any direction
new VocabMenu(words);



