using Domain.DTOs.Files;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Files
{
    public interface IFileManager
    {
        Task<List<CreateFileDTO>> SaveFile(SaveFileDTO saveFile);
        Task<IActionResult> DownloadFile(string url);
    }
}
