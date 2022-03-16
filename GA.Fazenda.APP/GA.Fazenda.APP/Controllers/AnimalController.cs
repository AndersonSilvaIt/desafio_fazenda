using AutoMapper;
using GA.Fazenda.APP.ViewModels;
using GA.Fazenda.Business.Interfaces;
using GA.Fazenda.Business.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GA.Fazenda.APP.Controllers
{
    public class AnimalController : BaseController
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;

        public AnimalController(IAnimalRepository animalRepository,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _animalRepository = animalRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var animais = _mapper.Map<IEnumerable<AnimalVM>>(await _animalRepository.ObterTodos());

            return View("Index", animais);
        }
    }
}
