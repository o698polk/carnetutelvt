using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;
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
using carnetutelvt.Controllers;
namespace carnetutelvt.Controllers
{
    public class UsertbsController : Controller
    {
        private readonly rgutelvtContext _context;
        private readonly rgutelvtContext _contextdeta;
        private const string rp0 = "0",rp1="1";
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _conter;
       
        
       
        public UsertbsController(rgutelvtContext context, rgutelvtContext contextdeta, IWebHostEnvironment env, IHttpContextAccessor conter)
        {
            _context = context;
            _env = env;
            _conter = conter;
            _contextdeta = contextdeta;
          

        }

        // GET: Usertbs
        public async Task<IActionResult> Index(string? Email)
        {
           
            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0 || _conter.HttpContext.Session.GetString("Rol")!="1")

            {

                return RedirectToAction(nameof(Login));

            }
            else
            {
				if (Email == null)
				{
					return View(await _context.Usertbs.ToListAsync());
                }
                else
                {
					return View(await _context.Usertbs.Where(u => u.Email.Contains(Email)).ToListAsync());
				}
				
            }
           
        }



		//Generar Codigo QR


        public void GenerarQr(string webtex)
        {
            QRCodeEncoder code = new QRCodeEncoder();
            Bitmap img = code.Encode(webtex);
            System.Drawing.Image Qr = (System.Drawing.Image)img;
            using (MemoryStream mem = new MemoryStream())
            {
                Qr.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imgbyte = mem.ToArray();
                ViewData["qr"] = "data:image/git;base64,"+Convert.ToBase64String(imgbyte);
            }
        }


        // GET: Usertbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0 || _conter.HttpContext.Session.GetString("Rol") != "1")

            {

                return RedirectToAction(nameof(Login));

            }
            else
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
        }
      

        // GET: Usertbs/Create
        public IActionResult Create()
        {

            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0 || _conter.HttpContext.Session.GetString("Rol") != "1")

            {

                return RedirectToAction(nameof(Login));

            }
            else
            {


                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (_conter.HttpContext.Session.GetInt32("Id") != null )

            {

                return RedirectToAction(nameof(Tablero));

            }
            else
            {

                ViewData["Error"] = "";
                return View();
            }
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

            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0)

            {

                return RedirectToAction(nameof(Login));

            }
            else
            {

                if (_conter.HttpContext.Session.GetInt32("Id") != null)

                {
                    var detallestb = new Detallestb();
                    if (_conter.HttpContext.Session.GetInt32("Id") > 0)
                    {
                        int ids = Convert.ToInt32(_conter.HttpContext.Session.GetInt32("Id"));

                        detallestb = _contextdeta.Detallestbs.Where(e => e.Iduser == ids).FirstOrDefault();
                        if (detallestb == null)
                        {
                            return NotFound();
                        }
                    }

                    ViewData["Iduser"] = new SelectList(_context.Usertbs, "Id", "Id", detallestb.Iduser);
                    ViewData["Datecreate"] = detallestb.Datecreate;
                    ViewData["Imgcarnet"] = detallestb.Imgcarnet;
                 
                    ViewData["Fullname"] = detallestb.Fullname;
                    ViewData["Surnames"] = detallestb.Surnames;
                  
                    ViewData["Specialty"] = detallestb.Specialty;
                    ViewData["Faculty"] = detallestb.Faculty;
                    ViewData["Ci"] = detallestb.Ci;
                    ViewData["email"] = _conter.HttpContext.Session.GetString("email");
                  //  GenerarQr("https://localhost:7288/Detallestbs/Estudiantes/"+detallestb.Iduser);
                    GenerarQr("http://polkdev.somee.com/Detallestbs/Estudiantes/"+detallestb.Iduser);
                    return View(detallestb);

                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
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
                    smtep.Host = "smtp.live.com";
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
        public async Task<IActionResult> Register([Bind("Email,Passwords")] Usertb usertb)
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

                    Random rnd = new Random();
                    long ran = rnd.Next(5000870, 8000008);
                    usertb.Passwords = EncrypData(usertb.Passwords);
                    usertb.Datecreate = DateTime.Now;
                    usertb.Dateupdate = DateTime.Now;
                    usertb.Numberverify = ran.ToString();
                    usertb.Verifyuser = 0;
                    usertb.Rol = "0";
                    Detallestb dll = new Detallestb();
                    _context.Add(usertb);

                    await _context.SaveChangesAsync();
                    var usernw = _context.Usertbs.Where(u => u.Email == usertb.Email).FirstOrDefault();
                    if (usernw != null)
                    {
                        dll.Iduser = usernw.Id;
                        dll.Datecreate = DateTime.Now;
                        dll.Dateupdate = DateTime.Now;
                        _contextdeta.Add(dll);
                        await _contextdeta.SaveChangesAsync();
                    }
                    ViewData["Error"] = rp1;
                    return View();
                    //  return RedirectToAction(nameof(Index));
                }
            }
            return View(usertb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Passwords,Rol")] Usertb usertb)
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



                    Random rnd = new Random();
                    long ran = rnd.Next(5000870,8000008);
                    usertb.Passwords = EncrypData(usertb.Passwords);
                    usertb.Datecreate = DateTime.Now;
                    usertb.Dateupdate = DateTime.Now;
                    usertb.Numberverify = ran.ToString();
                    usertb.Verifyuser = 1;
                    Detallestb dll = new Detallestb();
                   
                    _context.Add(usertb);
                    await _context.SaveChangesAsync();

                    var usernw = _context.Usertbs.Where(u => u.Email == usertb.Email).FirstOrDefault();
                    if (usernw != null)
                    {
                        dll.Iduser = usernw.Id;
                        dll.Datecreate = DateTime.Now;
                        dll.Dateupdate = DateTime.Now;
                        _contextdeta.Add(dll);
                        await _contextdeta.SaveChangesAsync();
                    }
                   
                    ViewData["Error"] = rp1;
                    //return View();
                     return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Usertbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0 || _conter.HttpContext.Session.GetString("Rol") != "1")

            {

                return RedirectToAction(nameof(Login));

            }
            else
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
                ViewData["Datecreate"] = usertb.Datecreate;
                return View(usertb);
            }
        }

        // POST: Usertbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Passwords,Verifyuser,Numberverify,Rol,Datecreate")] Usertb usertb)
        {
            
            if (id != usertb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usertb.Passwords = EncrypData(usertb.Passwords);
                    usertb.Dateupdate = DateTime.Now;
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
                ViewData["Error"] = rp0;
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
                        ViewData["Error"] = rp0;
                        return View();
                    }
                    else
                    {
                        string[] cadena = user.Email.Split("@");
                        _conter.HttpContext.Session.SetString("name", cadena[0]);
                        _conter.HttpContext.Session.SetString("email", user.Email);
                        _conter.HttpContext.Session.SetInt32("Id", user.Id);
                        _conter.HttpContext.Session.SetString("Rol", user.Rol);
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
            if (_conter.HttpContext.Session.GetInt32("Id") == null || _conter.HttpContext.Session.GetInt32("Id") < 0 || _conter.HttpContext.Session.GetString("Rol") != "1")

            {

                return RedirectToAction(nameof(Login));

            }
            else
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
            var detallestb =  _contextdeta.Detallestbs.Where(e=> e.Iduser==id).FirstOrDefault();
            if (detallestb != null)
            {
                _contextdeta.Detallestbs.Remove(detallestb);
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
