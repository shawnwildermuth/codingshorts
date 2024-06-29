using static System.Console;

// IEnumerable<int> items = Enumerable.Range(1,1000);

// Span<int> part = items;

var name = "Shawn Wildermuth";
var last = name.AsSpan(6);
var first = name.AsSpan(0,5);


