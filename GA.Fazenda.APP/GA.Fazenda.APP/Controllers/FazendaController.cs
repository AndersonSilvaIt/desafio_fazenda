using AutoMapper;
using GA.Fazenda.APP.Models;
using GA.Fazenda.APP.ViewModels;
using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Interfaces.Servicos;
using GA.Fazenda.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.APP.Controllers
{
    public class FazendaController : BaseController
    {
        private readonly IFazendaRepository _fazendaRepository;
        private readonly IFazendaService _fazendaService;
        private readonly IMapper _mapper;
        private readonly int pageSize = 4;

        public FazendaController(IFazendaRepository fazendaRepository,
                                    IFazendaService fazendaService,
                                 IMapper mapper,
                                        INotificador notificador) : base(notificador)
        {
            _fazendaRepository = fazendaRepository;
            _fazendaService = fazendaService;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Search(int? pageNumber, string filterCode)
        //{
        //    ViewData["FilterCode"] = filterCode;
        //
        //    var lista = _mapper.Map<IEnumerable<FazendaVM>>(await _refugoService.Search(filterCode));
        //    return View(PaginatedList<FazendaVM>.Create(lista.AsQueryable(), pageNumber ?? 1, pageSize));
        //}

        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewData["FilterCode"] = "";
            var fazendas = _mapper.Map<IEnumerable<FazendaVM>>(await _fazendaRepository.ObterTodos());

            return View(PaginatedList<FazendaVM>.Create(fazendas.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FazendaVM fazendaVM)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid) return View(fazendaVM);

            var fazenda = _mapper.Map<EntidadeFazenda>(fazendaVM);

            await _fazendaService.Adicionar(fazenda);
            
            if (!OperacaoValida()) return View(fazendaVM);

            if (!await _fazendaService.Commited())
            {
                NotificarErro("Erro ao persistir os dados no banco de dados.");
                return View(fazendaVM);
            }

            var url = Url.Action("Index", "Fazenda");
            return Json(new { success = true, url });
        }


        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var fazendaVM = _mapper.Map<FazendaVM>(await _fazendaRepository.ObterPorId(id));

            return View("Edit", fazendaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FazendaVM fazendaVM)
        {
            if (!ModelState.IsValid) return View(fazendaVM);

            await _fazendaService.Atualizar(_mapper.Map<EntidadeFazenda>(fazendaVM));

            if (!OperacaoValida()) return View(fazendaVM);

            await _fazendaService.Commited();

            var url = Url.Action("Index", "Fazenda");
            return Json(new { success = true, url });
        }

        public async Task<IActionResult> Details(int id)
        {
            var fazendaVM = _mapper.Map<FazendaVM>(await _fazendaRepository.ObterPorId(id));

            return View("Details", fazendaVM);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int id)
        {
            var fazendaVM = _mapper.Map<FazendaVM>(await _fazendaRepository.ObterPorId(id));

            return View("Delete", fazendaVM);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirma(int Id)
        {
            _fazendaService.Remover(Id);

            if (!OperacaoValida()) return NotFound();

            await _fazendaService.Commited();

            var url = Url.Action("Index", "Fazenda");
            return Json(new { success = true, url });
        }
    }
}
