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
    public class SubCategoryController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;

        public SubCategoryController(Sxylo_Stock.Model.DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        // cette fonction permet d'afficher la liste de toutes les categories
        [HttpGet]
        [Route("ListSubCategory")]
        public async Task<ActionResult<List<SubCategory>>> ListSubCategory()
        {
           
            var listSubCategory = await this.databaseContext.subCategory.Where(p => p.archived == 1 && p.category.archived == 1).Include(p => p.category).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listSubCategory == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Cette  sous categorie n'existe pas !",
                });
            
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des sous categories ", mot = listSubCategory });
        }


        [HttpGet]
        [Route("ListSubCategoryByCategory")]
        public async Task<ActionResult<List<SubCategory>>> ListSubCategoryByCategory(int idCategory)
        {

            var listSubCategoryByCategory = await this.databaseContext.subCategory.Where(p => p.archived == 1 && p.category.archived == 1 && p.categoryid == idCategory).Include(p => p.category).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listSubCategoryByCategory == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Aucune sous categorie trouver pour cette Categorie !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des sous categories selon votre choix ", mot = listSubCategoryByCategory });
        }




        // cette fonction va permettre l'ajout une sous categorie
        [HttpPost]
        [Route("AddSubCategory")]
        public async Task<ActionResult<SubCategory>> AddSubCategory(SubCategoryDto subCategoryDto)
        {
            SubCategory subCategory = new SubCategory();

            subCategory.name = subCategoryDto.name;
            subCategory.description = subCategoryDto.description;
            subCategory.categoryid = subCategoryDto.categoryid;
            subCategory.creatAt = DateTime.Now.Date;
            subCategory.archived = 1;
            databaseContext.subCategory.Add(subCategory);
            databaseContext.SaveChanges();

            return new JsonResult(new { statusCode = 1, message = " Cette sous categorie a été ajouter !" , sousCategorie = subCategory });
        }

        // cette fonction permet la modification d'une sous categorie
        [HttpPut]
        [Route("UpdateSubCategory")]
        public async Task<ActionResult<SubCategory>> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {

            var subCategoryData = await this.databaseContext.subCategory.Where(h => h.id == updateSubCategoryDto.id && h.archived == 1).FirstOrDefaultAsync();

            if (subCategoryData == null) return new JsonResult(new { StatusCode = -1, message = "Aucun element trouver" });

            subCategoryData.name = updateSubCategoryDto.name;
            subCategoryData.description = updateSubCategoryDto.description;
            subCategoryData.categoryid = updateSubCategoryDto.categoryid;
            databaseContext.subCategory.Update(subCategoryData);
            databaseContext.SaveChanges();


            return new JsonResult(new { StatusCode = 1, message = "Cette sous categorie a été modifier" });

        }


        // cette fonction va permettre de supprimer une sous categorie
        [HttpGet]
        [Route("DeleteSubCatagory"), Authorize]
        public async Task<ActionResult> DeleteSubCatagory(int id)
        {

            var subCategoryData = await this.databaseContext.subCategory.Where(h => h.id == id && h.archived == 1).FirstOrDefaultAsync();
            if (subCategoryData == null) return new JsonResult(new { StatusCode = -1, message = "Cette sous categorie a déja été supprimer" });
            subCategoryData.archived = 0;
            databaseContext.subCategory.Update(subCategoryData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette  sous categorie a été supprimer" });
        }
    }
}
