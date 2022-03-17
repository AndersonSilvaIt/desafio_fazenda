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
    public class AnimalController : BaseController
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IFazendaRepository _fazendaRepository;
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;
        private readonly int pageSize = 4;

        public AnimalController(IAnimalRepository animalRepository, IFazendaRepository fazendaRepository,
                                    IAnimalService animalService, IMapper mapper,
                                        INotificador notificador) : base(notificador)
        {
            _animalRepository = animalRepository;
            _fazendaRepository = fazendaRepository;
            _animalService = animalService;
            _mapper = mapper;
        }

        [Route("filtro-de-animais")]
        public async Task<IActionResult> Search(int? pageNumber, string filterTag, string filterFazendaId)
        {
            ViewData["FilterTag"] = filterTag;
            ViewData["FilterFazendaId"] = filterFazendaId;

            IEnumerable<AnimalVM> lista;

            int fazendaId = 0;
            int.TryParse(filterFazendaId, out fazendaId);

            lista = _mapper.Map<IEnumerable<AnimalVM>>(await _animalRepository.ObterListaAnimaisComFazendasPorFiltro(filterTag, fazendaId));

            return View("Index", PaginatedList<AnimalVM>.Create(lista.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [Route("lista-de-animais")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var animals = _mapper.Map<IEnumerable<AnimalVM>>(await _animalRepository.ObterListaAnimaisComFazendas());

            return View(PaginatedList<AnimalVM>.Create(animals.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [Route("novo-animal")]
        public async Task<IActionResult> Create()
        {
            AnimalVM animalVM = await PopularFazendas(new AnimalVM());
            return View(animalVM);
        }

        [Route("novo-animal")]
        [HttpPost]
        public async Task<IActionResult> Create(AnimalVM animalVM)
        {
            animalVM = await PopularFazendas(animalVM);
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
                return View(animalVM);

            var animal = _mapper.Map<Animal>(animalVM);

            await _animalService.Adicionar(animal);

            if (!OperacaoValida()) return View(animalVM);

            if (!await _animalService.Commited())
            {
                NotificarErro("Erro ao persistir os dados no banco de dados.");
                return View(animalVM);
            }

            var url = Url.Action("Index", "Animal");
            return Json(new { success = true, url });
        }

        [Route("editar-animal/{id:int}")]
        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterPorId(id));
            animalVM = await PopularFazendas(animalVM);

            return View("Edit", animalVM);
        }

        [Route("editar-animal/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnimalVM animalVM)
        {
            animalVM = await PopularFazendas(animalVM);
            if (!ModelState.IsValid) return View(animalVM);

            await _animalService.Atualizar(_mapper.Map<Animal>(animalVM));

            if (!OperacaoValida()) return View(animalVM);

            await _animalService.Commited();

            var url = Url.Action("Index", "Animal");
            return Json(new { success = true, url });
        }

        [Route("detalhes-animal/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterAnimalComFazenda(id));

            return View("Details", animalVM);
        }

        [Route("excluir-animal/{id:int}")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterAnimalComFazenda(id));

            return View("Delete", animalVM);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirma(int Id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterPorId(Id));

            _animalService.Remover(Id);

            if (!OperacaoValida()) return View("Delete", animalVM);

            await _animalService.Commited();

            var url = Url.Action("Index", "Animal");
            return Json(new { success = true, url });
        }
        private async Task<AnimalVM> PopularFazendas(AnimalVM animal)
        {
            animal.Fazendas = _mapper.Map<IEnumerable<FazendaVM>>(await _fazendaRepository.ObterTodos());
            return animal;
        }
    }
}
