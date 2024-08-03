using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Domain.Entities
{
    public class AdministrativeRegion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string NameEn { get; set; }

        [MaxLength(255)]
        public string CodeName { get; set; }

        [MaxLength(255)]
        public string CodeNameEn { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }

    public class AdministrativeUnit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(255)]
        public string FullNameEn { get; set; }

        [MaxLength(255)]
        public string ShortName { get; set; }

        [MaxLength(255)]
        public string ShortNameEn { get; set; }

        [MaxLength(255)]
        public string CodeName { get; set; }

        [MaxLength(255)]
        public string CodeNameEn { get; set; }

        public ICollection<Province> Provinces { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Ward> Wards { get; set; }
    }

    public class Province
    {
        [Key]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string NameEn { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(255)]
        public string FullNameEn { get; set; }

        [MaxLength(255)]
        public string CodeName { get; set; }

        public int? AdministrativeUnitId { get; set; }
        [ForeignKey("AdministrativeUnitId")]
        public AdministrativeUnit AdministrativeUnit { get; set; }

        public int? AdministrativeRegionId { get; set; }
        [ForeignKey("AdministrativeRegionId")]
        public AdministrativeRegion AdministrativeRegion { get; set; }

        public ICollection<District> Districts { get; set; }
    }

    public class District
    {
        [Key]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string NameEn { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(255)]
        public string FullNameEn { get; set; }

        [MaxLength(255)]
        public string CodeName { get; set; }

        [MaxLength(20)]
        public string ProvinceCode { get; set; }
        [ForeignKey("ProvinceCode")]
        public Province Province { get; set; }

        public int? AdministrativeUnitId { get; set; }
        [ForeignKey("AdministrativeUnitId")]
        public AdministrativeUnit AdministrativeUnit { get; set; }

        public ICollection<Ward> Wards { get; set; }
    }

    public class Ward
    {
        [Key]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string NameEn { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(255)]
        public string FullNameEn { get; set; }

        [MaxLength(255)]
        public string CodeName { get; set; }

        [MaxLength(20)]
        public string DistrictCode { get; set; }
        [ForeignKey("DistrictCode")]
        public District District { get; set; }

        public int? AdministrativeUnitId { get; set; }
        [ForeignKey("AdministrativeUnitId")]
        public AdministrativeUnit AdministrativeUnit { get; set; }
    }
}
