using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoDonate.Helpers
{
    public class AutorizacijaAtributte : TypeFilterAttribute
    {
        public AutorizacijaAtributte(bool korisnik, bool administrator) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }

        public class MyAuthorizeImpl : IActionFilter {
            private readonly bool _korisnik;
            private readonly bool _administrator;

            public MyAuthorizeImpl(bool korisnik, bool administrator)
            {
                this._korisnik = korisnik;
                this._administrator = administrator;
            }
            public void OnActionExecuted (ActionExecutedContext actionExecutedContext)
            {

            }
            public void OnActionExecuting (ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.GetLoginInfo().isLogiran)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
                KretanjePoSistemu.Save(filterContext.HttpContext);
                if (filterContext.HttpContext.GetLoginInfo().isLogiran)
                {
                    return;
                }
                filterContext.Result=new UnauthorizedResult();  
            }
        }

    }
}
