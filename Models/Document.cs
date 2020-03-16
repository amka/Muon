using System;
using System.IO;

namespace Muon.Models
{
    public class Document
    {
        public Uri FilePath { get; set; }
        public string Name => Path.GetFileName(FilePath.LocalPath);
    }
}