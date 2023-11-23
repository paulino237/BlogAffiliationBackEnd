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
    public class CategoryController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;

        public CategoryController(Sxylo_Stock.Model.DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        // fonction permettant d'obtenir la liste des categories
        [HttpGet]
        [Route("ListCategory")]
        public async Task<ActionResult<List<Category>>> ListCategory()
        {

            var listCategory = databaseContext.category.Where(p => p.archived == 1).OrderByDescending(p => p.creatAt).ToList();
            if (listCategory == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Cette  Categorie n'existe pas !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des categories ", mot = listCategory });
        }


        // cette fonction permet l'ajout d'une categorie
        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            Category category = new Category();

            category.name = categoryDto.name;
            category.description = categoryDto.description;
            category.creatAt = DateTime.Now;
            category.archived = 1;
            databaseContext.category.Add(category);
            databaseContext.SaveChanges();

            return new JsonResult(new { statusCode = 1, message = " Cette categorie a été ajouter !" });
        }


        // cette fonction permet la modification d'une category
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {

            var categoryData = await this.databaseContext.category.Where(h => h.id == categoryUpdateDto.id && h.archived == 1).FirstOrDefaultAsync();

            if (categoryData == null) return new JsonResult(new { StatusCode = -1, message = "Aucun element trouver" });

            categoryData.name = categoryUpdateDto.name;
            categoryData.description = categoryUpdateDto.description;
            databaseContext.category.Update(categoryData);
            databaseContext.SaveChanges();


            return new JsonResult(new { StatusCode = 1, message = "Cette categorie a été modifier" });

        }


        // cette fonction va permettre de supprimer une categorie
        [HttpGet]
        [Route("DeleteCatagory"), Authorize]
        public async Task<ActionResult> DeleteCatagory(int id)
        {

            var categoryData = await this.databaseContext.category.Where(h => h.id == id && h.archived == 1).FirstOrDefaultAsync();
            if (categoryData == null) return new JsonResult(new { StatusCode = -1, message = "Cette categorie a déja été supprimer" });
            categoryData.archived = 0;
            databaseContext.category.Update(categoryData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette categorie a été supprimer" });
        }
    }
}
