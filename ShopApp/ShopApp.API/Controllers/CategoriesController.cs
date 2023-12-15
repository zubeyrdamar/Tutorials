using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.API.Models.DTO;
using ShopApp.Business.Abstract;
using ShopApp.Entities;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult List() 
        {
            return Ok(mapper.Map<List<CategoryDTO>>(categoryService.List().ToList()));    
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Read([FromRoute] int id) 
        { 
            var category = categoryService.Read(id);
            if(category == null) { return NotFound(); }

            return Ok(mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            categoryService.Create(category);
            return Ok(categoryDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] Category category)
        {
            var tempCategory = categoryService.Read(category.Id);
            if (tempCategory == null) { return BadRequest(); }

            tempCategory.Name = category.Name;
            categoryService.Update(tempCategory);

            var categoryDTO = mapper.Map<CategoryDTO>(tempCategory);
            return Ok(categoryDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var category = categoryService.Read(id);
            if (category == null) { return NotFound(); }

            categoryService.Delete(category);
            var categoryDTO = mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

    }
}
