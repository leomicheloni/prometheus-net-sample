using Prometheus;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using var server = new KestrelMetricServer(port: 1234);
            server.Start();

            // Generate some sample data from fake business logic.
            var recordsProcessed = Metrics.CreateCounter("sample_records_processed_total", "Total number of records processed.");


            _ = Task.Run(async delegate
            {
                while (true)
                {
                    // Pretend to process a record approximately every second, just for changing sample data.
                    recordsProcessed.Inc();

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });

            Console.WriteLine("Listening on port 1234");
            Console.ReadLine();
        }
    }
}