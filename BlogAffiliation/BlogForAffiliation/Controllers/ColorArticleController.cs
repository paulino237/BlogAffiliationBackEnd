using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sxylo_Stock.Dto;
using Sxylo_Stock.Model;

namespace Sxylo_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorArticleController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;

        public ColorArticleController(Sxylo_Stock.Model.DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }



        // fonction permettant d'obtenir la liste des couleurs d'un article
        [HttpGet]
        [Route("ListColorArticle")]
        public async Task<ActionResult<List<ColorArticle>>> ListColorArticle()
        {

            var listColorArticle = await this.databaseContext.colorArticles.Where(p => p.archived == 1).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listColorArticle == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Cette  couleur n'existe pas !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des couleurs d'un article ", mot = listColorArticle });
        }


        // cette fonction permet l'ajout d'une couleur
        [HttpPost]
        [Route("AddColorArticle")]
        public async Task<ActionResult> AddColorArticle(ColorArticleDto colorArticleDto)
        {
            ColorArticle colorArticle = new ColorArticle();

            colorArticle.libelle = colorArticleDto.libelle;
            colorArticle.description = colorArticleDto.description;
            colorArticle.creatAt = DateTime.Now;
            colorArticle.archived = 1;
            databaseContext.colorArticles.Add(colorArticle);
            databaseContext.SaveChanges();

            return new JsonResult(new { statusCode = 1, message = " Cette couleur a été ajouter !" });
        }


        // cette fonction permet la modification d'une couleur
        [HttpPut]
        [Route("UpdateColorArticle")]
        public async Task<ActionResult<ColorArticle>> UpdatUpdateColorArticleeCategory(UpdateColorArticleDto updateColorArticleDto)
        {

            var colorArticleData = await this.databaseContext.colorArticles.Where(h => h.id == updateColorArticleDto.id && h.archived == 1).FirstOrDefaultAsync();

            if (colorArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Aucun element trouver" });

            colorArticleData.libelle = updateColorArticleDto.libelle;
            colorArticleData.description = updateColorArticleDto.description;
            databaseContext.colorArticles.Update(colorArticleData);
            databaseContext.SaveChanges();


            return new JsonResult(new { StatusCode = 1, message = "Cette couleur a été modifier" });

        }

        // cette fonction va permettre de supprimer une couleur
        [HttpGet]
        [Route("DeleteColorArticle")]
        public async Task<ActionResult> DeleteColorArticle(int id)
        {

            var colorArticleData = await this.databaseContext.colorArticles.Where(h => h.id == id && h.archived == 1).FirstOrDefaultAsync();
            if (colorArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Cette couleur a déja été supprimer" });
            colorArticleData.archived = 0;
            databaseContext.colorArticles.Update(colorArticleData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette couleur a été supprimer" });
        }
    }
}
