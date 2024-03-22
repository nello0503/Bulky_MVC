using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)    
            { 
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
                return View(productList);
            }
            else
            {
                List<Product> productList = _unitOfWork.Product.GetAll().Where(x=>x.CategoryId == id).ToList();
                return View(productList);
            }
           

        }

        public IActionResult Create()
        {


            ProductVM productVM = new()
            {
                ShiftList = _unitOfWork.Category.GetAll()
                        .Select(x => new SelectListItem
                        {
                            Text = x.Name.ToString(),
                            Value = x.Id.ToString(),
                        }),

                Product = new Product()
                    
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created";
                return RedirectToAction("Index");
            }
            else
            {
                obj.ShiftList = _unitOfWork.Category.GetAll()
                        .Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(),
                        });
                return View(obj);
            }

            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _unitOfWork.Product.Get(x => x.Id == id);

            return View(obj);
        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated";
                return RedirectToAction("Index");
            }


            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _unitOfWork.Product.Get(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.Get(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category removed";
            return RedirectToAction("Index");
        }



    }
}
