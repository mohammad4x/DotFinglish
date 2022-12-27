using System.Text;

namespace DotFinglish
{
    internal class FileReaderUtility
    {
        private async Task<IEnumerable<string>> ReadLinesAsync(string filePath)
        {
            var fileLines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);

            return fileLines.Where(x => !x.StartsWith('#')).Select(x => x.TrimEnd()).ToList();
        }

        public async Task<Dictionary<string, IEnumerable<string>>> GetFileDictionaryListAsync(string filePath, string delimiter)
        {
            var fileLines = await ReadLinesAsync(filePath);

            return fileLines.ToDictionary(x => x.Split(delimiter).First(), x => x.Split(delimiter).Skip(1).ToList().AsEnumerable());
        }

        public async Task<Dictionary<string, T>> GetFileDictionaryAsync<T>(string filePath, params char[] delimiters)
        {
            var fileLines = await ReadLinesAsync(filePath);

            return fileLines.ToDictionary(x => x.Split(delimiters).First(), x => x.Split(delimiters).Last().Convert<T>());
        }
    }
}
