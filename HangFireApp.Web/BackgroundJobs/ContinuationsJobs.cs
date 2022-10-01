using System.Diagnostics;

namespace HangFireApp.Web.BackgroundJobs;

public class ContinuationsJobs
{
    // ContinuationsJob bir job türüdür.
    // ContinuationsJob bir job işlemini bitirdikten sonra arka planda işlenmesi istenilen joblar için kullanılır.
    // Bu yüzden bir job işini bitirdikten sonra bir jobId döner.
    // Bu sayede ContinuationsJob hangi job dan sonra çalışması gerektiğini bilebilir.
    public static void WriteWaterMarkStatusJob(string id, string fileName)
    {
        Hangfire.BackgroundJob.ContinueJobWith(id,
            () => Debug.WriteLine($"{fileName}: fotoğrafına watermark eklenmiştir"));
    }
}