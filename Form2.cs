using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace LazerMarka
{
    public partial class Form2 : Form
    {
        private System.Windows.Forms.Timer timerControl;
        Thread threadRead, threadWriteBool, threadReadString, threadWriteString;

        bool boolReadStatus;

        private Dictionary<string, string> lastValidValues = new Dictionary<string, string>();

        Dictionary<string, (double Min, double Max)> valueRanges = new Dictionary<string, (double Min, double Max)>
        {
            // 1, 4, 7, 10, 13, 16, 19, 22, 25, 28 için aynı min ve max değerini atıyoruz
            { "txBx1Pos1", (Min: 0, Max: 1690) },
            { "txBx1Pos4", (Min: 0, Max: 1690) },
            { "txBx1Pos7", (Min: 0, Max: 1690) },
            { "txBx1Pos10", (Min: 0, Max: 1690) },
            { "txBx1Pos13", (Min: 0, Max: 1690) },
            { "txBx1Pos16", (Min: 0, Max: 1690) },
            { "txBx1Pos19", (Min: 0, Max: 1690) },
            { "txBx1Pos22", (Min: 0, Max: 1690) },
            { "txBx1Pos25", (Min: 0, Max: 1690) },
            { "txBx1Pos28", (Min: 0, Max: 1690) },

            // 2, 5, 8, 11, 14, 17, 20, 23, 26, 29 için aynı min ve max değerini atıyoruz
            { "txBx1Pos2", (Min: 0, Max: 300) },
            { "txBx1Pos5", (Min: 0, Max: 300) },
            { "txBx1Pos8", (Min: 0, Max: 300) },
            { "txBx1Pos11", (Min: 0, Max: 300) },
            { "txBx1Pos14", (Min: 0, Max: 300) },
            { "txBx1Pos17", (Min: 0, Max: 300) },
            { "txBx1Pos20", (Min: 0, Max: 300) },
            { "txBx1Pos23", (Min: 0, Max: 300) },
            { "txBx1Pos26", (Min: 0, Max: 300) },
            { "txBx1Pos29", (Min: 0, Max: 300) },

            // 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 için aynı min ve max değerini atıyoruz
            { "txBx1Pos3", (Min: 0, Max: 260) },
            { "txBx1Pos6", (Min: 0, Max: 260) },
            { "txBx1Pos9", (Min: 0, Max: 260) },
            { "txBx1Pos12", (Min: 0, Max: 260) },
            { "txBx1Pos15", (Min: 0, Max: 260) },
            { "txBx1Pos18", (Min: 0, Max: 260) },
            { "txBx1Pos21", (Min: 0, Max: 260) },
            { "txBx1Pos24", (Min: 0, Max: 260) },
            { "txBx1Pos27", (Min: 0, Max: 260) },
            { "txBx1Pos30", (Min: 0, Max: 260) },

                        // 1, 4, 7, 10, 13, 16, 19, 22, 25, 28 için aynı min ve max değerini atıyoruz
            { "txBx2Pos1", (Min: 0, Max: 1690) },
            { "txBx2Pos4", (Min: 0, Max: 1690) },
            { "txBx2Pos7", (Min: 0, Max: 1690) },
            { "txBx2Pos10", (Min: 0, Max: 1690) },
            { "txBx2Pos13", (Min: 0, Max: 1690) },
            { "txBx2Pos16", (Min: 0, Max: 1690) },
            { "txBx2Pos19", (Min: 0, Max: 1690) },
            { "txBx2Pos22", (Min: 0, Max: 1690) },
            { "txBx2Pos25", (Min: 0, Max: 1690) },
            { "txBx2Pos28", (Min: 0, Max: 1690) },
        
            // 2, 5, 8, 11, 14, 17, 20, 23, 26, 29 için aynı min ve max değerini atıyoruz
            { "txBx2Pos2", (Min: 0, Max: 300) },
            { "txBx2Pos5", (Min: 0, Max: 300) },
            { "txBx2Pos8", (Min: 0, Max: 300) },
            { "txBx2Pos11", (Min: 0, Max: 300) },
            { "txBx2Pos14", (Min: 0, Max: 300) },
            { "txBx2Pos17", (Min: 0, Max: 300) },
            { "txBx2Pos20", (Min: 0, Max: 300) },
            { "txBx2Pos23", (Min: 0, Max: 300) },
            { "txBx2Pos26", (Min: 0, Max: 300) },
            { "txBx2Pos29", (Min: 0, Max: 300) },

            // 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 için aynı min ve max değerini atıyoruz
            { "txBx2Pos3", (Min: 0, Max: 260) },
            { "txBx2Pos6", (Min: 0, Max: 260) },
            { "txBx2Pos9", (Min: 0, Max: 260) },
            { "txBx2Pos12", (Min: 0, Max: 260) },
            { "txBx2Pos15", (Min: 0, Max: 260) },
            { "txBx2Pos18", (Min: 0, Max: 260) },
            { "txBx2Pos21", (Min: 0, Max: 260) },
            { "txBx2Pos24", (Min: 0, Max: 260) },
            { "txBx2Pos27", (Min: 0, Max: 260) },
            { "txBx2Pos30", (Min: 0, Max: 260) }
        };

        private BacgroundWorker bacgroundWorker;

        private int currentState1 = 0;
        private int currentState2 = 0;

        private int myInteger;

        string[] station1Manuel1 = new string[9];
        string[] station2Manuel2 = new string[9];

        List<TextBox> textBoxes;
        List<TextBox> textBoxes2;

        List<Button> buttons;
        List<Button> buttons2;
        public Form2()
        {
            InitializeComponent();

            textBoxes = new List<TextBox>
            {
                txBx1Pos1, txBx1Pos2, txBx1Pos3, txBx1Pos4, txBx1Pos5,
                txBx1Pos6, txBx1Pos7, txBx1Pos8, txBx1Pos9, txBx1Pos10,
                txBx1Pos11, txBx1Pos12, txBx1Pos13, txBx1Pos14, txBx1Pos15,
                txBx1Pos16, txBx1Pos17, txBx1Pos18, txBx1Pos19, txBx1Pos20,
                txBx1Pos21, txBx1Pos22, txBx1Pos23, txBx1Pos24, txBx1Pos25,
                txBx1Pos26, txBx1Pos27, txBx1Pos28, txBx1Pos29, txBx1Pos30
            };

            textBoxes2 = new List<TextBox>
            {
                txBx2Pos1, txBx2Pos2, txBx2Pos3, txBx2Pos4, txBx2Pos5,
                txBx2Pos6, txBx2Pos7, txBx2Pos8, txBx2Pos9, txBx2Pos10,
                txBx2Pos11, txBx2Pos12, txBx2Pos13, txBx2Pos14, txBx2Pos15,
                txBx2Pos16, txBx2Pos17, txBx2Pos18, txBx2Pos19, txBx2Pos20,
                txBx2Pos21, txBx2Pos22, txBx2Pos23, txBx2Pos24, txBx2Pos25,
                txBx2Pos26, txBx2Pos27, txBx2Pos28, txBx2Pos29, txBx2Pos30
            };

            buttons = new List<Button> { btnPos1, btnPos2, btnPos3, btnPos4, btnPos5, btnPos6, btnPos7, btnPos8, btnPos9, btnPos10 };

            buttons2 = new List<Button> { btnPos11, btnPos12, btnPos13, btnPos14, btnPos15, btnPos16, btnPos17, btnPos18, btnPos19, btnPos20 };

            bacgroundWorker = new BacgroundWorker();
            bacgroundWorker.LazerKontrolMetodu = LazerKontrolMetodu;

            txbxPos1.KeyDown += txbxPos1_KeyDown;
            txbxPos2.KeyDown += txbxPos2_KeyDown;

            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = $"txBx1Pos{i}";
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    // TextBox'a 'Leave' event'ını bağlama
                    textBox.Leave += TextBox_Leave;
                }
            }

            // Tüm TextBox'ların TextChanged olayını bağlama
            foreach (var key in valueRanges.Keys)
            {
                var textBox = Controls.Find(key, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {

                    textBox.KeyDown += TextBox_KeyDown;

                    textBox.KeyPress += TextBox_KeyPress;
                }
            }
        }

        private void LazerKontrolMetodu()
        {
            // Burada konveyor kontrollerini gerçekleştirin
            for (int i = 1; i < 6; i++)
            {
                int istasyonNo = i;

                // Form oluşturulduğunda ve aktif olduğunda
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SaatDakika();
                        markSureA();
                        UretilenAdetA();
                        markSureB();
                        UretilenAdetB();
                        SistemDurumA();
                        SistemDurumB();
                        M1HomeDurum();
                        M2HomeDurum();
                        ReadACT();
                        CalıanModel();
                        CalıanModel2();
                        AcilStopRead();
                        SolKapi();
                        ArkaKapi();
                        Bariyer1();
                        PLCHaberlesme();
                        MotorAriza();

                        // Diğer metodları da aynı şekilde kontrol edin
                    });
                }
            }
        }

        public void SaatDakika()
        {
            string dakika = nxCompoletStringRead("M2LazerCalisma[1]");
            string saat = nxCompoletStringRead("M2LazerCalisma[2]");
            string adet = nxCompoletStringRead("M2LazerCalisma[0]");

            txbxLaserDakika.Text = dakika;
            txbxLaserSaat.Text = saat;
            txbxLaserAdet.Text = adet;
        }

        public void AcilStopRead()
        {
            bool acilStopDurum = nxCompoletBoolRead("M2Donanim[0]");
            
            switch (acilStopDurum)
            {
                case false:
                    btnAcil.Text = "ACİL STOP BASILI";
                    btnAcil.BackColor = Color.Maroon;
                    btnAcil.ForeColor = Color.WhiteSmoke;
                    break;

                case true:
                    btnAcil.Text = "ACİL STOP BASILI DEGİL";
                    btnAcil.BackColor = Color.DarkGreen;
                    btnAcil.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void SolKapi()
        {
            bool solKapi = nxCompoletBoolRead("M2Donanim[1]");

            switch (solKapi)
            {
                case false:
                    btnSolKapi.Text = "SOL KAPI AÇIK";
                    btnSolKapi.BackColor = Color.Maroon;
                    btnSolKapi.ForeColor = Color.WhiteSmoke;
                    break;

                case true:
                    btnSolKapi.Text = "SOL KAPI KAPALI";
                    btnSolKapi.BackColor = Color.DarkGreen;
                    btnSolKapi.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void ArkaKapi()
        {
            bool arkaKapi = nxCompoletBoolRead("M2Donanim[2]");

            switch (arkaKapi)
            {
                case false:
                    btnArkaKapi.Text = "ARKA KAPI AÇIK";
                    btnArkaKapi.BackColor = Color.Maroon;
                    btnArkaKapi.ForeColor = Color.WhiteSmoke;
                    break;

                case true:
                    btnArkaKapi.Text = "ARKA KAPI KAPALI";
                    btnArkaKapi.BackColor = Color.DarkGreen;
                    btnArkaKapi.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void Bariyer1()
        {
            bool bariyer1 = nxCompoletBoolRead("M2Donanim[3]");

            switch (bariyer1)
            {
                case false:
                    btnBariyer1.Text = "BARİYER SENSÖR AKTİF";
                    btnBariyer1.BackColor = Color.Maroon;
                    btnBariyer1.ForeColor = Color.WhiteSmoke;
                    break;

                case true:
                    btnBariyer1.Text = "BARİYER SENSÖR PASİF";
                    btnBariyer1.BackColor = Color.DarkGreen;
                    btnBariyer1.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void PLCHaberlesme()
        {
            bool plcHab = nxCompoletBoolRead("M2Donanim[4]");

            switch (plcHab)
            {
                case false:
                    btnPLCHab1.Text = "PLC HABERLEŞME PASİF";
                    btnPLCHab1.BackColor = Color.Maroon;
                    btnPLCHab1.ForeColor = Color.WhiteSmoke;
                    break;

                case true:
                    btnPLCHab1.Text = "PLC HABERLEŞME AKTİF";
                    btnPLCHab1.BackColor = Color.DarkGreen;
                    btnPLCHab1.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void MotorAriza()
        {
            bool motorAriza = nxCompoletBoolRead("M2Donanim[5]");

            switch (motorAriza)
            {
                case true:
                    btnMotorAriza.Text = "MOTOR ALARM VAR";
                    btnMotorAriza.BackColor = Color.Maroon;
                    btnMotorAriza.ForeColor = Color.WhiteSmoke;
                    break;

                case false:
                    btnMotorAriza.Text = "MOTOR ALARM YOK";
                    btnMotorAriza.BackColor = Color.DarkGreen;
                    btnMotorAriza.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void CalıanModel()
        {
            string sure1 = nxCompoletStringRead("M2FaCalisanModel");
            txBxCalısanMdl1.Text = sure1;
        }

        public void CalıanModel2()
        {
            string sure2 = nxCompoletStringRead("M2FbCalisanModel");
            txBxCalısanMdl2.Text = sure2;
        }

        public void markSureA()
        {
            string sure1 = nxCompoletStringRead("M2FaSure");
            txBxMarkaSuresi1.Text = sure1;
        }
        public void UretilenAdetA()
        {
            string uretilenAdt1 = nxCompoletStringRead("M2FaUretilenAdet");
            txBxUretlenAdt1.Text = uretilenAdt1;
        }

        public void markSureB()
        {
            string sure2 = nxCompoletStringRead("M2FbSure");
            txBxMarkaSuresi2.Text = sure2;
        }
        public void UretilenAdetB()
        {
            string uretilenAdt2 = nxCompoletStringRead("M2FbUretilenAdet");
            txBxUretlenAdt2.Text = uretilenAdt2;
        }

        public void SistemDurumA()
        {
            string uretimDurum1 = nxCompoletStringRead("M2FaUretimDurum");
            btnSistemDurum1.Text = uretimDurum1;

            switch (uretimDurum1)
            {
                case "0":
                    btnSistemDurum1.Text = "SİSTEM HAZIR DEĞİL";
                    break;
                case "1":
                    btnSistemDurum1.Text = "OPERATOR BEKLENİYOR";
                    break;
                case "2":
                    btnSistemDurum1.Text = "LAZER BEKLENİYOR";
                    break;
                case "3":
                    btnSistemDurum1.Text = "MARKALAMA YAPILIYOR";
                    break;
                case "4":
                    btnSistemDurum1.Text = "SİSTEM DURDURULDU";
                    break;
            }
        }

        public void SistemDurumB()
        {
            string uretimDurum2 = nxCompoletStringRead("M2FbUretimDurum");
            btnSistemDurum2.Text = uretimDurum2;

            switch (uretimDurum2)
            {
                case "0":
                    btnSistemDurum2.Text = "SİSTEM HAZIR DEĞİL";
                    break;
                case "1":
                    btnSistemDurum2.Text = "OPERATOR BEKLENİYOR";
                    break;
                case "2":
                    btnSistemDurum2.Text = "LAZER BEKLENİYOR";
                    break;
                case "3":
                    btnSistemDurum2.Text = "MARKALAMA YAPILIYOR";
                    break;
                case "4":
                    btnSistemDurum2.Text = "SİSTEM DURDURULDU";
                    break;
            }
        }

        public void M1HomeDurum()
        {
            string homeDurum = nxCompoletStringRead("M2HomeDurum");

            switch (homeDurum)
            {
                case "0":
                    btnHome1.Text = "HOME NOT OK";
                    btnHome1.BackColor = Color.Maroon;
                    btnHome1.ForeColor = Color.WhiteSmoke;
                    break;
                case "1":
                    btnHome1.Text = "HOME YAPILIYOR";
                    btnHome1.BackColor = Color.LightYellow;
                    btnHome1.ForeColor = Color.Black;
                    break;
                case "2":
                    btnHome1.Text = "HOME OK";
                    btnHome1.BackColor = Color.DarkGreen;
                    btnHome1.ForeColor = Color.WhiteSmoke;
                    break;
            }
        }

        public void M2HomeDurum()
        {
            string sure2 = nxCompoletStringRead("M2HomeDurum");
        }

        public void ReadACT()
        {
            string act0 = nxCompoletDoubleRead("M2AxisPos[0]");
            string act4 = nxCompoletDoubleRead("M2AxisPos[1]");
            string act2 = nxCompoletDoubleRead("M2AxisPos[2]");
            string act3 = nxCompoletDoubleRead("M2AxisPos[3]");

            txBxAct1.Text = FormatDecimalString(act0);
            txBxAct2.Text = FormatDecimalString(act2);
            txBxAct3.Text = FormatDecimalString(act3);
            txBxAct4.Text = FormatDecimalString(act4);
            txBxAct5.Text = FormatDecimalString(act2);
            txBxAct6.Text = FormatDecimalString(act3);
        }

        private string FormatDecimalString(string value)
        {
            if (double.TryParse(value, out double number))
            {
                // Sayıyı virgülden sonra 2 basamaklı hale getir ve metin olarak döndür
                return number.ToString("F2");
            }
            return value; // Sayı değilse orijinal değeri döndür
        }

        private void btnXLeft1_MouseDown(object sender, MouseEventArgs e)
        {
            btnXLeft1.BackColor = Color.DarkGreen;
            station1Manuel1[0] = "M2FaJogButon[0]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[0], true));
            threadWriteBool.Start();
        }

        private void btnXLeft1_MouseUp(object sender, MouseEventArgs e)
        {
            btnXLeft1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[0] = "M2FaJogButon[0]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[0], false));
            threadWriteBool.Start();
        }

        private void btnYUp1_MouseDown(object sender, MouseEventArgs e)
        {
            btnYUp1.BackColor = Color.DarkGreen;
            station1Manuel1[2] = "M2FaJogButon[2]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[2], true));
            threadWriteBool.Start();
        }

        private void btnYUp1_MouseUp(object sender, MouseEventArgs e)
        {
            btnYUp1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[2] = "M2FaJogButon[2]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[2], false));
            threadWriteBool.Start();
        }

        private void btnXRight1_MouseDown(object sender, MouseEventArgs e)
        {
            btnXRight1.BackColor = Color.DarkGreen;
            station1Manuel1[1] = "M2FaJogButon[1]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[1], true));
            threadWriteBool.Start();
        }

        private void btnXRight1_MouseUp(object sender, MouseEventArgs e)
        {
            btnXRight1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[1] = "M2FaJogButon[1]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[1], false));
            threadWriteBool.Start();
        }

        private void btnYDown1_MouseDown(object sender, MouseEventArgs e)
        {
            btnYDown1.BackColor = Color.DarkGreen;
            station1Manuel1[3] = "M2FaJogButon[3]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[3], true));
            threadWriteBool.Start();
        }

        private void btnYDown1_MouseUp(object sender, MouseEventArgs e)
        {
            btnYDown1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[3] = "M2FaJogButon[3]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[3], false));
            threadWriteBool.Start();
        }

        private void btnZUp1_MouseDown(object sender, MouseEventArgs e)
        {
            btnZUp1.BackColor = Color.DarkGreen;
            station1Manuel1[4] = "M2FaJogButon[4]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[4], true));
            threadWriteBool.Start();
        }

        private void btnZUp1_MouseUp(object sender, MouseEventArgs e)
        {
            btnZUp1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[4] = "M2FaJogButon[4]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[4], false));
            threadWriteBool.Start();
        }

        private void btnZDown1_MouseDown(object sender, MouseEventArgs e)
        {
            btnZDown1.BackColor = Color.DarkGreen;
            station1Manuel1[5] = "M2FaJogButon[5]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[5], true));
            threadWriteBool.Start();
        }

        private void btnZDown1_MouseUp(object sender, MouseEventArgs e)
        {
            btnZDown1.BackColor = Color.FromArgb(31, 31, 31);
            station1Manuel1[5] = "M2FaJogButon[5]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station1Manuel1[5], false));
            threadWriteBool.Start();
        }

        private void txbxJogHareket1_Click(object sender, EventArgs e)
        {
            // Duruma göre butonun metnini değiştir
            switch (currentState1)
            {
                case 0:
                    txbxJogHareket1.Text = "SÜREKLİ";  // 2. Durum -> 3. Durum
                    nxCompoletStringWrite("M2FaJogHareket", "0");
                    break;
                case 1:
                    txbxJogHareket1.Text = "1 mm";  // 2. Durum -> 3. Durum
                    nxCompoletStringWrite("M2FaJogHareket", "1");
                    break;
                case 2:
                    txbxJogHareket1.Text = "0.5 mm";    // 3. Durum -> 4. Durum
                    nxCompoletStringWrite("M2FaJogHareket", "2");
                    break;
                case 3:
                    txbxJogHareket1.Text = "0.1 mm";  // 4. Durum -> 1. Durum (geri başa döner)
                    nxCompoletStringWrite("M2FaJogHareket", "3");
                    break;
            }

            // Durumu artır ve 4. durumdan sonra tekrar sıfırla
            currentState1 = (currentState1 + 1) % 4;
        }

        private void txbxJogHiz1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter tuşuna basıldığında işlemi gerçekleştirin
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun standart "ding" sesini engeller

                // Girilen metni bir sayıya dönüştür
                if (int.TryParse(txbxJogHiz1.Text, out int number))
                {
                    // Girilen sayının belirtilen aralıkta olup olmadığını kontrol edin
                    if (number >= 1 && number <= 100)
                    {
                        // Değer geçerli ise işlemi başlat
                        threadWriteString = new Thread(() => nxCompoletStringWrite("M2FaJogHiz", txbxJogHiz1.Text));
                        threadWriteString.Start();
                    }
                    else
                    {
                        // Uygun aralıkta değilse uyarı göster ve TextBox'ı temizle
                        txbxJogHiz1.Text = "20";
                        nxCompoletStringWrite("M2FaJogHiz", "20");
                        MessageBox.Show("Lütfen 1 ile 100 arasında bir sayı girin.");

                    }
                }
                else
                {
                    // Geçerli bir sayı değilse uyarı göster ve TextBox'ı temizle
                    txbxJogHiz1.Text = "20";
                    nxCompoletStringWrite("M2FaJogHiz", "20");
                    MessageBox.Show("Lütfen 1 ile 100 arasında bir sayı girin.");

                }
            }
        }

        private void txbxJogHareket2_Click(object sender, EventArgs e)
        {
            // Duruma göre butonun metnini değiştir
            switch (currentState2)
            {
                case 0:
                    txbxJogHareket2.Text = "SÜREKLİ";  // 2. Durum -> 3. Durum
                    nxCompoletStringWrite("M2FbJogHareket", "0");
                    break;
                case 1:
                    txbxJogHareket2.Text = "1 mm";  // 2. Durum -> 3. Durum
                    nxCompoletStringWrite("M2FbJogHareket", "1");
                    break;
                case 2:
                    txbxJogHareket2.Text = "0.5 mm";    // 3. Durum -> 4. Durum
                    nxCompoletStringWrite("M2FbJogHareket", "2");
                    break;
                case 3:
                    txbxJogHareket2.Text = "0.1 mm";  // 4. Durum -> 1. Durum (geri başa döner)
                    nxCompoletStringWrite("M2FbJogHareket", "3");
                    break;
            }

            // Durumu artır ve 4. durumdan sonra tekrar sıfırla
            currentState2 = (currentState2 + 1) % 4;
        }

        private void txbxJogHiz2_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter tuşuna basıldığında işlemi gerçekleştirin
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun standart "ding" sesini engeller

                // Girilen metni bir sayıya dönüştür
                if (int.TryParse(txbxJogHiz2.Text, out int number))
                {
                    // Girilen sayının belirtilen aralıkta olup olmadığını kontrol edin
                    if (number >= 1 && number <= 100)
                    {
                        // Değer geçerli ise işlemi başlat
                        threadWriteString = new Thread(() => nxCompoletStringWrite("M2FbJogHiz", txbxJogHiz2.Text));
                        threadWriteString.Start();
                    }
                    else
                    {
                        // Uygun aralıkta değilse uyarı göster ve TextBox'ı temizle
                        txbxJogHiz2.Text = "20";
                        nxCompoletStringWrite("M2FbJogHiz", "20");
                        MessageBox.Show("Lütfen 1 ile 100 arasında bir sayı girin.");

                    }
                }
                else
                {
                    // Geçerli bir sayı değilse uyarı göster ve TextBox'ı temizle
                    txbxJogHiz2.Text = "20";
                    nxCompoletStringWrite("M2FbJogHiz", "20");
                    MessageBox.Show("Lütfen 1 ile 100 arasında bir sayı girin.");

                }
            }
        }

        private void btnXLeft2_MouseDown(object sender, MouseEventArgs e)
        {
            btnXLeft2.BackColor = Color.DarkGreen;
            station2Manuel2[0] = "M2FbJogButon[0]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[0], true));
            threadWriteBool.Start();
        }

        private void btnXLeft2_MouseUp(object sender, MouseEventArgs e)
        {
            btnXLeft2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[0] = "M2FbJogButon[0]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[0], false));
            threadWriteBool.Start();
        }

        private void btnXRight2_MouseDown(object sender, MouseEventArgs e)
        {
            btnXRight2.BackColor = Color.DarkGreen;
            station2Manuel2[1] = "M2FbJogButon[1]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[1], true));
            threadWriteBool.Start();
        }

        private void btnXRight2_MouseUp(object sender, MouseEventArgs e)
        {
            btnXRight2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[1] = "M2FbJogButon[1]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[1], false));
            threadWriteBool.Start();
        }

        private void btnYUp2_MouseDown(object sender, MouseEventArgs e)
        {
            btnYUp2.BackColor = Color.DarkGreen;
            station2Manuel2[2] = "M2FbJogButon[2]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[2], true));
            threadWriteBool.Start();
        }

        private void btnYUp2_MouseUp(object sender, MouseEventArgs e)
        {
            btnYUp2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[2] = "M2FbJogButon[2]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[2], false));
            threadWriteBool.Start();
        }

        private void btnYDown2_MouseDown(object sender, MouseEventArgs e)
        {
            btnYDown2.BackColor = Color.DarkGreen;
            station2Manuel2[3] = "M2FbJogButon[3]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[3], true));
            threadWriteBool.Start();
        }

        private void btnYDown2_MouseUp(object sender, MouseEventArgs e)
        {
            btnYDown2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[3] = "M2FbJogButon[3]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[3], false));
            threadWriteBool.Start();
        }

        private void btnZUp2_MouseDown(object sender, MouseEventArgs e)
        {
            btnZUp2.BackColor = Color.DarkGreen;
            station2Manuel2[4] = "M2FbJogButon[4]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[4], true));
            threadWriteBool.Start();
        }

        private void btnZUp2_MouseUp(object sender, MouseEventArgs e)
        {
            btnZUp2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[4] = "M2FbJogButon[4]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[4], false));
            threadWriteBool.Start();
        }

        private void btnZDown2_MouseDown(object sender, MouseEventArgs e)
        {
            btnZDown2.BackColor = Color.DarkGreen;
            station2Manuel2[5] = "M2FbJogButon[5]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[5], true));
            threadWriteBool.Start();
        }

        private void btnZDown2_MouseUp(object sender, MouseEventArgs e)
        {
            btnZDown2.BackColor = Color.FromArgb(31, 31, 31);
            station2Manuel2[5] = "M2FbJogButon[5]";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(station2Manuel2[5], false));
            threadWriteBool.Start();
        }

        private void txbxPos1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter tuşuna basıldığında
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan davranışını engelle
                TextBox textBox = sender as TextBox; // Olayı tetikleyen TextBox'u al

                // Geçerli bir sayı olup olmadığını kontrol et
                if (int.TryParse(textBox.Text.Trim(), out int number))
                {
                    ValidateAndActivateTextBoxes(number); // Sayıyı ValidateAndActivateTextBoxes metoduna gönder
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir rakam girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnMdlYkle1_Click(object sender, EventArgs e)
        {
            // 1. Model adı kontrolü
            string modelName = txBxMdlAdi1.Text.Trim();
            if (string.IsNullOrEmpty(modelName))
            {
                MessageBox.Show("Lütfen model adını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txbxPos1.Text) || txbxPos1.Text == "0")
            {
                MessageBox.Show("Lütfen Pozisyon Sayısını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Tüm TextBox'ların dolu olup olmadığını kontrol et
            if (!AreAllTextBoxesFilled())
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. "Modeller" klasörünün yolunu belirle
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-A Modeller");

            // Klasör yoksa oluştur
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            // 4. INI dosyasını "Modeller" klasörüne kaydetme yolu
            string iniFilePath = System.IO.Path.Combine(folderPath, modelName + ".ini");
            IniFile ini = new IniFile(iniFilePath);

            // 5. TextBox değerlerini INI dosyasına yazma işlemi
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = $"txBx1Pos{i}";
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    ini.Write("Positions", $"Pos{i}", textBox.Text);
                }
            }

            // 6. txBx1Pos1 değeri için INI dosyasına yazma (ekstra)
            TextBox txBxPos1 = Controls.Find("txBxPos1", true).FirstOrDefault() as TextBox;
            if (txBxPos1 != null)
            {
                ini.Write("Positions", "PosNumber", txBxPos1.Text);
            }

            // 7. Başarılı işlem mesajı

            verileriGonder();

            MessageBox.Show($"Veriler {modelName}.ini dosyasına 'Modeller' klasöründe kaydedildi ve PLC'ye gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void verileriGonder()
        {
            string modelName = txBxMdlAdi1.Text.Trim();
            // INI dosyasının yolunu belirleyin
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-A Modeller");
            string iniFilePath = System.IO.Path.Combine(folderPath, modelName + ".ini");
            IniFile ini = new IniFile(iniFilePath);

            // "Positions" bölümündeki değerleri okuyun ve PLC'ye gönderin

            string posNumberValue = ini.Read("Positions", "PosNumber");

            if (!string.IsNullOrEmpty(posNumberValue))
            {
                string posNumberVariable = "M2Degisken[0]"; // PLC'deki M0 değişkenine gönderiliyor
                nxCompoletStringWrite(posNumberVariable, posNumberValue);
            }

            for (int i = 1; i <= 30; i++)
            {
                string key = $"Pos{i}";
                string value = ini.Read("Positions", key);

                // Eğer değer boşsa atlama yapılabilir
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                value = value.Replace(',', '.');

                // PLC'ye veri gönderme işlemi
                string variableName = $"M2Degisken[{i}]"; // PLC değişken ismi (M1Degisken[0], M1Degisken[1] vb.)
                nxCompoletStringWrite(variableName, value); // nxCompoletStringWrite, PLC'ye veri yazmak için kullanılan fonksiyon
            }

            nxCompoletStringWrite("M2Degisken[31]", modelName);

        }

        private bool AreAllTextBoxesFilled()
        {
            for (int i = 1; i <= Convert.ToInt32(txbxPos1.Text) * 3; i++)
            {
                string textBoxName = $"txBx1Pos{i}";
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Eğer herhangi bir TextBox boşsa, false döneriz
                    return false;
                }
            }

            // Tüm TextBox'lar doluysa, true döneriz
            return true;
        }

        private bool AreAllTextBoxesFilled2()
        {
            for (int i = 1; i <= Convert.ToInt32(txbxPos2.Text) * 3; i++)
            {
                string textBoxName = $"txBx2Pos{i}";
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Eğer herhangi bir TextBox boşsa, false döneriz
                    return false;
                }
            }

            // Tüm TextBox'lar doluysa, true döneriz
            return true;
        }


        private void btnMdlSec1_Click(object sender, EventArgs e)
        {
            // txBx1Pos1'den txBx1Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = "txBx1Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.Clear(); // TextBox içeriğini sil
                    textBox.BackColor = Color.Silver;
                    textBox.Enabled = false;
                }
            }

            txBxMdlAdi1.Text = "";

            txbxPos1.Text = "";

            for (int k = 1; k <= 10; k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + k;

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    button.BackColor = Color.Silver;
                    button.Enabled = false;
                }
            }

            // "Modeller" klasörünün yolunu belirle
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-A Modeller");

            // Klasör yoksa kullanıcıya bilgi ver ve işlemi sonlandır
            if (!System.IO.Directory.Exists(folderPath))
            {
                MessageBox.Show("Modeller klasörü bulunamadı. Lütfen önce bir model kaydedin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // OpenFileDialog kullanarak dosya seçme işlemi
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = folderPath;
                openFileDialog.Filter = "INI Dosyaları (*.ini)|*.ini";
                openFileDialog.Title = "Bir model dosyası seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    IniFile ini = new IniFile(selectedFilePath); // INI dosyasını okumak için kullanılacak nesne

                    // Seçilen dosya adını model adı olarak al (uzantısı olmadan)
                    string modelName = System.IO.Path.GetFileNameWithoutExtension(selectedFilePath);
                    txBxMdlAdi1.Text = modelName; // txtModelName TextBox'ına yaz

                    // txBx1Pos1'den txBx1Pos30'a kadar değerleri yükle
                    for (int i = 1; i <= 30; i++)
                    {
                        string key = $"Pos{i}";
                        string value = ini.Read("Positions", key);

                        // İlgili TextBox'u bul ve değeri yaz
                        string textBoxName = "txBx1Pos" + i;
                        TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = value;
                        }
                    }

                    // txBxPos1 değeri için INI dosyasına yazma (ekstra) ve "PosNumber" değerini oku
                    string posNumber = ini.Read("Positions", "PosNumber");
                    // txBxPos1 TextBox'ına yazma
                    TextBox txBxPos1 = Controls.Find("txBxPos1", true).FirstOrDefault() as TextBox;
                    if (txBxPos1 != null)
                    {
                        txBxPos1.Text = posNumber; // "PosNumber" değerini txBxPos1'e yaz

                        ValidateAndActivateTextBoxes(15);
                    }


                    //MessageBox.Show($"Veriler {selectedFilePath} dosyasından yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

        }

        private void btnYeniMdl1_Click(object sender, EventArgs e)
        {
            btnPosKydet1.Enabled = false;

            // txBx1Pos1'den txBx1Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = "txBx1Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.Clear(); // TextBox içeriğini sil
                    textBox.BackColor = Color.Silver;
                    textBox.Enabled = false;
                }
            }

            txBxMdlAdi1.Text = "";

            txbxPos1.Text = "";

            for (int k = 1; k <= 10; k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + k;

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    button.BackColor = Color.Silver;
                    button.Enabled = false;
                }
            }

            // Kullanıcıya bilgilendirme mesajı (isteğe bağlı)
            //MessageBox.Show("Tüm alanlar temizlendi ve yeni model oluşturulabilir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txbxPos2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter tuşuna basıldığında
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan davranışını engelle
                TextBox textBox = sender as TextBox; // Olayı tetikleyen TextBox'u al

                // Geçerli bir sayı olup olmadığını kontrol et
                if (int.TryParse(textBox.Text.Trim(), out int number))
                {
                    ValidateAndActivateTextBoxes2(number); // Sayıyı ValidateAndActivateTextBoxes metoduna gönder
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir rakam girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        public string nxCompoletStringRead(string variable)  //NX STRING
        {
            try
            {
                string staticStringValue = Convert.ToString(nxCompolet1.ReadVariable(variable));
                return staticStringValue;
            }
            catch (Exception e)
            {
                return "error";
            }

        }

            public bool nxCompoletBoolRead(string variable)  //NX READ
            {
                try
                {
                    boolReadStatus = false;
                    bool staticValue = Convert.ToBoolean(nxCompolet1.ReadVariable(variable));
                    return staticValue;
                }
                catch
                {
                    boolReadStatus = true;
                    return false;
                }
            }

            public bool nxCompoletBoolWrite(string variable, bool value)  //NX WRITE
        {
            try
            {
                nxCompolet1.WriteVariable(variable, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string nxCompoletDoubleRead(string variable)  //NX STRING
        {
            try
            {
                string s = Convert.ToString(nxCompolet1.ReadVariable(variable));
                return s;
            }
            catch (Exception e)
            {
                // otherConsoleAppendLine("nxCompolet Hatası" + "\nKonum : DoubleRead" + "\nvariable = " + variable, Color.Red);
                return "-1";
            }
        }

        public bool nxCompoletStringWrite(string variable, string value)  //NX WRITE
        {
            try
            {
                nxCompolet1.WriteVariable(variable, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ValidateAndActivateTextBoxes(int number)
        {
            // Kullanıcının girdiği değeri al
            if (int.TryParse(txbxPos1.Text, out int input) && input >= 1 && input <= 10)
            {
                // Her 1 girdiğinde 3 TextBox aktif olacak şekilde hesaplama yap
                int textBoxCountToActivate = input * 3;

                // Tüm TextBox'ları devre dışı bırak
                foreach (TextBox tb in textBoxes)
                {
                    tb.Enabled = false;
                    tb.BackColor = Color.Silver;
                }

                // Girilen değere göre belirli sayıda TextBox'ı aktif et
                for (int i = 0; i < textBoxCountToActivate; i++)
                {
                    textBoxes[i].Enabled = true;
                    textBoxes[i].BackColor = Color.WhiteSmoke;
                }

                // Tüm butonları başlangıçta pasif yap
                foreach (Button btn in buttons)
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.Gray;
                }

                // Girilen sayı kadar butonu aktif et
                for (int i = 0; i < input; i++)
                {
                    buttons[i].Enabled = true;
                    buttons[i].BackColor = Color.WhiteSmoke;
                }
            }
            else
            {
                MessageBox.Show("Lütfen 1 ile 10 arasında bir sayı girin.");
            }
        }

        private void ValidateAndActivateTextBoxes2(int number)
        {
            // Kullanıcının girdiği değeri al
            if (int.TryParse(txbxPos2.Text, out int input) && input >= 1 && input <= 10)
            {
                // Her 1 girdiğinde 3 TextBox aktif olacak şekilde hesaplama yap
                int textBoxCountToActivate = input * 3;

                // Tüm TextBox'ları devre dışı bırak
                foreach (TextBox tb in textBoxes2)
                {
                    tb.Enabled = false;
                    tb.BackColor = Color.Silver;
                }

                // Girilen değere göre belirli sayıda TextBox'ı aktif et
                for (int i = 0; i < textBoxCountToActivate; i++)
                {
                    textBoxes2[i].Enabled = true;
                    textBoxes2[i].BackColor = Color.WhiteSmoke;
                }

                // Tüm butonları başlangıçta pasif yap
                foreach (Button btn in buttons2)
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.Gray;
                }

                // Girilen sayı kadar butonu aktif et
                for (int i = 0; i < input; i++)
                {
                    buttons2[i].Enabled = true;
                    buttons2[i].BackColor = Color.WhiteSmoke;
                }
            }
            else
            {
                MessageBox.Show("Lütfen 1 ile 10 arasında bir sayı girin.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bacgroundWorker.Baslat();

            string send2 = txbxJogHiz2.Text = "20";
            string send1 = txbxJogHiz1.Text = "20";

            nxCompoletBoolWrite("M2ConnectionStart", true);

            // İlk işlemi başlat
            nxCompoletStringWrite("M2FaJogHiz", send1);

            // İkinci işlemi başlat
            nxCompoletStringWrite("M2FbJogHiz", send2);

            nxCompoletStringWrite("M2FaJogHareket", "");

            nxCompoletStringWrite("M2FbJogHareket", "");
        }

        private void btnMdlYkle2_Click(object sender, EventArgs e)
        {
            // 1. Model adı kontrolü
            string modelName = txBxMdlAdi2.Text.Trim();
            if (string.IsNullOrEmpty(modelName))
            {
                MessageBox.Show("Lütfen model adını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txbxPos2.Text) || txbxPos2.Text == "0")
            {
                MessageBox.Show("Lütfen Pozisyon Sayısını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Tüm TextBox'ların dolu olup olmadığını kontrol et
            if (!AreAllTextBoxesFilled2())
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. "Modeller" klasörünün yolunu belirle
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-B Modeller");

            // Klasör yoksa oluştur
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            // 4. INI dosyasını "Modeller" klasörüne kaydetme yolu
            string iniFilePath = System.IO.Path.Combine(folderPath, modelName + ".ini");
            IniFile ini = new IniFile(iniFilePath);

            // 5. TextBox değerlerini INI dosyasına yazma işlemi
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = $"txBx2Pos{i}";
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    ini.Write("Positions", $"Pos{i}", textBox.Text);
                }
            }

            // 6. txBx1Pos1 değeri için INI dosyasına yazma (ekstra)
            TextBox txBxPos2 = Controls.Find("txBxPos2", true).FirstOrDefault() as TextBox;
            if (txBxPos2 != null)
            {
                ini.Write("Positions", "PosNumber", txBxPos2.Text);
            }

            verileriGonder2();

            MessageBox.Show($"Veriler {modelName}.ini dosyasına 'Modeller' klasöründe kaydedildi ve PLC'ye gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void verileriGonder2()
        {
            string modelName = txBxMdlAdi2.Text.Trim();
            // INI dosyasının yolunu belirleyin
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-B Modeller");
            string iniFilePath = System.IO.Path.Combine(folderPath, modelName + ".ini");
            IniFile ini = new IniFile(iniFilePath);

            // "Positions" bölümündeki değerleri okuyun ve PLC'ye gönderin

            string posNumberValue = ini.Read("Positions", "PosNumber");

            if (!string.IsNullOrEmpty(posNumberValue))
            {
                string posNumberVariable = "M2Degisken[50]"; // PLC'deki M0 değişkenine gönderiliyor
                nxCompoletStringWrite(posNumberVariable, posNumberValue);
            }

            for (int i = 1; i <= 30; i++)
            {
                string key = $"Pos{i}";
                string value = ini.Read("Positions", key);

                // Eğer değer boşsa atlama yapılabilir
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                value = value.Replace(',', '.');

                // PLC'ye veri gönderme işlemi
                string variableName = $"M2Degisken[{i+50}]"; // PLC değişken ismi (M1Degisken[0], M1Degisken[1] vb.)
                nxCompoletStringWrite(variableName, value); // nxCompoletStringWrite, PLC'ye veri yazmak için kullanılan fonksiyon
            }

            nxCompoletStringWrite("M2Degisken[81]", modelName);

            //MessageBox.Show("Değerler .", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnMdlSec2_Click(object sender, EventArgs e)
        {
            // txBx2Pos1'den txBx2Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = "txBx2Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.Clear(); // TextBox içeriğini sil
                    textBox.BackColor = Color.Silver;
                    textBox.Enabled = false;
                }
            }

            txBxMdlAdi2.Text = "";

            txbxPos2.Text = "";

            for (int k = 11; k <= 20; k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + k;

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    button.BackColor = Color.Silver;
                    button.Enabled = false;
                }
            }

            // "Modeller" klasörünün yolunu belirle
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Fikstür-B Modeller");

            // Klasör yoksa kullanıcıya bilgi ver ve işlemi sonlandır
            if (!System.IO.Directory.Exists(folderPath))
            {
                MessageBox.Show("Modeller2 klasörü bulunamadı. Lütfen önce bir model kaydedin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // OpenFileDialog kullanarak dosya seçme işlemi
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = folderPath;
                openFileDialog.Filter = "INI Dosyaları (*.ini)|*.ini";
                openFileDialog.Title = "Bir model dosyası seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    IniFile ini = new IniFile(selectedFilePath); // INI dosyasını okumak için kullanılacak nesne

                    // Seçilen dosya adını model adı olarak al (uzantısı olmadan)
                    string modelName = System.IO.Path.GetFileNameWithoutExtension(selectedFilePath);
                    txBxMdlAdi2.Text = modelName; // txtModelName TextBox'ına yaz

                    // txBx1Pos1'den txBx1Pos30'a kadar değerleri yükle
                    for (int i = 1; i <= 30; i++)
                    {
                        string key = $"Pos{i}";
                        string value = ini.Read("Positions", key);

                        // İlgili TextBox'u bul ve değeri yaz
                        string textBoxName = "txBx2Pos" + i;
                        TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = value;
                        }
                    }

                    // txBxPos1 değeri için INI dosyasına yazma (ekstra) ve "PosNumber" değerini oku
                    string posNumber = ini.Read("Positions", "PosNumber");
                    // txBxPos1 TextBox'ına yazma
                    TextBox txBxPos2 = Controls.Find("txBxPos2", true).FirstOrDefault() as TextBox;
                    if (txBxPos2 != null)
                    {
                        txBxPos2.Text = posNumber; // "PosNumber" değerini txBxPos1'e yaz

                        ValidateAndActivateTextBoxes2(15);
                    }


                    //MessageBox.Show($"Veriler {selectedFilePath} dosyasından yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

        }

        private void btnYeniMdl2_Click(object sender, EventArgs e)
        {
            btnPosKaydet2.Enabled = false;

            // txBx1Pos1'den txBx1Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= 30; i++)
            {
                string textBoxName = "txBx2Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.Clear(); // TextBox içeriğini sil
                    textBox.BackColor = Color.Silver;
                    textBox.Enabled = false;
                }
            }

            txBxMdlAdi2.Text = "";

            txbxPos2.Text = "";

            for (int k = 11; k <= 20; k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + k;

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    button.BackColor = Color.Silver;
                    button.Enabled = false;
                }
            }

            // Kullanıcıya bilgilendirme mesajı (isteğe bağlı)
            //MessageBox.Show("Tüm alanlar temizlendi ve yeni model oluşturulabilir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnPosKydet1_Click(object sender, EventArgs e)
        {
            // Formdaki tüm kontrolleri gezerek, ismi "txBx1Pos" ile başlayan TextBox'lara MouseDown olayını bağla
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox && textBox.Name.StartsWith("txBx1Pos"))
                {
                    textBox.MouseDown += TextBoxPos_MouseDown;
                }
            }

            int textBoxSifirla = 1; // TextBox sıfırlama değeri
            bool allTextBoxesFilled = true; // Tüm TextBox'ların dolu olup olmadığını kontrol etmek için değişken

            // myInteger ile başlayan ve 3 adet TextBox üzerinde işlem yapacak bir döngü başlat
            for (int j = myInteger; j <= myInteger + 2; j++)
            {
                // İlgili TextBox adını oluştur
                string textBoxActuelPosName = "txBxAct" + textBoxSifirla;
                TextBox textBoxActuelPosV = Controls.Find(textBoxActuelPosName, true).FirstOrDefault() as TextBox;

                if (textBoxActuelPosV != null)
                {
                    // TextBox'ın boş olup olmadığını kontrol et
                    if (string.IsNullOrEmpty(textBoxActuelPosV.Text))
                    {
                        allTextBoxesFilled = false;
                        MessageBox.Show("Lütfen tüm değerleri girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break; // Eğer bir TextBox boşsa döngüyü sonlandır
                    }
                }
            }

            // Eğer tüm TextBox'lar doluysa devam et
            if (allTextBoxesFilled)
            {
                textBoxSifirla = 1; // textBoxSifirla'yı sıfırla

                // Altı TextBox'ın değerlerini sırasıyla "txBx1Pos" adındaki TextBox'lara kopyalama işlemi
                for (int j = myInteger; j <= myInteger + 2; j++)
                {
                    // TextBox adlarını oluştur
                    string textBoxEOLName = "txBx1Pos" + j;
                    TextBox textBoxEOL = Controls.Find(textBoxEOLName, true).FirstOrDefault() as TextBox;

                    string textBoxActuelPosName = "txBxAct" + textBoxSifirla;
                    TextBox textBoxActuelPos = Controls.Find(textBoxActuelPosName, true).FirstOrDefault() as TextBox;

                    if (textBoxEOL != null && textBoxActuelPos != null)
                    {
                        // Değerleri kopyala
                        textBoxEOL.Text = textBoxActuelPos.Text;

                        // TextBox'a odaklan
                        textBoxEOL.Focus();

                        // Tüm metni seç
                        textBoxEOL.SelectAll();

                        // Enter tuşuna basma simülasyonu
                        SendKeys.Send("{ENTER}");

                        // textBoxSifirla değişkenini güncelle
                        textBoxSifirla++;
                    }
                }

                // Arka plan rengini ayarla
                for (int k = myInteger; k <= myInteger + 2; k++)
                {
                    string textBoxEOLName = "txBx1Pos" + k;
                    TextBox textBoxEOL = Controls.Find(textBoxEOLName, true).FirstOrDefault() as TextBox;

                    if (textBoxEOL != null)
                    {
                        textBoxEOL.BackColor = Color.Snow; // Renk ataması
                    }
                }

                // Girilen sayı kadar butonu aktif et
                for (int i = 0; i < Convert.ToInt32(txbxPos1.Text); i++)
                {
                    buttons[i].BackColor = Color.WhiteSmoke;
                }

                btnPosKydet1.Enabled = false;
            }
        }

        private void TextBoxPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CheckNumber((TextBox)sender);
            }
        }

        private void CheckNumber(TextBox textBox)
        {

        }

        private void btnPosGit1_Click(object sender, EventArgs e)
        {
            btnPosGit1.Enabled = false;

            for (int i = 1; i <= Convert.ToInt32(txbxPos1.Text); i++)
            {
                // Butonun ismini oluştur (örneğin: btnpos1, btnpos2, ..., btnpos10)
                string buttonName = "btnpos" + i;

                // İlgili butonu formdaki kontrollerden bul
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    // Arka plan rengini değiştir
                    button.BackColor = Color.WhiteSmoke; // İstediğiniz rengi buraya yazabilirsiniz
                }
            }

            // txBx1Pos1'den txBx1Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= Convert.ToInt32(txbxPos1.Text) * 3; i++)
            {
                string textBoxName = "txBx1Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.BackColor = Color.WhiteSmoke;
                    //textBox.Enabled = false;
                }
            }

            string xValue = "";
            string yValue = "";
            string zValue = "";


            string variable = "M2FaSecilenPos";

            string XVariable = "M2FaSecilenPosX";
            string YVariable = "M2FaSecilenPosY";
            string ZVariable = "M2FaSecilenPosZ";

            switch (myInteger)
            {
                case 1:

                    nxCompoletStringWrite(variable, "1");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos1.Text.Replace(',', '.');
                    yValue = txBx1Pos2.Text.Replace(',', '.');
                    zValue = txBx1Pos3.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 4:

                    nxCompoletStringWrite(variable, "2");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos4.Text.Replace(',', '.');
                    yValue = txBx1Pos5.Text.Replace(',', '.');
                    zValue = txBx1Pos6.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 7:

                    nxCompoletStringWrite(variable, "3");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos7.Text.Replace(',', '.');
                    yValue = txBx1Pos8.Text.Replace(',', '.');
                    zValue = txBx1Pos9.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 10:

                    nxCompoletStringWrite(variable, "4");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos10.Text.Replace(',', '.');
                    yValue = txBx1Pos11.Text.Replace(',', '.');
                    zValue = txBx1Pos12.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 13:

                    nxCompoletStringWrite(variable, "5");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos13.Text.Replace(',', '.');
                    yValue = txBx1Pos14.Text.Replace(',', '.');
                    zValue = txBx1Pos15.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);


                    break;
                case 16:

                    nxCompoletStringWrite(variable, "6");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos16.Text.Replace(',', '.');
                    yValue = txBx1Pos17.Text.Replace(',', '.');
                    zValue = txBx1Pos18.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 19:

                    nxCompoletStringWrite(variable, "7");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos19.Text.Replace(',', '.');
                    yValue = txBx1Pos20.Text.Replace(',', '.');
                    zValue = txBx1Pos21.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 22:

                    nxCompoletStringWrite(variable, "8");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos22.Text.Replace(',', '.');
                    yValue = txBx1Pos23.Text.Replace(',', '.');
                    zValue = txBx1Pos24.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 25:

                    nxCompoletStringWrite(variable, "9");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos25.Text.Replace(',', '.');
                    yValue = txBx1Pos26.Text.Replace(',', '.');
                    zValue = txBx1Pos27.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 28:

                    nxCompoletStringWrite(variable, "10");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx1Pos28.Text.Replace(',', '.');
                    yValue = txBx1Pos29.Text.Replace(',', '.');
                    zValue = txBx1Pos30.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
            }

            nxCompoletBoolWrite("M2FaPozisyonaGit", true);
            //MessageBox.Show("Değerler PLC'ye gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPosKaydet2_Click(object sender, EventArgs e)
        {
            // Formdaki tüm kontrolleri gezerek, ismi "txBx1Pos" ile başlayan TextBox'lara MouseDown olayını bağla
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox2 && textBox2.Name.StartsWith("txBx2Pos"))
                {
                     //textBox2.MouseDown += TextBoxPos2_MouseDown;
                }
            }

            int textBoxSifirla = 4; // TextBox sıfırlama değeri
            bool allTextBoxesFilled = true; // Tüm TextBox'ların dolu olup olmadığını kontrol etmek için değişken

            // myInteger ile başlayan ve 3 adet TextBox üzerinde işlem yapacak bir döngü başlat
            for (int j = myInteger; j <= myInteger + 2; j++)
            {
                // İlgili TextBox adını oluştur
                string textBoxActuelPosName = "txBxAct" + textBoxSifirla;
                TextBox textBoxActuelPosV = Controls.Find(textBoxActuelPosName, true).FirstOrDefault() as TextBox;

                if (textBoxActuelPosV != null)
                {
                    // TextBox'ın boş olup olmadığını kontrol et
                    if (string.IsNullOrEmpty(textBoxActuelPosV.Text))
                    {
                        allTextBoxesFilled = false;
                        MessageBox.Show("Lütfen tüm değerleri girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break; // Eğer bir TextBox boşsa döngüyü sonlandır
                    }
                }
            }

            // Eğer tüm TextBox'lar doluysa devam et
            if (allTextBoxesFilled)
            {
                textBoxSifirla = 4; // textBoxSifirla'yı sıfırla

                // Altı TextBox'ın değerlerini sırasıyla "txBx1Pos" adındaki TextBox'lara kopyalama işlemi
                for (int j = myInteger; j <= myInteger + 2; j++)
                {
                    // TextBox adlarını oluştur
                    string textBoxEOLName = "txBx2Pos" + j;
                    TextBox textBoxEOL = Controls.Find(textBoxEOLName, true).FirstOrDefault() as TextBox;

                    string textBoxActuelPosName = "txBxAct" + textBoxSifirla;
                    TextBox textBoxActuelPos = Controls.Find(textBoxActuelPosName, true).FirstOrDefault() as TextBox;

                    if (textBoxEOL != null && textBoxActuelPos != null)
                    {
                        // Değerleri kopyala
                        textBoxEOL.Text = textBoxActuelPos.Text;

                        // TextBox'a odaklan
                        textBoxEOL.Focus();

                        // Tüm metni seç
                        textBoxEOL.SelectAll();

                        // Enter tuşuna basma simülasyonu
                        SendKeys.Send("{ENTER}");

                        // textBoxSifirla değişkenini güncelle
                        textBoxSifirla++;
                    }
                }

                // Arka plan rengini ayarla
                for (int k = myInteger; k <= myInteger + 2; k++)
                {
                    string textBoxEOLName = "txBx2Pos" + k;
                    TextBox textBoxEOL = Controls.Find(textBoxEOLName, true).FirstOrDefault() as TextBox;

                    if (textBoxEOL != null)
                    {
                        textBoxEOL.BackColor = Color.Snow; // Renk ataması
                    }
                }

                // Girilen sayı kadar butonu aktif et
                for (int i = 0; i < Convert.ToInt32(txbxPos2.Text); i++)
                {
                    buttons2[i].BackColor = Color.WhiteSmoke;
                }

                btnPosKaydet2.Enabled = false;
            }

        }

        private void btnPosGit2_Click(object sender, EventArgs e)
        {
            btnPosGit2.Enabled = false;

            for (int i = 1; i <= Convert.ToInt32(txbxPos2.Text); i++)
            {
                // Butonun ismini oluştur (örneğin: btnpos1, btnpos2, ..., btnpos10)
                string buttonName = "btnpos" + (i+10);

                // İlgili butonu formdaki kontrollerden bul
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    // Arka plan rengini değiştir
                    button.BackColor = Color.WhiteSmoke; // İstediğiniz rengi buraya yazabilirsiniz
                }
            }

            // txBx1Pos1'den txBx1Pos30'a kadar olan TextBox'ların içeriğini temizle
            for (int i = 1; i <= Convert.ToInt32(txbxPos2.Text) * 3; i++)
            {
                string textBoxName = "txBx2Pos" + i;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.BackColor = Color.WhiteSmoke;
                    //textBox.Enabled = false;
                }
            }

            string xValue = "";
            string yValue = "";
            string zValue = "";

            string variable = "M2FbSecilenPos";

            string XVariable = "M2FbSecilenPosX";
            string YVariable = "M2FbSecilenPosY";
            string ZVariable = "M2FbSecilenPosZ";

            switch (myInteger)
            {
                case 1:

                    nxCompoletStringWrite(variable, "1");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos1.Text.Replace(',', '.');
                    yValue = txBx2Pos2.Text.Replace(',', '.');
                    zValue = txBx2Pos3.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 4:

                    nxCompoletStringWrite(variable, "2");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos4.Text.Replace(',', '.');
                    yValue = txBx2Pos5.Text.Replace(',', '.');
                    zValue = txBx2Pos6.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 7:

                    nxCompoletStringWrite(variable, "3");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos7.Text.Replace(',', '.');
                    yValue = txBx2Pos8.Text.Replace(',', '.');
                    zValue = txBx2Pos9.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 10:

                    nxCompoletStringWrite(variable, "4");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos10.Text.Replace(',', '.');
                    yValue = txBx2Pos11.Text.Replace(',', '.');
                    zValue = txBx2Pos12.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 13:

                    nxCompoletStringWrite(variable, "5");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos13.Text.Replace(',', '.');
                    yValue = txBx2Pos14.Text.Replace(',', '.');
                    zValue = txBx2Pos15.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 16:

                    nxCompoletStringWrite(variable, "6");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos16.Text.Replace(',', '.');
                    yValue = txBx2Pos17.Text.Replace(',', '.');
                    zValue = txBx2Pos18.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 19:

                    nxCompoletStringWrite(variable, "7");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos19.Text.Replace(',', '.');
                    yValue = txBx2Pos20.Text.Replace(',', '.');
                    zValue = txBx2Pos21.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 22:

                    nxCompoletStringWrite(variable, "8");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos22.Text.Replace(',', '.');
                    yValue = txBx2Pos23.Text.Replace(',', '.');
                    zValue = txBx2Pos24.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 25:

                    nxCompoletStringWrite(variable, "9");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos25.Text.Replace(',', '.');
                    yValue = txBx2Pos26.Text.Replace(',', '.');
                    zValue = txBx2Pos27.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;
                case 28:

                    nxCompoletStringWrite(variable, "10");

                    // TextBox'taki değerlerde ',' varsa '.' ile değiştir
                    xValue = txBx2Pos28.Text.Replace(',', '.');
                    yValue = txBx2Pos29.Text.Replace(',', '.');
                    zValue = txBx2Pos30.Text.Replace(',', '.');

                    nxCompoletStringWrite(XVariable, xValue);
                    nxCompoletStringWrite(YVariable, yValue);
                    nxCompoletStringWrite(ZVariable, zValue);

                    break;

            }

            nxCompoletBoolWrite("M2FbPozisyonaGit", true);
            //MessageBox.Show("Değerler PLC'ye gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnPos11_Click(object sender, EventArgs e)
        {
            btnPos11.BackColor = Color.PaleTurquoise;

            int i = 1;
            int j = 11;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos12_Click(object sender, EventArgs e)
        {
            btnPos12.BackColor = Color.PaleTurquoise;

            int i = 4;
            int j = 12;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos13_Click(object sender, EventArgs e)
        {
            btnPos13.BackColor = Color.PaleTurquoise;

            int i = 7;
            int j = 13;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos14_Click(object sender, EventArgs e)
        {
            btnPos14.BackColor = Color.PaleTurquoise;

            int i = 10;
            int j = 14;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos15_Click(object sender, EventArgs e)
        {
            btnPos15.BackColor = Color.PaleTurquoise;

            int i = 13;
            int j = 15;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos16_Click(object sender, EventArgs e)
        {
            btnPos16.BackColor = Color.PaleTurquoise;

            int i = 16;
            int j = 16;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos17_Click(object sender, EventArgs e)
        {
            btnPos17.BackColor = Color.PaleTurquoise;

            int i = 19;
            int j = 17;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos18_Click(object sender, EventArgs e)
        {
            btnPos18.BackColor = Color.PaleTurquoise;

            int i = 22;
            int j = 18;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos19_Click(object sender, EventArgs e)
        {
            btnPos19.BackColor = Color.PaleTurquoise;

            int i = 25;
            int j = 19;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos20_Click(object sender, EventArgs e)
        {
            btnPos20.BackColor = Color.PaleTurquoise;

            int i = 28;
            int j = 20;
            BackgroungVibrTurquaise2(i, j);

            myInteger = i;
        }

        private void btnPos1_Click(object sender, EventArgs e)
        {
            btnPos1.BackColor = Color.PaleTurquoise;

            int i = 1;
            int j = 1;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos2_Click(object sender, EventArgs e)
        {
            btnPos2.BackColor = Color.PaleTurquoise;

            int i = 4;
            int j = 2;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos3_Click(object sender, EventArgs e)
        {
            btnPos3.BackColor = Color.PaleTurquoise;

            int i = 7;
            int j = 3;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos4_Click(object sender, EventArgs e)
        {
            btnPos4.BackColor = Color.PaleTurquoise;

            int i = 10;
            int j = 4;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos5_Click(object sender, EventArgs e)
        {
            btnPos5.BackColor = Color.PaleTurquoise;

            int i = 13;
            int j = 5;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos6_Click(object sender, EventArgs e)
        {
            btnPos6.BackColor = Color.PaleTurquoise;

            int i = 16;
            int j = 6;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos7_Click(object sender, EventArgs e)
        {
            btnPos7.BackColor = Color.PaleTurquoise;

            int i = 19;
            int j = 7;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos8_Click(object sender, EventArgs e)
        {
            btnPos8.BackColor = Color.PaleTurquoise;

            int i = 22;
            int j = 8;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos9_Click(object sender, EventArgs e)
        {
            btnPos9.BackColor = Color.PaleTurquoise;

            int i = 25;
            int j = 9;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void btnPos10_Click(object sender, EventArgs e)
        {
            btnPos10.BackColor = Color.PaleTurquoise;

            int i = 28;
            int j = 10;
            BackgroungVibrTurquaise(i, j);

            myInteger = i;
        }

        private void BackgroungVibrTurquaise(int i, int j)
        {
            // 3 adet textbox'ın arka plan rengini değiştir
            for (int k = 1; k <= Convert.ToInt32(txbxPos1.Text) * 3; k++)
            {
                // Textbox kontrolünün ismini oluştur
                string textBoxName = "txBx1Pos" + k;

                // Form içinde bu isimde bir TextBox kontrolü ara
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                // TextBox bulunduysa, arka plan rengini değiştir
                if (textBox != null)
                {
                    if ((textBox.Name == "txBx1Pos" + i) || (textBox.Name == "txBx1Pos" + (i + 1)) || (textBox.Name == "txBx1Pos" + (i + 2)))
                    {
                        textBox.BackColor = Color.PaleTurquoise;
                    }
                    else
                        textBox.BackColor = Color.WhiteSmoke;

                }
            }

            // 10 adet buton arasında dolaşarak arka plan rengini değiştir // x = 0 - 1690, y = 0 - 300, z = 0- 260
            for (int k = 1; k <= Convert.ToInt32(txbxPos1.Text); k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + k;

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    if (button.Name == ("btnPos" + j))
                        continue;
                    else
                        button.BackColor = Color.WhiteSmoke;
                }
            }

            btnPosKydet1.Enabled = true;

            if (AreTextBoxesFilled(i))
            {
                btnPosGit1.Enabled = true;
            }
            else
            {
                btnPosGit1.Enabled = false;
            }

        }

        bool AreTextBoxesFilled(int i)
        {
            // Belirtilen adlara sahip TextBox'ları sırayla bul ve kontrol et
            for (int offset = 0; offset <= 2; offset++)
            {
                string textBoxName = "txBx1Pos" + (i + offset); // İlgili TextBox adı
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Eğer TextBox bulunamazsa veya boşsa, false döndür
                    return false;
                }
            }

            // Eğer tüm TextBox'lar doluysa, true döndür
            return true;
        }

        private void BackgroungVibrTurquaise2(int i, int j)
        {
            // 3 adet textbox'ın arka plan rengini değiştir
            for (int k = 1; k <= Convert.ToInt32(txbxPos2.Text) * 3; k++)
            {
                // Textbox kontrolünün ismini oluştur
                string textBoxName = "txBx2Pos" + k;

                // Form içinde bu isimde bir TextBox kontrolü ara
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                // TextBox bulunduysa, arka plan rengini değiştir
                if (textBox != null)
                {
                    if ((textBox.Name == "txBx2Pos" + i) || (textBox.Name == "txBx2Pos" + (i + 1)) || (textBox.Name == "txBx2Pos" + (i + 2)))
                    {
                        textBox.BackColor = Color.PaleTurquoise;
                    }
                    else
                        textBox.BackColor = Color.WhiteSmoke;

                }
            }

            // 10 adet buton arasında dolaşarak arka plan rengini değiştir // x = 0 - 1690, y = 0 - 300, z = 0- 260
            for (int k = 1; k <= Convert.ToInt32(txbxPos2.Text); k++)
            {
                // Buton kontrolünün adını oluştur
                string buttonName = "btnPos" + (k+10);

                // Form içinde bu isimde bir Button kontrolü ara
                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                // Buton bulunduysa, arka plan rengini değiştir
                if (button != null)
                {
                    if (button.Name == ("btnPos" + j))
                        continue;
                    else
                        button.BackColor = Color.WhiteSmoke;
                }
            }

            btnPosKaydet2.Enabled = true;

            if (AreTextBoxesFilled2(i))
            {
                btnPosGit2.Enabled = true;
            }
            else
            {
                btnPosGit2.Enabled = false;
            }

        }

        bool AreTextBoxesFilled2(int i)
        {
            // Belirtilen adlara sahip TextBox'ları sırayla bul ve kontrol et
            for (int offset = 0; offset <= 2; offset++)
            {
                string textBoxName = "txBx2Pos" + (i + offset); // İlgili TextBox adı
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Eğer TextBox bulunamazsa veya boşsa, false döndür
                    return false;
                }
            }

            // Eğer tüm TextBox'lar doluysa, true döndür
            return true;
        }



        private void TextBox_Leave(object sender, EventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Enter tuşuna basıldığında işlem yapılacak
                if (e.KeyCode == Keys.Enter)
                {
                    // TextBox'ın adına göre Min ve Max değerleri kontrol et
                    if (valueRanges.TryGetValue(textBox.Name, out var range))
                    {
                        CheckMinMax(textBox, range.Min, range.Max);
                    }
                }
            }
        }

        private void CheckMinMax(TextBox textBox, double minValue, double maxValue)
        {
            // Kullanıcının girdiği değeri kontrol et
            if (double.TryParse(textBox.Text, out double value))
            {
                // Değer geçerli aralıkta mı?
                if (value < minValue || value > maxValue)
                {
                    // Uyarı mesajı
                    MessageBox.Show($"{textBox.Name} için geçerli değer {minValue:F2} ile {maxValue:F2} arasında olmalıdır.",
                                    "Geçersiz Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Son geçerli değeri geri yükle veya boş bırak
                    RestoreLastValidValue(textBox);
                    textBox.Focus();
                }
                else
                {
                    // Geçerli bir değer olduğunda listeye ekle
                    UpdateLastValidValue(textBox);
                }
            }
            else
            {
                // Geçersiz giriş durumunda
                MessageBox.Show($"{textBox.Name} geçerli bir sayı olmalıdır.",
                                 "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Son geçerli değeri geri yükle veya boş bırak
                RestoreLastValidValue(textBox);
                textBox.Focus();
            }
        }

        private void tableLayoutPanel73_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome1_Click(object sender, EventArgs e)
        {
            nxCompoletBoolWrite("M2HomeButon", true);
        }

        private void btnM1Sifirla_MouseDown(object sender, MouseEventArgs e)
        {
            btnM1Sifirla.BackColor = Color.DarkGreen;
            btnM1Sifirla.ForeColor = Color.WhiteSmoke;
            string M1Sifirla = "M2Sifirla";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(M1Sifirla, true));
            threadWriteBool.Start();
        }

        private void btnM1Sifirla_MouseUp(object sender, MouseEventArgs e)
        {
            btnM1Sifirla.BackColor = Color.DarkOrange;
            btnM1Sifirla.ForeColor = Color.Black;
            string M1Sifirla = "M2Sifirla";

            threadWriteBool = new Thread(() => nxCompoletBoolWrite(M1Sifirla, false));
            threadWriteBool.Start();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            bacgroundWorker.Durdur();
            nxCompoletBoolWrite("M2ConnectionStart", false);
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Çalışanları ve bağlı kaynakları durdurun
            if (bacgroundWorker != null)
            {
                bacgroundWorker.Durdur();
                while (bacgroundWorker.IsBusy) // Çalışma bitene kadar bekle
                {
                    Application.DoEvents(); // UI'nin donmaması için
                }
            }

            // nxCompolet bağlantısını kapatın
            try
            {
                nxCompoletBoolWrite("M2ConnectionStart", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı kapatma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Uygulamayı güvenli şekilde kapat
            Application.Exit();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;

            }
            else
            {

                WindowState = FormWindowState.Normal;
                this.Height = 800;
                this.Width = 1400;
                StartPosition = FormStartPosition.CenterScreen;

            }
        }

        private void RestoreLastValidValue(TextBox textBox)
        {
            if (lastValidValues.ContainsKey(textBox.Name))
            {
                textBox.Text = lastValidValues[textBox.Name];
            }
            else
            {
                textBox.Text = string.Empty; // Eğer sözlükte bir değer yoksa, metni boş bırak
            }
        }

        private void UpdateLastValidValue(TextBox textBox)
        {
            if (lastValidValues.ContainsKey(textBox.Name))
            {
                lastValidValues[textBox.Name] = textBox.Text;
            }
            else
            {
                lastValidValues.Add(textBox.Name, textBox.Text);
            }
        }


        // KeyPress olayı ile sadece rakam girişine izin verme
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Eğer tuş rakam değilse, virgül değilse ve kontrol tuşu değilse (örneğin Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // İşlenmiş olarak işaretleyerek girişini engelle
            }

            // İlk karakterin virgül olmasını engelle
            if (e.KeyChar == ',' && string.IsNullOrEmpty(textBox?.Text))
            {
                e.Handled = true; // İlk karakter virgül olamaz
            }

            // Birden fazla virgül girişini engelle
            if (e.KeyChar == ',' && textBox?.Text.Contains(",") == true)
            {
                e.Handled = true; // Virgül zaten varsa tekrar eklenemez
            }
        }



    }
}
