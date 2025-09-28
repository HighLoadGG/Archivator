using System.Text;

namespace ArchiverApp;

public static class Archiver
{


    /// <summary>
    /// Должна сжимать строку методом RLE.
    /// Текущая реализация не удовлетворяет всем тестам
    /// Также требуется использовать метод WriteRun для записи в outputLine и заменить тип string на StringBuilder.
    /// Пример: "aaabb" → "3a2b"
    /// Пример: "aabccc"   → "2ab3c"
    /// </summary>

    public static string CompressString(string inputLine)
    { 
        StringBuilder outputLine = new();
        int i = 0;
        int countTotalRepeat = 0;
        while (i < inputLine.Length)
        {
            char currentChar = inputLine[i];
            int count = 1;
            while (i + count < inputLine.Length && inputLine[i + count] == currentChar)
            {
                count++;
                countTotalRepeat++;
            }

            if (count > 1)
            {
                outputLine.Append(count);
                outputLine.Append("\\");
            }

            if(currentChar != '\\')
                outputLine.Append(currentChar);
            i += count;
        }
        return outputLine.Length <= inputLine.Length ? outputLine.ToString() : inputLine;
    }

    /// <summary>
    /// Заглушка: должна разжимать строку, сжатую методом RLE.
    /// Пример: "3a2b" → "aaabb"
    /// Пример: "2ab3c"   → "aabccc"
    /// Важно: при экранирова
    /// </summary>
    public static string DecompressString(string compressed) // 2\2
    {
        StringBuilder outputLine = new();
        if(!compressed.Contains('\\'))
            return compressed;
        int i = 0;
        while (i < compressed.Length)
        {
            int count = 0;
            int indNum = i;

            while (i < compressed.Length && char.IsDigit(compressed[i]))
            {
                count = count * 10 + (compressed[i] - '0');
                i += 1;
            }
            
            bool hasDigit = i > indNum;
            if(!hasDigit)
                count = 1;
            if (i >= compressed.Length)
                break;
            
            char currentChar = compressed[i];
            if (currentChar == '\\')
            {
                // 4\
                var countLess = 0;
                var ind = 0;
                while (char.IsDigit(compressed[ind]) && compressed.Length == i + 1 )
                {
                    countLess = countLess * 10 + (compressed[ind] - '0');
                    ind++;
                }
                if (countLess > 0)
                {
                    WriteRun(outputLine, currentChar, countLess);
                    break;
                }
                i++; //2
                if (i < compressed.Length)
                {
                    currentChar = compressed[i];
                    for (int j = 0; j < count; j++)
                    {
                        outputLine.Append(currentChar);
                    }

                    i++;
                }
            }
            else
            {
                if (hasDigit)
                {
                    for (int j = 0; j < count; j++)
                    {
                        outputLine.Append(currentChar);
                    }
                    i++;
                }
                else
                {
                    outputLine.Append(currentChar);
                    i++;
                }
            }
        }
        
        return outputLine.ToString() == string.Empty ? compressed : outputLine.ToString();
    }

    /// <summary>
    /// Заглушка: должна записывать (count, symbol) в выходной буфер.
    /// Пример: count=3, symbol='a' → "3a"
    /// Пример: count=1, symbol='b' → "b"
    /// </summary>
    public static void WriteRun(StringBuilder output, char symbol, int count)
    {
        for (int i = 0; i < count; i++)
        {
            output.Append(symbol);
        }
    }

    private static bool IsCharDigitAndEsсaping(char symbol)
    {
        return char.IsDigit(symbol) && symbol != '\\';
    }
}
