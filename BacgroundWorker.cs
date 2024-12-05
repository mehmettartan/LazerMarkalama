using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace LazerMarka
{
    public class BacgroundWorker
    {
        private readonly BackgroundWorker worker;
        private bool running;

        // Her form için özel kontrol işlevlerini temsil eden delegeler
        public Action LazerKontrolMetodu { get; set; }

        public bool IsBusy => running; // IsBusy özelliği

        public BacgroundWorker()
        {
            worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true // İptal desteği ekleniyor
            };
            worker.DoWork += KontrolleriYurut;
            running = false;
        }

        public void Baslat()
        {
            if (!running)
            {
                running = true;
                worker.RunWorkerAsync();
            }
        }

        public void Durdur()
        {
            if (running)
            {
                running = false;
                if (worker.IsBusy)
                {
                    worker.CancelAsync(); // İşlem iptal edildi olarak işaretlenir
                }
            }
        }

        private void KontrolleriYurut(object sender, DoWorkEventArgs e)
        {
            while (running)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true; // İşlemi iptal ettiğimizi belirtiyoruz
                    break;
                }

                // Her formun kendi özel kontrol işlevlerini çağır
                LazerKontrolMetodu?.Invoke();

                Thread.Sleep(50); // 50 ms bekle
            }
        }
    }
}

