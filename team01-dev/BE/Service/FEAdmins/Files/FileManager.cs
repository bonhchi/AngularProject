using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Files;
using Infrastructure.Extensions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service.Files
{
    public class FileManager : IFileManager
    {
        private readonly IMapper _mapper;
        private readonly FileConfig _fileConfig;
        private readonly Cloudinary _cloudinary;

        public FileManager(IMapper mapper, FileConfig fileConfig)
        {
            _mapper = mapper;
            _fileConfig = fileConfig;
            var account = new Account(_fileConfig.CloudName, _fileConfig.ApiKey, _fileConfig.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<IActionResult> DownloadFile(string url)
        {
            var ext = Path.GetExtension(url);
            if (DataType.TypeAccept[DataType.ETypeFile.Image].Contains(ext))
            {
                var urlReturn = "https://localhost:44309/files/" + url;
                return new OkObjectResult(urlReturn);
            }

            var memory = new MemoryStream();
            if (url.IsNullOrEmpty())
            {
                return new FileStreamResult(memory, "application/octet-stream");
            }
            var filePath = Path.Combine(UrlConstants.BaseLocalUrlFile, Path.GetFileName(url));

            if (!System.IO.File.Exists(filePath))
            {
                return new FileStreamResult(memory, "application/octet-stream");
            }

            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return new FileStreamResult(memory, GetContentType(filePath));
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        public async Task<List<CreateFileDTO>> SaveFile(SaveFileDTO saveFile)
        {
            if (!DataType.TypeName.ContainsKey(saveFile.EntityType))
            {
                return new List<CreateFileDTO>();
            }
            saveFile.EntityType = DataType.TypeName[saveFile.EntityType];
            
            var filePaths = UrlConstants.BaseLocalUrlFile;
            //var urlPath = UrlConstants.BaseCloudUrlFile;
            List<CreateFileDTO> createFileDTOs = new List<CreateFileDTO>();
            if (!Directory.Exists(filePaths))
            {
                //Directory.Delete(filePath, true);
                Directory.CreateDirectory(filePaths);
            }
            if (saveFile.Files != null && saveFile.Files.Count > 0)
            {
                try
                {
                    foreach (var formFile in saveFile.Files)
                    {
                        var ext = Path.GetExtension(formFile.FileName).ToLower();
                        if (!DataType.CheckTypeAccept(saveFile.EntityType, ext))
                        {
                            continue;
                        }
                        var fileName = Guid.NewGuid().ToString() + ext;
                        var filePath = Path.Combine(filePaths, fileName);

                        var item = _mapper.Map<SaveFileDTO, CreateFileDTO>(saveFile);
                        if (formFile.Length <= 0)
                        {
                            continue;
                        }

                        if (DataType.TypeAccept[DataType.ETypeFile.Image].Contains(ext) && saveFile.TypeUpload == 1)
                        {
                            var uploadParams = new ImageUploadParams();

                            using (var memory = new MemoryStream())
                            {
                                using var fileStream = formFile.OpenReadStream();
                                byte[] bytes = new byte[formFile.Length];
                                fileStream.Read(bytes, 0, (int)formFile.Length);
                                fileStream.Position = 0;
                                uploadParams.File = new FileDescription(fileName, fileStream);
                                var result = _cloudinary.Upload(uploadParams);

                                item.Url = result.SecureUrl.ToString();
                                item.Name = formFile.FileName;
                                item.FileExt = ext;
                                item.TypeUpload = 1;
                            }
                            createFileDTOs.Add(item);
                        }

                        if (!DataType.TypeAccept[DataType.ETypeFile.Image].Contains(ext))
                        {
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                //stream.Write();
                                formFile.CopyTo(stream);

                                //item.Url = Path.Combine(urlPath, fileName);
                                item.Url = fileName;
                                item.Name = formFile.FileName;
                                item.FileExt = ext;
                                item.TypeUpload = 0;
                            }
                            createFileDTOs.Add(item);
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


            return createFileDTOs;
        }
    }
}
