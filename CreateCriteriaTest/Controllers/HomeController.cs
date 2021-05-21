using CreateCriteriaTest.Utils;
using CreateCriteriaTest.Utils.Criterias;
using System;
using System.Web.Mvc;

namespace CreateCriteriaTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDatabase _database;

        public HomeController()
        {
            _database = new DatabaseQuery();
        }

        // GET: Home
        public ActionResult Index()
        {
            var data = _database
                .Query("SELECT * FROM tb_usuarios")
                .Where(CreateCriteria(new QueryFields { Value = "Teste" }))
                .OrderBy("Id", "desc")
                .Build();

            var proc = _database
                .Query($"exec spInsereAtualizaCadastroCliente {654856594}, '{"teste@teste.com.br"}', '{"12/01/2021"}'")
                .Build();

            ViewBag.Query = data;
            ViewBag.Procedure = proc;

            return View();
        }

        private Action<Criteria> CreateCriteria(QueryFields query) =>
            criteria =>
            {
                criteria.Eq("meu_campo", $"'{query.Value}'")
                .Eq("campo1", "23")
                .Eq("status", "1");
            };
    }

    public class QueryFields
    {
        public string Value { get; set; }
    }        
}