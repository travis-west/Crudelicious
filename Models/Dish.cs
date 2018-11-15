using System;
using System.ComponentModel.DataAnnotations;
 
namespace Crudelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
         [MinLength(3)]
        [Display(Name="Name of Dish:")]
        public string Name { get; set; }

         [Required]
        [MinLength(3)]
        [Display(Name="Chef's Name:")]
        public string Chef { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required]
        [Range(1,5000)]
        [Display(Name="Number of Calories:")]
        public int Calories { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Dish(){
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

    }
}
