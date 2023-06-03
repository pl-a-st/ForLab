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
            GetListHeades();
        }

        private static void GetListHeades()
        {
            ListHeadNames = DAO.binReadFileToObject(ListHeadNames, PathListHeadNames, out var result);
            if (result != MethodResultStatus.Sucsess)
            {
                ListHeadNames.Add("Головизин Р.П.");
                ListHeadNames.Add("Верин С.Г.");
            }
        }

        public const string RegNumber = "RA.RU.312196";
        public const string PositionHeadMetrologist = "Главный метролог";
        public const string NameHeadMetrologist = "Головизин Р.П.";
        public const string PositionHeadDepart = "Доверенность №14/23 от 09.01.2023, начальник технического отдела";
        public const string TargetStrForRegNumber = "аккредитованных лиц";
        public const string TargetStrForPosHead = "должность руководителя или";
        public const string TargetStrForNameHead = "фамилия, инициалы";
        static public string PathListHeadNames = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"Head.bin";
        public const int CountOfTries = 3;
        static public List<string> ListPositionHead = new List<string>();
        static public List<string> ListHeadNames = new List<string>();
        static public List<FileInfo> ListMetrologySimbols = new List<FileInfo>();
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
