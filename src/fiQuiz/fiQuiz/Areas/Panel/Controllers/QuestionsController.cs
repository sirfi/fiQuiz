using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Areas.Panel.Models.ViewModels;
using fiQuiz.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fiQuiz.Models;
using Microsoft.AspNetCore.Authorization;

namespace fiQuiz.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(Roles = "Admin,Moderator,Questioner")]
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            IQueryable<Question> questionsQueryable = _context.Questions.Include(q => q.Category);
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                questionsQueryable = questionsQueryable.Where(x => x.CreatedBy == User.FindFirst("UserId").Value);
            return View(await questionsQueryable.OrderByDescending(x => x.CreatedAt).ToPaginatedAsync(pageIndex, 20));
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionText,CategoryId,Id")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.ApprovalStatus = User.IsInRole("Admin") || User.IsInRole("Moderator")
                    ? QuestionApprovalStatus.Approved
                    : QuestionApprovalStatus.Waiting;
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        public IActionResult CreateWithAnswers(int answerCount)
        {
            if (answerCount < 4) return BadRequest();
            Question question = new Question
            {
                Answers = Enumerable.Range(0, answerCount).Select(x => new QuestionAnswer() { IsCorrect = x == 0 }).ToList()
            };
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name");
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithAnswers(Question question)
        {
            if (ModelState.IsValid)
            {
                question.ApprovalStatus = User.IsInRole("Admin") || User.IsInRole("Moderator")
                    ? QuestionApprovalStatus.Approved
                    : QuestionApprovalStatus.Waiting;
                _context.Questions.Add(question);
                foreach (var questionAnswer in question.Answers)
                {
                    questionAnswer.Question = question;
                    _context.QuestionAnswers.Add(questionAnswer);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator") &&
                question.CreatedBy == User.FindFirst("UserId").Value)
            {
                return Unauthorized();
            }
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionText,CategoryId,Id")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Questions.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.QuestionCategories, "Id", "Name", question.CategoryId);
            return View(question);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator") &&
                question.CreatedBy == User.FindFirst("UserId").Value)
            {
                return Unauthorized();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> SetApprovalStatus([Bind("Id,ApprovalStatus")] SetApprovalStatusViewModel setApprovalStatusViewModel)
        {
            if (setApprovalStatusViewModel == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(setApprovalStatusViewModel.Id);
            if (question == null)
            {
                return NotFound();
            }

            question.ApprovalStatus = setApprovalStatusViewModel.ApprovalStatus;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "QuestionAnswers", new { questionId = question.Id });
        }
    }
}
