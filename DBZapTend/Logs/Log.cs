namespace DBZapTend.Logs
{
    public static class Log
    {
        public static async Task LogToFile(string file, string logMessage)
        {
            string fileName = file + DateTime.Now.ToString("ddMMyyyy") + ".txt";

            try
            {
                await using (StreamWriter swLog = new StreamWriter(fileName, true))
                {
                    await swLog.WriteLineAsync($"{DateTime.Now}: {logMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
            }
        }
    }
}
