using System;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models.Base
{
    public abstract class RdsModel
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Пользователь-создатель
        /// </summary>
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
    }
}
