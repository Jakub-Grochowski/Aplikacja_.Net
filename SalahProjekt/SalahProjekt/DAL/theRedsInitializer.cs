using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SalahProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SalahProjekt.DAL
{

    public class TheRedsInitializer:DropCreateDatabaseIfModelChanges<TheRedsContext>
    {
        protected override void Seed(TheRedsContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));


            var users = new List<Profile>
            {
                new Profile{Login="admin@gmail.com"},
                new Profile{Login="Grochowski@gmail.com"}
            };

            var user = new ApplicationUser { UserName = "admin@gmail.com" };
            string passwor = "Admin123.";
            userManager.Create(user, passwor);
            userManager.AddToRole(user.Id, "Admin");

            user = new ApplicationUser { UserName = "Grochowski@gmail.com" };
            passwor = "Grochowa123.";
            userManager.Create(user, passwor);
            userManager.AddToRole(user.Id, "User");


            users.ForEach(p => context.Profiles.Add(p));
            context.SaveChanges();

            var druzyny = new List<Druzyna>
            {
                new Druzyna {Nazwa_Druzyny="Liverpool FC"},
                new Druzyna {Nazwa_Druzyny="Tottenham"},
                new Druzyna {Nazwa_Druzyny="Ajax Amsterdam"},
                new Druzyna {Nazwa_Druzyny="FC Barcelona"}
            };
            druzyny.ForEach(d => context.Druzyny.Add(d));
            context.SaveChanges();

            var mecze = new List<Mecz>
            {
                new Mecz{druzyna1=druzyny[0],druzyna2=druzyny[3],Bramki_D1=4,Bramki_D2=0,dataMeczu=new DateTime(2018,5,10,18,00,00)},
                new Mecz{druzyna1=druzyny[2],druzyna2=druzyny[1],Bramki_D1=2,Bramki_D2=3,dataMeczu=new DateTime(2018,5,11,18,00,00)},
                new Mecz{druzyna1=druzyny[1],druzyna2=druzyny[0],dataMeczu=new DateTime(2018,5,10,21,00,00)}
            };
            mecze.ForEach(m => context.Mecze.Add(m));
            context.SaveChanges();

            var artykuly = new List<Artykul>
            {
                new Artykul{Profil=users[1],Tytul="Nie samowity powrot druzyny LFC!",Tresc="Wczorajszego wieczoru" +
                " bylismy swiadkami pokazu niesamowitej sily mentalnej" +
                " klubu Liverpool ktory mimo straty 3 bramek w pierwszym meczu.."},
                new Artykul{Profil=users[0],Tytul="Regulamin",Tresc="1. na tej stronie" +
                " nie wolno kibicowac united xD"}
            };
            artykuly.ForEach(a => context.Artykuly.Add(a));
            context.SaveChanges();

            var komentarze = new List<Komentarz>
            {
                new Komentarz{Artykul=artykuly[0],Profil=users[0],Tresc="To bylo niesamowite, Alisson MVP!"},
                new Komentarz{Artykul=artykuly[1],Profil=users[1],Tresc="xD"},
            };
            komentarze.ForEach(k => context.Komentarze.Add(k));
            context.SaveChanges();

            var listaTypow= new List<Typ>
           {
                 new Typ{Tprofile=users[1],Mecz=mecze[0],Bramki_D1=3,Bramki_D2=0},
                new Typ{Tprofile=users[1],Mecz=mecze[2],Bramki_D1=4,Bramki_D2=2},
                new Typ{Tprofile=users[0],Mecz=mecze[1],Bramki_D1=1,Bramki_D2=1}
           };
            listaTypow.ForEach(t => context.Typy.Add(t));
            context.SaveChanges();
        }
    }
}