using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using carnetutelvt.Models;
using System.Security.Cryptography;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;

namespace carnetutelvt.Controllers
{
    public class UsertbsController : Controller
    {
        private readonly rgutelvtContext _context;
        private const string rp0 = "0",rp1="1";
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _conter;
        public UsertbsController(rgutelvtContext context, IWebHostEnvironment env, IHttpContextAccessor conter)
        {
            _context = context;
            _env = env;
            _conter = conter;
        }

        // GET: Usertbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usertbs.ToListAsync());
        }

        // GET: Usertbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usertbs == null)
            {
                return NotFound();
            }

            var usertb = await _context.Usertbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usertb == null)
            {
                return NotFound();
            }

            return View(usertb);
        }

        // GET: Usertbs/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login()
        {
            ViewData["Error"] = "";
            return View();
        }
        public dynamic CorreoEnviar()
        {
            return EnviarCorreo("orsicen@gmail.com", "Verificar Funcion", "Si estan llegando los correo");
          
                
        }
        public IActionResult Logout()
        {

            _conter.HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        public IActionResult Tablero()
        {
            if (_conter.HttpContext.Session.GetInt32("Id") > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }
        // Encriptar datos
        public string EncrypData(string pass)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(pass);
            HashAlgorithm algorithm = CryptoConfig.CreateFromName("SHA256") as HashAlgorithm;
            byte[] hash = algorithm.ComputeHash(passwordBytes);

            return BitConverter.ToString(hash).Replace("-", "");
        }

        // Enviar Correos

        public dynamic EnviarCorreo(string para,string asunto,string bodyst)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("utelvtcarnet@gmail.com");
                mailMessage.To.Add(para);
                mailMessage.Subject=asunto;
                mailMessage.Body = bodyst;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                // por si necesitas almacenar archivos en carpetas
                string rutaArchivo = Path.Combine(_env.ContentRootPath, "../Temporal", "");

                // configuracion de SMTP carnetel123
                SmtpClient smtep = new SmtpClient();
                    smtep.Host = "smtp.gamil.com";
                    smtep.Port = 25;
                    smtep.EnableSsl = true;
                    smtep.UseDefaultCredentials = true;
                    string correodefaul = "utelvtcarnet@gmail.com";
                    string clave = "carnetel123";

                smtep.Credentials = new NetworkCredential(correodefaul, clave);
                smtep.Send(mailMessage);
            }
            catch (Exception ex)
            {
                return new
                {
                    response = "error: " + ex
                };
            }



            return new
            {
                response = "enviado"
            };
        }

        // POST: Usertbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Passwords")] Usertb usertb)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Usertbs.Where(u => u.Email == usertb.Email).FirstOrDefault();
                if (user != null)
                {
                    ViewData["Error"] = rp0;
                    return View();
                }
                else
                {

                    usertb.Passwords = EncrypData(usertb.Passwords);

                    _context.Add(usertb);
                    await _context.SaveChangesAsync();

                    ViewData["Error"] = rp1;
                    return View();
                    //  return RedirectToAction(nameof(Index));
                }
            }
            return View(usertb);
        }

        // GET: Usertbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usertbs == null)
            {
                return NotFound();
            }

            var usertb = await _context.Usertbs.FindAsync(id);
            if (usertb == null)
            {
                return NotFound();
            }
            return View(usertb);
        }

        // POST: Usertbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Passwords,Dateupdate,Datecreate,Numberverify,Verifyuser")] Usertb usertb)
        {
            if (id != usertb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usertb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsertbExists(usertb.Id))
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
            return View(usertb);
        }


        //lOGIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Passwords")] Usertb userdt)
        {
            if (userdt.Email == null || userdt.Passwords == null)
            {
                ViewData["Error"] = "Vacio todo";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userdt.Passwords = EncrypData(userdt.Passwords);
                    var user = _context.Usertbs.Where(u => u.Email == userdt.Email && u.Passwords == userdt.Passwords).FirstOrDefault();
                    if (user == null)
                    {
                        ViewData["Error"] = "Datos Incorrectos";
                        return View();
                    }
                    else
                    {
                        string[] cadena = user.Email.Split("@");
                        _conter.HttpContext.Session.SetString("name", cadena[0]);
                        _conter.HttpContext.Session.SetString("email", user.Email);
                        _conter.HttpContext.Session.SetInt32("Id", user.Id);

                        return RedirectToAction(nameof(Tablero));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserdtExists(userdt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(userdt);
        }
        private bool UserdtExists(int id)
        {
            return _context.Usertbs.Any(e => e.Id == id);
        }
        // GET: Usertbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usertbs == null)
            {
                return NotFound();
            }

            var usertb = await _context.Usertbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usertb == null)
            {
                return NotFound();
            }

            return View(usertb);
        }

        // POST: Usertbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usertbs == null)
            {
                return Problem("Entity set 'rgutelvtContext.Usertbs'  is null.");
            }
            var usertb = await _context.Usertbs.FindAsync(id);
            if (usertb != null)
            {
                _context.Usertbs.Remove(usertb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsertbExists(int id)
        {
          return _context.Usertbs.Any(e => e.Id == id);
        }
    }
}
