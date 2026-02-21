using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCardLibrary
{
    /// <summary>
    /// Перечисление специальностей врачей
    /// </summary>
    public enum DoctorSpecialty
    {
        /// <summary>
        /// Кардиолог - специалист по заболеваниям сердечно-сосудистой системы
        /// </summary>
        cardiologist,

        /// <summary>
        /// Стоматолог - специалист по лечению заболеваний зубов и полости рта
        /// </summary>
        dentist,

        /// <summary>
        /// Дерматолог - специалист по заболеваниям кожи, волос и ногтей
        /// </summary>
        dermatologist,

        /// <summary>
        /// Онколог - специалист по диагностике и лечению опухолевых заболеваний
        /// </summary>
        oncologist,

        /// <summary>
        /// Психиатр - специалист по лечению психических расстройств
        /// </summary>
        psychiatrist
    }
}