using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file, CancellationToken cancellationToken)
        {
            // eğer bir fotoğraf geldiğinde işlem bir anda sonlanırsa cancellacion token sayesinde fotografın yüklenmesi engellenir.
            // create if not exist kullanılabilir.
            if(file is not null && file.Length>0) 
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "photos", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var return_path = Path.Combine("photos", file.FileName);
                return CreateActionResultInstance<SavePhotoDto>(new ResponseObject<SavePhotoDto>(new SavePhotoDto { Url = return_path },200,true));
            }
            return CreateActionResultInstance<SavePhotoDto>(new ResponseObject<SavePhotoDto>(new SavePhotoDto { Url = "#" }, 404, false,new List<string> { "Hatalı resim gönderildi."}));

        }
        [HttpDelete]
        public IActionResult Delete([FromQuery]string photoUrl)
        { 
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance<SavePhotoDto>(new ResponseObject<SavePhotoDto>(new SavePhotoDto { Url = "#" }, 404, false, new List<string> { "Hatalı url gönderildi." }));
            }
            else
            {
                System.IO.File.Delete(path);
                return CreateActionResultInstance<SavePhotoDto>(new ResponseObject<SavePhotoDto>(new SavePhotoDto { Url = "#" }, 200, true));

            }

        }
    }
}
