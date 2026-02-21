using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCardLibrary
{
    /// <summary>
    /// Представляет врача медицинского учреждения
    /// </summary>
    public class Doctor
    {
        private string name { get; }
        private DoctorSpecialty doctorSpecialty { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса Doctor с указанным именем и специальностью
        /// </summary>
        /// <param name="name">Имя врача</param>
        /// <param name="doctorSpecialty">Специальность врача</param>
        public Doctor(string name, DoctorSpecialty doctorSpecialty)
        {
            this.name = name;
            this.doctorSpecialty = doctorSpecialty;
        }

        /// <summary>
        /// Получает имя врача
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Получает специальность врача
        /// </summary>
        public DoctorSpecialty DoctorSpecialty => doctorSpecialty;
    }
}