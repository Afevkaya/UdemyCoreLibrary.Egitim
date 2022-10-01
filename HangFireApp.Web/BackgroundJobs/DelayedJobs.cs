using System.Drawing;

namespace HangFireApp.Web.BackgroundJobs;

public class DelayedJobs
{
    // DelayedJob bir job türüdür.
    // DelayedJob bir defaya mahsus yapılması gereken joblar için kullanılır.
    // FireAndForget job türünden farkı arka tarafta işleme konulacak job belli bir zaman sonra işleme konulur.
    // Yani job bir müddet sonra işlenmeye başlar.
    public static string AddWaterMarkJob(string fileName, string waterMarkText)
    {
        return Hangfire.BackgroundJob.Schedule(() =>
                ApplyWaterMark(fileName, waterMarkText),
            TimeSpan.FromSeconds(20)
        );
    }

    // Fotoğrafa yazı yazıp yeni bir foto olarak kaydedecek metod.
    public static void ApplyWaterMark(string fileName, string waterMarkText)
    {
        // Fotoğrafın yolunu birleştirdik.
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", fileName);
        // Fotoğrafın yolunu aldık
        using (var bitmap = Bitmap.FromFile(path))
        {
            // Fotografın width ve height'ını aldık.
            // Bizim fotoğrafın boyutlarında bir çerçeve oluşturduk.
            using (Bitmap tempBitMap = new Bitmap(bitmap.Width, bitmap.Height))
            {
                // fotoğrafı çerçevenin içine çizecez ve fotoğrafa bir yazı ekleyeceğiz.
                using (Graphics graphics = Graphics.FromImage(tempBitMap))
                {
                    // Foto çizme
                    graphics.DrawImage(bitmap, 0, 0);
                    // Yazıya ait gerekli bilgiler.
                    var font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                    var color = Color.FromArgb(255, 0, 0);
                    var brush = new SolidBrush(color);
                    var point = new Point(20, bitmap.Height - 50);
                    // Yazıyı fotoya yazma
                    graphics.DrawString(waterMarkText, font, brush, point);
                    tempBitMap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures/watermarks",
                        fileName));
                }
            }
        }
    }
}