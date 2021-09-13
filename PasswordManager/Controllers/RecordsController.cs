using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PasswordManager.Controllers
{
    public class RecordsController : Controller
    {
        private readonly PasswordManagerContext _context;

        public RecordsController(PasswordManagerContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Records
        public async Task<IActionResult> Index()
        {
            var currentUser = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();
            return View(await _context.Record.Where(r => r.User == currentUser).ToListAsync());
        }

        [Authorize]
        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();

            var @record = await _context.Record
                .Where(r => r.User == currentUser)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // GET: Records/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,RecordName,UserName,Password,User")] RecordForm @recordForm)
        {
            if (ModelState.IsValid)
            {
                var record = new Record
                {
                    RecordName = @recordForm.RecordName,
                    UserName = @recordForm.UserName,
                    Password = @recordForm.Password
                };
                record.User = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();

                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Model state validation failed!");
            }
            return View(@recordForm);
        }

        // GET: Records/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();

            //var @record = await _context.Record.FindAsync(id);

            var @record = await _context.Record
                .Where(r => r.User == currentUser)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (@record == null)
            {
                return NotFound();
            }

            var form = new RecordForm
            {
                Id = @record.Id,
                RecordName = record.RecordName,
                UserName = @record.UserName,
                Password = record.Password
            };

            return View(form);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordName,UserName,Password")] RecordForm @recordForm)
        {
            if (id != @recordForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var record = await _context.Record.Where(r => r.Id == @recordForm.Id).FirstOrDefaultAsync();
                    record.UserName = recordForm.UserName;
                    record.RecordName = recordForm.RecordName;
                    record.Password = recordForm.Password;
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@recordForm.Id))
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
            return View(@recordForm);
        }

        // GET: Records/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();

            var @record = await _context.Record
                .Where(r => r.User == currentUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = _context.User.Where(u => u.LoginName == User.Identity.Name).FirstOrDefault();
            var @record = await _context.Record
                .Where(r => r.User == currentUser)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
            _context.Record.Remove(@record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.Id == id);
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
    }
}
