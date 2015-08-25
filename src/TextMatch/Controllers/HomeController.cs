namespace TextMatch.Controllers
{
    using System;
    using System.Web.Mvc;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private ITextMatchService _textMatchService;

        public HomeController()
        {
            
        }

        public HomeController(ITextMatchService textMatchService)
        {
            if (textMatchService == null) 
                throw new ArgumentNullException("textMatchService");

            _textMatchService = textMatchService;
        }

        protected ITextMatchService TextMatchService
        {
            get
            {
                _textMatchService = _textMatchService ?? new TextMatchService();
                return _textMatchService;
            }
            private set { _textMatchService = value; }
        }

        //
        // GET: /Index
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TextMatchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Output = TextMatchService.FindMatches(
                model.Text ?? string.Empty, 
                model.SubText ?? string.Empty);

            return View(model);
        }
    }
}