using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sxylo_Stock.Dto;
using Sxylo_Stock.Model;

namespace Sxylo_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureArticleController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;
        private static IWebHostEnvironment _webHostEnvironment;

        public PictureArticleController(Sxylo_Stock.Model.DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment)
        {
            this.databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpGet]
        [Route("ListPictureArticle")]
        public async Task<ActionResult<List<PictureArticle>>> ListPictureArticle()
        {

            var listPictureArticle = databaseContext.pictureArticles.Where(p => p.archived == 1).OrderByDescending(p => p.creatAt).ToList();
            if (listPictureArticle == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Aucune image  n'existe  !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des image d'un article ", mot = listPictureArticle });
        }


        [HttpPost]
        [Route("AddPictureArticle")]
        public async Task<ActionResult<PictureArticle>> AddPictureArticle([FromForm] PictureArticleDto pictureArticleDto)
        {
            /*Random rnd = new Random();
            string extension = Path.GetExtension(pictureArticleDto.pathJoinced.FileName);
            String fileName = "article" + rnd.Next() + "" + extension;
            string save = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles");
            
           
            string filepath = Path.Combine(save, fileName);
           

            
            PictureArticle pictureArticle = new PictureArticle();

            pictureArticle.fileName = pictureArticleDto.fileName;

            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                await pictureArticleDto.pathJoinced.CopyToAsync(filestream);
            }
            pictureArticle.pathJoinced = fileName;
            pictureArticle.creatAt = DateTime.Now;
            pictureArticle.archived = 1;
            pictureArticle.fileName = "";
            pictureArticle.articleid = 1; 
            databaseContext.pictureArticles.Add(pictureArticle);
            databaseContext.SaveChanges();*/
            return new JsonResult(new { StatusCode = 1, message = "Images Ajouter" });



        }


        // modifier les images d'un article
        [HttpPut]
        [Route("UpdatePictureArticle")]
        public async Task<ActionResult<PictureArticle>> UpdatePictureArticle([FromForm] UpdatePictureArticleDto updatePictureArticleDto)
        {
            string save = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles");
            var pictureArticleData = databaseContext.pictureArticles.Where(h => h.id == updatePictureArticleDto.id && h.archived == 1).FirstOrDefault();


            pictureArticleData.fileName = updatePictureArticleDto.fileName;
            string filepath = Path.Combine(save, updatePictureArticleDto.pathJoinced.FileName);
            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                await updatePictureArticleDto.pathJoinced.CopyToAsync(filestream);
            }
            pictureArticleData.pathJoinced = updatePictureArticleDto.pathJoinced.FileName;

            databaseContext.pictureArticles.Update(pictureArticleData);
            databaseContext.SaveChanges();

            return new JsonResult(new { StatusCode = 1, message = "Images Modifier" });



        }


        // cette fonction va permettre de supprimer des images
        [HttpGet]
        [Route("DeletePictureArticle")]
        public async Task<ActionResult> DeletePictureArticle(int id)
        {

            var pictureArticleData = databaseContext.pictureArticles.Where(h => h.id == id && h.archived == 1).FirstOrDefault();
            if (pictureArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Cette image a déja été supprimer" });
            pictureArticleData.archived = 0;
            databaseContext.pictureArticles.Update(pictureArticleData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette image a été supprimer" });
        }
    }
}