using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fiQuiz.Models;
using Microsoft.AspNetCore.Authorization;

namespace fiQuiz.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(Roles = "Admin,Moderator,Questioner")]
    public class QuestionAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? questionId)
        {
            if (questionId == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["Question"] = question;
            IQueryable<QuestionAnswer> questionAnswersQueryable = _context.QuestionAnswers.Where(x => x.QuestionId == questionId);
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                questionAnswersQueryable = questionAnswersQueryable.Where(x => x.CreatedBy == User.FindFirst("UserId").Value);

            return View(await questionAnswersQueryable.ToListAsync());
        }

        public async Task<IActionResult> Create(int? questionId)
        {
            if (questionId == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["Question"] = question;
            QuestionAnswer questionAnswer = new QuestionAnswer
            {
                QuestionId = question.Id
            };
            return View(questionAnswer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer,IsCorrect,QuestionId,Id")] QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.QuestionAnswers.Add(questionAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { questionId = questionAnswer.QuestionId });
            }
            var question = await _context.Questions.FindAsync(questionAnswer.QuestionId);
            ViewData["Question"] = question;
            return View(questionAnswer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(questionAnswer.QuestionId);
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator") &&
                question.CreatedBy == User.FindFirst("UserId").Value)
            {
                return Unauthorized();
            }

            ViewData["Question"] = question;
            return View(questionAnswer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Answer,IsCorrect,QuestionId,Id")] QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.QuestionAnswers.Update(questionAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionAnswerExists(questionAnswer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { questionId = questionAnswer.QuestionId });
            }
            var question = await _context.Questions.FindAsync(questionAnswer.QuestionId);
            ViewData["Question"] = question;
            return View(questionAnswer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswers
                .Include(q => q.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(questionAnswer.QuestionId);
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator") &&
                question.CreatedBy == User.FindFirst("UserId").Value)
            {
                return Unauthorized();
            }

            _context.QuestionAnswers.Remove(questionAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { questionId = question.Id });
        }

        private bool QuestionAnswerExists(int id)
        {
            return _context.QuestionAnswers.Any(e => e.Id == id);
        }
    }
}
