using HangFireApp.Web.Services;

namespace HangFireApp.Web.BackgroundJobs;

// Fire And Forget bir job türüdür.
// Fire And Forget job bri defaya mahsus yapılması gereken joblar için kullanılır.
public class FireAndForgetJobs
{
    // job işlemi burada kodlanır.
    public static void EmailSendToUserJob(string userId, string message)
    {
        // email gönderme metodunu arka planda kuruğa ekleme
        Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));
    }
}