using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Files;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseFile)]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IFileManager _fileManager;
        public FileController(IAuthService authService, IUserManager userManager, IFileService fileService, IFileManager fileManager) : base(authService, userManager, fileService)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        public IActionResult GetFile([FromQuery] SearchPaginationDTO<FileDTO> serachPagination)
        {
            var result = _fileService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.BaseFileGetType)]
        public IActionResult GetFileType()
        {
            var result = DataType.TypeName.ToArray();
            return CommonResponse(new ReturnMessage<KeyValuePair<string, string>[]>(false, result, MessageConstants.SearchSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile([FromForm] SaveFileDTO dto)
        {
            var saveFiles = await _fileManager.SaveFile(dto);
            var result = _fileService.Create(saveFiles);
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult UpdateFile([FromForm] List<UpdateFileDTO> dto)
        {

            var result = _fileService.Update(dto);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.BaseFileDownload)]
        public async Task<IActionResult> DownloadFile([FromQuery] string url)
        {
            var fileDownload = await _fileManager.DownloadFile(url);
            return fileDownload;
        }

    }
}
