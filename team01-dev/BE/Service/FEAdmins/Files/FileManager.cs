﻿using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Constants;
using Domain.DTOs.Files;
using Infrastructure.Extensions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (!File.Exists(filePath))
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

            if (!provider.TryGetContentType(path, out string contentType))
            {
                contentType = "application/octet-stream"; //need constraint
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
                        var fileName = formFile.FileName.ToString();
                        var filePath = Path.Combine(filePaths, fileName);

                        var item = _mapper.Map<SaveFileDTO, CreateFileDTO>(saveFile);
                        if (formFile.Length <= 0)
                        {
                            continue;
                        }

                        //file name : base file name + guid + extension
                        if (DataType.TypeAccept[DataType.ETypeFile.Image].Contains(ext) && saveFile.TypeUpload == 1)
                        {
                            var uploadParams = new ImageUploadParams();

                            // convert to new file stream avoid different file name
                            //using (var fileStream = new FileStream(filePath, FileMode.Create))
                            //{
                            //    formFile.CopyTo(fileStream);
                            //    ImageUploadParams imageUploadParams = new ImageUploadParams()
                            //    {
                            //        File = new FileDescription(filePath),
                            //    };
                            //    var uploadParams = imageUploadParams;
                            //    var result = _cloudinary.Upload(uploadParams);
                            //    item.Url = result.SecureUrl.ToString();
                            //    item.Name = formFile.FileName;
                            //    item.FileExt = ext;
                            //    item.TypeUpload = 1;
                            //}


                            //upload file with base64 stream
                            using (var memory = new MemoryStream())
                            {
                                //check for filestream may be effect file name upload in Cloudinary
                                using var fileStream = formFile.OpenReadStream();
                                byte[] bytes = new byte[formFile.Length];
                                fileStream.Read(bytes, 0, (int)formFile.Length);
                                fileStream.Position = 0;
                                //using file path
                                uploadParams.File = new FileDescription(fileName, fileStream);
                                var result = _cloudinary.Upload(uploadParams);

                                //random string 
                                item.Url = result.SecureUrl.ToString();
                                item.Name = formFile.FileName;
                                item.FileExt = ext;
                                item.TypeUpload = 1;
                            }
                            createFileDTOs.Add(item);
                        }

                        if (!DataType.TypeAccept[DataType.ETypeFile.Image].Contains(ext))
                        {
                            using (var stream = File.Create(filePath))
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

            await Task.CompletedTask;
            return createFileDTOs;
        }
    }
}
