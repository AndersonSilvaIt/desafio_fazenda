using AutoMapper;
using GA.Fazenda.APP.Models;
using GA.Fazenda.APP.ViewModels;
using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Interfaces.Servicos;
using GA.Fazenda.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GA.Fazenda.APP.Controllers
{
    public class CadastroListaAnimalController : BaseController
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IFazendaRepository _fazendaRepository;
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;
        private readonly int pageSize = 4;

        public CadastroListaAnimalController(IAnimalRepository animalRepository, IFazendaRepository fazendaRepository,
                                    IAnimalService animalService,
                                 IMapper mapper,
                                        INotificador notificador) : base(notificador)
        {
            _animalRepository = animalRepository;
            _fazendaRepository = fazendaRepository;
            _animalService = animalService;
            _mapper = mapper;
        }

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

        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewData["FilterTag"] = "";
            var animals = _mapper.Map<IEnumerable<AnimalVM>>(await _animalRepository.ObterListaAnimaisComFazendas());

            return View(PaginatedList<AnimalVM>.Create(animals.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            CadastroListAnimal animalVM = await PopularFazendasAnimalList(new CadastroListAnimal());
            return View(animalVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CadastroListAnimal animalVM)
        {
            PreencherModel(ref animalVM);

            animalVM = await PopularFazendasAnimalList(animalVM);
            ModelState.Remove("Id");
            
            if (!ModelState.IsValid) return View(animalVM);

            AnimalVM animalAux;

            foreach (var item in animalVM.Animais)
            {
                animalAux = new AnimalVM { Tag = item.Tag, FazendaId = animalVM.FazendaId};

                var animalInsert = _mapper.Map<Animal>(animalAux);
                await _animalService.Adicionar(animalInsert);

                if (!OperacaoValida())  return View(animalVM); 
            }

            if (!await _animalService.Commited())
            {
                NotificarErro("Erro ao persistir os dados no banco de dados.");
                return View(animalVM);
            }

            var url = Url.Action("Index", "CadastroListaAnimal");
            return Json(new { success = true, url });
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterPorId(id));
            animalVM = await PopularFazendas(animalVM);

            return View("Edit", animalVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnimalVM animalVM)
        {
            animalVM = await PopularFazendas(animalVM);
            if (!ModelState.IsValid) return View(animalVM);

            await _animalService.Atualizar(_mapper.Map<Animal>(animalVM));

            if (!OperacaoValida()) return View(animalVM);

            await _animalService.Commited();

            var url = Url.Action("Index", "CadastroListaAnimal");
            return Json(new { success = true, url });
        }

        public async Task<IActionResult> Details(int id)
        {
            var animalVM = _mapper.Map<AnimalVM>(await _animalRepository.ObterAnimalComFazenda(id));

            return View("Details", animalVM);
        }

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

            var url = Url.Action("Index", "CadastroListaAnimal");
            return Json(new { success = true, url });
        }
        private async Task<CadastroListAnimal> PopularFazendasAnimalList(CadastroListAnimal animal)
        {
            animal.Fazendas = _mapper.Map<IEnumerable<FazendaVM>>(await _fazendaRepository.ObterTodos());
            return animal;
        }

        private async Task<AnimalVM> PopularFazendas(AnimalVM animal)
        {
            animal.Fazendas = _mapper.Map<IEnumerable<FazendaVM>>(await _fazendaRepository.ObterTodos());
            return animal;
        }

        private void PreencherModel(ref CadastroListAnimal animalListVM)
        {
            if (!string.IsNullOrWhiteSpace(animalListVM.ListAnimaisJson))
            {
                string[] animais = JsonConvert.DeserializeObject<string[]>(animalListVM.ListAnimaisJson);
                if (animais.Count() > 0)
                {
                    animalListVM.Animais = new List<AnimalVM>();

                    foreach (var item in animais)
                        animalListVM.Animais.Add(new AnimalVM { Tag = item });
                }
            }
        }
    }
}
