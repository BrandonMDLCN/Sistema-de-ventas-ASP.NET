using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _categoriaServicio;
        public CategoriaController(IMapper mapper, ICategoriaService categoriaServicio)
        {
            _mapper = mapper;
            _categoriaServicio = categoriaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCategoria> vmCategoriaLista = _mapper.Map<List<VMCategoria>>(await _categoriaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK,new {data = vmCategoriaLista});
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMCategoria modelo)
        {
            GenericResponse<VMCategoria> gResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoriaCreada = await _categoriaServicio.Crear(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoriaCreada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch(Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK,gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMCategoria modelo)
        {
            GenericResponse<VMCategoria> gResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoriaEditada = await _categoriaServicio.Editar(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoriaEditada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idCategoria)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _categoriaServicio.Eliminar(idCategoria);
            }
            catch(Exception ex)
            {
                gResponse.Estado = true;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
