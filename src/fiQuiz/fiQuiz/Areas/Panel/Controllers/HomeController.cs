using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace fiQuiz.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(Roles = "Admin,Moderator,Questioner")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

#if DEBUG
        public async Task<IActionResult> TransferQuestions1()
        {
            using (MySqlConnection mySqlConnectionBU = new MySqlConnection("server=127.0.0.1;port=3306;database=bugenelvt;uid=root;password=3.mayis.1988"))
            {
                mySqlConnectionBU.Open();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(@"
                SELECT k.kategori_adi,s.soru,s.dogru_cevap,s.yanlis_cevap_1,s.yanlis_cevap_2,s.yanlis_cevap_3,s.ekleyen_kullanici_adi FROM bugenelvt.by_sorular as s
                right join bugenelvt.by_soru_kategori_bag as skb on s.no = skb.bag_soru_no
                right join bugenelvt.by_kategoriler as k on skb.bag_kategori_no = k.no", mySqlConnectionBU);
                DataTable questionDataTable = new DataTable();
                mySqlDataAdapter.Fill(questionDataTable);
                foreach (DataRow questionDataRow in questionDataTable.Rows)
                {
                    string questionCategoryName = questionDataRow["kategori_adi"] as string;
                    string questionText = questionDataRow["soru"] as string;
                    string questionCorrectAnswer = questionDataRow["dogru_cevap"] as string;
                    string questionWrongAnswer1 = questionDataRow["yanlis_cevap_1"] as string;
                    string questionWrongAnswer2 = questionDataRow["yanlis_cevap_2"] as string;
                    string questionWrongAnswer3 = questionDataRow["yanlis_cevap_3"] as string;
                    string questionCreatedBy = questionDataRow["ekleyen_kullanici_adi"] as string;

                    QuestionCategory questionCategory = await _context.QuestionCategories.FirstOrDefaultAsync(x =>
                         x.Name.Equals(questionCategoryName, StringComparison.OrdinalIgnoreCase));
                    if (questionCategory == null)
                    {
                        questionCategory = new QuestionCategory
                        {
                            Name = questionCategoryName
                        };
                        await _context.QuestionCategories.AddAsync(questionCategory);
                        await _context.SaveChangesAsync();
                    }
                    Question question = new Question
                    {
                        CategoryId = questionCategory.Id,
                        Category = questionCategory,
                        QuestionText = questionText,
                        CreatedBy = questionCreatedBy,
                        LastUpdatedBy = questionCreatedBy
                    };
                    await _context.Questions.AddAsync(question);
                    await _context.SaveChangesAsync();

                    await _context.QuestionAnswers.AddRangeAsync(new QuestionAnswer
                    {
                        QuestionId = question.Id,
                        Question = question,
                        CreatedBy = questionCreatedBy,
                        LastUpdatedBy = questionCreatedBy,
                        Answer = questionCorrectAnswer,
                        IsCorrect = true
                    }, new QuestionAnswer
                    {
                        QuestionId = question.Id,
                        Question = question,
                        CreatedBy = questionCreatedBy,
                        LastUpdatedBy = questionCreatedBy,
                        Answer = questionWrongAnswer1,
                        IsCorrect = false
                    }, new QuestionAnswer
                    {
                        QuestionId = question.Id,
                        Question = question,
                        CreatedBy = questionCreatedBy,
                        LastUpdatedBy = questionCreatedBy,
                        Answer = questionWrongAnswer2,
                        IsCorrect = false
                    }, new QuestionAnswer
                    {
                        QuestionId = question.Id,
                        Question = question,
                        CreatedBy = questionCreatedBy,
                        LastUpdatedBy = questionCreatedBy,
                        Answer = questionWrongAnswer3,
                        IsCorrect = false
                    });
                    await _context.SaveChangesAsync();

                }

            }
            return Content("OK");
        }
#endif
    }

}