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
    public class SizeArticleController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;

        public SizeArticleController(Sxylo_Stock.Model.DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }



        // cette fonction permet d'afficher la liste de toutes les tailles d'articles
        [HttpGet]
        [Route("ListSizeArticle")]
        public async Task<ActionResult<List<SizeArticle>>> ListSizeArticle()
        {

            var listSizeArticle = await this.databaseContext.sizeArticles.Where(p => p.archived == 1 && p.subCategory.archived == 1).Include(p => p.subCategory).Include(p=>p.subCategory.category).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listSizeArticle == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Cette  taille n'existe pas !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des tailles ", mot = listSizeArticle });
        }

        [HttpGet]
        [Route("ListSizeArticleBySubCategory")]
        public async Task<ActionResult<List<SizeArticle>>> ListSizeArticleBySubCategory(int idSubCategory)
        {

            var listSizeArticleBySubCategory = await this.databaseContext.sizeArticles.Where(p => p.archived == 1 && p.subCategoryid == idSubCategory && p.subCategory.category.archived == 1).Include(p => p.subCategory).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listSizeArticleBySubCategory == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Aucune taille trouver pour cette sous categorie !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste tailles selon votre choix ", mot = listSizeArticleBySubCategory });
        }

        // cette fonction va permettre l'ajout d'une taille
        [HttpPost]
        [Route("AddSizeArticle")]
        public async Task<ActionResult> AddSizeArticle(SizeArticleDto sizeArticleDto)
        {
            SizeArticle sizeArticle = new SizeArticle();

            sizeArticle.libelle = sizeArticleDto.libelle;
            sizeArticle.subCategoryid = sizeArticleDto.subCategoryid;
            sizeArticle.creatAt = DateTime.Now;
            sizeArticle.archived = 1;
            databaseContext.sizeArticles.Add(sizeArticle);
            databaseContext.SaveChanges();

            return new JsonResult(new { statusCode = 1, message = " Cette taille a été ajouter !",  TailleArticle = sizeArticle });
        }


        // cette fonction permet la modification d'une taille d'article
        [HttpPut]
        [Route("UpdateSizeArticle")]
        public async Task<ActionResult<SizeArticle>> UpdateSizeArticle(UpdateSizeArticleDto updateSizeArticleDto)
        {
            var sizeArticleData = await this.databaseContext.sizeArticles.Where(h => h.id == updateSizeArticleDto.id && h.archived == 1).FirstOrDefaultAsync();

            if (sizeArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Aucun element trouver" });

            sizeArticleData.libelle = updateSizeArticleDto.libelle;
            sizeArticleData.subCategoryid = updateSizeArticleDto.subCategoryid;
            databaseContext.sizeArticles.Update(sizeArticleData);
            databaseContext.SaveChanges();


            return new JsonResult(new { StatusCode = 1, message = "Cette taille a été modifier" });

        }


        // cette fonction va permettre de supprimer une taille
        [HttpGet]
        [Route("DeleteSizeArticle")]
        public async Task<ActionResult> DeleteSizeArticle(int id)
        {

            var sizeArticleData = await this.databaseContext.sizeArticles.Where(h => h.id == id && h.archived == 1).FirstOrDefaultAsync();
            if (sizeArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Cette taille a déja été supprimer" });
            sizeArticleData.archived = 0;
            databaseContext.sizeArticles.Update(sizeArticleData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette taille a été supprimer" });
        }
    }
}
