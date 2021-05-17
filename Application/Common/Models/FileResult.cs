using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class FileResult : IFileResult
    {
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string ContentType { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public long FileLenght { get; set; }
        public string ErrorMessage { get; set; }
    }
}
