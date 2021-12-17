using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HotelRoomDTO
    {

        //Required for client side validation
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter room name")]
        public string Name { get; set; }
        [Range(1, 3000, ErrorMessage = "Please enter occupancy value")]
        public int Occupancy { get; set; }
        [Range(1, 3000, ErrorMessage = "Regular Rate must be between 1 and 3000")]
        public double Rate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }
        public List<string> ImageUrls { get; set; }
        public double StayLength { get; set; }
        public double Cost { get; set; }

        public bool IsBooked { get; set; }
    }
}
