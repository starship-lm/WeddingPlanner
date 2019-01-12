using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class Weddings
    {
        [Key]
        public int WeddingId {get;set;}
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="Bride's name must be 2 characters or more.")]
        public string Bride {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="Groom's name must be 2 characters or more.")]
        public string Groom {get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required]
        public string Address {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;}

        //navigation properties
        public List<Reservations> Reservations {get;set;}
        public Users Planner {get;set;} //the one who created the wedding, hence 'Planner', but I don't think we ever used this...?
    }
    public class Reservations
    {
        [Key]
        public int ReservationId {get;set;}
        public int WeddingId {get;set;}
        public int UserId {get;set;}

        //navigation properties
        // can we add public Weddings Weddings {get;set;}?? to get the 'wedding'?
        public Users Guest {get;set;} // why not List<Users>? to get all the people on the guest
    }
}