using AutoMapper;
using Loja_Projeto.Model;
using Loja_Projeto.Repository.Interface;
using Loja_Projeto.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Loja_Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        private ActionResult ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errorMessages });
            }
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CategoriaResponseViewModel>>> Get()
        {
            var listaCategorias = await _categoriaRepository.FindAllAsync();

            if (listaCategorias != null && listaCategorias.Count > 0)
            {
                var categoriaResponse = _mapper.Map<IList<CategoriaResponseViewModel>>(listaCategorias);
                return Ok(categoriaResponse);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaResponseViewModel>> Get([FromRoute] int id)
        {
            var categoriaModel = await _categoriaRepository.FindByIdAsync(id);

            if (categoriaModel != null)
            {
                var categoriaResponse = _mapper.Map<CategoriaResponseViewModel>(categoriaModel);
                return Ok(categoriaResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var categoria = await _categoriaRepository.FindByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            await _categoriaRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponseViewModel>> Post([FromBody] CategoriaRequestViewModel categoriaRequest)
        {
            var validationError = ValidateModelState();
            if (validationError != null)
            {
                return validationError;
            }

            var categoriaModel = _mapper.Map<CategoriaModel>(categoriaRequest);
            var categoriaId = await _categoriaRepository.InsertAsync(categoriaModel);
            categoriaModel.CategoriaId = categoriaId;

            var categoriaResponse = _mapper.Map<CategoriaResponseViewModel>(categoriaModel);
            return CreatedAtAction(nameof(Get), new { id = categoriaId }, categoriaResponse);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CategoriaRequestViewModel categoriaRequest)
        {
            var validationError = ValidateModelState();
            if (validationError != null)
            {
                return validationError;
            }

            var categoriaExistente = await _categoriaRepository.FindByIdAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound();
            }

            var categoriaModel = _mapper.Map(categoriaRequest, categoriaExistente);
            categoriaModel.CategoriaId = id; // Assegura que o ID não seja alterado

            await _categoriaRepository.UpdateAsync(categoriaModel);
            return NoContent();

        }
    }
}
