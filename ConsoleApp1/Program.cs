﻿using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;


// nainstalovat balíček přes PMC -> install-package microsoft.entityframeworkcore.sqlite

CheckDb();

LoadRecords();

//Console.ReadKey();
//DeleteStudentIfExists();
//Console.ReadKey();
//LoadRecords();
//Console.ReadKey();
//DeleteClsroom();
//LoadRecords();



static void CheckDb()
{
    using (var db = new Context())
    {
        if (db.Students.Count() == 0)
        {
            CreateRecords();
        }
    }
}

static void LoadRecords()
{
    Console.WriteLine("Načítám záznamy:\n");
    using (var db = new Context())
    {
        // vytažení třídy I1B z databáze a připojení jejich závislých objektů (Studenti)
        Classroom? I1B = db.Classrooms.Include(cls => cls.Students).FirstOrDefault(cls => cls.Name == "I1B");

        // výpis žáků I1B, podle abecedy
        I1B?.Students?.OrderBy(s => s.Name).ToList().ForEach(s => Console.WriteLine(s));
        Console.WriteLine("\nHotovo!\n");
    }

}

static void CreateRecords()
{   // vytvoření záznamů
    Console.WriteLine("Vytvářím záznamy:\n");
    using (var db = new Context())
    {
        db.Classrooms.Add(new Classroom
        {
            Name = "I1B"
        });

        db.Classrooms.Add(new Classroom
        {
            Name = "EA2"
        });

        db.Students.Add(new Student {  ClassroomId = 1, Name = "Vašek Doškář" });
        db.Students.Add(new Student {  ClassroomId = 1, Name = "Michal Velký" });
        db.Students.Add(new Student {  ClassroomId = 1, Name = "Patrik Zelený" });

        db.Students.Add(new Student { ClassroomId = 2, Name = "David Čápka" });

        // uložení záznamů do databáze - bez této metody se neuloží data nahraná v RAM do databáze.db
        db.SaveChanges();
        Console.WriteLine("Vytvořeno!\n");
    }
}


static void DeleteStudentIfExists(int id = 1)
{
    using (var db = new Context())
    {
        Student? vasek = db.Students.FirstOrDefault(s => s.Name == "Vašek Doškář");
        if (vasek != null)
        {
            db.Remove(vasek);
            db.SaveChanges();
            Console.WriteLine("\nZáznam smazán:\n");
        }
        else
        {
            Console.WriteLine("\nZáznam neexistuje!\n");
        }
    }
}

static void DeleteClsroom()
{
    using (var db = new Context())
    {
        Classroom i1b = db.Classrooms.First();
        db.Remove(i1b);
        db.SaveChanges();
        Console.WriteLine("I1B již neexistuje");
    }
}