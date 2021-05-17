using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IFileManager
    {
        public  System.Threading.Tasks.Task<IFileResult> SaveAsync(IFormFile file);
    }
}
