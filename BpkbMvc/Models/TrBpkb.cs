using System;
using System.ComponentModel.DataAnnotations;

namespace BpkbMvc.Models
{
    public class TrBpkb
    {
        [Required(ErrorMessage = "Agreement Number is required")]
        public string AgreementNumber { get; set; }

        [Required(ErrorMessage = "BPKB No is required")]
        public string BpkbNo { get; set; }

        [Required(ErrorMessage = "Branch ID is required")]
        public string BranchId { get; set; }

        [Required(ErrorMessage = "BPKB Date is required")]
        [DataType(DataType.Date)]
        public DateTime BpkbDate { get; set; }

        [Required(ErrorMessage = "Faktur No is required")]
        public string FakturNo { get; set; }

        [Required(ErrorMessage = "Faktur Date is required")]
        [DataType(DataType.Date)]
        public DateTime FakturDate { get; set; }

        [Required(ErrorMessage = "LocationId field is required")]
        public string LocationId { get; set; }

        [Required(ErrorMessage = "Police No is required")]
        public string PoliceNo { get; set; }

        [Required(ErrorMessage = "BPKB Date In is required")]
        [DataType(DataType.Date)]
        public DateTime BpkbDateIn { get; set; }

        public string CreatedBy { get; set; } // Otomatis diisi oleh Controller
        public string LastUpdatedBy { get; set; } // Otomatis diisi oleh Controller
        public DateTime? CreatedOn { get; set; } // Otomatis diisi oleh Controller
        public DateTime? LastUpdatedOn { get; set; } // Otomatis diisi oleh Controller
    }
}