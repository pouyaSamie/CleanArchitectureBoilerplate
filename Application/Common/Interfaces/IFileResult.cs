using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IFileResult
    {
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string ContentType { get; set; }
        public bool IsSuccessful { get; set; }
        public long FileLenght { get; set; }
        public string ErrorMessage { get; set; }
    }
}
