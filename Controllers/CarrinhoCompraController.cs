using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Lanches.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSeleciado = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == lancheId);

            if(lancheSeleciado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSeleciado);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public RedirectToActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            var lancheSeleciado = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == lancheId);

            if(lancheSeleciado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSeleciado);
            }

            return RedirectToAction("Index");
        }
    }
}