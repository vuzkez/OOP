using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCardLibrary
{
    /// <summary>
    /// Представляет запись на прием к врачу
    /// </summary>
    public class Appointment
    {
        private DateTime date { get; }
        private Doctor doctor { get; }
        private Patient patient { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса Appointment
        /// </summary>
        /// <param name="date">Дата и время приема</param>
        /// <param name="doctor">Врач, к которому записан пациент</param>
        /// <param name="patient">Пациент, записанный на прием</param>
        public Appointment(DateTime date, Doctor doctor, Patient patient)
        {
            this.date = date;
            this.patient = patient;
            this.doctor = doctor;
        }

        /// <summary>
        /// Получает врача, к которому записан пациент
        /// </summary>
        public Doctor Doctor => doctor;

        /// <summary>
        /// Получает дату приема в формате короткой строки
        /// </summary>
        public string Date => date.ToShortDateString();

        /// <summary>
        /// Получает пациента, записанного на прием
        /// </summary>
        public Patient Patient => patient;
    }
}