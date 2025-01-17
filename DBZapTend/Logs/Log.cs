using System.Data;

namespace DBZapTend.Logs
{

    public static class Log
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public static async Task LogToFile(string file, string logMessage)
        {
            string fileName = file + DateTime.Now.ToString("ddMMyyyy") + ".txt";

            try
            {
                
                await _semaphore.WaitAsync();

                
                await using (StreamWriter swLog = new StreamWriter(fileName, true))
                {
                    
                    await swLog.WriteLineAsync($"{DateTime.Now}: {logMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new($"Erro ao escrever no arquivo de log: {ex.Message}");
            }
            finally
            {
                
                _semaphore.Release();
            }
        }
    }
}
