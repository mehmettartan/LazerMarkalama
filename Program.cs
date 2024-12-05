using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LazerMarka
{
    static class Program
    {
        private static Form2 mainForm; // Ana formu tanımlayın.

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ana formu başlat


            // Splash ekranını göster ve ana formu arka planda yükle
            using (SplashForm splashForm = new SplashForm())
            {
                Thread splashThread = new Thread(() =>
                {
                    Application.Run(splashForm); // ShowDialog yerine Application.Run
                });

                splashThread.Start();

                // Ana formu arka planda yükle
                LoadMainForm();

                // Splash ekranını kapat
                splashForm.Invoke(new Action(() => splashForm.Close()));
                splashThread.Join();

            }

            // Ana formu çalıştır
            Application.Run(mainForm);
        }

        private static void LoadMainForm()
        {
            // Ana formu arka planda oluştur
            mainForm = new Form2();
            Thread.Sleep(8000); // Simülasyon için bekleme, gerçek yükleme işlemini buraya koyabilirsiniz.
        }
    }
}
