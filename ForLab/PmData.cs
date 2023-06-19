using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Word = Microsoft.Office.Interop.Word;

namespace ForLab
{
    public static class PmData
    {
        public const string RegNumber = "RA.RU.312196";
        //public const string RegNumber = "РОСС RU.0001.310044";
        public const string NameHeadMetrologist = "Головизин Р.П.";
        //public const string NameHeadMetrologist = "Пур А.С.";
        public const string NameHeadDepart = "Верин С.Г";
        public const string PositionHeadMetrologist = "Главный метролог";
        public const string PositionHeadDepart = "Дов. №14/23 от 09.01.2023, начальник технического отдела";
        //public const string Code = "ГДН";
        public const string Code = "ГАТ";
        
        public const string TargetStrForRegNumber = "аккредитованных лиц";
        public const string TargetStrForPosHead = "должность руководителя или";
        public const string TargetStrForNameHead = "фамилия, инициалы";
        public const string TargetStrForSymble = "Знак поверки:";

        static public Word.Document CertificateLogFile;
        static public Word.Application WordApplication;
        static private string PathAppData;
        static public  FileInfo FileInfoCertificateLog;
        static public string PathListHeadNames
        {
            get 
            {
                return PathAppData + @"\Names.bin";
            }
        } 
        static public string PathListHeadPositions
        {
            get
            {
                return PathAppData + @"\Position.bin";
            }
        }
        static public string PathFileInfoCertificateLog
        {
            get
            {
                return PathAppData + @"\FileInfoCertificateLog.bin";
            }
        }
        static public string PathListDateSymbol
        {
            get
            {
                return PathAppData + @"\DateSymbol.bin";
            }
        }
        public const int CountOfTries = 3;
        static public List<string> ListPositionHead = new List<string>();
        static public List<string> ListHeadNames = new List<string>();
        static public List<string> ListDateSimbols = new List<string>();
        static public string PositionHead
        {
            get;
            private set;
        } = PositionHeadMetrologist;
        static public string NameHead
        {
            get;
            private set;
        }
        static PmData()
        {
            SetAppDataFolderName();
            GetListHeadNames();
            GetListHeadPositions();
            GetListDateSimbols();
            GetFileInfoCertificateLog();
        }
        private static void GetListHeadPositions()
        {
            ListPositionHead = DAO.binReadFileToObject(ListPositionHead, PathListHeadPositions, out var result);
            if (result != MethodResultStatus.Sucsess)
            {
                ListPositionHead.Add(PositionHeadMetrologist);
                ListPositionHead.Add(PositionHeadDepart);
            }
        }
        private static void GetFileInfoCertificateLog()
        {
            FileInfoCertificateLog = DAO.binReadFileToObject(FileInfoCertificateLog, PathFileInfoCertificateLog, out var result);
            if(result!= MethodResultStatus.Sucsess)
            {
                if (File.Exists(@"W:\8.Технический отдел\Общая\Калибровка\Журналы\Журнал учета электронных свидетельств о поверке\Журнал учета выдачи электронных свидетельств.docm"))
                {
                    FileInfoCertificateLog = new FileInfo(@"W:\8.Технический отдел\Общая\Калибровка\Журналы\Журнал учета электронных свидетельств о поверке\Журнал учета выдачи электронных свидетельств.docm");
                }
            }
        }
        private static void GetListHeadNames()
        {
            ListHeadNames = DAO.binReadFileToObject(ListHeadNames, PathListHeadNames, out var result);
            if (result != MethodResultStatus.Sucsess)
            {
                ListHeadNames.Add(NameHeadMetrologist);
                ListHeadNames.Add(NameHeadDepart);
            }
        }
        private static void SetAppDataFolderName()
        {
            PathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ForLab";
            if (Directory.Exists(PathAppData))
            {
                Directory.CreateDirectory(PathAppData);
            }
        }
        private static void GetListDateSimbols()
        {
            ListDateSimbols = DAO.binReadFileToObject(ListDateSimbols, PathListDateSymbol, out var result);
            if (result != MethodResultStatus.Sucsess)
            {
                ListDateSimbols.Add("2023");
                ListDateSimbols.Add("2022");
                ListDateSimbols.Add("2021");
            }
        }
        static public void SetPositionHead(string positionHead)
        {
            PositionHead = positionHead;
        }
        static public void SetNameHead(string nameHead)
        {
            PositionHead = nameHead;
        }
    }
}
