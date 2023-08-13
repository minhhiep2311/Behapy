namespace Scripts;

public static class ConsoleCommon
{
    public static int Ask(string question, string[] options, char[] abbreviates) =>
        Ask(question, options, Array.ConvertAll<char, char?>(abbreviates, x => x));

    private static int Ask(string question, IReadOnlyList<string> options, IReadOnlyList<char?> abbreviates)
    {
        if (options.Count < abbreviates.Count) throw new Exception("Số phím tắt không được lớn hơn số lựa chọn!");

        var answerIdx = -1;
        var choice = options.Count > 0 ? BuildChoice(options, abbreviates) : "";

        do
        {
            Console.WriteLine();
            Console.WriteLine(question);
            Console.Write(choice);
            Console.Write("Lựa chọn: ");

            var answer = Console.ReadLine();

            if (answer is null) continue;

            for (var i = 0; i < options.Count; i++)
            {
                if ((answer.Length == 1 && answer[0].ToString() == i.ToString()) ||
                    (i < abbreviates.Count &&
                     answer.Length == 1 &&
                     abbreviates[i] is not null &&
                     char.ToLower(answer[0]) == char.ToLower(abbreviates[i]!.Value)) ||
                    string.Equals(answer, options[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    answerIdx = i;
                }
            }
        } while (answerIdx < 0);

        return answerIdx;
    }

    private static string BuildChoice(IReadOnlyList<string> options, IReadOnlyList<char?> abbreviates)
    {
        var result = "";

        for (var i = 0; i < options.Count; i++)
        {
            if (i < abbreviates.Count && abbreviates[i] != null)
            {
                result += $"({i}/{abbreviates[i]}) ";
            }

            result += options[i] + '\n';
        }

        return result;
    }
}