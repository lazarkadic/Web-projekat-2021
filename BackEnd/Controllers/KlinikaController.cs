using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackEnd.Controllers
{
    //[EnableCors("CORS")]
    [ApiController]
    [Route("[controller]")]
    public class KlinikaController : ControllerBase
    {
        public KlinikaContext Context { get; set; }
        public KlinikaController(KlinikaContext context)
        {
            Context = context;
        }

        #region Klinika

        [Route("PreuzmiKlinike")]
        [HttpGet]
        public async Task<List<Klinika>> PreuzmiKlinike()
        {
            return await Context.Klinike.Include(p => p.Sobe).ToListAsync();
        }

        [Route("KreirajKliniku")]
        [HttpPost]
        public async Task KreirajKliniku([FromBody] Klinika klinika)
        {
            Context.Klinike.Add(klinika);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniKliniku")]
        [HttpPut]
        public async Task IzmeniKliniku([FromBody] Klinika klinika)
        {
            Context.Update<Klinika>(klinika);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiKliniku/{id}")]
        [HttpDelete]
        public async Task IzbrisiKliniku(int id)
        {
            Context.Remove<Klinika>(Context.Find<Klinika>(id));
            await Context.SaveChangesAsync();
        }

        #endregion

        #region Soba

        [Route("PreuzmiSobe/{id}")]
        [HttpGet]
        public async Task<List<Soba>> PreuzmiSobe(int id)
        {
            var listKlinika = await Context.Klinike.Include(p => p.Sobe).ToListAsync();
            Klinika klinika = null;
            listKlinika.ForEach(k => 
            {
                if(k.ID == id)
                    klinika = k;
            });
            return klinika.Sobe;
        }

        [Route("PreuzmiSveSobe")]
        [HttpGet]
        public async Task<List<Soba>> PreuzmiSveSobe()
        {
            return await Context.Sobe.ToListAsync();
        }


        [Route("KreirajSobu/{id}")]
        [HttpPost]
        public async Task<IActionResult> KreirajSobu(int id, [FromBody] Soba soba)
        {
            //var klinika = await Context.Klinike.FindAsync(id);
            var listKlinika = await Context.Klinike.Include(p => p.Sobe).ToListAsync();
            Klinika klinika = null;
            listKlinika.ForEach(k => 
            {
                if(k.ID == id)
                    klinika = k;
            });
            if(klinika.BrojSoba.CompareTo(klinika.Sobe.Count()) > 0 && soba.BrKreveta != 0 && soba.BrSobe != 0)
            {
                soba.PripadaKlinici = klinika;
                Context.Sobe.Add(soba);
                await Context.SaveChangesAsync();

                return Ok(soba);
                //return "Uspesno dodavanje, " + "trenutno ima: " + klinika.Sobe.Count() + " soba.";
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("IzmeniSobu")]
        [HttpPut]
        public async Task IzmeniSobu([FromBody] Soba soba)
        {
            Context.Update<Soba>(soba);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiSobu/{id}")]
        [HttpPost]
        public async Task IzbrisiSobu(int id)
        {
            Context.Remove<Soba>(Context.Find<Soba>(id));
            await Context.SaveChangesAsync();
        }

        #endregion
        
        #region Pacijent


        [Route("PreuzmiPacijente/{id}")]
        [HttpGet]
        public async Task<List<Pacijent>> PreuzmiPacijente(int id)
        {
            var listSoba = await Context.Sobe.Include(p => p.ListaPacijenata).ToListAsync();
            Soba soba = null;
            listSoba.ForEach(s => 
            {
                if(s.ID == id)
                    soba = s;
            });
            return soba.ListaPacijenata;
        }

        [Route("PreuzmiSvepacijente")]
        [HttpGet]
        public async Task<List<Pacijent>> PreuzmiSvepacijente()
        {
            return await Context.Pacijenti.ToListAsync();
        }


        [Route("KreirajPacijenta/{id}")]
        [HttpPost]
        public async Task<IActionResult> KreirajPacijenta(int id, [FromBody] Pacijent pacijent)
        {
            //var klinika = await Context.Klinike.FindAsync(id);
            var listSoba = await Context.Sobe.Include(p => p.ListaPacijenata).ToListAsync();
            Soba soba = null;
            listSoba.ForEach(s => 
            {
                if(s.ID == id)
                    soba = s;
            });
            if(soba.BrKreveta.CompareTo(soba.ListaPacijenata.Count()) > 0 && pacijent.Ime != "" && pacijent.Prezime != "" && pacijent.JMBG != "" && pacijent.Bolest != "" && pacijent.Stanje != "")
            {
                pacijent.BrojSobe = soba;
                Context.Pacijenti.Add(pacijent);
                await Context.SaveChangesAsync();

                return Ok(pacijent);
                //return "Uspesno dodavanje " + pacijent.Ime + ", trenutno ima: " + soba.ListaPacijenata.Count() + " pacijenata.";
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("IzmeniPacijenta")]
        [HttpPut]
        public async Task IzmeniPacijenta([FromBody] Pacijent pacijent)
        {
            Context.Update<Pacijent>(pacijent);
            await Context.SaveChangesAsync();
        }


        // Imam problema sa CORS-om i antivirusom na racunaru, i ne dozvoljava mi DELETE metod
        [Route("IzbrisiPacijenta/{id}")]
        [HttpPost]
        public async Task IzbrisiPacijenta(int id)
        {
            Context.Remove<Pacijent>(Context.Find<Pacijent>(id));
            await Context.SaveChangesAsync();
        }

        #endregion

    }
}