using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;

namespace HandleRequestsWindowsService
{
    public partial class Service1 : ServiceBase
    {
        static HttpClient client = new HttpClient();
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            string path = @"F:\Study\HandleRequestsWindowsService\HandleRequestsWindowsService\Requests.csv";
            DataTable csvTable=ReadFromCsvFile(path);
            InsertRequest(csvTable);
        }

        public void WriteToFile(string Message,string FileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + FileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\"+ FileName+"\\"+ FileName+"" + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        public DataTable ReadFromCsvFile(string path)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
            }
            return csvTable;
        }

        public void InsertRequest(DataTable csvTable)
        {
            string Url = "http://localhost:64589/api/Requests/AddRequest";
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                var Request = new { MobileNumber = int.Parse(csvTable.Rows[i]["MobileNumber"].ToString()) };
                var json = JsonConvert.SerializeObject(Request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var reponse = client.PostAsync(Url, data);
                response result = JsonConvert.DeserializeObject<response>(reponse.Result.Content.ReadAsStringAsync().Result);
                if (result.Status == 2)
                    WriteToFile(Request.MobileNumber.ToString(), "Dublicate");
                else if (result.Status == 3)
                    WriteToFile(Request.MobileNumber.ToString(), "Failed");
            }
            
            WriteToFile("Finished @" + DateTime.Now + "", "Done");
            //Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Done");
        }
        public void MoveFile(string src,string des)
        {
            File.Move(src, des);
        }
    }
}
