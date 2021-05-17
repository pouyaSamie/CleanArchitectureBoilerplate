using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.File
{
    public class FileManager : IFileManager
    {
        public IFileResult _fileResult { get; set; }
        private string _saveFilePath;
        private readonly IStringLocalizer<Result> _localizer;
        private HashSet<string> _acceptableFileExtention;
        private HashSet<string> _acceptableFileContentType;
        public FileManager(IFileResult fileResult, IConfiguration configuration, IStringLocalizer<Result> localizer)
        {
            _fileResult = fileResult;
            _localizer = localizer;
           
            _saveFilePath = configuration.GetValue<string>("FilePath");
            _acceptableFileExtention = new HashSet<string> (configuration.GetValue<string>("acceptableFileExtensions").Split(","));
            _acceptableFileContentType = new HashSet<string> (configuration.GetValue<string>("acceptableFileContentType").Split(","));
        }
        public async System.Threading.Tasks.Task<IFileResult> SaveAsync(IFormFile file)
        {
            try
            {
                var fileExtenstion = System.IO.Path.GetExtension(file.FileName).ToLower();
                
                if (!_acceptableFileExtention.Contains(fileExtenstion) || !_acceptableFileContentType.Contains(file.ContentType))
                {
                    // Check whether selected file is valid or not
                    _fileResult.IsSuccessful = false;
                    _fileResult.ErrorMessage = _localizer[LocalizationKeys.InvalidFileExtension];
                    return _fileResult;
                }

                var fileName = Guid.NewGuid().ToString();
                var filePath = System.IO.Path.Combine(_saveFilePath, fileName);
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                }

                _fileResult.FileExtention = fileExtenstion;
                _fileResult.FileName = fileName;
                _fileResult.ContentType = file.ContentType;
                _fileResult.FileLenght = file.Length;
                _fileResult.IsSuccessful = true;
                return _fileResult;
            }
            catch (Exception ex)
            {

                _fileResult.IsSuccessful = false;
                return _fileResult;
            }
          
        }
    }
}
