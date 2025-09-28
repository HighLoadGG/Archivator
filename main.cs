using ArchiverApp;

var compress = "111aaa222bbb";
Console.WriteLine(compress);

var compressed = Archiver.CompressString(compress);
Console.WriteLine(compressed);

var decompressed = Archiver.DecompressString(compressed);
Console.WriteLine(decompressed);
