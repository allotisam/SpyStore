using SpyStore.MVC.ViewModels.Base;
using SpyStore.MVC.Validations;

namespace SpyStore.MVC.ViewModels
{
    public class AddToCartViewModel : CartViewModelBase
    {
        [MustBeGreaterThanZero]
        [MustNotBeGreaterThan(nameof(UnitsInStock))]
        public int Quantity { get; set; }
    }
}
