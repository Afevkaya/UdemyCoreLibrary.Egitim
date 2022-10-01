using System.Diagnostics;
using Hangfire;

namespace HangFireApp.Web.BackgroundJobs;

public class RecurringJobs
{
    // RecurringJob bir job türüdür.
    // RecurringJob belirli aralıklarla tekrarlanan joblar 'ı bu job altında tanımlanır.
    // Job zamanı istenilen zamana göre belirlenebilir. (Dakika, Saat, Gün vs.)
    public static void ReportingJob()
    {
        Hangfire.RecurringJob.AddOrUpdate("reportjob1", () => EmailReport(), Cron.Minutely);
    }


    public static void EmailReport()
    {
        Debug.WriteLine("Rapor, email olarak gönderildi");
    }
}