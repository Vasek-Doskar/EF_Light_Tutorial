using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClassroomId { get; set; }
        public Classroom? Clsroom { get; set; }


        public override string ToString()
        {
            return $"jméno:{Name}\ttřída: {Clsroom.Name}";
        }
    }
}