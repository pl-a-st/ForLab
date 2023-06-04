using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ForLab
{
    public static class PmData
    {
        static PmData()
        {
            SetAppDataFolderName();
            GetListHeadNames();
            GetListHeadPositions();
            GetListDateSimbols();

        }

        private static void SetAppDataFolderName()
        {
            PathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ForLab";
            if (Directory.Exists(PathAppData))
            {
                Directory.CreateDirectory(PathAppData);
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
        private static void GetListHeadPositions()
        {
            ListPositionHead = DAO.binReadFileToObject(ListPositionHead, PathListHeadPositions, out var result);
            if (result != MethodResultStatus.Sucsess)
            {
                ListPositionHead.Add(PositionHeadMetrologist);
                ListPositionHead.Add(PositionHeadDepart);
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
        public const string RegNumber = "RA.RU.312196";
        public const string NameHeadMetrologist = "Головизин Р.П.";
        public const string NameHeadDepart = "Верин С.Г";
        public const string PositionHeadMetrologist = "Главный метролог";
        public const string PositionHeadDepart = "Дов. №14/23 от 09.01.2023, начальник технического отдела";
        public const string Code = "ГАТ";

        public const string TargetStrForRegNumber = "аккредитованных лиц";
        public const string TargetStrForPosHead = "должность руководителя или";
        public const string TargetStrForNameHead = "фамилия, инициалы";
        public const string TargetStrForSymble = "Знак поверки:";
        private static string PathAppData;
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
        static public void SetPositionHead(string positionHead)
        {
            PositionHead = positionHead;
        }
        static public string NameHead
        {
            get;
            private set;
        }
        static public void SetNameHead(string nameHead)
        {
            PositionHead = nameHead;
        }
    }
}
