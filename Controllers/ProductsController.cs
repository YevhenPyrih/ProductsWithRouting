using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class ProductsController : Controller
    {
        private List<Product> myProducts;
        private int nextId= 0;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
            nextId += myProducts.Count + 1;
        }

        [Route("[controller]/[action]")]
        [Route("items/[action]")]
        [Route("[controller]")]
        [Route("items")]
        public IActionResult Index(int filterId, string filtername)
        {
            ViewBag.FilterId = filterId;
            ViewBag.FilterName = filtername;
            return View(myProducts);
        }

        [Route("[controller]/{id}")]
        public IActionResult View(int id)
        {
            foreach(var product in myProducts)
            {
                if (product.Id == id)
                {
                    return View("View", product);
                }
            }
            Response.StatusCode = 404;
            return View("NoIndexError");
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Edit(int id)
        {
            foreach(var product in myProducts)
            {
                if (product.Id == id)
                {
                    return View("Edit", product);
                }
            }
            Response.StatusCode = 404;
            return View("NoIndexError");
        } 

        [HttpPost]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Edit(Product product)
        {
            for(int i = 0; i < myProducts.Count; i++)
            {
                if (myProducts[i].Id == product.Id)
                {
                    myProducts[i] = product;
                    break;
                }
            }
            return View("Index", myProducts);
        } 
        
        [HttpPost]
        [Route("[controller]/[action]")]
        [Route("[controller]/new")]
        public IActionResult Create(Product product)
        {
            myProducts.Add(product);
            return View("Index", myProducts);
        }

        [Route("[controller]/[action]")]
        [Route("[controller]/new")]
        public IActionResult Create()
        {
            ViewBag.Index = nextId;
            nextId++;
            return View(new Product());
        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < myProducts.Count; i++)
            {
                if (myProducts[i].Id == id)
                {
                    myProducts.RemoveAt(i);
                    break;
                }
            }
            return View("Index", myProducts);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
