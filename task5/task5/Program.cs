string[] GetLongestWords(string[] text, int n)
{
var words = text.SelectMany(s => s.ToLower().Split(' ', ',', '.', '!', '?', ';', ':', '"','-', '(', ')', '[', ']'))
                .Distinct()
                .OrderByDescending(w => w.Length)
                .ThenBy(w => w)
                .Take(n)
                .ToArray();
return words;
}

static void PrintMaxDurationMonth(int clientId, List<Record> visits)
{
var clientVisits = visits.Where(v => v.ClientID == clientId);

if (clientVisits.Any())
{
var visitsByYear = clientVisits.GroupBy(v => v.Year);

var maxDurationMonths = visitsByYear.Select(g => new
{
Year = g.Key,
Month = g.GroupBy(v => v.Month)
              .OrderByDescending(mg => mg.Sum(v => v.Duration))
              .ThenBy(mg => mg.Key)
              .First().Key,
Duration = g.GroupBy(v => v.Month)
               .OrderByDescending(mg => mg.Sum(v => v.Duration))
               .First().Sum(v => v.Duration)
});

var sortedResults = maxDurationMonths.OrderByDescending(r => r.Year);

foreach (var result in sortedResults)
{
Console.WriteLine($"{result.Year} {result.Month} {result.Duration}");
}
}
else
{
Console.WriteLine($"Нет данных для клиента с кодом {clientId}");
}
}

class Record
{
    public int ClientID { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Duration { get; set; }

    public Record(int clientId, int year, int month, int duration)
    {
        ClientID = clientId;
        Year = year;
        Month = month;
        Duration = duration;
    }
};
