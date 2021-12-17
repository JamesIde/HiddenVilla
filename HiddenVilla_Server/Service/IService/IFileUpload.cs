using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string fileName);

    }
}
