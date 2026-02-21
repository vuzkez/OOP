using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCardLibrary
{
    /// <summary>
    /// Представляет пациента медицинского учреждения
    /// </summary>
    public class Patient
    {
        private string name { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса Patient с указанным именем
        /// </summary>
        /// <param name="name">Имя пациента</param>
        public Patient(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Получает имя пациента
        /// </summary>
        public string Name { get => name; }
    }
}