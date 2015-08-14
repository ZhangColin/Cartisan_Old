using System;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Views {
    public class CartisanViewPage<TModel>: WebViewPage<TModel> {
        public override void Execute() {
            throw new NotImplementedException();
        }
    }

    public class CartisanViewPage: CartisanViewPage<dynamic> { }
}