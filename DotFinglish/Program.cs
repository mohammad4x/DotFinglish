// See https://aka.ms/new-console-template for more information
using DotFinglish;

Console.WriteLine("Hello, World!");

var fileReaderUtility = new FileReaderUtility();

var beginning = await fileReaderUtility.GetFileDictionaryListAsync("f2p-beginning.txt", " ");
var middle = await fileReaderUtility.GetFileDictionaryListAsync("f2p-middle.txt", " ");
var end = await fileReaderUtility.GetFileDictionaryListAsync("f2p-ending.txt", " ");

var dict = await fileReaderUtility.GetFileDictionaryAsync<string>("f2p-dict.txt", ' ', '\t');
var persianDict = await fileReaderUtility.GetFileDictionaryAsync<int>("persian-word-freq.txt", ' ', '\t');

var finglish = new Finglish(beginning, middle, end, dict, persianDict, 15, 3);

var finglishText = Console.ReadLine();

var persianText = finglish.Convert(finglishText);

Console.ReadLine();
Console.ReadLine();