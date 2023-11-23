using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sxylo_Stock.Dto;
using Sxylo_Stock.Model;
using Sxylo_Stock.Database;
using Sxylo_Stock.Model.Entities;

namespace Sxylo_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly Sxylo_Stock.Model.DatabaseContext databaseContext;
        private static IWebHostEnvironment _webHostEnvironment;

        public ArticleController(Sxylo_Stock.Model.DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment)
        {
            this.databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
        }




        [HttpGet]
        [Route("ListArticle"), Authorize]
        public async Task<ActionResult<List<Article>>> ListArticle()
        {

            var listArticle = await this.databaseContext.articles.Where(p => p.archived == 1).Include(p => p.subCategory).Include(p => p.subCategory.category).OrderByDescending(p => p.creatAt).ToListAsync();
            if (listArticle == null)
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Aucune image  n'existe  !",
                });
            return new JsonResult(new { StatusCode = 1, message = "Voici la liste des  articles ", mot = listArticle });
        }
        [HttpGet]
        [Route("ListArticleWithImage")]
        public async Task<ActionResult<List<Article>>> ListArticleWithImage()
        {
            var article = databaseContext.articles.Where(p => p.archived == 1).FirstOrDefault();
            var listArticleWithImage = databaseContext.articles.Where(p => p.archived == 1 && p.subCategory.archived == 1 && p.subCategory.category.archived == 1 && p.archived == 1).Include(p => p.subCategory).OrderByDescending(p => p.creatAt).ToList();



            //var baseUri = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            List<Dictionary<string, Object>> datas = new List<Dictionary<string, Object>>();
            foreach (var item in listArticleWithImage.ToList()) datas.Add(Constante.getJsonAll(item));


            return new JsonResult(new { StatusCode = 1, data = datas, message = "Voici la liste de vos articles" });
        }

        [HttpGet]
        [Route("ListArticleBySubCategory"), Authorize]
        public async Task<ActionResult<List<Article>>> ListArticleBySubCategory(int IdSubcategory)
        {
            var listArticleBySubCategory = await this.databaseContext.articles.Where(p => p.archived == 1 && p.subCategoryid == IdSubcategory && p.subCategory.archived == 1).Include(p => p.subCategory).Include(p => p.subCategory.category).OrderByDescending(p => p.creatAt).ToListAsync();
            List<Dictionary<string, Object>> datas = new List<Dictionary<string, Object>>();
            foreach (var item in listArticleBySubCategory.ToList()) datas.Add(Constante.getJsonAllBySubCategory(item));


            return new JsonResult(new { StatusCode = 1, data = datas, message = "Voici la liste de vos articles selon votre choix" });
        }

        /*[HttpGet]
        [Route("ListAllInformationsOnArticle")]
        public async Task<ActionResult<List<Article>>> ListAllInformationsOnArticle(int IdArticle)
        {
            var listItemArticleOnTableArticle = await this.databaseContext.articles.Where(p => p.archived == 1 && p.id == IdArticle && p.subCategory.archived == 1 && p.subCategory.category.archived == 1).Include(p => p.subCategory).Include(p => p.subCategory.category).ToListAsync();
            var listItemArticleOnTablePictureArticle = await this.databaseContext.pictureArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1 && p.article.subCategory.category.archived == 1).ToListAsync();
            var listItemArticleOnTableAssocColor = await this.databaseContext.assocColorArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1 && p.colorArticle.archived == 1 && p.article.subCategory.category.archived == 1).Include(p => p.colorArticle).ToListAsync();
            List<Dictionary<string, Object>> datas = new List<Dictionary<string, Object>>();
            foreach (var item in listItemArticleOnTableArticle.ToList()) datas.Add(Constante.getJsonAll(item));
            foreach (var item in listItemArticleOnTablePictureArticle.ToList()) datas.Add(Constante.getJsonPictureArticle(item));
            foreach (var item in listItemArticleOnTableAssocColor.ToList()) datas.Add(Constante.getJsonColorArticle(item));


            return new JsonResult(new { StatusCode = 1, data = datas, message = "Voici la liste de vos articles selon votre choix" });
        }*/

        [HttpGet]
        [Route("ListOfAllItemsArticle")]
        public async Task<ActionResult<List<Article>>> ListOfAllItemsArticle(int IdArticle)
        {
            var listItemArticleOnTableArticle = await this.databaseContext.articles.Where(p => p.archived == 1 && p.id == IdArticle && p.subCategory.archived == 1 && p.subCategory.category.archived == 1).Include(p => p.subCategory).Include(p => p.subCategory.category).ToListAsync();
            var listItemArticleOnTablePictureArticle = await this.databaseContext.pictureArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1 && p.article.subCategory.category.archived == 1).ToListAsync();
            AssocSizeArticle listItemArticleOnTableAssocSize = databaseContext.assocSizeArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1).FirstOrDefault();
            var IdSizeOnAsssocArticle = listItemArticleOnTableAssocSize.sizeArticleid;
            var getSizeOfArticle = await this.databaseContext.sizeArticles.Where(p => p.archived == 1 && p.id == IdSizeOnAsssocArticle && p.subCategory.archived == 1 && p.subCategory.category.archived == 1).ToListAsync();
            var listItemArticleOnTableAssocColor = await this.databaseContext.assocColorArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1 && p.colorArticle.archived == 1 && p.article.subCategory.category.archived == 1).Include(p => p.colorArticle).ToListAsync();
            if (listItemArticleOnTableArticle == null || listItemArticleOnTablePictureArticle == null || listItemArticleOnTableAssocColor == null || getSizeOfArticle == null)
            {
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Il manque des informations sur cette article !",
                });
            }

            //var baseUri = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            //List<Dictionary<string, Object>> listItemArticleOnTablePictureArticle = new List<Dictionary<string, Object>>();
            //foreach (var item in listPictureArticle.ToList()) listItemArticleOnTablePictureArticle.Add(Constante.getJsonPictureArticle(item, baseUri));
            return new JsonResult(new
            {
                StatusCode = 1,
                datas = listItemArticleOnTableArticle,
                listItemArticleOnTablePictureArticle,
                listItemArticleOnTableAssocColor,
                getSizeOfArticle,


                message = "Voici toutes les informations concernant cette article"

            });
        }

       /* [HttpGet]
        [Route("ListOfAllImageArticle")]
        public async Task<ActionResult<List<PictureArticle>>> ListOfAllImageArticle(int IdArticle)
        {
            var listItemArticleOnTablePictureArticle = databaseContext.pictureArticles.Where(p => p.archived == 1 && p.articleid == IdArticle && p.article.archived == 1 && p.article.subCategory.category.archived == 1).ToList();

            if (listItemArticleOnTablePictureArticle == null)
            {
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Cette Article ne possede aucune image!",
                });
            }
            var baseUri = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            List<Dictionary<string, Object>> datas = new List<Dictionary<string, Object>>();
            foreach (var item in listItemArticleOnTablePictureArticle.ToList()) datas.Add(Constante.getImageArticle(item, baseUri));



            return new JsonResult(new
            {
                StatusCode = 1,
                data =
                datas,

                message = "Voici toutes les informations concernant cette article"

            });
        }*/





        // cette fonction va permettre l'ajout d'un article
        // cette fonction va permettre l'ajout d'un article
        [HttpPost]
        [Route("AddArticle")]
        public async Task<ActionResult<Article>> AddArticle([FromForm] ArticleDto articleDto)
        {

            Article article = new Article();

            article.name = articleDto.name;
            article.description = articleDto.description;
            if (articleDto.alertquantity >= articleDto.quantity)
            {
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "La quantite d'alerte doit etre inferieur a la quantite en stock!",
                });
            }
            article.quantity = articleDto.quantity;
            article.alertquantity = articleDto.alertquantity;
            article.newPrice = articleDto.newPrice;
            article.oldPrice = articleDto.oldPrice;
            article.lienAffiliation = articleDto.lienAffiliation;
            article.nbreMonthGaranti = articleDto.nbreMonthGaranti;
            article.codeArticle = Constante.GenerationCodeArticle();
            article.dateExpiration = articleDto.dateExpiration;
            article.subCategoryid = articleDto.subCategoryid;
            article.numSeri = articleDto.numSeri;
            article.memoryStorage = articleDto.memoryStorage;
            article.modelName = articleDto.modelName;
            article.renewed = articleDto.renewed;
            article.operatingSystem = articleDto.operatingSystem;
            article.status = 0;
            article.etat = 0;
            article.creatAt = DateTime.Now;
            article.updatAt = DateTime.Now.Date;
            article.archived = 1;

            databaseContext.articles.Add(article);
            databaseContext.SaveChanges();

            //Upload et enregistrement dans la table pictureArticle
            var IdArticle = article.id;

            //Ajout la taille d'un article
            SaveSize(articleDto.sizeid, IdArticle);

            // Ajout des couleurs d'un article
            string[] colorList = articleDto.colorid.Split(",");
            SaveColor(colorList, IdArticle);

            //ResponseImage response = await uploadFile(articleDto.pictureArticle);
            Random rnd = new Random();
            string extension = Path.GetExtension(articleDto.pictureArticle.FileName);

            String fileName = "article" + rnd.Next() + "" + extension;
            string[] listExtention = { ".png", ".jpg", ".jpeg", ".PNG" };

            if (articleDto.pictureArticle.Length >= 5 * 1024 * 1024)
            {
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "La taille du fichier  doit être inférieur a 5 Mo!",
                });
            }


            if (!listExtention.Contains(extension))
            {
                return new JsonResult(new
                {
                    StatusCode = -1,
                    message = "Extention du fichier non valide!",
                });
            }
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles/", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await articleDto.pictureArticle.CopyToAsync(stream);

            }

            //Insert In Article Profile table
            PictureArticle pictureArticle = new PictureArticle();

            pictureArticle.articleid = IdArticle;
            pictureArticle.fileName = fileName;
            pictureArticle.pathJoinced = fileName;
            pictureArticle.archived = 1;
            pictureArticle.creatAt = DateTime.Now;
            databaseContext.pictureArticles.Add(pictureArticle);
            databaseContext.SaveChanges();




            return new JsonResult(new { StatusCode = 1, message = " Article ajouté", mot = article });
        }

        // Cette fonction va permettre de De recuperer la couleur , l'id de l'article et l'ajouter dans
        // la table AssocArticle
        private string SaveColor(string[] listColorArticleid, int IdArticle)
        {

            try
            {
                foreach (var item in listColorArticleid)
                {

                    AssocColorArticle assocColorArticle = new AssocColorArticle();
                    assocColorArticle.articleid = IdArticle;
                    assocColorArticle.colorArticleid = int.Parse(item);
                    assocColorArticle.archived = 1;
                    assocColorArticle.libelle = "";
                    assocColorArticle.creatAt = DateTime.Now.Date;

                    databaseContext.assocColorArticles.Add(assocColorArticle);
                    databaseContext.SaveChanges();

                }

                return "Ok";
            }
            catch (Exception e)
            {
                return "No";

            }

        }

        private string SaveMorePicture(IFormFile[] listPicture, int IdArticle)
        {

            try

            {
                foreach (var item in listPicture)
                {
                    Random rnd = new Random();
                    string extension = Path.GetExtension(item.FileName);

                    String fileName = "article" + rnd.Next() + "" + extension;
                    string[] listExtention = { ".png", ".jpg", ".jpeg", ".PNG" };

                    if (item.Length >= 5 * 1024 * 1024)
                    {
                        /* return new JsonResult(new
                         {
                             StatusCode = -1,
                             message = "La taille du fichier  doit être inférieur a 5 Mo!",
                         });*/
                        return "Ok";
                    }


                    if (!listExtention.Contains(extension))
                    {
                        /*return new JsonResult(new
                        {
                            StatusCode = -1,
                            message = "Extention du fichier non valide!",
                        });*/
                        return "Ok";
                    }
                    var path = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles/", fileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        item.CopyToAsync(stream)
    ;

                    }

                    // Enregistrement 
                    PictureArticle pictureArticle = new PictureArticle();
                    pictureArticle.articleid = IdArticle;
                    pictureArticle.fileName = fileName;
                    pictureArticle.pathJoinced = fileName;
                    pictureArticle.archived = 1;
                    pictureArticle.creatAt = DateTime.Now;
                    databaseContext.pictureArticles.Add(pictureArticle);
                    databaseContext.SaveChanges();

                }

                return "Ok";
            }
            catch (Exception e)
            {
                return "No";

            }

        }

        private string SaveSize(int sizeArticleid, int IdArticle)
        {

            try
            {

                AssocSizeArticle assocSizeArticle = new AssocSizeArticle();
                assocSizeArticle.articleid = IdArticle;
                assocSizeArticle.sizeArticleid = sizeArticleid;
                assocSizeArticle.archived = 1;
                assocSizeArticle.libelle = "";
                assocSizeArticle.creatAt = DateTime.Now.Date;

                databaseContext.assocSizeArticles.Add(assocSizeArticle);
                databaseContext.SaveChanges();

                return "Ok";
            }
            catch (Exception e)
            {
                return "No";

            }

        }

        private async Task<ResponseImage> uploadFile(IFormFile fichier)
        {
            Random rnd = new Random();
            ResponseImage response = new ResponseImage();

            string extension = Path.GetExtension(fichier.FileName);

            String fileName = "article" + rnd.Next() + "" + extension;


            if (fichier.Length >= 5 * 1024 * 1024)
            {
                response.message = "La taille du fichier  doit être inférieur a 5 Mo";
                response.statusCode = -1;
                return response;
            }

            string[] listExtention = { ".png", ".jpg", ".jpeg", ".PNG" };
            if (!listExtention.Contains(extension))
            {
                response.message = "Extention du fichier non valide";
                response.statusCode = -1;
                return response;
            }
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles/", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await fichier.CopyToAsync(stream);

            }

            response.message = "Sauvegarde ok";
            response.statusCode = 1;
            response.nameFile = fileName;
            return response;

        }



        [HttpPost]
        [Route("AddMorePicture")]
        public async Task<ActionResult<Article>> AddMorePicture([FromForm] MoreImageDto moreImageDto)
        {
            //var ArticleData = databaseContext.pictureArticles.Where(h => h.articleid == moreImageDto.IdArticle && h.archived == 1).FirstOrDefault();
            //if (ArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Aucun Article Trouver" });
            foreach (var item in moreImageDto.listImageOfArticle)
            {
                Random rnd = new Random();
                string extension = Path.GetExtension(item.FileName);

                String fileName = "article" + rnd.Next() + "" + extension;
                string[] listExtention = { ".png", ".jpg", ".jpeg", ".PNG" };

                if (item.Length >= 5 * 1024 * 1024)
                {
                    return new JsonResult(new
                    {
                        StatusCode = -1,
                        message = "La taille du fichier  doit être inférieur a 5 Mo!",
                    });
                }


                if (!listExtention.Contains(extension))
                {
                    return new JsonResult(new
                    {
                        StatusCode = -1,
                        message = "Extention du fichier non valide!",
                    });
                }
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "PicturesArticles/", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream)
;

                }

                // Enregistrement 
                PictureArticle pictureArticle = new PictureArticle();
                pictureArticle.articleid = moreImageDto.IdArticle;
                pictureArticle.fileName = fileName;
                pictureArticle.pathJoinced = fileName;
                pictureArticle.archived = 1;
                pictureArticle.creatAt = DateTime.Now;
                databaseContext.pictureArticles.Add(pictureArticle);
                databaseContext.SaveChanges();

            }
            //IFormFile[] pictureList = moreImageDto.listImageOfArticle;
            //SaveMorePicture()

            return new JsonResult(new { StatusCode = 1, message = "Vos images ont ete ajouter " });

        }

        [HttpGet]
        [Route("DeleteArticle")]
        public async Task<ActionResult> DeleteArticle(int idArticle)
        {

            var ArticleData = await this.databaseContext.articles.Where(h => h.id == idArticle && h.archived == 1).FirstOrDefaultAsync();
            if (ArticleData == null) return new JsonResult(new { StatusCode = -1, message = "Cet article a déja été supprimer" });
            ArticleData.archived = 0;
            databaseContext.articles.Update(ArticleData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cet article a été supprimer" });
        }

        [HttpGet]
        [Route("DeletePictureArticle")]
        public async Task<ActionResult> DeletePictureArticle(int idPicture)
        {

            var PictureData = databaseContext.pictureArticles.Where(h => h.id == idPicture && h.archived == 1).FirstOrDefault();
            if (PictureData == null) return new JsonResult(new { StatusCode = -1, message = "Cette image a déja été supprimer" });
            PictureData.archived = 0;
            databaseContext.pictureArticles.Update(PictureData);
            await this.databaseContext.SaveChangesAsync();

            return new JsonResult(new { StatusCode = 1, message = " Cette image a été supprimer" });
        }
    }





}

