/*
 * FILE				: Record.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: Manages the Records
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace A07.Classes
{
   class Record
   {
      private static ObservableCollection<Record> Records = new ObservableCollection<Record>();

      public string Name
      { get; set; }
      public string Path
      { get; set; }
      public double Time
      { get; set; }


      public Record(string name, string path, double time)
      {
         Name = name;
         Path = path;
         Time = time;
      }

      public Record()
      {

      }

      public void SetRecords(ObservableCollection<Record> recs)
      {
         Records = recs;
      }


      public void AddRecord(Record newR)
      {
         Records.Add(newR);
      }

      public ObservableCollection<Record> GetRecords()
      {
         return Records;
      }
   }
}
