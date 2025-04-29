using Microsoft.KernelMemory;

var memory = new MemoryWebClient("http://127.0.0.1:9001/");

var documentId1 = "195c4f89508b";
var documentId2 = "36fa9724f348";

await memory.ImportWebPageAsync(
    "https://vizsphere.com/faq/",
    documentId1);
await memory.ImportWebPageAsync("https://www.microsoft.com/en-us/education/products/frequently-asked-questions",
                                documentId2);

Console.WriteLine("Waiting for memory ingestion to complete...");

while (!await memory.IsDocumentReadyAsync(documentId1))
{
    Console.WriteLine("Ask your questions\n");

    await Task.Delay(TimeSpan.FromMilliseconds(1500));

    var question = Console.ReadLine();

    var answer = await memory.AskAsync(question);

    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine(answer.Result);

    Console.ForegroundColor = ConsoleColor.DarkBlue;

    Console.WriteLine("\n Source:");

    foreach (var item in answer.RelevantSources)
    {
        Console.WriteLine($"{item.SourceName} - {item.SourceUrl} - {item.Link}");
    }

    Console.ForegroundColor = ConsoleColor.White;
}
